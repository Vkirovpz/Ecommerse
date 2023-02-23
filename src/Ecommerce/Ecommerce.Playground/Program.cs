using Ecommerce;
using Ecommerce.Customer;
using Ecommerce.Customer.Commands;
using Ecommerce.Playground;
using Ecommerce.EntityFramework;
using Ecommerce.Projections;
using Ecommerce.Customer.Queries;

var type = typeof(IEventHandler<>);
var types = (from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetExportedTypes())
             from z in x.GetInterfaces()
             let y = x.BaseType
             where
             (y != null && y.IsGenericType && type.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
             (z.IsGenericType && type.IsAssignableFrom(z.GetGenericTypeDefinition()))
             select x).Distinct();

var eventsDbContext = new EcommerceEventsDbContext();
var dbContext = new EcommerceDbContext();
var projHandler = new EventSourcedProjectionWriter(types, eventsDbContext);
var shoppingCartRepository = new ShoppingCartRepository(projHandler, eventsDbContext);
var customerRepo = new CustomerRepository(shoppingCartRepository, projHandler, eventsDbContext);
var appService = new CustomerApplicationService(customerRepo);
//var queryService = new CustomerQueryService(dbContext);
var product = new Product(ProductSku.From("123"), ProductName.From("phone"), ProductPrice.From(100, "USD"));

var customerId = CustomerId.New();
await appService.HandleAsync(new CreateCustomer(customerId, FirstName.From("Valio"), LastName.From("Kirov")));
await appService.HandleAsync(new RenameCustomer(customerId, FirstName.From("Elder-42"), LastName.From("TestLastName")));
await appService.HandleAsync(new RenameCustomer(customerId, FirstName.From("Elder-42"), LastName.From("EldersLastName")));
await appService.HandleAsync(new AddProductToCart(customerId, product, 5));
await appService.HandleAsync(new AddProductToCart(customerId, product, 5));


var projLoader = new EventSourcedProjectionReader(eventsDbContext);
var proj = projLoader.Load<CustomerDetailProjection>(customerId.Value);

Console.WriteLine(proj.Id);
Console.WriteLine(proj.FirstName);
Console.WriteLine(proj.LastName);
Console.WriteLine();
Console.WriteLine("History:");
foreach (var name in proj.NameHistory)
{
    Console.WriteLine(name);
}
Console.WriteLine();
foreach (var sku in proj.BoughtProducts)
{
    Console.WriteLine(sku);
}

var productProjection = projLoader.Load<SkuDetailProjection>(product.Sku.Value);
Console.WriteLine(productProjection.ProductInfo);
Console.WriteLine();
foreach (var purchase in productProjection.PurchaseHistory)
{
    Console.WriteLine($"{purchase.customerName}, {purchase.quantity}");
}















//var result = await queryService.HandleAsync(new GetCustomerById(customerId.Value));
//if (result.Success)
//    Console.WriteLine(result.Result.FullName);


//await appService.HandleAsync(new RemoveProductFromCart(customerId, product, 2));

//var customer2Id = CustomerId.New();
//await appService.HandleAsync(new CreateCustomer(customer2Id, FirstName.From("TestFirstName"), LastName.From("TestLastName")));

Console.ReadLine();

using Ecommerce;
using Ecommerce.Customer;
using Ecommerce.Customer.Commands;
using Ecommerce.Playground;

var type = typeof(IEventHandler<>);
var types = (from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetExportedTypes())
            from z in x.GetInterfaces()
            let y = x.BaseType
            where
            (y != null && y.IsGenericType && type.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
            (z.IsGenericType && type.IsAssignableFrom(z.GetGenericTypeDefinition()))
            select x).Distinct();

var shoppingCartRepository = new ShoppingCartRepository(new ProjectionHandler(types));
var customerRepo = new CustomerRepository(shoppingCartRepository, new ProjectionHandler(types));
var appService = new CustomerApplicationService(customerRepo);
var product = new Product(ProductSku.From("123"), ProductName.From("phone"), ProductPrice.From(100, "USD"));

var customerId = CustomerId.New();
await appService.HandleAsync(new CreateCustomer(customerId, FirstName.From("Valio"), LastName.From("Kirov")));
await appService.HandleAsync(new AddProductToCart(customerId, product, 5));
await appService.HandleAsync(new RemoveProductFromCart(customerId, product, 2));

//var gg = await customerRepo.LoadAsync(customerId.Value);
//var id2 = Guid.NewGuid().ToString();
//await appService.HandleAsync(new CreateCustomer(id2, "Customer2", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id2, "Customer222", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id2, "Customer22", "asdfasdfa"));

Console.ReadLine();

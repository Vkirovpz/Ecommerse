using Ecommerce;
using Ecommerce.Customer;
using Ecommerce.Customer.Commands;
using Ecommerce.Playground;

var shoppingCartRepository = new ShoppingCartRepository();
var customerRepo = new CustomerRepository(shoppingCartRepository);
var appService = new CustomerApplicationService(customerRepo);
var product = new Product("123", "phone", 200);

var customerId = CustomerId.New();
await appService.HandleAsync(new CreateCustomer(customerId, FirstName.From("Valio"), LastName.From("Kirov")));
//await appService.HandleAsync(new RenameCustomer(id, "Valention", "Kirov"));
await appService.HandleAsync(new AddProductToCart(customerId, product, 5));

//await appService.HandleAsync(new AddProductToCart(customerId, product, 2));
await appService.HandleAsync(new RemoveProductFromCart(customerId, product, 2));

//var gg = await customerRepo.LoadAsync(customerId.Value);
//var id2 = Guid.NewGuid().ToString();
//await appService.HandleAsync(new CreateCustomer(id2, "Customer2", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id2, "Customer222", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id2, "Customer22", "asdfasdfa"));

Console.ReadLine();

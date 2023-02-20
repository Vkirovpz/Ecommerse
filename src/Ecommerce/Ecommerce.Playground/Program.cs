using Ecommerce;
using Ecommerce.Customer;
using Ecommerce.Customer.Commands;
using Ecommerce.Playground;

var shoppingCartRepository = new ShoppingCartRepository();
var appService = new CustomerApplicationService(new CustomerRepository(shoppingCartRepository));
var product = new Product("123", "phone", 2);

var customerId = Guid.NewGuid().ToString();
await appService.HandleAsync(new CreateCustomer(customerId, "Valio", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id, "Valention", "Kirov"));
await appService.HandleAsync(new AddProductToCart(customerId, product, 5));
await appService.HandleAsync(new AddProductToCart(customerId, product, 2));
await appService.HandleAsync(new RemoveProductFromCart(customerId, product));

//var id2 = Guid.NewGuid().ToString();
//await appService.HandleAsync(new CreateCustomer(id2, "Customer2", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id2, "Customer222", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id2, "Customer22", "asdfasdfa"));

Console.ReadLine();

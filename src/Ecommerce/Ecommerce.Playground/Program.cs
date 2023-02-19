using Ecommerce;
using Ecommerce.Customer;
using Ecommerce.Customer.Commands;

var appService = new CustomerApplicationService(new CustomerRepository());
var product = new Product("123", "Iphone", 2);

var id = Guid.NewGuid().ToString();
await appService.HandleAsync(new CreateCustomer(id, "Valio", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id, "Valention", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id, "asdf", "asdfasdfa"));
await appService.HandleAsync(new AddToCart(id, product, 2));
await appService.HandleAsync(new RemoveFromCart(id, product));

//var id2 = Guid.NewGuid().ToString();
//await appService.HandleAsync(new CreateCustomer(id2, "Customer2", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id2, "Customer222", "Kirov"));
//await appService.HandleAsync(new RenameCustomer(id2, "Customer22", "asdfasdfa"));

Console.ReadLine();

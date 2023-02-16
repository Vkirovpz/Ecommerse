using Ecommerce.Customer;
using Ecommerce.Customer.Commands;

var appService = new CustomerApplicationService(new CustomerRepository());

var id = Guid.NewGuid().ToString();
await appService.HandleAsync(new CreateCustomer(id, "Valio", "Kirov"));
await appService.HandleAsync(new RenameCustomer(id, "Valention", "Kirov"));
await appService.HandleAsync(new RenameCustomer(id, "asdf", "asdfasdfa"));

var id2 = Guid.NewGuid().ToString();
await appService.HandleAsync(new CreateCustomer(id2, "Customer2", "Kirov"));
await appService.HandleAsync(new RenameCustomer(id2, "Customer222", "Kirov"));
await appService.HandleAsync(new RenameCustomer(id2, "Customer22", "asdfasdfa"));

Console.ReadLine();

namespace Ecommerce.Customer.Commands
{
    public class CreateCustomer : ICommand
    {
        public CreateCustomer(CustomerId id, FirstName firstName, LastName lastName)
        { 
            Id = id ?? throw new ArgumentNullException(nameof(id));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public CustomerId Id { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
    }
}
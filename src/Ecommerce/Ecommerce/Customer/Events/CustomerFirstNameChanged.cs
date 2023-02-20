namespace Ecommerce.Customer.Events
{
    public class CustomerFirstNameChanged : IEvent
    {
        public CustomerFirstNameChanged(CustomerId id, FirstName oldFirstName, FirstName newFirstName)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            OldFirstName = oldFirstName ?? throw new ArgumentNullException(nameof(oldFirstName));
            NewFirstName = newFirstName ?? throw new ArgumentNullException(nameof(newFirstName));
        }

        public CustomerId Id { get; }
        public FirstName OldFirstName { get; }
        public FirstName NewFirstName { get; }
    }
}
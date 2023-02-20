namespace Ecommerce.Customer.Events
{
    public class CustomerLastNameChanged : IEvent
    {
        public CustomerLastNameChanged(CustomerId id, LastName oldLastName, LastName newLastName)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            OldLastName = oldLastName ?? throw new ArgumentNullException(nameof(oldLastName));
            NewLastName = newLastName ?? throw new ArgumentNullException(nameof(newLastName));
        }

        public CustomerId Id { get; }
        public LastName OldLastName { get; }
        public LastName NewLastName { get; }
    }
}
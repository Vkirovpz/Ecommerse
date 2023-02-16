using Ecommerce.Customer.Events;

namespace Ecommerce.Customer
{
    public class CustomerState
    {
        public Queue<IEvent> UnsavedEvents { get; } = new Queue<IEvent>();

        public string Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public ShoppingCart Cart { get; private set; }

        private void When(CustomerCreated e)
        {
            Id = e.Id;
            FirstName = e.FirstName;
            LastName = e.LastName;
        }

        private void When(CustomerFirstNameChanged e)
        {
            FirstName = e.NewFirstName;
        }

        private void When(CustomerLastNameChanged e)
        {
            LastName = e.NewLastName;
        }

        public void Apply(IEvent @event)
        {
            UnsavedEvents.Enqueue(@event);
            When((dynamic)@event);
        }

        public void Restore(IEvent @event)
        {
            When((dynamic)@event);
        }
    }
}
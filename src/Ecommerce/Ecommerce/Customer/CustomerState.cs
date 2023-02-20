using Ecommerce.Customer.Events;
using Ecommerce.Cart;

namespace Ecommerce.Customer
{
    public class CustomerState
    {
        public Queue<IEvent> UnsavedEvents { get; } = new Queue<IEvent>();

        public CustomerId Id { get; private set; }

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

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

        private void When(ShoppingCartCreated e)
        {
            Cart = new ShoppingCart(e.Id);
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

        public void SetCart(ShoppingCart cart) 
        { 
            Cart = cart;
        }
    }
}
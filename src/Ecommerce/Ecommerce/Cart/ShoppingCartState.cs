using Ecommerce.Cart.Events;
using Ecommerce.Customer;

namespace Ecommerce.Cart
{
    public class ShoppingCartState
    {
        public Queue<IEvent> UnsavedEvents { get; } = new Queue<IEvent>();

        public string Id { get; private set; }

        public CustomerAggregate Customer { get; private set; }

        public HashSet<ShoppingCartItem> Items { get; private set; }

        private void When(ShoppingCartCreated e)
        {
            Id = e.Id;
            Customer = e.Customer;
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

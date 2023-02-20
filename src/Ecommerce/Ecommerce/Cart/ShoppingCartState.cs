using Ecommerce.Cart.Events;
using Ecommerce.Customer;

namespace Ecommerce.Cart
{
    public class ShoppingCartState
    {
        public Queue<IEvent> UnsavedEvents { get; } = new Queue<IEvent>();

        public string Id { get; private set; }
        public string CustomerId { get; private set; }

        public CustomerAggregate Customer { get; private set; }

        public HashSet<ShoppingCartItem> Items { get; private set; } = new();

        private void When(ItemAddedToShoppingCart e)
        {
            Items.Add(e.Item);
        }

        private void When(ItemQuantityIncreased e)
        {
            var item = Items.First(i => i == e.Item);
            item.IncreaseQuantity(e.NewQuantity);
        }

        private void When(ItemRemovedFromShoppingCart e)
        {
            var item = Items.FirstOrDefault(i => i.Product.Sku == e.Sku);
            Items.Remove(item);
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

        public void setId(string id)
        {
            Id= id;
        }

        public void setCustomerId(string customerId)
        {
            CustomerId= customerId;
        }
    }
}

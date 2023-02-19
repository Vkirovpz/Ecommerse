using Ecommerce.Cart.Events;
using Ecommerce.Customer;
using Ecommerce.Customer.Events;

namespace Ecommerce.Cart
{
    public class ShoppingCartState
    {
        public Queue<IEvent> UnsavedEvents { get; } = new Queue<IEvent>();

        public string Id { get; private set; }
        public string CustomerId { get; private set; }

        public CustomerAggregate Customer { get; private set; }

        public HashSet<ShoppingCartItem> Items { get; private set; }

        private void When(ShoppingCartCreated e)
        {
            Id = e.Id;
            CustomerId = e.CustomerId;
        }

        private void When(ItemAddedToShoppingCart e)
        {
            var shoppingCartItem = Items.FirstOrDefault(i => i.Product.Sku == e.Product.Sku);
            if (shoppingCartItem is null)
            {
                var item = new ShoppingCartItem(e.Product, e.Quantity);
                Items.Add(item);
            }
            else
            {
                shoppingCartItem.IncreaseQuantity(e.Quantity);
            }
            
        }

        private void When(ItemRemovedFromShoppingCart e)
        {
            var shoppingCartItem = Items.FirstOrDefault(i => i.Product.Sku == e.Sku);
            if (shoppingCartItem is null) return;
            Items.Remove(shoppingCartItem);
     
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

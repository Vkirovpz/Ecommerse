using Ecommerce.Cart.Events;
using Ecommerce.Customer;

namespace Ecommerce.Cart
{
    public class ShoppingCartState
    {
        public ShoppingCartState(ShoppingCartId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public Queue<IEvent> UnsavedEvents { get; } = new Queue<IEvent>();

        public ShoppingCartId Id { get; private set; }

        public CustomerAggregate Customer { get; private set; }

        public List<ShoppingCartItem> Items { get; } = new();

        public void Apply(IEvent @event)
        {
            UnsavedEvents.Enqueue(@event);
            When((dynamic)@event);
        }

        public void Restore(IEvent @event)
        {
            When((dynamic)@event);
        }

        public class ShoppingCartItem
        {
            public ShoppingCartItem(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }

            public Product Product { get; }

            public int Quantity { get; private set; }

            public void IncreaseQuantity(int count)
            {
                Quantity += count;
            }
        }

        private void When(ProductAddedToShoppingCart e)
        {
            Items.Add(new ShoppingCartItem(e.Product, e.Quantity));
        }

        private void When(ProductQuantityIncreased e)
        {
            var item = Items.FirstOrDefault(i => i.Product == e.Product);
            item.IncreaseQuantity(e.NewQuantity);
        }

        private void When(ProductRemovedFromShoppingCart e)
        {
            var item = Items.FirstOrDefault(i => i.Product.Sku == e.Sku);
            Items.Remove(item);
        }
    }
}

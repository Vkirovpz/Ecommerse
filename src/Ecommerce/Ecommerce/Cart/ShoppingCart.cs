using Ecommerce.Cart.Events;
using Ecommerce.Customer.Events;

namespace Ecommerce.Cart
{
    public class ShoppingCart
    {
        public ShoppingCart(ShoppingCartState state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public ShoppingCart(string id, string customerId)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            if (string.IsNullOrEmpty(customerId)) throw new ArgumentException($"'{nameof(customerId)}' cannot be null or empty.", nameof(customerId));

            State = new ShoppingCartState();

            var @event = new ShoppingCartCreated(id, customerId);
            State.Apply(@event);
        }

        public ShoppingCartState State { get;}

        public void AddProduct(Product product, int quantity)
        {
            State.Apply(new ItemAddedToShoppingCart(product, quantity));
        }

        public void RemoveProduct(string sku)
        {
            State.Apply(new ItemRemovedFromShoppingCart(sku));
        }
    }
}
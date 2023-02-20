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
            State.setId(id);
            State.setCustomerId(customerId);
            //State.Apply();
        }

        public ShoppingCartState State { get;}

        public void AddProduct(Product product, int quantity)
        {
            var shoppingCartItem = State.Items.FirstOrDefault(i => i.Product.Sku == product.Sku);
            if (shoppingCartItem is null)
            {
                var item = new ShoppingCartItem(product, quantity);
                State.Apply(new ItemAddedToShoppingCart(item));
            }
            else
            {
                State.Apply(new ItemQuantityIncreased(shoppingCartItem, shoppingCartItem.Quantity, quantity));
            }

        }

        public void RemoveProduct(string sku)
        {
            var shoppingCartItem = State.Items.FirstOrDefault(i => i.Product.Sku == sku);
            if (shoppingCartItem is null) return;
            State.Apply(new ItemRemovedFromShoppingCart(sku));
        }
    }
}
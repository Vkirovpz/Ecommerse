using Ecommerce.Cart.Events;

namespace Ecommerce.Cart
{
    public class ShoppingCart
    {
        public ShoppingCart(ShoppingCartState state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public ShoppingCart(ShoppingCartId id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            State = new ShoppingCartState(id);
        }

        public ShoppingCartState State { get; }

        public void AddProduct(Product product, int quantity)
        {
            var shoppingCartItem = State.Items.FirstOrDefault(i => i.Product.Sku == product.Sku);
            if (shoppingCartItem is null)
                State.Apply(new ProductAddedToShoppingCart(State.Id, product, quantity));
            else
                State.Apply(new ProductQuantityIncreased(State.Id, product, shoppingCartItem.Quantity, quantity));
        }

        public void RemoveProduct(string sku)
        {
            var shoppingCartItem = State.Items.FirstOrDefault(i => i.Product.Sku == sku);
            if (shoppingCartItem is null)
                return;

            State.Apply(new ProductRemovedFromShoppingCart(State.Id, sku));
        }
    }
}
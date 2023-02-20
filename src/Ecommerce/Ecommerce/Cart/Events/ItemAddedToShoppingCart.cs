namespace Ecommerce.Cart.Events
{
    public class ItemAddedToShoppingCart : IEvent
    {
        public ItemAddedToShoppingCart(ShoppingCartItem item)
        {
            Item = item;
        }

        public ShoppingCartItem Item { get; }
    }
}

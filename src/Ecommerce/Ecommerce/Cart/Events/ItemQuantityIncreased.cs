namespace Ecommerce.Cart.Events
{
    public class ItemQuantityIncreased : IEvent
    {
        public ItemQuantityIncreased(ShoppingCartItem item, int oldQuantity, int newQuantity)
        {
            Item = item;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }

        public ShoppingCartItem Item { get; }

        public int OldQuantity { get; }
        public int NewQuantity { get; }
    }
}

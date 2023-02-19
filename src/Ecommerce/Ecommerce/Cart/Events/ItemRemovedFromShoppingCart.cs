namespace Ecommerce.Cart.Events
{
    public class ItemRemovedFromShoppingCart : IEvent
    {
        public ItemRemovedFromShoppingCart(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }
}

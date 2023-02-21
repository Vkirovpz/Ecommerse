namespace Ecommerce.Cart.Events
{
    public class ProductRemovedFromShoppingCart : IEvent
    {
        public ProductRemovedFromShoppingCart(ShoppingCartId id, ProductSku sku)
        {


            Id = id ?? throw new ArgumentNullException(nameof(id));
            Sku = sku ?? throw new ArgumentNullException(nameof(sku));
        }

        public ShoppingCartId Id { get; }

        public ProductSku Sku { get; }
    }
}

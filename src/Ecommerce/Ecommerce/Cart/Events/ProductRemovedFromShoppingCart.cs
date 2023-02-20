namespace Ecommerce.Cart.Events
{
    public class ProductRemovedFromShoppingCart : IEvent
    {
        public ProductRemovedFromShoppingCart(ShoppingCartId id, string sku)
        {
            if (string.IsNullOrEmpty(sku)) throw new ArgumentException($"'{nameof(sku)}' cannot be null or empty.", nameof(sku));

            Id = id ?? throw new ArgumentNullException(nameof(id));
            Sku = sku;
        }

        public ShoppingCartId Id { get; }

        public string Sku { get; }
    }
}

namespace Ecommerce.Cart.Events
{
    public class ProductAddedToShoppingCart : IEvent
    {
        public ProductAddedToShoppingCart(ShoppingCartId id, Product product, int quantity)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity;
        }

        public ShoppingCartId Id { get; }
        public Product Product { get; }
        public int Quantity { get; }
    }
}

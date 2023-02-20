namespace Ecommerce.Cart.Events
{
    public class ProductQuantityIncreased : IEvent
    {
        public ProductQuantityIncreased(ShoppingCartId id, Product product, int oldQuantity, int newQuantity)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Product = product ?? throw new ArgumentNullException(nameof(product));
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }

        public ShoppingCartId Id { get; }

        public Product Product { get; }

        public int OldQuantity { get; }

        public int NewQuantity { get; }
    }
}

namespace Ecommerce
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem(Product product, int quantity)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException();

            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public int Quantity { get; }
    }
}
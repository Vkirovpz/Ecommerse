namespace Ecommerce
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public int Quantity { get; private set; }

        public void IncreaseQuantity(int count)
        {
            Quantity += count;
        }
    }
}
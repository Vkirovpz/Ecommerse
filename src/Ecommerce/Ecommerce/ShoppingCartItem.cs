namespace Ecommerce
{
    public class ShoppingCartItem
    {
        
        public Product Product { get; }
        public int Quantity { get; private set; }
    }
}
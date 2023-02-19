namespace Ecommerce.Cart.Events
{
    public class ItemAddedToShoppingCart : IEvent
    {
        public ItemAddedToShoppingCart(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public int Quantity { get; }
    }
}

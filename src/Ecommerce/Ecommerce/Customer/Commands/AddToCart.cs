namespace Ecommerce.Customer.Commands
{
    public class AddToCart: ICommand
    {
        public AddToCart(Product product, int quantity)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException();

            Product = product;
            Quantity = quantity;
        }

        public Product Product { get;}
        public int Quantity { get;}
    }
}

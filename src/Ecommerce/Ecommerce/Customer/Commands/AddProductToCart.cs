namespace Ecommerce.Customer.Commands
{
    public class AddProductToCart: ICommand
    {
        public AddProductToCart(CustomerId id, Product product, int quantity)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException();

            Id = id ?? throw new ArgumentNullException(nameof(id));
            Product = product;
            Quantity = quantity;
        }
        public CustomerId Id { get; }
        public Product Product { get;}
        public int Quantity { get;}
    }
}

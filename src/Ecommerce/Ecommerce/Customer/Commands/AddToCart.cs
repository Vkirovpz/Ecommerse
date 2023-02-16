namespace Ecommerce.Customer.Commands
{
    public class AddToCart: ICommand
    {
        public AddToCart(string id, Product product, int quantity)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));

            if (product is null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException();

            Product = product;
            Quantity = quantity;
        }
        public string Id { get; }
        public Product Product { get;}
        public int Quantity { get;}
    }
}

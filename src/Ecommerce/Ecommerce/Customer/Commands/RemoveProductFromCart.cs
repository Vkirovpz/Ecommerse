namespace Ecommerce.Customer.Commands
{
    public class RemoveProductFromCart : ICommand
    {
        public RemoveProductFromCart(string customerid, Product product)
        {
            if (string.IsNullOrEmpty(customerid)) throw new ArgumentException($"'{nameof(customerid)}' cannot be null or empty.", nameof(customerid));

            if (product is null) throw new ArgumentNullException(nameof(product));
            Customerid = customerid;
            Product = product;
        }

        public string Customerid { get; }
        public Product Product { get; }
    }
}

namespace Ecommerce.Customer.Commands
{
    public class RemoveProductFromCart : ICommand
    {
        public RemoveProductFromCart(CustomerId customerid, Product product, int quantity)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));

            Customerid = customerid ?? throw new ArgumentNullException(nameof(customerid));
            Product = product;
            Quantity = quantity;
        }

        public CustomerId Customerid { get; }

        public Product Product { get; }
        public int Quantity { get; }
    }
}

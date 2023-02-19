namespace Ecommerce
{
    public class Product
    {
        public Product(string sku, string name, decimal price)
        {
            Sku = sku;
            Name = name;
            Price = price;
        }

        public string Sku { get; }
        public string Name { get; }
        public decimal Price { get; }
    }
}
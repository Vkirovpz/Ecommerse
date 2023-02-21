namespace Ecommerce
{
    public class Product : IEquatable<Product>
    {
        public Product(ProductSku sku, ProductName name, ProductPrice price)
        {
            Sku = sku ?? throw new ArgumentNullException(nameof(sku));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public ProductSku Sku { get; }
        public ProductName Name { get; }
        public ProductPrice Price { get; }

        public override int GetHashCode() => HashCode.Combine(Sku, Name, Price);

        public override bool Equals(object obj) => Equals(obj as Product);

        public bool Equals(Product other)
        {
            if (other is null) return false;

            return other.Name.Equals(Name)
                && other.Price == Price
                && other.Sku == Sku;
        }

        public static bool operator ==(Product obj1, Product obj2) => obj1.Equals(obj2);
        public static bool operator !=(Product obj1, Product obj2) => (obj1 == obj2) == false;
    }
}
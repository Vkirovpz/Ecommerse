namespace Ecommerce
{
    public class Product : IEquatable<Product>
    {
        public Product(string sku, string name, decimal price)
        {
            if (string.IsNullOrEmpty(sku)) throw new ArgumentException($"'{nameof(sku)}' cannot be null or empty.", nameof(sku));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));

            Sku = sku;
            Name = name;
            Price = price;
        }

        public string Sku { get; }
        public string Name { get; }
        public decimal Price { get; }

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
using Ecommerce.Customer;

namespace Ecommerce
{
    public class ProductName : IEquatable<ProductName>
    {
        public ProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            Value = value;
        }

        public string Value { get; }

        public static ProductName From(string value) => new ProductName(value);

        public override string ToString() => Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as FirstName);

        public bool Equals(ProductName other)
        {
            if (other is null)
                return false;

            return other.Value.Equals(Value);
        }

        public static bool operator ==(ProductName obj1, ProductName obj2) => obj1.Equals(obj2);
        public static bool operator !=(ProductName obj1, ProductName obj2) => (obj1 == obj2) == false;

        public static implicit operator string(ProductName s) => s.Value;

        public static explicit operator ProductName(string v) => new(v);
    }
}

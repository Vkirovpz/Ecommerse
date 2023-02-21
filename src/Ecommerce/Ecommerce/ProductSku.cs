using Ecommerce.Customer;

namespace Ecommerce
{
    public class ProductSku : IEquatable<ProductSku>
    {
        public ProductSku(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            Value = value;
        }

        public string Value { get; }

        public static ProductSku From(string value) => new ProductSku(value);

        public override string ToString() => Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as ProductSku);

        public bool Equals(ProductSku other)
        {
            if (other is null)
                return false;

            return other.Value.Equals(Value);
        }

        public static bool operator ==(ProductSku obj1, ProductSku obj2) => obj1.Equals(obj2);
        public static bool operator !=(ProductSku obj1, ProductSku obj2) => (obj1 == obj2) == false;
    }
}

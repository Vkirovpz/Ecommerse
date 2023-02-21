using System.Text.Json.Serialization;

namespace Ecommerce
{
    public class ProductPrice : IEquatable<ProductPrice>
    {
        public ProductPrice(string price)
        {
            if (string.IsNullOrWhiteSpace(price)) throw new ArgumentException($"'{nameof(price)}' cannot be null or whitespace.", nameof(price));

            var parts = price.Split(":");
            if (parts.Length != 2)
                throw new InvalidOperationException($"Invalid shopping price '{price}'");

            Price = decimal.Parse(parts[0]);
            Currency = parts[1];
        }

        [JsonConstructor]
        public ProductPrice(decimal price, string currency)
        {
            if (string.IsNullOrEmpty(currency)) throw new ArgumentException($"'{nameof(currency)}' cannot be null or empty.", nameof(currency));

            Price = price;
            Currency = currency;
        }

        public decimal Price { get; }
        public string Currency { get; }
        public string Value => $"{Price}:{Currency}";

        public override string ToString() => Value;
        public override int GetHashCode() => HashCode.Combine(Price, Currency);
        public override bool Equals(object obj) => Equals(obj as ProductPrice);

        public bool Equals(ProductPrice other)
        {
            if (other is null)
                return false;

            return other.Price.Equals(Price)
                && other.Currency.Equals(Currency);
        }

        public static bool operator ==(ProductPrice obj1, ProductPrice obj2) => obj1.Equals(obj2);
        public static bool operator !=(ProductPrice obj1, ProductPrice obj2) => (obj1 == obj2) == false;
    }
}

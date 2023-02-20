using Ecommerce.Customer;
using System.Text.Json.Serialization;

namespace Ecommerce.Cart
{
    public class ShoppingCartId : IEquatable<ShoppingCartId>
    {
        public ShoppingCartId(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var parts = id.Split(":");
            if (parts.Length != 2)
                throw new InvalidOperationException($"Invalid shopping cart id '{id}'");

            CustomerId = CustomerId.New(parts[0]);
            Id = parts[1];
        }

        [JsonConstructor]
        public ShoppingCartId(CustomerId customerId, string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            CustomerId = customerId;
            Id = id;
        }

        public CustomerId CustomerId { get; }

        public string Id { get; }

        public string Value => $"{CustomerId}:{Id}";

        public override string ToString() => Value;
        public override int GetHashCode() => HashCode.Combine(CustomerId, Id);
        public override bool Equals(object obj) => Equals(obj as ShoppingCartId);

        public bool Equals(ShoppingCartId other)
        {
            if (other is null)
                return false;

            return other.Id.Equals(Id)
                && other.CustomerId.Equals(CustomerId);
        }

        public static bool operator ==(ShoppingCartId obj1, ShoppingCartId obj2) => obj1.Equals(obj2);
        public static bool operator !=(ShoppingCartId obj1, ShoppingCartId obj2) => (obj1 == obj2) == false;
    }
}
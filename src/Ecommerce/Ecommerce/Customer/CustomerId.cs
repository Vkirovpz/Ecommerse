namespace Ecommerce.Customer
{
    public class CustomerId : IEquatable<CustomerId>
    {
        public CustomerId(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            Value = value;
        }

        public string Value { get; private set; }

        public static CustomerId New(string value) => new (value);

        public static CustomerId New() => new(Guid.NewGuid().ToString());

        public override string ToString() => Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as CustomerId);

        public bool Equals(CustomerId other)
        {
            if (other is null)
                return false;

            return other.Value.Equals(Value);
        }

        public static bool operator ==(CustomerId obj1, CustomerId obj2) => obj1.Equals(obj2);
        public static bool operator !=(CustomerId obj1, CustomerId obj2) => (obj1 == obj2) == false;
    }
}
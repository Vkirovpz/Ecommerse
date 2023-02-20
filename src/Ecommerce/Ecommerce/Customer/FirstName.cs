namespace Ecommerce.Customer
{
    public class FirstName : IEquatable<FirstName>
    {
        public FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            Value = value;
        }

        public string Value { get; }

        public static FirstName From(string value) => new FirstName(value);

        public override string ToString() => Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as FirstName);

        public bool Equals(FirstName other)
        {
            if (other is null)
                return false;

            return other.Value.Equals(Value);
        }

        public static bool operator ==(FirstName obj1, FirstName obj2) => obj1.Equals(obj2);
        public static bool operator !=(FirstName obj1, FirstName obj2) => (obj1 == obj2) == false;
    }
}
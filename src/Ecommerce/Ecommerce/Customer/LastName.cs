namespace Ecommerce.Customer
{
    public class LastName : IEquatable<LastName>
    {
        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            Value = value;
        }

        public string Value { get; }

        public static LastName From(string value) => new LastName(value);

        public override string ToString() => Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as LastName);

        public bool Equals(LastName other)
        {
            if (other is null)
                return false;

            return other.Value.Equals(Value);
        }

        public static bool operator ==(LastName obj1, LastName obj2) => obj1.Equals(obj2);
        public static bool operator !=(LastName obj1, LastName obj2) => (obj1 == obj2) == false;
    }
}
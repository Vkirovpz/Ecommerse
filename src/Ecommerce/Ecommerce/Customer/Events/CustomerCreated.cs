namespace Ecommerce.Customer.Events
{
    public class CustomerCreated : IEvent
    {
        public CustomerCreated(string id, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException($"'{nameof(firstName)}' cannot be null or empty.", nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException($"'{nameof(lastName)}' cannot be null or empty.", nameof(lastName));

            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
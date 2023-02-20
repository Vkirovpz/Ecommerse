namespace Ecommerce.Customer.Commands
{
    public class RenameCustomer : ICommand
    {
        public RenameCustomer(string id, FirstName firstName, LastName lastName)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));

            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public string Id { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
    }
}

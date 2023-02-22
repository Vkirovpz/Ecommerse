namespace Ecommerce.Customer.Queries
{
    public class GetCustomerById : IQuery
    {
        public GetCustomerById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            Id = id;
        }

        public string Id { get; }

    }
}

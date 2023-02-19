namespace Ecommerce.Customer.Events
{
    public class ShoppingCartCreated : IEvent
    {
        public ShoppingCartCreated(string id, string customerId)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            if (string.IsNullOrEmpty(customerId)) throw new ArgumentException($"'{nameof(customerId)}' cannot be null or empty.", nameof(customerId));

            Id = id;
            CustomerId = customerId;

        }

        public string Id { get; }
        public string CustomerId { get; }
        public CustomerAggregate Customer { get; }

    }
}

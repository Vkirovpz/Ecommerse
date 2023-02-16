using Ecommerce.Customer;

namespace Ecommerce.Cart.Events
{
    public class ShoppingCartCreated : IEvent
    {
        public ShoppingCartCreated(string id, CustomerAggregate customer)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            if (customer is null) throw new ArgumentNullException(nameof(customer));

            Id = id;
            Customer = customer;

        }

        public string Id { get; }
        public CustomerAggregate Customer { get; }

    }
}

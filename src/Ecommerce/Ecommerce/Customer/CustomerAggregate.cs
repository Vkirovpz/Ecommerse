using Ecommerce.Customer.Events;

namespace Ecommerce.Customer
{
    public class CustomerAggregate
    {
        public CustomerAggregate(CustomerState state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public CustomerAggregate(string id, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException($"'{nameof(firstName)}' cannot be null or empty.", nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException($"'{nameof(lastName)}' cannot be null or empty.", nameof(lastName));

            State = new CustomerState();

            var @event = new CustomerCreated(id, firstName, lastName);
            State.Apply(@event);
        }

        public CustomerState State { get; }

        public void AddToCart(Product product, int quantity)
        {
            State.Cart.AddProduct(product, quantity);
        }

        public void RemoveFromCart(Product product)
        {
            State.Cart.RemoveProduct(product.Sku);
        }

        public void Rename(string firstName, string lastName)
        {
            if (firstName != State.FirstName)
                State.Apply(new CustomerFirstNameChanged(State.Id, State.FirstName, firstName));

            if (lastName != State.LastName)
                State.Apply(new CustomerLastNameChanged(State.Id, State.LastName, lastName));
        }
    }
}
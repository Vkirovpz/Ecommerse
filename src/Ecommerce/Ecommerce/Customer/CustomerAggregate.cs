using Ecommerce.Customer.Events;

namespace Ecommerce.Customer
{
    public class CustomerAggregate
    {
        public CustomerAggregate(CustomerState state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public CustomerAggregate(CustomerId id, FirstName firstName, LastName lastName)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            if (firstName is null) throw new ArgumentNullException(nameof(firstName));
            if (lastName is null) throw new ArgumentNullException(nameof(lastName));

            State = new CustomerState();

            var @event = new CustomerCreated(id, firstName, lastName);
            State.Apply(@event);
        }

        public CustomerState State { get; }

        public void AddProductToCart(Product product, int quantity)
        {
            if (State.Cart is null)
            {
                var cartId = Guid.NewGuid().ToString();
                State.Apply(new ShoppingCartCreated(new Cart.ShoppingCartId(State.Id, cartId)));
            }

            State.Cart.AddProduct(product, quantity);
        }

        public void RemoveProductFromCart(Product product, int quantity)
        {
            State.Cart.RemoveProduct(product, quantity);
        }

        public void Rename(FirstName firstName, LastName lastName)
        {
            if (firstName != State.FirstName)
                State.Apply(new CustomerFirstNameChanged(State.Id, State.FirstName, firstName));

            if (lastName != State.LastName)
                State.Apply(new CustomerLastNameChanged(State.Id, State.LastName, lastName));
        }
    }
}
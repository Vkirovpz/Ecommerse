using Ecommerce.Cart.Events;
using Ecommerce.Customer;
using Ecommerce.Customer.Events;

namespace Ecommerce.Cart
{
    public class ShoppingCart
    {
        public ShoppingCart(ShoppingCartState state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public ShoppingCart(string id, CustomerAggregate customer)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            if (customer is null) throw new ArgumentNullException(nameof(customer));

            State = new ShoppingCartState();

            var @event = new ShoppingCartCreated(id, customer);
            State.Apply(@event);
        }

        public ShoppingCartState State { get;}

        public void AddProduct(Product product, int quantity)
        {
            //State.Apply(ItemAddedToShoppingCart);

        }

        public void RemoveProduct(string sku)
        {

        }
    }
}
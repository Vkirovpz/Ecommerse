using Ecommerce.Cart;

namespace Ecommerce.Customer.Events
{
    public class ShoppingCartCreated : IEvent
    {
        public ShoppingCartCreated(ShoppingCartId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public ShoppingCartId Id { get; }
    }
}

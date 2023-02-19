using Ecommerce.Cart.Commands;
using Ecommerce.Customer;
using Ecommerce.Customer.Commands;

namespace Ecommerce.Cart
{
    public class ShoppingCartService :
        ICommandHandler<AddProduct>,
        ICommandHandler<RemoveProduct>,
        ICommandHandler<CreateCart>
    {

        private readonly IAggregateRootRepository<ShoppingCart> repository;

        public ShoppingCartService(IAggregateRootRepository<ShoppingCart> repository)
        {
            this.repository = repository;
        }

        public Task HandleAsync(AddProduct command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(RemoveProduct command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(CreateCart command)
        {
            throw new NotImplementedException();
        }
    }
}

using Ecommerce.Cart;
using Ecommerce.Customer.Commands;

namespace Ecommerce.Customer
{
    public class CustomerApplicationService :
        ICommandHandler<CreateCustomer>,
        ICommandHandler<RenameCustomer>,
        ICommandHandler<AddToCart>,
        ICommandHandler<RemoveFromCart>
    {
        private readonly IAggregateRootRepository<CustomerAggregate> repository;

        public CustomerApplicationService(IAggregateRootRepository<CustomerAggregate> repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(CreateCustomer command)
        {
            var customer = await repository.LoadAsync(command.Id).ConfigureAwait(false);
            if (customer is null)
            {
                customer = new CustomerAggregate(command.Id, command.FirstName, command.LastName);
                await repository.SaveAsync(customer).ConfigureAwait(false);
            }
        }

        public async Task HandleAsync(RenameCustomer command)
        {
            var customer = await repository.LoadAsync(command.Id).ConfigureAwait(false);
            if (customer is null)
                return;
            customer.Rename(command.FirstName, command.LastName);
            await repository.SaveAsync(customer).ConfigureAwait(false);
        }

        public async Task HandleAsync(AddToCart command)
        {
            var customer = await repository.LoadAsync(command.Id).ConfigureAwait(false);
            if (customer is null) return;

            customer.AddToCart(command.Product, command.Quantity);

            await repository.SaveAsync(customer).ConfigureAwait(false);
        }

        public async Task HandleAsync(RemoveFromCart command)
        {
            var customer = await repository.LoadAsync(command.Customerid).ConfigureAwait(false);
            if (customer is null) return;
            var cart = customer.State.Cart;
            if (cart is null) return;
            cart.RemoveProduct(command.Product.Sku);
            await repository.SaveAsync(customer).ConfigureAwait(false);
        }
    }
}
using Ecommerce.Customer.Commands;

namespace Ecommerce.Customer
{
    public class CustomerApplicationService : ICommandHandler<CreateCustomer>, ICommandHandler<RenameCustomer>
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

        }
    }
}
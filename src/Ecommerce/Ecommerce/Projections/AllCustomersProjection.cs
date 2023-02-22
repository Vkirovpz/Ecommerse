using Ecommerce.Customer.Events;
using Ecommerce.EntityFramework;
using Ecommerce.EntityFramework.Models;

namespace Ecommerce.Projections
{
    public class AllCustomersProjection :
        //IHaveProjectionId,
        IEventHandler<CustomerCreated>,
        IEventHandler<CustomerFirstNameChanged>,
        IEventHandler<CustomerLastNameChanged>
    {

        private readonly EcommerceDbContext _dbContext;
        public AllCustomersProjection(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GetId(IEvent e)
        {
            switch (e)
            {
                case CustomerCreated cc:
                    return cc.Id.Value;
                case CustomerFirstNameChanged cfc:
                    return cfc.Id.Value;
                case CustomerLastNameChanged clc:
                    return clc.Id.Value;
                default:
                    throw new NotImplementedException($"Can not get projection id from '{e.GetType().Name}'");
            }
        }

        public void Handle(CustomerCreated e)
        {
            if (_dbContext.Customers.Any(c => c.Id == e.Id.Value) == false)
            {
                _dbContext.Customers.Add(new CustomeEfModel()
                {
                    Id = e.Id.ToString(),
                    FirstName = e.FirstName.Value,
                    LastName = e.LastName.Value,
                });
                _dbContext.SaveChanges();
            }
        }

        public void Handle(CustomerFirstNameChanged e)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == e.Id.Value);

            if (customer is not null)
            {
                customer.FirstName = e.NewFirstName.Value;
                _dbContext.SaveChanges();
            }
        }

        public void Handle(CustomerLastNameChanged e)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == e.Id.Value);

            if (customer is not null)
            {
                customer.LastName = e.NewLastName.Value;
                _dbContext.SaveChanges();
            }
        }
    }
}

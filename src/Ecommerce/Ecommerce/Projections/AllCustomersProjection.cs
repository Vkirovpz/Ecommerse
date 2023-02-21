using Ecommerce.Customer.Events;
using static Ecommerce.Projections.ProductsInCartsProjection;

namespace Ecommerce.Projections
{
    public class AllCustomersProjection : IEventHandler<CustomerCreated>,
        IEventHandler<CustomerFirstNameChanged>,
        IEventHandler<CustomerLastNameChanged>
    {
        public static List<CustomerView> Customers { get; set; } = new();
        public void Handle(CustomerCreated e)
        {
            if (Customers.Any(c => c.Id == e.Id.ToString()) == false)
            {
                Customers.Add(new CustomerView()
                {
                    Id = e.Id.ToString(),
                    FirstName = e.FirstName.ToString(),
                    LastName = e.LastName.ToString(),
                });
            }
        }

        public void Handle(CustomerFirstNameChanged e)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == e.Id.ToString());
            if (customer == null) 
            { 
                customer.FirstName = e.NewFirstName.Value.ToString();
            }
        }

        public void Handle(CustomerLastNameChanged e)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == e.Id.ToString());
            if (customer == null)
            {
                customer.FirstName = e.NewLastName.Value.ToString();
            }
        }

        public class CustomerView
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}

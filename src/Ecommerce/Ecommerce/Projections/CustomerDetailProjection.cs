using Ecommerce.Cart.Events;
using Ecommerce.Customer.Events;

namespace Ecommerce.Projections
{
    public class CustomerDetailProjection :
        IHaveProjectionId,
        IEventHandler<CustomerCreated>,
        IEventHandler<CustomerFirstNameChanged>,
        IEventHandler<CustomerLastNameChanged>,
        IEventHandler<ProductAddedToShoppingCart>
    {
        private List<string> nameHistory = new List<string>();
        private List<string> boughtProducts = new List<string>();

        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public IEnumerable<string> NameHistory => nameHistory.AsReadOnly();
        public IEnumerable<string> BoughtProducts => boughtProducts.AsReadOnly();

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
                    case ProductAddedToShoppingCart ev:
                    return ev.Id.CustomerId.Value;
                default:
                    throw new NotImplementedException($"Can not get projection id from '{e.GetType().Name}'");
            }
        }

        public void Handle(CustomerCreated e)
        {
            Id = e.Id.Value;
            FirstName = e.FirstName.Value;
            LastName = e.LastName.Value;
        }

        public void Handle(CustomerFirstNameChanged e)
        {
            Id = e.Id.Value;
            FirstName = e.NewFirstName.Value;
            nameHistory.Add($"{e.OldFirstName.Value} {LastName}");
        }

        public void Handle(CustomerLastNameChanged e)
        {
            Id = e.Id.Value;
            LastName = e.NewLastName.Value;
            nameHistory.Add($"{FirstName} {e.OldLastName.Value}");
        }

        public void Handle(ProductAddedToShoppingCart e)
        {
            boughtProducts.Add(e.Product.Sku);
        }
    }
}

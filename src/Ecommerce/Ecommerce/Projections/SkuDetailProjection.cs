using Ecommerce.Cart.Events;
using Ecommerce.Customer.Events;

namespace Ecommerce.Projections
{
    public class SkuDetailProjection :
        IHaveProjectionId,
        IEventHandler<CustomerCreated>,
        IEventHandler<ProductAddedToShoppingCart>,
        IEventHandler<ShoppingCartCreated>
    {

        private List<Purchase> purchaseHistory = new List<Purchase>();
        public IEnumerable<Purchase> PurchaseHistory => purchaseHistory.AsReadOnly();


        public string Sku { get; private set; }
        public string ProductInfo { get; private set; }

        public string CustomerId { get; private set; }
        public string Customer { get; private set; }
        public string Cart { get; private set; }

        public string GetId(IEvent e)
        {
            switch (e)
            {
                case ProductAddedToShoppingCart ev:
                    return ev.Product.Sku;
                case CustomerCreated cc:
                    return cc.Id.Value;
                case ShoppingCartCreated shcc:
                    return shcc.Id.CustomerId.Value;
                default:
                    throw new NotImplementedException($"Can not get projection product from '{e.GetType().Name}'");
            }
        }

        public void Handle(ProductAddedToShoppingCart e)
        {
            Sku = e.Product.Sku;
            ProductInfo = $"ID {e.Product.Sku.Value} Product: {e.Product.Name.Value}, Price {e.Product.Price.Price}.{e.Product.Price.Currency}";
            purchaseHistory.Add(new Purchase(e.Quantity, Customer));

        }

        public void Handle(CustomerCreated e)
        {
            Customer = e.FirstName.Value;
            CustomerId = e.Id.Value;
        }

        public void Handle(ShoppingCartCreated e)
        {
           
        }

        public record Purchase(int quantity, string customerName);
    }
}

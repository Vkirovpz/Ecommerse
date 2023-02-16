using Ecommerce.Customer;

namespace Ecommerce
{
    public class ShoppingCart
    {
        public CustomerAggregate Customer { get; }

        public HashSet<ShoppingCartItem> Items { get; }

        public void AddProduct(Product product, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product.Sku == product.Sku);
            if (item is null)
            {
                var itemToAdd = new ShoppingCartItem(product, quantity);
                Items.Add(itemToAdd);
            }
            else
            {
                //item.Quantity += quantity;
            }

        }

        public void RemoveProduct(string sku)
        {

        }
    }
}
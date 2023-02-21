using Ecommerce.Cart.Events;

namespace Ecommerce.Projections
{
    public class ProductsInCartsProjection : 
        IEventHandler<ProductAddedToShoppingCart>,
        IEventHandler<ProductRemovedFromShoppingCart>,
        IEventHandler<ProductQuantityIncreased>,
        IEventHandler<ProductQuantityDecreased>
    {
        public static List<ProductView> Products { get; set; } = new();

        public void Handle(ProductRemovedFromShoppingCart e)
        {
            var product = Products.FirstOrDefault(p => p.Sku == e.Sku.ToString());
            if (product is not null)
            {
                Products.Remove(product);
            }
        }

        public void Handle(ProductAddedToShoppingCart e)
        {
            if (Products.Any(p => p.Sku == e.Product.Sku.ToString()) == false)
            {
                Products.Add(new ProductView()
                {
                    Sku = e.Product.Sku.ToString(),
                    Quantity = e.Quantity,
                    Total = (e.Product.Price.Price) * e.Quantity
                });
            }
        }

        public void Handle(ProductQuantityIncreased e)
        {
            var product = Products.FirstOrDefault(p => p.Sku == e.Product.Sku.ToString());
            if (product is not null)
            {
                product.Quantity += e.NewQuantity;
                product.Total += (e.Product.Price.Price * e.NewQuantity);
            }
        }

        public void Handle(ProductQuantityDecreased e)
        {
            var product = Products.FirstOrDefault(p => p.Sku == e.Product.Sku.ToString());
            if (product is not null)
            {
                product.Quantity -= e.NewQuantity;
                product.Total -= (e.Product.Price.Price * e.NewQuantity);
            }
        }

        public class ProductView
        {
            public string Sku { get; set; }

            public int Quantity { get; set; }

            public decimal Total { get; set; }
        }
    }
}

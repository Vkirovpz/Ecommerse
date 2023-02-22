using Ecommerce.Cart.Events;
using Ecommerce.EntityFramework;
using Ecommerce.EntityFramework.Models;

namespace Ecommerce.Projections
{
    public class ProductsInCartsProjection : 
        IEventHandler<ProductAddedToShoppingCart>,
        IEventHandler<ProductRemovedFromShoppingCart>,
        IEventHandler<ProductQuantityIncreased>,
        IEventHandler<ProductQuantityDecreased>
    {
        private readonly EcommerceDbContext _dbContext;

        public ProductsInCartsProjection(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle(ProductAddedToShoppingCart e)
        {
            if (_dbContext.Products.Any(p => p.Sku == e.Product.Sku) == false)
            {
                _dbContext.Products.Add(new EntityFramework.Models.ProductEfModel()
                {
                    Sku = e.Product.Sku.ToString(),
                    Quantity = e.Quantity,
                    Total = (e.Product.Price.Price) * e.Quantity,
                    Currency = e.Product.Price.Currency
                });
                _dbContext.SaveChanges();
            }
        }

        public void Handle(ProductRemovedFromShoppingCart e)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Sku == e.Sku);

            if (product is not null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        public void Handle(ProductQuantityIncreased e)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Sku == e.Product.Sku);

            if (product is not null)
            {
                product.Quantity += e.NewQuantity;
                product.Total += (e.Product.Price.Price * e.NewQuantity);
                _dbContext.SaveChanges();
            }
        }

        public void Handle(ProductQuantityDecreased e)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Sku == e.Product.Sku);

            if (product is not null)
            {
                product.Quantity -= e.NewQuantity;
                product.Total -= (e.Product.Price.Price * e.NewQuantity);
                _dbContext.SaveChanges();
            }
        }
    }
}

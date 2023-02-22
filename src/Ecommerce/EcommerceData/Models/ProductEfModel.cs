namespace Ecommerce.EntityFramework.Models
{
    public class ProductEfModel
    {
        public string Sku { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }
        public string Currency { get; set; }
    }
}

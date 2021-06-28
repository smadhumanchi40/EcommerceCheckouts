using AspNetCoreHero.Abstractions.Domain;

namespace CheckoutSys.Domain.Entities.Catalog
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int ProductCategoryId { get; set; }
        public int StockQuantity { get; set; }
        public int OrderMinimumQuantity { get; set; }
        public int OrderMaximumQuantity { get; set; }
        public bool NotReturnable { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
    }
}
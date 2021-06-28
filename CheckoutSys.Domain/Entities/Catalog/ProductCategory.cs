using AspNetCoreHero.Abstractions.Domain;

namespace CheckoutSys.Domain.Entities.Catalog
{
    public class ProductCategory : AuditableEntity
    {
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
    }
}
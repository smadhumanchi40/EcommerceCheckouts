using AspNetCoreHero.Abstractions.Domain;
using System;

namespace CheckoutSys.Domain.Entities.Discount
{
    public class Discount : AuditableEntity
    {
        public string Name { get; set; }

        public int DiscountTypeId { get; set; }

        public int EntityId { get; set; }

        public bool UsePercentage { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal? MaximumDiscountAmount { get; set; }

        public DateTime? StartDateUtc { get; set; }

        public DateTime? EndDateUtc { get; set; }

        public int DiscountLimitationId { get; set; }

        public int LimitationTimes { get; set; }

        public int? MaximumDiscountedQuantity { get; set; }
        public bool IsCumulative { get; set; }

        // set to true when we want to set bundle discount
        // like Buy 1 Get second at 50%
        public bool IsProductBundleEnabled { get; set; }

        //Buy product A and get product B free
        //for this we could leverage below property
        public int BundleSelectionProductId { get; set; }

        //3 is entered here because we want the discount to apply to multiples of 3 products.
        //If this discount was Buy 3 Get 1 Free, the Selection Quantity would be set to 4
        //because the 100% discount on 1 item is applied only after 4 items are in the cart.
        public int BundleSelectionQuantity { get; set; } = 1;

        //For our example the discount applies to multiples of 3 products
        //(it applies when 3, 6, 9, 12, 15 etc. products are added to the cart),
        //and for each multiple of 3 products added to the cart the discount will be applied once
        public string BundleSelectionOperation { get; set; }

        public DiscountType DiscountType
        {
            get => (DiscountType)DiscountTypeId;
            set => DiscountTypeId = (int)value;
        }

        public DiscountLimitationType DiscountLimitation
        {
            get => (DiscountLimitationType)DiscountLimitationId;
            set => DiscountLimitationId = (int)value;
        }
    }
}

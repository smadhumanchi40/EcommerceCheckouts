using System;

namespace CheckoutSys.Application.Features.Discounts.Queries.GetAllPaged
{
    public class GetAllDiscountsResponse
    {
        public int Id { get; set; }
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
    }
}
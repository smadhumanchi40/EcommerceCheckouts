namespace CheckoutSys.Infrastructure.CacheKeys
{
    public static class DiscountCacheKeys
    {
        public static string ListKey => "DiscountList";

        public static string SelectListKey => "DiscountSelectList";

        public static string GetKey(int discountId) => $"Discount-{discountId}";

        public static string GetDetailsKey(int discountId) => $"DiscountDetails-{discountId}";
    }
}
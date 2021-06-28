
namespace CheckoutSys.Domain.Entities.Discount
{
    public enum DiscountType
    {
        AssignedToOrderTotal = 1,
        AssignedToProduct = 2,
        AssignedToCategories = 5,
        AssignedToShipping = 10,
        AssignedToOrderSubTotal = 20
    }

    public enum DiscountLimitationType
    {
        /// <summary>
        /// None
        /// </summary>
        Unlimited = 0,

        /// <summary>
        /// N Times Only
        /// </summary>
        NTimesOnly = 15,

        /// <summary>
        /// N Times Per Customer
        /// </summary>
        NTimesPerCustomer = 25
    }
}

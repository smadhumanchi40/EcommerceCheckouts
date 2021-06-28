namespace CheckoutSys.Application.Features.Orders.Queries.GetCartItemsTotal
{
    public class GetCartItemsTotalResponse
    {
        public decimal OrderTotal { get; set; }
        public decimal OrderDiscount { get; set; }
    }
}
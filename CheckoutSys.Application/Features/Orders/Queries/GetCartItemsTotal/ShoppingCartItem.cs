
using CheckoutSys.Domain.Entities.Catalog;

namespace CheckoutSys.Application.Features.Orders.Queries.GetCartItemsTotal
{
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}

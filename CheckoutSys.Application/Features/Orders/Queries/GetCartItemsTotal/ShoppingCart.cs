using System.Collections.Generic;

namespace CheckoutSys.Application.Features.Orders.Queries.GetCartItemsTotal
{
   public class ShoppingCart
    {
        public int CustomerId { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}

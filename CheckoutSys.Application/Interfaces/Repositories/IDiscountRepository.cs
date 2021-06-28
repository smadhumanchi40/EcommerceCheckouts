using CheckoutSys.Domain.Entities.Catalog;
using CheckoutSys.Domain.Entities.Discount;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckoutSys.Application.Interfaces.Repositories
{
    public interface IDiscountRepository
    {
        IQueryable<Discount> Discounts { get; }

        Task<List<Discount>> GetListAsync();

        Task<Discount> GetByIdAsync(int discountId);

        Task<int> InsertAsync(Discount discount);

        Task UpdateAsync(Discount discount);

        Task DeleteAsync(Discount discount);
        Task<decimal> GetDiscountAmount(Product product, int quantity);
    }
}
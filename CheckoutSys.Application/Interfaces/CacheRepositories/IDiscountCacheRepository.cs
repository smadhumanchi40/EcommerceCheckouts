using CheckoutSys.Domain.Entities.Discount;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckoutSys.Application.Interfaces.CacheRepositories
{
    public interface IDiscountCacheRepository
    {
        Task<List<Discount>> GetCachedListAsync();

        Task<Discount> GetByIdAsync(int discountId);
    }
}
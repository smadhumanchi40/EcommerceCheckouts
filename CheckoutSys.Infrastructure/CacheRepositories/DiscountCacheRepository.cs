using CheckoutSys.Application.Interfaces.CacheRepositories;
using CheckoutSys.Application.Interfaces.Repositories;
using CheckoutSys.Infrastructure.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using CheckoutSys.Domain.Entities.Discount;

namespace CheckoutSys.Infrastructure.CacheRepositories
{
    public class DiscountCacheRepository : IDiscountCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDiscountRepository _discountRepository;

        public DiscountCacheRepository(IDistributedCache distributedCache, IDiscountRepository discountRepository)
        {
            _distributedCache = distributedCache;
            _discountRepository = discountRepository;
        }

        public async Task<Discount> GetByIdAsync(int discountId)
        {
            string cacheKey = DiscountCacheKeys.GetKey(discountId);
            var discount = await _distributedCache.GetAsync<Discount>(cacheKey);
            if (discount == null)
            {
                discount = await _discountRepository.GetByIdAsync(discountId);
                Throw.Exception.IfNull(discount, "Discount", "No Discount Found");
                await _distributedCache.SetAsync(cacheKey, discount);
            }
            return discount;
        }

        public async Task<List<Discount>> GetCachedListAsync()
        {
            string cacheKey = DiscountCacheKeys.ListKey;
            var discountList = await _distributedCache.GetAsync<List<Discount>>(cacheKey);
            if (discountList == null)
            {
                discountList = await _discountRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, discountList);
            }
            return discountList;
        }
    }
}
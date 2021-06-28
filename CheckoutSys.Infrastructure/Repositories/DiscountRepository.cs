using CheckoutSys.Application.Interfaces.Repositories;
using CheckoutSys.Domain.Entities.Catalog;
using CheckoutSys.Domain.Entities.Discount;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckoutSys.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IRepositoryAsync<Discount> _repository;
        private readonly IDistributedCache _distributedCache;

        public DiscountRepository(IDistributedCache distributedCache, IRepositoryAsync<Discount> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Discount> Discounts => _repository.Entities;

        public async Task DeleteAsync(Discount discount)
        {
            await _repository.DeleteAsync(discount);
            await _distributedCache.RemoveAsync(CacheKeys.DiscountCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.DiscountCacheKeys.GetKey(discount.Id));
        }

        public async Task<Discount> GetByIdAsync(int discountId)
        {
            return await _repository.Entities.Where(p => p.Id == discountId).FirstOrDefaultAsync();
        }

        public async Task<List<Discount>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Discount discount)
        {
            await _repository.AddAsync(discount);
            await _distributedCache.RemoveAsync(CacheKeys.DiscountCacheKeys.ListKey);
            return discount.Id;
        }

        public async Task UpdateAsync(Discount discount)
        {
            await _repository.UpdateAsync(discount);
            await _distributedCache.RemoveAsync(CacheKeys.DiscountCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.DiscountCacheKeys.GetKey(discount.Id));
        }

        public async Task<decimal> GetDiscountAmount(Product product, int quantity)
        {
            var allowedDiscounts = GetAllowedDiscounts(product);
            var appliedDiscountAmount = decimal.Zero;
            List<Discount> appliedDiscounts = GetPreferredDiscount(allowedDiscounts.Where(x => !x.IsProductBundleEnabled).ToList(), product.Rate, out appliedDiscountAmount);
            var bundleDiscountAmount = GetBundleDiscountAmount(allowedDiscounts.Where(x => x.IsProductBundleEnabled).ToList(), product.Rate, quantity);
            return appliedDiscountAmount*quantity + bundleDiscountAmount;
        }
        
        protected List<Discount> GetPreferredDiscount(IList<Discount> discounts,
            decimal amount, out decimal discountAmount)
        {

            var result = new List<Discount>();
            discountAmount = decimal.Zero;
            if (!discounts.Any())
                return result;

            //first we check simple discounts
            foreach (var discount in discounts)
            {
                var currentDiscountValue = GetDiscountAmount(discount, amount);
                if (currentDiscountValue <= discountAmount)
                    continue;

                discountAmount = currentDiscountValue;

                result.Clear();
                result.Add(discount);
            }
            //now let's check bundle discounts
            var cumulativeDiscounts = discounts.Where(x => x.IsCumulative).OrderBy(x => x.Name).ToList();
            if (cumulativeDiscounts.Count < 1)
                return result;

            var cumulativeDiscountAmount = cumulativeDiscounts.Sum(d => GetDiscountAmount(d, amount));
            if (cumulativeDiscountAmount <= discountAmount)
                return result;

            discountAmount = cumulativeDiscountAmount;

            result.Clear();
            result.AddRange(cumulativeDiscounts);

            return result;
        }

        protected IList<Discount> GetAllowedDiscounts(Product product)
        {
            var allowedDiscounts = new List<Discount>();
            //discounts applied to products
            foreach (var discount in GetAllowedDiscountsAppliedToProduct(product))
                if (!ContainsDiscount(allowedDiscounts, discount))
                    allowedDiscounts.Add(discount);

            return allowedDiscounts;
        }
        protected virtual IList<Discount> GetAllowedDiscountsAppliedToProduct(Product product)
        {
            return GetAppliedDiscounts(product.Id, (int)DiscountType.AssignedToProduct);
        }
        protected virtual bool ContainsDiscount(IList<Discount> discounts, Discount discount)
        {
            if (discounts == null || discount == null)
                return false;

            return discounts.Any(x => x.Id == discount.Id);
        }
        
        protected virtual decimal GetDiscountAmount(Discount discount, decimal amount)
        {
            if (discount == null)
                return decimal.Zero;

            //calculate discount amount
            decimal result;
            if (discount.UsePercentage)
                result = (decimal)((float)amount * (float)discount.DiscountPercentage / 100f);
            else
                result = discount.DiscountAmount;

            //validate maximum discount amount
            if (discount.UsePercentage &&
                discount.MaximumDiscountAmount.HasValue &&
                result > discount.MaximumDiscountAmount.Value)
                result = discount.MaximumDiscountAmount.Value;

            if (result < decimal.Zero)
                result = decimal.Zero;

            return result;
        }

        private decimal GetBundleDiscountAmount(List<Discount> discounts, decimal amount, int quantity)
        {
            var bundleDiscountAmount = decimal.Zero;
            foreach (var discount in discounts)
                bundleDiscountAmount = bundleDiscountAmount + GetSingleBundleDiscountAmount(discount,amount,quantity);
            return bundleDiscountAmount;
        }

        private decimal GetSingleBundleDiscountAmount(Discount discount, decimal amount, int quantity)
        {
            var currentDiscountValue = decimal.Zero;

            if(discount.BundleSelectionOperation == "MultipleOf")
            {
                //For our example the discount applies to multiples of 3 products
                //(it applies when 3, 6, 9, 12, 15 etc. products are added to the cart),
                //and for each multiple of 3 products added to the cart the discount will be applied once
                int discountedQuantity = quantity / discount.BundleSelectionQuantity;
                currentDiscountValue = GetDiscountAmount(discount, amount* discountedQuantity);
            } 
            else
            {
                int discountedQuantity = quantity / discount.BundleSelectionQuantity;
                if (discountedQuantity >= 1)
                    currentDiscountValue = GetDiscountAmount(discount, amount);
            }
            return currentDiscountValue;
        }

        private IList<Discount> GetAppliedDiscounts(int entityId, int discountTypeId)
        {
            return (from d in _repository.Entities
                    where d.EntityId == entityId && d.DiscountTypeId == discountTypeId
                    select d).ToList();
        }
    }

}
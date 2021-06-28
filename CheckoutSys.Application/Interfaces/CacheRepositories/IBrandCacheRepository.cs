﻿using CheckoutSys.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckoutSys.Application.Interfaces.CacheRepositories
{
    public interface IBrandCacheRepository
    {
        Task<List<Brand>> GetCachedListAsync();

        Task<Brand> GetByIdAsync(int brandId);
    }
}
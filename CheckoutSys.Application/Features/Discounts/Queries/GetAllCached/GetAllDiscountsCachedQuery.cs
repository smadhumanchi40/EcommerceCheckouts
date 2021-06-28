using CheckoutSys.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutSys.Application.Features.Discounts.Queries.GetAllCached
{
    public class GetAllDiscountsCachedQuery : IRequest<Result<List<GetAllDiscountsCachedResponse>>>
    {
        public GetAllDiscountsCachedQuery()
        {
        }
    }

    public class GetAllProductsCachedQueryHandler : IRequestHandler<GetAllDiscountsCachedQuery, Result<List<GetAllDiscountsCachedResponse>>>
    {
        private readonly IDiscountCacheRepository _discountCache;
        private readonly IMapper _mapper;

        public GetAllProductsCachedQueryHandler(IDiscountCacheRepository discountCache, IMapper mapper)
        {
            _discountCache = discountCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllDiscountsCachedResponse>>> Handle(GetAllDiscountsCachedQuery request, CancellationToken cancellationToken)
        {
            var discountList = await _discountCache.GetCachedListAsync();
            var mappedDiscounts = _mapper.Map<List<GetAllDiscountsCachedResponse>>(discountList);
            return Result<List<GetAllDiscountsCachedResponse>>.Success(mappedDiscounts);
        }
    }
}
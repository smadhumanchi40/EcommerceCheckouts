using CheckoutSys.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutSys.Application.Features.Discounts.Queries.GetById
{
    public class GetDiscountByIdQuery : IRequest<Result<GetDiscountByIdResponse>>
    {
        public int Id { get; set; }

        public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, Result<GetDiscountByIdResponse>>
        {
            private readonly IDiscountCacheRepository _discountCache;
            private readonly IMapper _mapper;

            public GetDiscountByIdQueryHandler(IDiscountCacheRepository discountCache, IMapper mapper)
            {
                _discountCache = discountCache;
                _mapper = mapper;
            }

            public async Task<Result<GetDiscountByIdResponse>> Handle(GetDiscountByIdQuery query, CancellationToken cancellationToken)
            {
                var discount = await _discountCache.GetByIdAsync(query.Id);
                var mappedDiscount = _mapper.Map<GetDiscountByIdResponse>(discount);
                return Result<GetDiscountByIdResponse>.Success(mappedDiscount);
            }
        }
    }
}
using CheckoutSys.Application.Extensions;
using CheckoutSys.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CheckoutSys.Domain.Entities.Discount;

namespace CheckoutSys.Application.Features.Discounts.Queries.GetAllPaged
{
    public class GetAllDiscountsQuery : IRequest<PaginatedResult<GetAllDiscountsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllDiscountsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllDiscountsQueryHandler : IRequestHandler<GetAllDiscountsQuery, PaginatedResult<GetAllDiscountsResponse>>
    {
        private readonly IDiscountRepository _repository;

        public GGetAllDiscountsQueryHandler(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllDiscountsResponse>> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Discount, GetAllDiscountsResponse>> expression = e => new GetAllDiscountsResponse
            {
                Id = e.Id,
                Name = e.Name,
                DiscountAmount = e.DiscountAmount,
                DiscountPercentage = e.DiscountPercentage,
                DiscountLimitationId = e.DiscountLimitationId,
                DiscountTypeId = e.DiscountTypeId,
                EndDateUtc = e.EndDateUtc,
                EntityId = e.EntityId,
                LimitationTimes = e.LimitationTimes,
                MaximumDiscountAmount = e.MaximumDiscountAmount,
                MaximumDiscountedQuantity = e.MaximumDiscountedQuantity,
                StartDateUtc = e.StartDateUtc,
                UsePercentage = e.UsePercentage
        };
            var paginatedList = await _repository.Discounts
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}
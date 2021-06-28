using CheckoutSys.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CheckoutSys.Domain.Entities.Discount;
using System;

namespace CheckoutSys.Application.Features.Discounts.Commands.Create
{
    public partial class CreateDiscountCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }

        public int DiscountTypeId { get; set; }

        public int EntityId { get; set; }

        public bool UsePercentage { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal? MaximumDiscountAmount { get; set; }

        public DateTime? StartDateUtc { get; set; }

        public DateTime? EndDateUtc { get; set; }

        public int DiscountLimitationId { get; set; }

        public int LimitationTimes { get; set; }

        public int? MaximumDiscountedQuantity { get; set; }
        public bool IsCumulative { get; set; }
        public bool IsProductBundleEnabled { get; set; }
        public int BundleSelectionProductId { get; set; }
        public int BundleSelectionQuantity { get; set; }
        public string BundleSelectionOperation { get; set; }

    }

    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Result<int>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = _mapper.Map<Discount>(request);
            await _discountRepository.InsertAsync(discount);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(discount.Id);
        }
    }
}
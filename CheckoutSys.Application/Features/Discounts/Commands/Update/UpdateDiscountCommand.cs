using CheckoutSys.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CheckoutSys.Application.Features.Discounts.Commands.Update
{
    public class UpdateDiscountCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
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
        public bool IsCumulative { get; set; }
        public int? MaximumDiscountedQuantity { get; set; }
        public bool IsProductBundleEnabled { get; set; }
        public int BundleSelectionProductId { get; set; }
        public int BundleSelectionQuantity { get; set; }
        public string BundleSelectionOperation { get; set; }

        public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDiscountRepository _discountRepository;

            public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
            {
                _discountRepository = discountRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateDiscountCommand command, CancellationToken cancellationToken)
            {
                var discount = await _discountRepository.GetByIdAsync(command.Id);

                if (discount == null)
                {
                    return Result<int>.Fail($"Discount Not Found.");
                }
                else
                {
                    discount.Name = command.Name ?? discount.Name;
                    discount.DiscountAmount = command.DiscountAmount;
                    discount.DiscountPercentage = command.DiscountPercentage;
                    discount.DiscountLimitationId = command.DiscountLimitationId;
                    discount.DiscountTypeId = command.DiscountTypeId;
                    discount.EndDateUtc = command.EndDateUtc;
                    discount.EntityId = command.EntityId;
                    discount.Id = command.Id;
                    discount.LimitationTimes = command.LimitationTimes;
                    discount.MaximumDiscountAmount = command.MaximumDiscountAmount;
                    discount.MaximumDiscountedQuantity = command.MaximumDiscountedQuantity;
                    discount.StartDateUtc = command.StartDateUtc;
                    discount.UsePercentage = command.UsePercentage;
                    discount.BundleSelectionOperation = command.BundleSelectionOperation;
                    discount.BundleSelectionProductId = command.BundleSelectionProductId;
                    discount.BundleSelectionQuantity = command.BundleSelectionQuantity;
                    discount.IsProductBundleEnabled = command.IsProductBundleEnabled;
                    discount.IsCumulative = command.IsCumulative;
                    await _discountRepository.UpdateAsync(discount);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(discount.Id);
                }
            }
        }
    }
}
using CheckoutSys.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutSys.Application.Features.Discounts.Commands.Delete
{
    public class DeleteDiscountCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, Result<int>>
        {
            private readonly IDiscountRepository _discountRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteDiscountCommandHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
            {
                _discountRepository = discountRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteDiscountCommand command, CancellationToken cancellationToken)
            {
                var product = await _discountRepository.GetByIdAsync(command.Id);
                await _discountRepository.DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(product.Id);
            }
        }
    }
}
using CheckoutSys.Application.Features.Discounts.Commands.Create;
using CheckoutSys.Application.Features.Discounts.Queries.GetAllCached;
using CheckoutSys.Application.Features.Discounts.Queries.GetAllPaged;
using CheckoutSys.Application.Features.Discounts.Queries.GetById;
using AutoMapper;
using CheckoutSys.Domain.Entities.Discount;

namespace CheckoutSys.Application.Mappings
{
    internal class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<CreateDiscountCommand, Discount>().ReverseMap();
            CreateMap<GetDiscountByIdResponse, Discount>().ReverseMap();
            CreateMap<GetAllDiscountsCachedResponse, Discount>().ReverseMap();
            CreateMap<GetAllDiscountsResponse, Discount>().ReverseMap();
        }
    }
}
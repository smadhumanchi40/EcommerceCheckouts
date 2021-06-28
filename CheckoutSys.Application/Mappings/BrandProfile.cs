using CheckoutSys.Application.Features.Brands.Commands.Create;
using CheckoutSys.Application.Features.Brands.Queries.GetAllCached;
using CheckoutSys.Application.Features.Brands.Queries.GetById;
using CheckoutSys.Domain.Entities.Catalog;
using AutoMapper;

namespace CheckoutSys.Application.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsCachedResponse, Brand>().ReverseMap();
        }
    }
}
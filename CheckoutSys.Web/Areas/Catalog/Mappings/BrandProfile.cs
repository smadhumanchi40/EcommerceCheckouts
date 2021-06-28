using CheckoutSys.Application.Features.Brands.Commands.Create;
using CheckoutSys.Application.Features.Brands.Commands.Update;
using CheckoutSys.Application.Features.Brands.Queries.GetAllCached;
using CheckoutSys.Application.Features.Brands.Queries.GetById;
using CheckoutSys.Web.Areas.Catalog.Models;
using AutoMapper;

namespace CheckoutSys.Web.Areas.Catalog.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<GetAllBrandsCachedResponse, BrandViewModel>().ReverseMap();
            CreateMap<GetBrandByIdResponse, BrandViewModel>().ReverseMap();
            CreateMap<CreateBrandCommand, BrandViewModel>().ReverseMap();
            CreateMap<UpdateBrandCommand, BrandViewModel>().ReverseMap();
        }
    }
}
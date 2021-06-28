using CheckoutSys.Application.Features.Products.Commands.Create;
using CheckoutSys.Application.Features.Products.Commands.Update;
using CheckoutSys.Application.Features.Products.Queries.GetAllCached;
using CheckoutSys.Application.Features.Products.Queries.GetById;
using CheckoutSys.Web.Areas.Catalog.Models;
using AutoMapper;

namespace CheckoutSys.Web.Areas.Catalog.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductsCachedResponse, ProductViewModel>().ReverseMap();
            CreateMap<GetProductByIdResponse, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductViewModel>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductViewModel>().ReverseMap();
        }
    }
}
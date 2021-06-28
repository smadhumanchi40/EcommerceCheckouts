using CheckoutSys.Application.Features.Products.Commands.Create;
using CheckoutSys.Application.Features.Products.Queries.GetAllCached;
using CheckoutSys.Application.Features.Products.Queries.GetAllPaged;
using CheckoutSys.Application.Features.Products.Queries.GetById;
using CheckoutSys.Domain.Entities.Catalog;
using AutoMapper;

namespace CheckoutSys.Application.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsCachedResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsResponse, Product>().ReverseMap();
        }
    }
}
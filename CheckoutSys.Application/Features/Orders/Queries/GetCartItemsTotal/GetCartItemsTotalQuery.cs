using CheckoutSys.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CheckoutSys.Domain.Entities.Catalog;
using System.Linq;

namespace CheckoutSys.Application.Features.Orders.Queries.GetCartItemsTotal
{
    public class GetCartItemsTotalQuery : IRequest<Result<GetCartItemsTotalResponse>>
    {
        public List<string> Products { get; set; }
        public bool ApplyDiscount { get; set; }

        public class GetCartItemsTotalQueryHandler : IRequestHandler<GetCartItemsTotalQuery, Result<GetCartItemsTotalResponse>>
        {
            private readonly IProductRepository _product;
            private readonly IDiscountRepository _discount;
            private readonly IMapper _mapper;

            public GetCartItemsTotalQueryHandler(IProductRepository product, IDiscountRepository discount, IMapper mapper)
            {
                _product = product;
                _mapper = mapper;
                _discount = discount;
            }

            public async Task<Result<GetCartItemsTotalResponse>> Handle(GetCartItemsTotalQuery query, CancellationToken cancellationToken)
            {
                ShoppingCart cart = new ShoppingCart() { CustomerId = 1, Items = new List<ShoppingCartItem>() };
                //get product lists 
                List<Product> products = await _product.GetListAsync(query.Products);
                // convert list of string to cart items
                foreach (string product in query.Products)
                {
                    Product currentProduct = products.Where(x => x.Name == product).FirstOrDefault();
                    if (currentProduct != null && currentProduct.Id > 0)
                    {
                        if (cart.Items.Any(x => x.ProductId == currentProduct.Id))
                        {
                            var updateCurrentItem = cart.Items.FirstOrDefault(x => x.ProductId == currentProduct.Id);
                            updateCurrentItem.Quantity = updateCurrentItem.Quantity + 1;
                        }
                        else
                        {
                            cart.Items.Add(new ShoppingCartItem() { Product = currentProduct, Quantity = 1, ProductId = currentProduct.Id });
                        }
                    }
                }
                // iterate through cart items
                GetCartItemsTotalResponse response = new GetCartItemsTotalResponse();
                foreach (var cartItem in cart.Items)
                {
                    if (!cartItem.Product.IsPublished || cartItem.Product.IsDeleted)
                    {
                        return Result<GetCartItemsTotalResponse>.Fail($"The product {cartItem.Product.Name} is not available any more");
                    }

                    if (cartItem.Product.StockQuantity < cartItem.Quantity)
                    {
                        return Result<GetCartItemsTotalResponse>.Fail($"There are only {cartItem.Product.StockQuantity} items available for {cartItem.Product.Name}");
                    }
                    if (cartItem.Quantity < cartItem.Product.OrderMinimumQuantity)
                    {
                        return Result<GetCartItemsTotalResponse>.Fail($"The product minimum quantity is {cartItem.Product.OrderMinimumQuantity} ");
                    }

                    if (cartItem.Quantity > cartItem.Product.OrderMaximumQuantity)
                    {
                        return Result<GetCartItemsTotalResponse>.Fail($"The product maximum quantity is {cartItem.Product.OrderMinimumQuantity} ");
                    }
                    var productPrice = cartItem.Product.Rate;
                    decimal taxPercent = 0;
                    productPrice = productPrice / (1 + (taxPercent / 100));
                    if (query.ApplyDiscount)
                    {
                        var discountedItem = await _discount.GetDiscountAmount(cartItem.Product, cartItem.Quantity);
                        response.OrderDiscount = response.OrderDiscount + discountedItem;
                    }                    

                    response.OrderTotal = response.OrderTotal + productPrice*cartItem.Quantity;
         
                }
                response.OrderTotal = response.OrderTotal - response.OrderDiscount;
                // send the cart items total
                return Result<GetCartItemsTotalResponse>.Success(response);
            }
        }
    }
}
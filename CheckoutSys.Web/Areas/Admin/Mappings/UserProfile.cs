using CheckoutSys.Infrastructure.Identity.Models;
using CheckoutSys.Web.Areas.Admin.Models;
using AutoMapper;

namespace CheckoutSys.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
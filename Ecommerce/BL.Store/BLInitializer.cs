using AutoMapper;
using DataContext.EntityFramework;
using Domain.Store;

namespace BL.Store
{
    public class BLInitializer :Profile
    {
        public void Initialize()
        {
            InitializeAutoMapper();
        }

        private void InitializeAutoMapper()
        {
            CreateMap<UserDO, User>().ReverseMap();
            CreateMap<ImageUrl, ImageUrlDO>().ReverseMap();
            CreateMap<Category, CategoryDO>().ReverseMap();
            CreateMap<Product, ProductDO>().ReverseMap().ForMember(dest => dest.CartItems, opt => opt.Ignore()).ForMember(dest => dest.OrderItems, opt => opt.Ignore()).ForMember(dest => dest.Category, opt => opt.Ignore()).ReverseMap();
        }


    }
}

using AutoMapper;
using DataContext.EntityFramework;
using Domain.Backoffice;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Backoffice
{
    public class BLInitializer :Profile
    {
        public BLInitializer()
        {
            InitializeAutoMapper();
        }

        private void InitializeAutoMapper()
        {
            CreateMap<UserDO, User>().ReverseMap();
            CreateMap<ImageUrl, ImageUrlDO>().ReverseMap();
            CreateMap<Category, CategoryDO>().ForMember(dest => dest.Url, opt => opt.Ignore()).ReverseMap();
            CreateMap<ProductDO, Product>().ForMember(dest => dest.OrderItems, opt => opt.Ignore()).ForMember(dest => dest.ImageUrl, opt => opt.Ignore()).ReverseMap();
        }
    }
}

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
            CreateMap<Category, CategoryDO>().ReverseMap();
            CreateMap<ProductDO, Product>().ReverseMap();
            CreateMap<ProductDO, ProductViewModel>().ForMember(dest => dest.Image, opt => opt.Ignore()).ReverseMap();
        }
    }
}

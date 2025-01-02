using AutoMapper;
using DataContext.EntityFramework;
using Domain.Backoffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Backoffice
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
        }


    }
}

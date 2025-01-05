using AutoMapper;
using DataContext.EntityFramework;


namespace Domain.Backoffice
{
    public class MapInitializer : Profile
    {
        public MapInitializer() 
        {
            CreateMap<UserDO,User>().ReverseMap();
            CreateMap<Product,ProductDO>().ReverseMap();
            CreateMap<CategoryDO,Category>().ReverseMap();
            CreateMap<ProductViewModel,Product>().ReverseMap();
        }
    }
}

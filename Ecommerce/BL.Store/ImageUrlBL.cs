using AutoMapper;
using DataContext.EntityFramework.Provider;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store
{
    public class ImageUrlBL
    {
        private readonly IMapper _mapper;

        public ImageUrlBL(IMapper mapper)
        {
            _mapper = mapper;

        }
        //public List<ImageUrlDO> GetAll()
        //{
        //    using (DbContextProvider dcp = new DbContextProvider())
        //    {
        //        Repository<ImageUrl> repImageUrl = new Repository<ImageUrl>(dcp);
        //        List<ImageUrlDO> imageUrlDOList = new List<ImageUrlDO>();
        //        List<ImageUrl> imageUrlList = repImageUrl.GetAll().ToList();
        //        return _mapper.Map<List<ImageUrl>, List<ImageUrlDO>>(imageUrlList);
        //    }
        //}
        //public List<ImageUrl> GetAllDbObject()
        //{
        //    using (DbContextProvider dcp = new DbContextProvider())
        //    {
        //        Repository<ImageUrl> repImageUrl = new Repository<ImageUrl>(dcp);
        //        return repImageUrl.GetAll().ToList();
        //    }
        //}
        //public int SaveProfileImageAndGetID(string imageurl, Guid guid)
        //{
        //    if (!string.IsNullOrWhiteSpace(imageurl))
        //    {
        //        using (DbContextProvider dcp = new DbContextProvider())
        //        {
        //            Repository<ImageUrl> repImageUrl = new Repository<ImageUrl>(dcp);
        //            ImageUrl dbObject = new ImageUrl()
        //            {
        //                Url = imageurl,
        //                Guid = guid
        //            };
        //            repImageUrl.InsertOnSubmit(dbObject);
        //            dcp.CommitChanges();

        //            var a = GetAll().FirstOrDefault(x => x.Guid == guid);
        //            return a != null ? a.ID : 0;
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //public string GetImageUrlByID(int? id)
        //{
        //    if (id == null)
        //    {
        //        return "~/UserImages/Default_Profile.png";
        //    }
        //    ImageUrlDO imageUrlDO = GetAll().FirstOrDefault(x => x.ID == id);
        //    return imageUrlDO != null ? imageUrlDO.Url : "~/UserImages/Default_Profile.png";
        //}
        //public ImageUrlDO GetByID(int? id)
        //{
        //    return GetAll().FirstOrDefault(x => x.ID == id);
        //}
        //public void UpdateImageUrl(int imageUrlId, string value)
        //{
        //    if (imageUrlId != 0 && !String.IsNullOrWhiteSpace(value))
        //    {
        //        using (DbContextProvider dbContextProvider = new DbContextProvider())
        //        {
        //            Repository<ImageUrl> imageUrlRepository = new Repository<ImageUrl>(dbContextProvider);
        //            ImageUrl imageUrl = GetAllDbObject().FirstOrDefault(x => x.Id == imageUrlId);
        //            imageUrl.Url = value;
        //            imageUrlRepository.UpdateByIdOnSubmit(imageUrl);
        //            imageUrlRepository.DCP.CommitChanges();

        //        }
        //    }

        //}
    }
}

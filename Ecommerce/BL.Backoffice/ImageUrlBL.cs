using AutoMapper;
using DataContext.EntityFramework;
using DataContext.EntityFramework.Provider;
using Domain.Backoffice;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BL.Backoffice
{
    public class ImageUrlBL
    {
        private readonly IMapper _mapper;
        public ImageUrlBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<ImageUrlDO> GetAll()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<ImageUrl> imageRepository = new Repository<ImageUrl>(dcp);
                return _mapper.Map<List<ImageUrl>, List<ImageUrlDO>>(imageRepository.GetAll().ToList());
            }
        }
        public string GetImageUrlById(int id)
        {
            ImageUrlDO imageUrl = GetAll().FirstOrDefault(x=>x.Id == id);
            return imageUrl != null ? imageUrl.Url : String.Empty;
        }
        public int GetImageIdByGuid(Guid guid)
        {
            ImageUrlDO imageUrl = GetAll().FirstOrDefault(x => x.Guid == (Guid)guid);
            return imageUrl != null ? imageUrl.Id : 0;
        }

        public int UploadImageAndGetId(string imageurl)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Guid generateGuid = Guid.NewGuid();
                ImageUrl dbObject = new ImageUrl()
                {
                    Url = imageurl,
                    Guid = generateGuid
                };
                Repository<ImageUrl> imageRepository = new Repository<ImageUrl>(dcp);
                imageRepository.InsertOnSubmit(dbObject);
                dcp.CommitChanges();
                int id = GetImageIdByGuid(generateGuid);
                return id;
            }
            
        }
    }
}

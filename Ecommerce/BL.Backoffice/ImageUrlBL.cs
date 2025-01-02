using AutoMapper;
using DataContext.EntityFramework;
using DataContext.EntityFramework.Provider;
using Domain.Backoffice;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ImageUrlDO GetFirst()
        {
            return GetAll().First();
        }
    }
}

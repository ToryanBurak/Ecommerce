using DataContext.EntityFramework.Provider;
using DataContext.EntityFramework;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Backoffice;

namespace BL.Backoffice
{
    public class CategoryBL
    {
        private readonly IMapper _mapper;
        public CategoryBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<CategoryDO> GetAll()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                try
                {
                    Repository<Category> categoryRepository = new Repository<Category>(dcp);
                    return _mapper.Map<List<Category>, List<CategoryDO>>(categoryRepository.GetAll().ToList());
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public bool AddCategory(CategoryDO category)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Guid generateGuid = new Guid();
                Category dbObject = _mapper.Map<CategoryDO, Category>(category);
                Repository<Category> categoryRepository = new Repository<Category>(dcp);
                categoryRepository.InsertOnSubmit(dbObject);
                dcp.CommitChanges();
                return true;
            }

        }
    }
}

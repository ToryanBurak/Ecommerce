using AutoMapper;
using DataContext.EntityFramework;
using DataContext.EntityFramework.Provider;
using Domain.Store;
using Repository.EFContextRepository;

namespace BL.Store
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
        public CategoryDO GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
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

        public void UpdateCategory(CategoryDO category)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {

                Repository<Category> categoryRepository = new Repository<Category>(dcp);
                Category dbObject = _mapper.Map<Category>(category);
                categoryRepository.UpdateByIdOnSubmit(dbObject);
                dcp.CommitChanges();
            }

        }
    }
}

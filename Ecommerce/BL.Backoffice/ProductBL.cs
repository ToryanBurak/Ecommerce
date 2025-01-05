using AutoMapper;
using DataContext.EntityFramework.Provider;
using DataContext.EntityFramework;
using Domain.Backoffice;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Backoffice
{
    public class ProductBL
    {
        private readonly IMapper _mapper;
        public ProductBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<ProductDO> GetAll()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Product> productRepository = new Repository<Product>(dcp);
                return _mapper.Map<List<Product>, List<ProductDO>>(productRepository.GetAll().ToList());
            }
        }

        public void AddProduct(ProductDO product)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Guid generateGuid = Guid.NewGuid();
                Product dbObject = _mapper.Map<Product>(product);
                Repository<Product> productRepository = new Repository<Product>(dcp);
                productRepository.InsertOnSubmit(dbObject);
                dcp.CommitChanges();
            }

        }
    }
}

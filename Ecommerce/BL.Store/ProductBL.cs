using AutoMapper;
using DataContext.EntityFramework;
using DataContext.EntityFramework.Provider;
using Domain.Backoffice;
using Microsoft.EntityFrameworkCore;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store
{
    public class ProductBL
    {
        private IMapper _mapper;

        public ProductBL(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<ProductDO> GetAll()
        {
            DbContextProvider dataContextProvider = new DbContextProvider();
            Repository<Product> repProduct = new Repository<Product>(dataContextProvider);
            List<Product> productList = repProduct.GetAll().ToList();
            return _mapper.Map<List<ProductDO>>(productList);
        }
    }
}

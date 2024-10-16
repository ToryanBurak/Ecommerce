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

namespace BL.Backoffice
{
    public class ProductBL
    {
        public ProductDO GetById(int id)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Product> productRepository = new Repository<Product>(dcp);
            }
            
        }
    }
}

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
    public class OrderBL
    {
        private readonly IMapper _mapper;
        public OrderBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<OrderDO> GetAll()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Order> orderRepository = new Repository<Order>(dcp);
                return _mapper.Map<List<Order>, List<OrderDO>>(orderRepository.GetAll().ToList());
            }
        }
        public OrderDO GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}

using DataContext.EntityFramework.Provider;
using DataContext.EntityFramework;
using Domain.Backoffice;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BL.Backoffice
{
    public class OrderItemBL
    {
        private readonly IMapper _mapper;
        public OrderItemBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<OrderItemDO> GetAll()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                try
                {
                    Repository<OrderItem> orderItemRepository = new Repository<OrderItem>(dcp);
                    return _mapper.Map<List<OrderItem>, List<OrderItemDO>>(orderItemRepository.GetAll().ToList());
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public List<OrderItemDO> GetItemListByOrderId(int orderId)
        {
            return GetAll().Where(x => x.OrderId == orderId).ToList();
        }
    }
}

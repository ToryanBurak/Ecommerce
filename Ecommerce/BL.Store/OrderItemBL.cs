using AutoMapper;
using DataContext.EntityFramework;
using DataContext.EntityFramework.Provider;
using Domain.Store;
using Repository.EFContextRepository;

namespace BL.Store
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

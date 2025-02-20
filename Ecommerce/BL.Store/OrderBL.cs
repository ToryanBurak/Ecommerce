using AutoMapper;
using DataContext.EntityFramework;
using DataContext.EntityFramework.Provider;
using Domain.Store;
using Repository.EFContextRepository;

namespace BL.Store
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

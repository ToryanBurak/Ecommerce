using AutoMapper;
using DataContext.EntityFramework;
using DataContext.EntityFramework.Provider;
using Domain.Store;
using Domain.Store.Enum;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store
{
    public class CheckoutBL
    {
        private readonly IMapper _mapper;
        public CheckoutBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public bool Checkout(CheckoutViewModel checkoutViewModel, UserDO user)
        {
            bool success = false;
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Order> orderRepository = new Repository<Order>(dcp);
                Repository<OrderItem> orderItemRepository = new Repository<OrderItem>(dcp);
                using (var transaction = dcp.GetDataContext().Database.BeginTransaction())
                {
                    try
                    {
                        Order order = new Order()
                        {
                            UserId = checkoutViewModel.Cart.UserId,
                            AddressId = checkoutViewModel.Adress.Id,
                            Amount = checkoutViewModel.Cart.TotalPrice,
                            State = (int)OrderStatusEnum.ApproveWaiting

                        };
                        orderRepository.InsertOnSubmit(order);
                        dcp.CommitChanges();
                        int orderid = dcp.GetDataContext().Entry(order).Entity.Id;
                        foreach (CartItemDO cartItem in checkoutViewModel.Cart.CartItems)
                        {
                            OrderItem orderItem = new OrderItem()
                            {
                                OrderId = orderid,
                                ProductId = cartItem.ProductId,
                                Amount = cartItem.Product.Price
                            };
                            orderItemRepository.InsertOnSubmit(orderItem);
                        }
                        transaction.Commit();
                        success = true;
                    }
                    catch (Exception)
                    {
                        success = false;
                        transaction.Rollback();
                    }
                }
            }
            return success;
        }
    }
}

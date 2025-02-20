using DataContext.EntityFramework.Provider;
using DataContext.EntityFramework;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Store;

namespace BL.Store
{
    public class CartBL
    {
        private readonly IMapper _mapper;
        public CartBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<CartDO> GetAll()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Cart> cartRepository = new Repository<Cart>(dcp);
                return _mapper.Map<List<Cart>, List<CartDO>>(cartRepository.GetAll().ToList());
            }
        }
        public CartDO GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
        public CartDO GetByUserId(int userId)
        {
            return GetAll().FirstOrDefault(x => x.UserId == userId);
        }
        public List<CartItemDO> GetAllCartItem()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<CartItem> cartItemRepository = new Repository<CartItem>(dcp);
                return _mapper.Map<List<CartItem>, List<CartItemDO>>(cartItemRepository.GetAll().ToList());
            }
        }
        public List<CartItemDO> GetCartItemListByCartId(int cartId)
        {
            return GetAllCartItem().Where(x => x.CartId == cartId).ToList();
        }
        public CartItemDO GetCartItemById(int Id)
        {
            return GetAllCartItem().FirstOrDefault(x => x.Id == Id);
        }

        public CartDO NewCart(int userId)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Cart> cartRepository = new Repository<Cart>(dcp);
                Cart dbObject = new Cart()
                {
                    UserId = userId
                };
                cartRepository.InsertOnSubmit(dbObject);
                dcp.CommitChanges();
                return GetByUserId(userId);
            }
        }
        public void AddCartItem(int productId,int userId)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Cart> cartRepository = new Repository<Cart>(_dataContextProvider: dcp);
                Repository<Product> productRepository = new Repository<Product>(dcp);
                Repository<CartItem> cartItemRepository = new Repository<CartItem>(dcp);
                Cart cart = cartRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
                Product product = productRepository.GetAll().FirstOrDefault(x => x.Id == productId);
                CartItem dbObject = new CartItem()
                {
                    CartId = cart.Id,
                    ProductId = productId
                };
                cart.TotalPrice += (decimal)product.Price;
                cartRepository.UpdateByIdOnSubmit(cart);
                cartItemRepository.InsertOnSubmit(dbObject);
                dcp.CommitChanges();
                
            }
        }
        public void RemoveCartItem(int cartItemId)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Cart> cartRepository = new Repository<Cart>(_dataContextProvider: dcp);
                Repository<Product> productRepository = new Repository<Product>(dcp);
                Repository<CartItem> cartItemRepository = new Repository<CartItem>(dcp);
                CartItem cartItem = cartItemRepository.GetAll().FirstOrDefault(x => x.Id == cartItemId);
                Cart cart = cartRepository.GetAll().FirstOrDefault(x => x.Id == cartItem.CartId);
                cart.TotalPrice -= (decimal)cartItem.Product.Price;
                cartRepository.UpdateByIdOnSubmit(cart);
                cartItemRepository.DeleteByIdOnSubmit(cartItem.Id);
                dcp.CommitChanges();

            }
        }
        public void RefreshCart(int userId)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Cart> cartRepository = new Repository<Cart>(_dataContextProvider: dcp);
                Repository<CartItem> cartItemRepository = new Repository<CartItem>(dcp);
                Cart cart = cartRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
                List<int> cartItemIdList = cartItemRepository.GetAll().Where(x => x.CartId == cart.Id).Select(a=>a.Id).ToList();
                cartItemRepository.DeleteByIdListOnSubmit(cartItemIdList);
                cart.TotalPrice = 0;
                cartRepository.UpdateByIdOnSubmit(cart);
                dcp.CommitChanges();

            }
        }
    }
}

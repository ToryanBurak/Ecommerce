using AutoMapper;
using DataContext.EntityFramework.Provider;
using DataContext.EntityFramework;
using Domain.Store;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;
using System.Text;
using Repository.EFContextRepository;

namespace BL.Store
{
    public class UserBL
    {
        private readonly IMapper _mapper;
        public UserBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<UserDO> GetAll()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<User> userRepository = new Repository<User>(dcp);
                return _mapper.Map<List<User>, List<UserDO>>(userRepository.GetAll().ToList());
            }
        }
        private List<User> GetAllDbObject()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<User> userRepository = new Repository<User>(dcp);
                return userRepository.GetAll().ToList();
            }
        }
        public UserDO GetUserByID(int id)
        {
            try
            {
                return _mapper.Map<UserDO>(GetAll().FirstOrDefault(x => x.Id == id));
            }
            catch (Exception)
            {
                return null;
            }

        }

        public UserDO GetUserByGuid(Guid guid)
        {
            UserDO userDO = GetAll().FirstOrDefault(x => x.Guid == guid.ToString());
            if (userDO != null)
            {
                return userDO;
            }
            return null;

        }


    }
}

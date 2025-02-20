using AutoMapper;
using DataContext.EntityFramework.Provider;
using DataContext.EntityFramework;
using Domain.Backoffice;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;
using System.Text;
using Repository.EFContextRepository;
using Domain.Backoffice;
using Domain.Backoffice.UserLogin;

namespace BL.Backoffice
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
            UserDO userDO = GetAll().FirstOrDefault(x => x.Guid == guid);
            if (userDO != null)
            {
                return userDO;
            }
            return null;

        }

        private string CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public LoginResponse LoginCheck(LoginViewModel loginViewModel)
        {
            LoginResponse loginResponse = new LoginResponse();
            UserDO user = GetAll().FirstOrDefault(x => x.Email == loginViewModel.Email && x.IsAdmin == true);
            if (user != null)
            {

                MD5 md5 = MD5.Create();
                string saltPassword = loginViewModel.Password + user.SaltString;
                if (user.Password == Convert.ToHexString(md5.ComputeHash(Encoding.Default.GetBytes(saltPassword))))
                {
                    loginResponse.UserID = user.Id;
                    loginResponse.IsSuccess = true;
                    loginResponse.Message = "Başarıyla Giriş Yapıldı.";
                    return loginResponse;
                }
                else
                {
                    loginResponse.IsSuccess = false;
                    loginResponse.Message = "Giriş Başarısız.Geçersiz Kullanıcı Adı Veya Şifre";
                    return loginResponse;
                }
            }
            else
            {
                loginResponse.IsSuccess = false;
                loginResponse.Message = "Giriş Başarısız.Geçersiz Kullanıcı Adı Veya Şifre";
                return loginResponse;
            }
        }

    }
}

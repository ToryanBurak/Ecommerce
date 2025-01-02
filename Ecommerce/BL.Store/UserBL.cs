using AutoMapper;
using DataContext.EntityFramework.Provider;
using DataContext.EntityFramework;
using Domain.Store;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;
using System.Text;
using Repository.EFContextRepository;
using Domain.Store.UserLogin;
using Domain.Store.Enum;
using RentACar.Global.User;

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
            UserDO userDO = GetAll().FirstOrDefault(x => x.Guid == guid);
            if (userDO != null)
            {
                return userDO;
            }
            return null;

        }

        public RegisterResponse CheckUniqueFieldsForRegister(RegisterViewModel model)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                RegisterResponse response = new RegisterResponse();
                Repository<User> repUser = new Repository<User>(dcp);
                bool uniqueemail = repUser.GetAll().Any(x => x.Email == model.Email);
                if (uniqueemail)
                {
                    response.IsSuccessfull = false;
                    response.ErrorMessage = "Kayıt olmak istediğiniz Email sistemde kayıtlı";
                    return response;
                }
                response.IsSuccessfull = true;
                return response;
            }
        }

        public RegisterResponse Register(RegisterViewModel model, Guid guid)
        {
            RegisterResponse registerResponse = new RegisterResponse();
            MD5 md5 = MD5.Create();
            string saltstring = CreateSalt(5);
            byte[] hashedPassword = md5.ComputeHash(Encoding.Default.GetBytes(model.Password + saltstring));
            Random random = new Random();
            string verifyConfirmCode = random.Next(100000, 999999).ToString();

            using (DbContextProvider dbContextProvider = new DbContextProvider())
            {
                Repository<User> repUser = new Repository<User>(dbContextProvider);
                User forRegister = new User()
                {
                    Password = Convert.ToHexString(hashedPassword),
                    SaltString = saltstring,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    VerifyConfirmCode = verifyConfirmCode,
                    VerifyState = (int)VerifyStateEnum.Unverified,
                    Phone = model.Phone,
                    Guid = guid,
                };
                repUser.InsertOnSubmit(forRegister);
                dbContextProvider.CommitChanges();
            }
            return registerResponse;

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
            UserDO user = GetAll().FirstOrDefault(x => x.Email == loginViewModel.Email);
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

        public bool UpdateUserVerifyState(int id, VerifyStateEnum verifyStateEnum)
        {
            User user = GetAllDbObject().FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                DbContextProvider dbContextProvider = new DbContextProvider();
                Repository<User> userRepository = new Repository<User>(dbContextProvider);
                user.VerifyState = ((int)verifyStateEnum);
                userRepository.UpdateByIdOnSubmit(user);
                userRepository.DCP.CommitChanges();
                return true;
            }
            return false;
        }

    }
}

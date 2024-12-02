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
        //public RegisterResponse CheckUniqueFieldsForRegister(RegisterViewModel model)
        //{
        //    using (DbContextProvider dcp = new DbContextProvider())
        //    {
        //        RegisterResponse response = new RegisterResponse();
        //        Repository<User> repUser = new Repository<User>(dcp);
        //        bool uniqueemail = repUser.GetAll().Any(x => x.Email == model.Email);
        //        bool uniqueuserName = repUser.GetAll().Any(x => x.UserName == model.UserName);
        //        if (uniqueemail)
        //        {
        //            response.IsSuccessfull = false;
        //            response.ErrorMessage = "Kayıt olmak istediğiniz Email sistemde kayıtlı";
        //            return response;
        //        }
        //        if (uniqueuserName)
        //        {
        //            response.IsSuccessfull = false;
        //            response.ErrorMessage = "Kayıt olmak istediğiniz Kullanıcı Adı sistemde kayıtlı";
        //            return response;
        //        }
        //        response.IsSuccessfull = true;
        //        return response;
        //    }
        //}

        //public RegisterResponse Register(RegisterViewModel model, int imageUrlID, Guid guid)
        //{
        //    RegisterResponse registerResponse = new RegisterResponse();
        //    MD5 md5 = MD5.Create();
        //    string saltstring = CreateSalt(5);
        //    byte[] hashedPassword = md5.ComputeHash(Encoding.Default.GetBytes(model.Password + saltstring));
        //    Random random = new Random();
        //    string verifyConfirmCode = random.Next(100000, 999999).ToString();

        //    using (DbContextProvider dbContextProvider = new DbContextProvider())
        //    {
        //        Repository<Wallet> walletRepository = new Repository<Wallet>(dbContextProvider);
        //        int walletId = 0;
        //        using (IDbContextTransaction transaction = walletRepository.dataContext.Database.BeginTransaction())
        //        {
        //            Wallet wallet = new Wallet()
        //            {
        //                Amount = 0
        //            };
        //            walletRepository.InsertOnSubmit(wallet);
        //            walletRepository.DCP.CommitChanges();
        //            walletId = walletRepository.dataContext.ChangeTracker.Entries<Wallet>().Select(x => x.Entity).First().Id;
        //            transaction.Commit();
        //        }


        //        Repository<User> repUser = new Repository<User>(dbContextProvider);
        //        User forRegister = new User()
        //        {
        //            UserName = model.UserName,
        //            Password = Convert.ToHexString(hashedPassword),
        //            SaltString = saltstring,
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            Email = model.Email,
        //            VerifyConfirmCode = verifyConfirmCode,
        //            Gender = (int)model.Gender,
        //            Address = model.Address,
        //            Age = model.Age,
        //            VerifyState = (int)VerifyStateEnum.Unverified,
        //            Phone = model.Phone,
        //            Tc = model.TC,
        //            RentRequestWarrant = false,
        //            ImageUrlId = imageUrlID != 0 ? imageUrlID : null,
        //            Guid = guid,
        //            WalletId = walletId
        //        };
        //        repUser.InsertOnSubmit(forRegister);
        //        dbContextProvider.CommitChanges();
        //    }


        //    return registerResponse;

        //}

        //private string CreateSalt(int size)
        //{
        //    var rng = new RNGCryptoServiceProvider();
        //    var buff = new byte[size];
        //    rng.GetBytes(buff);
        //    return Convert.ToBase64String(buff);
        //}

        //public LoginResponse LoginCheck(LoginViewModel loginViewModel)
        //{
        //    LoginResponse loginResponse = new LoginResponse();
        //    UserDO user = GetAll().FirstOrDefault(x => x.UserName == loginViewModel.UserName);
        //    if (user != null)
        //    {

        //        MD5 md5 = MD5.Create();
        //        string saltPassword = loginViewModel.Password + user.SaltString;
        //        if (user.Password == Convert.ToHexString(md5.ComputeHash(Encoding.Default.GetBytes(saltPassword))))
        //        {
        //            loginResponse.UserID = user.ID;
        //            loginResponse.IsSuccess = true;
        //            loginResponse.Message = "Başarıyla Giriş Yapıldı.";
        //            return loginResponse;
        //        }
        //        else
        //        {
        //            loginResponse.IsSuccess = false;
        //            loginResponse.Message = "Giriş Başarısız.Geçersiz Kullanıcı Adı Veya Şifre";
        //            return loginResponse;
        //        }
        //    }
        //    else
        //    {
        //        loginResponse.IsSuccess = false;
        //        loginResponse.Message = "Giriş Başarısız.Geçersiz Kullanıcı Adı Veya Şifre";
        //        return loginResponse;
        //    }
        //}

        //public UserDO GetUserByMail(ForgetPasswordViewModel model)
        //{

        //    if (!String.IsNullOrWhiteSpace(model.Email))
        //    {
        //        using (DbContextProvider dcp = new DbContextProvider())
        //        {
        //            Repository<User> userRepository = new Repository<User>(dcp);
        //            UserDO user = GetAll().FirstOrDefault(x => x.Email == model.Email);
        //            if (user != null)
        //            {
        //                return user;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public bool UpdateUserVerifyState(int id, VerifyStateEnum verifyStateEnum)
        //{
        //    User user = GetAllDbObject().FirstOrDefault(x => x.Id == id);
        //    if (user != null)
        //    {
        //        DbContextProvider dbContextProvider = new DbContextProvider();
        //        Repository<User> userRepository = new Repository<User>(dbContextProvider);
        //        user.VerifyState = ((int)verifyStateEnum);
        //        userRepository.UpdateByIdOnSubmit(user);
        //        userRepository.DCP.CommitChanges();
        //        return true;
        //    }
        //    return false;
        //}

        //public bool UpdatePassword(string newPassword, Guid userGuid)
        //{
        //    if (!String.IsNullOrWhiteSpace(newPassword))
        //    {
        //        using (DbContextProvider dcp = new DbContextProvider())
        //        {
        //            Repository<User> userRepository = new Repository<User>(dcp);
        //            User user = GetAllDbObject().FirstOrDefault(x => x.Guid == userGuid);
        //            if (user != null)
        //            {
        //                string newHashedPassword = HashMD5(newPassword, user.SaltString);
        //                user.Password = newHashedPassword;
        //                userRepository.InsertOnSubmit(user);
        //                dcp.CommitChanges();
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //private string HashMD5(string value, string saltString)
        //{
        //    MD5 md5 = MD5.Create();
        //    return Convert.ToHexString(md5.ComputeHash(Encoding.Default.GetBytes(value + saltString)));
        //}

    }
}

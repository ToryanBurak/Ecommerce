using BL.Store;
using DataContext.EntityFramework;
using Domain.Backoffice;
using Domain.Store;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace UI.Ecommerce.Helper
{
    public class SelectHelper
    {
        public static IEnumerable<SelectListItem> GetCategoryList()
        {
            var mapper = StaticMapper.Mapper;
            CategoryBL categoryBL = new CategoryBL(mapper);
            List<CategoryDO> categoryList = categoryBL.GetAll();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "Lütfen Kategori Seçiniz..", Value = "" });
            selectList.AddRange(categoryList.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList());
            return selectList;
        }
        public static IEnumerable<SelectListItem> GetCityList()
        {
            var mapper = StaticMapper.Mapper;
            AdressBL adressBL = new AdressBL(mapper);
            List<CityDO> cityList = adressBL.GetAllCityList();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "Lütfen İl Seçiniz..", Value = "" });
            selectList.AddRange(cityList.Select(x => new SelectListItem { Text = x.Name, Value = x.Key.ToString() }).ToList());
            return selectList;
        }
        public static IEnumerable<SelectListItem> GetDistrictListByCityKey(int cityKey)
        {
            var mapper = StaticMapper.Mapper;
            AdressBL adressBL = new AdressBL(mapper);
            List<DistrictDO> districtList = adressBL.GetAllDistrictListByCityKey(cityKey);
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "Lütfen İlçe Seçiniz..", Value = "" });
            selectList.AddRange(districtList.Select(x => new SelectListItem { Text = x.Name, Value = x.Key.ToString() }).ToList());
            return selectList;
        }
        public static IEnumerable<SelectListItem> GetTownListByDistrictKey(int districtKey)
        {
            var mapper = StaticMapper.Mapper;
            AdressBL adressBL = new AdressBL(mapper);
            List<TownDO> townList = adressBL.GetAllTownListByDistrictKey(districtKey);
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "Lütfen Mahalle Seçiniz..", Value = "" });
            selectList.AddRange(townList.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList());
            return selectList;
        }
        public static IEnumerable<SelectListItem> GetUserAddressList(string userGuid)
        {
            var mapper = StaticMapper.Mapper;
            UserBL userBL = new UserBL(mapper);
            int userId = userBL.GetUserByGuid(Guid.Parse(userGuid)).Id;
            AdressBL adressBL = new AdressBL(mapper);
            List<AddressDO> adressList = adressBL.GetAllAdressListByUserId(userId);
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.AddRange(adressList.Select(x => new SelectListItem { Text = x.AddressDescription, Value = x.Id.ToString() }).ToList());
            return selectList;
        }
        public static int GetUserIdByGuid(string userGuid)
        {
            var mapper = StaticMapper.Mapper;
            UserBL userBL = new UserBL(mapper);
            return userBL.GetUserByGuid(Guid.Parse(userGuid)).Id;
        }
        public static string GetCategoryNameById(int id)
        {
            var mapper = StaticMapper.Mapper;
            CategoryBL categoryBL = new CategoryBL(mapper);
            return categoryBL.GetAll().FirstOrDefault(x => x.Id == id).Name;
        }

        public static string GetDescriptionFromEnumValue<TEnum>(int value) where TEnum : Enum
        {
            var enumValue = (TEnum)(object)value;

            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            if (fieldInfo != null)
            {
                var descriptionAttribute = fieldInfo
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
            }
            return enumValue.ToString();
        }
    }
}

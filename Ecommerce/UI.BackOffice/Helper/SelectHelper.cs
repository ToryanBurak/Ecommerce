using BL.Backoffice;
using Domain.Backoffice;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace UI.BackOffice.Helper
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

using BL.Backoffice;
using Domain.Backoffice;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    }
}

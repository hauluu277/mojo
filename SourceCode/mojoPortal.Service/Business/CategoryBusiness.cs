using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Business;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.Category;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace mojoPortal.Service.Business
{
    public class CategoryBusiness : BaseBusiness<core_Category>
    {
        public CategoryBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public int CountByParent(int parentId)
        {
            return this.GetAllAsQueryable().Where(x => x.ParentID == parentId).Count();
        }

        public List<core_Category> GetChild(int parentId)
        {
            return this.context.core_Category.Where(x => x.ParentID == parentId).ToList();
        }
        public List<CategoryBO> GetChildByCode(string code, int siteId)
        {
            var parent = this.Filter(x => x.Code == code && x.SiteID == siteId).FirstOrDefault();
            if (parent != null)
            {
                var searchResult = this.Filter(x => x.ParentID == parent.ItemID).ToList();
                if (searchResult != null && searchResult.Any())
                {
                    var result = new List<CategoryBO>();
                    foreach (var item in searchResult)
                    {
                        var category = new CategoryBO();
                        category = MaperData.Map<CategoryBO, core_Category>(category, item);
                        category.TotalDieuTra = this.context.mp_Sites.Where(x => x.LinhVucID == item.ItemID).Count();
                        result.Add(category);
                    }
                    return result;
                }
            }
            return new List<CategoryBO>();
        }
        public string GetName(int id = 0)
        {
            if (id > 0)
            {
                var search = this.Find(id);
                if (search != null) return search.Name;
            }
            return string.Empty;
        }

        public core_Category GetById(int id = 0)
        {
            if (id > 0)
            {
                var search = this.Find(id);
                if (search != null)
                {
                    return search;
                }
            }
            return new core_Category();
        }

        public core_Category GetByCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var search = context.core_Category.FirstOrDefault(x => x.Code.Equals(code));
                if (search != null)
                {
                    return search;
                }
            }
            return new core_Category();
        }

        public List<SelectListItem> GetChildItem(string parentCode, int siteId, int? selected = 0)
        {
            var search = this.Filter(x => x.Code == parentCode && x.SiteID == siteId).FirstOrDefault();
            if (search != null)
            {
                return this.Filter(x => x.ParentID == search.ItemID).
                    Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (x.ItemID == selected) }).ToList();
            }
            return new List<SelectListItem>();
        }




        public List<core_Category> GetListChild(string code)
        {
            var parent = GetByCode(code);
            var listChild = context.core_Category.Where(x => x.ParentID == parent.ItemID).ToList();
            return listChild;
        }
        public List<SelectListItem> GetListChildSelectItem(string code, long? selected = 0)
        {
            var parent = GetByCode(code);
            var result = context.core_Category.Where(x => x.ParentID == parent.ItemID)
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.ItemID.ToString(),
                    Selected = selected == x.ItemID
                }).ToList();
            return result;
        }
    }
}

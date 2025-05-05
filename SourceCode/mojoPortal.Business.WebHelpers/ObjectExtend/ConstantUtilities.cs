using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonHelper.ObjectExtend
{
    public static class ConstantUtilities
    {
        public static IEnumerable<SelectListItem> GetDropDown<TConst>(string selectedItem = null)
        {
            var result = new List<SelectListItem>();
            var properties = typeof(TConst).GetProperties();
            if (properties != null)
            {
                foreach (var item in properties)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        yield return new SelectListItem()
                        {
                            Text = name,
                            Value = val,
                            Selected = !string.IsNullOrEmpty(selectedItem) ? val == selectedItem : false
                        };
                    }
                }
            }
        }

        public static string GetNameByCode<TConst>(object code = null)
        {
            string result = string.Empty;
            var properties = typeof(TConst).GetProperties();
            if (properties != null)
            {
                result = properties.Where(x => x.GetValue(x).Equals(code))
                    .Select(x => x.GetAttribute<DisplayNameAttribute>(true).DisplayName)
                    .FirstOrDefault() ?? string.Empty;
            }
            return result;
        }

    }
}

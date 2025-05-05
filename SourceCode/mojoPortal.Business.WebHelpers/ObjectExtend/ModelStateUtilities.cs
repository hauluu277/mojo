using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonHelper.ObjectExtend
{
    public static class ModelStateUtilities
    {
        public static string GetErrorMessages(this ModelStateDictionary modelState)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in modelState)
            {
                var state = item.Value;
                if (state.Errors.Any())
                {
                    foreach (var error in state.Errors)
                    {
                        sb.Append(string.Format("{0} <br/>", error.ErrorMessage));
                    }
                }
            }
            string result = sb.ToString();
            return result;
        }
    }
}

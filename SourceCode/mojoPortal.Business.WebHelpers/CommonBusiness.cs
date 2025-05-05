using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace mojoPortal.Business.WebHelpers
{
    public static class CommonBusiness
    {

        public static string NotifyLabel(int total, int totalAll)
        {
            if (totalAll == 0)
            {
                return "Hiển thị 0 bản ghi";
            }
            else
            {
                return string.Format("Hiển thị <span class='bold'>1 - {0}</span> trong tổng số <span class='bold'>{1}</span>", total, totalAll);
            }
        }


        public static string GetPathCkfinder(SiteUser currentUser, SiteSettings siteSettings)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {

                CultureInfo cul = CultureInfo.CurrentCulture;
                var current = DateTime.Now;
                int weekNum = cul.Calendar.GetWeekOfYear(
                    current,
                    CalendarWeekRule.FirstDay,
                    DayOfWeek.Monday);

                string pathCkfinderUser = $"/data/media/{current.Year}/{current.Month}/{weekNum}/{string.Format("{0:dd}", current)}/{siteSettings.SiteId}/{currentUser.LoginName}";
                string pathCkfinder = HttpContext.Current.Server.MapPath(pathCkfinderUser);
                if (!Directory.Exists(pathCkfinder))
                {
                    Directory.CreateDirectory(pathCkfinder);
                }
                return pathCkfinderUser + "/";
            }
            return string.Empty;
        }

        public static void LoadPathCkfinder(SiteUser currentUser, SiteSettings siteSettings)
        {
            HttpContext.Current.Session["CkfinderPath"] = null;
            //Session[ConfigurationManager.AppSettings["CkfinderPath"]] = null;

            if (HttpContext.Current.Request.IsAuthenticated)
            {

                CultureInfo cul = CultureInfo.CurrentCulture;
                var current = DateTime.Now;
                int weekNum = cul.Calendar.GetWeekOfYear(
                    current,
                    CalendarWeekRule.FirstDay,
                    DayOfWeek.Monday);

                string pathCkfinderUser = $"/data/media/{current.Year}/{current.Month}/{weekNum}/{string.Format("{0:dd}", current)}/{siteSettings.SiteId}/{currentUser.LoginName}/";
                string pathCkfinder = HttpContext.Current.Server.MapPath(pathCkfinderUser);
                if (!Directory.Exists(pathCkfinder))
                {
                    Directory.CreateDirectory(pathCkfinder);
                }
                HttpContext.Current.Session["CkfinderPath"] = pathCkfinderUser + "/";
            }
        }

        public static bool SowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                string filePath = imageUrl;
                if (imageUrl.Contains("/"))
                {
                    filePath = HttpContext.Current.Server.MapPath(imageUrl);
                }
                else
                {
                    filePath = HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + imageUrl;

                }

                filePath = filePath.Replace("/", "\\");
                return File.Exists(filePath);
            }
        }

        public static void PopulateChildNode(ListControl root)
        {
            for (int i = 0; i < root.Items.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(root.Items[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root.Items[i].Text.StartsWith("|"))
                {
                    prefix += root.Items[i].Text.Substring(0, 3);
                    root.Items[i].Text = root.Items[i].Text.Remove(0, 3);
                }
                root.Items[i].Text = prefix + root.Items[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString()
                    };
                    root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    index++;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Components
{
    public static class SiteContants
    {
        public class SkinType
        {
            public const int SUBPORTAL = 1;
            public const int SAN_PHAM = 2;
            public const int LAND_PAGE = 3;
        }
        public static List<ListItem> GetListSkinType()
        {
            List<ListItem> listSkin = new List<ListItem>();
            listSkin.Add(new ListItem { Value = SkinType.SUBPORTAL.ToString(), Text = "Template" });
            listSkin.Add(new ListItem { Value = SkinType.SAN_PHAM.ToString(), Text = "Sản phẩm" });
            listSkin.Add(new ListItem { Value = SkinType.LAND_PAGE.ToString(), Text = "Land page" });
            return listSkin;
        }

        public static string GetSkinType(int skinType)
        {
            var result = string.Empty;
            switch (skinType)
            {
                case (SkinType.SUBPORTAL):
                    result = "Template";
                    break;
                case (SkinType.SAN_PHAM):
                    result = "Sản phẩm";
                    break;
                case (SkinType.LAND_PAGE):
                    result = "Land page";
                    break;
                default:
                    result = "Template";
                    break;
            }
            return result;
        }
    }
}
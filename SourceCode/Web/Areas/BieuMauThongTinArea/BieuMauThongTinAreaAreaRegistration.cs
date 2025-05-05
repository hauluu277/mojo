using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BieuMauThongTinArea
{
    public class BieuMauThongTinAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BieuMauThongTinArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BieuMauThongTinArea_default",
                "BieuMauThongTinArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.QLLichLamViecArea
{
    public class QLLichLamViecAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QLLichLamViecArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QLLichLamViecArea_default",
                "QLLichLamViecArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
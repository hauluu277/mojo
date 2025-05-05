using System.Web.Mvc;

namespace mojoPortal.Web.Areas.CategoryArea
{
    public class CategoryAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CategoryArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CategoryArea_default",
                "CategoryArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
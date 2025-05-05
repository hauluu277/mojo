using mojoportal.Service.UoW;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace mojoPortal.Web.Base
{
    public class BaseController : Controller
    {
        // GET: Base
        private Dictionary<string, object> groupBusiness = new Dictionary<string, object>();
        private UnitOfWork unitOfWork = new UnitOfWork();
        protected readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected CategoryBusiness categoryBusiness;
        protected SiteBusiness siteBusiness;
        protected SettingServiceBusiness settingServiceBusiness;
        protected core_CCTC_DepartmentBusiness core_CCTC_DepartmentBusiness;
        protected core_CCTC_LeaderBusiness core_CCTC_LeaderBusiness;
        protected core_CTTCBusiness core_CTTCBusiness;
        protected RoleBusiness roleBusiness;
        protected static readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        protected BaoCaoBusiness baoCaoBusiness;
        protected ThuTucBusiness thuTucBusiness;
        protected ThuTucBieuMauBusiness thuTucBieuMauBusiness;
        protected GiaoDienBusiness giaoDienBusiness;
        protected ThuTucThanhPhanBusiness thuTucThanhPhanBusiness;
        protected QLLogBusiness qlLogBusiness;
        protected QLLienHeBusiness qlLienHeBusiness;

        protected CauHinhHienThiLogBusiness cauHinhHienThiLogBusiness;
        protected BieuMauThongTinBusiness BieuMauThongTinBusiness;
        protected TieuChiBieuMauBusiness TieuChiBieuMauBusiness;
        protected KeKhaiBieuMauBusiness KeKhaiBieuMauBusiness;
        protected NopBieuMauBusiness NopBieuMauBusiness;

        protected core_ClientBusiness core_ClientBusiness;
        protected md_ArticlesBusiness md_ArticlesBusiness;
        protected md_ArticleCategoryBusiness md_ArticleCategoryBusiness;
        protected LichLamViecBusiness lichLamViecBusiness;
        protected core_DanhMucBusiness core_DanhMucBusiness;
        public T Get<T>() where T : class
        {
            var type = typeof(T);
            var containsKey = groupBusiness.ContainsKey(type.Name);
            if (containsKey)
            {
                return (T)groupBusiness[type.Name];
            }
            try
            {
                T instance = (T)Activator.CreateInstance(type, unitOfWork);
                groupBusiness.Add(type.Name, instance);
                return instance;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {

                string culture = filterContext.RouteData.Values["culture"]?.ToString()
                              ?? "vi-VN";
                // Set the action parameter just in case we didn't get one
                // from the route.
                filterContext.ActionParameters["culture"] = culture;
                var cultureInfo = CultureInfo.GetCultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                // Because we've overwritten the ActionParameters, we
                // make sure we provide the override to the
                // base implementation.


                bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
                if (!skipAuthorization)
                {

                    if (filterContext.HttpContext.Session.IsNewSession || filterContext.HttpContext.Request.IsAuthenticated != true || siteUser == null)
                    {
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.HttpContext.Response.StatusCode = 401;
                            filterContext.HttpContext.Response.End();
                        }
                        else
                        {
                            var url = Request.Url.PathAndQuery.ToString();

                            //filterContext.Result = RedirectToAction("login", "account", new { area = "", returnUrl = url });
                            //filterContext.Result = $"Secure/Login.aspx?returnurl={url}";
                            filterContext.HttpContext.Response.Redirect($"/Secure/Login.aspx?returnurl={url}");
                        }
                        return;
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
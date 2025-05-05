using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Secure
{
    public partial class Action_SignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.IsAuthenticated)
                {
                    SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();

                    string winliveCookieName = "winliveid"
                        + siteSettings.SiteId.ToString(CultureInfo.InvariantCulture);

                    string roleCookieName = SiteUtils.GetRoleCookieName(siteSettings);

                    HttpCookie roleCookie = new HttpCookie(roleCookieName, string.Empty);
                    roleCookie.Expires = DateTime.Now.AddMinutes(1);
                    roleCookie.Path = "/";
                    Response.Cookies.Add(roleCookie);

                    HttpCookie displayNameCookie = new HttpCookie("DisplayName", string.Empty);
                    displayNameCookie.Expires = DateTime.Now.AddMinutes(1);
                    displayNameCookie.Path = "/";
                    Response.Cookies.Add(displayNameCookie);


                    HttpCookie CkfinderPath = new HttpCookie("user_CkfinderPath", string.Empty);
                    CkfinderPath.Expires = DateTime.Now.AddMinutes(1);
                    CkfinderPath.Path = "/";
                    Response.Cookies.Add(CkfinderPath);


                    CookieHelper.ExpireCookie("siteguid" + siteSettings.SiteGuid);

                    bool useFolderForSiteDetection = ConfigHelper.GetBoolProperty("UseFoldersInsteadOfHostnamesForMultipleSites", false);
                    if ((useFolderForSiteDetection) && (!WebConfigSettings.UseRelatedSiteMode))
                    {
                        string cookieName = "siteguid" + siteSettings.SiteGuid.ToString();

                        HttpCookie siteCookie = new HttpCookie(cookieName, string.Empty);
                        siteCookie.Expires = DateTime.Now.AddMinutes(1);
                        siteCookie.Path = "/";
                        Response.Cookies.Add(siteCookie);

                        CookieHelper.ExpireCookie("siteguid" + siteSettings.SiteGuid);

                    }
                    else
                    {
                        FormsAuthentication.SignOut();
                    }

                    string winLiveToken = CookieHelper.GetCookieValue(winliveCookieName);
                    WindowsLiveLogin.User liveUser = null;
                    if (winLiveToken.Length > 0)
                    {
                        WindowsLiveLogin windowsLive = WindowsLiveHelper.GetWindowsLiveLogin();

                        try
                        {
                            liveUser = windowsLive.ProcessToken(winLiveToken);
                            if (liveUser != null)
                            {
                                Response.Redirect(windowsLive.GetLogoutUrl());
                                Response.End();

                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }

                    try
                    {
                        if (Session != null) { Session.Abandon(); }
                    }
                    catch (HttpException) { }

                    Session[ConfigurationManager.AppSettings["CkfinderPath"]] = null;
                }
            }
        }
    }
}
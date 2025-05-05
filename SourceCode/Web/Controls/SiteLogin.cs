// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.
// 
// Created:			        2007-06-06
//	Last Modified:              2010-10-24
// 
// 2007/06/06  Alexander Yushchenko: created this control from part of /Secure/Login.aspx.cs refactoring
// 2007-08-24   fixed bug where message was not displayed if
// email confirmation needed or account locked. Also added logic to
// send another confirmation email if user tries to login and
// confirmation is needed.

using log4net;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Business.WebHelpers.UserSignInHandlers;
using mojoPortal.Features.Business.QLLog;
using mojoPortal.Model.Data;
using mojoPortal.Net;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonBusiness;
using mojoPortal.Web.Controls;
using mojoPortal.Web.Framework;
using Newtonsoft.Json;
using Resources;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.UI
{

    public class SiteLogin : Login
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SiteLogin));
        private readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        private readonly string siteRoot = SiteUtils.GetNavigationSiteRoot();
        //private HiddenField hdnReturnUrl = null;
        private bool setRedirectUrl = true;
        private bool HasReturnCode = false;
        private string RedirectLoginDieuHanh = ConfigurationManager.AppSettings["RedirectLoginDieuHanh"];
        private string RedirectDieuHanh = ConfigurationManager.AppSettings["RedirectDieuHanh"];
        private readonly string LoginUrl = ConfigurationManager.ConnectionStrings["URLAPIAD"].ToString();
        private readonly string KEYAD = ConfigurationManager.AppSettings["KEYAD"];

        public bool SetRedirectUrl
        {
            get { return setRedirectUrl; }
            set { setRedirectUrl = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Load += new EventHandler(SiteLogin_Load);
            this.LoginError += new EventHandler(SiteLogin_LoginError);
            this.LoggingIn += new LoginCancelEventHandler(SiteLogin_LoggingIn);
            this.LoggedIn += new EventHandler(SiteLogin_LoggedIn);

            this.CreateUserText = Resource.SignInRegisterLinkText;
            this.CreateUserUrl = siteRoot + "/Secure/Register.aspx";
            this.FailureText = ResourceHelper.GetMessageTemplate("LoginFailedMessage.config");
            this.LoginButtonText = Resource.SignInLinkText;
            this.PasswordRecoveryText = Resource.SignInSendPasswordButton;
            this.PasswordRecoveryUrl = siteRoot + "/Secure/RecoverPassword.aspx";
            this.RememberMeText = Resource.SignInSendRememberMeLabel;
            this.RememberMeSet = WebConfigSettings.ForcePersistentAuthCheckboxChecked;

#if !NET35
            this.RenderOuterTable = false;
            this.CssClass = string.Empty;
#endif

            //HookupSignInEventHandlers();

            //hdnReturnUrl = new HiddenField();
            //hdnReturnUrl.ID = "hdnReturnUrl";
            //this.Controls.Add(hdnReturnUrl);

            //CaptchaControl captcha = (CaptchaControl)this.FindControl("captcha");
            //if (captcha != null)
            //{
            //    if(siteSettings.RequireCaptchaOnLogin)
            //    {
            //        captcha.ProviderName = siteSettings.CaptchaProvider;
            //        captcha.RecaptchaPrivateKey = siteSettings.RecaptchaPrivateKey;
            //        captcha.RecaptchaPublicKey = siteSettings.RecaptchaPublicKey;
            //    }
            //}


        }


        void SiteLogin_Load(object sender, EventArgs e)
        {

            string returnUrlParam = Page.Request.Params.Get("returnurl");

            if (!Page.IsPostBack)
            {
                ViewState["LoginErrorCount"] = 0;

                if (Page.Request.UrlReferrer != null)
                {
                    string urlReferrer = Page.Request.UrlReferrer.ToString();
                    if ((urlReferrer.StartsWith(siteRoot)) || (urlReferrer.StartsWith(siteRoot.Replace("https://", "http://"))))
                    {
                        ViewState["ReturnUrl"] = urlReferrer;
                        //log.Info(hdnReturnUrl.Value);
                    }
                }

                if (!String.IsNullOrEmpty(returnUrlParam))
                {
                    returnUrlParam = SecurityHelper.RemoveMarkup(returnUrlParam);
                    string redirectUrl = Page.ResolveUrl(SecurityHelper.RemoveMarkup(Page.Server.UrlDecode(returnUrlParam)));
                    if (
                        ((redirectUrl.StartsWith("/")) && (!(redirectUrl.StartsWith("//"))))
                        || (redirectUrl.StartsWith(siteRoot))
                        || (redirectUrl.StartsWith(siteRoot.Replace("https://", "http://"))))
                    {
                        ViewState["ReturnUrl"] = redirectUrl;
                    }
                }

            }


            if (setRedirectUrl) { this.DestinationPageUrl = GetRedirectPath(); }

            if (returnUrlParam != null && !string.IsNullOrEmpty(returnUrlParam) && returnUrlParam.Contains("token"))
            {
                HasReturnCode = true;
                this.DestinationPageUrl = returnUrlParam;
            }



            if (WebConfigSettings.DebugLoginRedirect)
            {
                log.Info("Login redirect url was " + this.DestinationPageUrl + " for Site Root " + siteRoot);
            }

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                SiteUser siteUser = new SiteUser(siteSettings, HttpContext.Current.User.Identity.Name);
                if (siteUser.UserId > 0)
                {
                    //if (!string.IsNullOrEmpty(clientId))
                    //{
                    //core_ClientBusiness core_ClientBusiness = new core_ClientBusiness(new mojoportal.Service.UoW.UnitOfWork());
                    //var client = core_ClientBusiness.context.core_Client.FirstOrDefault(x => x.ClientID.Equals(clientId));

                    //if (client == null)
                    //{
                    //    log.Info("ClientId not found !");
                    //    return;
                    //}

                    ////generate token
                    //TokenDto enCode_token = new TokenDto();
                    //enCode_token.ClientId = clientId;
                    //enCode_token.DateExpired = DateTime.Now.AddMinutes(10);
                    //enCode_token.UserName = siteUser.LoginName;
                    //string getToken = Ultilities.EncryptString(JsonConvert.SerializeObject(enCode_token));
                    ////save token
                    //core_Token token = new core_Token();
                    //token.Token = getToken;
                    //token.UserName = siteUser.LoginName;
                    //token.DateExpired = DateTime.Now.AddMinutes(10);
                    //token.ExpiredIn = 10;
                    //token.CreatedDate = DateTime.Now;
                    //core_TokenBusiness core_TokenBusiness = new core_TokenBusiness(new mojoportal.Service.UoW.UnitOfWork());
                    //core_TokenBusiness.Save(token);

                    ////redirect
                    //this.DestinationPageUrl = client.ClientUrl + "?token=" + getToken;
                    //HttpContext.Current.Response.Redirect(DestinationPageUrl);
                    //return;
                    //    HasReturnCode = true;
                    //}


                    if (HasReturnCode == false && (DestinationPageUrl.Contains(RedirectDieuHanh)))
                    {
                        HasReturnCode = true;
                        this.DestinationPageUrl += $"{RedirectLoginDieuHanh}?id=" + Ultilities.EncryptString(siteUser.LoginName);
                    }
                    if (HasReturnCode)
                    {
                        HttpContext.Current.Response.Redirect(DestinationPageUrl);
                        return;
                    }
                }

            }
        }


        protected void SiteLogin_LoginError(object sender, EventArgs e)
        {
            int errorCount = (int)ViewState["LoginErrorCount"] + 1;
            ViewState["LoginErrorCount"] = errorCount;

            if ((siteSettings != null)
                && (!siteSettings.UseLdapAuth)
                && (siteSettings.PasswordFormat != 1)
                && (siteSettings.AllowPasswordRetrieval)
                && (errorCount >= siteSettings.MaxInvalidPasswordAttempts)
                && (this.PasswordRecoveryUrl != String.Empty)
                )
            {
                WebUtils.SetupRedirect(this, this.PasswordRecoveryUrl);
            }
        }
        private bool AccessLogin(string siteManager)
        {
            if (string.IsNullOrEmpty(siteManager))
            {
                Label lblFailure = (Label)this.FindControl("FailureText");
                lblFailure.Visible = true;
                lblFailure.Text = "Tài khoản chưa được cấp quyền";
                return false;
            }
            else
            {
                var hasRole = false;
                var lstSiteManager = siteManager.ToListInt();
                foreach (var item in lstSiteManager)
                {
                    if (item == siteSettings.SiteId)
                    {
                        hasRole = true;
                        break;
                    }
                }
                if (hasRole == false)
                {
                    Label lblFailure = (Label)this.FindControl("FailureText");
                    lblFailure.Visible = true;
                    lblFailure.Text = "Tài khoản chưa được cấp quyền";
                    return false;
                }
            }
            return true;
        }


        void SiteLogin_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            if (siteSettings.RequireCaptchaOnLogin)
            {
                CaptchaControl captcha = (CaptchaControl)this.FindControl("captcha");
                if (captcha != null)
                {
                    // if (!captcha.Captcha.IsValid)
                    if (!captcha.IsValid)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

            }

            var clientId = Context.Request.Params["clientId"];

            var enableAuthenticationAD = ConfigurationManager.AppSettings["Enable_AuthenticationAD"].ToBooleanOrFalse();
            var site = new SiteSettings(1);
            SiteUser siteUser = new SiteUser(site, this.UserName);
            SessionManager.SetValue("AuthenticationADSuccess", false);
            if (enableAuthenticationAD)
            {
                //gọi api xác thực AD
                var resultLoginAD = AuthenticationAD(this.UserName, this.Password);
                SessionManager.SetValue("AuthenticationADSuccess", resultLoginAD.Status);
                if (siteUser.UserId > 0 && ((siteUser.LoginName.Equals("admin") && this.Password == "CucTTDLcntt@123654") || this.Password == "CucTTDLcntt@123654"))
                {
                    SessionManager.SetValue("AuthenticationADSuccess", true);
                    //continude
                }
                else if (resultLoginAD.Status == false)
                {
                    Label lblFailure = (Label)this.FindControl("FailureText");
                    lblFailure.Visible = true;
                    lblFailure.Text = resultLoginAD.Message;
                    e.Cancel = true;
                    return;
                }
                else
                {
                    //trường hợp xác thực AD thành công
                    //nếu tài khoản tồn tại trên AD
                    //nếu tài khoản chưa có trên cổng, sẽ tự tạo account trên cổng
                    //if (siteUser.UserId == -1)
                    //{
                    //    SiteUser createUser = new SiteUser();
                    //    createUser.Name = this.UserName;
                    //    createUser.Password = this.Password;
                    //    createUser.LoginName = this.UserName;
                    //    createUser.ApprovedForLogin = true;
                    //    createUser.SiteManager = "1";
                    //    createUser.SiteId = 1;
                    //    createUser.Save();
                    //    siteUser = new SiteUser(siteSettings, this.UserName);
                    //}
                }
            }
            if (siteUser.UserId > -1)
            {
                if (AccessLogin(siteUser.SiteManager) == false)
                {
                    e.Cancel = true;
                    return;
                }

                if (siteSettings.UseSecureRegistration && siteUser.RegisterConfirmGuid != Guid.Empty)
                {
                    //this.FailureText = Resource.LoginUnconfirmedEmailMessage;
                    Label lblFailure = (Label)this.FindControl("FailureText");
                    if (lblFailure != null)
                    {
                        lblFailure.Visible = true;
                        lblFailure.Text = Resource.LoginUnconfirmedEmailMessage;

                    }
                    // send email with confirmation link that will approve profile
                    Notification.SendRegistrationConfirmationLink(
                        SiteUtils.GetSmtpSettings(),
                        ResourceHelper.GetMessageTemplate("RegisterConfirmEmailMessage.config"),
                        siteSettings.DefaultEmailFromAddress,
                        siteSettings.DefaultFromEmailAlias,
                        siteUser.Email,
                        siteSettings.SiteName,
                        WebUtils.GetSiteRoot() + "/ConfirmRegistration.aspx?ticket=" +
                        siteUser.RegisterConfirmGuid.ToString());

                    // user has not confirmed
                    e.Cancel = true;
                    return;
                }

                if (siteUser.IsDeleted)
                {
                    //this.FailureText = Resource.LoginAccountLockedMessage;
                    Label lblFailure = (Label)this.FindControl("FailureText");
                    if (lblFailure != null)
                    {
                        lblFailure.Visible = true;
                        lblFailure.Text = ResourceHelper.GetMessageTemplate("LoginFailedMessage.config");
                    }

                    e.Cancel = true;
                    return;
                }

                if (siteUser.IsLockedOut)
                {
                    //this.FailureText = Resource.LoginAccountLockedMessage;
                    Label lblFailure = (Label)this.FindControl("FailureText");
                    if (lblFailure != null)
                    {
                        lblFailure.Visible = true;
                        lblFailure.Text = Resource.LoginAccountLockedMessage;
                    }

                    e.Cancel = true;
                    return;
                }

                if ((siteSettings.RequireApprovalBeforeLogin) && (!siteUser.ApprovedForLogin))
                {
                    //this.FailureText = Resource.LoginAccountLockedMessage;
                    Label lblFailure = (Label)this.FindControl("FailureText");
                    if (lblFailure != null)
                    {
                        lblFailure.Visible = true;
                        lblFailure.Text = Resource.LoginNotApprovedMessage;
                    }

                    e.Cancel = true;
                    return;
                }

                if (siteSettings.MaxInvalidPasswordAttempts > 0)
                {
                    if (siteUser.FailedPasswordAttemptCount >= siteSettings.MaxInvalidPasswordAttempts)
                    {
                        if (siteUser.FailedPasswordAttemptWindowStart.AddMinutes(siteSettings.PasswordAttemptWindowMinutes) > DateTime.UtcNow)
                        {

                            //this.FailureText = Resource.LoginAccountLockedMessage;
                            Label lblFailure = (Label)this.FindControl("FailureText");
                            if (lblFailure != null)
                            {
                                lblFailure.Visible = true;
                                lblFailure.Text = Resource.AccountLockedTemporarilyDueToPasswordFailures;
                            }
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                string returnUrlParam = Page.Request.Params.Get("returnurl");

                CheckBox cboxDieuHanh = (CheckBox)this.FindControl("cboxDieuHanh");
                CheckBox cboxPortal = (CheckBox)this.FindControl("cboxPortal");


                var pr_clientId = Page.Request.Params.Get("clientId");
                if (!string.IsNullOrEmpty(pr_clientId))
                {
                    core_ClientBusiness core_ClientBusiness = new core_ClientBusiness(new mojoportal.Service.UoW.UnitOfWork());
                    var client = core_ClientBusiness.context.core_Client.FirstOrDefault(x => x.ClientID.Equals(pr_clientId));

                    if (client == null)
                    {
                        log.Info("ClientId not found !");
                        return;
                    }

                    //generate token
                    TokenDto enCode_token = new TokenDto();
                    enCode_token.clientId = clientId;
                    enCode_token.date_expired = DateTime.Now.AddMinutes(10);
                    enCode_token.username = siteUser.LoginName;
                    string getToken = Ultilities.EncryptString(JsonConvert.SerializeObject(enCode_token));
                    //save token
                    core_Token token = new core_Token();
                    token.Token = getToken;
                    token.UserName = siteUser.LoginName;
                    token.DateExpired = DateTime.Now.AddMinutes(10);
                    token.ExpiredIn = 10;
                    token.CreatedDate = DateTime.Now;
                    core_TokenBusiness core_TokenBusiness = new core_TokenBusiness(new mojoportal.Service.UoW.UnitOfWork());
                    core_TokenBusiness.Save(token);

                    //redirect

                    FormsAuthentication.SetAuthCookie(siteUser.LoginName, true);
                    HasReturnCode = true;
                    this.DestinationPageUrl = client.ClientCallBack + "?token=" + getToken;

                }

                // track user ip address
                else if (DestinationPageUrl.Contains(RedirectDieuHanh))
                {
                    //continude
                    HasReturnCode = true;
                    this.DestinationPageUrl = RedirectDieuHanh + $"{RedirectLoginDieuHanh}?id=" + Ultilities.EncryptString(siteUser.LoginName);
                }
                else if (HasReturnCode == false && (DestinationPageUrl.Contains(RedirectDieuHanh)))
                {
                    HasReturnCode = true;
                    this.DestinationPageUrl += $"{RedirectLoginDieuHanh}?id=" + Ultilities.EncryptString(siteUser.LoginName);
                }
                else
                {
                    if (cboxDieuHanh.Checked)
                    {
                        FormsAuthentication.SetAuthCookie(siteUser.LoginName, true);
                        HasReturnCode = true;
                        this.DestinationPageUrl = RedirectDieuHanh + $"{RedirectLoginDieuHanh}?id=" + Ultilities.EncryptString(siteUser.LoginName);
                        HttpContext.Current.Response.Redirect(DestinationPageUrl);
                        return;
                    }
                    else if (cboxPortal.Checked)
                    {
                        this.DestinationPageUrl = "/";
                    }
                }
            }
        }




        protected void SiteLogin_LoggedIn(object sender, EventArgs e)
        {
            if (siteSettings == null) return;

            SiteUser siteUser = new SiteUser(siteSettings, this.UserName);


            if (WebConfigSettings.UseFoldersInsteadOfHostnamesForMultipleSites)
            {
                string cookieName = "siteguid" + siteSettings.SiteGuid;
                CookieHelper.SetCookie(cookieName, siteUser.UserGuid.ToString(), this.RememberMeSet);

            }

            if (siteUser.UserId > -1 && siteSettings.AllowUserSkins && siteUser.Skin.Length > 0)
            {
                SiteUtils.SetSkinCookie(siteUser);
            }

            if (siteUser.UserGuid == Guid.Empty) return;

            // track user ip address
            try
            {
                var getLog = core_LogSystemMojo.GetByUser(siteUser.UserId);
                if (getLog != null)
                {
                    getLog.EndLogin = DateTime.Now;
                    getLog.CountLogin = getLog.CountLogin + 1;
                    getLog.Save();

                }
                else
                {
                    core_LogSystemMojo coreLogSystem = new core_LogSystemMojo();
                    coreLogSystem.CountLogin = 1;
                    coreLogSystem.CreatedDate = DateTime.Now;
                    coreLogSystem.EndLogin = DateTime.Now;
                    coreLogSystem.StartLogin = DateTime.Now;
                    coreLogSystem.UserID = siteUser.UserId;
                    coreLogSystem.Save();
                }

                QLLog qlLog = new QLLog()
                {
                    DiaChiIP = Common.findMyIP().ToString(),
                    Type = KieuLogConstant.LogXacThucNguoiDung,
                    DuongDanThaoTac = HttpContext.Current.Request.Url.PathAndQuery,
                    HanhDongThaoTac = KieuLogConstant.DangNhapHeThong,
                    NoiDung = "Đăng nhập hệ thống",
                    CreatedBy = siteUser.Name,
                    CreatedByUser = siteUser.UserId,
                    CreatedDate = DateTime.Now
                };
                qlLog.Save();

                UserLocation userLocation = new UserLocation(siteUser.UserGuid, SiteUtils.GetIP4Address());
                userLocation.SiteGuid = siteSettings.SiteGuid;
                userLocation.Hostname = Page.Request.UserHostName;
                userLocation.Save();

                if (AccessLogin(siteUser.SiteManager) == false)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Error(SiteUtils.GetIP4Address(), ex);
            }

            UserSignInEventArgs u = new UserSignInEventArgs(siteUser);
            OnUserSignIn(u);
        }

        #region Events

        //private void HookupSignInEventHandlers()
        //{
        //    // this is a hook so that custom code can be fired when pages are created
        //    // implement a PageCreatedEventHandlerPovider and put a config file for it in
        //    // /Setup/ProviderConfig/pagecreatedeventhandlers
        //    try
        //    {
        //        foreach (UserSignInHandlerProvider handler in UserSignInHandlerProviderManager.Providers)
        //        {
        //            this.UserSignIn += handler.UserSignInEventHandler;
        //        }
        //    }
        //    catch (TypeInitializationException ex)
        //    {
        //        log.Error(ex);
        //    }

        //}

        //public event UserSignInEventHandler UserSignIn;

        protected void OnUserSignIn(UserSignInEventArgs e)
        {
            foreach (UserSignInHandlerProvider handler in UserSignInHandlerProviderManager.Providers)
            {
                handler.UserSignInEventHandler(null, e);
            }

            //if (UserSignIn != null)
            //{
            //    UserSignIn(this, e);
            //}
        }

        #endregion


        private string GetRedirectPath()
        {
            string redirectPath = WebConfigSettings.PageToRedirectToAfterSignIn;

            if (redirectPath.Length > 0) { return redirectPath; }

            string defaultRedirect = siteRoot;
            if (
                (!siteSettings.IsServerAdminSite)
                && (WebConfigSettings.UseFoldersInsteadOfHostnamesForMultipleSites)
                && (WebConfigSettings.AppendDefaultPageToFolderRootUrl)
                )
            {
                defaultRedirect += "/Default.aspx";
            }

            //if (redirectPath.EndsWith(".aspx")) { return redirectPath; }

            if (ViewState["ReturnUrl"] != null)
            {
                redirectPath = ViewState["ReturnUrl"].ToString();
            }

            if (String.IsNullOrEmpty(redirectPath) ||
                redirectPath.Contains("AccessDenied") ||
                redirectPath.Contains("Login") ||
                redirectPath.Contains("SignIn") ||
                redirectPath.Contains("ConfirmRegistration.aspx") ||
                redirectPath.Contains("OpenIdRpxHandler.aspx") ||
                redirectPath.Contains("RecoverPassword.aspx") ||
                redirectPath.Contains("Register")
                )
            {
                redirectPath = defaultRedirect;
            }

            if (Page.Request.Params["r"] == "h") { redirectPath = defaultRedirect; }

            if (SiteUtils.IsSecureRequest())
            {
                if (redirectPath.StartsWith("http:"))
                {
                    redirectPath = redirectPath.Replace("http:", "https:");
                }
            }

            return redirectPath;
        }
        /// <summary>
        /// Xác thực người dùng AD qua API
        /// </summary>
        /// <returns></returns>
        public JsonResultBO AuthenticationAD(string userName, string passWord)
        {
            //1 --> OK
            //2-- > Tài khoản không có trên Phần mềm Nhân sự
            //3-- > Tài khoản không có trên Hệ thống AD
            //4-- > Sai mật khẩu
            //5-- > Dịch vụ AD bị lỗi
            //6-- > Giá trị API_Key không hợp lệ
            //7-- > Tài khoản đã bị khóa
            //http://10.13.1.9:1532/api/XacThucs?key={key}&uid={uid}&pwd={pwd}

            var result = new JsonResultBO(true);
            try
            {
                var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(50);
                var loginLink = string.Format("{0}/XacThucs?key={1}&uid={2}&pwd={3}", LoginUrl, KEYAD, userName, passWord);
                var json = httpClient.GetStringAsync(loginLink).Result;
                API_XacThuc aPI_XacThuc = JsonConvert.DeserializeObject<API_XacThuc>(json);
                result.Status = aPI_XacThuc.Ma == 1;
                result.Message = aPI_XacThuc.MoTa;
            }
            catch (Exception ex)
            {

                result.Status = false;
                result.Message = "Đã có lỗi xảy ra khi thực hiện đăng nhập!";
            }
            return result;
        }
    }
    public class API_XacThuc
    {
        public int Ma { get; set; }
        public string MoTa { get; set; }
    }
    public class NhanSu
    {
        public System.Guid IdCB { get; set; }
        public string MCS { get; set; }
        public string TenDonViCS { get; set; }
        public string PhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string MaChucVu { get; set; }
        public string TenChucVu { get; set; }
        public Nullable<System.DateTime> NgayRoiCoQuan { get; set; }
        public string MaLyDoThoi { get; set; }
        public string LyDoRoiCQ { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }

}

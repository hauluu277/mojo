// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
// You must not remove this notice, or any other, from this software. 
// Author:					
// Created:				    2004-07-25
// 
// 2007-07-17   Alexander Yushchenko added site folders properties (SiteDataFolder, etc)
// Last Modified:		    2014-01-10

using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using log4net;
using mojoPortal.Data;
using System.Collections.Generic;
using System.Security.Policy;
using mojoportal.CoreHelpers;

namespace mojoPortal.Business
{
    // why we need serializable attribute is for AppFabricCache
    // http://social.msdn.microsoft.com/Forums/en-HK/velocity/thread/b2e2f291-500f-4c25-8032-ae20cb7b99c8

    /// <summary>
    /// The preferred way to obtasin a reference to SiteSettings object is using mojoPortal.Business.WebHelpers.CacheHelper.GetCurrentSiteSettings();
    /// </summary>
    [Serializable()]
    public class SiteSettings
    {
        #region Constructors

        public SiteSettings(Guid siteGuid)
        {
            GetSiteSettings(siteGuid);
        }

        public SiteSettings(string hostName)
        {
            GetSiteSettings(hostName);
        }

        public SiteSettings(int siteId)
        {
            if (siteId > 0)
            {
                GetSiteSettings(siteId);
            }

        }

        public SiteSettings()
        {

        }

        #endregion

        #region Enums

        public enum ContentEditorSkin
        {
            normal,
            office2003,
            silver

        }

        public enum FriendlyUrlPattern
        {
            PageName,
            PageNameWithDotASPX


        }



        #endregion

        #region Private Properties

        private static readonly ILog log = LogManager.GetLogger(typeof(SiteSettings));
        private string urlSiteMap = string.Empty;
        private int siteID = -1;
        private Guid siteGuid = Guid.Empty;
        private string siteName = string.Empty;
        private string skin = string.Empty;
        private string logo = string.Empty;
        private string icon = string.Empty;
        private bool allowUserSkins = false;
        private bool allowPageSkins = false;
        private bool allowHideMenuOnPages = false;
        private bool allowNewRegistration = true;
        private bool useSecureRegistration = false;
        private bool useSSLOnAllPages = false;

        private int articleCategory = -1;
        private int coreEventCategory = -1;
        private int coreLoaiVanBanQuyPham = -1;
        private int coreLinhVucVanBanQuyPham = -1;
        private int coreCoQuanBanHanhVanBanQuyPham = -1;
        private int coreDonVi = -1;
        private int coreHoiDong = -1;
        private int coreLinhVucHoiDap = -1;
        private int coreDuLieuDaPhuongTien = -1;
        private int coreRSS = -1;
        private int coreChuDe = -1;

        //these are legacy fields may be removed some day
        private string metaKeyWords = string.Empty;
        private string metaDescription = string.Empty;
        private string metaEncoding = string.Empty;
        private string metaAdditional = string.Empty;


        private bool isServerAdminSite = false;
        private bool allowUserFullNameChange = true;
        private bool useEmailForLogin = true;
        private bool reallyDeleteUsers = true;
        private string editorProviderName = "CKEditorProvider";// CKEditorProvider FCKeditorProvider TinyMCEProvider
        private ContentEditorSkin editorSkin = SiteSettings.ContentEditorSkin.normal;
        private FriendlyUrlPattern defaultFriendlyUrlPattern = SiteSettings.FriendlyUrlPattern.PageName;

        // LDAP Settings
        private bool useLdapAuth = false;
        private bool autoCreateLDAPUserOnFirstLogin = true;
        private LdapSettings ldapSettings = new LdapSettings();

        private bool enableMyPageFeature = true;

        // extended properties
        private bool extendedPropertiesLoaded = false;
        private bool extendedPropertiesAreDirty = false;

        private bool allowPasswordRetrieval = true;
        private bool allowPasswordReset = false;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private int maxInvalidPasswordAttempts = 10;
        private int passwordAttemptWindowMinutes = 5;
        private int passwordFormat = 1;
        private int minRequiredPasswordLength = 7;
        private int minRequiredNonAlphanumericCharacters = 0;
        private string passwordStrengthRegularExpression = string.Empty;
        private string defaultEmailFromAddress = string.Empty;
        private string siteRoot = string.Empty;
        //private string skinBaseUrl = string.Empty;
        private string siteFolderName = string.Empty;
        private string virtualPageRoot = string.Empty;

        private string skinBaseUrl = string.Empty;

        // SubkismetCaptchaProvider
        //"RecaptchaCaptchaProvider"; 
        //"SimpleMathCaptchaProvider";
        //SubkismetInvisibleCaptchaProvider
        private string datePickerProvider = "mojoDatePicker";
        private string captchaProvider = "SubkismetCaptchaProvider";
        private string recaptchaPrivateKey = string.Empty;
        private string recaptchaPublicKey = string.Empty;
        private string wordpressAPIKey = string.Empty;
        private string windowsLiveAppID = string.Empty;
        private string windowsLiveKey = string.Empty;
        private bool allowOpenIDAuth = false;
        private bool allowWindowsLiveAuth = false;
        private string gmapApiKey = string.Empty;


        // AddThisDotComUsername maps to apiKeyExtra1
        private string apiKeyExtra1 = string.Empty;

        //GoogleAnalyticsAccountCode
        private string apiKeyExtra2 = string.Empty;

        //OpenIdSelectorId
        private string apiKeyExtra3 = string.Empty;

        // for future use
        private string apiKeyExtra4 = string.Empty;

        // maps To PreferredHostName as of 2008-05-22
        private string apiKeyExtra5 = string.Empty;

        [NonSerialized]
        private Currency currency = null;

        private bool disableDbAuth = false;

        //private string timeZoneId = "Eastern Standard Time"; //default

        private int isView = 0;

        private int templateType = -1;
        private bool isTemplate = false;
        private int templateSite = -1;
        private string subTitle = string.Empty;

        private string fanPageIframe = string.Empty;

        private string footer = string.Empty;
        private int linhVucID = -1;
        private string noiDungDieuTra = string.Empty;
        private int nam = -1;
        private string tanSuatDieuTra = string.Empty;
        private string phamViSoLieu = string.Empty;
        private string doiTuongDieuTra = string.Empty;
        private string pathIMG = string.Empty;
        private DateTime createdDate = DateTime.Now;
        private int createdByUser = -1;

        private bool isTongDieuTra = false;

        private bool isCuocDieuTra = false;
        private int? parentID = null;
        private int? trangThaiDieuTra = null;
        private string fileDuThao = string.Empty;
        private string giaoDienHienThi = string.Empty;
        #endregion

        #region Public Properties
        public string GiaoDienHienThi { get { return giaoDienHienThi; } set { giaoDienHienThi = value; } }

        public string FileDuThao
        {
            get { return fileDuThao; }
            set { fileDuThao = value; }
        }

        public int? TrangThaiDieuTra
        {
            get { return trangThaiDieuTra; }
            set { trangThaiDieuTra = value; }
        }

        public int? ParentID
        {
            set { parentID = value; }
            get { return parentID; }
        }
        public bool IsCuocDieuTra
        {
            get { return isCuocDieuTra; }
            set { isCuocDieuTra = value; }
        }
        public bool IsTongDieuTra
        {
            get { return isTongDieuTra; }
            set { isTongDieuTra = value; }
        }

        public int CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public string PathIMG
        {
            get { return pathIMG; }
            set { pathIMG = value; }
        }
        public string DoiTuongDieuTra
        {
            get { return doiTuongDieuTra; }
            set { doiTuongDieuTra = value; }
        }

        public string PhamViSoLieu
        {
            get
            {
                return phamViSoLieu;
            }
            set { phamViSoLieu = value; }
        }
        public string TanSuatDieuTra
        {
            get { return tanSuatDieuTra; }
            set { tanSuatDieuTra = value; }
        }
        public int Nam
        {
            get { return nam; }
            set { nam = value; }
        }
        public string NoiDungDieuTra
        {
            get { return noiDungDieuTra; }
            set { noiDungDieuTra = value; }
        }
        public int LinhVucID
        {
            get { return linhVucID; }
            set { linhVucID = value; }
        }
        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }
        public string FanPageIframe
        {
            get { return fanPageIframe; }
            set { fanPageIframe = value; }
        }
        public string SubTitle
        {
            get { return subTitle; }
            set { subTitle = value; }
        }
        public int TemplateSite
        {
            get { return templateSite; }
            set { templateSite = value; }
        }
        public string UrlSiteMap
        {
            get { return urlSiteMap; }
            set { urlSiteMap = value; }
        }
        public int TemplateType
        {
            get { return templateType; }
            set { templateType = value; }
        }
        public bool IsTemplate
        {
            get { return isTemplate; }
            set { isTemplate = value; }
        }

        public int IsView
        {
            get { return isView; }
            set { isView = value; }
        }

        public int SiteId
        {
            get { return siteID; }
            set { siteID = value; }
        }

        public Guid SiteGuid
        {
            get { return siteGuid; }

        }
        public String SkinBaseUrl
        {
            get { return skinBaseUrl; }
            set { skinBaseUrl = value; }
        }
        public string SiteName
        {
            get { return siteName; }
            set { siteName = value; }
        }
        public int ArticleCategory
        {
            get { return articleCategory; }
            set { articleCategory = value; }
        }
        public int CoreEventCategory
        {
            get { return coreEventCategory; }
            set { coreEventCategory = value; }
        }
        public int CoreLoaiVanBanQuyPham
        {
            get { return coreLoaiVanBanQuyPham; }
            set { coreLoaiVanBanQuyPham = value; }
        }
        public int CoreLinhVucVanBanQuyPham
        {
            get { return coreLinhVucVanBanQuyPham; }
            set { coreLinhVucVanBanQuyPham = value; }
        }
        public int CoreCoQuanBanHanhVanBanQuyPham
        {
            get { return coreCoQuanBanHanhVanBanQuyPham; }
            set { coreCoQuanBanHanhVanBanQuyPham = value; }
        }
        public int CoreDonVi
        {
            get { return coreDonVi; }
            set { coreDonVi = value; }
        }
        public int CoreHoiDong
        {
            get { return coreHoiDong; }
            set { coreHoiDong = value; }
        }
        public int CoreLinhVucHoiDap
        {
            get { return coreLinhVucHoiDap; }
            set { coreLinhVucHoiDap = value; }
        }

        public int CoreDuLieuDaPhuongTien
        {
            get { return coreDuLieuDaPhuongTien; }
            set { coreDuLieuDaPhuongTien = value; }
        }
        public int CoreRSS
        {
            get { return coreRSS; }
            set { coreRSS = value; }
        }
        public int CoreChuDe
        {
            get { return coreChuDe; }
            set { coreChuDe = value; }
        }
        /// <summary>
        /// In case multiple host names map to your site and you want to force a particular one.
        /// For example I want to force urls with hostname mojoportal.com to www.mojoportal.com,
        /// because my SSL certificate matches www.mojoportal.com but not mojoportal.com
        /// </summary>
        public string PreferredHostName
        {
            get { return apiKeyExtra5; }
            set { apiKeyExtra5 = value; }
        }

        public String DefaultEmailFromAddress
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return defaultEmailFromAddress;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                defaultEmailFromAddress = value;
            }
        }

        public string DefaultFromEmailAlias
        {
            get
            {
                string result = GetExpandoProperty("DefaultFromEmailAlias");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("DefaultFromEmailAlias", value); }
        }

        public string Skin
        {
            get { return skin; }
            set { skin = value; }
        }

        public string MobileSkin
        {
            get
            {
                string result = GetExpandoProperty("MobileSkin");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("MobileSkin", value); }
        }

        public string EditorProviderName
        {
            get { return editorProviderName; }
            set { editorProviderName = value; }
        }

        public ContentEditorSkin EditorSkin
        {
            get { return editorSkin; }
            set { editorSkin = value; }
        }

        public string Logo
        {
            get { return logo; }
            set { logo = value; }
        }
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }



        public bool EnableMyPageFeature
        {
            get { return enableMyPageFeature; }
            set { enableMyPageFeature = value; }
        }

        public bool AllowUserSkins
        {
            get { return allowUserSkins; }
            set { allowUserSkins = value; }
        }

        public bool AllowPageSkins
        {
            get { return allowPageSkins; }
            set { allowPageSkins = value; }
        }

        public bool AllowHideMenuOnPages
        {
            get { return allowHideMenuOnPages; }
            set { allowHideMenuOnPages = value; }
        }

        public bool AllowNewRegistration
        {
            get { return allowNewRegistration; }
            set { allowNewRegistration = value; }
        }
        public bool UseSecureRegistration
        {
            get { return useSecureRegistration; }
            set { useSecureRegistration = value; }
        }

        public bool UseSslOnAllPages
        {
            get { return useSSLOnAllPages; }
            set { useSSLOnAllPages = value; }
        }


        public bool IsServerAdminSite
        {
            get { return isServerAdminSite; }
            set { isServerAdminSite = value; }
        }

        public bool UseLdapAuth
        {
            get { return useLdapAuth; }
            set { useLdapAuth = value; }
        }

        public bool AllowDbFallbackWithLdap
        {
            get
            {
                string sBool = GetExpandoProperty("AllowDbFallbackWithLdap");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("AllowDbFallbackWithLdap", value.ToString()); }
        }

        public bool AllowEmailLoginWithLdapDbFallback
        {
            get
            {
                string sBool = GetExpandoProperty("AllowEmailLoginWithLdapDbFallback");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("AllowEmailLoginWithLdapDbFallback", value.ToString()); }
        }

        public bool AutoCreateLdapUserOnFirstLogin
        {
            get { return autoCreateLDAPUserOnFirstLogin; }
            set { autoCreateLDAPUserOnFirstLogin = value; }
        }

        public LdapSettings SiteLdapSettings
        {
            get { return ldapSettings; }
            set { ldapSettings = value; }
        }

        public bool AllowUserFullNameChange
        {
            get { return allowUserFullNameChange; }
            set { allowUserFullNameChange = value; }
        }

        public bool ReallyDeleteUsers
        {
            get { return reallyDeleteUsers; }
            set { reallyDeleteUsers = value; }
        }

        public bool UseEmailForLogin
        {
            get { return useEmailForLogin; }
            set { useEmailForLogin = value; }
        }

        public FriendlyUrlPattern DefaultFriendlyUrlPattern
        {
            get { return defaultFriendlyUrlPattern; }
            set { defaultFriendlyUrlPattern = value; }
        }

        public bool DisableDbAuth
        {
            get { return disableDbAuth; }
            set { disableDbAuth = value; }
        }

        public bool AllowPasswordRetrieval
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                // 1 = hashed, can't be retrieved
                //2009-01-25 commented this out because we can generate a new random password
                // and send it
                //if (PasswordFormat == 1) { return false; }
                return allowPasswordRetrieval;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                allowPasswordRetrieval = value;
            }
        }

        public bool AllowPasswordReset
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return allowPasswordReset;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                allowPasswordReset = value;
            }
        }

        public bool RequirePasswordChangeOnResetRecover
        {
            get
            {
                string sBool = GetExpandoProperty("RequirePasswordChangeOnResetRecover");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("RequirePasswordChangeOnResetRecover", value.ToString()); }
        }

        public bool RequiresQuestionAndAnswer
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return requiresQuestionAndAnswer;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                requiresQuestionAndAnswer = value;
            }
        }

        public bool RequiresUniqueEmail
        {
            // I'm not exposing this in the UI because it really needs to
            // always be true with the current design if email is used for login
            // we could expose this in scenario is loginname for login
            // but if someone starts that way and changes it things could get inconsistent
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return requiresUniqueEmail;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                requiresUniqueEmail = value;
            }
        }

        public int MaxInvalidPasswordAttempts
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return maxInvalidPasswordAttempts;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                maxInvalidPasswordAttempts = value;
            }
        }

        public int PasswordAttemptWindowMinutes
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return passwordAttemptWindowMinutes;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                passwordAttemptWindowMinutes = value;
            }
        }

        /// <summary>
        /// Clear = 0, Hashed = 1, Encrypted = 2, corresponding to MembershipPasswordFormat Enum
        /// </summary>
        public int PasswordFormat
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return passwordFormat;
            }
            set
            {
                extendedPropertiesAreDirty = true;

                passwordFormat = value;
            }
        }

        public int MinRequiredPasswordLength
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return minRequiredPasswordLength;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                minRequiredPasswordLength = value;
            }
        }

        public int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return minRequiredNonAlphanumericCharacters;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                minRequiredNonAlphanumericCharacters = value;
            }
        }

        public String PasswordStrengthRegularExpression
        {
            get
            {
                if (!this.extendedPropertiesLoaded)
                {
                    LoadExtendedProperties();
                }
                return passwordStrengthRegularExpression;
            }
            set
            {
                extendedPropertiesAreDirty = true;
                passwordStrengthRegularExpression = value;
            }
        }

        public bool AllowPersistentLogin
        {
            get
            {

                string s = GetExpandoProperty("AllowPersistentLogin");

                if ((s != null) && (s.Length > 0))
                {
                    return Convert.ToBoolean(s);
                }

                return true;
            }
            set { SetExpandoProperty("AllowPersistentLogin", value.ToString()); }
        }

        public bool RequireCaptchaOnRegistration
        {
            get
            {
                string sBool = GetExpandoProperty("RequireCaptchaOnRegistration");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("RequireCaptchaOnRegistration", value.ToString()); }
        }

        public bool RequireCaptchaOnLogin
        {
            get
            {
                string sBool = GetExpandoProperty("RequireCaptchaOnLogin");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("RequireCaptchaOnLogin", value.ToString()); }
        }

        public bool RequireEnterEmailTwiceOnRegistration
        {
            get
            {
                string sBool = GetExpandoProperty("RequireEnterEmailTwiceOnRegistration");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("RequireEnterEmailTwiceOnRegistration", value.ToString()); }
        }

        public bool ShowPasswordStrengthOnRegistration
        {
            get
            {
                string sBool = GetExpandoProperty("ShowPasswordStrengthOnRegistration");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("ShowPasswordStrengthOnRegistration", value.ToString()); }
        }

        [Obsolete("This method is obsolete, use SiteUtils.GetNavigationSiteRoot() instead")]
        public string SiteRoot
        {
            get
            {
                if (siteFolderName.Length > 0)
                {
                    return siteRoot.EndsWith("/")
                               ? siteRoot + siteFolderName
                               : siteRoot + "/" + siteFolderName;
                }
                return siteRoot;
            }
            set { siteRoot = value; }
        }

        //public String SkinBaseUrl
        //{
        //    get { return skinBaseUrl; }
        //    set { skinBaseUrl = value; }
        //}

        public string SiteFolderName
        {
            get { return siteFolderName; }
            set { siteFolderName = value; }
        }


        public string DataFolder
        {
            get
            {
                return GetDataFolder(this.SiteId);
            }
        }

        public string DatePickerProvider
        {
            get { return datePickerProvider; }
            set { datePickerProvider = value; }
        }

        public string CaptchaProvider
        {
            get { return captchaProvider; }
            set { captchaProvider = value; }
        }

        public string RecaptchaPrivateKey
        {
            get { return recaptchaPrivateKey; }
            set { recaptchaPrivateKey = value; }
        }

        public string RecaptchaPublicKey
        {
            get { return recaptchaPublicKey; }
            set { recaptchaPublicKey = value; }
        }

        public string WordpressApiKey
        {
            get { return wordpressAPIKey; }
            set { wordpressAPIKey = value; }
        }

        public string WindowsLiveAppId
        {
            get { return windowsLiveAppID; }
            set { windowsLiveAppID = value; }
        }

        public string WindowsLiveKey
        {
            get { return windowsLiveKey; }
            set { windowsLiveKey = value; }
        }

        public bool AllowOpenIdAuth
        {
            get { return allowOpenIDAuth; }
            set { allowOpenIDAuth = value; }
        }

        public bool AllowWindowsLiveAuth
        {
            get { return allowWindowsLiveAuth; }
            set { allowWindowsLiveAuth = value; }
        }

        public string GmapApiKey
        {
            get { return gmapApiKey; }
            set { gmapApiKey = value; }
        }

        public string AddThisDotComUsername
        {
            get { return apiKeyExtra1; }
            set { apiKeyExtra1 = value; }
        }

        public string GoogleAnalyticsAccountCode
        {
            get { return apiKeyExtra2; }
            set { apiKeyExtra2 = value; }
        }

        /// <summary>
        /// https://www.idselector.com/
        /// </summary>
        public string OpenIdSelectorId
        {
            get { return apiKeyExtra3; }
            set { apiKeyExtra3 = value; }
        }


        public string TimeZoneId
        {
            get
            {
                string result = GetExpandoProperty("TimeZoneId");
                if (result != null) { return result; }
                return "Eastern Standard Time";
            }
            set { SetExpandoProperty("TimeZoneId", value); }
        }


        public string MyPageSkin
        {
            get
            {
                string result = GetExpandoProperty("MyPageSkin");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("MyPageSkin", value); }
        }

        public string SiteMapSkin
        {
            get
            {
                string result = GetExpandoProperty("SiteMapSkin");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("SiteMapSkin", value); }
        }

        public string AppLogoForWindowsLive
        {
            get
            {
                string result = GetExpandoProperty("AppLogoForWindowsLive");
                if (result != null) { return result; }
                return "/Data/logos/mojomoonprint.jpg";
            }
            set { SetExpandoProperty("AppLogoForWindowsLive", value); }
        }

        public bool AllowWindowsLiveMessengerForMembers
        {
            get
            {
                string sBool = GetExpandoProperty("AllowWindowsLiveMessengerForMembers");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("AllowWindowsLiveMessengerForMembers", value.ToString()); }
        }

        public string RpxNowApiKey
        {
            get
            {
                string result = GetExpandoProperty("RpxNowApiKey");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RpxNowApiKey", value); }
        }

        public string RpxNowApplicationName
        {
            get
            {
                string result = GetExpandoProperty("RpxNowApplicationName");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RpxNowApplicationName", value); }
        }

        public string RpxNowAdminUrl
        {
            get
            {
                string result = GetExpandoProperty("RpxNowAdminUrl");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RpxNowAdminUrl", value); }
        }

        public string WebSnaprKey
        {
            get
            {
                string result = GetExpandoProperty("WebSnaprKey");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("WebSnaprKey", value); }
        }

        public string OpenSearchName
        {
            get
            {
                string result = GetExpandoProperty("OpenSearchName");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("OpenSearchName", value); }
        }


        public string BingAPIId
        {
            get
            {
                string result = GetExpandoProperty("BingAPIId");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("BingAPIId", value); }
        }

        public string GoogleCustomSearchId
        {
            get
            {
                string result = GetExpandoProperty("GoogleCustomSearchId");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("GoogleCustomSearchId", value); }
        }

        public string PrimarySearchEngine
        {
            get
            {
                string result = GetExpandoProperty("PrimarySearchEngine");
                if (result != null) { return result; }
                return "internal";
            }
            set { SetExpandoProperty("PrimarySearchEngine", value); }
        }

        public bool ShowAlternateSearchIfConfigured
        {
            get
            {
                string sBool = GetExpandoProperty("ShowAlternateSearchIfConfigured");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("ShowAlternateSearchIfConfigured", value.ToString()); }
        }

        public string Slogan
        {
            get
            {
                string result = GetExpandoProperty("Slogan");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("Slogan", value); }
        }

        public string CompanyName
        {
            get
            {
                string result = GetExpandoProperty("CompanyName");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyName", value); }
        }

        public string CompanyStreetAddress
        {
            get
            {
                string result = GetExpandoProperty("CompanyStreetAddress");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyStreetAddress", value); }
        }

        public string CompanyStreetAddress2
        {
            get
            {
                string result = GetExpandoProperty("CompanyStreetAddress2");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyStreetAddress2", value); }
        }

        public string CompanyLocality
        {
            get
            {
                string result = GetExpandoProperty("CompanyLocality");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyLocality", value); }
        }

        public string CompanyRegion
        {
            get
            {
                string result = GetExpandoProperty("CompanyRegion");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyRegion", value); }
        }

        public string CompanyPostalCode
        {
            get
            {
                string result = GetExpandoProperty("CompanyPostalCode");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyPostalCode", value); }
        }

        public string CompanyCountry
        {
            get
            {
                string result = GetExpandoProperty("CompanyCountry");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyCountry", value); }
        }

        public string CompanyPhone
        {
            get
            {
                string result = GetExpandoProperty("CompanyPhone");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyPhone", value); }
        }

        public string CompanyFax
        {
            get
            {
                string result = GetExpandoProperty("CompanyFax");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyFax", value); }
        }

        public string CompanyPublicEmail
        {
            get
            {
                string result = GetExpandoProperty("CompanyPublicEmail");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CompanyPublicEmail", value); }
        }



        public bool EnableWoopra
        {
            get
            {
                string sBool = GetExpandoProperty("EnableWoopra");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("EnableWoopra", value.ToString()); }
        }

        public string PrivacyPolicyUrl
        {
            get
            {
                string result = GetExpandoProperty("PrivacyPolicyUrl");
                if (result != null) { return result; }
                return "/privacy.aspx";
            }
            set { SetExpandoProperty("PrivacyPolicyUrl", value); }
        }

        public string SMTPUser
        {
            get { return GetExpandoProperty("SMTPUser"); }
            set { SetExpandoProperty("SMTPUser", value); }
        }

        public string SMTPPassword
        {
            get { return GetExpandoProperty("SMTPPassword"); }
            set { SetExpandoProperty("SMTPPassword", value); }
        }

        public int SMTPPort
        {
            get
            {
                string sPort = GetExpandoProperty("SMTPPort");
                if ((sPort != null) && (sPort.Length > 0))
                {
                    return Convert.ToInt32(sPort, CultureInfo.InvariantCulture);
                }
                return 25;
            }
            set
            {
                SetExpandoProperty("SMTPPort", value.ToString(CultureInfo.InvariantCulture));
            }
        }

        public string SMTPPreferredEncoding
        {
            get { return GetExpandoProperty("SMTPPreferredEncoding"); }
            set { SetExpandoProperty("SMTPPreferredEncoding", value); }
        }

        public string SMTPServer
        {
            get { return GetExpandoProperty("SMTPServer"); }
            set { SetExpandoProperty("SMTPServer", value); }
        }



        public bool SMTPRequiresAuthentication
        {
            get
            {

                string sUseSsl = GetExpandoProperty("SMTPRequiresAuthentication");

                if ((sUseSsl != null) && (sUseSsl.Length > 0))
                {
                    return Convert.ToBoolean(sUseSsl);
                }

                return false;
            }
            set { SetExpandoProperty("SMTPRequiresAuthentication", value.ToString()); }
        }


        public bool SMTPUseSsl
        {
            get
            {

                string sUseSsl = GetExpandoProperty("SMTPUseSsl");

                if ((sUseSsl != null) && (sUseSsl.Length > 0))
                {
                    return Convert.ToBoolean(sUseSsl);
                }

                return false;
            }
            set { SetExpandoProperty("SMTPUseSsl", value.ToString()); }
        }

        public bool AllowUserEditorPreference
        {
            get
            {

                string s = GetExpandoProperty("AllowUserEditorPreference");

                if ((s != null) && (s.Length > 0))
                {
                    return Convert.ToBoolean(s);
                }

                return false;
            }
            set { SetExpandoProperty("AllowUserEditorPreference", value.ToString()); }
        }

        public string SiteRootEditRoles
        {
            get
            {
                string result = GetExpandoProperty("SiteRootEditRoles");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("SiteRootEditRoles", value); }
        }

        public string SiteRootDraftEditRoles
        {
            get
            {
                string result = GetExpandoProperty("SiteRootDraftEditRoles");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("SiteRootDraftEditRoles", value); }
        }

        // added 2013-04-24 for 3 level workflow
        public string SiteRootDraftApprovalRoles
        {
            get
            {
                string result = GetExpandoProperty("SiteRootDraftApprovalRoles");
                if (result != null) { return result; }
                return String.Empty;
            }
            set { SetExpandoProperty("SiteRootDraftApprovalRoles", value); }
        }

        public string CommerceReportViewRoles
        {
            get
            {
                string result = GetExpandoProperty("CommerceReportViewRoles");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("CommerceReportViewRoles", value); }
        }

        public string RolesThatCanCreateRootPages
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanCreateRootPages");
                if (result != null) { return result; }
                return "Content Administrators;Content Publishers;";
            }
            set { SetExpandoProperty("RolesThatCanCreateRootPages", value); }
        }

        public string RolesThatCanViewMemberList
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanViewMemberList");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanViewMemberList", value); }
        }

        public string RolesThatCanCreateUsers
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanManageUsers");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanManageUsers", value); }
        }

        public string RolesThatCanManageUsers
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanFullyManageUsers");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanFullyManageUsers", value); }
        }

        public string RolesThatCanViewMyPage
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanViewMyPage");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanViewMyPage", value); }
        }

        public string RolesThatCanLookupUsers
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanLookupUsers");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanLookupUsers", value); }
        }

        public string RolesNotAllowedToEditModuleSettings
        {
            get
            {
                string result = GetExpandoProperty("RolesNotAllowedToEditModuleSettings");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesNotAllowedToEditModuleSettings", value); }
        }

        public string RolesThatCanEditContentTemplates
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanEditContentTemplates");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanEditContentTemplates", value); }
        }


        public string GeneralBrowseAndUploadRoles
        {
            get
            {
                string result = GetExpandoProperty("GeneralBrowseAndUploadRoles");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("GeneralBrowseAndUploadRoles", value); }
        }

        public string UserFilesBrowseAndUploadRoles
        {
            get
            {
                string result = GetExpandoProperty("UserFilesBrowseAndUploadRoles");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("UserFilesBrowseAndUploadRoles", value); }
        }

        public string RolesThatCanDeleteFilesInEditor
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanDeleteFilesInEditor");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanDeleteFilesInEditor", value); }
        }

        public string RolesThatCanAssignSkinsToPages
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanAssignSkinsToPages");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanAssignSkinsToPages", value); }
        }

        public string RolesThatCanManageSkins
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanManageSkins");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanManageSkins", value); }
        }

        public string DefaultRootPageViewRoles
        {
            get
            {
                string result = GetExpandoProperty("DefaultRootPageViewRoles");
                if (result != null) { return result; }
                return "All Users;";
            }
            set { SetExpandoProperty("DefaultRootPageViewRoles", value); }
        }

        public string DefaultRootPageEditRoles
        {
            get
            {
                string result = GetExpandoProperty("DefaultRootPageEditRoles");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("DefaultRootPageEditRoles", value); }
        }

        public string DefaultRootPageCreateChildPageRoles
        {
            get
            {
                string result = GetExpandoProperty("DefaultRootPageCreateChildPageRoles");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("DefaultRootPageCreateChildPageRoles", value); }
        }

        public Guid SkinVersion
        {
            get
            {
                string result = GetExpandoProperty("SkinVersion");
                if ((result != null) && (result.Length == 36)) { return new Guid(result); }
                return Guid.Empty;
            }
            set { SetExpandoProperty("SkinVersion", value.ToString()); }
        }

        //public string TermsOfUse
        //{
        //    get
        //    {
        //        string result = GetExpandoProperty("TermsOfUse");
        //        if (result != null) { return result; }
        //        return string.Empty;
        //    }
        //    set { SetExpandoProperty("TermsOfUse", value); }
        //}

        public string AvatarSystem
        {
            get
            {
                string result = GetExpandoProperty("AvatarSystem");
                if (result != null) { return result; }
                return "gravatar";
            }
            set { SetExpandoProperty("AvatarSystem", value); }
        }

        public string CommentProvider
        {
            get
            {
                string result = GetExpandoProperty("CommentProvider");
                if (result != null) { return result; }
                return "intensedebate";
            }
            set { SetExpandoProperty("CommentProvider", value); }
        }

        public string FacebookAppId
        {
            get
            {
                string result = GetExpandoProperty("FacebookAppId");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("FacebookAppId", value); }
        }

        public string IntenseDebateAccountId
        {
            get
            {
                string result = GetExpandoProperty("IntenseDebateAccountId");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("IntenseDebateAccountId", value); }
        }

        public string DisqusSiteShortName
        {
            get
            {
                string result = GetExpandoProperty("DisqusSiteShortName");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("DisqusSiteShortName", value); }
        }

        /// <summary>
        /// if you are using vocabularies such as Dublin Core in your page meta data, you can specify the profile which will be added to the head element
        /// http://dublincore.org/documents/dcq-html/
        /// ie for Dublin Core you would put http://dublincore.org/documents/dcq-html/ 
        /// if using multiple vocabularies you can separe the urls by white space
        /// </summary>
        public string MetaProfile
        {
            get
            {
                string result = GetExpandoProperty("MetaProfile");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("MetaProfile", value); }
        }

        public string NewsletterEditor
        {
            get
            {
                string result = GetExpandoProperty("NewsletterEditor");
                if (result != null) { return result; }
                return "TinyMCEProvider";
            }
            set { SetExpandoProperty("NewsletterEditor", value); }
        }


        public Guid DefaultCountryGuid
        {
            get
            {
                string result = GetExpandoProperty("DefaultCountryGuid");
                if ((result != null) && (result.Length == 36)) { return new Guid(result); }
                return new Guid("a71d6727-61e7-4282-9fcb-526d1e7bc24f"); //US
            }
            set { SetExpandoProperty("DefaultCountryGuid", value.ToString()); }
        }

        public Guid DefaultStateGuid
        {
            get
            {
                string result = GetExpandoProperty("DefaultStateGuid");
                if ((result != null) && (result.Length == 36)) { return new Guid(result); }
                return Guid.Empty;
            }
            set { SetExpandoProperty("DefaultStateGuid", value.ToString()); }
        }

        public Guid CurrencyGuid
        {
            get
            {
                string result = GetExpandoProperty("CurrencyGuid");
                if ((result != null) && (result.Length == 36)) { return new Guid(result); }
                return new Guid("ff2dde1b-e7d7-4c3a-9ab4-6474345e0f31"); //USD
            }
            set { SetExpandoProperty("CurrencyGuid", value.ToString()); }
        }


        public Currency GetCurrency()
        {
            if (currency == null) { currency = new Currency(CurrencyGuid); }
            return currency;
        }

        public bool ForceContentVersioning
        {
            get
            {
                string b = GetExpandoProperty("ForceContentVersioning");
                if ((b != null) && (b.Length > 0))
                {
                    return Convert.ToBoolean(b);
                }
                return false;
            }
            set { SetExpandoProperty("ForceContentVersioning", value.ToString()); }
        }

        public bool EnableContentWorkflow
        {
            get
            {

                string b = GetExpandoProperty("EnableContentWorkflow");

                if ((b != null) && (b.Length > 0))
                {
                    return Convert.ToBoolean(b);
                }

                return false;
            }
            set { SetExpandoProperty("EnableContentWorkflow", value.ToString()); }
        }


        public string GoogleAnalyticsEmail
        {
            get
            {
                string result = GetExpandoProperty("GoogleAnalyticsEmail");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("GoogleAnalyticsEmail", value); }
        }

        public string GoogleAnalyticsPassword
        {
            get
            {
                string result = GetExpandoProperty("GoogleAnalyticsPassword");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("GoogleAnalyticsPassword", value); }
        }

        public string GoogleAnalyticsProfileId
        {
            get
            {
                string result = GetExpandoProperty("GoogleAnalyticsProfileId");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("GoogleAnalyticsProfileId", value); }
        }

        public string GoogleAnalyticsSettings
        {
            get
            {
                string result = GetExpandoProperty("GoogleAnalyticsSettings");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("GoogleAnalyticsSettings", value); }
        }

        public string RolesThatCanViewGoogleAnalytics
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanViewGoogleAnalytics");
                if (result != null) { return result; }
                return "Admins;Content Administrators;";
            }
            set { SetExpandoProperty("RolesThatCanViewGoogleAnalytics", value); }
        }

        public string RolesThatCanEditGoogleAnalyticsQueries
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanEditGoogleAnalyticsQueries");
                if (result != null) { return result; }
                return "Admins;Content Administrators;";
            }
            set { SetExpandoProperty("RolesThatCanEditGoogleAnalyticsQueries", value); }
        }

        /// <summary>
        /// when a new user registers, if this is true then they cannot login until approved
        /// </summary>
        public bool RequireApprovalBeforeLogin
        {
            get
            {
                string b = GetExpandoProperty("RequireApprovalBeforeLogin");
                if (!string.IsNullOrEmpty(b))
                {
                    return Convert.ToBoolean(b);
                }
                return false;
            }
            set { SetExpandoProperty("RequireApprovalBeforeLogin", value.ToString()); }
        }

        // TODO: 

        //public bool AllowPersistentLogin
        //{
        //    get
        //    {
        //        string sBool = GetExpandoProperty("AllowPersistentLogin");

        //        if ((sBool != null) && (sBool.Length > 0))
        //        {
        //            return Convert.ToBoolean(sBool);
        //        }

        //        return false;
        //    }
        //    set { SetExpandoProperty("AllowPersistentLogin", value.ToString()); }
        //}


        public string EmailAdressesForUserApprovalNotification
        {
            get
            {
                string result = GetExpandoProperty("EmailAdressesForUserApprovalNotification");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("EmailAdressesForUserApprovalNotification", value); }
        }



        public string RolesThatCanApproveNewUsers
        {
            get
            {
                string result = GetExpandoProperty("RolesThatCanApproveNewUsers");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RolesThatCanApproveNewUsers", value); }
        }

        public string PasswordRegexWarning
        {
            get
            {
                string result = GetExpandoProperty("PasswordRegexWarning");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("PasswordRegexWarning", value); }
        }



        public string RegistrationAgreement
        {
            get
            {
                string result = GetExpandoProperty("RegistrationAgreement");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RegistrationAgreement", value); }
        }

        public string RegistrationPreamble
        {
            get
            {
                string result = GetExpandoProperty("RegistrationPreamble");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("RegistrationPreamble", value); }
        }

        public string LoginInfoTop
        {
            get
            {
                string result = GetExpandoProperty("LoginInfoTop");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("LoginInfoTop", value); }
        }

        public string LoginInfoBottom
        {
            get
            {
                string result = GetExpandoProperty("LoginInfoBottom");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("LoginInfoBottom", value); }
        }

        public bool SiteIsClosed
        {
            get
            {
                string sBool = GetExpandoProperty("SiteIsClosed");

                if ((sBool != null) && (sBool.Length > 0))
                {
                    return Convert.ToBoolean(sBool);
                }

                return false;
            }
            set { SetExpandoProperty("SiteIsClosed", value.ToString()); }
        }

        public string SiteIsClosedMessage
        {
            get
            {
                string result = GetExpandoProperty("SiteIsClosedMessage");
                if (result != null) { return result; }
                return string.Empty;
            }
            set { SetExpandoProperty("SiteIsClosedMessage", value); }
        }

        #endregion

        #region Private Methods

        private void GetSiteSettings(Guid siteGuid)
        {
            using (IDataReader result = DBSiteSettings.GetSite(siteGuid))
            {
                LoadSiteSettings(result);
            }
        }

        private void GetSiteSettings(string hostName)
        {
            using (IDataReader result = DBSiteSettings.GetSite(hostName))
            {
                LoadSiteSettings(result);
            }
        }

        private void GetSiteSettings(int siteId)
        {
            using (IDataReader result = DBSiteSettings.GetSite(siteId))
            {
                LoadSiteSettings(result);
            }
        }


        private void LoadSiteSettings(IDataReader reader)
        {
            if (reader == null) return;

            if (reader.Read())
            {
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.siteGuid = new Guid(reader["SiteGuid"].ToString());
                this.siteName = reader["SiteName"].ToString();
                this.skin = reader["Skin"].ToString();
                this.logo = reader["Logo"].ToString();
                this.icon = reader["Icon"].ToString();
                string editorProvider = reader["EditorProvider"].ToString().Trim();
                if (editorProvider.Length > 0)
                {
                    this.editorProviderName = editorProvider;
                }

                if (reader["EditorSkin"] != DBNull.Value)
                {
                    try
                    {

                        this.editorSkin =
                            (ContentEditorSkin)Enum.Parse(typeof(ContentEditorSkin),
                                                          reader["EditorSkin"].ToString());

                    }
                    catch (ArgumentException)
                    { }
                }


                this.enableMyPageFeature = Convert.ToBoolean(reader["EnableMyPageFeature"]);
                this.allowUserSkins = Convert.ToBoolean(reader["AllowUserSkins"]);
                this.allowPageSkins = Convert.ToBoolean(reader["AllowPageSkins"]);
                this.allowHideMenuOnPages = Convert.ToBoolean(reader["AllowHideMenuOnPages"]);
                this.allowNewRegistration = Convert.ToBoolean(reader["AllowNewRegistration"]);
                this.useSecureRegistration = Convert.ToBoolean(reader["UseSecureRegistration"]);
                this.useEmailForLogin = Convert.ToBoolean(reader["UseEmailForLogin"]);
                this.reallyDeleteUsers = Convert.ToBoolean(reader["ReallyDeleteUsers"]);
                this.useSSLOnAllPages = Convert.ToBoolean(reader["UseSSLOnAllPages"]);
                this.isServerAdminSite = Convert.ToBoolean(reader["IsServerAdminSite"]);


                //this.metaKeyWords = reader["DefaultPageKeyWords"].ToString();
                //this.metaDescription = reader["DefaultPageDescription"].ToString();
                //this.metaEncoding = reader["DefaultPageEncoding"].ToString();
                //this.metaAdditional = reader["DefaultAdditionalMetaTags"].ToString();

                //string useLdap = reader["UseLdapAuth"].ToString();
                this.useLdapAuth = Convert.ToBoolean(reader["UseLdapAuth"]);

                this.autoCreateLDAPUserOnFirstLogin = Convert.ToBoolean(reader["AutoCreateLDAPUserOnFirstLogin"]);

                this.ldapSettings.Server = reader["LdapServer"].ToString();
                if (reader["LdapPort"] != DBNull.Value)
                {
                    this.ldapSettings.Port = Convert.ToInt32(reader["LdapPort"]);
                }

                if (reader["GiaoDienHienThi"] != DBNull.Value)
                {
                    this.giaoDienHienThi = reader["GiaoDienHienThi"].ToString();
                }

                this.ldapSettings.Domain = reader["LdapDomain"].ToString();
                this.ldapSettings.RootDN = reader["LdapRootDN"].ToString();
                this.ldapSettings.UserDNKey = reader["LdapUserDNKey"].ToString();

                //this.allowPasswordRetrieval = (
                //                                  (string.Equals(reader["AllowPasswordRetrieval"].ToString(),"true", StringComparison.InvariantCultureIgnoreCase)) ||
                //                                  (reader["AllowPasswordRetrieval"].ToString() == "1"));

                //this.allowPasswordReset = (
                //                              (string.Equals(reader["AllowPasswordReset"].ToString(),"true", StringComparison.InvariantCultureIgnoreCase)) ||
                //                              (reader["AllowPasswordReset"].ToString() == "1"));

                //this.requiresQuestionAndAnswer = (
                //                                     (string.Equals(reader["RequiresQuestionAndAnswer"].ToString(), "true", StringComparison.InvariantCultureIgnoreCase)) ||
                //                                     (reader["RequiresQuestionAndAnswer"].ToString() == "1"));

                //this.requiresUniqueEmail = (
                //                               (string.Equals(reader["RequiresUniqueEmail"].ToString(),"true", StringComparison.InvariantCultureIgnoreCase)) ||
                //                               (reader["RequiresUniqueEmail"].ToString() == "1"));

                this.allowPasswordRetrieval = Convert.ToBoolean(reader["AllowPasswordRetrieval"]);
                this.allowPasswordReset = Convert.ToBoolean(reader["AllowPasswordReset"]);
                this.requiresQuestionAndAnswer = Convert.ToBoolean(reader["RequiresQuestionAndAnswer"]);
                this.requiresUniqueEmail = Convert.ToBoolean(reader["RequiresUniqueEmail"]);


                this.maxInvalidPasswordAttempts = Convert.ToInt32(reader["MaxInvalidPasswordAttempts"]);
                this.passwordAttemptWindowMinutes = Convert.ToInt32(reader["PasswordAttemptWindowMinutes"]);
                this.passwordFormat = Convert.ToInt32(reader["PasswordFormat"]);

                this.minRequiredPasswordLength = Convert.ToInt32(reader["MinRequiredPasswordLength"]);
                this.minRequiredNonAlphanumericCharacters = Convert.ToInt32(reader["MinReqNonAlphaChars"]);
                this.passwordStrengthRegularExpression = reader["PwdStrengthRegex"].ToString();

                this.defaultEmailFromAddress = reader["DefaultEmailFromAddress"].ToString();


                string dp = reader["DatePickerProvider"].ToString();
                if (dp.Length > 0)
                {
                    this.datePickerProvider = dp;
                }

                string cp = reader["CaptchaProvider"].ToString();
                if (cp.Length > 0)
                {
                    this.captchaProvider = cp;
                }

                this.recaptchaPrivateKey = reader["RecaptchaPrivateKey"].ToString();
                this.recaptchaPublicKey = reader["RecaptchaPublicKey"].ToString();
                this.wordpressAPIKey = reader["WordpressAPIKey"].ToString();
                this.windowsLiveAppID = reader["WindowsLiveAppID"].ToString();
                this.windowsLiveKey = reader["WindowsLiveKey"].ToString();



                this.allowOpenIDAuth = Convert.ToBoolean(reader["AllowOpenIDAuth"]);
                this.allowWindowsLiveAuth = Convert.ToBoolean(reader["AllowWindowsLiveAuth"]);
                this.allowUserFullNameChange = Convert.ToBoolean(reader["AllowUserFullNameChange"]);


                this.gmapApiKey = reader["GmapApiKey"].ToString();
                this.apiKeyExtra1 = reader["ApiKeyExtra1"].ToString();
                this.apiKeyExtra2 = reader["ApiKeyExtra2"].ToString();
                this.apiKeyExtra3 = reader["ApiKeyExtra3"].ToString();
                this.apiKeyExtra4 = reader["ApiKeyExtra4"].ToString();
                this.apiKeyExtra5 = reader["ApiKeyExtra5"].ToString();
                if (reader["DefaultFriendlyUrlPatternEnum"] != DBNull.Value)
                {
                    this.defaultFriendlyUrlPattern = (SiteSettings.FriendlyUrlPattern)Enum.Parse(typeof(SiteSettings.FriendlyUrlPattern), reader["DefaultFriendlyUrlPatternEnum"].ToString());
                }

                this.disableDbAuth = Convert.ToBoolean(reader["DisableDbAuth"]);

                this.extendedPropertiesLoaded = true;
                if (reader["ArticleCategoryID"] != DBNull.Value)
                {
                    this.articleCategory = Convert.ToInt32(reader["ArticleCategoryID"]);
                }
                if (reader["CoreEventCategoryID"] != DBNull.Value)
                {
                    this.coreEventCategory = Convert.ToInt32(reader["CoreEventCategoryID"]);
                }
                if (reader["CoreLoaiVanBanCategoryID"] != DBNull.Value)
                {
                    this.coreLoaiVanBanQuyPham = Convert.ToInt32(reader["CoreLoaiVanBanCategoryID"]);
                }
                if (reader["CoreLinhVucVanBanCategoryID"] != DBNull.Value)
                {
                    this.coreLinhVucVanBanQuyPham = Convert.ToInt32(reader["CoreLinhVucVanBanCategoryID"]);
                }
                if (reader["CoreCoQuanBanHanhVanBanCategoryID"] != DBNull.Value)
                {
                    this.coreCoQuanBanHanhVanBanQuyPham = Convert.ToInt32(reader["CoreCoQuanBanHanhVanBanCategoryID"]);
                }

                if (reader["CoreDonViCategoryID"] != DBNull.Value)
                {
                    this.coreDonVi = Convert.ToInt32(reader["CoreDonViCategoryID"]);
                }
                if (reader["CoreLinhVucHoiDapCategoryID"] != DBNull.Value)
                {
                    this.coreLinhVucHoiDap = Convert.ToInt32(reader["CoreLinhVucHoiDapCategoryID"]);
                }
                if (reader["CoreDuLieuDaPhuongTienCategoryID"] != DBNull.Value)
                {
                    this.coreDuLieuDaPhuongTien = Convert.ToInt32(reader["CoreDuLieuDaPhuongTienCategoryID"]);
                }
                if (reader["CoreRSSCategoryID"] != DBNull.Value)
                {
                    this.coreRSS = Convert.ToInt32(reader["CoreRSSCategoryID"]);
                }
                if (reader["CoreHoiDongCategoryID"] != DBNull.Value)
                {
                    this.coreHoiDong = Convert.ToInt32(reader["CoreHoiDongCategoryID"]);
                }
                if (reader["CoreChuDeCategoryID"] != DBNull.Value)
                {
                    this.coreChuDe = Convert.ToInt32(reader["CoreChuDeCategoryID"]);
                }
                if (reader["IsView"] != DBNull.Value)
                {
                    this.isView = int.Parse(reader["IsView"].ToString());
                }
                if (reader["TemplateType"] != DBNull.Value)
                {
                    this.templateType = Convert.ToInt32(reader["TemplateType"]);
                }
                if (reader["IsTemplate"] != DBNull.Value)
                {
                    this.isTemplate = Convert.ToBoolean(reader["IsTemplate"]);
                }
                if (reader["UrlSiteMap"] != DBNull.Value)
                {
                    this.urlSiteMap = reader["UrlSiteMap"].ToString();
                }

                if (reader["SiteSubTitle"] != DBNull.Value)
                {
                    this.subTitle = reader["SiteSubTitle"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["FanPageIframe"].ToString()))
                {
                    this.fanPageIframe = reader["FanPageIframe"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["Footer"].ToString()))
                {
                    this.footer = reader["Footer"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["LinhVucID"].ToString()))
                {
                    this.linhVucID = Convert.ToInt32(reader["LinhVucID"]);
                }
                this.noiDungDieuTra = GenericData<string>.GetDataOrDefault(reader["NoiDungDieuTra"], noiDungDieuTra);
                this.nam = GenericData<int>.GetDataOrDefault(reader["Nam"], nam);
                this.tanSuatDieuTra = GenericData<string>.GetDataOrDefault(reader["TanSuatDieuTra"], tanSuatDieuTra);
                this.phamViSoLieu = GenericData<string>.GetDataOrDefault(reader["PhamViSoLieu"], phamViSoLieu);
                this.doiTuongDieuTra = GenericData<string>.GetDataOrDefault(reader["DoiTuongDieuTra"], doiTuongDieuTra);
                this.pathIMG = GenericData<string>.GetDataOrDefault(reader["PathIMG"], pathIMG);
                this.createdDate = GenericData<DateTime>.GetDataOrDefault(reader["CreatedDate"], createdDate);
                this.createdByUser = GenericData<int>.GetDataOrDefault(reader["CreatedByUser"], createdByUser);
                this.isTongDieuTra = GenericData<bool>.GetDataOrDefault(reader["IsTongDieuTra"], isTongDieuTra);
                this.isCuocDieuTra = GenericData<bool>.GetDataOrDefault(reader["IsCuocDieuTra"], isCuocDieuTra);
                this.parentID = GenericData<int?>.GetDataOrDefault(reader["ParentID"], parentID);
                this.trangThaiDieuTra = GenericData<int?>.GetDataOrDefault(reader["TrangThaiDieuTra"], trangThaiDieuTra);
                this.fileDuThao = GenericData<string>.GetDataOrDefault(reader["FileDuThao"], fileDuThao);
            }


        }


        private void LoadExtendedProperties()
        {
            if (this.siteID > 0)
            {
                this.GetSiteSettings(this.siteID);
            }

            this.extendedPropertiesLoaded = true;
        }

        private bool Create()
        {
            this.siteGuid = Guid.NewGuid();

            int newID = DBSiteSettings.Create(
                this.siteGuid,
                this.siteName,
                this.skin,
                this.logo,
                this.icon,
                this.allowNewRegistration,
                this.allowUserSkins,
                this.allowPageSkins,
                this.allowHideMenuOnPages,
                this.useSecureRegistration,
                this.useSSLOnAllPages,
                this.metaKeyWords,
                this.metaDescription,
                this.metaEncoding,
                this.metaAdditional,
                this.isServerAdminSite,
                this.useLdapAuth,
                this.autoCreateLDAPUserOnFirstLogin,
                this.ldapSettings.Server,
                this.ldapSettings.Port,
                this.ldapSettings.Domain,
                this.ldapSettings.RootDN,
                this.ldapSettings.UserDNKey,
                this.allowUserFullNameChange,
                this.useEmailForLogin,
                this.reallyDeleteUsers,
                this.editorSkin.ToString(),
                this.defaultFriendlyUrlPattern.ToString(),
                this.enableMyPageFeature,
                this.editorProviderName,
                this.datePickerProvider,
                this.captchaProvider,
                this.recaptchaPrivateKey,
                this.recaptchaPublicKey,
                this.wordpressAPIKey,
                this.windowsLiveAppID,
                this.windowsLiveKey,
                this.allowOpenIDAuth,
                this.allowWindowsLiveAuth,
                this.gmapApiKey,
                this.apiKeyExtra1,
                this.apiKeyExtra2,
                this.apiKeyExtra3,
                this.apiKeyExtra4,
                this.apiKeyExtra5,
                this.disableDbAuth,
                this.articleCategory,
                this.coreEventCategory,
                this.coreLoaiVanBanQuyPham,
                this.coreLinhVucVanBanQuyPham,
                this.coreCoQuanBanHanhVanBanQuyPham,
                this.coreDonVi,
                this.coreLinhVucHoiDap,
                this.coreDuLieuDaPhuongTien,
                this.coreRSS,
                this.coreHoiDong,
                this.coreChuDe,
                this.templateType,
                this.isTemplate,
                this.urlSiteMap,
                this.templateSite,
                this.subTitle,
                this.linhVucID,
                this.noiDungDieuTra,
                this.nam,
                this.TanSuatDieuTra,
                this.phamViSoLieu,
                this.doiTuongDieuTra,
                this.pathIMG,
                this.createdDate,
                this.createdByUser,
                this.isTongDieuTra,
                this.isCuocDieuTra,
                this.parentID,
                this.trangThaiDieuTra,
                this.fileDuThao,
                this.giaoDienHienThi
                );

            bool result = (newID > 0);

            if (result)
            {
                this.siteID = newID;

                if (this.extendedPropertiesAreDirty)
                {
                    bool updatedExtended = UpdateExtendedProperties();
                }

                EnsureExpandoSettings();
                SaveExpandoProperties();

                SiteCreatedEventArgs e = new SiteCreatedEventArgs(this);
                OnSiteCreated(e);

            }

            return result;
        }


        private bool Update()
        {
            bool success = DBSiteSettings.Update(
                this.siteID,
                this.siteName,
                this.skin,
                this.logo,
                this.icon,
                this.allowNewRegistration,
                this.allowUserSkins,
                this.allowPageSkins,
                this.allowHideMenuOnPages,
                this.useSecureRegistration,
                this.useSSLOnAllPages,
                this.metaKeyWords,
                this.metaDescription,
                this.metaEncoding,
                this.metaAdditional,
                this.isServerAdminSite,
                this.useLdapAuth,
                this.autoCreateLDAPUserOnFirstLogin,
                this.ldapSettings.Server,
                this.ldapSettings.Port,
                this.ldapSettings.Domain,
                this.ldapSettings.RootDN,
                this.ldapSettings.UserDNKey,
                this.allowUserFullNameChange,
                this.useEmailForLogin,
                this.reallyDeleteUsers,
                this.editorSkin.ToString(),
                this.defaultFriendlyUrlPattern.ToString(),
                this.enableMyPageFeature,
                this.editorProviderName,
                this.datePickerProvider,
                this.captchaProvider,
                this.recaptchaPrivateKey,
                this.recaptchaPublicKey,
                this.wordpressAPIKey,
                this.windowsLiveAppID,
                this.windowsLiveKey,
                this.allowOpenIDAuth,
                this.allowWindowsLiveAuth,
                this.gmapApiKey,
                this.apiKeyExtra1,
                this.apiKeyExtra2,
                this.apiKeyExtra3,
                this.apiKeyExtra4,
                this.apiKeyExtra5,
                this.disableDbAuth,
                   this.articleCategory,
                this.coreEventCategory,
                this.coreLoaiVanBanQuyPham,
                this.coreLinhVucVanBanQuyPham,
                this.coreCoQuanBanHanhVanBanQuyPham,
                this.coreDonVi,
                this.coreLinhVucHoiDap,
                this.coreDuLieuDaPhuongTien,
                this.coreRSS,
                this.coreHoiDong,
                this.coreChuDe,
                this.templateType,
                this.isTemplate,
                this.urlSiteMap,
                this.templateSite,
                this.subTitle,
                this.linhVucID,
                this.noiDungDieuTra,
                this.nam,
                this.tanSuatDieuTra,
                this.phamViSoLieu,
                this.doiTuongDieuTra,
                this.pathIMG,
                this.isTongDieuTra,
                this.isCuocDieuTra,
                this.parentID,
                this.trangThaiDieuTra,
                this.fileDuThao,
                this.giaoDienHienThi
                );

            if (success && this.extendedPropertiesAreDirty)
            {
                success = UpdateExtendedProperties();
            }

            if (success)
            {
                SaveExpandoProperties();
            }

            return success;
        }

        private bool UpdateExtendedProperties()
        {
            bool success = DBSiteSettings.UpdateExtendedProperties(
                this.siteID,
                this.allowPasswordRetrieval,
                this.allowPasswordReset,
                this.requiresQuestionAndAnswer,
                this.maxInvalidPasswordAttempts,
                this.passwordAttemptWindowMinutes,
                this.requiresUniqueEmail,
                this.passwordFormat,
                this.minRequiredPasswordLength,
                this.minRequiredNonAlphanumericCharacters,
                this.passwordStrengthRegularExpression,
                this.defaultEmailFromAddress);

            return success;
        }

        #endregion

        #region Public Methods

        public bool Save()
        {
            if (this.siteID > 0)
            {
                return Update();
            }
            else
            {
                return Create();
            }
        }




        #endregion

        #region ExpandoProperties

        private DataTable exapandoProperties = null;

        private void EnsureExpandoProperties()
        {
            if (exapandoProperties == null)
            {
                exapandoProperties = GetExpandoProperties(siteID);
            }

        }

        public void ReloadExpandoProperties()
        {
            exapandoProperties = GetExpandoProperties(siteID);

        }

        public void SaveExpandoProperties()
        {
            if (exapandoProperties == null)
            {
                log.Info("SiteSettings expandoProperties was null so nothing was saved");
                return;
            }

            foreach (DataRow row in exapandoProperties.Rows)
            {
                bool isDirty = Convert.ToBoolean(row["IsDirty"]);
                if (isDirty)
                {
                    DBSiteSettingsEx.SaveExpandoProperty(
                        siteID,
                        siteGuid,
                        row["GroupName"].ToString(),
                        row["KeyName"].ToString(),
                        row["KeyValue"].ToString());

                }

            }

        }

        public string GetExpandoProperty(string keyName)
        {
            EnsureExpandoProperties();

            foreach (DataRow row in exapandoProperties.Rows)
            {
                if (row["KeyName"].ToString().Trim().Equals(keyName, StringComparison.InvariantCulture))
                {
                    return row["KeyValue"].ToString();
                }

            }

            return null;

        }

        public void SetExpandoProperty(string keyName, string keyValue)
        {
            EnsureExpandoProperties();
            //bool found = false;
            foreach (DataRow row in exapandoProperties.Rows)
            {
                if (row["KeyName"].ToString().Trim().Equals(keyName, StringComparison.InvariantCulture))
                {
                    row["KeyValue"] = keyValue;
                    row["IsDirty"] = true;
                    //found = true;
                    break;
                }

            }

            //if (!found)
            //{
            //    DBSiteSettingsEx.SaveExpandoProperty(
            //            siteID,
            //            siteGuid,
            //            "General",
            //            keyName,
            //            keyValue);

            //}

        }


        private static DataTable GetExpandoProperties(int siteId)
        {
            if (siteId == -1) { return GetDefaultExpandoProperties(); } //new site

            DataTable dataTable = CreateExpandoTable();
            dataTable.TableName = "expandoProperties";


            using (IDataReader reader = DBSiteSettingsEx.GetSiteSettingsExList(siteId))
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["SiteID"] = reader["SiteID"];
                    row["KeyName"] = reader["KeyName"];
                    row["KeyValue"] = reader["KeyValue"];
                    row["GroupName"] = reader["GroupName"];

                    row["IsDirty"] = false;

                    dataTable.Rows.Add(row);

                }
            }

            return dataTable;
        }

        private static DataTable GetDefaultExpandoProperties()
        {

            DataTable dataTable = CreateExpandoTable();

            using (IDataReader reader = DBSiteSettingsEx.GetDefaultExpandoSettings())
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["SiteID"] = -1;
                    row["KeyName"] = reader["KeyName"];
                    row["KeyValue"] = reader["DefaultValue"];
                    row["GroupName"] = reader["GroupName"];

                    row["IsDirty"] = false;

                    dataTable.Rows.Add(row);

                }
            }


            return dataTable;
        }

        private static DataTable CreateExpandoTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SiteID", typeof(int));
            dataTable.Columns.Add("KeyName", typeof(string));
            dataTable.Columns.Add("KeyValue", typeof(string));
            dataTable.Columns.Add("GroupName", typeof(string));
            dataTable.Columns.Add("IsDirty", typeof(bool));

            return dataTable;

        }


        #endregion


        #region Static Methods

        public static bool UpdateFanPage(int siteId, string fanpageIframe)
        {
            return DBSiteSettings.UpdateFanPage(siteId, fanpageIframe);
        }
        public static bool UpdateFooter(int siteId, string footer)
        {
            return DBSiteSettings.UpdateFooter(siteId, footer);
        }

        public static string GetDataFolder(int siteId)
        {
            return "~/Data/Sites/" + siteId.ToString(CultureInfo.InvariantCulture) + "/";
        }


        public static IDataReader GetSiteList()
        {
            return DBSiteSettings.GetSiteList();
        }

        public static IDataReader GetListSite()
        {
            return DBSiteSettings.GetListSite();
        }
        public static IDataReader GetListSiteParent()
        {
            return DBSiteSettings.GetListSiteParent();
        }

        public static IDataReader GetListSiteShort()
        {
            return DBSiteSettings.GetListSiteShort();
        }


        public static List<SiteSettings> GetSiteListTemplate()
        {
            return LoadSite(DBSiteSettings.GetListTemplate());
        }

        public static List<SiteSettings> GetListByParent(int parentId)
        {
            return LoadSite(DBSiteSettings.GetListByParent(parentId));
        }

        public static List<int> GetAllSiteID()
        {
            List<int> result = new List<int>();
            using (var reader = DBSiteSettings.GetAllSiteID())
            {
                while (reader.Read())
                {
                    result.Add(Convert.ToInt32(reader["SiteID"]));
                }
            }
            return result;
        }

        public static string GetSiteName(string sites)
        {
            var result = new List<string>();

            using (var reader = DBSiteSettings.GetSiteName(sites))
            {
                while (reader.Read())
                {
                    result.Add(reader["SiteName"].ToString());
                }
            }

            return string.Join(", ", result.ToArray());
        }


        public static List<SiteStatic> GetStaticSite(string siteList, DateTime? startDate, DateTime? endDate)
        {
            List<SiteStatic> result = new List<SiteStatic>();
            using (var reader = DBSiteSettings.GetStaticArticleSite(siteList, startDate, endDate))
            {
                while (reader.Read())
                {
                    SiteStatic siteStatic = new SiteStatic();
                    siteStatic.CountArticleID = Convert.ToInt32(reader["TotalArticle"]);
                    siteStatic.SiteName = reader["SiteName"].ToString();
                    result.Add(siteStatic);
                }
            }
            return result;
        }

        public static List<SiteSettings> LoadSite(IDataReader reader)
        {
            List<SiteSettings> listSite = new List<SiteSettings>();

            while (reader.Read())
            {
                SiteSettings site = new SiteSettings();
                site.siteID = Convert.ToInt32(reader["SiteID"]);
                site.siteName = reader["SiteName"].ToString();
                site.templateType = Convert.ToInt32(reader["TemplateType"]);
                if (reader["UrlSiteMap"] != DBNull.Value)
                {
                    site.urlSiteMap = reader["UrlSiteMap"].ToString();
                }
                if (reader["Nam"] != DBNull.Value)
                {
                    site.nam = Convert.ToInt32(reader["Nam"].ToString());
                }
                listSite.Add(site);
            }
            return listSite;
        }
        public static IDataReader GetListTemplate()
        {
            return DBSiteSettings.GetListTemplate();
        }

        public static string GetName(int siteId)
        {
            using (IDataReader reader = DBSiteSettings.GetName(siteId))
            {
                while (reader.Read())
                {
                    return reader["SiteName"].ToString();
                }
            }
            return string.Empty;
        }

        public static DataTable GetListAllSite()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SiteID", typeof(int));
            dataTable.Columns.Add("SiteName", typeof(string));
            using (IDataReader reader = DBSiteSettings.GetListSite())
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["SiteID"] = reader["SiteID"];
                    row["SiteName"] = reader["SiteName"];
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        public static DataTable GetByLinhVuc(int linhVucId)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SiteID", typeof(int));
            dataTable.Columns.Add("SiteName", typeof(string));
            dataTable.Columns.Add("UrlSiteMap", typeof(string));
            dataTable.Columns.Add("NoiDungDieuTra", typeof(string));
            dataTable.Columns.Add("CreatedDate", typeof(DateTime));
            dataTable.Columns.Add("PathIMG", typeof(string));
            dataTable.Columns.Add("LinhVucName", typeof(string));


            using (IDataReader reader = DBSiteSettings.GetByLinhVuc(linhVucId))
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["SiteID"] = reader["SiteID"];
                    row["SiteName"] = reader["SiteName"];
                    row["UrlSiteMap"] = reader["UrlSiteMap"];
                    row["NoiDungDieuTra"] = reader["NoiDungDieuTra"];
                    row["CreatedDate"] = reader["CreatedDate"];
                    row["PathIMG"] = reader["PathIMG"];
                    row["LinhVucName"] = reader["LinhVucName"];
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        public static DataTable GetSiteIdList()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("SiteID", typeof(int));

            using (IDataReader reader = GetSiteList())
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["SiteID"] = reader["SiteID"];
                    dataTable.Rows.Add(row);
                }
            }


            return dataTable;

        }

        public static Guid GetRootSiteGuid()
        {
            Guid result = Guid.Empty;

            using (IDataReader reader = DBSiteSettings.GetSiteList())
            {
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader["IsServerAdminSite"]))
                    {
                        result = new Guid(reader["SiteGuid"].ToString());
                        break;
                    }


                }
            }

            return result;
        }

        public static int SiteCount()
        {
            int sitesFound = 0;

            try
            {
                using (IDataReader reader = DBSiteSettings.GetSiteList())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            sitesFound += 1;

                        }
                    }
                }

            }
            catch (System.Data.Common.DbException) { }
            catch (InvalidOperationException) { }
            catch (System.Security.SecurityException) { }
            catch (System.Net.Sockets.SocketException) { }


            return sitesFound;


        }





        //public static DataSet GetPageListForAdmin(int siteID) 
        //{
        //    return dbSiteSettings.PageSettings_GetPageTree(siteID);
        //}

        //public static IDataReader GetPageListForAdminReader(int siteID)
        //{
        //    return dbSiteSettings.PageSettings_GetPageTreeReader(siteID);
        //}

        public static void AddFeature(Guid siteGuid, Guid featureGuid)
        {
            DBSiteSettings.AddFeature(siteGuid, featureGuid);
        }

        public static void RemoveFeature(Guid siteGuid, Guid featureGuid)
        {
            DBSiteSettings.RemoveFeature(siteGuid, featureGuid);
        }

        public static IDataReader GetHostList(int siteId)
        {
            return DBSiteSettings.GetHostList(siteId);
        }

        public static void AddHost(Guid siteGuid, int siteId, string hostName)
        {
            DBSiteSettings.AddHost(siteGuid, siteId, hostName);
        }

        public static void RemoveHost(int hostId)
        {
            DBSiteSettings.DeleteHost(hostId);
        }

        //public static SiteSettings GetCurrent()
        //{
        //    //if (HttpContext.Current != null)
        //    //{
        //    //    if (HttpContext.Current.Items["SiteSettings"] != null)
        //    //    {
        //    //        return (SiteSettings)HttpContext.Current.Items["SiteSettings"];
        //    //    }
        //    //}


        //    //return null;

        //    return CacheHelper.GetCurrentSiteSettings();

        //}


        //public static int GetCurrentSiteID()
        //{
        //    int siteID = -1;

        //    SiteSettings siteSettings = GetCurrent();
        //    if (siteSettings != null)
        //    {
        //        siteID = siteSettings.SiteID;
        //    }

        //    return siteID;

        //}

        public static int CreateNewSite()
        {
            return CreateNewSite("mojoPortal");

        }

        public static int CreateNewSite(String siteName)
        {
            //dbSiteSettings.CreateDefaultData(this.siteID);
            SiteSettings newSite = new SiteSettings();
            newSite.SiteName = siteName;
            newSite.Save();
            //CreateDefaultData(newSite.SiteID);

            return newSite.SiteId;

        }

        public static void Delete(int siteId)
        {
            DBSiteSettings.Delete(siteId);
        }

        public static int GetCountOfOtherSites(int currentSiteId)
        {
            return DBSiteSettings.CountOtherSites(currentSiteId);
        }

        public static IDataReader GetPageOfOtherSites(
            int currentSiteId,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            return DBSiteSettings.GetPageOfOtherSites(
                currentSiteId,
                pageNumber,
                pageSize,
                out totalPages);

        }

        //public static void CreateDefaultData(int siteID)
        //{
        //    //dbSiteSettings.CreateDefaultData(newSite.SiteID);


        //}


        //public static int GetSiteIdFromFolderName(string folderName)
        //{
        //    int siteID = 1;

        //    // TODO: implement, this is just test logic
        //    if (folderName.ToLower() == "joefolder") siteID = 2;



        //    return siteID;

        //}

        //public static IDataReader GetSettingsExList(int siteId)
        //{
        //    return DBSiteSettingsEx.GetSiteSettingsExList(siteId);
        //}
        //public static bool SaveSiteSettingsExList(int siteId, string xml)
        //{
        //    return DBSiteSettingsEx.SaveSiteSettingsExList(siteId, xml);
        //}

        public static void EnsureExpandoSettings()
        {
            DBSiteSettingsEx.EnsureSettings();
        }

        /// <summary>
        /// when using related sites mode this mthod is used to sync shared settings across sites when the parent site is updated
        /// </summary>
        public static void SyncRelatedSites(
            SiteSettings masterSite,
            bool usingFolderSites
            )
        {

            DBSiteSettings.UpdateRelatedSites(
                masterSite.siteID,
                masterSite.allowNewRegistration,
                masterSite.useSecureRegistration,
                masterSite.useLdapAuth,
                masterSite.autoCreateLDAPUserOnFirstLogin,
                masterSite.SiteLdapSettings.Server,
                masterSite.SiteLdapSettings.Domain,
                masterSite.SiteLdapSettings.Port,
                masterSite.SiteLdapSettings.RootDN,
                masterSite.SiteLdapSettings.UserDNKey,
                masterSite.allowUserFullNameChange,
                masterSite.useEmailForLogin,
                masterSite.allowOpenIDAuth,
                masterSite.allowWindowsLiveAuth,
                masterSite.allowPasswordRetrieval,
                masterSite.allowPasswordReset,
                masterSite.requiresQuestionAndAnswer,
                masterSite.maxInvalidPasswordAttempts,
                masterSite.passwordAttemptWindowMinutes,
                masterSite.requiresUniqueEmail,
                masterSite.passwordFormat,
                masterSite.minRequiredPasswordLength,
                masterSite.minRequiredNonAlphanumericCharacters,
                masterSite.passwordStrengthRegularExpression);

            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RequireCaptchaOnRegistration", masterSite.RequireCaptchaOnRegistration.ToString(CultureInfo.InvariantCulture));
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RequireCaptchaOnLogin", masterSite.RequireCaptchaOnLogin.ToString(CultureInfo.InvariantCulture));
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RequireEnterEmailTwiceOnRegistration", masterSite.RequireEnterEmailTwiceOnRegistration.ToString(CultureInfo.InvariantCulture));
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "ShowPasswordStrengthOnRegistration", masterSite.ShowPasswordStrengthOnRegistration.ToString(CultureInfo.InvariantCulture));
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "PasswordRegexWarning", masterSite.PasswordRegexWarning);
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RequireApprovalBeforeLogin", masterSite.RequireApprovalBeforeLogin.ToString(CultureInfo.InvariantCulture));
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RequirePasswordChangeOnResetRecover", masterSite.RequirePasswordChangeOnResetRecover.ToString(CultureInfo.InvariantCulture));
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "AllowPersistentLogin", masterSite.AllowPersistentLogin.ToString(CultureInfo.InvariantCulture));

            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "AllowDbFallbackWithLdap", masterSite.AllowDbFallbackWithLdap.ToString(CultureInfo.InvariantCulture));
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "AllowEmailLoginWithLdapDbFallback", masterSite.AllowEmailLoginWithLdapDbFallback.ToString(CultureInfo.InvariantCulture));



            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanViewMemberList", masterSite.RolesThatCanViewMemberList);
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanManageUsers", masterSite.RolesThatCanCreateUsers);
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanFullyManageUsers", masterSite.RolesThatCanManageUsers);
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanLookupUsers", masterSite.RolesThatCanLookupUsers);

            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "EmailAdressesForUserApprovalNotification", masterSite.EmailAdressesForUserApprovalNotification);



            //2011-12-14 these should be site specific even in rleated sites mode so I commented them out.
            //http://www.mojoportal.com/Forums/Thread.aspx?thread=9443&mid=34&pageid=5&ItemID=5&pagenumber=1#post39194
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "DefaultRootPageViewRoles", masterSite.DefaultRootPageViewRoles);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "DefaultRootPageEditRoles", masterSite.DefaultRootPageEditRoles);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "DefaultRootPageCreateChildPageRoles", masterSite.DefaultRootPageCreateChildPageRoles);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanAssignSkinsToPages", masterSite.RolesThatCanAssignSkinsToPages);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanViewMyPage", masterSite.RolesThatCanViewMyPage);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesNotAllowedToEditModuleSettings", masterSite.RolesNotAllowedToEditModuleSettings);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanEditContentTemplates", masterSite.RolesThatCanEditContentTemplates);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "GeneralBrowseAndUploadRoles", masterSite.GeneralBrowseAndUploadRoles);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "UserFilesBrowseAndUploadRoles", masterSite.UserFilesBrowseAndUploadRoles);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanDeleteFilesInEditor", masterSite.RolesThatCanDeleteFilesInEditor);
            //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RolesThatCanManageSkins", masterSite.RolesThatCanManageSkins);

            // note that when using host name based related sites you must specify each host name as whitelisted in your Janrain Engage account
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RpxNowApplicationName", masterSite.RpxNowApplicationName);
            DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "RpxNowApiKey", masterSite.RpxNowApiKey);

            if (usingFolderSites)
            {
                DBSiteSettings.UpdateRelatedSitesWindowsLive(masterSite.siteID, masterSite.windowsLiveAppID, masterSite.windowsLiveKey);

                // commented these out 2014-01-10 it should be possible to close folder sites independently from one another
                //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "SiteIsClosed", masterSite.SiteIsClosed.ToString(CultureInfo.InvariantCulture));
                //DBSiteSettingsEx.UpdateRelatedSitesProperty(masterSite.siteID, "SiteIsClosedMessage", masterSite.SiteIsClosedMessage);



            }


        }

        public static int GetSiteIdByHostName(string hostName)
        {
            return DBSiteSettings.GetSiteIdByHostName(hostName);
        }

        public static int GetSiteIdByFolder(string folderName)
        {
            return DBSiteSettings.GetSiteIdByFolder(folderName);
        }

        public static bool HostNameExists(string hostName)
        {
            return DBSiteSettings.HostNameExists(hostName);
        }
        public static bool UpdateView(int siteID, int isView)
        {
            return DBSiteSettings.UpdateView(siteID, isView);
        }
        #endregion

        public event SiteCreatedEventHandler SiteCreated;

        protected void OnSiteCreated(SiteCreatedEventArgs e)
        {
            if (SiteCreated != null)
            {
                SiteCreated(this, e);
            }
        }


    }

    public class SiteStatic
    {
        public int CountArticleID { get; set; }
        public string SiteName { get; set; }
    }


    public delegate void SiteCreatedEventHandler(object sender, SiteCreatedEventArgs e);

    public class SiteCreatedEventArgs : EventArgs
    {
        private SiteSettings _site = null;

        public SiteSettings Site
        {
            get { return _site; }
        }

        public SiteCreatedEventArgs(SiteSettings siteSettings)
        {
            _site = siteSettings;
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("mp_Sites")]
    public class mp_Sites
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SiteID { get; set; }
        public System.Guid SiteGuid { get; set; }
        public string SiteAlias { get; set; }
        public string SiteName { get; set; }
        public string Skin { get; set; }
        public string Logo { get; set; }
        public string Icon { get; set; }
        public bool AllowUserSkins { get; set; }
        public bool AllowPageSkins { get; set; }
        public bool AllowHideMenuOnPages { get; set; }
        public bool AllowNewRegistration { get; set; }
        public bool UseSecureRegistration { get; set; }
        public bool UseSSLOnAllPages { get; set; }
        public string DefaultPageKeyWords { get; set; }
        public string DefaultPageDescription { get; set; }
        public string DefaultPageEncoding { get; set; }
        public string DefaultAdditionalMetaTags { get; set; }
        public bool IsServerAdminSite { get; set; }
        public bool UseLdapAuth { get; set; }
        public bool AutoCreateLdapUserOnFirstLogin { get; set; }
        public string LdapServer { get; set; }
        public int LdapPort { get; set; }
        public string LdapDomain { get; set; }
        public string LdapRootDN { get; set; }
        public string LdapUserDNKey { get; set; }
        public bool ReallyDeleteUsers { get; set; }
        public bool UseEmailForLogin { get; set; }
        public bool AllowUserFullNameChange { get; set; }
        public string EditorSkin { get; set; }
        public string DefaultFriendlyUrlPatternEnum { get; set; }
        public bool AllowPasswordRetrieval { get; set; }
        public bool AllowPasswordReset { get; set; }
        public bool RequiresQuestionAndAnswer { get; set; }
        public int MaxInvalidPasswordAttempts { get; set; }
        public int PasswordAttemptWindowMinutes { get; set; }
        public bool RequiresUniqueEmail { get; set; }
        public int PasswordFormat { get; set; }
        public int MinRequiredPasswordLength { get; set; }
        public int MinReqNonAlphaChars { get; set; }
        public string PwdStrengthRegex { get; set; }
        public string DefaultEmailFromAddress { get; set; }
        public bool EnableMyPageFeature { get; set; }
        public string EditorProvider { get; set; }
        public string CaptchaProvider { get; set; }
        public string DatePickerProvider { get; set; }
        public string RecaptchaPrivateKey { get; set; }
        public string RecaptchaPublicKey { get; set; }
        public string WordpressAPIKey { get; set; }
        public string WindowsLiveAppID { get; set; }
        public string WindowsLiveKey { get; set; }
        public bool AllowOpenIDAuth { get; set; }
        public bool AllowWindowsLiveAuth { get; set; }
        public string GmapApiKey { get; set; }
        public string ApiKeyExtra1 { get; set; }
        public string ApiKeyExtra2 { get; set; }
        public string ApiKeyExtra3 { get; set; }
        public string ApiKeyExtra4 { get; set; }
        public string ApiKeyExtra5 { get; set; }
        public Nullable<bool> DisableDbAuth { get; set; }
        public Nullable<int> ArticleCategoryID { get; set; }
        public Nullable<int> CoreEventCategoryID { get; set; }
        public Nullable<int> CoreLoaiVanBanCategoryID { get; set; }
        public Nullable<int> CoreLinhVucVanBanCategoryID { get; set; }
        public Nullable<int> CoreCoQuanBanHanhVanBanCategoryID { get; set; }
        public Nullable<int> CoreDonviCategoryID { get; set; }
        public Nullable<int> CoreLinhVucHoiDapCategoryID { get; set; }
        public Nullable<int> CoreDuLieuDaPhuongTienCategoryID { get; set; }
        public Nullable<int> CoreRSSCategoryID { get; set; }
        public Nullable<int> CoreHoiDongCategoryID { get; set; }
        public Nullable<int> CoreChuDeCategoryID { get; set; }
        public Nullable<int> IsView { get; set; }
        public Nullable<int> TemplateType { get; set; }
        public Nullable<bool> IsTemplate { get; set; }
        public string UrlSiteMap { get; set; }
        public Nullable<int> TemplateSite { get; set; }
        public string SiteSubTitle { get; set; }
        public string FanPageIframe { get; set; }
        public string Footer { get; set; }
        public Nullable<int> LinhVucID { get; set; }
        public string NoiDungDieuTra { get; set; }
        public Nullable<int> Nam { get; set; }
        public string TanSuatDieuTra { get; set; }
        public string PhamViSoLieu { get; set; }
        public string DoiTuongDieuTra { get; set; }
        public string PathIMG { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedByUser { get; set; }
        public Nullable<bool> IsTongDieuTra { get; set; }
        public Nullable<bool> IsCuocDieuTra { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<System.DateTime> HanGopY { get; set; }
        public Nullable<int> TrangThaiDieuTra { get; set; }
        public string FileDuThao { get; set; }
    

        public virtual ICollection<mp_Roles> mp_Roles { get; set; }
    }
}

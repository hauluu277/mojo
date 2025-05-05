/// Author:					
/// Created:				2007-11-03
/// Last Modified:			2017-09-11
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.


using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;


namespace mojoPortal.Data
{

    public static class DBSiteSettings
    {

        public static int Create(
           Guid siteGuid,
           String siteName,
           String skin,
           String logo,
           String icon,
           bool allowNewRegistration,
           bool allowUserSkins,
           bool allowPageSkins,
           bool allowHideMenuOnPages,
           bool useSecureRegistration,
           bool useSslOnAllPages,
           String defaultPageKeywords,
           String defaultPageDescription,
           String defaultPageEncoding,
           String defaultAdditionalMetaTags,
           bool isServerAdminSite,
           bool useLdapAuth,
           bool autoCreateLdapUserOnFirstLogin,
           String ldapServer,
           int ldapPort,
           String ldapDomain,
           String ldapRootDN,
           String ldapUserDNKey,
           bool allowUserFullNameChange,
           bool useEmailForLogin,
           bool reallyDeleteUsers,
           String editorSkin,
           String defaultFriendlyUrlPattern,
           bool enableMyPageFeature,
           string editorProvider,
           string datePickerProvider,
           string captchaProvider,
           string recaptchaPrivateKey,
           string recaptchaPublicKey,
           string wordpressApiKey,
           string windowsLiveAppId,
           string windowsLiveKey,
           bool allowOpenIdAuth,
           bool allowWindowsLiveAuth,
           string gmapApiKey,
           string apiKeyExtra1,
           string apiKeyExtra2,
           string apiKeyExtra3,
           string apiKeyExtra4,
           string apiKeyExtra5,
           bool disableDbAuth,
            int articleCategory,
           int coreEventCategory,
           int coreLoaiVanBanQuyPham,
           int coreLinhVucVanBanQuyPham,
           int coreCoQuanBanHanhVanBanQuyPham,
           int coreDonVi,
           int coreLinhVucHoiDap,
           int coreDuLieuDaPhuongTien,
           int coreRSS,
           int coreHoiDong,
           int coreChuDe,
           int templateType,
           bool isTemplate,
           string urlSiteMap,
           int templateSite,
            string siteSubtitle,
            int linhVucID,
            string noiDungDieuTra,
            int nam,
            string tanSuatDieuTra,
            string phamViSoLieu,
            string doiTuongDieuTra,
            string pathIMG,
            DateTime createdDate,
            int createdByUser,
            bool isTongDieuTra,
            bool isCuocDieuTra,
            int? parentId,
            int? trangThaiDieuTra,
            string fileDuThao,
            string giaoDienHienThi)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_Insert", 77);
            sph.DefineSqlParameter("@SiteName", SqlDbType.NVarChar, 550, ParameterDirection.Input, siteName);
            sph.DefineSqlParameter("@Skin", SqlDbType.NVarChar, 100, ParameterDirection.Input, skin);
            sph.DefineSqlParameter("@Logo", SqlDbType.NVarChar, 50, ParameterDirection.Input, logo);
            sph.DefineSqlParameter("@Icon", SqlDbType.NVarChar, 50, ParameterDirection.Input, icon);
            sph.DefineSqlParameter("@AllowUserSkins", SqlDbType.Bit, ParameterDirection.Input, allowUserSkins);
            sph.DefineSqlParameter("@AllowNewRegistration", SqlDbType.Bit, ParameterDirection.Input, allowNewRegistration);
            sph.DefineSqlParameter("@UseSecureRegistration", SqlDbType.Bit, ParameterDirection.Input, useSecureRegistration);
            sph.DefineSqlParameter("@UseSSLOnAllPages", SqlDbType.Bit, ParameterDirection.Input, useSslOnAllPages);
            sph.DefineSqlParameter("@DefaultPageKeywords", SqlDbType.NVarChar, 255, ParameterDirection.Input, defaultPageKeywords);
            sph.DefineSqlParameter("@DefaultPageDescription", SqlDbType.NVarChar, 255, ParameterDirection.Input, defaultPageDescription);
            sph.DefineSqlParameter("@DefaultPageEncoding", SqlDbType.NVarChar, 255, ParameterDirection.Input, defaultPageEncoding);
            sph.DefineSqlParameter("@DefaultAdditionalMetaTags", SqlDbType.NVarChar, 255, ParameterDirection.Input, defaultAdditionalMetaTags);
            sph.DefineSqlParameter("@IsServerAdminSite", SqlDbType.Bit, ParameterDirection.Input, isServerAdminSite);
            sph.DefineSqlParameter("@AllowPageSkins", SqlDbType.Bit, ParameterDirection.Input, allowPageSkins);
            sph.DefineSqlParameter("@AllowHideMenuOnPages", SqlDbType.Bit, ParameterDirection.Input, allowHideMenuOnPages);
            sph.DefineSqlParameter("@UseLdapAuth", SqlDbType.Bit, ParameterDirection.Input, useLdapAuth);
            sph.DefineSqlParameter("@AutoCreateLDAPUserOnFirstLogin", SqlDbType.Bit, ParameterDirection.Input, autoCreateLdapUserOnFirstLogin);
            sph.DefineSqlParameter("@LdapServer", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapServer);
            sph.DefineSqlParameter("@LdapPort", SqlDbType.Int, ParameterDirection.Input, ldapPort);
            sph.DefineSqlParameter("@LdapDomain", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapDomain);
            sph.DefineSqlParameter("@LdapRootDN", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapRootDN);
            sph.DefineSqlParameter("@LdapUserDNKey", SqlDbType.NVarChar, 10, ParameterDirection.Input, ldapUserDNKey);
            sph.DefineSqlParameter("@AllowUserFullNameChange", SqlDbType.Bit, ParameterDirection.Input, allowUserFullNameChange);
            sph.DefineSqlParameter("@UseEmailForLogin", SqlDbType.Bit, ParameterDirection.Input, useEmailForLogin);
            sph.DefineSqlParameter("@ReallyDeleteUsers", SqlDbType.Bit, ParameterDirection.Input, reallyDeleteUsers);
            sph.DefineSqlParameter("@EditorSkin", SqlDbType.NVarChar, 50, ParameterDirection.Input, editorSkin);
            sph.DefineSqlParameter("@DefaultFriendlyUrlPatternEnum", SqlDbType.NVarChar, 50, ParameterDirection.Input, defaultFriendlyUrlPattern);
            sph.DefineSqlParameter("@SiteGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, siteGuid);
            sph.DefineSqlParameter("@EnableMyPageFeature", SqlDbType.Bit, ParameterDirection.Input, enableMyPageFeature);
            sph.DefineSqlParameter("@EditorProvider", SqlDbType.NVarChar, 255, ParameterDirection.Input, editorProvider);
            sph.DefineSqlParameter("@DatePickerProvider", SqlDbType.NVarChar, 255, ParameterDirection.Input, datePickerProvider);
            sph.DefineSqlParameter("@CaptchaProvider", SqlDbType.NVarChar, 255, ParameterDirection.Input, captchaProvider);
            sph.DefineSqlParameter("@RecaptchaPrivateKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, recaptchaPrivateKey);
            sph.DefineSqlParameter("@RecaptchaPublicKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, recaptchaPublicKey);
            sph.DefineSqlParameter("@WordpressAPIKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, wordpressApiKey);
            sph.DefineSqlParameter("@WindowsLiveAppID", SqlDbType.NVarChar, 255, ParameterDirection.Input, windowsLiveAppId);
            sph.DefineSqlParameter("@WindowsLiveKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, windowsLiveKey);
            sph.DefineSqlParameter("@AllowOpenIDAuth", SqlDbType.Bit, ParameterDirection.Input, allowOpenIdAuth);
            sph.DefineSqlParameter("@AllowWindowsLiveAuth", SqlDbType.Bit, ParameterDirection.Input, allowWindowsLiveAuth);

            sph.DefineSqlParameter("@GmapApiKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, gmapApiKey);
            sph.DefineSqlParameter("@ApiKeyExtra1", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra1);
            sph.DefineSqlParameter("@ApiKeyExtra2", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra2);
            sph.DefineSqlParameter("@ApiKeyExtra3", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra3);
            sph.DefineSqlParameter("@ApiKeyExtra4", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra4);
            sph.DefineSqlParameter("@ApiKeyExtra5", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra5);
            sph.DefineSqlParameter("@DisableDbAuth", SqlDbType.Bit, ParameterDirection.Input, disableDbAuth);

            sph.DefineSqlParameter("@ArticleCategoryID", SqlDbType.Int, ParameterDirection.Input, articleCategory);
            sph.DefineSqlParameter("@CoreEventCategoryID", SqlDbType.Int, ParameterDirection.Input, coreEventCategory);
            sph.DefineSqlParameter("@CoreLoaiVanBanCategoryID", SqlDbType.Int, ParameterDirection.Input, coreLoaiVanBanQuyPham);
            sph.DefineSqlParameter("@CoreLinhVucVanBanCategoryID", SqlDbType.Int, ParameterDirection.Input, coreLinhVucVanBanQuyPham);
            sph.DefineSqlParameter("@CoreCoQuanBanHanhVanBanCategoryID", SqlDbType.Int, ParameterDirection.Input, coreCoQuanBanHanhVanBanQuyPham);
            sph.DefineSqlParameter("@CoreDonViCategoryID", SqlDbType.Int, ParameterDirection.Input, coreDonVi);
            sph.DefineSqlParameter("@CoreLinhVucHoiDapCategoryID", SqlDbType.Int, ParameterDirection.Input, coreLinhVucHoiDap);
            sph.DefineSqlParameter("@CoreDuLieuDaPhuongTienCategoryID", SqlDbType.Int, ParameterDirection.Input, coreDuLieuDaPhuongTien);
            sph.DefineSqlParameter("@CoreRSSCategoryID", SqlDbType.Int, ParameterDirection.Input, coreRSS);
            sph.DefineSqlParameter("@CoreHoiDongCategoryID", SqlDbType.Int, ParameterDirection.Input, coreHoiDong);
            sph.DefineSqlParameter("@CoreChuDeCategoryID", SqlDbType.Int, ParameterDirection.Input, coreChuDe);
            sph.DefineSqlParameter("@TemplateType", SqlDbType.Int, ParameterDirection.Input, templateType);
            sph.DefineSqlParameter("@IsTemplate", SqlDbType.Bit, ParameterDirection.Input, isTemplate);
            sph.DefineSqlParameter("@UrlSiteMap", SqlDbType.NVarChar, 250, ParameterDirection.Input, urlSiteMap);
            sph.DefineSqlParameter("@TemplateSite", SqlDbType.Int, ParameterDirection.Input, templateSite);
            sph.DefineSqlParameter("@SiteSubTitle", SqlDbType.NVarChar, 250, ParameterDirection.Input, siteSubtitle);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucID);
            sph.DefineSqlParameter("@NoiDungDieuTra", SqlDbType.NVarChar, -1, ParameterDirection.Input, noiDungDieuTra);
            sph.DefineSqlParameter("@Nam", SqlDbType.Int, ParameterDirection.Input, nam);
            sph.DefineSqlParameter("@TanSuatDieuTra", SqlDbType.NVarChar, 250, ParameterDirection.Input, tanSuatDieuTra);
            sph.DefineSqlParameter("@PhamViSoLieu", SqlDbType.NVarChar, 250, ParameterDirection.Input, phamViSoLieu);
            sph.DefineSqlParameter("@DoiTuongDieuTra", SqlDbType.NVarChar, 250, ParameterDirection.Input, doiTuongDieuTra);
            sph.DefineSqlParameter("@PathIMG", SqlDbType.NVarChar, -1, ParameterDirection.Input, pathIMG);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.Int, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@IsTongDieuTra", SqlDbType.Bit, ParameterDirection.Input, isTongDieuTra);
            sph.DefineSqlParameter("@IsCuocDieuTra", SqlDbType.Bit, ParameterDirection.Input, isCuocDieuTra);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);
            sph.DefineSqlParameter("@TrangThaiDieuTra", SqlDbType.Int, ParameterDirection.Input, trangThaiDieuTra);
            sph.DefineSqlParameter("@FileDuThao", SqlDbType.NVarChar, -1, ParameterDirection.Input, fileDuThao);
            sph.DefineSqlParameter("@GiaoDienHienThi", SqlDbType.NVarChar, 250, ParameterDirection.Input, giaoDienHienThi);
            
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        public static bool UpdateFanPage(int siteId, string fanPageIframe)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_UpdateFanPage", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@FanPageIframe", SqlDbType.NVarChar, -1, ParameterDirection.Input, fanPageIframe);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static bool UpdateFooter(int siteId, string footer)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_UpdateFooter", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Footer", SqlDbType.NVarChar, -1, ParameterDirection.Input, footer);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }


        public static bool Update(
            int siteId,
            string siteName,
            string skin,
            string logo,
            string icon,
            bool allowNewRegistration,
            bool allowUserSkins,
            bool allowPageSkins,
            bool allowHideMenuOnPages,
            bool useSecureRegistration,
            bool useSslOnAllPages,
            string defaultPageKeywords,
            string defaultPageDescription,
            string defaultPageEncoding,
            string defaultAdditionalMetaTags,
            bool isServerAdminSite,
            bool useLdapAuth,
            bool autoCreateLdapUserOnFirstLogin,
            string ldapServer,
            int ldapPort,
            String ldapDomain,
            string ldapRootDN,
            string ldapUserDNKey,
            bool allowUserFullNameChange,
            bool useEmailForLogin,
            bool reallyDeleteUsers,
            String editorSkin,
            String defaultFriendlyUrlPattern,
            bool enableMyPageFeature,
            string editorProvider,
            string datePickerProvider,
            string captchaProvider,
            string recaptchaPrivateKey,
            string recaptchaPublicKey,
            string wordpressApiKey,
            string windowsLiveAppId,
            string windowsLiveKey,
            bool allowOpenIdAuth,
            bool allowWindowsLiveAuth,
            string gmapApiKey,
            string apiKeyExtra1,
            string apiKeyExtra2,
            string apiKeyExtra3,
            string apiKeyExtra4,
            string apiKeyExtra5,
            bool disableDbAuth,
            int articleCategory,
            int coreEventCategory,
            int coreLoaiVanBanQuyPham,
            int coreLinhVucVanBanQuyPham,
            int coreCoQuanBanHanhVanBanQuyPham,
            int coreDonVi,
            int coreLinhVucHoiDap,
            int coreDuLieuDaPhuongTien,
            int coreRSS,
            int coreHoiDong,
            int coreChuDe,
            int templateType,
            bool isTemplate,
            string urlSiteMap,
            int templateSite,
            string siteSubtitle,
            int linhVucID,
            string noiDungDieuTra,
            int nam,
            string tanSuatDieuTra,
            string phamViSoLieu,
            string doiTuongDieuTra,
            string pathIMG,
            bool isTongDieuTra,
            bool isCuocDieuTra,
            int? parentId,
            int? trangThaiDieuTra,
            string fileDuThao,
            string giaoDienHienThi)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_Update", 75);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@SiteName", SqlDbType.NVarChar, 550, ParameterDirection.Input, siteName);
            sph.DefineSqlParameter("@Skin", SqlDbType.NVarChar, 100, ParameterDirection.Input, skin);
            sph.DefineSqlParameter("@Logo", SqlDbType.NVarChar, 50, ParameterDirection.Input, logo);
            sph.DefineSqlParameter("@Icon", SqlDbType.NVarChar, 50, ParameterDirection.Input, icon);
            sph.DefineSqlParameter("@AllowNewRegistration", SqlDbType.Bit, ParameterDirection.Input, allowNewRegistration);
            sph.DefineSqlParameter("@AllowUserSkins", SqlDbType.Bit, ParameterDirection.Input, allowUserSkins);
            sph.DefineSqlParameter("@UseSecureRegistration", SqlDbType.Bit, ParameterDirection.Input, useSecureRegistration);
            sph.DefineSqlParameter("@UseSSLOnAllPages", SqlDbType.Bit, ParameterDirection.Input, useSslOnAllPages);
            sph.DefineSqlParameter("@DefaultPageKeywords", SqlDbType.NVarChar, 255, ParameterDirection.Input, defaultPageKeywords);
            sph.DefineSqlParameter("@DefaultPageDescription", SqlDbType.NVarChar, 255, ParameterDirection.Input, defaultPageDescription);
            sph.DefineSqlParameter("@DefaultPageEncoding", SqlDbType.NVarChar, 255, ParameterDirection.Input, defaultPageEncoding);
            sph.DefineSqlParameter("@DefaultAdditionalMetaTags", SqlDbType.NVarChar, 255, ParameterDirection.Input, defaultAdditionalMetaTags);
            sph.DefineSqlParameter("@IsServerAdminSite", SqlDbType.Bit, ParameterDirection.Input, isServerAdminSite);
            sph.DefineSqlParameter("@AllowPageSkins", SqlDbType.Bit, ParameterDirection.Input, allowPageSkins);
            sph.DefineSqlParameter("@AllowHideMenuOnPages", SqlDbType.Bit, ParameterDirection.Input, allowHideMenuOnPages);
            sph.DefineSqlParameter("@UseLdapAuth", SqlDbType.Bit, ParameterDirection.Input, useLdapAuth);
            sph.DefineSqlParameter("@AutoCreateLDAPUserOnFirstLogin", SqlDbType.Bit, ParameterDirection.Input, autoCreateLdapUserOnFirstLogin);
            sph.DefineSqlParameter("@LdapServer", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapServer);
            sph.DefineSqlParameter("@LdapPort", SqlDbType.Int, ParameterDirection.Input, ldapPort);
            sph.DefineSqlParameter("@LdapRootDN", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapRootDN);
            sph.DefineSqlParameter("@LdapUserDNKey", SqlDbType.NVarChar, 10, ParameterDirection.Input, ldapUserDNKey);
            sph.DefineSqlParameter("@AllowUserFullNameChange", SqlDbType.Bit, ParameterDirection.Input, allowUserFullNameChange);
            sph.DefineSqlParameter("@UseEmailForLogin", SqlDbType.Bit, ParameterDirection.Input, useEmailForLogin);
            sph.DefineSqlParameter("@ReallyDeleteUsers", SqlDbType.Bit, ParameterDirection.Input, reallyDeleteUsers);
            sph.DefineSqlParameter("@EditorSkin", SqlDbType.NVarChar, 50, ParameterDirection.Input, editorSkin);
            sph.DefineSqlParameter("@DefaultFriendlyUrlPatternEnum", SqlDbType.NVarChar, 50, ParameterDirection.Input, defaultFriendlyUrlPattern);
            sph.DefineSqlParameter("@EnableMyPageFeature", SqlDbType.Bit, ParameterDirection.Input, enableMyPageFeature);
            sph.DefineSqlParameter("@LdapDomain", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapDomain);
            sph.DefineSqlParameter("@EditorProvider", SqlDbType.NVarChar, 255, ParameterDirection.Input, editorProvider);
            sph.DefineSqlParameter("@DatePickerProvider", SqlDbType.NVarChar, 255, ParameterDirection.Input, datePickerProvider);
            sph.DefineSqlParameter("@CaptchaProvider", SqlDbType.NVarChar, 255, ParameterDirection.Input, captchaProvider);
            sph.DefineSqlParameter("@RecaptchaPrivateKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, recaptchaPrivateKey);
            sph.DefineSqlParameter("@RecaptchaPublicKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, recaptchaPublicKey);
            sph.DefineSqlParameter("@WordpressAPIKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, wordpressApiKey);
            sph.DefineSqlParameter("@WindowsLiveAppID", SqlDbType.NVarChar, 255, ParameterDirection.Input, windowsLiveAppId);
            sph.DefineSqlParameter("@WindowsLiveKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, windowsLiveKey);
            sph.DefineSqlParameter("@AllowOpenIDAuth", SqlDbType.Bit, ParameterDirection.Input, allowOpenIdAuth);
            sph.DefineSqlParameter("@AllowWindowsLiveAuth", SqlDbType.Bit, ParameterDirection.Input, allowWindowsLiveAuth);
            sph.DefineSqlParameter("@GmapApiKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, gmapApiKey);
            sph.DefineSqlParameter("@ApiKeyExtra1", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra1);
            sph.DefineSqlParameter("@ApiKeyExtra2", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra2);
            sph.DefineSqlParameter("@ApiKeyExtra3", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra3);
            sph.DefineSqlParameter("@ApiKeyExtra4", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra4);
            sph.DefineSqlParameter("@ApiKeyExtra5", SqlDbType.NVarChar, 255, ParameterDirection.Input, apiKeyExtra5);
            sph.DefineSqlParameter("@DisableDbAuth", SqlDbType.Bit, ParameterDirection.Input, disableDbAuth);
            sph.DefineSqlParameter("@ArticleCategoryID", SqlDbType.Int, ParameterDirection.Input, articleCategory);
            sph.DefineSqlParameter("@CoreEventCategoryID", SqlDbType.Int, ParameterDirection.Input, coreEventCategory);
            sph.DefineSqlParameter("@CoreLoaiVanBanCategoryID", SqlDbType.Int, ParameterDirection.Input, coreLoaiVanBanQuyPham);
            sph.DefineSqlParameter("@CoreLinhVucVanBanCategoryID", SqlDbType.Int, ParameterDirection.Input, coreLinhVucVanBanQuyPham);
            sph.DefineSqlParameter("@CoreCoQuanBanHanhVanBanCategoryID", SqlDbType.Int, ParameterDirection.Input, coreCoQuanBanHanhVanBanQuyPham);
            sph.DefineSqlParameter("@CoreDonViCategoryID", SqlDbType.Int, ParameterDirection.Input, coreDonVi);
            sph.DefineSqlParameter("@CoreLinhVucHoiDapCategoryID", SqlDbType.Int, ParameterDirection.Input, coreLinhVucHoiDap);
            sph.DefineSqlParameter("@CoreDuLieuDaPhuongTienCategoryID", SqlDbType.Int, ParameterDirection.Input, coreDuLieuDaPhuongTien);
            sph.DefineSqlParameter("@CoreRSSCategoryID", SqlDbType.Int, ParameterDirection.Input, coreRSS);
            sph.DefineSqlParameter("@CoreHoiDongCategoryID", SqlDbType.Int, ParameterDirection.Input, coreHoiDong);
            sph.DefineSqlParameter("@CoreChuDeCategoryID", SqlDbType.Int, ParameterDirection.Input, coreChuDe);
            sph.DefineSqlParameter("@TemplateType", SqlDbType.Int, ParameterDirection.Input, templateType);
            sph.DefineSqlParameter("@IsTemplate", SqlDbType.Bit, ParameterDirection.Input, isTemplate);
            sph.DefineSqlParameter("@UrlSiteMap", SqlDbType.NVarChar, 250, ParameterDirection.Input, urlSiteMap);
            sph.DefineSqlParameter("@TemplateSite", SqlDbType.Int, ParameterDirection.Input, templateSite);
            sph.DefineSqlParameter("@SiteSubTitle", SqlDbType.NVarChar, 250, ParameterDirection.Input, siteSubtitle);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucID);

            sph.DefineSqlParameter("@NoiDungDieuTra", SqlDbType.NVarChar, -1, ParameterDirection.Input, noiDungDieuTra);
            sph.DefineSqlParameter("@Nam", SqlDbType.Int, ParameterDirection.Input, nam);
            sph.DefineSqlParameter("@TanSuatDieuTra", SqlDbType.NVarChar, 250, ParameterDirection.Input, tanSuatDieuTra);
            sph.DefineSqlParameter("@PhamViSoLieu", SqlDbType.NVarChar, 250, ParameterDirection.Input, phamViSoLieu);
            sph.DefineSqlParameter("@DoiTuongDieuTra", SqlDbType.NVarChar, 250, ParameterDirection.Input, doiTuongDieuTra);
            sph.DefineSqlParameter("@PathIMG", SqlDbType.NVarChar, -1, ParameterDirection.Input, pathIMG);
            sph.DefineSqlParameter("@IsTongDieuTra", SqlDbType.Bit, ParameterDirection.Input, isTongDieuTra);
            sph.DefineSqlParameter("@isCuocDieuTra", SqlDbType.Bit, ParameterDirection.Input, isCuocDieuTra);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);
            sph.DefineSqlParameter("@TrangThaiDieuTra", SqlDbType.Int, ParameterDirection.Input, trangThaiDieuTra);
            sph.DefineSqlParameter("@FileDuThao", SqlDbType.NVarChar, -1, ParameterDirection.Input, fileDuThao);
            sph.DefineSqlParameter("@GiaoDienHienThi", SqlDbType.NVarChar, 250, ParameterDirection.Input, giaoDienHienThi);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static bool UpdateExtendedProperties(
            int siteId,
            bool allowPasswordRetrieval,
            bool allowPasswordReset,
            bool requiresQuestionAndAnswer,
            int maxInvalidPasswordAttempts,
            int passwordAttemptWindowMinutes,
            bool requiresUniqueEmail,
            int passwordFormat,
            int minRequiredPasswordLength,
            int minRequiredNonAlphanumericCharacters,
            String passwordStrengthRegularExpression,
            String defaultEmailFromAddress
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_UpdateExtendedProperties", 12);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@AllowPasswordRetrieval", SqlDbType.Bit, ParameterDirection.Input, allowPasswordRetrieval);
            sph.DefineSqlParameter("@AllowPasswordReset", SqlDbType.Bit, ParameterDirection.Input, allowPasswordReset);
            sph.DefineSqlParameter("@RequiresQuestionAndAnswer", SqlDbType.Bit, ParameterDirection.Input, requiresQuestionAndAnswer);
            sph.DefineSqlParameter("@MaxInvalidPasswordAttempts", SqlDbType.Int, ParameterDirection.Input, maxInvalidPasswordAttempts);
            sph.DefineSqlParameter("@PasswordAttemptWindowMinutes", SqlDbType.Int, ParameterDirection.Input, passwordAttemptWindowMinutes);
            sph.DefineSqlParameter("@RequiresUniqueEmail", SqlDbType.Bit, ParameterDirection.Input, requiresUniqueEmail);
            sph.DefineSqlParameter("@PasswordFormat", SqlDbType.Int, ParameterDirection.Input, passwordFormat);
            sph.DefineSqlParameter("@MinRequiredPasswordLength", SqlDbType.Int, ParameterDirection.Input, minRequiredPasswordLength);
            sph.DefineSqlParameter("@MinRequiredNonAlphanumericCharacters", SqlDbType.Int, ParameterDirection.Input, minRequiredNonAlphanumericCharacters);
            sph.DefineSqlParameter("@PasswordStrengthRegularExpression", SqlDbType.NVarChar, -1, ParameterDirection.Input, passwordStrengthRegularExpression);
            sph.DefineSqlParameter("@DefaultEmailFromAddress", SqlDbType.NVarChar, 100, ParameterDirection.Input, defaultEmailFromAddress);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static bool UpdateRelatedSites(
            int siteId,
            bool allowNewRegistration,
            bool useSecureRegistration,
            bool useLdapAuth,
            bool autoCreateLdapUserOnFirstLogin,
            string ldapServer,
            string ldapDomain,
            int ldapPort,
            string ldapRootDN,
            string ldapUserDNKey,
            bool allowUserFullNameChange,
            bool useEmailForLogin,
            bool allowOpenIdAuth,
            bool allowWindowsLiveAuth,
            bool allowPasswordRetrieval,
            bool allowPasswordReset,
            bool requiresQuestionAndAnswer,
            int maxInvalidPasswordAttempts,
            int passwordAttemptWindowMinutes,
            bool requiresUniqueEmail,
            int passwordFormat,
            int minRequiredPasswordLength,
            int minReqNonAlphaChars,
            string pwdStrengthRegex
            )
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_UpdateRelatedSiteSecurity", 24);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@AllowNewRegistration", SqlDbType.Bit, ParameterDirection.Input, allowNewRegistration);
            sph.DefineSqlParameter("@UseSecureRegistration", SqlDbType.Bit, ParameterDirection.Input, useSecureRegistration);
            sph.DefineSqlParameter("@UseLdapAuth", SqlDbType.Bit, ParameterDirection.Input, useLdapAuth);
            sph.DefineSqlParameter("@AutoCreateLDAPUserOnFirstLogin", SqlDbType.Bit, ParameterDirection.Input, autoCreateLdapUserOnFirstLogin);
            sph.DefineSqlParameter("@LdapServer", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapServer);
            sph.DefineSqlParameter("@LdapDomain", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapDomain);
            sph.DefineSqlParameter("@LdapPort", SqlDbType.Int, ParameterDirection.Input, ldapPort);
            sph.DefineSqlParameter("@LdapRootDN", SqlDbType.NVarChar, 255, ParameterDirection.Input, ldapRootDN);
            sph.DefineSqlParameter("@LdapUserDNKey", SqlDbType.NVarChar, 10, ParameterDirection.Input, ldapUserDNKey);
            sph.DefineSqlParameter("@AllowUserFullNameChange", SqlDbType.Bit, ParameterDirection.Input, allowUserFullNameChange);
            sph.DefineSqlParameter("@UseEmailForLogin", SqlDbType.Bit, ParameterDirection.Input, useEmailForLogin);
            sph.DefineSqlParameter("@AllowOpenIDAuth", SqlDbType.Bit, ParameterDirection.Input, allowOpenIdAuth);
            sph.DefineSqlParameter("@AllowWindowsLiveAuth", SqlDbType.Bit, ParameterDirection.Input, allowWindowsLiveAuth);
            sph.DefineSqlParameter("@AllowPasswordRetrieval", SqlDbType.Bit, ParameterDirection.Input, allowPasswordRetrieval);
            sph.DefineSqlParameter("@AllowPasswordReset", SqlDbType.Bit, ParameterDirection.Input, allowPasswordReset);
            sph.DefineSqlParameter("@RequiresQuestionAndAnswer", SqlDbType.Bit, ParameterDirection.Input, requiresQuestionAndAnswer);
            sph.DefineSqlParameter("@MaxInvalidPasswordAttempts", SqlDbType.Int, ParameterDirection.Input, maxInvalidPasswordAttempts);
            sph.DefineSqlParameter("@PasswordAttemptWindowMinutes", SqlDbType.Int, ParameterDirection.Input, passwordAttemptWindowMinutes);
            sph.DefineSqlParameter("@RequiresUniqueEmail", SqlDbType.Bit, ParameterDirection.Input, requiresUniqueEmail);
            sph.DefineSqlParameter("@PasswordFormat", SqlDbType.Int, ParameterDirection.Input, passwordFormat);
            sph.DefineSqlParameter("@MinRequiredPasswordLength", SqlDbType.Int, ParameterDirection.Input, minRequiredPasswordLength);
            sph.DefineSqlParameter("@MinReqNonAlphaChars", SqlDbType.Int, ParameterDirection.Input, minReqNonAlphaChars);
            sph.DefineSqlParameter("@PwdStrengthRegex", SqlDbType.NVarChar, -1, ParameterDirection.Input, pwdStrengthRegex);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static bool UpdateRelatedSitesWindowsLive(
            int siteId,
            string windowsLiveAppId,
            string windowsLiveKey
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_SyncRelatedSitesWinLive", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@WindowsLiveAppID", SqlDbType.NVarChar, 255, ParameterDirection.Input, windowsLiveAppId);
            sph.DefineSqlParameter("@WindowsLiveKey", SqlDbType.NVarChar, 255, ParameterDirection.Input, windowsLiveKey);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }




        public static bool Delete(int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_Delete", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            // sph.ExecuteNonQuery();

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static IDataReader GetSiteList()
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectAll", 0);
            return sph.ExecuteReader();
        }

        public static IDataReader GetByLinhVuc(int linhVucId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectByLinhVuc", 1);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucId);
            return sph.ExecuteReader();
        }


        public static IDataReader GetListSite()
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectSiteList", 0);
            return sph.ExecuteReader();
        }

        public static IDataReader GetListSiteParent()
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectSiteListParentShort", 0);
            return sph.ExecuteReader();
        }
        public static IDataReader GetListSiteShort()
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectSiteListShort", 0);
            return sph.ExecuteReader();
        }

        public static IDataReader GetSite(string hostName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectOneByHost", 1);
            sph.DefineSqlParameter("@HostName", SqlDbType.NVarChar, 50, ParameterDirection.Input, hostName);
            return sph.ExecuteReader();

        }


        public static void AddFeature(Guid siteGuid, Guid featureGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_SiteModuleDefinitions_Insert", 2);
            sph.DefineSqlParameter("@SiteGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, siteGuid);
            sph.DefineSqlParameter("@FeatureGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, featureGuid);

            sph.ExecuteNonQuery();

        }

        public static void RemoveFeature(Guid siteGuid, Guid featureGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_SiteModuleDefinitions_Delete", 2);
            sph.DefineSqlParameter("@SiteGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, siteGuid);
            sph.DefineSqlParameter("@FeatureGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, featureGuid);
            sph.ExecuteNonQuery();
        }
        public static IDataReader GetAllSiteID()
        {
            return SqlHelper.ExecuteReader(
             ConnectionString.GetReadConnectionString(),
             CommandType.StoredProcedure,
             "mp_Sites_SelectAllSiteID",
             null);
        }

        public static IDataReader GetStaticArticleSite(string siteList, DateTime? startDate, DateTime? endDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectStaticArticleSite", 3);
            sph.DefineSqlParameter("@SiteList", SqlDbType.NVarChar, 500, ParameterDirection.Input, siteList);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            return sph.ExecuteReader();
        }


        public static IDataReader GetSiteName(string sites)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectSiteName", 1);
            sph.DefineSqlParameter("@Sites", SqlDbType.NVarChar, 550, ParameterDirection.Input, sites);
            return sph.ExecuteReader();
        }

        public static IDataReader GetSite(int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectOne", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetSite(Guid siteGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectOneByGuid", 1);
            sph.DefineSqlParameter("@SiteGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, siteGuid);
            return sph.ExecuteReader();
        }



        public static IDataReader GetPageListForAdmin(int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Pages_SelectList", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetHostList(int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_SiteHosts_Select", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }

        public static void AddHost(Guid siteGuid, int siteId, string hostName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_SiteHosts_Insert", 3);
            sph.DefineSqlParameter("@SiteGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, siteGuid);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@HostName", SqlDbType.NVarChar, 255, ParameterDirection.Input, hostName);
            sph.ExecuteNonQuery();
        }

        public static void DeleteHost(int hostId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_SiteHosts_Delete", 1);
            sph.DefineSqlParameter("@HostID", SqlDbType.Int, ParameterDirection.Input, hostId);
            sph.ExecuteNonQuery();
        }


        public static int CountOtherSites(int currentSiteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_CountOtherSites", 1);
            sph.DefineSqlParameter("@CurrentSiteID", SqlDbType.Int, ParameterDirection.Input, currentSiteId);

            return Convert.ToInt32(sph.ExecuteScalar());

        }

        public static IDataReader GetPageOfOtherSites(
            int currentSiteId,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows = CountOtherSites(currentSiteId);

            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectPageOtherSites", 3);
            sph.DefineSqlParameter("@CurrentSiteID", SqlDbType.Int, ParameterDirection.Input, currentSiteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }


        public static int GetSiteIdByHostName(string hostName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_SiteHosts_SelectSiteIdByHost", 1);
            sph.DefineSqlParameter("@HostName", SqlDbType.NVarChar, 255, ParameterDirection.Input, hostName);

            return Convert.ToInt32(sph.ExecuteScalar());

        }

        public static int GetSiteIdByFolder(string folderName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_SiteFolders_SelectSiteIdByFolder", 1);
            sph.DefineSqlParameter("@FolderName", SqlDbType.NVarChar, 255, ParameterDirection.Input, folderName);

            return Convert.ToInt32(sph.ExecuteScalar());

        }

        public static bool HostNameExists(string hostName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_SiteHost_Exists", 1);
            sph.DefineSqlParameter("@HostName", SqlDbType.NVarChar, 255, ParameterDirection.Input, hostName);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
        }
        public static bool UpdateView(int siteId, int isView)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Sites_UpdateView", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@IsView", SqlDbType.Int, ParameterDirection.Input, isView);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static IDataReader GetListTemplate()
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectAll_Template", 0);
            return sph.ExecuteReader();
        }
        public static IDataReader GetListByParent(int parentId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_SelectByParent", 1);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetName(int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Sites_GetName", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }

    }
}

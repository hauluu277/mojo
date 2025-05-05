using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using Resources;
/// Author:					
/// Created:				2007-04-29
/// Last Modified:			2013-10-10
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Text;

namespace mojoPortal.Web.AdminUI
{

    public partial class UserMenu : NonCmsBasePage
    {
        private bool IsAdminOrContentAdmin = false;
        private bool IsAdmin = false;
        private bool isSiteEditor = false;
        private bool isCommerceReportViewer = false;
        private CommerceConfiguration commerceConfig = null;
        SecurityAdvisor securityAdvisor = new SecurityAdvisor();


        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this); return;
            }

            SecurityHelper.DisableBrowserCache();

            PopulateLabels();
            PopulateControls();


        }

        private void PopulateControls()
        {
            BuildAdditionalMenuListItems();

        }

        private void BuildAdditionalMenuListItems()
        {
            if (siteSettings == null) return;

            ContentAdminLinksConfiguration linksConfig
                = ContentAdminLinksConfiguration.GetConfig(siteSettings.SiteId);

            StringBuilder addedLinks = new StringBuilder();
            foreach (ContentAdminLink link in linksConfig.AdminLinks)
            {
                if (
                    (link.VisibleToRoles.Length == 0)
                    || (WebUser.IsInRoles(link.VisibleToRoles))
                    )
                {
                    addedLinks.Append("\n<li>");
                    addedLinks.Append("<a ");
                    string title = ResourceHelper.GetResourceString(link.ResourceFile, link.ResourceKey);
                    addedLinks.Append("title='" + title + "' ");
                    addedLinks.Append("class='" + link.CssClass + "' ");
                    string url = link.Url;
                    if (url.StartsWith("~/"))
                    {
                        url = SiteRoot + "/" + url.Replace("~/", string.Empty);
                    }
                    addedLinks.Append("href='" + url + "'>" + title + "</a>");
                    addedLinks.Append("</li>");
                }

            }

            litSupplementalLinks.Text = addedLinks.ToString();
        }


        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, Resource.AdminMenuHeading);

            heading.Text = Resource.AdminMenuHeading;


            //Quản trị tin bài
            ArticleMenu.Visible = (WebUser.IsInRoles(WebConfigSettings.RolePublishedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleApprovedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleManageArticle));
            lnkArticleMenu.Text = Resource.ManageArticleTitle;
            lnkArticleMenu.NavigateUrl = SiteRoot + "/Admin/Article/ArticleMenu.aspx";

            liCategoryManager.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageCategory);
            lnkCategoryManager.Text = "Quản lý danh mục";
            lnkCategoryManager.NavigateUrl = SiteRoot + "/Admin/CategoryManager.aspx";

            liPageModuleManager.Visible = (IsAdminOrContentAdmin || isSiteEditor);
            lnkPageModuleManager.Text = Resource.AdminMenuPageModuleManagerLink;
            lnkPageModuleManager.NavigateUrl = SiteRoot + "/Admin/PageModuleManager.aspx?siteid=" + siteSettings.SiteId;



            lnkSettingWebBuilder.Visible = IsAdmin;
            lnkSettingWebBuilder.Text = "Xây dựng website";
            lnkSettingWebBuilder.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderMenu.aspx";



            liDictionary.Visible = (WebUser.IsInRoles(WebConfigSettings.RolePublishedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleApprovedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleManageArticle));
            lnkDictionaryManager.Text = "Quản trị tag bài viết";
            lnkDictionaryManager.NavigateUrl = SiteRoot + "/admin/tagarticle/articletag.aspx";



            liSiteSettings.Visible = IsAdminOrContentAdmin || isSiteEditor;
            lnkSiteSettings.Text = Resource.AdminMenuSiteSettingsLink;
            lnkSiteSettings.NavigateUrl = SiteRoot + "/Admin/SiteSettings.aspx";

            liSiteList.Visible = ((WebConfigSettings.AllowMultipleSites) && (siteSettings.IsServerAdminSite) && IsAdmin);
            lnkSiteList.Text = Resource.SiteList;
            lnkSiteList.NavigateUrl = SiteRoot + "/Admin/SiteList.aspx";

            lnkSecurityAdvisor.Text = Resource.SecurityAdvisor;
            lnkSecurityAdvisor.NavigateUrl = SiteRoot + "/Admin/SecurityAdvisor.aspx";
            if (IsAdmin && siteSettings.IsServerAdminSite)
            {
                liSecurityAdvisor.Visible = true;
                if (!securityAdvisor.UsingCustomMachineKey())
                {
                    imgMachineKeyDanger.Visible = true;
                    lblNeedsAttantion.Visible = true;
                    lnkSecurityAdvisor.CssClass = "lnkSecurityAdvisorWarning";
                }

            }



            imgMachineKeyDanger.ImageUrl = "~/Data/SiteImages/warning.png";
            imgMachineKeyDanger.AlternateText = Resource.SecurityDangerLabel;


            liCommerceReports.Visible = (isCommerceReportViewer && (commerceConfig != null) && (commerceConfig.IsConfigured));
            lnkCommerceReports.Text = Resource.CommerceReportsLink;
            lnkCommerceReports.NavigateUrl = SiteRoot + "/Admin/SalesSummary.aspx";

            liContentManager.Visible = (IsAdminOrContentAdmin || isSiteEditor);
            lnkContentManager.Text = Resource.AdminMenuContentManagerLink;
            lnkContentManager.NavigateUrl = SiteRoot + "/Admin/ContentCatalog.aspx";

            liContentWorkFlow.Visible = (WebConfigSettings.EnableContentWorkflow && siteSettings.EnableContentWorkflow);
            lnkContentWorkFlow.Visible = siteSettings.EnableContentWorkflow && WebUser.IsAdminOrContentAdminOrContentPublisher;
            lnkContentWorkFlow.Text = Resource.AdminMenuContentWorkflowLabel;
            lnkContentWorkFlow.NavigateUrl = SiteRoot + "/Admin/ContentWorkflow.aspx";

            liContentTemplates.Visible = (IsAdminOrContentAdmin || isSiteEditor || (WebUser.IsInRoles(siteSettings.RolesThatCanEditContentTemplates)));
            lnkContentTemplates.Text = Resource.ContentTemplates;
            lnkContentTemplates.NavigateUrl = SiteRoot + "/Admin/ContentTemplates.aspx";

            liStyleTemplates.Visible = (IsAdminOrContentAdmin || isSiteEditor || (WebUser.IsInRoles(siteSettings.RolesThatCanEditContentTemplates)));
            lnkStyleTemplates.Text = Resource.ContentStyleTemplates;
            lnkStyleTemplates.NavigateUrl = SiteRoot + "/Admin/ContentStyles.aspx";



            liPageTree.Visible = (IsAdminOrContentAdmin || isSiteEditor || (SiteMapHelper.UserHasAnyCreatePagePermissions(siteSettings)));
            lnkPageTree.Text = Resource.AdminMenuPageTreeLink;
            lnkPageTree.NavigateUrl = SiteRoot + WebConfigSettings.PageTreeRelativeUrl;

            liRoleAdmin.Visible = (siteSettings.SiteId == 1 && (WebUser.IsRoleAdmin || IsAdmin));
            lnkRoleAdmin.Text = "Quản lý vai trò";
            lnkRoleAdmin.NavigateUrl = SiteRoot + "/Admin/RoleManager.aspx";

            liPermissions.Visible = IsAdmin;
            lnkPermissionAdmin.Text = Resource.SiteSettingsPermissionsTab;
            lnkPermissionAdmin.NavigateUrl = SiteRoot + "/Admin/PermissionsMenu.aspx";

            //biểu mẫu thông tin
            liBieuMauThongTin.Visible = WebUser.IsInRoles(WebConfigSettings.RoleBieuMauThongTin);
            lnkBieuMauThongTin.Text = "Biểu mẫu thông tin";
            lnkBieuMauThongTin.NavigateUrl = SiteRoot + "/GlobalModule/bieumauthongtin/manage.aspx";


            //quản lý thông tin kê khai
            liKeKhaiThongTin.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageKeKhaiThongTin);
            hplKeKhaiThongTin.Text = "Quản lý kê khai thông tin";
            hplKeKhaiThongTin.NavigateUrl = SiteRoot + "/GlobalModule/bieumauthongtin/listkekhai.aspx";


            //quản lý giao diện
            liQuanLyGiaoDien.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageGiaoDien);
            hplQuanLyGiaoDien.Text = "Quản lý giao diện";
            hplQuanLyGiaoDien.NavigateUrl = SiteRoot + "/globalmodule/giaodien/managegiaodien.aspx";

            //quản lý tin bài từ cổng thành viên
            liQuanLyGiaoDien.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageArticleCongThanhVien);
            hplQuanLyTinCongThanhVien.Text = "Quản lý tin từ cổng thành viên";
            hplQuanLyTinCongThanhVien.NavigateUrl = SiteRoot + "/Article/ManageArticleCTV.aspx";

            //quản lý thư viện ảnh
            liGallery.Visible = WebUser.IsInRoles(WebConfigSettings.RoleGalleryManage);
            hplGallery.Text = "Thư viện ảnh";
            hplGallery.NavigateUrl = SiteRoot + "/Media/managepostCategories.aspx";


            //quản lý thư viện video
            liVideo.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageVideo);
            hplVideo.Text = "Thư viện video";
            hplVideo.NavigateUrl = SiteRoot + "/VideoIntroduce/ManagePost.aspx";

            //quản lý thư viện audio
            liAudio.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageAudio);
            hplAudio.Text = "Thư viện audio";
            hplAudio.NavigateUrl = SiteRoot + "/Audio/ManageAudio.aspx";

            //quản lý hỏi đáp
            liHoiDap.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageHoiDap);
            hplHoiDap.Text = "Hỏi đáp";
            hplHoiDap.NavigateUrl = SiteRoot + "/QuestionAnswers/managepost.aspx?pageid=4340&mid=12204";

            //quản lý công khai ngân sách
            liCongKhaiNganSach.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageCongKhaiNganSach);
            hplCongKhaiNganSach.Text = "Công khai ngân sách";
            hplCongKhaiNganSach.NavigateUrl = SiteRoot + "/GlobalModule/Report/ManageReport.aspx";

            //quản lý thủ tục hành chính
            liThuTucHanhChinh.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageThuTucHanhChinh);
            hplThuTucHanhChinh.Text = "Thủ tục hành chính";
            hplThuTucHanhChinh.NavigateUrl = SiteRoot + "/GlobalModule/thutuchanhchinh/ManageThuTuc.aspx";

            //quản lý lịch công tác
            liLichCongTac.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageLichCongTac);
            hplLichCongTac.Text = "Lịch công tác";
            hplLichCongTac.NavigateUrl = SiteRoot + "/GlobalModule/thutuchanhchinh/ManageThuTuc.aspx";

            //quản lý văn bản
            liQuanLyVanBan.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageDocument);
            hplQuanLyVanBan.Text = "Văn bản";
            hplQuanLyVanBan.NavigateUrl = SiteRoot + "/document/managepost.aspx?pageid=3986&mid=9373";

            //quản lý cổng thành viên
            liCongThanhVien.Visible = WebUser.IsInRoles(WebConfigSettings.RoleCongThanhVien);
            hplCongThanhVien.Text = "Cổng thành viên";
            hplCongThanhVien.NavigateUrl = SiteRoot + "/GlobalModule/SSO/ManageClient.aspx";

            //quản lý dự thảo văn bản
            liDuThaoVanBan.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageDuThaoVanBan);
            hplDuThaoVanBan.Text = "Dự thảo văn bản";
            hplDuThaoVanBan.NavigateUrl = SiteRoot + "/duthaovanban/managepost.aspx?pageid=4339&mid=12203";

            //quản lý nhật ký
            liNhatky.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageNhatKy);
            hplNhatKy.Text = "Dự thảo văn bản";
            hplNhatKy.NavigateUrl = SiteRoot + "/GlobalModule/QLLog/ManageLog.aspx";

            //hoạt động người dùng
            liHoatDongNguoiDung.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageHoatDongNguoiDung);
            hplHoatDongNguoiDung.Text = "Hoạt động người dùng";
            hplHoatDongNguoiDung.NavigateUrl = SiteRoot + "/logsystem/manage.aspx";


            if ((WebConfigSettings.UseRelatedSiteMode) && (WebConfigSettings.RelatedSiteModeHideRoleManagerInChildSites))
            {
                if (siteSettings.SiteId != WebConfigSettings.RelatedSiteID)
                {
                    liRoleAdmin.Visible = false;
                }
            }


            //
            // File Manager Link
            //
            //liFileManager.Visible = IsAdminOrContentAdmin;

            //if ((!siteSettings.IsServerAdminSite) && (!WebConfigSettings.AllowFileManagerInChildSites))
            //{
            //	liFileManager.Visible = false;
            //}

            //if (WebConfigSettings.DisableFileManager)
            //{
            //	liFileManager.Visible = false;
            //}

            //lnkFileManager.Text = Resource.AdminMenuFileManagerLink;
            //lnkFileManager.NavigateUrl = SiteRoot + "/FileManager";


            //
            // Member List
            //
            lnkMemberList.Text = Resource.MemberListLink;
            lnkMemberList.NavigateUrl = SiteRoot + WebConfigSettings.MemberListUrl;
            lnkMemberList.Visible = ((WebUser.IsInRoles(siteSettings.RolesThatCanViewMemberList)) || (WebUser.IsInRoles(siteSettings.RolesThatCanLookupUsers)));

            liAddUser.Visible = ((WebUser.IsInRoles(siteSettings.RolesThatCanCreateUsers)) || (WebUser.IsInRoles(siteSettings.RolesThatCanManageUsers)) || (WebUser.IsInRoles(siteSettings.RolesThatCanLookupUsers)));
            lnkAddUser.Text = Resource.MemberListAddUserLabel;
            lnkAddUser.NavigateUrl = SiteRoot + "/Admin/ManageUsers.aspx?userId=-1";

            if (WebConfigSettings.EnableNewsletter)
            {
                liNewsletter.Visible = (IsAdmin || isSiteEditor || WebUser.IsNewsletterAdmin);
                lnkNewsletter.Text = Resource.AdminMenuNewsletterAdminLabel;
                lnkNewsletter.NavigateUrl = SiteRoot + "/eletter/Admin.aspx";

                //liTaskQueue.Visible = IsAdmin || WebUser.IsNewsletterAdmin;
                //lnkTaskQueue.Text = Resource.TaskQueueMonitorHeading;
                //lnkTaskQueue.NavigateUrl = SiteRoot + "/Admin/TaskQueueMonitor.aspx";

            }
            else
            {
                liNewsletter.Visible = false;
                //liTaskQueue.Visible = false;
            }

            liRegistrationAgreement.Visible = (IsAdminOrContentAdmin);
            lnkRegistrationAgreement.Text = Resource.RegistrationAgreementLink;
            lnkRegistrationAgreement.NavigateUrl = SiteRoot + "/Admin/EditRegistrationAgreement.aspx";

            liLoginInfo.Visible = (IsAdminOrContentAdmin) && !WebConfigSettings.DisableLoginInfo;
            lnkLoginInfo.Text = Resource.LoginPageContent;
            lnkLoginInfo.NavigateUrl = SiteRoot + "/Admin/EditLoginInfo.aspx";



            liCoreData.Visible = (IsAdminOrContentAdmin && siteSettings.IsServerAdminSite);
            lnkCoreData.Text = Resource.CoreDataAdministrationLink;
            lnkCoreData.NavigateUrl = SiteRoot + "/Admin/CoreData.aspx";

            liAdvancedTools.Visible = (IsAdminOrContentAdmin || isSiteEditor);
            lnkAdvancedTools.Text = Resource.AdvancedToolsLink;
            lnkAdvancedTools.NavigateUrl = SiteRoot + "/Admin/AdvancedTools.aspx";


            liServerInfo.Visible = (IsAdminOrContentAdmin || isSiteEditor) && (siteSettings.IsServerAdminSite || WebConfigSettings.ShowSystemInformationInChildSiteAdminMenu);
            lnkServerInfo.Text = Resource.AdminMenuServerInfoLabel;
            lnkServerInfo.NavigateUrl = SiteRoot + "/Admin/ServerInformation.aspx";

            liLogViewer.Visible = IsAdmin && siteSettings.IsServerAdminSite && WebConfigSettings.EnableLogViewer;
            lnkLogViewer.Text = Resource.AdminMenuServerLogLabel;
            lnkLogViewer.NavigateUrl = SiteRoot + "/Admin/ServerLog.aspx";

        }




        private void LoadSettings()
        {
            IsAdminOrContentAdmin = WebUser.IsAdminOrContentAdmin;
            IsAdmin = WebUser.IsAdmin;
            isSiteEditor = SiteUtils.UserIsSiteEditor();
            isCommerceReportViewer = WebUser.IsInRoles(siteSettings.CommerceReportViewRoles);
            commerceConfig = SiteUtils.GetCommerceConfig();
            AddClassToBody("administration");
            AddClassToBody("adminmenu");
        }


        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Load += new EventHandler(this.Page_Load);

            SuppressMenuSelection();
            SuppressPageMenu();



        }

        #endregion
    }
}
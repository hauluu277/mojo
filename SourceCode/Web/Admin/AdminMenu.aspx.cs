using Microsoft.Ajax.Utilities;
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

    public partial class AdminMenuPage : NonCmsBasePage
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
            //if (
            //    (!WebUser.IsAdminOrContentAdminOrRoleAdminOrNewsletterAdmin)
            //    && (!isSiteEditor)
            //    && (!isCommerceReportViewer)
            //    )
            //{
            //    WebUtils.SetupRedirect(this, SiteRoot + "/AccessDenied.aspx");
            //    return;
            //}

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


            //Quản trị hệ thống
            LiQuanTriHeThong.Visible = IsAdmin;
            hplQuanTriHeThong.Text = "Quản trị hệ thống";
            hplQuanTriHeThong.NavigateUrl = SiteRoot + "/Admin/SystemMenu.aspx";

            //Quản trị tin bài
            ArticleMenu.Visible = (WebUser.IsInRoles(WebConfigSettings.RolePublishedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleApprovedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleManageArticle));
            lnkArticleMenu.Text = Resource.ManageArticleTitle;
            lnkArticleMenu.NavigateUrl = SiteRoot + "/Admin/Article/ArticleMenu.aspx";

            liCategoryManager.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageCategory);
            lnkCategoryManager.Text = "Quản lý danh mục";
            lnkCategoryManager.NavigateUrl = SiteRoot + "/Admin/CategoryManager.aspx";


            //Quản trị tin 
            liPageModuleManager.Visible = (IsAdminOrContentAdmin || isSiteEditor);
            lnkPageModuleManager.Text = Resource.AdminMenuPageModuleManagerLink;
            lnkPageModuleManager.NavigateUrl = SiteRoot + "/Admin/PageModuleManager.aspx?siteid=" + siteSettings.SiteId;


            //liPageModuleManager.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageCategory);
            //lnkPageModuleManager.Text = "Quản lý tập tin ";
            //lnkPageModuleManager.NavigateUrl = SiteRoot + "/Admin/PageModuleManager.aspx";




            //liSiteList.Visible = ((WebConfigSettings.AllowMultipleSites) && (siteSettings.IsServerAdminSite) && IsAdmin);


            lnkSecurityAdvisor.Text = Resource.SecurityAdvisor;
            lnkSecurityAdvisor.NavigateUrl = SiteRoot + "/Admin/SecurityAdvisor.aspx";
            lnkSecurityAdvisor.Visible = false;
            //if (IsAdmin && siteSettings.IsServerAdminSite)
            //{
            //    liSecurityAdvisor.Visible = true;
            //    if (!securityAdvisor.UsingCustomMachineKey())
            //    {
            //        imgMachineKeyDanger.Visible = true;
            //        lblNeedsAttantion.Visible = true;
            //        lnkSecurityAdvisor.CssClass = "lnkSecurityAdvisorWarning";
            //    }

            //}



            imgMachineKeyDanger.ImageUrl = "~/Data/SiteImages/warning.png";
            imgMachineKeyDanger.AlternateText = Resource.SecurityDangerLabel;


            liCommerceReports.Visible = (isCommerceReportViewer && (commerceConfig != null) && (commerceConfig.IsConfigured));
            lnkCommerceReports.Text = Resource.CommerceReportsLink;
            lnkCommerceReports.NavigateUrl = SiteRoot + "/Admin/SalesSummary.aspx";



            liContentWorkFlow.Visible = (WebConfigSettings.EnableContentWorkflow && siteSettings.EnableContentWorkflow);
            lnkContentWorkFlow.Visible = siteSettings.EnableContentWorkflow && WebUser.IsAdminOrContentAdminOrContentPublisher;
            lnkContentWorkFlow.Text = Resource.AdminMenuContentWorkflowLabel;
            lnkContentWorkFlow.NavigateUrl = SiteRoot + "/Admin/ContentWorkflow.aspx";

            //liContentTemplates.Visible = (IsAdminOrContentAdmin || isSiteEditor || (WebUser.IsInRoles(siteSettings.RolesThatCanEditContentTemplates)));
            liContentTemplates.Visible = false;
            lnkContentTemplates.Text = Resource.ContentTemplates;
            lnkContentTemplates.NavigateUrl = SiteRoot + "/Admin/ContentTemplates.aspx";






            //liStyleTemplates.Visible = (IsAdminOrContentAdmin || isSiteEditor || (WebUser.IsInRoles(siteSettings.RolesThatCanEditContentTemplates)));
            liStyleTemplates.Visible = false;
            lnkStyleTemplates.Text = Resource.ContentStyleTemplates;
            lnkStyleTemplates.NavigateUrl = SiteRoot + "/Admin/ContentStyles.aspx";





            //biểu mẫu thông tin
            liBieuMauThongTin.Visible = WebUser.IsInRoles(WebConfigSettings.RoleBieuMauThongTin);
            lnkBieuMauThongTin.Text = "Biểu mẫu thông tin";
            lnkBieuMauThongTin.NavigateUrl = SiteRoot + "/GlobalModule/bieumauthongtin/manage.aspx";


            //quản lý thông tin kê khai
            liKeKhaiThongTin.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageKeKhaiThongTin);
            hplKeKhaiThongTin.Text = "Quản lý kê khai thông tin";
            hplKeKhaiThongTin.NavigateUrl = SiteRoot + "/GlobalModule/bieumauthongtin/listkekhai.aspx";

            //quản lý chuyên mục liên kết
            liChuyenMucLienKet.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageDanhMuc);
            hplChuyenMucLienKet.Text = "Quản lý chuyên mục liên kết";
            hplChuyenMucLienKet.NavigateUrl = SiteRoot + "/GlobalModule/danhmuc/managedanhmuc.aspx";


            //quản lý giao diện
            liQuanLyGiaoDien.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageGiaoDien);
            hplQuanLyGiaoDien.Text = "Quản lý giao diện";
            hplQuanLyGiaoDien.NavigateUrl = SiteRoot + "/globalmodule/giaodien/managegiaodien.aspx";


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
            hplLichCongTac.NavigateUrl = SiteRoot + "/LichCongTac/ManagePostNew.aspx";

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
            hplNhatKy.Text = "Nhật ký log";
            hplNhatKy.NavigateUrl = SiteRoot + "/GlobalModule/QLLog/ManageLog.aspx";

            //quản lý liên hệ
            liLienHe.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageLienHe);
            hplLienHe.Text = "Quản lý liên hệ";
            hplLienHe.NavigateUrl = SiteRoot + "/GlobalModule/QLLienHe/ManageLienHe.aspx";



            // file 
            //lnkFileManager.Text = "Quản trị tệp tin";
            //lnkFileManager.NavigateUrl = SiteRoot + "/Admin/RoleManager.aspx";


            //File Manager Link
            lnkFileManager.Visible = IsAdminOrContentAdmin;
            liFileManager.Visible = IsAdminOrContentAdmin;
            if ((!siteSettings.IsServerAdminSite) && (!WebConfigSettings.AllowFileManagerInChildSites))
            {
                liFileManager.Visible = false;
            }
            if (WebConfigSettings.DisableFileManager)
            {
                liFileManager.Visible = false;
            }
            lnkFileManager.Text = Resource.AdminMenuFileManagerLink;
            lnkFileManager.NavigateUrl = SiteRoot + "/filemanager";





            if (WebConfigSettings.EnableNewsletter)
            {
                //liNewsletter.Visible = (IsAdmin || isSiteEditor || WebUser.IsNewsletterAdmin);
                liNewsletter.Visible = false;
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

            //liRegistrationAgreement.Visible = (IsAdminOrContentAdmin);
            liRegistrationAgreement.Visible = false;
            lnkRegistrationAgreement.Text = Resource.RegistrationAgreementLink;
            lnkRegistrationAgreement.NavigateUrl = SiteRoot + "/Admin/EditRegistrationAgreement.aspx";

            //liLoginInfo.Visible = (IsAdminOrContentAdmin) && !WebConfigSettings.DisableLoginInfo;
            liLoginInfo.Visible = false;
            lnkLoginInfo.Text = Resource.LoginPageContent;
            lnkLoginInfo.NavigateUrl = SiteRoot + "/Admin/EditLoginInfo.aspx";



            //liCoreData.Visible = (IsAdminOrContentAdmin && siteSettings.IsServerAdminSite);
            liCoreData.Visible = false;
            lnkCoreData.Text = Resource.CoreDataAdministrationLink;
            lnkCoreData.NavigateUrl = SiteRoot + "/Admin/CoreData.aspx";

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
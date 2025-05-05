/// Author:					Joe Audette
/// Created:				2008-06-22
/// Last Modified:			2011-03-21
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Admin.Article;
using Resources;

namespace mojoPortal.Web.AdminUI
{

    public partial class ArticleMenu : NonCmsBasePage
    {
        private bool isAdmin = false;
        private bool isContentAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();

            if (!Request.IsAuthenticated && ArticleHelper.HasRoleArticle() == false)
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }


            PopulateLabels();
            PopulateControls();

        }

        private void PopulateControls()
        {


        }


        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, Resource.ManageArticleTitle);

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkCurrentPage.Text = Resource.ManageArticleTitle;
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/Article/ArticleMenu.aspx";

            heading.Text = Resource.ManageArticleTitle;
            //Quản trị tin bài
            lnkListArticle.Visible = ArticleHelper.HasRoleArticle();
            lnkListArticle.Text = "Quản trị tin bài";
            lnkListArticle.NavigateUrl = SiteRoot + "/Article/ManageArticle.aspx";

            //Thông kê tin bài
            lnkStatisticalArticle.Visible = WebUser.IsInRoles(WebConfigSettings.RoleThongKeArticle);
            lnkStatisticalArticle.Text = Resource.StatisticalArticleTitle;
            lnkStatisticalArticle.NavigateUrl = SiteRoot + "/ArticleStatistic/ManageStatistic.aspx";

            //Lịch sử tin bài
            lnkHistoryArticle.Visible = siteSettings.IsServerAdminSite;
            lnkHistoryArticle.Visible = false;
            lnkHistoryArticle.Text = Resource.HistoryArticleTitle;
            lnkHistoryArticle.NavigateUrl = SiteRoot + "/Admin/HistoryArticle.aspx";

            //Bình luận tin bài

            liCommentArticle.Visible = WebUser.IsInRoles(WebConfigSettings.RoleCommentArticle);
            hplCommentArticle.Text = "Bình luận tin bài";
            hplCommentArticle.NavigateUrl = SiteRoot + "/ArticleComment/ManageArticleComment.aspx";

            // Quản trị chuyên mục tin
            liCategoryArticle.Visible = ArticleHelper.HasRoleArticle();
            hplCategoryArticle.Text = "Quản trị chuyên mục tin";
            hplCategoryArticle.NavigateUrl = SiteRoot + "/Admin/CategoryArticleManage.aspx";

            //Quản trị tin cổng thành viên
            liArticleCongThanhVien.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageArticleCongThanhVien);
            hplArticleCongThanhVien.Text = "Quản trị tin cổng thành viên";
            hplArticleCongThanhVien.NavigateUrl = SiteRoot + "/Article/ManageArticleCTV.aspx";

            //Báo cáo tin bài
            liBaoCaoTinBai.Visible= ArticleHelper.HasRoleArticle();
            hplBaoCaoTinBai.Text = "Báo cáo tin bài";
            hplBaoCaoTinBai.NavigateUrl = SiteRoot + "/ArticleStatistic/ManageStatisticDanhMuc.aspx";

            //Thống kê bài viết
            liThongKeBaiViet.Visible = ArticleHelper.HasRoleArticle();
            hplThongKeBaiViet.Text = "Thống kê bài viết";
            hplThongKeBaiViet.NavigateUrl = SiteRoot + "/ArticleStatistic/ManageStatisticThongKeDonVi.aspx";


            //tag tin bài
            liDictionary.Visible = ArticleHelper.HasRoleArticle();
            lnkDictionaryManager.Text = "Tag tin bài";
            lnkDictionaryManager.NavigateUrl = SiteRoot + "/admin/tagarticle/articletag.aspx";

        }

        private void LoadSettings()
        {
            isAdmin = WebUser.IsAdmin;
            isContentAdmin = WebUser.IsContentAdmin;

            AddClassToBody("administration");
            AddClassToBody("coredata");
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
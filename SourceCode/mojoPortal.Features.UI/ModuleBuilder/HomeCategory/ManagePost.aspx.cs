using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomeCategoryFeature.UI
{
    public partial class ManagePost : mojoBasePage
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private int totalPages = 1;
        private int pageNumber = 1;
        private int pageSize = 10;
        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            Load += Page_Load;
            base.OnInit(e);
            LoadSettings();
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var homeCategory = new HomeCategory();
            if (!string.IsNullOrEmpty(hdfCategoryID.Value))
            {
                homeCategory = new HomeCategory(int.Parse(hdfCategoryID.Value));
            }
            homeCategory.Description = txtDescription.Text;
            homeCategory.IsPublish = true;
            homeCategory.ItemIcon = txtItemIcon.Text;
            homeCategory.ItemUrl = txtUrl.Text;
            homeCategory.ModuleID = moduleId;
            if (!string.IsNullOrEmpty(txtOrderBy.Text))
            {
                int order = -1;
                int.TryParse(txtOrderBy.Text, out order);
                homeCategory.OrderBy = order;
            }
            homeCategory.SiteID = SiteId;
            homeCategory.Title = txtTitle.Text;
            homeCategory.Save();
            Response.Redirect(Request.RawUrl);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();


            if (!userCanEdit)
            {
                SiteUtils.RedirectToEditAccessDeniedPage();
            }

            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }
        [WebMethod]
        [HttpPost]
        public static void Delete(int id)
        {
            HomeCategory.Delete(id);
        }
        private void PopulateControls()
        {
            LoadCategory();
        }
        private void LoadCategory()
        {
            var listCategory = HomeCategory.GetPage(SiteId, moduleId, pageNumber, pageSize, out totalPages);
            rptCategory.DataSource = listCategory;
            rptCategory.DataBind();

            string pageUrl =
            SiteRoot + "/ModuleBuilder/HomeCategory/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId
           + "&pagenumber={0}";
            pgrCategory.PageURLFormat = pageUrl;
            pgrCategory.ShowFirstLast = true;
            pgrCategory.PageSize = 10;
            pgrCategory.PageCount = totalPages;
            pgrCategory.CurrentIndex = pageNumber;
            pnlCategoryPager.Visible = (totalPages > 1);
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản trị lĩnh vực hiển thị");
            heading.Text = "Danh sách lĩnh vực";

            //lnkHome.Text = "Home";
            //lnkHome.NavigateUrl = SiteRoot + "/";
            //lnkCurrentPage.Text = "Danh sách lĩnh vực";

            UIHelper.DisableButtonAfterClick(
            btnSave,
            ArticleResources.ButtonDisabledPleaseWait,
            Page.ClientScript.GetPostBackEventReference(btnSave, string.Empty)
            );
        }
        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
        }
    }
}
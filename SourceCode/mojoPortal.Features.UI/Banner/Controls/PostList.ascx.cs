using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using mojoPortal.Features;
using log4net;
using mojoPortal.Business;
using Resources;
using mojoPortal.Business.WebHelpers;

namespace BannerFeature.UI
{

    public partial class PostList : UserControl
    {
        // FeatureGuid 34fd6909-6fa5-4dcc-b338-dcce0dbc41d0
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int pageNumber = 1;
        private int totalPages = 1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private bool? isPublic = null;
        private string keyword = string.Empty;
        private int status = -1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        private BannerConfiguration config = new BannerConfiguration();
        private SiteSettings siteSettings;
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        public BannerConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }

        public bool IsEditable { get; set; }
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);
            btnSearch.Click += btnSearch_Click;
            btnaddnew.Click += btnaddnew_Click;
            btnDelAll.Click += btnDelAll_Click;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateControls()
        {
            BindBanner();
            PopulateStatus();
            if (status >= 0)
            {
                ddlStatus.SelectedValue = status.ToString();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }

        }
        private void PopulateLabels()
        {
            legendSearchProperty.InnerText = ArticleResources.ArticleEditSearchPropertyLabel;
            btnSearch.Text = ArticleResources.ArticleSearchButton;
            btnDelAll.BackColor = System.Drawing.Color.Red;
            btnDelAll.Text = ArticleResources.ButtonDeleteAll;
            btnaddnew.Text = ArticleResources.ButtonAddNew;
            //btnDelAll.OnClientClick = "return confirm(Bạn có chắc chắn muốn xóa những baner đã chọn);";

            SiteUtils.AddConfirmButton(btnDelAll, "Bạn có chắc chắn muốn xóa những baner đã chọn");
            //ToDo?
            //ValidateDeleteAll();
            UIHelper.DisableButtonAfterClick(
                btnSearch,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty)
                );
            //UIHelper.DisableButtonAfterClick(
            //    btnDelAll,
            //    ArticleResources.ButtonDisabledPleaseWait,
            //    Page.ClientScript.GetPostBackEventReference(btnDelAll, string.Empty)
            //    );
        }
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new BannerConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            status = WebUtils.ParseInt32FromQueryString("status", -1);
            if (status == 1)
            {
                isPublic = true;
            }
            else if (status == 0)
            {
                isPublic = false;
            }
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);


        }
        private void PopulateStatus()
        {
            //var status = SiteUtils.StringToDictionary(BannerResources.BannerStatus.ToString(), ",");
            var status = SiteUtils.StringToDictionary(ArticleResources.ArticleStatus.ToString(), ",");
            ddlStatus.DataSource = status;
            ddlStatus.DataTextField = "Value";
            ddlStatus.DataValueField = "Key";
            ddlStatus.DataBind();
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
        }
        private void BindBanner()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;

            //List<Banner> banner = Banner.GetAll();
            List<Banner> banner = Banner.GetPage(siteSettings.SiteId, moduleId, pageNumber, config.PageSize, isPublic, keyword.ConvertToVN(), out totalPages);
            rptBanner.DataSource = banner;
            rptBanner.DataBind();
            string pageUrl = SiteRoot + "/banner/managepost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&status=" + status.ToInvariantString()
                   + "&keyword=" + keyword
                   + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
        }
        public string BuildFlashObject(bool type, string filepath)
        {
            if (type == false)
            {
                //string obj_format = "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0' width='200' height='200'><param name='movie' value='~/Data/Images/Banner/{0}' /><embed width='200' height='200' src='~/Data/Images/Banner/{0}' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer'></embed></object>";
                string obj_format = "<embed width='150' height='120' align='middle' quality='high' wmode='opaque' allowscriptaccess='always' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' src='{0}/Data/Images/Banner/{1}'></embed>";
                //<object data='<%#"~/Data/Images/Banner/"+Eval("FilePath") %>'></object>
                return string.Format(obj_format, SiteRoot, filepath);
            }
            return string.Empty;
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            status = int.Parse(ddlStatus.SelectedValue);
            keyword = txtKeyword.Text;
            string pageUrl = SiteRoot + "/banner/managepost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&status=" + status.ToInvariantString()
                    + "&keyword=" + keyword
                    + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }

        protected void rptBanner_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/banner/editpost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&item=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    Banner.Delete(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/banner/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
                else if (e.CommandName.Equals("ApproveItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    Banner.ChangeIsPublic(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/banner/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
            }
        }
        protected void rptBanner_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, "Bạn có chắc chắn muốn xóa mục này");
                ImageButton ibtnApprove = e.Item.FindControl("ibtnApprove") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnApprove, "Bạn có chắc chắn muốn thay đổi trạng thái?");
            }
        }
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/banner/editpost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());

        }

        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            foreach (RepeaterItem ri in rptBanner.Items)
            {
                CheckBox chkFlag = ri.FindControl("chk") as CheckBox;
                if (chkFlag.Checked)
                {
                    deleteNumber++;
                    int itemid = Convert.ToInt32((ri.FindControl("repeaterID") as Literal).Text);
                    Banner.Delete(itemid);
                }
            }
            WebUtils.SetupRedirect(this, SiteRoot + "/banner/managepost.aspx?pageid="
                                    + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
        }
    }
}




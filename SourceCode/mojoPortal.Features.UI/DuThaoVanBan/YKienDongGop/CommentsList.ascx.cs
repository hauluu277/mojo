using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DuThaoVanBanFeature.UI
{
    public partial class CommentsList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected DuThaoVanBanConfiguration config = new DuThaoVanBanConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int duThaoId = 1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int isApprove = -1;
        private int isPublic = -1;
        private string keyword = string.Empty;
        private int valueStatus = -1;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string ChangeApproveText = DuThaoVanBanResources.ChangeStateApprove;
        protected string ChangePublicText = DuThaoVanBanResources.ChangeStatePublic;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected int langId = 1;
        protected int langRefer = 1;
        protected string lnkDetail = string.Empty;
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
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public DuThaoVanBanConfiguration Config
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
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSearch.Click += btnSearch_Click;
            btnDelAll.Click += btnDelAll_Click;
            btnBack.Click += btnBack_Click;
            //EnableViewState = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
            legendSearchProperty.InnerText = ArticleResources.ArticleEditSearchPropertyLabel;
            btnSearch.Text = ArticleResources.ArticleSearchButton;
            btnDelAll.BackColor = System.Drawing.Color.Red;
            btnDelAll.Text = ArticleResources.ButtonDeleteAll;
            btnExport.Text = DuThaoVanBanResources.ExportLabel;
            btnBack.Text = DuThaoVanBanResources.BackLabel;
            btnDelAll.OnClientClick = "return confirm(" + "'" + ArticleResources.DeleteAllConfirmWarning + "');";
            //ToDo?
            //ValidateDeleteAll();
            UIHelper.DisableButtonAfterClick(
                btnSearch,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty)
                );
            UIHelper.DisableButtonAfterClick(
                btnDelAll,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnDelAll, string.Empty)
                );
        }

        private void PopulateControls()
        {
            if (langId == LanguageConstant.VN)
            {
                langRefer = LanguageConstant.EN;
            }
            else
            {
                langRefer = LanguageConstant.VN;
            }
            BindDocument();
            PopulateStatus();
            if (valueStatus>0)
            {
                ddlStatus.SelectedValue = valueStatus.ToString();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }
        }

        private void BindDocument()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            List<CommentsDraft> reader = CommentsDraft.GetPage(duThaoId, isApprove, isPublic, pageNumber, config.PageSize, out totalPages);
                rptArticles.DataSource = reader;
                rptArticles.DataBind();
                string pageUrl = SiteRoot + "/DuThaoVanBan/CommentManagePost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&DuThaoID=" + duThaoId.ToInvariantString()
                   + "&status=" + valueStatus.ToInvariantString()
                   + "&keyword=" + keyword
                   + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DuThaoVanBanConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            duThaoId = WebUtils.ParseInt32FromQueryString("DuThaoID", duThaoId);
            valueStatus = WebUtils.ParseInt32FromQueryString("status", valueStatus);
            if(valueStatus > 0)
            {
                if(valueStatus==1)//đã duyệt
                {
                    isApprove = 1;
                }
                else if(valueStatus==2)// chưa duyệt
                {
                    isApprove = 0;
                }
                else if (valueStatus == 3)// đã xuất bản
                {
                    isPublic = 1;
                }
                else // chưa xuất bản
                {
                    isPublic = 0;
                }
            }
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        protected string formatContent(string Content)
        {
            string _Content = string.Empty;
            if (!string.IsNullOrEmpty(Content))
            {
                _Content = Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
                return DuThaoVanBanUltils.Truncate(_Content, 310);
            }
            else
            {
                return string.Empty;
            }
        }     
        protected string formatLinkDeatail(int itemId)
        {
            lnkDetail = siteSettings.SiteRoot + "/duthaovanban/ykiendonggop/detailcomment.aspx?pageid=" + pageId + "&mid=" + moduleId + "&item=" + itemId;
            return lnkDetail;
        }
        private void PopulateStatus()
        {
            var status = SiteUtils.StringToDictionary(DuThaoVanBanResources.ChangeStatusLabel.ToString(), ",");
            ddlStatus.DataSource = status;
            ddlStatus.DataTextField = "Value";
            ddlStatus.DataValueField = "Key";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("--" + DuThaoVanBanResources.ChooseStatusLabel + "--", "0"));
        }
       
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            keyword = txtKeyword.Text;
            valueStatus=int.Parse(ddlStatus.SelectedValue);

            string pageUrl = SiteRoot + "/duthaovanban/CommentManagePost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&DuThaoID="+duThaoId.ToInvariantString()
                    + "&status=" + valueStatus.ToInvariantString()
                    + "&keyword=" + keyword
                    + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }

        protected void rptArticles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("ApproveItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    CommentsDraft cmt = new CommentsDraft(itemId);
                    if (cmt.IsApproved == false)
                    {
                        cmt.IsApproved = true;
                        cmt.DateApproved = DateTime.UtcNow;
                        cmt.UserApproved = siteUser.SiteId;
                    }
                    else
                    {
                        cmt.IsApproved = false;
                        cmt.IsPublished = false;
                    }
                    cmt.Save();
                    WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/CommentManagePost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&DuThaoID=" + cmt.DuThaoID);
                }
                else if (e.CommandName.Equals("PublicItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    CommentsDraft cmt = new CommentsDraft(itemId);
                    if (cmt.IsPublished == false)
                    {
                        cmt.IsApproved = true;
                        cmt.UserApproved = siteUser.SiteId;
                        cmt.IsPublished = true;
                        cmt.UserPublished = siteUser.SiteId;
                    }
                    else
                    {
                        cmt.IsPublished = false;
                    }
                    cmt.Save();
                    WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/CommentManagePost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&DuThaoID=" + cmt.DuThaoID);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    CommentsDraft cmt = new CommentsDraft(itemId);
                    CommentsDraft.Delete(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/CommentManagePost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&DuThaoID=" + cmt.DuThaoID);
                }
            }
        }

        protected void rptArticles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnApprove = e.Item.FindControl("ibtnApprove") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnApprove, DuThaoVanBanResources.ApproveConfirmWarning);

                ImageButton ibtnPublic = e.Item.FindControl("ibtnPublic") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnPublic, DuThaoVanBanResources.PublicConfirmWarning);

                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, DuThaoVanBanResources.DeleteConfirmWarning);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/ManagePost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/ykiendonggop/ThongKeYKien.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString()+"&duthaoid="+duThaoId.ToInvariantString());
        }
        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            foreach (RepeaterItem ri in rptArticles.Items)
            {
                CheckBox chkFlag = ri.FindControl("chk") as CheckBox;
                if (chkFlag.Checked)
                {
                    deleteNumber++;
                    int itemid = Convert.ToInt32((ri.FindControl("repeaterID") as Literal).Text);
                    Article.Delete(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                string pageUrl = SiteRoot + "/duthaovanban/CommentManagePost.aspx"
                        + "?pageid=" + PageId.ToInvariantString()
                        + "&mid=" + ModuleId.ToInvariantString()
                        + "&keyword=" + keyword
                        + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }

        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return startDate.ToString(config.DateTimeFormat);
        }

    }
}
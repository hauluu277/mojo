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
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LichCongTacFeature.UI
{
    public partial class PostList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected LichCongTacConfiguration config = new LichCongTacConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int week = -1;
        private int year = -1;
        private string keyword = string.Empty;
        private int status = -1;
        private int dayId = -1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        protected int langId = 1;
        protected int langRefer = 1;
        protected string StateLink = DuThaoVanBanResources.ChangeStatus;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int author = 0;
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
        public int Week
        {
            get { return week; }
            set { week = value; }
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

        public LichCongTacConfiguration Config
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
            btnaddnew.Click += btnaddnew_Click;
            btnDelAll.Click += btnDelAll_Click;
            btnSearch.Click += btnSearch_Click;
            ddlYear.SelectedIndexChanged += new EventHandler(ddlYear_SelectedIndexChanged);
            btnExport.Click += BtnExport_Click;
            btnExportAll.Click += BtnExportAll_Click;
            //EnableViewState = false;
        }

        private void BtnExportAll_Click(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string fileName = string.Format("Danh-sach-lich-cong-tac-{0}.xls", DateTime.Now.ToString("ddmmyyyyhhmmss"));
            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName + "");
            response.Charset = "UTF-8";
            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    var keyword_search = !string.IsNullOrEmpty(keyword) ? keyword.ConvertToVN() : string.Empty;
                    var _author = "";
                    if (author > 0)
                    {
                        _author = author.ToString();
                    }
                    var table = LichCongTac.ExportAll(siteSettings.SiteId, moduleId, week, year, dayId, _author, keyword, pageNumber, config.PageSize, out totalPages);
                    dg.DataSource = table;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string fileName = string.Format("Danh-sach-lich-cong-tac-{0}.xls", DateTime.Now.ToString("ddmmyyyyhhmmss"));
            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName + "");
            response.Charset = "UTF-8";
            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    var keyword_search = !string.IsNullOrEmpty(keyword) ? keyword.ConvertToVN() : string.Empty;
                    var _author = "";
                    if (author > 0)
                    {
                        _author = author.ToString();
                    }
                    var table = LichCongTac.Export(siteSettings.SiteId, moduleId, week, year, dayId, _author, keyword, pageNumber, config.PageSize, out totalPages);
                    dg.DataSource = table;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadParams();
            LoadSettings();
            PopulateLabels();
            btnExport.Visible = false;
            btnExportAll.Visible = false;
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
            btnDelAll.BackColor = System.Drawing.Color.Red;
            btnDelAll.Text = ArticleResources.ButtonDeleteAll;
            btnaddnew.Text = ArticleResources.ButtonAddNew;
            btnSearch.Text = LichCongTacResources.btnSearchLabel;
            legendSearchProperty.InnerText = "Tìm kiếm lịch làm việc";
            btnDelAll.OnClientClick = "return confirm(" + "'" + ArticleResources.DeleteAllConfirmWarning + "');";

            UIHelper.DisableButtonAfterClick(
                btnDelAll,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnDelAll, string.Empty)
                );
        }

        private void PopulateControls()
        {
            BindUser();
            PopulateYear();
            PopulateWeek();
            BindDocument();
            if (year > 0)
            {
                ddlYear.SelectedValue = year.ToString();
                List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(year);
                ddlWeek.DataValueField = "Week";
                ddlWeek.DataTextField = "NameWeek";
                ddlWeek.DataSource = lstWeek;
                ddlWeek.DataBind();
                ddlWeek.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseDayLabel + "--", "0"));
            }
            if (week > 0)
            {
                ddlWeek.SelectedValue = week.ToString();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }
            if (author > 0)
            {
                ddlCategoryAuthor.SelectedValue = author.ToString();
            }
        }
        private void BindUser()
        {

            var parent = CoreCategory.GetByCode(1, WebConfigSettings.DM_LANHDAO);
            if (parent != null)
            {
                ddlCategoryAuthor.DataSource = CoreCategory.GetChildren(parent.ItemID);
                ddlCategoryAuthor.DataTextField = "Name";
                ddlCategoryAuthor.DataValueField = "ItemID";
                ddlCategoryAuthor.DataBind();
                ddlCategoryAuthor.Items.Insert(0, new ListItem { Text = "--Chọn--", Value = "" });
            }
        }
        private void BindDocument()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            int totalCount = 0;
            var _author = "";
            if (author > 0)
            {
                _author = author.ToString();
            }
            List<LichCongTac> reader = LichCongTac.GetPageFix(siteSettings.SiteId, moduleId, week, year, dayId, _author, keyword, pageNumber, config.PageSize, out totalPages, out totalCount);
            if (totalPages > 1)
            {
                lblTotal.Text = string.Format("{0}/{1} tổng số lịch công tác", config.PageSize, totalCount);

            }
            else
            {
                lblTotal.Text = totalCount + " lịch công tác";
            }
            rptArticles.DataSource = reader;
            rptArticles.DataBind();
            string pageUrl = siteSetting.SiteRoot + "/lichcongtac/managepost.aspx"
               + "?pageid=" + PageId.ToInvariantString()
               + "&mid=" + ModuleId.ToInvariantString()
               + "&year=" + year.ToInvariantString()
               + "&week=" + week.ToInvariantString()
               + "&keyword=" + keyword
               + "&author=" + author
               + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
        }

        private void PopulateYear()
        {
            List<YearMapDate> lstYear = new List<YearMapDate>();
            int i = DateTime.Now.Year;
            int b = i + 10;
            for (int a = 2010; a < b; a++)
            {
                YearMapDate nam = new YearMapDate();
                nam.Year = a;
                nam.NameYear = "Năm " + a;
                lstYear.Add(nam);
            }
            ddlYear.DataValueField = "Year";
            ddlYear.DataTextField = "NameYear";
            ddlYear.DataSource = lstYear;
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseYearLabel + "--", "0"));
        }
        private void PopulateWeek()
        {
            //int year = DateTime.Now.Year;
            //int month = DateTime.Now.Month;
            //int currentWeek = Ultilities.GetIso8601WeekOfYear(DateTime.Now);
            //List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(year);
            //ddlWeek.DataValueField = "Week";
            //ddlWeek.DataTextField = "NameWeek";
            //ddlWeek.DataSource = lstWeek;
            //ddlWeek.DataBind();
            ddlWeek.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseWeekLabel + "--", "0"));
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            if (year > 0)
            {
                List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(year);
                ddlWeek.DataValueField = "Week";
                ddlWeek.DataTextField = "NameWeek";
                ddlWeek.DataSource = lstWeek;
                ddlWeek.DataBind();
                ddlWeek.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseWeekLabel + "--", "0"));
            }
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            if (moduleId == -1)
            {
                moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            }
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new LichCongTacConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            year = WebUtils.ParseInt32FromQueryString("year", year);
            week = WebUtils.ParseInt32FromQueryString("week", week);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            author = WebUtils.ParseInt32FromQueryString("author", author);
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            keyword = txtKeyword.Text;
            year = int.Parse(ddlYear.SelectedValue);
            week = int.Parse(ddlWeek.SelectedValue);
            if (ddlCategoryAuthor != null && ddlCategoryAuthor.SelectedValue != "")
            {
                author = Convert.ToInt32(ddlCategoryAuthor.SelectedValue);
            }
            else
            {
                author = 0;
            }

            string pageUrl = siteSetting.SiteRoot + "/lichcongtac/managepost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&year=" + year.ToInvariantString()
                    + "&week=" + week.ToInvariantString()
                    + "&keyword=" + keyword
                    + "&author=" + author
                    + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }

        protected void rptArticles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, siteSetting.SiteRoot + "/lichcongtac/editpost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&item=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    LichCongTac.Delete(itemId);
                    WebUtils.SetupRedirect(this, siteSetting.SiteRoot + "/lichcongtac/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }

            }
        }

        protected void rptArticles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, LichCongTacResources.DeleteWarningLabel);
            }
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, siteSetting.SiteRoot + "/lichcongtac/editpost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());

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
                    LichCongTac.Delete(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                string pageUrl = siteSetting.SiteRoot + "/lichcongtac/managepost.aspx"
                        + "?pageid=" + PageId.ToInvariantString()
                        + "&mid=" + ModuleId.ToInvariantString()
                        + "&week=" + week.ToInvariantString()
                        + "&keyword=" + keyword
                        + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }

    }
}
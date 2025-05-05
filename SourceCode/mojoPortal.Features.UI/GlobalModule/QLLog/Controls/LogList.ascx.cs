using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Service.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Newtonsoft.Json;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LogFeature.UI
{
    public partial class LogList : UserControl
    {
        //#region Properties
        //private int pageNumber = 1;
        //private int totalPages = 1;

        //private mojoBasePage basePage;
        //private Module module;
        //protected DocumentConfiguration config = new DocumentConfiguration();

        //private int pageId = -1;
        //private int moduleId = -1;
        //private int itemId = -1;
        //private string siteRoot = string.Empty;
        //private string imageSiteRoot = string.Empty;
        //public SiteSettings siteSettings;
        //private int linhVucId = -1;
        //private int loaiVb = -1;
        //private int coQuanId = -1;
        //private int namBanHanh = -1;
        //private int chuDe = -1;
        //protected int langId = 1;
        //private string keyword = string.Empty;

        //private int mucDoId = -1;
        //private int capThuTucId = -1;


        //protected string EditContentImage = WebConfigSettings.EditContentImage;
        //protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        //protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        //protected string DeleteLinkImageUrl = string.Empty;
        //protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        //protected string EditLinkImageUrl = string.Empty;
        //readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        //readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        //public int PageId
        //{
        //    get { return pageId; }
        //    set { pageId = value; }
        //}

        //public int ModuleId
        //{
        //    get { return moduleId; }
        //    set { moduleId = value; }
        //}
        //public int LinhVucId
        //{
        //    get { return linhVucId; }
        //    set { linhVucId = value; }
        //}
        //public int LoaiVb
        //{
        //    get { return loaiVb; }
        //    set { loaiVb = value; }
        //}
        //public int CoQuanId
        //{
        //    get { return coQuanId; }
        //    set { coQuanId = value; }
        //}
        //public int NamBanHanh
        //{
        //    get { return namBanHanh; }
        //    set { namBanHanh = value; }
        //}

        //public string Keyword
        //{
        //    get { return keyword; }
        //    set { keyword = value; }
        //}
        //public string SiteRoot
        //{
        //    get { return siteRoot; }
        //    set { siteRoot = value; }
        //}

        //public DocumentConfiguration Config
        //{
        //    get { return config; }
        //    set { config = value; }
        //}

        //public string ImageSiteRoot
        //{
        //    get { return imageSiteRoot; }
        //    set { imageSiteRoot = value; }
        //}

        //public bool IsEditable { get; set; }
        //#endregion

        //override protected void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    Load += Page_Load;
        //    btnSearch.Click += btnSearch_Click;
        //    btnReset.Click += btnReset_Click;
        //    //EnableViewState = false;
        //}

        //void btnReset_Click(object sender, EventArgs e)
        //{
        //    SiteUtils.RedirectToUrl(SiteRoot + "/GlobalModule/ThuTucHanhChinh/ViewPost.aspx?pageid=" + PageId + "&mid=" + ModuleId);
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    var lang = CultureInfo.CurrentCulture.Name;
        //    langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
        //    LoadSettings();
        //    PopulateLabels();
        //    if (!Page.IsPostBack)
        //    {
        //        PopulateControls();
        //        BindTree();
        //    }
        //}

        //private void BindTree()
        //{
        //    ThuTucBusiness thuTucBusiness = new ThuTucBusiness(new mojoportal.Service.UoW.UnitOfWork());

        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("<script type = 'text/javascript'>");
        //    sb.Append("$('document').ready(function(){");
        //    sb.Append("var data=[{");
        //    var getCoreCategory = CoreCategory.GetByCode(1, WebConfigSettings.DM_LinhVucThuTucHS);
        //    sb.Append("'text':'Sở Giáo dục và Đào tạo&nbsp;(" + thuTucBusiness.GetCount(getCoreCategory.ItemID, true) + ")','state':{'opened':true},children:[");
        //    var listChild = CoreCategory.GetChildren(getCoreCategory.ItemID);

        //    foreach (var item in listChild)
        //    {
        //        sb.Append("{'text':'" + string.Format("{0}&nbsp;({1})", item.Name, thuTucBusiness.GetCount(item.ItemID)) + "','id':" + item.ItemID + "},");
        //    }
        //    sb.Append("]");
        //    sb.Append("}];");

        //    sb.Append("$('#tree').jstree({");
        //    sb.Append("'core':{");
        //    sb.Append("'state':{'opened':true},");
        //    sb.Append("'themes': {");
        //    sb.Append("'theme': 'apple',");
        //    sb.Append("'dots': true,");
        //    sb.Append("'icons': false,");
        //    sb.Append("'responsive': true");
        //    sb.Append("},");
        //    sb.Append("'data':data");
        //    sb.Append("}");
        //    sb.Append("}).bind('loaded.jstree', function (event, data) {$(this).jstree('open_all');");
        //    sb.Append("}).on('changed.jstree', function (e, data) {");
        //    sb.Append(" var i, j, r = [];");
        //    sb.Append("for(i = 0, j = data.selected.length; i < j; i++) {");
        //    sb.Append("r.push(data.instance.get_node(data.selected[i]).id);");
        //    sb.Append("var id=r.join(', ');");
        //    sb.Append("window.location.href = '/GlobalModule/ThuTucHanhChinh/ManageThuTuc.aspx?idlinhvuc='+id;");
        //    sb.Append("}");
        //    sb.Append("});");
        //    sb.Append("});");
        //    sb.Append("</script>");
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "loadTree", sb.ToString());
        //}

        //private void PopulateLabels()
        //{
        //    heading.Text = "Tra cứu thủ tục hành chính";
        //    btnSearch.Text = DocumentResources.SearchButton;
        //    btnReset.Text = DocumentResources.ButtonReset;
        //    //ToDo?
        //    //ValidateDeleteAll();
        //    UIHelper.DisableButtonAfterClick(
        //        btnSearch,
        //        ArticleResources.ButtonDisabledPleaseWait,
        //        Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty)
        //        );
        //}

        //private void PopulateControls()
        //{
        //    bindLinhVuc();
        //    BindThuTuc();
        //    bindCapThuTuc();
        //    bindMucDoDVC();
        //    if (linhVucId > 0)
        //    {
        //        ddlLinhVuc.SelectedValue = linhVucId.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(keyword))
        //    {
        //        txtKeyword.Text = keyword;
        //    }
        //}

        //private void BindThuTuc()
        //{
        //    int totalVanBan = 0;
        //    var reader = ThuTuc.GetPage(siteSetting.SiteId, true, linhVucId, mucDoId, capThuTucId, keyword, 1, 20, out totalPages);
        //    rptArticles.DataSource = reader;
        //    rptArticles.DataBind();
        //    string pageUrl = SiteRoot + "/GlobalModule/ThuTucHanhChinh/viewpost.aspx"
        //           + "?pageid=" + PageId.ToInvariantString()
        //           + "&mid=" + ModuleId.ToInvariantString()
        //           + "&linhvucid=" + linhVucId.ToInvariantString()
        //           + "&loaivb=" + loaiVb.ToInvariantString()
        //           + "&coquan=" + coQuanId.ToInvariantString()
        //           + "&nam=" + namBanHanh.ToInvariantString()
        //           + "&chude=" + chuDe.ToInvariantString()
        //           + "&keyword=" + keyword
        //           + "&pagenumber={0}";

        //    pgrArticle.PageURLFormat = pageUrl;
        //    pgrArticle.ShowFirstLast = true;
        //    pgrArticle.PageSize = config.PageSize;
        //    pgrArticle.PageCount = totalPages;
        //    pgrArticle.CurrentIndex = pageNumber;
        //    pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
        //    lblTotalVanBan.Text = string.Format("Tổng số thủ tục <span class='red'>{0}</span> ", totalVanBan);
        //}

        //private void bindLinhVuc()
        //{
        //    //get all categories
        //    int CategoryConfig = siteSetting.CoreLinhVucVanBanQuyPham;
        //    List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_LinhVucThuTucHS);
        //    ddlLinhVuc.DataValueField = "ItemID";
        //    ddlLinhVuc.DataTextField = "Name";
        //    ddlLinhVuc.DataSource = lstLoai;
        //    ddlLinhVuc.DataBind();
        //    ddlLinhVuc.Items.Insert(0, new ListItem("--Chọn lĩnh vực văn bản--", "0"));
        //}
        //private void bindMucDoDVC()
        //{
        //    //get all categories
        //    int CategoryConfig = siteSetting.CoreLoaiVanBanQuyPham;
        //    //List<CoreCategory> lstLoai = CoreCategory.GetChildren(siteSetting.SiteId, CategoryConfig);
        //    List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_MucDoDVC);
        //    ddlMucDoDVC.DataValueField = "ItemID";
        //    ddlMucDoDVC.DataTextField = "Name";
        //    ddlMucDoDVC.DataSource = lstLoai;
        //    ddlMucDoDVC.DataBind();
        //    ddlMucDoDVC.Items.Insert(0, new ListItem("--Chọn--", "0"));
        //}
        //private void bindCapThuTuc()
        //{
        //    //get all categories
        //    int CategoryConfig = siteSetting.CoreCoQuanBanHanhVanBanQuyPham;
        //    //List<CoreCategory> lstCoQuan = CoreCategory.GetChildren(siteSetting.SiteId, CategoryConfig);
        //    List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_CapDoThuTuc);
        //    ddlCapThuTuc.DataValueField = "ItemID";
        //    ddlCapThuTuc.DataTextField = "Name";
        //    ddlCapThuTuc.DataSource = lstLoai;
        //    ddlCapThuTuc.DataBind();
        //    ddlCapThuTuc.Items.Insert(0, new ListItem("--Chọn--", "0"));
        //}


        //protected virtual void LoadSettings()
        //{
        //    Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
        //    config = new DocumentConfiguration(getModuleSettings);

        //    siteSettings = CacheHelper.GetCurrentSiteSettings();
        //    pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
        //    linhVucId = WebUtils.ParseInt32FromQueryString("linhvucid", linhVucId);
        //    loaiVb = WebUtils.ParseInt32FromQueryString("loaivb", loaiVb);
        //    coQuanId = WebUtils.ParseInt32FromQueryString("coquan", coQuanId);
        //    namBanHanh = WebUtils.ParseInt32FromQueryString("nam", namBanHanh);
        //    chuDe = WebUtils.ParseInt32FromQueryString("chude", chuDe);
        //    keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

        //    if (Page is mojoBasePage)
        //    {
        //        basePage = Page as mojoBasePage;
        //        module = basePage.GetModule(moduleId);
        //    }
        //}


        //protected string formatContent(string Content)
        //{
        //    if (!string.IsNullOrEmpty(Content))
        //    {
        //        return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}
        //protected void Search()
        //{
        //    linhVucId = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? -1 : int.Parse(ddlLinhVuc.SelectedValue);
        //    keyword = txtKeyword.Text;
        //    string pageUrl = SiteRoot + "/GlobalModule/ThuTucHanhChinh/viewpost.aspx"
        //              + "?pageid=" + PageId.ToInvariantString()
        //            + "&mid=" + ModuleId.ToInvariantString()
        //            + "&linhvucid=" + linhVucId.ToInvariantString()
        //           + "&loaivb=" + loaiVb.ToInvariantString()
        //           + "&coquan=" + coQuanId.ToInvariantString()
        //            + "&nam=" + namBanHanh.ToInvariantString()
        //            + "&keyword=" + keyword
        //            + "&pagenumber=1";
        //    WebUtils.SetupRedirect(this, pageUrl);
        //}
        //protected virtual void btnSearch_Click(object sender, EventArgs e)
        //{
        //    Search();
        //}

        //protected string FormatArticleDate(DateTime datePromulgate)
        //{
        //    if (config.DateTimeFormat == string.Empty) return string.Empty;
        //    return datePromulgate.ToString(config.DateTimeFormat);
        //}


        //protected string DownloadFile(string filePath)
        //{
        //    if (!string.IsNullOrEmpty(filePath))
        //    {
        //        return SiteRoot + "/" + ConfigurationManager.AppSettings["DocumentFileFolder"] + filePath;
        //    }
        //    return string.Empty;
        //}

    }

    public class FileOrFoderTree
    {

        //tên file or foder
        public string name { get; set; }
        // loại file hay folder: trống là file ; Tree.FOLDER là foder
        public string type { get; set; }
        public bool open { get; set; }
        public bool selected { get; set; }
        //đuôi file gồm .exe, css, skin, config, html, js,...
        public string DuoiFile { get; set; }
        //Con
        public List<FileOrFoderTree> children { get; set; }
        public string DuongdanCha { get; set; }
    }
}
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThuTucHanhChinhFeature.UI
{
    public partial class DetailThuTuchanhChinh : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected DocumentConfiguration config = new DocumentConfiguration();

        private int pageId = -1;
        private int moduleId = -1;

        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        public SiteSettings siteSettings;
        private int linhVucId = -1;
        private int loaiVb = -1;
        private int coQuanId = -1;
        private int namBanHanh = -1;
        private int chuDe = -1;
        protected int langId = 1;
        private string keyword = string.Empty;

        private int mucDoId = -1;
        private int capThuTucId = -1;



        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();

        public int ItemId
        {
            get { return pageId; }
            set { pageId = value; }
        }
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
        public int LinhVucId
        {
            get { return linhVucId; }
            set { linhVucId = value; }
        }
        public int LoaiVb
        {
            get { return loaiVb; }
            set { loaiVb = value; }
        }
        public int CoQuanId
        {
            get { return coQuanId; }
            set { coQuanId = value; }
        }
        public int NamBanHanh
        {
            get { return namBanHanh; }
            set { namBanHanh = value; }
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

        public DocumentConfiguration Config
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

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            rptBieuMau.ItemCommand += RptBieuMau_ItemCommand;
            //EnableViewState = false;
        }

        private void BindBieuMau()
        {
            ThuTucBieuMauBusiness thuTucBieuMauBusiness = new ThuTucBieuMauBusiness(new mojoportal.Service.UoW.UnitOfWork());
            var data = thuTucBieuMauBusiness.GetList(ItemId);
            pnlFileBieuMau.Visible = (data != null && data.Count > 0);
            rptBieuMau.DataSource = data;
            rptBieuMau.DataBind();

            var getDMThongKe = CoreCategory.GetByCode(1,"THONGKE_DVC");
            var listChild = CoreCategory.GetChildren(getDMThongKe.ItemID);
            rptThongKeDVC.DataSource = listChild;
            rptThongKeDVC.DataBind();

        }
        protected void RptBieuMau_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("CountDownload"))
                {
                    var itemId = int.Parse(e.CommandArgument.ToString());
                    ThuTucBieuMauBusiness thuTucBieuMauBusiness = new ThuTucBieuMauBusiness(new mojoportal.Service.UoW.UnitOfWork());
                    var getBieuMau = thuTucBieuMauBusiness.UpdateTotalDownload(itemId);
                    if (getBieuMau != null)
                    {
                        WebUtils.SetupRedirect(this, SiteRoot + getBieuMau.PathFile);
                    }
                }
            }
        }

        void btnReset_Click(object sender, EventArgs e)
        {
            SiteUtils.RedirectToUrl(SiteRoot + "/GlobalModule/ThuTucHanhChinh/ViewPost.aspx?pageid=" + PageId + "&mid=" + ModuleId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
                BindBieuMau();
                BindTree();
            }

        }

        private void BindTree()
        {
            ThuTucBusiness thuTucBusiness = new ThuTucBusiness(new mojoportal.Service.UoW.UnitOfWork());

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("$('document').ready(function(){");
            sb.Append("var data=[{");
            var getCoreCategory = CoreCategory.GetByCode(1, WebConfigSettings.DM_LinhVucThuTucHS);
            sb.Append("'text':'Sở Giáo dục và Đào tạo&nbsp;(" + thuTucBusiness.GetCount(getCoreCategory.ItemID, true) + ")','state':{'opened':true},children:[");
            var listChild = CoreCategory.GetChildren(getCoreCategory.ItemID);

            foreach (var item in listChild)
            {
                sb.Append("{'text':'" + string.Format("{0}&nbsp;({1})", item.Name, thuTucBusiness.GetCount(item.ItemID)) + "','id':" + item.ItemID + "},");
            }
            sb.Append("]");
            sb.Append("}];");

            sb.Append("$('#tree').jstree({");
            sb.Append("'core':{");
            sb.Append("'state':{'opened':true},");
            sb.Append("'themes': {");
            sb.Append("'theme': 'apple',");
            sb.Append("'dots': true,");
            sb.Append("'icons': false,");
            sb.Append("'responsive': true");
            sb.Append("},");
            sb.Append("'data':data");
            sb.Append("}");
            sb.Append("}).bind('loaded.jstree', function (event, data) {$(this).jstree('open_all');");
            sb.Append("}).on('changed.jstree', function (e, data) {");
            sb.Append(" var i, j, r = [];");
            sb.Append("for(i = 0, j = data.selected.length; i < j; i++) {");
            sb.Append("r.push(data.instance.get_node(data.selected[i]).id);");
            sb.Append("var id=r.join(', ');");
            sb.Append("window.location.href = '/GlobalModule/ThuTucHanhChinh/ViewPost.aspx?pageid=6340&linhvucid='+id;");
            sb.Append("}");
            sb.Append("});");
            sb.Append("});");
            sb.Append("</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "loadTree", sb.ToString());
        }

        private void PopulateLabels()
        {
            heading.Text = "Tra cứu thủ tục hành chính";

            //ToDo?
            //ValidateDeleteAll();

        }

        private void PopulateControls()
        {
            BinDataInTable();
            if (linhVucId > 0)
            {

            }

            if (!string.IsNullOrEmpty(keyword))
            {

            }
        }


        private void BinDataInTable()
        {
            var thuTuc = new ThuTuc(ItemId);
            hplDichVuCong.HRef = thuTuc.LinkDVC;
            lblTenThuTuc.Text = thuTuc.TenThuTuc;
            lblLinhVuc.Text = thuTuc.LinhVucName;
            lblTrinhTuThucHien.Text = thuTuc.TrinhTuThucHien;
            lblThoiHanGiaiQuyet.Text = thuTuc.ThoiHanGianQuyet;
            lblPhi.Text = thuTuc.Phi;
            lblLePhi.Text = thuTuc.LePhi;
            lblSoLuongHoSo.Text = thuTuc.SoLuongHoSo + " bộ";
            lblYeuCauDieuKien.Text = thuTuc.YeuCauDieuKien;
            lblCanCuPhapLy.Text = thuTuc.CanCuPhapLy;

            lblKetQuaThucHien.Text = thuTuc.KetQuaThucHien;
            literThanhPhanHS.Text = thuTuc.ThanhPhanHoSo;

            var getDoiTuong = new CoreCategory(thuTuc.IdDoiTuongThucHien);
            if (getDoiTuong != null)
            {
                liDoiTuongThucHien.Text = getDoiTuong.Name;
            }


            if (!string.IsNullOrEmpty(thuTuc.CachThucThucHien))
            {
                var listIdCachThucHien = thuTuc.CachThucThucHien;
                var arrId = listIdCachThucHien.Split(',');
                var listCanhThucHien = new List<core_Category>();

                if (arrId != null && arrId.Length > 0)
                {
                    foreach (var item in arrId)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            CategoryBusiness categoryBusiness = new CategoryBusiness(new mojoportal.Service.UoW.UnitOfWork());
                            var cachThucHien = categoryBusiness.GetById(int.Parse(item));
                            listCanhThucHien.Add(cachThucHien);
                        }

                    }
                }

                rptArticles.DataSource = listCanhThucHien;
                rptArticles.DataBind();
            }


        }


        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DocumentConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            linhVucId = WebUtils.ParseInt32FromQueryString("linhvucid", linhVucId);
            loaiVb = WebUtils.ParseInt32FromQueryString("loaivb", loaiVb);
            coQuanId = WebUtils.ParseInt32FromQueryString("coquan", coQuanId);
            namBanHanh = WebUtils.ParseInt32FromQueryString("nam", namBanHanh);
            chuDe = WebUtils.ParseInt32FromQueryString("chude", chuDe);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }


        protected string formatContent(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
        protected void Search()
        {

        }
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected string FormatArticleDate(DateTime datePromulgate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return datePromulgate.ToString(config.DateTimeFormat);
        }


        protected string DownloadFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                return SiteRoot + "/" + ConfigurationManager.AppSettings["DocumentFileFolder"] + filePath;
            }
            return string.Empty;
        }

    }
}
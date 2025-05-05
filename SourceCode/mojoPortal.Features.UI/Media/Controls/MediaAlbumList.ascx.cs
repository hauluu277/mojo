using MediaAlbumFeature.Business;
using MediaAlbumFeature.UI;
using MediaGroupFeature.Business;
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
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaFeature.UI
{
    public partial class MediaAlbumList : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected MediaConfiguration config = new MediaConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int groupMediaId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryID = -1;
        private bool? isFeatured = null;
        private int status = -1;
        private string keyword = string.Empty;
        private int orderBy = 0;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
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
        public int CategoryId
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public MediaConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        public int GroupMediaID
        {
            get { return groupMediaId; }
            set { groupMediaId = value; }
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

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            lnkDowload.Click += lnkDowload_Click;
            //btnSearch.Click += btnSearch_Click;
        }

        private void lnkDowload_Click(object sender, EventArgs e)
        {
            try
            {
                MediaAlbum media = new MediaAlbum(int.Parse(hfFile.Value));
                if (media != null)
                {
                    string filePath = media.FilePath;

                    System.IO.FileStream fs = null;
                    string path = HostingEnvironment.ApplicationPhysicalPath + @"Data\File\Media\" + filePath;
                    fs = System.IO.File.Open(path, System.IO.FileMode.Open);
                    long fileSize = fs.Length;

                    byte[] btFile = new byte[(int)fileSize];
                    fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" + media.FilePath);
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.BinaryWrite(btFile);
                    HttpContext.Current.Response.WriteFile(path);
                    HttpContext.Current.Response.End();
                    fs = null;
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void LoadTypeSearh()
        {

            List<ListItem> lst = new List<ListItem>();
            lst.Add(new ListItem { Text = "Xem dữ liệu theo", Value = "0" });
            lst.Add(new ListItem { Text = "Mới nhất", Value = "3" });
            lst.Add(new ListItem { Text = "Nổi bật", Value = "2" });
            lst.Add(new ListItem { Text = "Xem nhiều nhất", Value = "1" });
            drlSearch.DataTextField = "Text";
            drlSearch.DataValueField = "Value";
            drlSearch.DataSource = lst;
            drlSearch.DataBind();
            drlSearch.SelectedValue = orderBy.ToString();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            keyword = txtSearch2.Value;
            //groupMediaId = Convert.ToInt32(ddlMultiGroup.SelectedValue);
            //string _status = ddlFeatured.SelectedValue;
            //if (!string.IsNullOrEmpty(_status))
            //{
            //    status = int.Parse(_status);
            //    if (_status == "1")
            //    {
            //        isFeatured = true;
            //    }
            //    else if (_status == "0")
            //    {
            //        isFeatured = false;
            //    }
            //}
            orderBy = Convert.ToInt32(drlSearch.SelectedValue);
            string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&groupMediaId=" + groupMediaId
                  + "&orderBy=" + orderBy
                  + "&keyword=" + keyword
                  + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }
        private void loadGroupMedia()
        {
            ddlMultiGroup.DataTextField = "NameGroup";
            ddlMultiGroup.DataValueField = "ItemID";
            ddlMultiGroup.DataSource = MediaGroup.GetAll();
            ddlMultiGroup.DataBind();
            ddlMultiGroup.Items.Insert(0, new ListItem(MediaResources.GroupMediaStatus, "-1"));
        }
        private void loadFeatured()
        {
            var status = SiteUtils.StringToDictionary(MediaResources.FeaturedStatus, ",");
            ddlFeatured.DataTextField = "Value";
            ddlFeatured.DataValueField = "Key";
            ddlFeatured.DataSource = status;
            ddlFeatured.DataBind();
        }
        private void hplDowload_Click(object sender, EventArgs e)
        {
            try
            {
                MediaAlbum media = new MediaAlbum(int.Parse(hfFile.Value));
                if (media != null)
                {
                    string filePath = media.FilePath;

                    System.IO.FileStream fs = null;
                    string path = HostingEnvironment.ApplicationPhysicalPath + @"Data\File\Media\" + filePath;
                    fs = System.IO.File.Open(path, System.IO.FileMode.Open);
                    long fileSize = fs.Length;

                    byte[] btFile = new byte[(int)fileSize];
                    fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" + media.FilePath);
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.BinaryWrite(btFile);
                    HttpContext.Current.Response.WriteFile(path);
                    HttpContext.Current.Response.End();
                    fs = null;
                }

            }
            catch (Exception ex)
            {
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            LoadTypeSearh();
            LoadMediaData();
            loadFeatured();
            loadGroupMedia();
            if (status > -1)
            {
                ddlFeatured.SelectedValue = status.ToString();
            }
            if (groupMediaId > 0)
            {
                ddlMultiGroup.SelectedValue = groupMediaId.ToString();
            }
        }
        protected string GetHtml(string ID, string filePath, string typeData, string image, string Name)
        {
            string str = "";
            string url = ConfigurationManager.AppSettings["MultimediaFileFolder"];
            //if (int.Parse(typeData) == MediaConstant.IMAGE)
            //{
            //    str += "<a id='item_" + ID + "' data-type='" + MediaConstant.IMAGE + "' title='" + Name + "' onclick='show(" + ID + ")'  href='javascript:void(0)'>";
            //    str += " <img src='/" + url + filePath + "' /></a>";
            //}
            if (int.Parse(typeData) == MediaConstant.VIDEO)
            {
                str += "<a href='javascript:void(0)' id='item_" + ID + "' title='" + Name + "' data-type='" + typeData + "'  onclick='show(" + ID + ")' data-href='/" + url + filePath + "' >";
                str += "<span class='play-video-hover topview'></span>";
                str += "<img src='/" + url + image + " '/></a>";
            }
            else if (int.Parse(typeData) == MediaConstant.IMAGE)
            {
                str += "<a id='item_" + ID + "' data-type='" + typeData + "' title='" + Name + "' onclick='show(" + ID + ")'  href='javascript:void(0)'>";
                str += " <img src='/" + url + filePath + "' /></a>";
            }
            else
            {
                str += "<a id='item_" + ID + "' data-type='" + typeData + "' title='" + Name + "' onclick='show(" + ID + ")'  href='javascript:void(0)'>";
                str += "<span class='play-video-hover topview'></span>";
                str += " <img src='/" + url + filePath + "' /></a>";
            }
            return str;
        }

        private void LoadMediaData()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
           
            List<MediaAlbum> lstMedia = new List<MediaAlbum>();
            lstMedia = MediaAlbum.GetPage(siteSettings.SiteId, moduleId, pageNumber, config.PageSize, groupMediaId, status, orderBy, true, keyword, out totalPages);
            dtlData.DataSource = lstMedia;
            dtlData.DataBind();
            if (lstMedia.Count == 0)
            {
                DanhBanull.Visible = true;
                DanhBanull.Text = MediaResources.NoDataFound;
            }
            string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&groupMediaId=" + groupMediaId
                  + "&keyword=" + keyword
                  + "&orderBy=" + orderBy
                  + "&pagenumber={0}";

            pgrDanhBa.PageURLFormat = pageUrl;
            pgrDanhBa.ShowFirstLast = true;
            pgrDanhBa.PageSize = config.PageSize;
            pgrDanhBa.PageCount = totalPages;
            pgrDanhBa.CurrentIndex = pageNumber;
            pnlDonViPager.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void LoadParams()
        {
            //pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            // moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            orderBy = WebUtils.ParseInt32FromQueryString("orderBy", orderBy);

        }
        public void PopulateLabels()
        {
            lnkDowload.Text = MediaResources.DowloadTitle;
            lblDictionaryTitle.Text = MediaResources.MultiMediaDataTitle;
            hplDataGroup.Text = MediaResources.CategoryMultiMediaTitle;
            hplMutilData.Text = MediaResources.MultiMediaDataTitle;
            hplDataGroup.NavigateUrl = SiteRoot + "/Media/ViewListMediaGroup.aspx?pageid=" + pageId + "&mid=" + moduleId;
            hplMutilData.NavigateUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx?pageid=" + pageId + "&mid=" + moduleId;
            btnSearch.InnerText = MediaResources.MediaButtonSearch;
            hfView.Value = MediaResources.TotalViewsTitle;
            hfFeatured.Value = MediaResources.MediaAlbumFeaturedTitle;
            if (groupMediaId > -1)
            {
                MediaGroup meida = new MediaGroup(groupMediaId);
                hplMutilData.Text = meida.NameGroup;
                hplMutilData.NavigateUrl = "";
            }
        }
        protected virtual void LoadSettings()
        {

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new MediaConfiguration(getModuleSettings);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            groupMediaId = WebUtils.ParseInt32FromQueryString("groupMediaId", GroupMediaID);
            //string _status = ddlFeatured.SelectedValue;
            //if (!string.IsNullOrEmpty(_status))
            //{
            //    status = int.Parse(_status);
            //    if (_status == "1")
            //    {
            //        isFeatured = true;
            //    }
            //    else if (_status == "0")
            //    {
            //        isFeatured = false;
            //    }
            //}
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }

    }
}
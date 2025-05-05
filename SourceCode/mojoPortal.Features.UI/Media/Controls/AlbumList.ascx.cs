using MediaAlbumFeature.Business;
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaFeature.UI
{
    public partial class AlbumList : System.Web.UI.UserControl
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
        private bool? isPublish = null;
        private int status = -1;
        private string keyword = string.Empty;
        private int statusFeatureed = -1;
        private bool? isFeatured = null;
        private int searchBy = 0;
        private int galleryId = 0;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
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
        public int SearchBy
        {
            get { return searchBy; }
            set { searchBy = value; }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSearch.Click += btnSearch_Click;
            //btnAddNewMediaGroup.Click += btnAddNewMediaGroup_Click; ;
            btnRemoveAll.Click += btnRemoveAll_Click;
            btnAddMultiple.Click += btnAddMultiple_Click;
            btnAddNewMediaAlbum.Click += btnAddNewMediaAlbum_Click;
            rptMedia.ItemCommand += rptMedia_ItemCommand;
            rptMedia.ItemDataBound += rptMedia_ItemDataBound;
        }

        void btnAddMultiple_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Media/CreateMultipleImage.aspx?pageid=" + pageId + "&mid=" + moduleId + "&galleryId=" + groupMediaId;
            WebUtils.SetupRedirect(this, url);
        }

        private void rptMedia_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, MediaResources.ConfirmDeleteWarning);

                //ImageButton btnFeatured = e.Item.FindControl("btnFeatured") as ImageButton;
                //SiteUtils.AddConfirmButton(btnFeatured, "Bạn có chắc chắn muốn thay đổi trạng thái nổi bật?");

                ImageButton btnPublish = e.Item.FindControl("btnPublish") as ImageButton;
                SiteUtils.AddConfirmButton(btnPublish, "Bạn có chắc chắn muốn thay đổi trạng thái hiển thị?");
            }
        }
        private bool DeleteFileFromServer(int itemID)
        {
            MediaAlbum media = new MediaAlbum(itemID);
            if (media.TypeData == 2)
            {
                string filePath_IMG = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MediaIMG"] + media.ImageVideo;
                filePath_IMG = filePath_IMG.Replace("/", "\\");
                if (File.Exists(filePath_IMG))
                {
                    File.Delete(filePath_IMG);
                }
                string filePath_VIDEO = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MediaIMG"] + media.FileName;
                filePath_VIDEO = filePath_VIDEO.Replace("/", "\\");
                if (File.Exists(filePath_VIDEO))
                {
                    File.Delete(filePath_VIDEO);
                }
                return true;
            }
            else if (media.TypeData == 1)
            {
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MediaIMG"] + media.FileName;
                filePath = filePath.Replace("/", "\\");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            return false;
        }
        private void rptMedia_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/Media/EditMediaAlbum.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&item=" + itemId + "&galleryId=" + galleryId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    DeleteFileFromServer(itemId);
                    MediaAlbum.Delete(itemId);
                    string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&galleryId=" + groupMediaId.ToInvariantString();
                    WebUtils.SetupRedirect(this, pageUrl);
                }
                else if (e.CommandName.Equals("EditGroup"))
                {
                    groupMediaId = int.Parse(e.CommandArgument.ToString());
                    string url = SiteRoot + "/Media/EditPost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&item=" + groupMediaId;
                    WebUtils.SetupRedirect(this, url);
                }
                else if (e.CommandName.Equals("Featured"))
                {

                    itemId = int.Parse(e.CommandArgument.ToString());
                    var media = new MediaAlbum(itemId);
                    media.Featured = !media.Featured;
                    media.Save();
                    string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&galleryId=" + groupMediaId.ToInvariantString();
                    WebUtils.SetupRedirect(this, pageUrl);
                }
                else if (e.CommandName.Equals("IsPublish"))
                {

                    itemId = int.Parse(e.CommandArgument.ToString());
                    var media = new MediaAlbum(itemId);
                    media.IsPublish = !media.IsPublish;
                    media.Save();
                    string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&galleryId=" + groupMediaId.ToInvariantString();
                    WebUtils.SetupRedirect(this, pageUrl);
                }
                else if (e.CommandName.Equals("up"))
                {
                    List<MediaAlbum> lstMediaGroup = MediaAlbum.GetAllByModule(moduleId, Convert.ToInt32(galleryId));
                    int mediaGroupID = Convert.ToInt32(e.CommandArgument.ToString());

                    MediaAlbum currentFolder = null;

                    MediaAlbum swapFolder;

                    int currentItemIndex = -1;
                    int i = 0;

                    foreach (MediaAlbum sf in lstMediaGroup)
                    {
                        if (sf.ItemID == mediaGroupID)
                        {
                            currentFolder = sf;
                            currentItemIndex = i;
                        }

                        i += 1;


                    }

                    if (currentFolder == null) return;
                    if (currentItemIndex > 0)
                    {

                        swapFolder = lstMediaGroup[currentItemIndex - 1];

                        currentFolder.AlbumOrder = currentItemIndex - 1;
                        swapFolder.AlbumOrder = currentItemIndex;

                        currentFolder.Save();
                        swapFolder.Save();
                        string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&galleryId=" + groupMediaId.ToInvariantString();
                        WebUtils.SetupRedirect(this, pageUrl);
                    }
                }
                else if (e.CommandName.Equals("down"))
                {
                    List<MediaAlbum> lstMediaGroup = MediaAlbum.GetAllByModule(moduleId, Convert.ToInt32(galleryId));
                    int mediaGroupID = Convert.ToInt32(e.CommandArgument.ToString());

                    MediaAlbum currentFolder = null;

                    MediaAlbum swapFolder;

                    int currentItemIndex = -1;
                    int i = 0;

                    foreach (MediaAlbum sf in lstMediaGroup)
                    {
                        if (sf.ItemID == mediaGroupID)
                        {
                            currentFolder = sf;
                            currentItemIndex = i;
                        }

                        i += 1;


                    }

                    if (currentFolder == null) return;
                    if (currentItemIndex < lstMediaGroup.Count - 1)
                    {

                        swapFolder = lstMediaGroup[currentItemIndex + 1];

                        currentFolder.AlbumOrder = currentItemIndex + 1;
                        swapFolder.AlbumOrder = currentItemIndex;

                        currentFolder.Save();
                        swapFolder.Save();
                        string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&galleryId=" + groupMediaId.ToInvariantString();
                        WebUtils.SetupRedirect(this, pageUrl);
                    }
                }
            }
        }

        private void btnAddNewMediaAlbum_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Media/EditMediaAlbum.aspx?pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, url);
        }

        private void btnAddNewMediaGroup_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Media/EditPost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, url);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            groupMediaId = Convert.ToInt32(galleryId.ToString());
            //categoryID = Convert.ToInt32(drlCategory.SelectedValue);
            string _status = drlPublish.SelectedValue;
            searchBy = Convert.ToInt32(drlSearchBy.SelectedValue);
            keyword = txtSearch.Text;
            if (!string.IsNullOrEmpty(_status))
            {
                status = int.Parse(_status);
                if (_status == "1")
                {
                    isPublish = true;
                }
                else if (_status == "0")
                {
                    isPublish = false;
                }
            }
            string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&galleryId=" + groupMediaId.ToInvariantString();
            WebUtils.SetupRedirect(this, pageUrl);
        }
        private void PopulateStatus()
        {
            var status = SiteUtils.StringToDictionary(MediaResources.MediaStatus.ToString(), ",");

            drlPublish.DataSource = status;
            drlPublish.DataTextField = "Value";
            drlPublish.DataValueField = "Key";
            drlPublish.DataBind();
        }
        private void BindGroupMedia()
        {
            //List<MediaGroup> lstMediaGroup = new List<MediaGroup>();
            //lstMediaGroup = MediaGroup.GetAllBySite(siteSettings.SiteId);
            //drlGroupMedia.DataValueField = "ItemID";
            //drlGroupMedia.DataTextField = "NameGroup";
            //drlGroupMedia.DataSource = lstMediaGroup;
            //drlGroupMedia.DataBind();
            //drlGroupMedia.Items.Insert(0, new ListItem(MediaResources.GroupMultiMediaTitle, "-1"));
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
            BindGroupMedia();
            PopulateStatus();
            LoadMediaData();
            //LoadFeatured();
            LoadOrderBy();
            btnSearch.Text = Resources.MediaResources.MediaButtonSearch;
            //if (groupMediaId > 0)
            //{
            //    drlGroupMedia.SelectedValue = groupMediaId.ToString();
            //}
            if (status > -1)
            {
                drlPublish.SelectedValue = status.ToString();
            }
            if (searchBy > 0)
            {
                drlSearchBy.SelectedValue = SearchBy.ToString();
            }
            //if (categoryID > 0)
            //{
            //    drlCategory.SelectedValue = categoryID.ToString();
            //}
            //if (statusFeatureed > -1)
            //{
            //    ddlFeatured.SelectedValue = statusFeatureed.ToString();
            //}
        }
        //private void LoadFeatured()
        //{
        //    var status = SiteUtils.StringToDictionary(MediaResources.FeaturedStatus, ",");
        //    ddlFeatured.DataTextField = "Value";
        //    ddlFeatured.DataValueField = "Key";
        //    ddlFeatured.DataSource = status;
        //    ddlFeatured.DataBind();
        //}
        private void LoadCategories()
        {
            //List<CoreCategory> children = CoreCategory.GetChildren(siteSettings.CoreDuLieuDaPhuongTien);
            //drlCategory.DataSource = children;
            //drlCategory.DataTextField = "Name";
            //drlCategory.DataValueField = "ItemID";
            //drlCategory.DataBind();
            //drlCategory.Items.Insert(0, new ListItem(Resources.MediaResources.CategoryStatus, "-1"));
        }
        protected string GetFeatured(bool? Featured)
        {
            if (Featured.HasValue && Featured == true)
            {
                return MediaResources.MediaAlbumFeaturedTitle;
            }
            else
            {
                return "";
            }
        }
        private void LoadMediaData()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            List<MediaAlbum> lstMedia = new List<MediaAlbum>();
            lstMedia = MediaAlbum.GetPage(siteSettings.SiteId, moduleId, pageNumber, config.PageSize, groupMediaId, status, searchBy, isPublish, keyword, out totalPages);
            rptMedia.DataSource = lstMedia;
            rptMedia.DataBind();
            if (lstMedia.Count == 0)
            {
                DanhBanull.Visible = true;
                DanhBanull.Text = MediaResources.NoDataFound;
                pnlPostList.Visible = false;
            }
            string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&galleryId=" + groupMediaId.ToInvariantString();

            pgrDanhBa.PageURLFormat = pageUrl;
            pgrDanhBa.ShowFirstLast = true;
            pgrDanhBa.PageSize = config.PageSize;
            pgrDanhBa.PageCount = totalPages;
            pgrDanhBa.CurrentIndex = pageNumber;
            pnlDonViPager.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            galleryId = WebUtils.ParseInt32FromQueryString("galleryId", galleryId);

        }
        protected string GetPublishStatus(bool? publish)
        {
            if (publish.HasValue && publish == true)
            {
                return MediaResources.PublishedTitle;
            }
            else
            {
                return MediaResources.UnPublishTitle;
            }
        }
        public void PopulateLabels()
        {
            btnAddMultiple.Text = "Thêm nhiều ảnh";
            btnRemoveAll.Text = MediaResources.ButtonRemoveAll;
            btnAddNewMediaAlbum.Text = "Thêm mới ảnh";
            btnManageCategories.Text = "Quản lý phóng sự ảnh";
            btnManageCategories.PostBackUrl = SiteRoot + "/Media/ManagePostCategories.aspx?pageid=" + pageId + "&mid=" + moduleId;
            SiteUtils.AddConfirmButton(btnRemoveAll, "Dữ liệu xóa sẽ không khôi phục được, bạn có chắc chắn?");

            btnAddMultiple.Visible = AccessDelete(galleryId);
            btnAddNewMediaAlbum.Visible = AccessDelete(galleryId);
            //btnAddNewMediaGroup.Text = MediaResources.ButtonAddNewGroupMedia;
        }

        //private void loadFeatured()
        //{
        //    var status = SiteUtils.StringToDictionary(MediaResources.FeaturedStatus, ",");
        //    ddlFeatured.DataTextField = "Value";
        //    ddlFeatured.DataValueField = "Key";
        //    ddlFeatured.DataSource = status;
        //    ddlFeatured.DataBind();
        //}
        private void LoadOrderBy()
        {
            var status = SiteUtils.StringToDictionary(MediaResources.StoredStatus, ",");
            drlSearchBy.DataTextField = "Value";
            drlSearchBy.DataValueField = "Key";
            drlSearchBy.DataSource = status;
            drlSearchBy.DataBind();
        }
        protected virtual void LoadSettings()
        {

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new MediaConfiguration(getModuleSettings);

            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            searchBy = WebUtils.ParseInt32FromQueryString("order", searchBy);
            groupMediaId = WebUtils.ParseInt32FromQueryString("galleryId", galleryId);

            string _status = WebUtils.ParseStringFromQueryString("status", string.Empty);
            if (!string.IsNullOrEmpty(_status))
            {
                status = int.Parse(_status);
                if (_status == "1")
                {
                    isPublish = true;
                }
                else if (_status == "0")
                {
                    isPublish = false;
                }
            }
            string _statusFeatured = WebUtils.ParseStringFromQueryString("status", string.Empty);
            if (!string.IsNullOrEmpty(_statusFeatured))
            {
                statusFeatureed = int.Parse(_statusFeatured);
                if (_statusFeatured == "1")
                {
                    isFeatured = true;
                }
                else if (_statusFeatured == "0")
                {
                    isFeatured = false;
                }
            }
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            foreach (RepeaterItem ri in rptMedia.Items)
            {
                CheckBox chkFlag = ri.FindControl("chk") as CheckBox;
                if (chkFlag.Checked)
                {
                    deleteNumber++;
                    int itemid = Convert.ToInt32((ri.FindControl("repeaterID") as Literal).Text);
                    MediaAlbum.Delete(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                 + "?pageid=" + PageId.ToInvariantString()
                 + "&mid=" + ModuleId.ToInvariantString()
                 + "&galleryId=" + groupMediaId.ToInvariantString();
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }
        protected bool AccessEdit(int galleryId)
        {
            if (siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleManageMedia))
            {
                return true;
            }
            return false;
        }

        protected bool AccessDelete(int galleryId)
        {
            if (siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleManageMedia))
            {
                return true;
            }
            return false;
        }
    }
}
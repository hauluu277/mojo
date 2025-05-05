using MediaAlbumFeature.Business;
using MediaGroupFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.UI.Media.Component;
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

namespace AudioFeature.UI
{
    public partial class CategoriesListAudio : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private string createByUser = string.Empty;
        private string dateParam = string.Empty;
        private DateTime? createDate = null;


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
        private int step = 0;
        private string isCaNhan = "false";


        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
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
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSearch.Click += btnSearch_Click;
            btnAddNewMediaGroup.Click += btnAddNewMediaGroup_Click;
            btnReload.Click += BtnReload_Click;
            btnRemoveAll.Click += btnRemoveAll_Click;
            rptMedia.ItemCommand += rptMedia_ItemCommand;
            rptMedia.ItemDataBound += rptMedia_ItemDataBound;
        }

        protected void BtnReload_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Audio/ManageAudio.aspx?iscanhan=" + isCaNhan;
            WebUtils.SetupRedirect(this, url);
        }

        private void btnAddNewMediaGroup_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Audio/EditPost.aspx?mid=" + moduleId + "&pageid=" + pageId;
            WebUtils.SetupRedirect(this, url);
        }
        private void LoadMediaGroup()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;

            List<md_AudioGroup> listAudio = new List<md_AudioGroup>();
            listAudio = md_AudioGroup.GetPageManage(createByUser, createDate, siteSettings.SiteId, moduleId, pageNumber, config.PageSize, keyword, categoryID, isPublish, siteUser.UserId, MediaGalleryConstant.RoleSystem, step, isCaNhan.ToBoolOrFalse(), out totalPages);

            rptMedia.DataSource = listAudio;
            rptMedia.DataBind();
            if (listAudio.Count == 0)
            {
                DanhBanull.Visible = true;
                DanhBanull.Text = MediaResources.NoDataFound;
                pnlPostList.Visible = false;
            }
            string pageUrl = SiteRoot + "/Audio/ManageAudio.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&categoryID=" + categoryID.ToInvariantString()
                  + "&keyword=" + keyword
                  + "&createByUser=" + txtCreateByUser.Text
                  + "&createDate=" + dpCreateDate.Text
                  + "&iscanhan=" + isCaNhan
                  + "&step=" + step
                  + "&pagenumber={0}";

            pgrDanhBa.PageURLFormat = pageUrl;
            pgrDanhBa.ShowFirstLast = true;
            pgrDanhBa.PageSize = config.PageSize;
            pgrDanhBa.PageCount = totalPages;
            pgrDanhBa.CurrentIndex = pageNumber;
            pnlDonViPager.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void rptMedia_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, MediaResources.ConfirmDeleteWarning);

                ImageButton btnPublish = e.Item.FindControl("btnPublish") as ImageButton;
                SiteUtils.AddConfirmButton(btnPublish, "Bạn có chắc chắn muốn thay đổi trạng thái hiển thị?");
            }
        }
        private bool DeleteFileFromServer(int itemID)
        {
            MediaAlbum media = new MediaAlbum(itemID);
            if (media.TypeData == 2)
            {
                string filePath_IMG = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MultimediaFileFolder"] + media.ImageVideo;
                filePath_IMG = filePath_IMG.Replace("/", "\\");
                if (File.Exists(filePath_IMG))
                {
                    File.Delete(filePath_IMG);
                }
                string filePath_VIDEO = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MultimediaFileFolder"] + media.FilePath;
                filePath_VIDEO = filePath_VIDEO.Replace("/", "\\");
                if (File.Exists(filePath_VIDEO))
                {
                    File.Delete(filePath_VIDEO);
                }
                return true;
            }
            else if (media.TypeData == 1)
            {
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MultimediaFileFolder"] + media.FilePath;
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
                    WebUtils.SetupRedirect(this, SiteRoot + "/Audio/Editpost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&item=" + itemId);
                }
                else if (e.CommandName.Equals("IsPublish"))
                {

                    itemId = int.Parse(e.CommandArgument.ToString());
                    var media = new MediaGroup(itemId);
                    media.IsPublish = !media.IsPublish;
                    media.Save();
                    WebUtils.SetupRedirect(this, SiteRoot + "/Audio/managepostcategories.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&iscanhan=" + isCaNhan + "&step=" + step);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    List<MediaAlbum> lstAlbum = MediaAlbum.GetAllByModule(moduleId, itemId);
                    foreach (var item in lstAlbum)
                    {
                        DeleteFileFromServer(item.ItemID);
                    }
                    md_AudioGroup group = new md_AudioGroup(itemId);
                    string filePath_IMG = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MultimediaFileFolder"] + group.FilePath;
                    filePath_IMG = filePath_IMG.Replace("/", "\\");
                    if (File.Exists(filePath_IMG))
                    {
                        File.Delete(filePath_IMG);
                    }
                    MediaAlbum.DeleteByGroupID(itemId);


                    md_AudioGroup.Delete(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/Audio/ManageAudio.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&iscanhan=" + isCaNhan + "&step=" + step);
                }
                else if (e.CommandName.Equals("down"))
                {
                    List<md_AudioGroup> lstMediaGroup = md_AudioGroup.GetAllByModule(moduleId);
                    int mediaGroupID = Convert.ToInt32(e.CommandArgument.ToString());

                    md_AudioGroup currentFolder = null;

                    md_AudioGroup swapFolder;

                    int currentItemIndex = -1;
                    int i = 0;

                    foreach (md_AudioGroup sf in lstMediaGroup)
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

                        currentFolder.GroupOrder = currentItemIndex - 1;
                        swapFolder.GroupOrder = currentItemIndex;

                        currentFolder.Save();
                        swapFolder.Save();
                        WebUtils.SetupRedirect(this, SiteRoot + "/Audio/ManageAudio.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&iscanhan=" + isCaNhan);
                    }
                }
                else if (e.CommandName.Equals("up"))
                {
                    List<md_AudioGroup> lstMediaGroup = md_AudioGroup.GetAllByModule(moduleId);
                    int mediaGroupID = Convert.ToInt32(e.CommandArgument.ToString());

                    md_AudioGroup currentFolder = null;

                    md_AudioGroup swapFolder;

                    int currentItemIndex = -1;
                    int i = 0;

                    foreach (md_AudioGroup sf in lstMediaGroup)
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

                        currentFolder.GroupOrder = currentItemIndex + 1;
                        swapFolder.GroupOrder = currentItemIndex;

                        currentFolder.Save();
                        swapFolder.Save();
                        WebUtils.SetupRedirect(this, SiteRoot + "/Audio/ManageAudio.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&iscanhan=" + isCaNhan + "&step=" + step);
                    }
                }
            }

        }
        protected string FormatTrainingtDate(DateTime startDate)
        {
            if (startDate != null)
            {
                return string.Format("{0:dd/MM/yyyy}", startDate);
            }
            return "";
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
            PopulateStatus();
            LoadMediaGroup();
            btnSearch.Text = Resources.MediaResources.MediaButtonSearch;
            btnReload.Text = "Tải lại";
            if (status > -1)
            {
                drlPublish.SelectedValue = status.ToString();
            }

            txtSearch.Text = keyword;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string _status = drlPublish.SelectedValue;
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
            string pageUrl = SiteRoot + "/Audio/ManageAudio.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                   + "&status=" + status.ToInvariantString()
                  + "&keyword=" + keyword
                  + "&createByUser=" + txtCreateByUser.Text
                  + "&createDate=" + dpCreateDate.Text
                  + "&step=" + step
                  + "&iscanhan=" + isCaNhan
                  + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }
        private void PopulateStatus()
        {
            var listStatus = new List<ListItem>();
            listStatus.Add(new ListItem { Text = "Tất cả", Value = "-1" });
            listStatus.Add(new ListItem { Text = "Đã phát hành", Value = "1" });
            listStatus.Add(new ListItem { Text = "Chưa phát hành", Value = "0" });


            drlPublish.DataSource = listStatus;
            drlPublish.DataTextField = "Text";
            drlPublish.DataValueField = "Value";
            drlPublish.DataBind();
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            step = WebUtils.ParseInt32FromQueryString("step", step);
            createByUser = WebUtils.ParseStringFromQueryString("createByUser", createByUser);
            dateParam = WebUtils.ParseStringFromQueryString("createDate", dateParam);
            if (!string.IsNullOrEmpty(dateParam))
            {
                createDate = dateParam.ToDateTime();
            }
        }
        public void PopulateLabels()
        {
            //btnAddNewMediaGroup.Text = MediaResources.GroupMultiMediaTitle;
            btnRemoveAll.Text = MediaResources.ButtonRemoveAll;
            btnAddNewMediaGroup.Text = "Thêm mới Audio";
            btnManageData.Text = MediaResources.ManageMediaDataTitle;
            btnManageData.PostBackUrl = SiteRoot + "/Media/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            SiteUtils.AddConfirmButton(btnRemoveAll, "Dữ liệu sẽ bị xóa vĩnh viễn. Bạn có thực sự muốn xóa");
            btnRemoveAll.Visible = false;
        }

        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new MediaConfiguration(getModuleSettings);

            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            categoryID = WebUtils.ParseInt32FromQueryString("categoryID", categoryID);
            isCaNhan = WebUtils.ParseStringFromQueryString("iscanhan", isCaNhan);
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
                    MediaGroup.Delete(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                string pageUrl = SiteRoot + "/Audio/ManageAudio.aspx"
                      + "?pageid=" + PageId.ToInvariantString()
                      + "&mid=" + ModuleId.ToInvariantString()
                      + "&categoryID=" + categoryID.ToInvariantString()
                       + "&status=" + status.ToInvariantString()
                      + "&keyword=" + keyword
                      + "&createByUser=" + createByUser
                      + "&createDate=" + createDate
                      + "&iscanhan=" + isCaNhan
                      + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }
        protected string GetUrlGalleryManage(int galleryID)
        {
            return SiteRoot + "/Media/ManagePost.aspx?galleryId=" + galleryID;
        }


        protected bool AccessEdit(int createByID, int step)
        {
            if (siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleManageMedia))
            {
                return true;
            }
          
            return false;
        }

        protected bool AccessDelete(int createByID, int step)
        {
            if (siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleManageMedia))
            {
                return true;
            }
                        return false;

        }
    }
}
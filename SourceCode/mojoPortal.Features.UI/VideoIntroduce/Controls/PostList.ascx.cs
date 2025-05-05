using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoIntroduceFeatures.Business;

namespace VideoIntroduceFeatures.UI
{
    public partial class PostList : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;

        protected VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int isPublic = -1;
        private int typePlayer = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private string keyword = string.Empty;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private int siteID = -1;
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
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
        public VideoIntroduceConfiguration Config
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
            rptMedia.ItemCommand += rptMedia_ItemCommand;
            rptMedia.ItemDataBound += rptMedia_ItemDataBound;
            btnAddNew.Click += btnAddNew_Click;
            btnRemoveAll.Click += btnRemoveAll_Click;
            btnSearch.Click += btnSearch_Click;
            btnViewVideo.Click += btnViewVideo_Click;
        }

        void btnViewVideo_Click(object sender, EventArgs e)
        {
            SiteUtils.RedirectToUrl(SiteRoot + "/video");
        }

        protected void rptMedia_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, "Bạn có chắc chắn muốn xóa bỏ video hoặc link youtube này?");

                ImageButton btnChangePublic = e.Item.FindControl("btnChangePublic") as ImageButton;
                SiteUtils.AddConfirmButton(btnChangePublic, "Bạn có chắc chắn muốn thay đổi trạng thái hiển thị của video hoặc link youtube này?");

                ImageButton imagebuttonIsHot = e.Item.FindControl("imagebuttonIsHot") as ImageButton;
                SiteUtils.AddConfirmButton(imagebuttonIsHot, "Bạn có chắn chắn muốn thay đổi trạng thái nổi bật?");
                //
            }
        }

        protected void rptMedia_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/VideoIntroduce/EditPost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&item=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    DeleteFileFromServer(itemId);
                    DeleteFileIMGFromServer(itemId);
                    VideoIntroduce.Delete(itemId);
                    string pageUrl = SiteRoot + "/VideoIntroduce/managepost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ispublic=" + isPublic.ToInvariantString()
                   + "&typeplayer=" + typePlayer.ToInvariantString()
                   + "&keyword=" + keyword.ConvertToFTS()
                   + "&pagenumber=1";
                    SiteUtils.RedirectToUrl(pageUrl);
                }
                else if (e.CommandName.Equals("EditPublic"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    VideoIntroduce doc = new VideoIntroduce(itemId);
                    if (doc != null)
                    {
                        if (doc.IsPublic == true)
                        {
                            doc.IsPublic = false;
                            doc.Save();
                        }
                        else
                        {
                            doc.IsPublic = true;
                            doc.Save();
                        }
                    }
                    string pageUrl = SiteRoot + "/VideoIntroduce/managepost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ispublic=" + isPublic.ToInvariantString()
                   + "&typeplayer=" + typePlayer.ToInvariantString()
                   + "&keyword=" + keyword.ConvertToFTS()
                   + "&pagenumber=" + pageNumber;
                    SiteUtils.RedirectToUrl(pageUrl);
                }
                else if (e.CommandName.Equals("EditIsHotState"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    VideoIntroduce doc = new VideoIntroduce(itemId);
                    if (doc != null)
                    {
                        if (doc.IsHot == true)
                        {
                            doc.IsHot = false;
                            doc.Save();
                        }
                        else
                        {
                            VideoIntroduce.UpdateIsHot(siteSettings.SiteId);
                            doc.IsHot = true;
                            doc.Save();
                        }
                    }
                    string pageUrl = SiteRoot + "/VideoIntroduce/managepost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ispublic=" + isPublic.ToInvariantString()
                   + "&typeplayer=" + typePlayer.ToInvariantString()
                   + "&keyword=" + keyword.ConvertToFTS()
                   + "&pagenumber=" + pageNumber;
                    SiteUtils.RedirectToUrl(pageUrl);
                }
            }
        }
        private bool DeleteFileFromServer(int id)
        {
            VideoIntroduce video = new VideoIntroduce(id);
            if (video != null)
            {
                string filePath_VIDEO = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + video.Video;
                filePath_VIDEO = filePath_VIDEO.Replace("/", "\\");
                if (File.Exists(filePath_VIDEO))
                {
                    File.Delete(filePath_VIDEO);
                }
            }
            return true;
        }
        private bool DeleteFileIMGFromServer(int id)
        {
            VideoIntroduce video = new VideoIntroduce(id);
            if (video != null)
            {
                string filePath_VIDEO = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + video.ImageVideo;
                filePath_VIDEO = filePath_VIDEO.Replace("/", "\\");
                if (File.Exists(filePath_VIDEO))
                {
                    File.Delete(filePath_VIDEO);
                }
            }
            return true;
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
                    DeleteFileFromServer(itemId);
                    DeleteFileIMGFromServer(itemId);
                    VideoIntroduce.Delete(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                string pageUrl = SiteRoot + "/VideoIntroduce/managepost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ispublic=" + isPublic.ToInvariantString()
              + "&typeplayer=" + typePlayer.ToInvariantString()
              + "&keyword=" + keyword.ConvertToFTS()
              + "&pagenumber=" + pageNumber;
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }

        void btnAddNew_Click(object sender, EventArgs e)
        {
            string pageUrl = SiteRoot + "/VideoIntroduce/editpost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, pageUrl);
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            isPublic = int.Parse(drlStatus.SelectedValue);
            typePlayer = int.Parse(drlTypeVideoPlayer.SelectedValue);
            keyword = txtSearch.Text;

            string pageUrl = SiteRoot + "/VideoIntroduce/managepost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ispublic=" + isPublic.ToInvariantString()
           + "&typeplayer=" + typePlayer.ToInvariantString()
           + "&keyword=" + keyword.ConvertToFTS()
           + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
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
            BindStatus();
            BindTypePlayer();
            LoadVideo();
            btnSearch.Text = "Tìm kiếm";

        }
        private void LoadVideo()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            List<VideoIntroduce> lstMenu = new List<VideoIntroduce>();
            lstMenu = VideoIntroduce.GetPage(siteID, isPublic, typePlayer, keyword.ConvertToFTS(), pageNumber, 10, out totalPages);
            rptMedia.DataSource = lstMenu;
            rptMedia.DataBind();
            if (lstMenu.Count == 0)
            {
                Videonull.Visible = true;
                Videonull.Text = "Không tìm thấy dữ liệu";
                pnlPostList.Visible = false;
            }
            string pageUrl = SiteRoot + "/VideoIntroduce/managepost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ispublic=" + isPublic.ToInvariantString()
           + "&typeplayer=" + typePlayer.ToInvariantString()
           + "&keyword=" + keyword.ConvertToFTS()
           + "&pagenumber={0}";

            pgrVideo.PageURLFormat = pageUrl;
            pgrVideo.ShowFirstLast = true;
            pgrVideo.PageSize = 10;
            pgrVideo.PageCount = totalPages;
            pgrVideo.CurrentIndex = pageNumber;
            pnlVideoPager.Visible = (totalPages > 1);
            txtSearch.Text = keyword;
            try
            {
                drlStatus.SelectedValue = isPublic.ToString();
                drlTypeVideoPlayer.SelectedValue = typePlayer.ToString();
            }
            catch
            {

            }

        }
        private void LoadParams()
        {
            //pageId = WebUtils.ParseInt32FromQueryString("id", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
        }
        private void BindStatus()
        {
            List<ListItem> ListStatus = new List<ListItem>();
            ListStatus.Add(new ListItem { Text = "--- Trạng thái ---", Value = "-1", });
            ListStatus.Add(new ListItem { Text = "Xuất bản", Value = "1", });
            ListStatus.Add(new ListItem { Text = "Chưa xuất bản", Value = "0", });
            drlStatus.DataValueField = "Value";
            drlStatus.DataTextField = "Text";
            drlStatus.DataSource = ListStatus;
            drlStatus.DataBind();
        }
        private void BindTypePlayer()
        {
            List<ListItem> ListTypePlayer = new List<ListItem>();
            ListTypePlayer.Add(new ListItem { Text = "--- Kiểu loại video ---", Value = "-1", });
            ListTypePlayer.Add(new ListItem { Text = "Video", Value = VideoIntroduceConstant.VideoPlayer.ToString(), });
            ListTypePlayer.Add(new ListItem { Text = "Video Youtube", Value = VideoIntroduceConstant.YoutubePlayer.ToString(), });
            drlTypeVideoPlayer.DataValueField = "Value";
            drlTypeVideoPlayer.DataTextField = "Text";
            drlTypeVideoPlayer.DataSource = ListTypePlayer;
            drlTypeVideoPlayer.DataBind();
        }
        public void PopulateLabels()
        {
            btnAddNew.Text = "Thêm mới";
            btnRemoveAll.Text = "Xóa tất cả";
            btnViewVideo.Text = "Xem danh sách video";
            SiteUtils.AddConfirmButton(btnRemoveAll, "Bạn có chắc chắn muốn xóa tất cả dữ liệu?");
            //txtSearch.Text = keyword;
        }
        protected virtual void LoadSettings()
        {

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new VideoIntroduceConfiguration(getModuleSettings);

            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            typePlayer = WebUtils.ParseInt32FromQueryString("typeplayer", typePlayer);
            isPublic = WebUtils.ParseInt32FromQueryString("ispublic", isPublic);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        protected string GetState(bool status)
        {
            string nameStatus = string.Empty;
            if (status)
            {
                nameStatus = "Video đang được hiển thị, bạn có muốn thay đổi?";
            }
            else
            {
                nameStatus = "Video chưa được hiển thị, bạn có muốn thay đổi?";
            }
            return nameStatus;
        }
        protected string GetStateIsHot(bool status)
        {
            string nameStatus = string.Empty;
            if (status)
            {
                nameStatus = "Video đang là nổi bật, bạn có muốn thay đổi?";
            }
            else
            {
                nameStatus = "Video chưa được thiết lập nổi bật, bạn có muốn thay đổi?";
            }
            return nameStatus;
        }
        protected string GetStateTypeVideo(int typePlayer)
        {

            if (typePlayer == VideoIntroduceConstant.VideoPlayer)
            {
                return "Video";
            }
            else
            {
                return "Youtube";
            }
        }
    }
}
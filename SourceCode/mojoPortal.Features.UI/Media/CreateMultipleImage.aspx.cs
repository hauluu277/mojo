using MediaAlbumFeature.Business;
using MediaGroupFeature.Business;
using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using System.IO;
using System.Configuration;
using System.Drawing;
using mojoPortal.Business.WebHelpers;

namespace MediaAlbumFeature.UI
{
    public partial class CreateMultipleImage : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        protected int mediaGroupID = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private MediaConfiguration config = new MediaConfiguration();
        private MediaAlbum mediaAlbum;
        private Guid featureGuid = Guid.Empty;
        private string name = string.Empty;
        private string names = string.Empty;
        private string widthImage = string.Empty;
        private string fileName = string.Empty;
        public int ModuleID
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public int PageID
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        private bool userCanEdit = false;


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleGalleryManage))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            //SecurityHelper.DisableBrowserCache();
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        private void BindMediaGroup()
        {
            //MediaGroup mediaGroup = new MediaGroup();
            //List<MediaGroup> lstMediaGroup = new List<MediaGroup>();
            //lstMediaGroup.Add(new MediaGroup(mediaGroupID));
            //lstMediaGroup =  MediaGroup.GetAllBySite(siteSettings.SiteId);
            //drlCategory.DataTextField = "NameGroup";
            //drlCategory.DataValueField = "ItemID";
            //drlCategory.DataSource = lstMediaGroup;
            //drlCategory.DataBind();
            //drlCategory.Items.Insert(0, new ListItem("Thư viện ảnh", ""));
        }

        private void PopulateControls()
        {
            BindMediaGroup();
            hdfCategoryID.Value = mediaGroupID.ToString();
            //if (mediaGroupID > 0)
            //{
            //    drlCategory.SelectedValue = mediaGroupID.ToString();
            //}
        }


        private void PopulateLabels()
        {
            btnCancel.Text = "Quay lại";
            btnCancel.PostBackUrl = SiteRoot + "/Media/ManagePost.aspx?mid=" + moduleId + "&pageid=" + pageId + "&galleryId=" + mediaGroupID;
            hdfSiteID.Value = siteSettings.SiteId.ToString();
            TitleControl.Visible = false;
            TitleControl.EditUrl = SiteRoot + "/Media/EditMediaAlbum.aspx?mid=" + moduleId + "&pageid=" + pageId + "&galleryId=" + mediaGroupID;
            Title = SiteUtils.FormatPageTitle(siteSettings, "Thêm nhiều ảnh");
            heading.Text = "Thêm nhiều ảnh";

            if (userCanEdit)
            {
                TitleControl.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/Media/CreateMultipleImage.aspx?pageid=" + pageId.ToInvariantString()
                    + "&amp;mid=" + moduleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='Thêm nhiều ảnh'>Thêm nhiều ảnh</a>"
                    + "&nbsp;<a href='"
                    + SiteRoot
                    + "/Media/ManagePost.aspx?pageid=" + pageId.ToInvariantString()
                    + "&amp;mid=" + moduleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='"
                    + BlogResources.Administration + "'>"
                    + BlogResources.Administration + "</a>"

                    ;
            }
            TitleControl.EditText = config.NameTitle;
            Title = SiteUtils.FormatPageTitle(siteSettings, "Tải nhiều hình ảnh");
            hdfModuleId.Value = moduleId.ToInvariantString();

        }

        private void LoadSettings()
        {
            if (itemId > -1)
            {
                mediaAlbum = new MediaAlbum(itemId);
                if (mediaAlbum.ModuleID != moduleId) { mediaAlbum = null; }
            }
            userCanEdit = UserCanEditModule(moduleId);
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            siteId = siteSettings.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
            mediaGroupID = WebUtils.ParseInt32FromQueryString("galleryId", mediaGroupID);
        }
        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
        }
        #endregion
    }
}
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
    public partial class EditMediaAlbum : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        protected int galleryId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private MediaConfiguration config = new MediaConfiguration();
        private MediaAlbum mediaAlbum;
        private Guid featureGuid = Guid.Empty;
        private string name = string.Empty;
        private string names = string.Empty;
        private string widthImage = string.Empty;
        private string fileName = string.Empty;

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

            LoadParams();
            LoadSettings();
           
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }
        private void BindCategory()
        {
            //List<CoreCategory> children = CoreCategory.GetChildren(siteSettings.CoreDuLieuDaPhuongTien);
            //drlCategory.DataSource = children;
            //drlCategory.DataTextField = "Name";
            //drlCategory.DataValueField = "ItemID";
            //drlCategory.DataBind();
            //drlCategory.Items.Insert(0, new ListItem(MediaResources.MultimediaTypeTitle, ""));
        }
        private void BindMediaGroup()
        {
            //MediaGroup mediaGroup = new MediaGroup();
            //List<MediaGroup> lstMediaGroup = new List<MediaGroup>();
            //lstMediaGroup = MediaGroup.GetAllBySite(siteSettings.SiteId);
            //drlGroupMedia.DataTextField = "NameGroup";
            //drlGroupMedia.DataValueField = "ItemID";
            //drlGroupMedia.DataSource = lstMediaGroup;
            //drlGroupMedia.DataBind();
            //drlGroupMedia.Items.Insert(0, new ListItem(MediaResources.GroupMultiMediaTitle, ""));
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

                string filePath_thumb = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MediaIMG_thumb"] + media.FileName;
                filePath_thumb = filePath_thumb.Replace("/", "\\");
                if (File.Exists(filePath_thumb))
                {
                    File.Delete(filePath_thumb);
                }

            }
            return false;
        }
        private void PopulateControls()
        {

            BindCategory();
            BindMediaGroup();
            loadTypeData();
            SiteUtils.AddConfirmButton(btnDel, MediaResources.ConfirmDeleteWarning);
            if (mediaAlbum != null)
            {
                txtFileName.Text = mediaAlbum.FileName;
                //lbCreateByUser.Text = mediaAlbum.CreatedByUser;
                //pnUserCreate.Visible = true;
                ccboxPublish.Checked = mediaAlbum.IsPublish;
                txtTitle.Text = mediaAlbum.Description;
                //txtDescription.Text = mediaAlbum.Description;
                ccboxFeatured.Checked = mediaAlbum.Featured;
                //drlCategory.SelectedValue = mediaAlbum.CategoryID.ToString();
                //drlGroupMedia.SelectedValue = mediaAlbum.GroupMediaID.ToString();
                TypeDataChange();
                hfFilePath.Value = mediaAlbum.FilePath;
                hfFileName.Value = mediaAlbum.FileName;
                RqfvFileImage.Visible = false;
                if (mediaAlbum.TypeData == 2)// video
                {
                    RqfVideo.Visible = false;
                }
                //rvfFileUpload.Visible = false;
                //rfvFileImage.Visible = false;
                hfImageFilePath.Value = mediaAlbum.ImageVideo;
                txtCode.Text = mediaAlbum.EmbedCode;
                if (mediaAlbum.FilePath != "" || mediaAlbum.ImageVideo != "")
                {
                    pnIMG.Visible = true;
                    if (mediaAlbum.TypeData == 2)
                    {
                        ImageID.Src = mediaAlbum.FilePath;
                    }
                    else
                    {
                        ImageID.Src = mediaAlbum.FilePath;


                    }
                }
            }
            else
            {
                pnVideo.Visible = false;
                pnCode.Visible = false;
                RqfVideo.Visible = false;
                RgExVideo.Visible = false;
                RqfCode.Visible = false;
            }
        }
        public void loadTypeData()
        {
            var status = SiteUtils.StringToDictionary(MediaResources.TypeDataStatus.ToString(), ",");
            ddlTypeData.DataSource = status;
            ddlTypeData.DataTextField = "Value";
            ddlTypeData.DataValueField = "Key";
            ddlTypeData.DataBind();
            if (mediaAlbum != null)
            {
                ddlTypeData.SelectedValue = mediaAlbum.TypeData.ToString();
            }
        }
        private bool Save()
        {
            Page.Validate("albumGroup");
            string fileOld = "";
            if (!Page.IsValid) return false;

            if (mediaAlbum == null)
            {
                mediaAlbum = new MediaAlbum();
                mediaAlbum.TotalView = 0;
                mediaAlbum.CreatedDate = DateTime.Now;

            }
            else
            {

                fileOld = mediaAlbum.FilePath;
                mediaAlbum = new MediaAlbum(itemId);
                mediaAlbum.LastModUtc = DateTime.Now;
                mediaAlbum.LastModUserGuid = siteUser.UserGuid;
            }
            mediaAlbum.Description = txtTitle.Text;
            mediaAlbum.ItemUrl = SuggestUrl();
            mediaAlbum.ModuleID = moduleId;
            mediaAlbum.SiteID = siteId;
            mediaAlbum.UserGuid = siteUser.UserGuid;
            mediaAlbum.FileName = txtFileName.Text;
            mediaAlbum.Description = txtDescription.Text;
            mediaAlbum.IsPublish = ccboxPublish.Checked;
            mediaAlbum.TypeData = MediaConstant.IMAGE;
            //mediaAlbum.TypeData = Convert.ToInt32(ddlTypeData.SelectedValue);
            //mediaAlbum.CategoryID = Convert.ToInt32(drlCategory.SelectedValue);
            //mediaAlbum.GroupMediaID = Convert.ToInt32(drlGroupMedia.SelectedValue);
            mediaAlbum.Featured = ccboxFeatured.Checked;
            mediaAlbum.CreatedByUser = siteUser.Name;
            mediaAlbum.CategoryID = galleryId;



            if (ddlTypeData.SelectedValue == "1") //img
            {
                mediaAlbum.EmbedCode = "";
                if (!neatUpLoadImage.HasFile && neatUpLoadImage.ContentLength == 0)
                {
                    if (itemId > -1)
                    {
                        mediaAlbum.FilePath = hfFilePath.Value;
                    }
                }
                else
                {
                    if (itemId > -1)
                    {
                        DeleteFileFromServer(itemId);
                    }
                    if (neatUpLoadImage.HasFile && neatUpLoadImage.ContentLength > 0)
                    {

                        if (!SaveImageUrl(out fileName, out widthImage))
                        {
                            return false;
                        }
                        mediaAlbum.FileName = fileName;
                        mediaAlbum.FilePath = fileName;
                    }
                }
            }


            mediaAlbum.EmbedCode = txtCode.Text;

            mediaAlbum.ItemUrl = SuggestUrl();
            mediaAlbum.Save();
            return true;
        }
        private void PopulateLabels()
        {
            pnIMG.Visible = false;
            btnSubmit.Text = MediaResources.GroupMediaButtonSave;
            btnCancel.Text = MediaResources.GroupMediaButtonBack;
            btnCancel.PostBackUrl = SiteRoot + "/Media/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&galleryId=" + galleryId;
            btnDel.Text = MediaResources.GroupMediaButtonDelete;
            lblEmbedCode.Text = MediaResources.NodeEmbedCode;
            if (itemId > 0)
            {
                heading.Text = "Cập nhật ảnh";
            }
            else
            {
                heading.Text = "Thêm mới ảnh";
            }
            if (mediaAlbum != null)
            {
                legendMediaAlbum.InnerText = MediaResources.MediaAlbumEditTitle;
                Title = SiteUtils.FormatPageTitle(siteSettings, Resources.MediaResources.EditMutilMediaTitle);

            }
            else
            {
                legendMediaAlbum.InnerText = MediaResources.MediaAlbumAddNewTitle;
                Title = SiteUtils.FormatPageTitle(siteSettings, Resources.MediaResources.AddNewMultilMediaTitle);


            }
        }

        private void LoadSettings()
        {
            if (itemId > -1)
            {
                mediaAlbum = new MediaAlbum(itemId);
                if (mediaAlbum.ModuleID != moduleId) { mediaAlbum = null; }
            }
        }
        private bool SaveImageUrl(out string fileName, out string widthImage)
        {
            String pathToApplicationsFolder = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["MediaIMG"]);
            if (!Directory.Exists(pathToApplicationsFolder))
            {
                Directory.CreateDirectory(pathToApplicationsFolder);
            }

            String pathToApplicationsFolder_thumb = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["MediaIMG_thumb"]);
            if (!Directory.Exists(pathToApplicationsFolder))
            {
                Directory.CreateDirectory(pathToApplicationsFolder);
            }
            string dirFullPath_thumbSlide = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["MediaIMG_thumbSlide"]);
            if (!Directory.Exists(dirFullPath_thumbSlide))
            {
                Directory.CreateDirectory(dirFullPath_thumbSlide);
            }
            bool flag = false;
            int width = 0;
            int height = 0;
            fileName = string.Empty;
            string fileName_thumb = string.Empty;
            widthImage = string.Empty;
            try
            {
                //Check valid file upload
                if (neatUpLoadImage.HasFile && neatUpLoadImage.ContentLength > 0)
                {

                    string fileExtension = Path.GetExtension(neatUpLoadImage.FileName);
                    Double fileSize = neatUpLoadImage.ContentLength / 1024;

                    //Kiem tra ten mo rong file upload
                    if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedImageFileExtensions"))
                    {
                        lblFileUrlError.Text = MediaResources.FileFormatUploadError;
                        return false;
                    }
                    //Kiem tra kich thuoc file upload
                    if (!SiteUtils.IsValidFileSize(fileSize, "AllowedImageSize"))
                    {
                        lblFileUrlError.Text = "File vượt quá độ lớn cho phép";
                        return false;
                    }

                    string path = Server.MapPath("~/" + ConfigurationManager.AppSettings["MediaIMG"]);
                    string path_thumb = Server.MapPath("~/" + ConfigurationManager.AppSettings["MediaIMG_thumbSlide"]);
                    string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    string fileNameIMGSlide_thumb = string.Empty;
                    System.Drawing.Image image = System.Drawing.Image.FromStream(neatUpLoadImage.FileContent);
                    neatUpLoadImage.FileContent.Close();
                    int resizeWidth;
                    int.TryParse(MediaConstant.ImageMaxWidth, out resizeWidth);
                    int resizeHeight;
                    int.TryParse(MediaConstant.ImageMaxHeight, out resizeHeight);
                    int thumbnailWidth;
                    int.TryParse(MediaConstant.ImageMaxThumbnailWidth, out thumbnailWidth);
                    int thumbnailHeight;
                    int.TryParse(MediaConstant.ImageMaxThumbnailHeight, out thumbnailHeight);
                    SiteUtils.ResizeImage(ref width, ref height, resizeWidth, resizeHeight, image.Width, image.Height);
                    fileName = path + guid + fileExtension;
                    fileName_thumb = path_thumb + guid + fileExtension;
                    fileNameIMGSlide_thumb = path_thumb + guid + fileExtension;
                    if (height != 0)
                    {
                        // lưu file thường
                        using (Bitmap bitmap = new Bitmap(image, width, height))
                        {
                            widthImage = image.Width.ToString();
                            bitmap.Save(fileName, image.RawFormat);
                        }

                    }
                    else
                    {
                        //lưu file thường
                        using (Bitmap bitmap = new Bitmap(image, image.Width, image.Height))
                        {
                            widthImage = image.Width.ToString();
                            bitmap.Save(fileName, image.RawFormat);
                        }
                        //nuImageUrl.MoveTo(fileName, MoveToOptions.Overwrite);
                        //fuImageUrl.PostedFile.SaveAs(fileName);
                    }
                    SiteUtils.ResizeImage(ref width, ref height, thumbnailWidth, thumbnailHeight, image.Width, image.Height);
                    if (height != 0)
                    {
                        using (Bitmap bitmap = new Bitmap(image, thumbnailWidth, thumbnailHeight))
                        {
                            bitmap.Save(fileName_thumb, image.RawFormat);
                        }
                    }
                    else
                    {
                        using (Bitmap bitmap = new Bitmap(image, thumbnailWidth, thumbnailHeight))
                        {
                            bitmap.Save(fileName_thumb, image.RawFormat);
                        }
                        //nuImageUrl.MoveTo(fileName, MoveToOptions.Overwrite);
                        //fuImageUrl.PostedFile.SaveAs(fileName);
                    }

                    // Save anh thum_b Slide
                    using (Bitmap bitmap = new Bitmap(image, MediaAlbumFeature.UI.MediaConstant.ImageMaxThumbnailSlideWidth, MediaAlbumFeature.UI.MediaConstant.ImageMaxThumbnailSlideHeight))
                    {
                        bitmap.Save(fileNameIMGSlide_thumb, image.RawFormat);
                    }

                    fileName = guid + fileExtension;
                }
            }
            catch (Exception e)
            {
                lblFileUrlError.Text = "Đã có lỗi xả ra";
                return false;
            }
            return true;

        }
        private string SuggestUrl()
        {
            string pageName = txtFileName.Text;
            return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            siteId = siteSettings.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
            galleryId = WebUtils.ParseInt32FromQueryString("galleryId", galleryId);
        }
        #region OnInit
        void btnSubmit_Click(object sender, System.EventArgs e)
        {
            btnSubmit.Text = MediaResources.PleaseWait;
            if (Save())
            {
                string url = SiteRoot + "/Media/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&galleryId=" + galleryId;
                WebUtils.SetupRedirect(this, url);

            }
            else
            {
                lblFileUrlError.Visible = true;
                btnSubmit.Text = MediaResources.ButtonAddNewMedia;
            }

        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSubmit.Click += btnSubmit_Click;
            btnDel.Click += btnDel_Click;
            btnCancel.Click += btnCancel_Click;
            ddlTypeData.SelectedIndexChanged += ddlTypeData_SelectedIndexChanged;
        }

        private void ddlTypeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeDataChange();
        }
        private void TypeDataChange()
        {
            if (ddlTypeData.SelectedValue == "1")//img
            {
                if (itemId > 0)
                {
                    RqfvFileImage.Visible = false;
                }
                pnVideo.Visible = false;
                pnCode.Visible = false;
                RqfVideo.Visible = false;
                RgExVideo.Visible = false;
                RqfCode.Visible = false;
            }
            else if (ddlTypeData.SelectedValue == "2") //video
            {
                if (itemId > 0)
                {
                    RqfvFileImage.Visible = false;
                    RqfVideo.Visible = false;
                }
                pnVideo.Visible = true;
                pnCode.Visible = false;
                RqfVideo.Visible = true;
                RgExVideo.Visible = true;
                RqfCode.Visible = false;
            }
            else
            {
                if (itemId > 0)
                {
                    RqfvFileImage.Visible = false;
                    RqfCode.Visible = true;
                }
                pnVideo.Visible = false;
                pnCode.Visible = true;
                RqfVideo.Visible = false;
                RgExVideo.Visible = false;
                RqfCode.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Media/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&galleryId=" + galleryId;
            WebUtils.SetupRedirect(this, url);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteFileFromServer(itemId);
            MediaAlbum.Delete(itemId);
            string url = SiteRoot + "/Media/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&galleryId=" + galleryId;
            WebUtils.SetupRedirect(this, url);
        }


        #endregion
    }
}
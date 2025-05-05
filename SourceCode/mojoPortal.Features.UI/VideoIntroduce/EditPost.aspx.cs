using MediaAlbumFeature.Business;
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
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoIntroduceFeatures.Business;

namespace VideoIntroduceFeatures.UI
{
    public partial class EditPost : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private VideoIntroduce video = new VideoIntroduce();
        #region OnInit
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            //editContent.Height = 130;
            //editContent.WebEditor.ToolBar = ToolBar.Newsletter;
            //SiteUtils.SetupEditor(editContent);
            btnSubmit.Click += btnSubmit_Click;
            btnDel.Click += btnDel_Click;
            btnCancel.Click += btnCancel_Click;
            //imgDeleteVideo.Click += imgDeleteVideo_Click;
            imgDeleteIMG.Click += imgDeleteIMG_Click;
            //imgDeleteYoutube.Click += imgDeleteYoutube_Click;
            //drlTypePlayer.SelectedIndexChanged += drlTypePlayer_SelectedIndexChanged;
        }

        protected void drlTypePlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (drlTypePlayer.SelectedValue == VideoIntroduceConstant.VideoPlayer.ToString())
            //{
            //    pnlVideoPlayer.Visible = true;
            //    pnlYoutubePlayer.Visible = false;
            //}
            //else
            //{
            //    pnlYoutubePlayer.Visible = true;
            //    pnlVideoPlayer.Visible = false;
            //}
        }


        void imgDeleteYoutube_Click(object sender, ImageClickEventArgs e)
        {
            //pnlShowYoutube.Visible = false;
        }

        void imgDeleteIMG_Click(object sender, ImageClickEventArgs e)
        {
            hdfImageValue.Value = string.Empty;
            pnlShowIMG.Visible = false;
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            SiteUtils.RedirectToUrl("/VideoIntroduce/ManagePost.aspx?pid=" + pageId + "&mid=" + moduleId);
        }

        void imgDeleteVideo_Click(object sender, ImageClickEventArgs e)
        {
            //hdfVideoValue.Value = string.Empty;
            //pnlShowVideo.Visible = false;
        }

        void btnDel_Click(object sender, EventArgs e)
        {
            DeleteFileFromServer();
            VideoIntroduce.Delete(itemId);
            SiteUtils.RedirectToUrl("/VideoIntroduce/ManagePost.aspx?pid=" + pageId + "&mid=" + moduleId);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {

            if (Save())
            {
                string url = SiteRoot + "/VideoIntroduce/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
                SiteUtils.RedirectToUrl(url);
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageVideo))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();
            LoadParams();
            LoadSettings();
            PopulateLabel();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabel()
        {
            btnDel.Text = "Xóa";
            btnCancel.Text = "Hủy";
            btnSubmit.Text = "Lưu";
            if (itemId > 0)
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, "Cập nhập video giới thiệu");
                //SiteUtils.AddConfirmButton(imgDeleteVideo, "Bạn có chắc chắn muốn xóa video này?");
                SiteUtils.AddConfirmButton(btnDel, "Bạn có chắc chắn muốn xóa video này?");
                btnDel.Visible = true;
                //hplDetail.Visible = true;
                //hplDetail.Text = "Chi tiết?";
                //hplDetail.NavigateUrl = video.ItemUrl.Replace("~", "");
                heading.Text = "Cập nhập video";
            }
            else
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, "Thêm mới video giới thiệu");
                heading.Text = "Thêm mới video";
            }
        }
        private void BindTypePlayer()
        {
            List<ListItem> listItem = new List<ListItem>();
            //listItem.Add(new ListItem { Value = VideoIntroduceConstant.VideoPlayer.ToString(), Text = "Tải file video", Selected = true });
            listItem.Add(new ListItem { Value = VideoIntroduceConstant.YoutubePlayer.ToString(), Text = "Đường dẫn youtube" });
            //drlTypePlayer.DataTextField = "Text";
            //drlTypePlayer.DataValueField = "Value";
            //drlTypePlayer.DataSource = listItem;
            //drlTypePlayer.DataBind();

        }
        private void PopulateControls()
        {
            BindTypePlayer();
            if (itemId > 0 && video != null)
            {
                txtTieuDe.Text = video.Title;
                editContent.Text = video.ContentDetail;
                chkPublic.Checked = video.IsPublic;
                linkVideo.Value = video.ItemUrl;

                if(!string.IsNullOrEmpty(video.ItemUrl))
                {
                    StringBuilder setup = new StringBuilder();
                    setup.Append("<video width='400' class='video-in-form' height='240' controls src='"+ video.ItemUrl + "'>");
                    setup.Append("</video>");
                    literVideoPlayer.Text = setup.ToString();
                } 
                else
                {
                    StringBuilder setup = new StringBuilder();
                    setup.Append("<video width='400' class='video-in-form' height='240' controls src=''>");
                    setup.Append("</video>");
                    literVideoPlayer.Text = setup.ToString();
                }

                //drlTypePlayer.SelectedValue = video.TypePlayer.ToString();
                chkIsHot.Checked = video.IsHot;
                if (!string.IsNullOrEmpty(video.Video))
                {
                    if (video.TypePlayer == VideoIntroduceConstant.VideoPlayer)
                    {
                        //pnlVideoPlayer.Visible = true;
                        pnlYoutubePlayer.Visible = false;
                        //pnlShowVideo.Visible = true;
                        StringBuilder setup = new StringBuilder();
                        setup.Append("<video width='320' height='240' style='float: left;' controls>");
                        setup.Append("<source src='" + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + video.Video + "' type='video/mp4'>");
                        setup.Append("</video>");
                        //literVideoPlayer.Text = setup.ToString();
                        //hdfVideoValue.Value = video.Video;
                        //imgDeleteVideo.ToolTip = "Xóa video";
                        //imgDeleteVideo.ImageUrl = "/Data/SiteImages/delete.gif";
                    }
                }
                if (!string.IsNullOrEmpty(video.YoutubeUrl))
                {
                    //pnlVideoPlayer.Visible = false;
                    pnlYoutubePlayer.Visible = true;
                    //pnlShowYoutube.Visible = true;
                    //txtYoutube.Text = video.YoutubeUrl;
                    StringBuilder setup = new StringBuilder();
                    setup.Append(video.YoutubeUrl);
                    //literYoutubePlayer.Text = setup.ToString();
                    //imgDeleteYoutube.ToolTip = "Xóa đường youtube?";
                    //imgDeleteYoutube.ImageUrl = "/Data/SiteImages/delete.gif";
                }
                if (!string.IsNullOrEmpty(video.ImageVideo) && !video.ImageVideo.Equals(ConfigurationManager.AppSettings["ImageVideoIntroduceDefault"]))
                {
                    hdfImageValue.Value = video.ImageVideo;
                    pnlShowIMG.Visible = true;
                    imgBackgroundImage.ImageUrl = ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + video.ImageVideo;
                    imgDeleteIMG.ImageUrl = "/Data/SiteImages/delete.gif";
                    imgDeleteIMG.ToolTip = "Xóa ảnh nền video hoặc youtube?";
                }
            } else
            {
                StringBuilder setup = new StringBuilder();
                setup.Append("<video width='400' class='video-in-form' height='240' controls src=''>");
                setup.Append("</video>");
                literVideoPlayer.Text = setup.ToString();
            }
        }
        private bool Save()
        {
            Page.Validate("videoVilid");
            if (!Page.IsValid) return false;

            if (!string.IsNullOrEmpty(txtTieuDe.Text))
            {
                txtTieuDe.Text = txtTieuDe.Text.Trim();
            }
            string oldUrl = video.ItemUrl.Replace("~/", string.Empty);

            video.Title = txtTieuDe.Text;
            video.SiteID = siteSetting.SiteId;
            video.ContentDetail = editContent.Text;
            video.CreateBy = siteUser.UserId;
            video.CreateDate = DateTime.Now;
            video.ModuleID = moduleId;
            video.PageID = pageId;
            video.Name = txtTieuDe.Text.ConvertToFTS();
            video.IsPublic = chkPublic.Checked;
            //video.TypePlayer = int.Parse(drlTypePlayer.SelectedValue);
            video.IsHot = chkIsHot.Checked;


           


            if (video.TypePlayer == VideoIntroduceConstant.VideoPlayer)
            {
                double filesSize = 0;
                string typeFile = string.Empty;
                string fileName = string.Empty;
                var checkSaveIMG = SaveVideoUrl(out fileName, out filesSize, out typeFile);
                if (!checkSaveIMG) { return false; }
                if (!string.IsNullOrEmpty(fileName))
                {
                    video.Video = fileName;
                }
                if (filesSize > 0)
                {
                    video.SizeVideo = filesSize;
                }
                if (!string.IsNullOrEmpty(typeFile))
                {
                    video.TypeVideo = typeFile;
                }
            }
            else //youtube
            {
                //video.YoutubeUrl = txtYoutube.Text;
            }
            string imgUrl = string.Empty;
            SaveImageUrl(out imgUrl);
            if (!string.IsNullOrEmpty(imgUrl))
            {
                video.ImageVideo = imgUrl;
            }
            if (chkIsHot.Checked)
            {
                VideoIntroduce.UpdateIsHot(siteSetting.SiteId);
            }
            #region lưu itemUrl
            Guid DoanhNghiepGuid = Guid.NewGuid();
            String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(video.ItemUrl.Replace("~/", String.Empty));
            FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);



            var objVideo = hdfVideo.Value;
            var videoxx = new JavaScriptSerializer().Deserialize<MediaAlbum>(objVideo);


            string newUrl = SiteUtils.RemoveInvalidUrlChars(video.ItemUrl.Replace("~/", string.Empty));
            if(videoxx != null)
            {
            video.ItemUrl = videoxx.FilePath;

            }

            video.Save();

            #region lưu ProductCategory


            #endregion

            //if (!friendlyUrl.FoundFriendlyUrl)
            //{
            if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
            {
                if (oldUrl.Equals(newUrl))
                {
                    FriendlyUrl oldFriendlyUrl = new FriendlyUrl(siteSettings.SiteId, oldUrl);
                    oldFriendlyUrl.SiteId = siteSettings.SiteId;
                    oldFriendlyUrl.SiteGuid = siteSettings.SiteGuid;
                    oldFriendlyUrl.PageGuid = DoanhNghiepGuid;
                    oldFriendlyUrl.Url = friendlyUrlString;
                    oldFriendlyUrl.RealUrl = "~/VideoIntroduce/DetailVideo.aspx?pageid="
                                   + pageId.ToInvariantString()
                                   + "&mid=" + video.ModuleID.ToInvariantString()
                                   + "&item=" + video.ItemID.ToInvariantString();
                    oldFriendlyUrl.Save();
                }
                else
                {
                    FriendlyUrl newFriendlyUrl = new FriendlyUrl
                    {
                        SiteId = siteSettings.SiteId,
                        SiteGuid = siteSettings.SiteGuid,
                        PageGuid = DoanhNghiepGuid,
                        Url = friendlyUrlString,
                        RealUrl = "~/VideoIntroduce/DetailVideo.aspx?pageid="
                                   + pageId.ToInvariantString()
                                   + "&mid=" + video.ModuleID.ToInvariantString()
                                   + "&item=" + video.ItemID.ToInvariantString(),

                    };

                    newFriendlyUrl.Save();
                }
            }

            //if post was renamed url will change, if url changes we need to redirect from the old url to the new with 301
            if ((oldUrl.Length > 0) && (newUrl.Length > 0) && (!SiteUtils.UrlsMatch(oldUrl, newUrl)))
            {
                //worry about the risk of a redirect loop if the page is restored to the old url again
                // don't create it if a redirect for the new url exists
                if (
                    (!RedirectInfo.Exists(siteSettings.SiteId, oldUrl))
                    && (!RedirectInfo.Exists(siteSettings.SiteId, newUrl))
                    )
                {
                    RedirectInfo redirect = new RedirectInfo
                    {
                        SiteGuid = siteSettings.SiteGuid,
                        SiteId = siteSettings.SiteId,
                        OldUrl = oldUrl,
                        NewUrl = newUrl
                    };
                    redirect.Save();
                }
                // since we have created a redirect we don't need the old friendly url
                FriendlyUrl oldFriendlyUrl = new FriendlyUrl(siteSettings.SiteId, oldUrl);
                if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == DoanhNghiepGuid))
                {
                    FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                }

            }
            #endregion

            #region Lưu đường dẫn video
        
            //if(!string.IsNullOrEmpty(videoxx.FilePath))
            //{
            //    MediaAlbum ha = new MediaAlbum();
            //    ha.ItemUrl = SuggestUrl();
            //    ha.GroupMediaID = media.ItemID;
            //    ha.ModuleID = media.ModuleID;
            //    ha.SiteID = media.SiteID;
            //    ha.CreatedByUser = videoxx.CreatedByUser;
            //    ha.Description = videoxx.Description;
            //    ha.AlbumOrder = videoxx .AlbumOrder;
            //    string path = Server.MapPath("~/" + ConfigurationManager.AppSettings["MediaImagesFolder"]);
            //    ha.FilePath = videoxx.FilePath;
            //    ha.IsPublish = true;
            //    ha.Save();
            //}

            #endregion


            return true;
        }

        private string SuggestUrl()
        {
            string pageName = string.Empty;
            if (!string.IsNullOrEmpty(txtTieuDe.Text))
            {
                if (!video.Title.ToLower().Equals(txtTieuDe.Text))
                {

                    pageName = txtTieuDe.Text;
                    return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);

                }
            }
            return string.Empty;
        }
        private bool DeleteFileFromServer()
        {
            string filePath_VIDEO = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + video.Video;
            filePath_VIDEO = filePath_VIDEO.Replace("/", "\\");
            if (File.Exists(filePath_VIDEO))
            {
                File.Delete(filePath_VIDEO);
            }
            video.Video = string.Empty;
            return true;
        }
        private bool DeleteFileIMGFromServer()
        {
            string filePath_VIDEO = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + video.ImageVideo;
            filePath_VIDEO = filePath_VIDEO.Replace("/", "\\");
            if (File.Exists(filePath_VIDEO))
            {
                File.Delete(filePath_VIDEO);
            }
            video.ImageVideo = string.Empty;
            return true;
        }

        private bool SaveImageUrl(out string fileName)
        {
            HttpPostedFile files = floadBackground.PostedFile;
            fileName = string.Empty;
            string names = string.Empty;
            if (floadBackground.HasFile && files.ContentLength > 0)
            {
                //DELETE FILE OLD
                if (itemId > -1)
                {
                    DeleteFileIMGFromServer();
                }
                string fileExtension = Path.GetExtension(files.FileName);
                if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedImageFileExtensions"))
                {
                    lblError.Text = "File bạn tải lên không đúng!";
                    return false;

                }
                Double fileSize = (files.ContentLength / 1024) / 1024;
                if (!SiteUtils.IsValidFileSize(fileSize, "AllowedFileSize"))
                {
                    lblError.Text = "File vượt quá độ lớn cho phép !";
                    return false;
                }
                try
                {

                    String pathToApplicationsFolder = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"]);
                    if (!Directory.Exists(pathToApplicationsFolder))
                    {
                        Directory.CreateDirectory(pathToApplicationsFolder);
                    }
                    string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    names = guid + fileExtension;
                    files.SaveAs(Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + names);
                    fileName = names;
                }
                catch
                {
                    lblError.Text = "Đã có lỗi xảy ra";
                    return false;
                }
            }
            else
            {
                fileName = hdfImageValue.Value;
            }
            return true;
        }


        private bool SaveVideoUrl(out string fileName, out double MBSite, out string typeFile)
        {
            //HttpPostedFile files = fileUpload.PostedFile;
            fileName = string.Empty;
            MBSite = 0;
            typeFile = string.Empty;
            string names = string.Empty;
            //if (!fileUpload.HasFile && files.ContentLength <= 0 && string.IsNullOrEmpty(hdfVideoValue.Value))
            //{
            //    lblRequiredVideo.Text = "Bạn chưa tải file video !";
            //    lblRequiredVideo.Visible = true;
            //    return false;
            //}
            //else
            //{
            //    lblRequiredVideo.Visible = false;
            //}

            //if (fileUpload.HasFile && files.ContentLength > 0)
            //{
            //    //DELETE FILE OLD
            //    if (itemId > -1)
            //    {
            //        DeleteFileFromServer();
            //    }
            //    string fileExtension = Path.GetExtension(files.FileName);
            //    typeFile = fileExtension;
            //    if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedVideoFileExtensions"))
            //    {
            //        lblError.Text = "File bạn tải lên không đúng!";
            //        return false;

            //    }
            //    Double fileSize = (files.ContentLength / 1024) / 1024;
            //    MBSite = fileSize;
            //    if (!SiteUtils.IsValidFileSize(fileSize, "AllowedVideoIntroduceSize"))
            //    {
            //        lblError.Text = "File vượt quá độ lớn cho phép !";
            //        return false;
            //    }
            //    try
            //    {

            //        String pathToApplicationsFolder = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"]);
            //        if (!Directory.Exists(pathToApplicationsFolder))
            //        {
            //            Directory.CreateDirectory(pathToApplicationsFolder);
            //        }
            //        string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            //        names = guid + fileExtension;
            //        files.SaveAs(Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + names);
            //        fileName = names;
            //    }
            //    catch
            //    {
            //        lblError.Text = "Đã có lỗi xảy ra";
            //        return false;
            //    }
            //}
            //else
            //{
            //    fileName = hdfVideoValue.Value;
            //}
            return true;
        }
        private void LoadParams()
        {
            itemId = WebUtils.ParseInt32FromQueryString("item", itemId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
        }
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new VideoIntroduceConfiguration(getModuleSettings);
            //LoadSideContent(true, true);
            if (itemId > 0)
            {
                video = new VideoIntroduce(itemId);
            }

        }
    }
}
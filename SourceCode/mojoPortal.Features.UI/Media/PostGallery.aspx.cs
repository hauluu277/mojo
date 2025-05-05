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
using MediaAlbumFeature.Business;
using mojoPortal.Business.WebHelpers;
using System.Web.Script.Serialization;
using System.Globalization;

namespace MediaFeature.UI
{
    public partial class PostGallery : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private MediaConfiguration config = new MediaConfiguration();
        private MediaGroup media;
        //private md_MediaImage mediaImage;
        private Guid featureGuid = Guid.Empty;
        private string name = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();

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

            

            SecurityHelper.DisableBrowserCache();
            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                SetupButton();
                PopulateControls();
            }
        }


        private void PopulateControls()
        {
            BindCategory();
            BindImage();

            pnIMG.Visible = false;
            if (media != null)
            {
                chkIsPublished.Checked = media.IsPublish.GetValueOrDefault(false);
                txtNameGroup.Text = media.NameGroup;
                hfFilePath.Value = media.FilePath;
                txtOrder.Text = media.GroupOrder.ToString();
                ddlCategory.SelectedValue = media.Category.ToString();
                txtCreatedByUser.Text = media.CreatedByUser;
                rvfFileUpload.Visible = false;
                dpCreatedDate.Text = string.Format("{0:dd/MM/yyyy}", media.CreatedDate);
                if (media.FilePath != "")
                {
                    pnIMG.Visible = true;
                    ImageID.Src = "/Data/File/Media/" + media.FilePath;
                }
            }
            else
            {
                txtCreatedByUser.Text = siteUser.Name;
                btnDelete.Visible = false;
                dpCreatedDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }

        }
        private void BindImage()
        {
            if (itemId > 0)
            {
                pnlFirst.Visible = false;
                var listImage = MediaAlbum.GetByGroup(siteSettings.SiteId, itemId);
                rptMediaImage.DataSource = listImage;
                rptMediaImage.DataBind();
            }
        }
        private void BindCategory()
        {
            var category = System.Configuration.ConfigurationManager.AppSettings["CategoryID"].ToIntOrZero();
            var sourceCD = CoreCategory.GetChildrenByParent(siteSettings.CoreDuLieuDaPhuongTien);
            ddlCategory.DataValueField = "ItemID";
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataSource = sourceCD;
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem { Text = "-- Chọn chuyên mục --", Value = "" });
        }
        private void SetupButton()
        {
            btnSave.Visible = true;
            btnDelete.Visible = false;
            if (itemId > 0)
            {
                btnDelete.Visible = true;
                btnSave.Text = "Cập nhật";
            }
            else
            {
                btnSave.Text = "Thêm mới";
            }

        }



        private bool Save()
        {
            Page.Validate("mediaGroup");
            if (!Page.IsValid) return false;
            string textUrl = string.Empty;

            if (media == null)
            {
                media = new MediaGroup();
                media.CreatedDate = DateTime.Now;
                media.CreatedByUser = siteUser.Name;
                media.UserGuid = siteUser.UserGuid;
            }
            else
            {
                media = new MediaGroup(itemId);
            }
            if (media.NameGroup != txtNameGroup.Text || itemId <= 0)
            {
                textUrl = SuggestUrl();
            }
            else
            {
                textUrl = media.ItemUrl;
            }
            //media.ItemUrl = SuggestUrl();
            media.ModuleID = moduleId;
            media.SiteID = siteSettings.SiteId;
            media.UserGuid = siteUser.UserGuid;
            media.NameGroup = txtNameGroup.Text;
            media.Category = ddlCategory.SelectedValue.ToIntOrZero();
            media.CreatedByUser = txtCreatedByUser.Text;
            media.IsPublish = chkIsPublished.Checked;
            if (!string.IsNullOrEmpty(dpCreatedDate.Text))
            {
                DateTime localTime1 = DateTime.Parse(dpCreatedDate.Text, CultureInfo.CurrentCulture);
                media.CreatedDate = localTime1;
            }
            if (!string.IsNullOrEmpty(txtOrder.Text))
            {
                int num;
                bool isNum = Int32.TryParse(txtOrder.Text, out num);

                if (isNum)
                {
                    media.GroupOrder = Convert.ToInt32(txtOrder.Text);
                }
            }

            HttpPostedFile files = uploadFile.PostedFile;
            string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            if (uploadFile.HasFile && files.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(uploadFile.FileName);
                Double fileSize = files.ContentLength / 1024;
                if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedImageFileExtensions"))
                {
                    lblFileUrlError.Text = MediaResources.FileFormatUploadError;
                    return false;
                }
                if (!SiteUtils.IsValidFileSize(fileSize, "AllowedImageSize"))
                {
                    lblFileUrlError.Text = MediaResources.Invalidfile;
                    return false;
                }
                else
                {
                    try
                    {
                        string path = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["MultimediaFileFolder"]);
                        bool folderExists = Directory.Exists(path);
                        if (!folderExists)
                        {
                            Directory.CreateDirectory(path);
                        }
                        name = guid + fileExtension;
                        uploadFile.SaveAs(path + name);
                        media.FilePath = name;
                    }
                    catch
                    {
                        lblFileUrlError.Text = MediaResources.UploadFileDuplicates;
                    }
                }
            }
            else
            {
                if (itemId > -1)
                {
                    media.FilePath = hfFilePath.Value;
                }
            }


            //if (textUrl != "")
            //{
            //    textUrl = textUrl.Replace(" ", "-");
            //}

            Guid DocumentGuid = Guid.NewGuid();
            String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(textUrl.Replace("~/", String.Empty));
            FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

            if (
                ((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != DocumentGuid))
                && (media.ItemUrl != textUrl)
                )
            {
                //lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                //return;
                return false;
            }

            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString))
                {
                    //lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                    return false;
                }
            }

            string oldUrl = media.ItemUrl.Replace("~/", string.Empty);
            string newUrl = SiteUtils.RemoveInvalidUrlChars(textUrl.Replace("~/", string.Empty));
            media.ItemUrl = "~/" + newUrl;
            media.Save();
            if (itemId > 0)
            {
                MediaAlbum.DeleteByGroupID(itemId);
            }
            var listHinhAnh = hdfHinhAnh.Value;
            var dsHinhAnh = new JavaScriptSerializer().Deserialize<List<MediaAlbum>>(listHinhAnh);
            if (dsHinhAnh != null)
            {
                foreach (var hinhanh in dsHinhAnh)
                {
                    if (string.IsNullOrEmpty(hinhanh.FilePath)) { continue; }
                    MediaAlbum ha = new MediaAlbum();
                    ha.ItemUrl = SuggestUrl();
                    ha.GroupMediaID = media.ItemID;
                    ha.ModuleID = media.ModuleID;
                    ha.SiteID = media.SiteID;
                    ha.CreatedByUser = hinhanh.CreatedByUser;
                    ha.Description = hinhanh.Description;
                    string path = Server.MapPath("~/" + ConfigurationManager.AppSettings["MediaImagesFolder"]);
                    ha.FilePath = hinhanh.FilePath;
                    ha.IsPublish = true;
                    ha.Save();
                }
            }


            //if (media.NameGroup != txtNameGroup.Text)
            //{
            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                {
                    FriendlyUrl newFriendlyUrl = new FriendlyUrl
                    {
                        SiteId = siteSettings.SiteId,
                        SiteGuid = siteSettings.SiteGuid,
                        PageGuid = DocumentGuid,
                        Url = friendlyUrlString,
                        RealUrl = "/Media/ViewListMediaAlbum.aspx?pageid="
                                  + pageId.ToInvariantString()
                                  + "&mid=" + media.ModuleID.ToInvariantString()
                                  + "&groupMediaId=" + media.ItemID.ToInvariantString()
                    };
                    newFriendlyUrl.Save();
                }
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
                    if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == DocumentGuid))
                    {
                        FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                    }

                }
                else
                {
                    if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                    {
                        string realUrl = string.Empty;
                        realUrl = "/Media/ViewListMediaAlbum.aspx?pageid="
                                         + pageId.ToInvariantString()
                                         + "&mid=" + media.ModuleID.ToInvariantString()
                                         + "&groupMediaId=" + media.ItemID.ToInvariantString();
                        FriendlyUrl updateFriendlyUrl = new FriendlyUrl(friendlyUrl.UrlId);
                        if (updateFriendlyUrl != null)
                        {
                            updateFriendlyUrl.RealUrl = realUrl;
                            updateFriendlyUrl.Save();
                        }
                    }
                }
                //}
            }

            return true;
        }



        private void PopulateLabels()
        {
            btnSave.Text = "Cập nhật";
            btnCancel.Text = MediaResources.GroupMediaButtonBack;
            btnCancel.PostBackUrl = SiteRoot + "/Media/managepostCategories.aspx?pageid=" + pageId + "&mid=" + moduleId;
            btnDelete.Text = MediaResources.GroupMediaButtonDelete;
            rexp.ErrorMessage = MediaResources.FileFormatUploadError;
            pnIMG.Visible = false;
            if (itemId > 0)
            {
            }
            if (media != null)
            {
                legendGroupMedia.InnerText = MediaResources.GroupMediaEditTitle;
                Title = SiteUtils.FormatPageTitle(siteSettings, Resources.MediaResources.EditCategoriesTitle);
                heading.Text = "Cập nhật thư viện ảnh";
            }
            else
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, Resources.MediaResources.AddNewCategoriesTitle);
                heading.Text = "Thêm mới thư viện ảnh";

            }
            legendGroupMedia.InnerText = "Thông tin thư viện ảnh";

            UIHelper.AddConfirmationDialog(btnDelete, MediaResources.ConfirmDeleteWarning);
        }

        private void LoadSettings()
        {
            if (itemId > -1)
            {
                media = new MediaGroup(itemId);
                //if (media.ModuleID != moduleId) { media = null; }
            }
        }
        private string SuggestUrl()
        {
            string pageName = txtNameGroup.Text;
            //if (pageName != "")
            //{
            //    pageName = pageName.Replace(" ", "-");
            //}
            return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            siteId = siteSettings.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
        }
        #region OnInit
        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (Save())
            {
                WebUtils.SetupRedirect(this, SiteRoot + "/Media/managepostCategories.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID" + media.ItemID);
            }
        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSave.Click += btnSubmit_Click;
            btnDelete.Click += btnDel_Click;
        }



        protected void btnDel_Click(object sender, EventArgs e)
        {
            List<MediaAlbum> lstAlbum = MediaAlbum.GetAllByModule(moduleId, itemId);
            foreach (var item in lstAlbum)
            {
                DeleteFileFromServer(item.ItemID);
            }
            MediaGroup group = new MediaGroup(itemId);
            string filePath_IMG = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["MultimediaFileFolder"] + group.FilePath;
            filePath_IMG = filePath_IMG.Replace("/", "\\");
            if (File.Exists(filePath_IMG))
            {
                File.Delete(filePath_IMG);
            }
            MediaAlbum.DeleteByGroupID(itemId);
            MediaGroup.Delete(itemId);
            string url = SiteRoot + "/Media/managepostCategories.aspx?pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, url);
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
        #endregion

        protected string GenerateIMG(string urlIMG, object orderBy, string author)
        {
            string append = string.Empty;
            if (!string.IsNullOrEmpty(urlIMG))
            {
                var listIMG = urlIMG.Split(';');
                if (listIMG != null && listIMG.Count() > 0)
                {
                    foreach (var item in listIMG)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            append += "<p class='add-img'>";
                            append += "<input type = 'hidden' />";
                            append += "<img src = '" + item + "'' width='50'>";
                            append += "<span class='text-primary pointer choose-img' title='Chọn ảnh'><i class='fa fa-upload' aria-hidden='true'></i></span>";
                            append += "&nbsp;&nbsp;";
                            append += "<span class='text-danger pointer remove-img'><i class='fa fa-trash' aria-hidden='true'></i></span>";
                            append += "<input type='text' name='txtOrderBy' value='" + orderBy + "'/>";
                            append += "<input type='text' name='txtAuthor' value='" + author + "'/>";
                            append += "</p>";
                        }
                    }
                }

            }
            return append;
        }
    }
}

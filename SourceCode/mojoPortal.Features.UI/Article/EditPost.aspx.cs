using ArticleFeature.Business;
using Brettle.Web.NeatUpload;
using DictionaryFeature.Business;
using EventFeature.Business;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Net;
using mojoPortal.SearchIndex;
using mojoPortal.Web;
using mojoPortal.Web.Editor;
using mojoPortal.Web.Framework;
using Newtonsoft.Json;
using PollFeature.Business;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SD = System.Drawing;

namespace ArticleFeature.UI
{
    public partial class EditPost : mojoBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EditPost));
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;

        private int loadedModuleId = -1;
        private int loadedPageId = -1;
        private int loadedSiteId = -1;
        private SiteSettings settingsSync;
        protected String cacheDependencyKey;
        protected string virtualRoot;
        protected Double timeOffset;
        private TimeZoneInfo timeZone;
        protected Hashtable moduleSettings;
        protected ArticleConfiguration config = new ArticleConfiguration();
        private int pageNumber = 1;
        private const int pageSize = 10;
        private int totalPages = 1;
        private Guid restoreGuid = Guid.Empty;
        private Article article;
        private Article articleSync;
        private bool enableContentVersioning;
        protected bool isAdmin;
        readonly ContentMetaRespository metaRepository = new ContentMetaRespository();
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private int catId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private bool isUserPost;
        private bool isApprover;
        private string dateTimeFormat;
        protected string fileAudio = string.Empty;
        protected bool RoleAccess = false;
        private static int siteid = -1;
        private static int userid = -1;
        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
            SiteUtils.SetupEditor(edContent, AllowSkinOverride, this);
        }

        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            LoadSettings();
            LoadPanels();
            base.OnInit(e);
            Load += Page_Load;

            //ScriptConfig.IncludeYuiTabs = true;
            //IncludeYuiTabsCss = true;
            rptAttachments.ItemDataBound += rptAttachments_ItemDataBound;
            rptAttachments.ItemCommand += rptAttachments_ItemCommand;

            rdoIsApproved.SelectedIndexChanged += rdoIsApproved_SelectedIndexChanged;
            btnUpload.Click += btnUpload_Click;

            btnUpdate.Click += btnUpdate_Click;
            btnUpdate3.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnDelete3.Click += btnDelete_Click;

            btnDeleteImg.Click += btnDeleteImg_Click;
            btnDeleteAudio.Click += btnDeleteAudio_Click;
            btnSaveAndPreview.Click += btnSaveAndPreview_Click;

            grdHistory.RowCommand += grdHistory_RowCommand;
            grdHistory.RowDataBound += grdHistory_RowDataBound;
            pgrHistory.Command += pgrHistory_Command;
            btnRestoreFromGreyBox.Click += btnRestoreFromGreyBox_Click;
            btnDeleteHistory.Click += btnDeleteHistory_Click;
            //chkIsApproved.CheckedChanged += chkIsApproved_CheckedChanged;

            grdContentMeta.RowCommand += grdContentMeta_RowCommand;
            grdContentMeta.RowEditing += grdContentMeta_RowEditing;
            grdContentMeta.RowUpdating += grdContentMeta_RowUpdating;
            grdContentMeta.RowCancelingEdit += grdContentMeta_RowCancelingEdit;
            grdContentMeta.RowDeleting += grdContentMeta_RowDeleting;
            grdContentMeta.RowDataBound += grdContentMeta_RowDataBound;
            btnAddMeta.Click += btnAddMeta_Click;

            grdMetaLinks.RowCommand += grdMetaLinks_RowCommand;
            grdMetaLinks.RowEditing += grdMetaLinks_RowEditing;
            grdMetaLinks.RowUpdating += grdMetaLinks_RowUpdating;
            grdMetaLinks.RowCancelingEdit += grdMetaLinks_RowCancelingEdit;
            grdMetaLinks.RowDeleting += grdMetaLinks_RowDeleting;
            grdMetaLinks.RowDataBound += grdMetaLinks_RowDataBound;
            btnAddMetaLink.Click += btnAddMetaLink_Click;
            btnReloadTag.Click += BtnReloadTag_Click;
        }

        protected void BtnReloadTag_Click(object sender, EventArgs e)
        {
            var listTag = Dictionary.GetAllPublic().Where(x => x.SiteID == siteSettings.SiteId).ToList();
            if (listTag != null && listTag.Count > 0)
            {
                lboxTag.DataSource = listTag;
                lboxTag.DataTextField = "Name";
                lboxTag.DataValueField = "ItemID";
                lboxTag.DataBind();
                if (article != null)
                {
                    var listTagMapArticle = ArticleItemTag.GetAllByArticle(article.ItemID).Select(o => o.TagID).ToList();
                    if (lboxTag.Items.Count > 0)
                    {
                        for (int i = 0; i < lboxTag.Items.Count; i++)
                        {
                            if (listTagMapArticle.Contains(int.Parse(lboxTag.Items[i].Value)))
                            {
                                lboxTag.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }



        #region Sự kiện
        //protected void BtnReloadEvent_Click(object sender, EventArgs e)
        //{
        //    var listEvent = Event.GetAllForArticle(siteId);
        //    if (listEvent != null && listEvent.Count > 0)
        //    {
        //        lboxEvent.DataSource = listEvent;
        //        lboxEvent.DataTextField = "Title";
        //        lboxEvent.DataValueField = "ItemID";
        //        lboxEvent.DataBind();
        //    }
        //}
        #endregion


        #endregion
        private void Page_Load(object sender, EventArgs e)
        {
            Page.EnableViewState = true;
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }

            SecurityHelper.DisableBrowserCache();

            if (!UserCanEditModule(moduleId) && !isUserPost && !isApprover)
            {
                SiteUtils.RedirectToEditAccessDeniedPage();
                return;
            }

            SetupScripts();
            if ((Request.UrlReferrer != null) && (hdnReturnUrl.Value.Length == 0))
            {
                hdnReturnUrl.Value = Request.UrlReferrer.ToString();
                lnkCancel.NavigateUrl = Request.UrlReferrer.ToString();
                lnkCancel3.NavigateUrl = lnkCancel.NavigateUrl;

            }
            else
            {
                lnkCancel.Visible = false;
                lnkCancel3.Visible = false;
            }
            ScriptManager.RegisterStartupScript(
           uptag,
           this.GetType(),
           "ReloadTag",
           "ReloadTag();",
           true);
            if (!Page.IsPostBack)
            {
                PopulateLabels();
                PopulateCategories();
                PopulateCategoriesReference();
                PopulatePolls();
                LoadArticleReference();
                PopulateControls();
                BindArticleLog();
                //BindMeta();
                //BindMetaLinks();
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static string SaveJcrop(string fileName, int w, int h, int x, int y)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["ArticleImagesFolder"];
            string pathArticle = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"]);

            if (!Directory.Exists(pathArticle))
            {
                Directory.CreateDirectory(pathArticle);
            }
            string dirFullPath = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"]);
            //string[] files;

            //int numFiles;

            var guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();

            //files = System.IO.Directory.GetFiles(dirFullPath);
            //numFiles = files.Length;
            //numFiles = numFiles + 1;
            string fileExtension = Path.GetExtension(fileName);
            string ImageName = fileName;
            string url = Path.Combine(HttpContext.Current.Server.MapPath("~/" + path), fileName);
            string fileResult = guid + fileExtension;
            string pathUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/" + path), fileResult);
            byte[] CropImage = Crop(url, w, h, x, y);
            using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
            {
                ms.Write(CropImage, 0, CropImage.Length);
                using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                {
                    //fileResult = "crop" + ImageName;
                    //string SaveTo = path + "crop" + ImageName;
                    CroppedImage.Save(pathUrl, CroppedImage.RawFormat);
                    CroppedImage.Dispose();
                    //imgCropped.ImageUrl = "images/crop" + ImageName;
                    string fileThumb = string.Empty;
                    System.Drawing.Image image;
                    image = System.Drawing.Image.FromFile(pathUrl);
                    image.Dispose();
                }
            }
            return fileResult;
        }


        static byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            try
            {
                using (SD.Image OriginalImage = SD.Image.FromFile(Img))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            bmp.Dispose();
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        private void LoadArticleReference()
        {
            var listArticleReference = ArticleReferenceBO.GetList(siteSettings.SiteId, itemId);
            lboxArticleReference.DataValueField = "ItemID";
            lboxArticleReference.DataTextField = "Title";
            lboxArticleReference.DataSource = listArticleReference;
            lboxArticleReference.DataBind();
        }
        protected virtual void PopulateControls()
        {
            hdfReference.Value = new JavaScriptSerializer().Serialize(ArticleReferenceBO.GetList(1, -1));
            if (article != null)
            {
                string selectedValue = article.CategoryID.ToString();
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem item = ddlCategories.Items.FindByValue(selectedValue);
                    if (item != null)
                    {
                        ddlCategories.Items.FindByValue(selectedValue).Selected = true;
                    }
                }
                else { ddlCategories.SelectedIndex = 0; }

                //dpBeginDate2.ShowTime = true;
                dpBeginDate2.Text = timeZone != null ? article.StartDate.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.StartDate.ToString(dateTimeFormat));

                if (article.EndDate.HasValue)
                {
                    dpEndDate2.Text = timeZone != null ? article.EndDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.EndDate.Value.ToString(dateTimeFormat));
                }
                ArticleReferenceAtive.Value = article.ArticleReference;
                dpAddDate2.Text = timeZone != null ? article.CreatedDate.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.CreatedDate.ToString(dateTimeFormat));
                txtTitle.Text = article.Title;
                txtSummary.Text = article.Summary;
                txtItemUrl.Text = article.ItemUrl;
                //ckHotNew.Checked = article.IsHotNew;
                edContent.Text = article.Description;
                //txtTag.Text = article.Tag;
                txtMetaDescription.Text = article.MetaDescription;
                if (!string.IsNullOrEmpty(article.CreatedByUser))
                {
                    txtAuthor.Text = article.CreatedByUser;
                }
                if (!string.IsNullOrEmpty(article.ImageUrl))
                {
                    divImage.Visible = true;
                    imgView.ImageUrl = "~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"] + article.ImageUrl;
                }
                else { divImage.Visible = false; }

                //chkIsApproved.Checked = article.IsApproved;
                if (article.IsApproved.HasValue)
                {
                    rdoIsApproved.SelectedIndex = article.IsApproved.Value ? 1 : 0;
                }
                if (article.IsPublished.HasValue)
                {
                    chkIsPublished.Checked = article.IsPublished.Value;
                }
                chkIncludeInFeed.Checked = article.IncludeInFeed;
                chkIsHome.Checked = article.IsHome;
                chkIsAllowComment.Checked = article.AllowComment;
                chkIsHot.Checked = article.IsHot;
                chkIsAllowWCAG.Checked = article.AllowWCAG;
                txtCommentByBoss.Text = article.CommentByBoss;
                txtMetaPublisher.Text = article.MetaPublisher;
                txtMetaTitle.Text = article.MetaTitle;
                txtMetaIdentifier.Text = article.MetaIdentifier;
                txtMetaCreator.Text = article.MetaCreator;
                if (article.MetaDate.HasValue)
                {
                    calenderMetaDate.Text = timeZone != null ? article.MetaDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.MetaDate.Value.ToString(dateTimeFormat));
                }
                if (restoreGuid != Guid.Empty)
                {
                    ContentHistory rHistory = new ContentHistory(restoreGuid);
                    if (rHistory.ContentGuid == article.ArticleGuid)
                    {
                        edContent.Text = rHistory.ContentText;
                    }
                }
                // show preview button for saved drafts
                if ((article.IsApproved.HasValue && !article.IsApproved.Value) || (article.StartDate > DateTime.UtcNow))
                {
                    btnSaveAndPreview.Visible = true;
                }

                //#region Bind bài viết liên quan
                //if (!string.IsNullOrEmpty(article.ArticleReference))
                //{
                //    var listArticleReference = article.ArticleReference.Split(',');
                //    if (listArticleReference != null && listArticleReference.Count() > 0)
                //    {
                //        for (int i = 0; i < listArticleReference.Count(); i++)
                //        {
                //            lboxArticleReference.Items.FindByValue(listArticleReference[i]).Selected = true;
                //        }
                //    }
                //}
                //#endregion

                string pollSelectedValue = article.PollGuid.ToString();
                if (!pollSelectedValue.Equals(string.Empty))
                {
                    ListItem item = ddlPoll.Items.FindByValue(pollSelectedValue);
                    if (item != null)
                    {
                        ddlPoll.Items.FindByValue(pollSelectedValue).Selected = true;
                    }
                }
                else { ddlPoll.SelectedIndex = 0; }

                if (!string.IsNullOrEmpty(article.AudioUrl))
                {
                    TTSPanel.Visible = true;
                    divAudio.Visible = true;
                    fileAudio = SiteRoot + "/" + ConfigurationManager.AppSettings["ArticleAudiosFolder"] + article.AudioUrl;
                }
                else
                {
                    TTSPanel.Visible = false;
                    divAudio.Visible = false;
                }
                BindHistory();
                BindAttachments();
            }
            else
            {
                rdoIsApproved.SelectedIndex = 0;
                chkIsPublished.Checked = false;
                dpAddDate2.Text = DateTime.UtcNow.ToLocalTime(timeZone).ToString(dateTimeFormat);
                dpBeginDate2.Text = DateTime.UtcNow.ToLocalTime(timeZone).ToString(dateTimeFormat);// DateTimeHelper.LocalizeToCalendar(DateTime.UtcNow.ToString("g"));
                dpEndDate2.Text = string.Empty;
                btnDelete.Visible = false;
                btnDelete3.Visible = false;
                pnlHistory.Visible = false;
                if (Request.IsAuthenticated && siteUser != null)
                {
                    txtAuthor.Text = siteUser.Name;
                    txtMetaCreator.Text = siteUser.Name;
                    //txtMetaPublisher.Text =;
                }
                //set default category
                if (ddlCategories.Items.Count == 2)
                {
                    ddlCategories.SelectedIndex = 1;
                }
            }

            if ((txtItemUrl.Text.Length == 0) && (txtTitle.Text.Length > 0))
            {
                string friendlyUrl = txtTitle.Text.UrlRewriteDefault();

                txtItemUrl.Text = @"~/" + friendlyUrl;
            }

            if (article != null)
            {
                hdnTitle.Value = txtTitle.Text;
            }
            #region sự kiện
            //var listEvent = Event.GetAllForArticle(siteId);
            //if (listEvent != null && listEvent.Count > 0)
            //{
            //    lboxEvent.DataSource = listEvent;
            //    lboxEvent.DataTextField = "Title";
            //    lboxEvent.DataValueField = "ItemID";
            //    lboxEvent.DataBind();
            //}
            #endregion
            var listTag = Dictionary.GetAllPublic().Where(x => x.SiteID == siteSettings.SiteId).ToList();
            if (listTag != null && listTag.Count > 0)
            {
                lboxTag.DataSource = listTag;
                lboxTag.DataTextField = "Name";
                lboxTag.DataValueField = "ItemID";
                lboxTag.DataBind();
            }
            if (article != null)
            {
                #region sự kiện
                ////set selected for event
                //var listEventMapArticle = EventMapArticle.GetAllByArticle(article.ItemID).Select(o => o.EventID).ToList();
                //if (lboxEvent.Items.Count > 0)
                //{
                //    for (int i = 0; i < lboxEvent.Items.Count; i++)
                //    {
                //        if (listEventMapArticle.Contains(int.Parse(lboxEvent.Items[i].Value)))
                //        {
                //            lboxEvent.Items[i].Selected = true;
                //        }
                //    }
                //}
                #endregion
                //set selected for tag
                var listTagMapArticle = ArticleItemTag.GetAllByArticle(article.ItemID).Select(o => o.TagID).ToList();
                if (lboxTag.Items.Count > 0)
                {
                    for (int i = 0; i < lboxTag.Items.Count; i++)
                    {
                        if (listTagMapArticle.Contains(int.Parse(lboxTag.Items[i].Value)))
                        {
                            lboxTag.Items[i].Selected = true;
                        }
                    }
                }
            }
        }

        private void BindAttachments()
        {
            var listFile = ArticleAttachment.GetAllObjectByItemID(itemId);
            List<ArticleAttachmentBO> listFileDownload = new List<ArticleAttachmentBO>();
            if (listFile != null && listFile.Count > 0)
            {
                var chuyenmuc = new CoreCategory(article.CategoryID);
                var ngaydang = string.Format("{0:yyyyMMdd}", article.CreatedDate);
                foreach (var item in listFile)
                {
                    string downloadPath = Page.Server.MapPath(WebUtils.GetApplicationRoot() + "/Data/Sites/" + siteSettings.SiteId + "/media/ArticleAttachments/" + chuyenmuc.Name.ConvertToFTS().Replace(" ", "") + "/" + ngaydang + "/" + item.ServerFileName);
                    if (File.Exists(downloadPath))
                    {
                        listFileDownload.Add(item);
                    }
                }
            }
            rptAttachments.DataSource = listFileDownload;
            rptAttachments.DataBind();
            lblAttachments.Visible = rptAttachments.Items.Count > 0;
        }

        void rptAttachments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "DeleteItem") return;
            string ServerFileName = e.CommandArgument.ToString();
            bool IsDelAIA = ArticleItemAttachment.DeleteItemAttachmentByServerFileName(ServerFileName);
            bool IsDelAA = ArticleAttachment.DeleteAttachmentByServerFileName(ServerFileName);
            if (IsDelAIA && IsDelAA)
            {
                string path = Request.PhysicalApplicationPath + "Data/Sites/" + siteId + "/media/ArticleAttachments/" + ServerFileName;
                path = path.Replace("/", "\\");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            WebUtils.SetupRedirect(this, Request.RawUrl);
        }

        void rptAttachments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            System.Web.UI.WebControls.Image imgType = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgType");
            if (imgType == null) return;
            string name = DataBinder.Eval(e.Item.DataItem, "FileName", "{0}").Trim();
            // ReSharper disable PossibleNullReferenceException
            string imgFile = Path.GetExtension(name).ToLower().Replace(".", "") + ".png";
            string path = Request.PhysicalApplicationPath + "/Data/SiteImages/Icons/" + imgFile;
            if (File.Exists(path))
            {
                imgType.ImageUrl = ImageSiteRoot + "/Data/SiteImages/Icons/" + imgFile;
            }
            ImageButton ibtnDelete = (ImageButton)e.Item.FindControl("ibtnDelete");
            ArticleUtils.AddConfirmButton(ibtnDelete, ArticleResources.DeleteAttachmentItemConfirmation);
        }

        void btnUpload_Click(object sender, EventArgs e)
        {
            if (itemId == -1 || article == null || article.ItemID <= 0)
            {
                Page.Validate("article");
                if ((!Page.IsValid) || (!ParamsAreValid()))
                {
                    edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
                    edContent.WebEditor.Height = config.EditorHeight;
                    return;
                }
                Save();
            }
            if (article == null) { WebUtils.SetupRedirect(this, Request.RawUrl); return; }
            if ((article.ItemID <= 0) || (article.ModuleID != moduleId)) { WebUtils.SetupRedirect(this, Request.RawUrl); return; }
            if (fileUpload.HasFile)
            {
                HttpFileCollection uploadedFiles = Request.Files;
                var totalFile = uploadedFiles.Count;
                var listFile = uploadedFiles.AllKeys.ToList();
                for (int i = 0; i < totalFile; i++)
                {
                    //Hiện tại có rất nhiều file upload
                    //ta check chỉ lấy danh sách file upload của fileUpload control
                    if (listFile != null && listFile.Count > 0 && listFile[i].Contains("fileUpload"))
                    {
                        HttpPostedFile file = uploadedFiles[i];
                        if (file.ContentLength > 0 && file.FileName != null && file.FileName.Trim().Length > 0)
                        {
                            string fileExtension = Path.GetExtension(file.FileName);
                            if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedLibraryFileExtensions"))
                            {
                                lblImageUrlError.Text = ArticleResources.ImageUrlErrorFileExtension;
                                lblImageUrlError.Visible = true;
                                return;
                            }
                            //Kiem tra kich thuoc file upload
                            //if (!SiteUtils.IsValidFileSize(file.ContentLength, "AllowedLibraryFileSize"))
                            //{
                            //    lblImageUrlError.Text = ArticleResources.ImageUrlErrorFileSize + ConfigurationManager.AppSettings["AllowedLibraryFileSize"] + @" KB";
                            //    lblImageUrlError.Visible = true;
                            //    return;
                            //}
                            string serverFileName = file.FileName;// Guid.NewGuid() + fileExtension;
                            ArticleAttachment articleAttachment = new ArticleAttachment();
                            articleAttachment.FileID = 0;
                            articleAttachment.ModuleID = moduleId;
                            articleAttachment.FileName = file.FileName;
                            articleAttachment.ServerFileName = serverFileName;
                            articleAttachment.SizeInKB = (int)(file.ContentLength / 1024);
                            articleAttachment.DownloadCount = 0;
                            articleAttachment.LastModified = DateTime.Now;

                            articleAttachment.Save();

                            if (articleAttachment.FileID <= 0)
                            {
                                return;
                            }
                            else
                            {
                                var chuyenmuc = new CoreCategory(article.CategoryID);
                                var ngaydang = string.Format("{0:yyyyMMdd}", article.CreatedDate);
                                string upLoadPath = Page.Server.MapPath(WebUtils.GetApplicationRoot() + "/Data/Sites/" + siteId + "/media/ArticleAttachments/" + chuyenmuc.Name.ConvertToFTS().Replace(" ", "") + "/" + ngaydang + "/");
                                if (!Directory.Exists(upLoadPath))
                                {
                                    Directory.CreateDirectory(upLoadPath);
                                }
                                ArticleItemAttachment articleItemAttachment = new ArticleItemAttachment();
                                articleItemAttachment.ModuleID = moduleId;
                                articleItemAttachment.ItemID = article.ItemID;
                                articleItemAttachment.FileID = articleAttachment.FileID;
                                if (articleItemAttachment.Save())
                                {
                                    string destPath = upLoadPath + serverFileName;
                                    if (File.Exists(destPath))
                                    {
                                        File.Delete(destPath);
                                    }
                                    file.SaveAs(destPath);
                                    //file.MoveTo(destPath, MoveToOptions.Overwrite);
                                }
                            }
                        }
                    }
                }
            }
            CurrentPage.UpdateLastModifiedTime();
            CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
            var url = SiteRoot + "/article/editpost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
                        article.ItemID;
            WebUtils.SetupRedirect(this, url);

        }

        void btnDeleteImg_Click(object sender, ImageClickEventArgs e)
        {
            if (article == null) { article = new Article(itemId); }

            DeleteImageFromServer();
            divImage.Visible = false;
            article.ImageUrl = string.Empty;
            article.Save();
            if (articleSync == null) return;
            articleSync.ImageUrl = string.Empty;
            articleSync.Save();
        }
        //btnDeleteAudio_Click
        void btnDeleteAudio_Click(object sender, ImageClickEventArgs e)
        {
            if (article == null) { article = new Article(itemId); }

            DeleteAudioFromServer();
            divAudio.Visible = false;
            article.AudioUrl = string.Empty;
            article.Save();
            if (articleSync == null) return;
            articleSync.AudioUrl = string.Empty;
            articleSync.Save();
        }
        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static int SaveTag(string tag = "", string description = "")
        {
            var site = CacheHelper.GetCurrentSiteSettings();

            Dictionary dictionary = new Dictionary();

            dictionary.Censorship = true;
            dictionary.Description = description;
            dictionary.DateCreate = DateTime.Now;
            dictionary.DateApprove = DateTime.Now;
            dictionary.IsPublic = true;
            dictionary.Name = tag;
            dictionary.SiteID = site.SiteId;
            dictionary.UserApprove = userid;
            dictionary.UserCreate = userid;
            dictionary.Save();
            return dictionary.ItemID;
        }

        protected virtual void btnUpdate_Click(object sender, EventArgs e)
        {
            if (itemId <= 0)
            {
                if (itemId == -1 || article == null || article.ItemID <= 0)
                {
                    Page.Validate("article");
                    if ((!Page.IsValid) || (!ParamsAreValid()))
                    {
                        edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
                        edContent.WebEditor.Height = config.EditorHeight;
                        return;
                    }
                    Save();
                }
                if (article == null)
                {
                    WebUtils.SetupRedirect(this, Request.RawUrl); return;
                }
                if ((article.ItemID <= 0) || (article.ModuleID != moduleId))
                {
                    WebUtils.SetupRedirect(this, Request.RawUrl); return;
                }
                if (fileUpload.HasFile)
                {
                    HttpFileCollection uploadedFiles = Request.Files;
                    var totalFile = uploadedFiles.Count;
                    var listFile = uploadedFiles.AllKeys.ToList();
                    for (int i = 0; i < totalFile; i++)
                    {
                        //Hiện tại có rất nhiều file upload
                        //ta check chỉ lấy danh sách file upload của fileUpload control
                        if (listFile != null && listFile.Count > 0 && listFile[i].Contains("fileUpload"))
                        {
                            HttpPostedFile file = uploadedFiles[i];
                            if (file.ContentLength > 0 && file.FileName != null && file.FileName.Trim().Length > 0)
                            {
                                string fileExtension = Path.GetExtension(file.FileName);
                                if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedLibraryFileExtensions"))
                                {
                                    lblImageUrlError.Text = ArticleResources.ImageUrlErrorFileExtension;
                                    lblImageUrlError.Visible = true;
                                    return;
                                }
                                //Kiem tra kich thuoc file upload
                                //if (!SiteUtils.IsValidFileSize(file.ContentLength, "AllowedLibraryFileSize"))
                                //{
                                //    lblImageUrlError.Text = ArticleResources.ImageUrlErrorFileSize + ConfigurationManager.AppSettings["AllowedLibraryFileSize"] + @" KB";
                                //    lblImageUrlError.Visible = true;
                                //    return;
                                //}
                                string serverFileName = file.FileName;// Guid.NewGuid() + fileExtension;
                                ArticleAttachment articleAttachment = new ArticleAttachment();
                                articleAttachment.FileID = 0;
                                articleAttachment.ModuleID = moduleId;
                                articleAttachment.FileName = file.FileName;
                                articleAttachment.ServerFileName = serverFileName;
                                articleAttachment.SizeInKB = (int)(file.ContentLength / 1024);
                                articleAttachment.DownloadCount = 0;
                                articleAttachment.LastModified = DateTime.Now;

                                articleAttachment.Save();

                                if (articleAttachment.FileID <= 0)
                                {
                                    return;
                                }
                                else
                                {
                                    var chuyenmuc = new CoreCategory(article.CategoryID);
                                    var ngaydang = string.Format("{0:yyyyMMdd}", article.CreatedDate);
                                    string upLoadPath = Page.Server.MapPath(WebUtils.GetApplicationRoot() + "/Data/Sites/" + siteId + "/media/ArticleAttachments/" + chuyenmuc.Name.ConvertToFTS().Replace(" ", "") + "/" + ngaydang + "/");
                                    if (!Directory.Exists(upLoadPath))
                                    {
                                        Directory.CreateDirectory(upLoadPath);
                                    }
                                    ArticleItemAttachment articleItemAttachment = new ArticleItemAttachment();
                                    articleItemAttachment.ModuleID = moduleId;
                                    articleItemAttachment.ItemID = article.ItemID;
                                    articleItemAttachment.FileID = articleAttachment.FileID;
                                    if (articleItemAttachment.Save())
                                    {
                                        string destPath = upLoadPath + serverFileName;
                                        if (File.Exists(destPath))
                                        {
                                            File.Delete(destPath);
                                        }
                                        file.SaveAs(destPath);
                                        //file.MoveTo(destPath, MoveToOptions.Overwrite);
                                    }
                                }
                            }
                        }
                    }
                }
                CurrentPage.UpdateLastModifiedTime();
                CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
                var url = SiteRoot + "/article/editpost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
                            article.ItemID;
                WebUtils.SetupRedirect(this, url);
            }
            else
            {
                Page.Validate("article");
                if (!Page.IsValid)
                {
                    edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
                    edContent.WebEditor.Height = config.EditorHeight;
                    return;
                }
                if (!Save()) return;
            }
            WebUtils.SetupRedirect(this, SiteRoot + article.ItemUrl.Replace("~/", "/"));
            //string url = SiteRoot + "/article/editpost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
            //             article.ItemID;
            //if (itemId > -1 || !config.UseAttachmentSetting)
            //{
            //    WebUtils.SetupRedirect(this, SiteRoot + article.ItemUrl.Replace("~/", "/"));
            //}
            //else
            //{
            //    WebUtils.SetupRedirect(this, url);
            //}
        }

        void btnSaveAndPreview_Click(object sender, EventArgs e)
        {

            if (itemId <= 0)
            {
                if (itemId == -1 || article == null || article.ItemID <= 0)
                {
                    Page.Validate("article");
                    if ((!Page.IsValid) || (!ParamsAreValid()))
                    {
                        edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
                        edContent.WebEditor.Height = config.EditorHeight;
                        return;
                    }
                    Save();
                }
                if (article == null) { WebUtils.SetupRedirect(this, Request.RawUrl); return; }
                if ((article.ItemID <= 0) || (article.ModuleID != moduleId)) { WebUtils.SetupRedirect(this, Request.RawUrl); return; }
                if (fileUpload.HasFile)
                {
                    HttpFileCollection uploadedFiles = Request.Files;
                    var totalFile = uploadedFiles.Count;
                    var listFile = uploadedFiles.AllKeys.ToList();
                    for (int i = 0; i < totalFile; i++)
                    {
                        //Hiện tại có rất nhiều file upload
                        //ta check chỉ lấy danh sách file upload của fileUpload control
                        if (listFile != null && listFile.Count > 0 && listFile[i].Contains("fileUpload"))
                        {
                            HttpPostedFile file = uploadedFiles[i];
                            if (file.ContentLength > 0 && file.FileName != null && file.FileName.Trim().Length > 0)
                            {
                                string fileExtension = Path.GetExtension(file.FileName);
                                if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedLibraryFileExtensions"))
                                {
                                    lblImageUrlError.Text = ArticleResources.ImageUrlErrorFileExtension;
                                    lblImageUrlError.Visible = true;
                                    return;
                                }
                                //Kiem tra kich thuoc file upload
                                //if (!SiteUtils.IsValidFileSize(file.ContentLength, "AllowedLibraryFileSize"))
                                //{
                                //    lblImageUrlError.Text = ArticleResources.ImageUrlErrorFileSize + ConfigurationManager.AppSettings["AllowedLibraryFileSize"] + @" KB";
                                //    lblImageUrlError.Visible = true;
                                //    return;
                                //}
                                string serverFileName = file.FileName;// Guid.NewGuid() + fileExtension;
                                ArticleAttachment articleAttachment = new ArticleAttachment();
                                articleAttachment.FileID = 0;
                                articleAttachment.ModuleID = moduleId;
                                articleAttachment.FileName = file.FileName;
                                articleAttachment.ServerFileName = serverFileName;
                                articleAttachment.SizeInKB = (int)(file.ContentLength / 1024);
                                articleAttachment.DownloadCount = 0;
                                articleAttachment.LastModified = DateTime.Now;

                                articleAttachment.Save();

                                if (articleAttachment.FileID <= 0)
                                {
                                    return;
                                }
                                else
                                {
                                    var chuyenmuc = new CoreCategory(article.CategoryID);
                                    var ngaydang = string.Format("{0:yyyyMMdd}", article.CreatedDate);
                                    string upLoadPath = Page.Server.MapPath(WebUtils.GetApplicationRoot() + "/Data/Sites/" + siteId + "/media/ArticleAttachments/" + chuyenmuc.Name.ConvertToFTS().Replace(" ", "") + "/" + ngaydang + "/");
                                    if (!Directory.Exists(upLoadPath))
                                    {
                                        Directory.CreateDirectory(upLoadPath);
                                    }
                                    ArticleItemAttachment articleItemAttachment = new ArticleItemAttachment();
                                    articleItemAttachment.ModuleID = moduleId;
                                    articleItemAttachment.ItemID = article.ItemID;
                                    articleItemAttachment.FileID = articleAttachment.FileID;
                                    if (articleItemAttachment.Save())
                                    {
                                        string destPath = upLoadPath + serverFileName;
                                        if (File.Exists(destPath))
                                        {
                                            File.Delete(destPath);
                                        }
                                        file.SaveAs(destPath);
                                        //file.MoveTo(destPath, MoveToOptions.Overwrite);
                                    }
                                }
                            }
                        }
                    }
                }
                CurrentPage.UpdateLastModifiedTime();
                CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
                var url = SiteRoot + "/article/editpost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
                            article.ItemID;
                WebUtils.SetupRedirect(this, url);
            }
            else
            {
                Page.Validate("article");
                if ((!Page.IsValid) || (!ParamsAreValid()))
                {
                    edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
                    edContent.WebEditor.Height = config.EditorHeight;
                    return;
                }
                if (!Save()) return;
                //btnUpload_Click(null, null);
            }
            WebUtils.SetupRedirect(this, SiteRoot + article.ItemUrl.Replace("~/", "/"));
        }

        private void PopulatePolls()
        {
            var listPollActives = Poll.GetActivePolls(siteSettings.SiteGuid);
            ddlPoll.DataSource = listPollActives;
            ddlPoll.DataTextField = "Question";
            ddlPoll.DataValueField = "PollGuid";
            ddlPoll.DataBind();
            ddlPoll.Items.Insert(0, new ListItem(ArticleResources.ParentPollChoose, ""));
        }

        private void PopulateCategories()
        {
            string listCategoryConfig = pageSettings.CategoryConfig;
            if (!string.IsNullOrEmpty(config.ArticleCategoryConfig))
                listCategoryConfig = config.ArticleCategoryConfig;
            var list_category_config = listCategoryConfig.Split('-');
            CoreCategory coreCategory = new CoreCategory();
            foreach (var item in list_category_config)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    coreCategory = new CoreCategory(int.Parse(item));
                    ListItem list = new ListItem
                    {
                        Text = coreCategory.Name,
                        Value = item
                    };
                    ddlCategories.Items.Add(list);
                }
            }
            PopulateChildNode(ddlCategories);
            ddlCategories.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
        }
        private void PopulateCategoriesReference()
        {
            var coreCategory = new CoreCategory(siteSettings.ArticleCategory);
            ListItem list = new ListItem
            {
                Text = coreCategory.Name,
                Value = siteSettings.ArticleCategory.ToString()
            };
            ddlCategoryReference.Items.Add(list);
            PopulateChildNode(ddlCategoryReference);
            ddlCategoryReference.Items.RemoveAt(0);
            ddlCategoryReference.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
        }

        private void PopulateChildNode(ListControl root)
        {
            for (int i = 0; i < root.Items.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(root.Items[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root.Items[i].Text.StartsWith("|"))
                {
                    prefix += root.Items[i].Text.Substring(0, 3);
                    root.Items[i].Text = root.Items[i].Text.Remove(0, 3);
                }
                root.Items[i].Text = prefix + root.Items[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString()
                    };
                    root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    index++;
                }
            }
        }

        private bool ParamsAreValid()
        {
            try
            {
                DateTime.Parse(dpBeginDate2.Text);
            }
            catch (FormatException)
            {
                lblErrorMessage.Text = ArticleResources.ParseDateFailureMessage;
                return false;
            }
            catch (ArgumentNullException)
            {
                lblErrorMessage.Text = ArticleResources.ParseDateFailureMessage;
                return false;
            }
            return true;
        }

        private string SuggestUrl()
        {
            string pageName = txtTitle.Text;
            return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }
        private bool Save()
        {
            Module module = new Module(moduleId);
            if (module == null) { return false; }

            if (siteUser == null) { return false; }

            if (article == null)
            {
                article = new Article(itemId);
                article.UserGuid = siteUser.UserGuid;
            }
            string currentDescription = article.Description;
            string currentTitle = article.Title;
            //Module module = GetModule(moduleId);


            article.ArticleReference = ArticleReferenceAtive.Value;

            article.LastModUserGuid = siteUser.UserGuid;
            article.SiteId = siteId;
            article.ModuleID = moduleId;
            article.ModuleGuid = module.ModuleGuid;
            article.Title = txtTitle.Text;
            article.Summary = txtSummary.Text;
            article.Description = edContent.Text;
            //Thêm vào RSS
            article.IncludeInFeed = chkIncludeInFeed.Checked;
            //Tin hot
            article.IsHot = chkIsHot.Checked;
            //Hiển thị trên trang chủ
            article.IsHome = chkIsHome.Checked;
            //tin tiêu điểm
            //article.IsHotNew = ckHotNew.Checked;
            article.AllowWCAG = chkIsAllowWCAG.Checked;
            //Kiểm duyệt nội dung tin bài
            //article.IsApproved = chkIsApproved.Checked;
            DateTime localTime = DateTime.Parse(dpBeginDate2.Text, CultureInfo.CurrentCulture);
            article.StartDate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
            if (!string.IsNullOrEmpty(dpEndDate2.Text))
            {
                DateTime endTime = DateTime.Parse(dpEndDate2.Text, CultureInfo.CurrentCulture);
                article.EndDate = timeZone != null ? endTime.ToUtc(timeZone) : endTime.AddHours(-timeOffset);
            }
            else
            {
                article.EndDate = null;
            }
            DateTime lastModified = DateTime.Now;
            article.LastModUtc = lastModified;
            if (isApprover)
            {
                article.IsApproved = rdoIsApproved.SelectedItem.Value == "0" ? false : true;
                article.CommentByBoss = txtCommentByBoss.Text;
                article.IsPublished = chkIsPublished.Checked;
            }
            //if (chkIsApproved.Checked)
            if (article.IsApproved.HasValue && article.IsApproved.Value)
            {
                article.ApprovedDate = DateTime.Now;
                article.ApprovedGuid = siteUser.UserGuid;
            }
            //Xuất bản nội dung tin bài khi tin bài đã được phê duyệt
            if (isUserPost && !isApprover && article.IsApproved.HasValue && article.IsApproved.Value)
            {
                article.IsPublished = chkIsPublished.Checked;
            }
            if (article.IsPublished.HasValue && article.IsPublished.Value)
            {
                article.PublishedDate = DateTime.Now;
                article.PublishedGuid = siteUser.UserGuid;
            }
            //Cho phép bình luận
            article.AllowComment = chkIsAllowComment.Checked;
            //Nhận xét của cấp trên

            //article.Tag = txtTag.Text;

            article.CreatedByUser = txtAuthor.Text;

            //Cơ quan ban hành meta
            article.MetaPublisher = txtMetaPublisher.Text;
            //Thời gian meta
            if (!string.IsNullOrEmpty(calenderMetaDate.Text))
            {
                try
                {
                    DateTime metaTime = DateTime.Parse(calenderMetaDate.Text, CultureInfo.CurrentCulture);
                    article.MetaDate = timeZone != null ? metaTime.ToUtc(timeZone) : metaTime.AddHours(-timeOffset);
                }
                catch (FormatException)
                {
                    lblMetaTimeError.Text = ArticleResources.ParseDateFailureMessage;
                    return false;
                }

            }
            else
            {
                article.MetaDate = article.StartDate;
            }
            //Người tạo meta
            if (txtMetaCreator.Text == "")
            {
                article.MetaCreator = txtAuthor.Text;
            }
            else
            {
                article.MetaCreator = txtMetaCreator.Text;
            }
            //Tiêu đề meta
            if (txtMetaTitle.Text == "")
            {
                article.MetaTitle = txtTitle.Text;
            }
            else
            {
                article.MetaTitle = txtMetaTitle.Text;
            }
            //Mô tả meta
            if (txtMetaDescription.Text == "")
            {
                article.MetaDescription = txtSummary.Text;
            }
            else
            {
                article.MetaDescription = txtMetaDescription.Text;
            }
            DateTime localAddTime = DateTime.Now;
            if (!string.IsNullOrEmpty(dpAddDate2.Text))
            {
                localAddTime = DateTime.Parse(dpAddDate2.Text, CultureInfo.CurrentCulture);
            }
            article.CreatedDate = timeZone != null ? localAddTime.ToUtc(timeZone) : localAddTime.AddHours(-timeOffset);
            if (txtItemUrl.Text.Length == 0)
            {
                txtItemUrl.Text = SuggestUrl();
            }

            //#region Lưu bài viết liên quan
            //var articleReference = string.Empty;
            //for (int i = 0; i < lboxArticleReference.Items.Count; i++)
            //{
            //    if (lboxArticleReference.Items[i].Selected)
            //    {
            //        articleReference += lboxArticleReference.Items[i].Value + ",";
            //    }
            //}
            //article.ArticleReference = articleReference;
            //#endregion


            String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", String.Empty));
            FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

            if (
                ((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != article.ArticleGuid))
                && (article.ItemUrl != txtItemUrl.Text)
                )
            {
                lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                return false;
            }

            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString))
                {
                    lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                    return false;
                }
            }

            string oldUrl = article.ItemUrl.Replace("~/", string.Empty);
            string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));

            article.ItemUrl = "~/" + newUrl;
            //Lưu ảnh đại diện
            string fileName;
            if (!string.IsNullOrEmpty(ImageName.Value))
            {
                article.ImageUrl = ImageName.Value;
            }
            //if (!SaveImageUrl(out fileName))
            //{
            //    return false;
            //}
            //if (!fileName.Equals(string.Empty))
            //{
            //    article.ImageUrl = fileName;
            //}
            //lưu audio file for WCAG site
            string fileAudioName;
            if (!SaveAudioUrl(out fileAudioName))
            {
                return false;
            }
            if (!fileAudioName.Equals(string.Empty))
            {
                article.AudioUrl = fileAudioName;
            }
            //Lưu lịch sử phiên bản nội dung
            if (enableContentVersioning)
            {
                article.CreateHistory(siteSettings.SiteGuid);
                //if (currentTitle != article.Title || currentDescription != article.Description)
                //{
                //    ContentHistory contenHistory = new ContentHistory();
                //    contenHistory.SiteGuid = siteSettings.SiteGuid;
                //    contenHistory.UserGuid = article.LastModUserGuid;
                //    contenHistory.ContentGuid = article.ArticleGuid;
                //    contenHistory.Title = article.Title;
                //    contenHistory.ContentText = article.Description;
                //    contenHistory.CreatedUtc = article.LastModUtc;
                //    contenHistory.Save();
                //}
            }
            article.CategoryID = int.Parse(ddlCategories.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(ddlPoll.SelectedValue.ToString()))
            {
                article.PollGuid = Guid.Parse(ddlPoll.SelectedValue.ToString());
            }
            else
            {
                article.PollGuid = Guid.Empty;
            }
            //todo build fts for article
            var fulltext = article.Title + " " + article.Summary + " " + article.Description;
            fulltext = fulltext.ConvertToFTS();
            if (!string.IsNullOrEmpty(fulltext))
            {
                article.FTS = fulltext;
            }
            else
            {
                article.FTS = (article.Title + article.Summary).ConvertToVN();
            }
            //Định danh meta

            if (!String.IsNullOrEmpty(txtMetaIdentifier.Text))
            {
                article.MetaIdentifier = txtMetaIdentifier.Text;
            }
            else
            {
                article.MetaIdentifier = article.ItemUrl;
            }
            article.Save();

            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                {
                    FriendlyUrl newFriendlyUrl = new FriendlyUrl
                    {
                        SiteId = siteSettings.SiteId,
                        SiteGuid = siteSettings.SiteGuid,
                        PageGuid = article.ArticleGuid,
                        Url = friendlyUrlString,
                        RealUrl = "~/Article/ViewPost.aspx?pageid="
                                  + pageId.ToInvariantString()
                                  + "&mid=" + article.ModuleID.ToInvariantString()
                                  + "&ItemID=" + article.ItemID.ToInvariantString()
                    };

                    newFriendlyUrl.Save();
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
                    if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == article.ArticleGuid))
                    {
                        FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                    }

                }
            }
            else
            {
                if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                {
                    string realUrl = string.Empty;
                    realUrl = "~/Article/ViewPost.aspx?pageid="
                                 + pageId.ToInvariantString()
                                  + "&mid=" + article.ModuleID.ToInvariantString()
                                  + "&ItemID=" + article.ItemID.ToInvariantString();
                    FriendlyUrl updateFriendlyUrl = new FriendlyUrl(friendlyUrl.UrlId);
                    if (updateFriendlyUrl != null)
                    {
                        updateFriendlyUrl.RealUrl = realUrl;
                        updateFriendlyUrl.Save();
                    }
                }
            }
            #region Lưu log bài viết
            ArticleLog articleLog = new ArticleLog();
            articleLog.ArticleID = article.ItemID;
            articleLog.Comment = txtCommentByBoss.Text;
            if (!string.IsNullOrEmpty(dpEndDate2.Text))
            {
                articleLog.EndDate = DateTime.Parse(dpEndDate2.Text, CultureInfo.CurrentCulture);

            }
            articleLog.IsApprove = rdoIsApproved.SelectedItem.Value == "0" ? false : true;
            articleLog.IsPublic = chkIsPublished.Checked;
            if (!string.IsNullOrEmpty(dpAddDate2.Text))
            {
                articleLog.PostDate = DateTime.Parse(dpAddDate2.Text, CultureInfo.CurrentCulture);

            }
            if (!string.IsNullOrEmpty(dpBeginDate2.Text))
            {
                articleLog.StartDate = DateTime.Parse(dpBeginDate2.Text, CultureInfo.CurrentCulture);

            }
            articleLog.UserID = siteUser.UserId;
            articleLog.Save();

            #endregion

            #region lưu sự kiện
            //Lưu vào bảng map với sự kiện
            //if (lboxEvent.Items.Count > 0)
            //{
            //    //Lấy các event đã được map
            //    var mapEventHas = EventMapArticle.GetAllByArticle(article.ItemID);
            //    string events = "";
            //    string event_select = "";
            //    if (mapEventHas != null && mapEventHas.Count > 0)
            //    {
            //        foreach (var item in mapEventHas)
            //        {
            //            events += item.EventID + ",";
            //        }
            //    }
            //    for (int i = 0; i < lboxEvent.Items.Count; i++)
            //    {
            //        if (lboxEvent.Items[i].Selected)
            //        {
            //            event_select += lboxEvent.Items[i].Value + ",";
            //        }
            //    }
            //    if (events != event_select)
            //    {
            //        //Xóa các event đã được map
            //        var hasDelete = EventMapArticle.DeleteAllByArticle(article.ItemID);
            //        //Insert các event mới
            //        var listEvent = event_select.Split(',');
            //        foreach (var item in listEvent)
            //        {
            //            if (!string.IsNullOrEmpty(item))
            //            {
            //                EventMapArticle mapEvent = new EventMapArticle();
            //                mapEvent.ArticleID = article.ItemID;
            //                mapEvent.EventID = int.Parse(item);
            //                mapEvent.Save();
            //            }
            //        }
            //    }
            //}
            #endregion

            //Lưu vào bảng map với từ điển (tag)
            if (lboxTag.Items.Count > 0)
            {
                //Lấy các tag đã được map
                var mapTagHas = ArticleItemTag.GetAllByArticle(article.ItemID);
                string tag = "";
                string tag_select = "";
                if (mapTagHas != null && mapTagHas.Count > 0)
                {
                    foreach (var item in mapTagHas)
                    {
                        tag += item.TagID + ",";
                    }
                }
                for (int i = 0; i < lboxTag.Items.Count; i++)
                {
                    if (lboxTag.Items[i].Selected)
                    {
                        tag_select += lboxTag.Items[i].Value + ",";
                    }
                }
                if (tag != tag_select)
                {
                    //Xóa các tag đã được map
                    var hasDelete = ArticleItemTag.DeleteAllByArticle(article.ItemID);
                    //Insert các tag mới
                    var listTag = tag_select.Split(',');
                    foreach (var item in listTag)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            ArticleItemTag mapTag = new ArticleItemTag();
                            mapTag.ModuleID = article.ModuleID;
                            mapTag.ItemID = article.ItemID;
                            mapTag.TagID = int.Parse(item);
                            mapTag.Save();
                        }
                    }
                }
            }

            // new item posted so ping services
            if ((itemId == -1) && (article.IsApproved.HasValue && article.IsApproved.Value) && (article.StartDate <= DateTime.UtcNow))
            {
                QueuePings();
            }

            CurrentPage.UpdateLastModifiedTime();

            String blogFriendlyUrl = "article" + article.ModuleID.ToInvariantString() + "rss.aspx";
            if (!FriendlyUrl.Exists(siteSettings.SiteId, blogFriendlyUrl))
            {
                FriendlyUrl rssUrl = new FriendlyUrl
                {
                    SiteId = siteSettings.SiteId,
                    SiteGuid = siteSettings.SiteGuid,
                    PageGuid = article.ModuleGuid,
                    Url = blogFriendlyUrl,
                    RealUrl = "~/Article/RSS.aspx?pageid=" + pageId.ToInvariantString()
                              + "&mid=" + article.ModuleID.ToInvariantString()
                };
                rssUrl.Save();
            }

            //SaveSyncPost();
            CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
            SiteUtils.QueueIndexing();
            //EmailNewPost();
            return true;
        }
        #region SaveSyncPost
        //private void SaveSyncPost()
        //{
        //    if (!chkSyncPost.Checked)
        //    {
        //        if (articleSync != null)
        //        {
        //            articleSync.ContentChanged += blog_ContentChanged;
        //            articleSync.DeleteSync();
        //            FriendlyUrl.DeleteByPageGuid(articleSync.ArticleGuid);
        //            article.SyncItemID = 0;
        //            article.Save();
        //        }
        //        return;
        //    }
        //    if (loadedModuleId < 0) return;
        //    if (loadedSiteId < 1) return;
        //    if (articleSync == null)
        //    {
        //        articleSync = new Article();
        //    }
        //    Module moduleSync = new Module(loadedModuleId);
        //    articleSync.AllowCommentsForDays = article.AllowCommentsForDays;
        //    articleSync.CompiledMeta = article.CompiledMeta;
        //    articleSync.CreatedUtc = article.CreatedUtc;
        //    articleSync.Description = article.Description;
        //    articleSync.Excerpt = article.Excerpt;
        //    articleSync.HitCount = article.HitCount;
        //    articleSync.ImageUrl = article.ImageUrl;
        //    articleSync.IsHot = article.IsHot;
        //    articleSync.IsRandomize = article.IsRandomize;
        //    articleSync.IsApproved = chkIsApprovedSyncPost.Checked;
        //    articleSync.IsInNewsletter = article.IsInNewsletter;
        //    articleSync.IncludeInFeed = article.IncludeInFeed;
        //    articleSync.LastModUserGuid = article.LastModUserGuid;
        //    articleSync.LastModUtc = article.LastModUtc;
        //    articleSync.Location = article.Location;
        //    articleSync.MetaDescription = article.MetaDescription;
        //    articleSync.MetaKeywords = article.MetaKeywords;
        //    articleSync.ModuleGuid = moduleSync.ModuleGuid;
        //    articleSync.ModuleId = moduleSync.ModuleId;
        //    articleSync.StartDate = article.StartDate;
        //    articleSync.Title = article.Title;
        //    articleSync.UserGuid = article.UserGuid;
        //    articleSync.UserName = article.UserName;
        //    articleSync.SyncItemID = article.ItemId;
        //    articleSync.Tag = article.Tag;
        //    articleSync.OverrideUrl = article.OverrideUrl;
        //    articleSync.Save();
        //    article.SyncItemID = articleSync.ItemId;
        //    article.Save();
        //    String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", String.Empty));
        //    FriendlyUrl friendlyUrl = new FriendlyUrl(settingsSync.SiteId, friendlyUrlString);

        //    if (((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != articleSync.ArticleGuid))
        //        && (articleSync.ItemUrl != txtItemUrl.Text)
        //        )
        //    {
        //        lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
        //        return;
        //    }

        //    if (!friendlyUrl.FoundFriendlyUrl)
        //    {
        //        if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString))
        //        {
        //            lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
        //            return;
        //        }
        //    }

        //    string oldUrl = articleSync.ItemUrl.Replace("~/", string.Empty);
        //    string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));

        //    articleSync.ItemUrl = "~/" + newUrl;
        //    articleSync.Save();

        //    if (!friendlyUrl.FoundFriendlyUrl)
        //    {
        //        if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
        //        {
        //            FriendlyUrl newFriendlyUrl = new FriendlyUrl
        //            {
        //                SiteId = settingsSync.SiteId,
        //                SiteGuid = settingsSync.SiteGuid,
        //                PageGuid = articleSync.ArticleGuid,
        //                Url = friendlyUrlString,
        //                RealUrl = "~/Article/ViewPost.aspx?pageid="
        //                          + loadedPageId.ToInvariantString()
        //                          + "&mid=" + articleSync.ModuleId.ToInvariantString()
        //                          + "&ItemID=" + articleSync.ItemId.ToInvariantString()
        //            };

        //            newFriendlyUrl.Save();
        //        }

        //        //if post was renamed url will change, if url changes we need to redirect from the old url to the new with 301
        //        if ((oldUrl.Length > 0) && (newUrl.Length > 0) && (!SiteUtils.UrlsMatch(oldUrl, newUrl)))
        //        {
        //            //worry about the risk of a redirect loop if the page is restored to the old url again
        //            // don't create it if a redirect for the new url exists
        //            if (
        //                (!RedirectInfo.Exists(settingsSync.SiteId, oldUrl))
        //                && (!RedirectInfo.Exists(settingsSync.SiteId, newUrl))
        //                )
        //            {
        //                RedirectInfo redirect = new RedirectInfo
        //                {
        //                    SiteGuid = settingsSync.SiteGuid,
        //                    SiteId = settingsSync.SiteId,
        //                    OldUrl = oldUrl,
        //                    NewUrl = newUrl
        //                };
        //                redirect.Save();
        //            }
        //            // since we have created a redirect we don't need the old friendly url
        //            FriendlyUrl oldFriendlyUrl = new FriendlyUrl(settingsSync.SiteId, oldUrl);
        //            if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == articleSync.ArticleGuid))
        //            {
        //                FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
        //            }

        //        }
        //    }
        //    Article.DeleteItemCategories(articleSync.ItemId);
        //    // Mono doesn't see this in update panel
        //    // so help find it
        //    if (lbCategoriesSyncPost == null)
        //    {
        //        log.Error("lbCategoriesSyncPost was null");

        //        lbCategoriesSyncPost = (ListBox)UpdatePanel1.FindControl("lbCategoriesSyncPost");
        //    }
        //    Int32 categoryId;
        //    Int32.TryParse(lbCategoriesSyncPost.SelectedValue, out categoryId);
        //    if (categoryId > 0)
        //    {
        //        Article.AddItemCategory(articleSync.ItemId, categoryId);
        //    }
        //}
        #endregion
        private void EmailNewPost()
        {
            if (config.EmailNewPost == null || config.EmailNewPost.Count == 0) return;
            SmtpSettings smtpSettings = SiteUtils.GetSmtpSettings();
            string fromAddress = siteSettings.DefaultEmailFromAddress;
            StringBuilder message = new StringBuilder();
            if (article != null)
            {
                if (articleSync != null)
                {
                    message.Append(String.Format(ArticleResources.SubjectEmailNewPostBody, siteUser.Name,
                                                 settingsSync.SiteName, "<a href=" +
                                                 Page.ResolveUrl(ImageSiteRoot + settingsSync.SiteFolderName +
                                                                 articleSync.ItemUrl.Replace("~", string.Empty)) + ">" + articleSync.Title + "</a>",
                                                 "<a href=" +
                                                 Page.ResolveUrl(ImageSiteRoot + settingsSync.SiteFolderName + "/Article/EditPost.aspx?pageid=" + loadedPageId + "&mid=" + articleSync.ModuleID + "&itemid=" + articleSync.ItemID) + ">",
                                                 "</a>", "<p>", "</p>"));
                }
                else
                {
                    message.Append(String.Format(ArticleResources.SubjectEmailNewPostBodyNoSync, siteUser.Name,
                                                 siteSettings.SiteName,
                                                 "<a href=" +
                                                 Page.ResolveUrl(SiteRoot + article.ItemUrl.Replace("~", string.Empty)) +
                                                 ">" + article.Title + "</a>", "<p>", "</p>"));
                }
            }
            string subject = ArticleResources.SubjectEmailNewPost;
            if (article != null)
            {
                subject = article.Title;
            }
            foreach (string to in config.EmailNewPost)
            {
                EmailMessageTask messageTask = new EmailMessageTask(smtpSettings)
                {
                    EmailFrom = fromAddress,
                    EmailBcc = string.Empty,
                    EmailTo = to,
                    Subject = subject,
                    TextBody = message.ToString(),
                    SiteGuid = siteSettings.SiteGuid,
                    PreferredEncoding = "utf-8"
                };
                messageTask.QueueTask();
                WebTaskManager.StartOrResumeTasks();
            }
        }

        private bool SaveImageUrl(out string fileName)
        {
            String pathToApplicationsFolder
                = HttpContext.Current.Server.MapPath(
                "~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"]);
            if (!Directory.Exists(pathToApplicationsFolder))
            {
                Directory.CreateDirectory(pathToApplicationsFolder);
            }
            bool flag = false;
            int width = 0;
            int height = 0;
            fileName = string.Empty;
            try
            {
                //Check valid file upload
                if (nuImageUrl.HasFile && nuImageUrl.ContentLength > 0)
                {

                    string fileExtension = Path.GetExtension(nuImageUrl.FileName);
                    Double fileSize = nuImageUrl.ContentLength / 1024;

                    //Kiem tra ten mo rong file upload
                    if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedImageFileExtensions"))
                    {
                        lblImageUrlError.Text = ArticleResources.ImageUrlErrorFileExtension;
                        return false;
                    }
                    //Kiem tra kich thuoc file upload
                    if (!SiteUtils.IsValidFileSize(fileSize, "AllowedImageSize"))
                    {
                        lblImageUrlError.Text = ArticleResources.ImageUrlErrorFileSize + ConfigurationManager.AppSettings["AllowedImageSize"] + @" KB";
                        return false;
                    }
                    string path = Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"]);
                    string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    System.Drawing.Image image = System.Drawing.Image.FromStream(nuImageUrl.FileContent);
                    nuImageUrl.FileContent.Close();
                    int resizeWidth;
                    int.TryParse(ConfigurationManager.AppSettings["ArticleImageMaxWidth"], out resizeWidth);
                    int resizeHeight;
                    int.TryParse(ConfigurationManager.AppSettings["ArticleImageMaxHeight"], out resizeHeight);
                    int thumbnailWidth;
                    int.TryParse(ConfigurationManager.AppSettings["ArticleImageMaxThumbnailWidth"], out thumbnailWidth);
                    int thumbnailHeight;
                    int.TryParse(ConfigurationManager.AppSettings["ArticleImageMaxThumbnailHeight"], out thumbnailHeight);
                    SiteUtils.ResizeImage(ref width, ref height, resizeWidth, resizeHeight, image.Width, image.Height);
                    fileName = path + guid + "_t" + fileExtension;
                    if (height != 0)
                    {
                        using (Bitmap bitmap = new Bitmap(image, width, height))
                        {
                            bitmap.Save(fileName, image.RawFormat);
                        }
                    }
                    else
                    {
                        using (Bitmap bitmap = new Bitmap(image, image.Width, image.Height))
                        {
                            bitmap.Save(fileName, image.RawFormat);
                        }
                        //nuImageUrl.MoveTo(fileName, MoveToOptions.Overwrite);
                        //fuImageUrl.PostedFile.SaveAs(fileName);
                    }
                    SiteUtils.ResizeImage(ref width, ref height, thumbnailWidth, thumbnailHeight, image.Width, image.Height);
                    fileName = path + guid + fileExtension;
                    if (height != 0)
                    {
                        using (Bitmap bitmap = new Bitmap(image, width, height))
                        {
                            bitmap.Save(fileName, image.RawFormat);
                        }
                    }
                    else
                    {
                        using (Bitmap bitmap = new Bitmap(image, image.Width, image.Height))
                        {
                            bitmap.Save(fileName, image.RawFormat);
                        }
                        //nuImageUrl.MoveTo(fileName, MoveToOptions.Overwrite);
                        //fuImageUrl.PostedFile.SaveAs(fileName);
                    }
                    fileName = guid + fileExtension;
                    if (!article.ImageUrl.Equals(string.Empty))
                    {
                        DeleteImageFromServer();
                    }
                    flag = true;
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                lblImageUrlError.Visible = true;
                lblImageUrlError.Text = e.Message;//"Error when upload image";
            }
            return flag;

        }

        private void DeleteImageFromServer()
        {
            string thumbnailImageURL = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + article.ImageUrl;
            thumbnailImageURL = thumbnailImageURL.Replace("/", "\\");
            string imageURL = thumbnailImageURL.Substring(0, thumbnailImageURL.LastIndexOf(".")) + "_t" + thumbnailImageURL.Substring(thumbnailImageURL.LastIndexOf("."));
            if (File.Exists(imageURL))
            {
                File.Delete(imageURL);
            }
            if (File.Exists(thumbnailImageURL))
            {
                File.Delete(thumbnailImageURL);
            }
        }

        private bool SaveAudioUrl(out string fileAudioName)
        {
            String pathToApplicationsFolder
                = HttpContext.Current.Server.MapPath(
                "~/" + ConfigurationManager.AppSettings["ArticleAudiosFolder"]);
            if (!Directory.Exists(pathToApplicationsFolder))
            {
                Directory.CreateDirectory(pathToApplicationsFolder);
            }
            bool flag = false;
            fileAudioName = string.Empty;
            try
            {
                //Check valid file upload
                if (nuAudioUrl.HasFile && nuAudioUrl.ContentLength > 0)
                {

                    string fileExtension = Path.GetExtension(nuAudioUrl.FileName);
                    Double fileSize = nuImageUrl.ContentLength / 1024;

                    //Kiem tra ten mo rong file upload
                    if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedAudioFileExtensions"))
                    {
                        lblAudioUrlError.Text = ArticleResources.AudioUrlErrorFileExtension;
                        return false;
                    }
                    //Kiem tra kich thuoc file upload
                    if (!SiteUtils.IsValidFileSize(fileSize, "AllowedAudioSize"))
                    {
                        lblAudioUrlError.Text = ArticleResources.AudioUrlErrorFileSize + ConfigurationManager.AppSettings["AllowedAudioSize"] + @" KB";
                        return false;
                    }
                    string path = Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleAudiosFolder"]);
                    string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();

                    fileAudioName = guid + fileExtension;
                    nuAudioUrl.MoveTo(path + fileAudioName, MoveToOptions.Overwrite);
                    if (!article.AudioUrl.Equals(string.Empty))
                    {
                        DeleteAudioFromServer();
                    }
                    flag = true;
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                lblAudioUrlError.Visible = true;
                lblAudioUrlError.Text = e.Message;//"Error when upload image";
            }
            return flag;

        }

        private void DeleteAudioFromServer()
        {
            string audioURL = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleAudiosFolder"] + article.AudioUrl;
            audioURL = audioURL.Replace("/", "\\");
            if (File.Exists(audioURL))
            {
                File.Delete(audioURL);
            }
        }

        void blog_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            IndexBuilderProvider indexBuilder = IndexBuilderManager.Providers["ArticleIndexBuilderProvider"];
            if (indexBuilder != null)
            {
                indexBuilder.ContentChangedHandler(sender, e);
            }
        }

        #region Meta Data

        private void BindMeta()
        {
            if (article == null) { return; }
            if (article.ArticleGuid == Guid.Empty) { return; }

            List<ContentMeta> meta = metaRepository.FetchByContent(article.ArticleGuid);
            grdContentMeta.DataSource = meta;
            grdContentMeta.DataBind();

            btnAddMeta.Visible = false;
        }

        void grdContentMeta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (article == null) { return; }
            if (article.ArticleGuid == Guid.Empty) { return; }

            string sGuid = e.CommandArgument.ToString();
            if (sGuid.Length != 36) { return; }

            Guid guid = new Guid(sGuid);
            ContentMeta meta = metaRepository.Fetch(guid);
            if (meta == null) { return; }

            switch (e.CommandName)
            {
                case "MoveUp":
                    meta.SortRank -= 3;
                    break;

                case "MoveDown":
                    meta.SortRank += 3;
                    break;
            }

            metaRepository.Save(meta);
            List<ContentMeta> metaList = metaRepository.FetchByContent(article.ArticleGuid);
            metaRepository.ResortMeta(metaList);

            article.CompiledMeta = metaRepository.GetMetaString(article.ArticleGuid);
            article.Save();

            BindMeta();
            upMeta.Update();


        }

        void grdContentMeta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (article == null) { return; }
            if (article.ArticleGuid == Guid.Empty) { return; }

            GridView grid = (GridView)sender;
            // ReSharper disable PossibleNullReferenceException
            Guid guid = new Guid(grid.DataKeys[e.RowIndex].Value.ToString());
            // ReSharper restore PossibleNullReferenceException
            metaRepository.Delete(guid);

            article.CompiledMeta = metaRepository.GetMetaString(article.ArticleGuid);
            article.Save();
            grdContentMeta.Columns[2].Visible = true;
            BindMeta();
            upMeta.Update();
        }

        void grdContentMeta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView grid = (GridView)sender;
            grid.EditIndex = e.NewEditIndex;

            BindMeta();

            Button btnDeleteMeta = (Button)grid.Rows[e.NewEditIndex].Cells[1].FindControl("btnDeleteMeta");
            if (btnDeleteMeta != null)
            {
                btnDelete.Attributes.Add("OnClick", "return confirm('"
                    + ArticleResources.ContentMetaDeleteWarning + "');");
            }

            upMeta.Update();
        }

        void grdContentMeta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView grid = (GridView)sender;
            if (grid.EditIndex > -1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddDirection = (DropDownList)e.Row.Cells[1].FindControl("ddDirection");
                    if (ddDirection != null)
                    {
                        if (e.Row.DataItem is ContentMeta)
                        {
                            ListItem item = ddDirection.Items.FindByValue(((ContentMeta)e.Row.DataItem).Dir);
                            if (item != null)
                            {
                                ddDirection.ClearSelection();
                                item.Selected = true;
                            }
                        }
                    }

                    if (!(e.Row.DataItem is ContentMeta))
                    {
                        //the add button was clicked so hide the delete button
                        Button btnDeleteMeta = (Button)e.Row.Cells[1].FindControl("btnDeleteMeta");
                        if (btnDeleteMeta != null) { btnDeleteMeta.Visible = false; }
                    }
                }
            }
        }

        void grdContentMeta_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (article == null) { return; }
            if (article.ArticleGuid == Guid.Empty) { return; }
            GridView grid = (GridView)sender;

            // ReSharper disable PossibleNullReferenceException
            Guid guid = new Guid(grid.DataKeys[e.RowIndex].Value.ToString());
            // ReSharper restore PossibleNullReferenceException
            TextBox txtName = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtName");
            TextBox txtScheme = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtScheme");
            TextBox txtLangCode = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtLangCode");
            DropDownList ddDirection = (DropDownList)grid.Rows[e.RowIndex].Cells[1].FindControl("ddDirection");
            TextBox txtMetaContent = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtMetaContent");
            SiteUser currentSiteUser = SiteUtils.GetCurrentSiteUser();
            ContentMeta meta;
            if (guid != Guid.Empty)
            {
                meta = metaRepository.Fetch(guid);
            }
            else
            {
                meta = new ContentMeta();
                Module module = new Module(moduleId);
                meta.ModuleGuid = module.ModuleGuid;
                if (currentSiteUser != null) { meta.CreatedBy = currentSiteUser.UserGuid; }
                meta.SortRank = metaRepository.GetNextSortRank(article.ArticleGuid);
            }

            if (meta != null)
            {
                meta.SiteGuid = siteSettings.SiteGuid;
                meta.ContentGuid = article.ArticleGuid;
                meta.Dir = ddDirection.SelectedValue;
                meta.LangCode = txtLangCode.Text;
                meta.MetaContent = txtMetaContent.Text;
                meta.Name = txtName.Text;
                meta.Scheme = txtScheme.Text;
                if (currentSiteUser != null) { meta.LastModBy = currentSiteUser.UserGuid; }
                metaRepository.Save(meta);

                article.CompiledMeta = metaRepository.GetMetaString(article.ArticleGuid);
                article.Save();

            }

            grid.EditIndex = -1;
            grdContentMeta.Columns[2].Visible = true;
            BindMeta();
            upMeta.Update();

        }

        void grdContentMeta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdContentMeta.EditIndex = -1;
            grdContentMeta.Columns[2].Visible = true;
            BindMeta();
            upMeta.Update();
        }

        void btnAddMeta_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Guid", typeof(Guid));
            dataTable.Columns.Add("SiteGuid", typeof(Guid));
            dataTable.Columns.Add("ModuleGuid", typeof(Guid));
            dataTable.Columns.Add("ContentGuid", typeof(Guid));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Scheme", typeof(string));
            dataTable.Columns.Add("LangCode", typeof(string));
            dataTable.Columns.Add("Dir", typeof(string));
            dataTable.Columns.Add("MetaContent", typeof(string));
            dataTable.Columns.Add("SortRank", typeof(int));

            DataRow row = dataTable.NewRow();
            row["Guid"] = Guid.Empty;
            row["SiteGuid"] = siteSettings.SiteGuid;
            row["ModuleGuid"] = Guid.Empty;
            row["ContentGuid"] = Guid.Empty;
            row["Name"] = string.Empty;
            row["Scheme"] = string.Empty;
            row["LangCode"] = string.Empty;
            row["Dir"] = string.Empty;
            row["MetaContent"] = string.Empty;
            row["SortRank"] = 3;

            dataTable.Rows.Add(row);

            grdContentMeta.EditIndex = 0;
            grdContentMeta.DataSource = dataTable.DefaultView;
            grdContentMeta.DataBind();
            grdContentMeta.Columns[2].Visible = false;
            btnAddMeta.Visible = false;

            upMeta.Update();

        }

        private void BindMetaLinks()
        {
            if (article == null) { return; }
            if (article.ArticleGuid == Guid.Empty) { return; }

            List<ContentMetaLink> meta = metaRepository.FetchLinksByContent(article.ArticleGuid);

            grdMetaLinks.DataSource = meta;
            grdMetaLinks.DataBind();

            btnAddMetaLink.Visible = false;
        }

        void btnAddMetaLink_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Guid", typeof(Guid));
            dataTable.Columns.Add("SiteGuid", typeof(Guid));
            dataTable.Columns.Add("ModuleGuid", typeof(Guid));
            dataTable.Columns.Add("ContentGuid", typeof(Guid));
            dataTable.Columns.Add("Rel", typeof(string));
            dataTable.Columns.Add("Href", typeof(string));
            dataTable.Columns.Add("HrefLang", typeof(string));
            dataTable.Columns.Add("SortRank", typeof(int));

            DataRow row = dataTable.NewRow();
            row["Guid"] = Guid.Empty;
            row["SiteGuid"] = siteSettings.SiteGuid;
            row["ModuleGuid"] = Guid.Empty;
            row["ContentGuid"] = Guid.Empty;
            row["Rel"] = string.Empty;
            row["Href"] = string.Empty;
            row["HrefLang"] = string.Empty;
            row["SortRank"] = 3;

            dataTable.Rows.Add(row);

            grdMetaLinks.Columns[2].Visible = false;
            grdMetaLinks.EditIndex = 0;
            grdMetaLinks.DataSource = dataTable.DefaultView;
            grdMetaLinks.DataBind();
            btnAddMetaLink.Visible = false;

            updMetaLinks.Update();
        }

        void grdMetaLinks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView grid = (GridView)sender;
            if (grid.EditIndex > -1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (!(e.Row.DataItem is ContentMetaLink))
                    {
                        //the add button was clicked so hide the delete button
                        Button btnDeleteMetaLink = (Button)e.Row.Cells[1].FindControl("btnDeleteMetaLink");
                        if (btnDeleteMetaLink != null) { btnDeleteMetaLink.Visible = false; }

                    }

                }

            }
        }

        void grdMetaLinks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (article == null) { return; }
            if (article.ArticleGuid == Guid.Empty) { return; }

            GridView grid = (GridView)sender;
            // ReSharper disable PossibleNullReferenceException
            Guid guid = new Guid(grid.DataKeys[e.RowIndex].Value.ToString());
            // ReSharper restore PossibleNullReferenceException
            metaRepository.DeleteLink(guid);

            article.CompiledMeta = metaRepository.GetMetaString(article.ArticleGuid);
            article.Save();

            grid.Columns[2].Visible = true;
            BindMetaLinks();

            updMetaLinks.Update();
        }

        void grdMetaLinks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdMetaLinks.EditIndex = -1;
            grdMetaLinks.Columns[2].Visible = true;
            BindMetaLinks();
            updMetaLinks.Update();
        }

        void grdMetaLinks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (article == null) { return; }
            if (article.ArticleGuid == Guid.Empty) { return; }

            GridView grid = (GridView)sender;

            // ReSharper disable PossibleNullReferenceException
            Guid guid = new Guid(grid.DataKeys[e.RowIndex].Value.ToString());
            // ReSharper restore PossibleNullReferenceException
            TextBox txtRel = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtRel");
            TextBox txtHref = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtHref");
            TextBox txtHrefLang = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtHrefLang");
            SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
            ContentMetaLink meta;
            if (guid != Guid.Empty)
            {
                meta = metaRepository.FetchLink(guid);
            }
            else
            {
                meta = new ContentMetaLink();
                Module module = new Module(moduleId);
                meta.ModuleGuid = module.ModuleGuid;
                if (currentUser != null) { meta.CreatedBy = currentUser.UserGuid; }
                meta.SortRank = metaRepository.GetNextLinkSortRank(article.ArticleGuid);
            }

            if (meta != null)
            {
                meta.SiteGuid = siteSettings.SiteGuid;
                meta.ContentGuid = article.ArticleGuid;
                meta.Rel = txtRel.Text;
                meta.Href = txtHref.Text;
                meta.HrefLang = txtHrefLang.Text;

                if (currentUser != null) { meta.LastModBy = currentUser.UserGuid; }
                metaRepository.Save(meta);

                article.CompiledMeta = metaRepository.GetMetaString(article.ArticleGuid);
                article.Save();

            }

            grid.EditIndex = -1;
            grdMetaLinks.Columns[2].Visible = true;
            BindMetaLinks();
            updMetaLinks.Update();
        }

        void grdMetaLinks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView grid = (GridView)sender;
            grid.EditIndex = e.NewEditIndex;

            BindMetaLinks();

            // ReSharper disable PossibleNullReferenceException
            Guid guid = new Guid(grid.DataKeys[grid.EditIndex].Value.ToString());
            // ReSharper restore PossibleNullReferenceException

            Button btnDeleteMetaLink = (Button)grid.Rows[e.NewEditIndex].Cells[1].FindControl("btnDeleteMetaLink");
            if (btnDeleteMetaLink != null)
            {
                btnDeleteMetaLink.Attributes.Add("OnClick", "return confirm('"
                    + ArticleResources.ContentMetaLinkDeleteWarning + "');");

                if (guid == Guid.Empty) { btnDeleteMetaLink.Visible = false; }
            }

            updMetaLinks.Update();
        }

        void grdMetaLinks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (article == null) { return; }
            if (article.ArticleGuid == Guid.Empty) { return; }

            string sGuid = e.CommandArgument.ToString();
            if (sGuid.Length != 36) { return; }

            Guid guid = new Guid(sGuid);
            ContentMetaLink meta = metaRepository.FetchLink(guid);
            if (meta == null) { return; }

            switch (e.CommandName)
            {
                case "MoveUp":
                    meta.SortRank -= 3;
                    break;

                case "MoveDown":
                    meta.SortRank += 3;
                    break;

            }

            metaRepository.Save(meta);
            List<ContentMetaLink> metaList = metaRepository.FetchLinksByContent(article.ArticleGuid);
            metaRepository.ResortMeta(metaList);

            article.CompiledMeta = metaRepository.GetMetaString(article.ArticleGuid);
            article.Save();

            BindMetaLinks();
            updMetaLinks.Update();
        }


        #endregion

        #region History

        private void BindHistory()
        {
            if (!enableContentVersioning) { return; }

            if ((article == null) || (article.ItemID == -1))
            {
                pnlHistory.Visible = false;
                return;
            }

            List<ContentHistory> history = ContentHistory.GetPage(article.ArticleGuid, pageNumber, pageSize, out totalPages);

            pgrHistory.ShowFirstLast = true;
            pgrHistory.PageSize = pageSize;
            pgrHistory.PageCount = totalPages;
            pgrHistory.Visible = (totalPages > 1);

            grdHistory.DataSource = history;
            grdHistory.DataBind();

            btnDeleteHistory.Visible = (grdHistory.Rows.Count > 0);
            pnlHistory.Visible = (grdHistory.Rows.Count > 0);
            updHx.Update();
        }

        void pgrHistory_Command(object sender, CommandEventArgs e)
        {
            pageNumber = Convert.ToInt32(e.CommandArgument);
            pgrHistory.CurrentIndex = pageNumber;
            BindHistory();
        }

        void grdHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string g = e.CommandArgument.ToString();
            if (g.Length != 36) { return; }
            Guid historyGuid = new Guid(g);

            switch (e.CommandName)
            {
                case "RestoreToEditor":
                    ContentHistory history = new ContentHistory(historyGuid);
                    if (history.Guid == Guid.Empty) { return; }

                    edContent.Text = history.ContentText;
                    BindHistory();
                    break;

                case "DeleteHistory":
                    ContentHistory.Delete(historyGuid);
                    BindHistory();
                    break;

                default:

                    break;
            }
        }

        void grdHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Button button = (Button)e.Row.Cells[0].FindControl("btnDelete");
            if (button != null)
            {
                button.Attributes.Add("OnClick", "return confirm('"
                    + ArticleResources.DeleteHistoryItemWarning + "');");
            }
        }

        void btnRestoreFromGreyBox_Click(object sender, ImageClickEventArgs e)
        {
            if (hdnHxToRestore.Value.Length != 36)
            {
                BindHistory();
                return;
            }

            Guid h = new Guid(hdnHxToRestore.Value);

            ContentHistory history = new ContentHistory(h);
            if (history.Guid == Guid.Empty) { return; }

            edContent.Text = history.ContentText;
            BindHistory();

        }

        void btnDeleteHistory_Click(object sender, EventArgs e)
        {
            if (article == null) { return; }

            ContentHistory.DeleteByContent(article.ArticleGuid);
            BindHistory();

        }

        #endregion

        private void DoPings(object pingersList)
        {

            if (!(pingersList is List<ServicePinger>)) return;

            List<ServicePinger> pingers = pingersList as List<ServicePinger>;
            foreach (ServicePinger pinger in pingers)
            {
                pinger.Ping();
            }

        }

        protected virtual void QueuePings()
        {
            if (config.OdiogoFeedId.Length == 0) return;

            const string odogioRpcUrl = "http://rpc.odiogo.com/ping/";
            ServicePinger pinger = new ServicePinger(
                siteSettings.SiteName,
                SiteRoot,
                odogioRpcUrl);

            List<ServicePinger> pingers = new List<ServicePinger> { pinger };


            if (!ThreadPool.QueueUserWorkItem(DoPings, pingers))
            {
                throw new Exception("Couldn't queue the DoPings on a new thread.");
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (hdnReturnUrl.Value.Length > 0)
            {
                WebUtils.SetupRedirect(this, hdnReturnUrl.Value);
                return;
            }

            WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());

            return;

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (article != null)
            {

                if (config.IsDeleteSetting)
                {
                    article.IsDelete = true;
                    article.Save();
                }
                else
                {
                    if (!article.ImageUrl.Equals(string.Empty))
                    {
                        DeleteImageFromServer();
                    }
                    FriendlyUrl.DeleteByPageGuid(article.ArticleGuid, siteSettings.SiteId, article.ItemUrl);
                    Article.Delete(article.ItemID);
                }
                //if (articleSync != null)
                //{
                //    articleSync.ContentChanged += blog_ContentChanged;
                //    articleSync.Delete();
                //    FriendlyUrl.DeleteByPageGuid(articleSync.ArticleGuid);
                //}
                CurrentPage.UpdateLastModifiedTime();
                SiteUtils.QueueIndexing();
            }

            //if (hdnReturnUrl.Value.Length > 0)
            //{
            //    WebUtils.SetupRedirect(this, hdnReturnUrl.Value);
            //    return;
            //}

            WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());

            return;
        }

        private void PopulateLabels()
        {


            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.EditPostPageTitle);
            moduleTitle.EditText = ArticleResources.BlogEditEntryLabel;
            progressBar.AddTrigger(btnUpload);
            progressBar.AddTrigger(btnUpdate);

            litContentTab.Text = @"<a href='#tabContent'>" + ArticleResources.ContentTab + @"</a>";

            litMetaTab.Text = @"<a href='#tabMeta'>" + ArticleResources.MetaTab + @"</a>";

            if (itemId > 0)
            {
                heading.Text = "Cập nhật tin bài";
            }
            else
            {
                heading.Text = "Thêm mới tin bài";
            }

            //dpBeginDate2.Enabled = false;
            //dpEndDate2.Enabled = false;
            if (isUserPost && !isApprover)
            {
                if (article != null && !article.IsApproved.HasValue)
                {
                    rdoIsApproved.SelectedIndex = 0;
                    pnlIsApproved.Visible = false;
                    pnlIsPublished.Visible = false;
                }
                else if (article != null && article.IsApproved.HasValue)
                {
                    pnlIsApproved.Visible = true;
                    rdoIsApproved.Enabled = false;
                    txtCommentByBoss.Enabled = false;
                    if (article.IsApproved.Value)
                    {
                        pnlIsPublished.Visible = true;
                    }
                    else
                    {
                        pnlIsPublished.Visible = false;
                    }
                }
                else
                {
                    pnlIsApproved.Visible = false;
                    pnlIsPublished.Visible = false;
                }
            }
            //if (UserCanEditModule(moduleId) || isApprover)
            if (isApprover)
            {
                //chkIsApproved.Checked = true;
                rdoIsApproved.SelectedIndex = 1;//true
                pnlIsApproved.Visible = true;
                pnlIsPublished.Visible = true;
            }
            btnDeleteAudio.ImageUrl = "~/Data/SiteImages/delete.gif";
            btnDeleteAudio.ToolTip = ArticleResources.ArticleDeleteLinkText;

            btnDeleteImg.ImageUrl = "~/Data/SiteImages/delete.gif";
            btnDeleteImg.ToolTip = ArticleResources.ArticleDeleteLinkText;

            //edContent.WebEditor.ToolBar = ToolBar.Newsletter;
            edContent.WebEditor.Height = config.EditorHeight;
            edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;

            btnUpdate.Text = ArticleResources.BlogEditUpdateButton;
            SiteUtils.SetButtonAccessKey(btnUpdate, ArticleResources.BlogEditUpdateButtonAccessKey);
            btnUpdate3.Text = ArticleResources.BlogEditUpdateButton;
            btnSaveAndPreview.Text = ArticleResources.SaveAndPreviewButton;
            btnAddMeta.Text = ArticleResources.AddMetaButton;
            grdContentMeta.Columns[0].HeaderText = string.Empty;
            grdContentMeta.Columns[1].HeaderText = ArticleResources.ContentMetaNameLabel;
            grdContentMeta.Columns[2].HeaderText = ArticleResources.ContentMetaMetaContentLabel;

            btnAddMetaLink.Text = ArticleResources.AddMetaLinkButton;

            grdMetaLinks.Columns[0].HeaderText = string.Empty;
            grdMetaLinks.Columns[1].HeaderText = ArticleResources.ContentMetaRelLabel;
            grdMetaLinks.Columns[2].HeaderText = ArticleResources.ContentMetaMetaHrefLabel;
            UIHelper.DisableButtonAfterClick(
                btnUpdate,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnUpdate, string.Empty)
                );


            UIHelper.DisableButtonAfterClick(
                btnUpdate3,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnUpdate3, string.Empty)
                );

            UIHelper.DisableButtonAfterClick(
                btnSaveAndPreview,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnSaveAndPreview, string.Empty)
                );

            lnkCancel.Text = ArticleResources.BlogEditCancelButton;
            lnkCancel3.Text = ArticleResources.BlogEditCancelButton;
            btnDelete.Text = ArticleResources.BlogEditDeleteButton;
            btnDelete3.Text = ArticleResources.BlogEditDeleteButton;
            SiteUtils.SetButtonAccessKey(btnDelete, ArticleResources.BlogEditDeleteButtonAccessKey);
            UIHelper.AddConfirmationDialog(btnDelete, ArticleResources.ArticleDeletePostWarning);
            UIHelper.AddConfirmationDialog(btnDelete3, ArticleResources.ArticleDeletePostWarning);

            rfvCategory.ErrorMessage = ArticleResources.CategoryRequired;
            rfvTitle.ErrorMessage = ArticleResources.TitleRequiredWarning;
            reqStartDate.ErrorMessage = ArticleResources.BlogBeginDateRequiredHelp;
            dpBeginDate2.ClockHours = ConfigurationManager.AppSettings["ClockHours"];
            dpEndDate2.ClockHours = ConfigurationManager.AppSettings["ClockHours"];
            regexUrl.ErrorMessage = ArticleResources.FriendlyUrlRegexWarning;

            grdHistory.Columns[0].HeaderText = ArticleResources.CreatedDateGridHeader;
            grdHistory.Columns[1].HeaderText = ArticleResources.ArchiveDateGridHeader;

            btnRestoreFromGreyBox.ImageUrl = Page.ResolveUrl("~/Data/SiteImages/1x1.gif");
            btnRestoreFromGreyBox.AlternateText = @" ";

            btnDeleteHistory.Text = ArticleResources.DeleteAllHistoryButton;
            UIHelper.AddConfirmationDialog(btnDeleteHistory, ArticleResources.DeleteAllHistoryWarning);

            lblImageUrlError.Text = ArticleResources.ImageUrlErrorLabel + ConfigurationManager.AppSettings["AllowedImageFileExtensions"]
                + @" -- " + ArticleResources.ImageUrlErrorLabelFileSize + ConfigurationManager.AppSettings["AllowedImageSize"] + @" KB";

            lblAudioUrlError.Text = ArticleResources.ImageUrlErrorLabel + ConfigurationManager.AppSettings["AllowedAudioFileExtensions"]
    + @" -- " + ArticleResources.ImageUrlErrorLabelFileSize + ConfigurationManager.AppSettings["AllowedAudioSize"] + @" KB";

        }

        private void LoadSettings()
        {

            if ((WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; }

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(getModuleSettings);

            if (WebUser.IsInRoles(config.RolePost) || WebUser.IsInRoles(config.RoleApproved))
            {
                isUserPost = true;
            }
            if (WebUser.IsInRoles(config.RoleApproved))
            {
                isApprover = true;
            }
            if (WebUser.IsInRoles(config.RoleApproved))
            {
                RoleAccess = true;
            }

            lnkCancel.NavigateUrl = SiteUtils.GetCurrentPageUrl();

            enableContentVersioning = config.EnableContentVersioning;

            if ((siteSettings.ForceContentVersioning) || (WebConfigSettings.EnforceContentVersioningGlobally))
            {
                enableContentVersioning = true;
            }

            if (itemId > -1)
            {
                article = new Article(itemId);
                if (article.ModuleID != moduleId) { article = null; }
            }

            pnlAttachment.Visible = config.UseAttachmentSetting;
            btnUpload.Visible = config.UseAttachmentSetting && itemId > 0;

            divHistoryDelete.Visible = (enableContentVersioning && isAdmin);

            pnlHistory.Visible = enableContentVersioning;

            if (enableContentVersioning)
            {
                SetupHistoryRestoreScript();
            }

            try
            {
                // this keeps the action from changing during ajax postback in folder based sites
                SiteUtils.SetFormAction(Page, Request.RawUrl);
            }
            catch (MissingMethodException)
            {
                //this method was introduced in .NET 3.5 SP1
            }
        }
        //[WebMethod]
        //[HttpPost]
        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        public static string GetArticleReference()
        {
            return new JavaScriptSerializer().Serialize(ArticleReferenceBO.GetList(1, -1));
        }

        private void SetupHistoryRestoreScript()
        {
            StringBuilder script = new StringBuilder();

            script.Append("\n<script type='text/javascript'>");
            script.Append("function LoadHistoryInEditor(hxGuid) {");

            script.Append("GB_hide();");
            //script.Append("alert(hxGuid);");

            script.Append("var hdn = document.getElementById('" + hdnHxToRestore.ClientID + "'); ");
            script.Append("hdn.value = hxGuid; ");
            script.Append("var btn = document.getElementById('" + btnRestoreFromGreyBox.ClientID + "');  ");
            script.Append("btn.click(); ");
            script.Append("}");
            script.Append("</script>");


            Page.ClientScript.RegisterStartupScript(typeof(Page), "gbHandler", script.ToString());

        }
        /// <summary>
        /// Return text trạng thái của log bài viết
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string StatePublic(string state = "")
        {
            var result = "<span class='iFail'><i class='fa fa-ban' aria-hidden='true'></i></span>";
            if (string.IsNullOrEmpty(state))
            {
                result = "<span class='iSuccess'><i class='fa check' aria-hidden='true'></i><span>";
            }
            else
            {
                if (bool.Parse(state))
                {
                    result = "<span class='iSuccess'><i class='fa fa-check' aria-hidden='true'></i></span>";
                }
                else
                {
                    result = "<span class='iFail'><i class='fa fa-ban' aria-hidden='true'></i></span>";
                }
            }

            return result;
        }
        /// <summary>
        /// Bind log tin bài
        /// </summary>
        private void BindArticleLog()
        {
            pnlArticleLog.Visible = false;
            if (itemId > 0)
            {
                rptLogArticle.DataSource = ArticleLog.GetListByArticle(itemId);
                rptLogArticle.DataBind();
                pnlArticleLog.Visible = true;
            }
        }

        private void LoadParams()
        {
            timeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            siteId = siteSettings.SiteId;
            siteid = siteSettings.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("ItemID", -1);
            catId = WebUtils.ParseInt32FromQueryString("catid", -1);
            restoreGuid = WebUtils.ParseGuidFromQueryString("r", restoreGuid);
            cacheDependencyKey = "Module-" + moduleId.ToInvariantString();
            virtualRoot = WebUtils.GetApplicationRoot();
            dateTimeFormat = config.DateTimeFormat.ToString();

            var siteuser = SiteUtils.GetCurrentSiteUser();
            if (siteuser != null)
            {
                userid = siteuser.UserId;
            }
        }

        private void LoadPanels()
        {
            bool showLeftColumnAdmin = ConfigHelper.GetBoolProperty("ShowLeftColumnAdmin", false);
            bool showRightColumnAdmin = ConfigHelper.GetBoolProperty("ShowRightColumnAdmin", false);
            bool showTopColumnAdmin = ConfigHelper.GetBoolProperty("ShowTopColumnAdmin", false);
            bool showBottomColumnAdmin = ConfigHelper.GetBoolProperty("ShowBottomColumnAdmin", false);
            int showCenterColumnAdmin = ConfigHelper.GetIntProperty("ShowCenterColumnAdmin", 0);
            LoadSideContent(showLeftColumnAdmin, showRightColumnAdmin/*, showTopColumnAdmin, showBottomColumnAdmin, showCenterColumnAdmin*/);
        }

        private void SetupScripts()
        {
            //if (!Page.ClientScript.IsClientScriptBlockRegistered("friendlyurlsuggest"))
            //{
            //    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "friendlyurlsuggest", "<script src=\""
            //        + ResolveUrl(WebConfigSettings.FriendlyUrlSuggestScript) + "\" type=\"text/javascript\"></script>");
            //}

            //string focusScript = string.Empty;
            //if (itemId == -1) { focusScript = "document.getElementById('" + this.txtTitle.ClientID + "').focus();"; }

            //string hookupInputScript = "<script type=\"text/javascript\">"
            //    + "new UrlHelper( "
            //    + "document.getElementById('" + txtTitle.ClientID + "'),  "
            //    + "document.getElementById('" + txtItemUrl.ClientID + "'), "
            //    + "document.getElementById('" + hdnTitle.ClientID + "'), "
            //    + "document.getElementById('" + spnUrlWarning.ClientID + "'), "
            //    + "\"" + SiteRoot + "/Blog/BlogUrlSuggestService.ashx" + "\""
            //    + "); " + focusScript + "</script>";

            //if (!Page.ClientScript.IsStartupScriptRegistered(this.UniqueID + "urlscript"))
            //{
            //    Page.ClientScript.RegisterStartupScript(
            //        this.GetType(),
            //        this.UniqueID + "urlscript", hookupInputScript);
            //}

            if (!Page.ClientScript.IsClientScriptBlockRegistered("sarissa"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "sarissa", "<script src=\""
                    + ResolveUrl("~/ClientScript/sarissa/sarissa.js") + "\" type=\"text/javascript\"></script>");
            }

            if (!Page.ClientScript.IsClientScriptBlockRegistered("sarissa_ieemu_xpath"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "sarissa_ieemu_xpath", "<script src=\""
                    + ResolveUrl("~/ClientScript/sarissa/sarissa_ieemu_xpath.js") + "\" type=\"text/javascript\"></script>");
            }

            if (!Page.ClientScript.IsClientScriptBlockRegistered("friendlyurlsuggest"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "friendlyurlsuggest", "<script src=\""
                    + ResolveUrl("~/ClientScript/friendlyurlsuggest_v2.js") + "\" type=\"text/javascript\"></script>");
            }

            string focusScript = string.Empty;
            if (itemId == -1) { focusScript = "document.getElementById('" + txtTitle.ClientID + "').focus();"; }

            string hookupInputScript = "<script type=\"text/javascript\">"
                + "new UrlHelper( "
                + "document.getElementById('" + txtTitle.ClientID + "'),  "
                + "document.getElementById('" + txtItemUrl.ClientID + "'), "
                + "document.getElementById('" + hdnTitle.ClientID + "'), "
                + "document.getElementById('" + spnUrlWarning.ClientID + "'), "
                + "\"" + SiteRoot + "/UIUtils/BlogUrlSuggestService.ashx" + "\""
                + "); " + focusScript + "</script>";

            if (!Page.ClientScript.IsStartupScriptRegistered(UniqueID + "urlscript"))
            {
                Page.ClientScript.RegisterStartupScript(
                    GetType(),
                    UniqueID + "urlscript", hookupInputScript);
            }
        }

        protected void chkIsApproved_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbIsApproved = (CheckBox)sender;
            if (cbIsApproved.Checked == true)
            {
                dpBeginDate2.Enabled = true;
                dpEndDate2.Enabled = true;
            }
            else
            {
                dpBeginDate2.Enabled = false;
                dpEndDate2.Enabled = false;
            }
        }

        protected void rdoIsApproved_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rdoIsApproved = (RadioButtonList)sender;
            if (rdoIsApproved.SelectedValue == "1")
            {
                dpBeginDate2.Enabled = true;
                dpEndDate2.Enabled = true;
            }
            else
            {
                dpBeginDate2.Enabled = false;
                dpEndDate2.Enabled = false;
            }
        }

    }
}
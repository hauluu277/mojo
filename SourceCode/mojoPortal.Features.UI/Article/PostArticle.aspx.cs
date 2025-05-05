using ArticleFeature.Business;
using DictionaryFeature.Business;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Net;
using mojoPortal.SearchIndex;
using mojoPortal.Web;
using mojoPortal.Web.Editor;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
    public partial class PostArticle : mojoBasePage
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
        protected List<int> ListCategory = new List<int>();
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

            btnUpdate.Click += btnUpdate_Click;
            btnUpdate3.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnDelete3.Click += btnDelete_Click;

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

            ScriptManager.RegisterStartupScript(
              uptag,
              this.GetType(),
              "ReloadTag",
              "ReloadTag();",
              true);
        }

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

            if (!CommonArticle.AccessPublisheedArticle && !CommonArticle.AccessApprovedArticle && !CommonArticle.AccessManageArticle)
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
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

            if (!Page.IsPostBack)
            {
                if (CommonArticle.AccessManageArticle)
                {
                    hdfCategoryAccess.Value = "all";
                }
                else
                {
                    hdfCategoryAccess.Value = string.Join(",", ListCategory.ToArray());
                }
                if (itemId > 0)
                {
                    var listFile = FileAttachments.GetByObject(itemId);
                    rptAttachments.DataSource = listFile;
                    rptAttachments.DataBind();
                    //var listAttachFile = listFile.Select(x => new AtachmentFile { fileName = x.FileName, filePath = x.FilePath }).ToList();
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //hdfFileAtachment.Value = js.Serialize(listAttachFile);
                }

                PopulateLabels();
                PopulateCategories();
                PopulateCategoriesReference();
                //PopulatePolls();
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
            //hdfReference.Value = new JavaScriptSerializer().Serialize(ArticleReferenceBO.GetList(siteSettings.SiteId, -1));
            //ddlChuyenMuc.Text = LoadCategoryHtml();
            if (article != null)
            {

                //loader article reference
                if (!string.IsNullOrEmpty(article.ArticleReference))
                {
                    //lấy danh sách tin tức liên quan từ elasticSearh

                    rptArticleReference.DataSource = Article.SelectByReference(article.ArticleReference);
                    rptArticleReference.DataBind();
                }



                if (!string.IsNullOrEmpty(article.ImageUrl))
                {
                    if (article.ImageUrl.Contains("/"))
                    {
                        imgJcrop.ImageUrl = article.ImageUrl;
                    }
                    else
                    {
                        imgJcrop.ImageUrl = ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], article.ImageUrl);
                    }
                }
                ImageName.Value = article.ImageUrl;

                //string selectedValue = article.CategoryID.ToString();
                //if (!selectedValue.Equals(string.Empty))
                //{
                //    ListItem item = ddlCategories.Items.FindByValue(selectedValue);
                //    if (item != null)
                //    {
                //        ddlCategories.Items.FindByValue(selectedValue).Selected = true;
                //    }
                //}
                //else { ddlCategories.SelectedIndex = 0; }

                //dpBeginDate2.ShowTime = true;
                //dpBeginDate2.Text = timeZone != null ? article.StartDate.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.StartDate.ToString(dateTimeFormat));
                //if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                //{
                //    dpBeginDate2.Text = string.Format("{0:MM/dd/yyyy HH:mm}", article.StartDate);
                //    if (article.EndDate.HasValue)
                //    {
                //        dpEndDate2.Text = string.Format("{0:MM/dd/yyyy HH:mm}", article.EndDate);
                //    }
                //    dpAddDate2.Text = string.Format("{0:MM/dd/yyyy HH:mm}", article.CreatedDate);


                //    if (article.MetaDate.HasValue)
                //    {
                //        //calenderMetaDate.Text = timeZone != null ? article.MetaDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.MetaDate.Value.ToString(dateTimeFormat));
                //        calenderMetaDate.Text = string.Format("{0:MM/dd/yyyy HH:mm}", article.MetaDate);
                //    }
                //}
                //else
                //{
                dpBeginDate2.Text = string.Format("{0:dd/MM/yyyy HH:mm}", article.StartDate);
                if (article.EndDate.HasValue)
                {
                    //dpEndDate2.Text = timeZone != null ? article.EndDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.EndDate.Value.ToString(dateTimeFormat));
                    dpEndDate2.Text = string.Format("{0:dd/MM/yyyy HH:mm}", article.EndDate);
                }
                //dpAddDate2.Text = timeZone != null ? article.CreatedDate.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.CreatedDate.ToString(dateTimeFormat));
                dpAddDate2.Text = string.Format("{0:dd/MM/yyyy HH:mm}", article.CreatedDate);


                if (article.MetaDate.HasValue)
                {
                    //calenderMetaDate.Text = timeZone != null ? article.MetaDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.MetaDate.Value.ToString(dateTimeFormat));
                    calenderMetaDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}", article.MetaDate);
                }
                //}
                ArticleReferenceAtive.Value = article.ArticleReference;

                txtTitle.Value = article.Title;
                txtSummary.Text = article.Summary;
                txtItemUrl.Value = article.ItemUrl;
                //ckHotNew.Checked = article.IsHotNew;
                Literal literContent = new Literal();
                literContent.Text = System.Web.HttpUtility.HtmlDecode(article.Description);
                edContent.Text = literContent.Text;
                //txtTag.Text = article.Tag;
                txtMetaDescription.Text = article.MetaDescription;
                if (!string.IsNullOrEmpty(article.CreatedByUser))
                {
                    txtAuthor.Text = article.CreatedByUser;
                }


                //chkIsApproved.Checked = article.IsApproved;
                if (article.IsApproved.HasValue)
                {
                    rdoIsApproved.SelectedIndex = article.IsApproved.Value ? 1 : 0;
                }

                ViTriHienThiNgayDang.SelectedValue = string.IsNullOrEmpty(article.ViTriHienThiNgayDang) || article.ViTriHienThiNgayDang == "left" ? "left" : "right";
                IsHienThiTacGia.Checked = article.IsHienThiTacGia ? true : false;
                if (!string.IsNullOrEmpty(article.CreatedByUser))
                {
                    txtAuthor.Text = article.CreatedByUser;
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

                if (restoreGuid != Guid.Empty)
                {
                    ContentHistory rHistory = new ContentHistory(restoreGuid);
                    if (rHistory.ContentGuid == article.ArticleGuid)
                    {
                        Literal literContentV2 = new Literal();
                        literContent.Text = System.Web.HttpUtility.HtmlDecode(rHistory.ContentText);
                        edContent.Text = literContent.Text;
                    }
                }
                // show preview button for saved drafts
                if ((article.IsApproved.HasValue && !article.IsApproved.Value) || (article.StartDate > DateTime.UtcNow))
                {
                    btnSaveAndPreview.Visible = true;
                }

                string pollSelectedValue = article.PollGuid.ToString();
                //if (!pollSelectedValue.Equals(string.Empty))
                //{
                //    ListItem item = ddlPoll.Items.FindByValue(pollSelectedValue);
                //    if (item != null)
                //    {
                //        ddlPoll.Items.FindByValue(pollSelectedValue).Selected = true;
                //    }
                //}
                //else { ddlPoll.SelectedIndex = 0; }


                BindHistory();
                //BindAttachments();

                var listCategory = ArticleCategory.GetList(itemId).Select(x => x.CategoryID).ToList();
                if (listCategory != null && listCategory.Count > 0)
                {
                    foreach (ListItem category in lboxCategories.Items)
                    {
                        if (listCategory.Contains(int.Parse(category.Value)))
                        {
                            lboxCategories.Items.FindByValue(category.Value).Selected = true;
                            category.Selected = true;
                        }
                    }
                }

            }
            else
            {
                rdoIsApproved.SelectedIndex = 0;
                chkIsPublished.Checked = false;
                //dpAddDate2.Text = DateTime.UtcNow.ToLocalTime(timeZone).ToString(dateTimeFormat);
                //dpBeginDate2.Text = DateTime.UtcNow.ToLocalTime(timeZone).ToString(dateTimeFormat);// DateTimeHelper.LocalizeToCalendar(DateTime.UtcNow.ToString("g"));


                //if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                //{
                //    dpAddDate2.Text = string.Format("{0:MM/dd/yyyy}", DateTime.Now);
                //    dpBeginDate2.Text = string.Format("{0:MM/dd/yyyy}", DateTime.Now);
                //}
                //else
                //{
                dpAddDate2.Text = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                dpBeginDate2.Text = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                //}



                dpEndDate2.Text = string.Empty;
                btnDelete.Visible = false;
                btnDelete3.Visible = false;
                pnlHistory.Visible = false;
                if (Request.IsAuthenticated && siteUser != null)
                {
                    //txtAuthor.Text = siteUser.Name;
                    //txtMetaCreator.Text = siteUser.Name;
                    //txtMetaPublisher.Text =;
                }
                //set default category
                //if (ddlCategories.Items.Count == 2)
                //{
                //    ddlCategories.SelectedIndex = 1;
                //};
                ViTriHienThiNgayDang.SelectedValue = "left";
                IsHienThiTacGia.Checked = false;
            }

            if ((txtItemUrl.Value.Length == 0) && (txtTitle.Value.Length > 0))
            {
                string friendlyUrl = txtTitle.Value.UrlRewriteDefault();

                txtItemUrl.Value = @"~/" + friendlyUrl;
            }

            if (article != null)
            {
                hdnTitle.Value = txtTitle.Value;
            }
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

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static List<ListItem> GetCategoryChild(int id)
        {
            var listCategory = CoreCategory.GetChildren(id);
            List<ListItem> listItem = new List<ListItem>();
            if (listCategory != null && listCategory.Count > 0)
            {
                listItem = listCategory.Select(x => new ListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
            }

            for (int i = 0; i < listItem.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(listItem[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (listItem[i].Text.StartsWith("|"))
                {
                    prefix += listItem[i].Text.Substring(0, 3);
                    listItem[i].Text = listItem[i].Text.Remove(0, 3);
                }
                listItem[i].Text = prefix + listItem[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString(),
                    };
                    listItem.Insert(listItem.IndexOf(listItem[i]) + index, list);
                    index++;
                }
            }
            return listItem;
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
                    string downloadPath = Page.Server.MapPath(WebUtils.GetApplicationRoot() + "/Data/Sites/" + siteSettings.SiteId + "/media/ArticleAttachments/" + chuyenmuc.Name.ConvertToFTS().Replace(" ", "") + "/" + item.ServerFileName);
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
                if (!Page.IsValid)
                {
                    edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
                    edContent.WebEditor.Height = config.EditorHeight;
                    return;
                }
                Save();
            }
            if (article == null) { WebUtils.SetupRedirect(this, Request.RawUrl); return; }
            if ((article.ItemID <= 0) || (article.ModuleID != moduleId)) { WebUtils.SetupRedirect(this, Request.RawUrl); return; }

            CurrentPage.UpdateLastModifiedTime();
            CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
            var url = SiteRoot + "/article/postarticle.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
                        article.ItemID;
            WebUtils.SetupRedirect(this, url);

        }

        void btnDeleteImg_Click(object sender, ImageClickEventArgs e)
        {
            if (article == null) { article = new Article(itemId); }

            DeleteImageFromServer();
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
            article.AudioUrl = string.Empty;
            article.Save();
            if (articleSync == null) return;
            articleSync.AudioUrl = string.Empty;
            articleSync.Save();
        }

        [WebMethod]
        public static int SaveTag(string tag = "", string description = "")
        {
            Dictionary dictionary = new Dictionary();
            dictionary.Censorship = true;
            dictionary.Description = description;
            dictionary.DateCreate = DateTime.Now;
            dictionary.DateApprove = DateTime.Now;
            dictionary.IsPublic = true;
            dictionary.Name = tag;
            dictionary.SiteID = siteid;
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
                    if (!Page.IsValid)
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
                CurrentPage.UpdateLastModifiedTime();
                CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
                var url = SiteRoot + "/article/postarticle.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
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
        }

        void btnSaveAndPreview_Click(object sender, EventArgs e)
        {

            if (itemId <= 0)
            {
                if (itemId == -1 || article == null || article.ItemID <= 0)
                {
                    Page.Validate("article");
                    if (!Page.IsValid)
                    {
                        edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
                        edContent.WebEditor.Height = config.EditorHeight;
                        return;
                    }
                    Save();
                }
                if (article == null) { WebUtils.SetupRedirect(this, Request.RawUrl); return; }
                if ((article.ItemID <= 0) || (article.ModuleID != moduleId)) { WebUtils.SetupRedirect(this, Request.RawUrl); return; }
                CurrentPage.UpdateLastModifiedTime();
                CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
                var url = SiteRoot + "/article/postarticle.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
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
                //btnUpload_Click(null, null);
            }
            WebUtils.SetupRedirect(this, SiteRoot + article.ItemUrl.Replace("~/", "/"));
        }

        private void PopulateCategories()
        {
            if (CommonArticle.AccessManageArticle)
            {
                var listChild = CoreCategory.GetChildren(siteSettings.ArticleCategory);
                var listCategory = new List<ListItem>();
                foreach (var item in listChild)
                {
                    lboxCategories.Items.Add(new ListItem { Text = item.Name, Value = item.ItemID.ToString() });
                }
                PopulateChildNode(lboxCategories);
            }
            else
            {
                lboxCategories.DataValueField = "CategoryID";
                lboxCategories.DataTextField = "CategoryName";
                lboxCategories.DataSource = CategoryUserArticle.GetListItemByUserBO(siteUser.UserId);
                lboxCategories.DataBind();

            }

           
            //if (CommonArticle.AccessManageArticle)
            //{
            //    foreach (var item in listChild)
            //    {
            //        ddlCategories.Items.Add(new ListItem { Text = item.Name, Value = item.ItemID.ToString() });
            //    }
            //}
            //else
            //{
            //    var listCategoryID = new List<int>();
            //    if (ListCategory != null && ListCategory.Count > 0)
            //    {
            //        foreach (var item in ListCategory)
            //        {
            //            listCategoryID.Add(item);
            //            listCategoryID.AddRange(CoreCategory.GetListParent(item));
            //        }
            //    }
            //    foreach (var item in listChild)
            //    {
            //        if (listCategoryID.Contains(item.ItemID))
            //        {
            //            ddlCategories.Items.Add(new ListItem { Text = item.Name, Value = item.ItemID.ToString() });
            //        }
            //    }
            //}
            //var articleCategories = CoreCategory.GetChildren(siteSettings.ArticleCategory);
            //lboxCategories.Items.RemoveAt(0);
            //for (int i = 0; i < lboxCategories.Items.Count; i++)
            //{
            //    lboxCategories.Items[i].Text = lboxCategories.Items[i].Text.Replace("|--", string.Empty);
            //}
            //lboxCategories.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
            if (!string.IsNullOrEmpty(config.ArticleCategoryConfig))
            {
                var categoryList = config.ArticleCategoryConfig.Split('-');
                if (categoryList != null && categoryList.Count() > 0)
                {
                    lboxCategories.SelectedValue = categoryList[0];
                }

            }
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
                var lever = 0;
                while (root.Items[i].Text.StartsWith("|"))
                {
                    lever++;
                    prefix += root.Items[i].Text.Substring(0, 3);
                    root.Items[i].Text = root.Items[i].Text.Remove(0, 3);
                }
                root.Items[i].Text = prefix + root.Items[i].Text;
                root.Items[i].Attributes.Add("data-lever", lever.ToString());
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString(),

                    };
                    root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    root.Items[root.Items.IndexOf(root.Items[i]) + index].Attributes.Add("data-lever", (lever + 1).ToString());
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
            string pageName = txtTitle.Value;
            var getCategory = lboxCategories.SelectedValue;
            if (!string.IsNullOrEmpty(getCategory))
            {
                return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings, getCategory);
            }
            else
            {
                return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
            }
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
            article.Title = txtTitle.Value;
            article.Summary = txtSummary.Text;
            article.Description = System.Web.HttpUtility.HtmlDecode(edContent.Text);
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
            //DateTime localTime = DateTime.Parse(dpBeginDate2.Text, CultureInfo.CurrentCulture);
            //article.StartDate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
            if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
            {
                var formatDate = dpBeginDate2.Text.ToDateTimeEn();
                article.StartDate = DateTime.Parse(formatDate, CultureInfo.CurrentCulture);
            }
            else
            {
                article.StartDate = DateTime.Parse(dpBeginDate2.Text, CultureInfo.CurrentCulture);
            }
            if (!string.IsNullOrEmpty(dpEndDate2.Text))
            {
                //DateTime endTime = DateTime.Parse(dpEndDate2.Text, CultureInfo.CurrentCulture);
                //article.EndDate = timeZone != null ? endTime.ToUtc(timeZone) : endTime.AddHours(-timeOffset);
                if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                {
                    var formatDate = dpEndDate2.Text.ToDateTimeEn();
                    article.EndDate = DateTime.Parse(formatDate, CultureInfo.CurrentCulture);
                }
                else
                {
                    article.EndDate = DateTime.Parse(dpEndDate2.Text, CultureInfo.CurrentCulture);
                }
            }
            else
            {
                article.EndDate = null;
            }
            DateTime lastModified = DateTime.Now;
            article.LastModUtc = lastModified;
            if (isApprover)
            {
                if (!string.IsNullOrEmpty(rdoIsApproved.SelectedValue))
                {
                    article.IsApproved = rdoIsApproved.SelectedItem.Value == "0" ? false : true;
                }
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
            //if (isUserPost && !isApprover && article.IsApproved.HasValue && article.IsApproved.Value)
            //{
            //    article.IsPublished = chkIsPublished.Checked;
            //}
            if (isUserPost)
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
                    //DateTime metaTime = DateTime.Parse(calenderMetaDate.Text, CultureInfo.CurrentCulture);
                    //article.MetaDate = timeZone != null ? metaTime.ToUtc(timeZone) : metaTime.AddHours(-timeOffset);
                    if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                    {
                        var formatDate = calenderMetaDate.Text.ToDateTimeEn();
                        article.MetaDate = DateTime.Parse(formatDate, CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        article.MetaDate = DateTime.Parse(calenderMetaDate.Text, CultureInfo.CurrentCulture);
                    }

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
                article.MetaTitle = txtTitle.Value;
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
                if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                {
                    var formatDate = dpAddDate2.Text.ToDateTimeEn();
                    localAddTime = DateTime.Parse(formatDate, CultureInfo.CurrentCulture);
                }
                else
                {
                    localAddTime = DateTime.Parse(dpAddDate2.Text, CultureInfo.CurrentCulture);
                }
            }
            article.CreatedDate = localAddTime;
            if (txtItemUrl.Value.Length == 0)
            {
                txtItemUrl.Value = SuggestUrl();
            }
            if (txtItemUrl.Value.Contains("http"))
            {
                article.ItemUrl = txtItemUrl.Value;
            }
            else
            {
                String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Value.Replace("~/", String.Empty));
                FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

                if (
                    ((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != article.ArticleGuid))
                    && (article.ItemUrl != txtItemUrl.Value)
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
                string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Value.Replace("~/", string.Empty));

                article.ItemUrl = "~/" + newUrl;
            }
            //Lưu ảnh đại diện
            string fileName;
            //if (!string.IsNullOrEmpty(ImageName.Value))
            //{
            //}
            article.ImageUrl = ImageName.Value;

            //Lưu lịch sử phiên bản nội dung
            //if (enableContentVersioning)
            //{
            article.CreateHistory(siteSettings.SiteGuid);
            if (currentTitle != article.Title || currentDescription != article.Description)
            {
                ContentHistory contenHistory = new ContentHistory();
                contenHistory.SiteGuid = siteSettings.SiteGuid;
                contenHistory.UserGuid = article.LastModUserGuid;
                contenHistory.ContentGuid = article.ArticleGuid;
                contenHistory.Title = article.Title;
                contenHistory.ContentText = article.Description;
                contenHistory.CreatedUtc = article.LastModUtc;
                contenHistory.Save();
            }
            //}
            //article.CategoryID = int.Parse(ddlCategories.SelectedValue.ToString());
            //if (!string.IsNullOrEmpty(ddlPoll.SelectedValue.ToString()))
            //{
            //    article.PollGuid = Guid.Parse(ddlPoll.SelectedValue.ToString());
            //}
            //else
            //{
            //    article.PollGuid = Guid.Empty;
            //}
            //todo build fts for article
            var fulltext = article.Title + " " + article.Summary + " " + article.CreatedByUser;
            fulltext = fulltext.ConvertToFTS();
            if (!string.IsNullOrEmpty(fulltext))
            {
                article.FTS = fulltext;
            }

            article.TitleFTS = txtTitle.Value.ConvertToFTS();
            article.SapoFTS = txtSummary.Text.ConvertToFTS();
            article.AuthorFTS = txtAuthor.Text.ConvertToFTS();
            //Định danh meta

            if (!String.IsNullOrEmpty(txtMetaIdentifier.Text))
            {
                article.MetaIdentifier = txtMetaIdentifier.Text;
            }
            else
            {
                article.MetaIdentifier = article.ItemUrl;
            }
            if (pageId <= 0)
            {
                pageId = 1;

            }

            article.ViTriHienThiNgayDang = ViTriHienThiNgayDang.SelectedValue;
            article.IsHienThiTacGia = IsHienThiTacGia.Checked;

            //article.PageID = pageId;
            article.Save();
            string categories = string.Empty;
            #region Save danh sách chuyên mục
            //remote old category article
            if (itemId > 0)
            {
                ArticleCategory.DeleteAll(itemId);
            }
            string categoryArticle = string.Empty;

            if (!string.IsNullOrEmpty(hdfCategories.Value))
            {
                var listCategory = hdfCategories.Value.Split(',');
                if (listCategory != null && listCategory.Count() > 0)
                {
                    foreach (var item in listCategory)
                    {
                        ArticleCategory articleCategory = new ArticleCategory();
                        articleCategory.ArticleID = article.ItemID;
                        articleCategory.CategoryID = item.ToIntOrZero();
                        articleCategory.SiteID = siteSettings.SiteId;
                        articleCategory.Save();
                    }
                    article.CategoryID = listCategory[0].ToIntOrZero();
                }
            }
            article.Save();

            #endregion


            if (txtItemUrl.Value.Contains("http") == false)
            {
                String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Value.Replace("~/", String.Empty));
                FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

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
                    //if ((oldUrl.Length > 0) && (newUrl.Length > 0) && (!SiteUtils.UrlsMatch(oldUrl, newUrl)))
                    //{
                    //    //worry about the risk of a redirect loop if the page is restored to the old url again
                    //    // don't create it if a redirect for the new url exists
                    //    if (
                    //        (!RedirectInfo.Exists(siteSettings.SiteId, oldUrl))
                    //        && (!RedirectInfo.Exists(siteSettings.SiteId, newUrl))
                    //        )
                    //    {
                    //        RedirectInfo redirect = new RedirectInfo
                    //        {
                    //            SiteGuid = siteSettings.SiteGuid,
                    //            SiteId = siteSettings.SiteId,
                    //            OldUrl = oldUrl,
                    //            NewUrl = newUrl
                    //        };
                    //        redirect.Save();
                    //    }
                    //    // since we have created a redirect we don't need the old friendly url
                    //    FriendlyUrl oldFriendlyUrl = new FriendlyUrl(siteSettings.SiteId, oldUrl);
                    //    if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == article.ArticleGuid))
                    //    {
                    //        FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                    //    }
                    //}
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
            }
            #region Lưu log bài viết
            ArticleLog articleLog = new ArticleLog();
            articleLog.ArticleID = article.ItemID;
            articleLog.Comment = txtCommentByBoss.Text;
            if (!string.IsNullOrEmpty(dpEndDate2.Text))
            {
                if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                {
                    var formatDate = dpEndDate2.Text.ToDateTimeEn();
                    articleLog.EndDate = DateTime.Parse(formatDate, CultureInfo.CurrentCulture);
                }
                else
                {
                    articleLog.EndDate = DateTime.Parse(dpEndDate2.Text, CultureInfo.CurrentCulture);
                }

            }

            articleLog.IsApprove = article.IsApproved;
            articleLog.IsPublic = article.IsPublished;
            if (!string.IsNullOrEmpty(dpAddDate2.Text))
            {
                if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                {
                    var formatDate = dpAddDate2.Text.ToDateTimeEn();
                    articleLog.PostDate = DateTime.Parse(formatDate, CultureInfo.CurrentCulture);
                }
                else
                {
                    articleLog.PostDate = DateTime.Parse(dpAddDate2.Text, CultureInfo.CurrentCulture);
                }

            }
            if (!string.IsNullOrEmpty(dpBeginDate2.Text))
            {
                if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                {
                    var formatDate = dpBeginDate2.Text.ToDateTimeEn();
                    articleLog.StartDate = DateTime.Parse(formatDate, CultureInfo.CurrentCulture);
                }
                else
                {
                    articleLog.StartDate = DateTime.Parse(dpBeginDate2.Text, CultureInfo.CurrentCulture);
                }

            }
            articleLog.UserID = siteUser.UserId;
            articleLog.Save();

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
            //save file atachment
            if (!string.IsNullOrEmpty(hdfFileAtachment.Value))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var listFile = js.Deserialize<List<AtachmentFile>>(hdfFileAtachment.Value);
                if (listFile != null && listFile.Count > 0)
                {
                    foreach (var item in listFile)
                    {
                        FileAttachments fileAttachments = new FileAttachments();
                        fileAttachments.CreatedDate = DateTime.Now;
                        fileAttachments.DownloadCount = 0;
                        var index = item.fileName.LastIndexOf(".");
                        fileAttachments.FileExtensions = item.fileName.Substring(index);
                        fileAttachments.FileName = item.fileName;
                        fileAttachments.FilePath = item.filePath;
                        fileAttachments.ObjectID = article.ItemID;
                        fileAttachments.TypeItem = 1;
                        fileAttachments.Save();
                    }
                }
            }


            article.ContentChanged += new ContentChangedEventHandler(article_ContentChanged);
            //SaveSyncPost();
            CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
            SiteUtils.QueueIndexing();
            //EmailNewPost();
            return true;
        }

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
                                                 Page.ResolveUrl(ImageSiteRoot + settingsSync.SiteFolderName + "/article/postarticle.aspx?pageid=" + loadedPageId + "&mid=" + articleSync.ModuleID + "&itemid=" + articleSync.ItemID) + ">",
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

        private void DeleteAudioFromServer()
        {
            string audioURL = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleAudiosFolder"] + article.AudioUrl;
            audioURL = audioURL.Replace("/", "\\");
            if (File.Exists(audioURL))
            {
                File.Delete(audioURL);
            }
        }

        void article_ContentChanged(object sender, ContentChangedEventArgs e)
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

                    edContent.Text = System.Web.HttpUtility.HtmlDecode(history.ContentText);
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

            Literal literContent = new Literal();
            literContent.Text = System.Web.HttpUtility.HtmlDecode(history.ContentText);
            edContent.Text = literContent.Text;
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
                    article.ContentChanged += new ContentChangedEventHandler(article_ContentChanged);
                    Article.Delete(article.ItemID);
                    //FriendlyUrl.DeleteByPageGuid(article.ArticleGuid, siteSettings.SiteId, article.ItemUrl);
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
            if (itemId > 0)
            {
                moduleTitle.EditText = "Cập nhật tin bài";
                heading.Text = "Cập nhật tin bài";
            }
            else
            {
                moduleTitle.EditText = "Thêm mới tin bài";
                heading.Text = "Thêm mới tin bài";
            }
            progressBar.AddTrigger(btnUpdate);

            litContentTab.Text = @"<a href='#tabContent'>" + ArticleResources.ContentTab + @"</a>";

            litMetaTab.Text = @"<a href='#tabMeta'>" + ArticleResources.MetaTab + @"</a>";

            //dpBeginDate2.Enabled = false;
            //dpEndDate2.Enabled = false;
            if (isApprover)
            {
                pnlIsApproved.Visible = true;
                pnlIsPublished.Visible = true;

            }
            else if (isUserPost)
            {
                pnlIsApproved.Visible = false;
                pnlIsPublished.Visible = true;
            }
            else
            {
                pnlIsApproved.Visible = false;
                pnlIsPublished.Visible = false;
            }



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

            //rfvCategory.ErrorMessage = ArticleResources.CategoryRequired;


            reqStartDate.ErrorMessage = ArticleResources.BlogBeginDateRequiredHelp;
            dpBeginDate2.ClockHours = ConfigurationManager.AppSettings["ClockHours"];
            dpEndDate2.ClockHours = ConfigurationManager.AppSettings["ClockHours"];

            grdHistory.Columns[0].HeaderText = ArticleResources.CreatedDateGridHeader;
            grdHistory.Columns[1].HeaderText = ArticleResources.ArchiveDateGridHeader;

            btnRestoreFromGreyBox.ImageUrl = Page.ResolveUrl("~/Data/SiteImages/1x1.gif");
            btnRestoreFromGreyBox.AlternateText = @" ";

            btnDeleteHistory.Text = ArticleResources.DeleteAllHistoryButton;
            UIHelper.AddConfirmationDialog(btnDeleteHistory, ArticleResources.DeleteAllHistoryWarning);
        }

        private void LoadSettings()
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(getModuleSettings);
            if (WebUser.IsInRoles("Admins"))
            {
                isAdmin = true;
            }
            if (CommonArticle.AccessApprovedArticle || CommonArticle.AccessManageArticle || CommonArticle.AccessPublisheedArticle)
            {
                isUserPost = true;
            }
            if (CommonArticle.AccessApprovedArticle || CommonArticle.AccessManageArticle)
            {
                isApprover = true;
            }
            if (!CommonArticle.AccessManageArticle)
            {
                ListCategory = CategoryUserArticle.GetCategoryIdByUser(siteUser.UserId);
            }


            lnkCancel.NavigateUrl = SiteUtils.GetCurrentPageUrl();

            //enableContentVersioning = config.EnableContentVersioning;

            //if ((siteSettings.ForceContentVersioning) || (WebConfigSettings.EnforceContentVersioningGlobally))
            //{
            //    enableContentVersioning = true;
            //}
            enableContentVersioning = true;


            if (itemId > -1)
            {
                article = new Article(itemId);
                //if (CommonArticle.AccessManageArticle || (ListCategory.Contains(article.CategoryID)))
                //{
                //    //continude
                //}
                //else
                //{
                //    SiteUtils.RedirectToAccessDeniedPage();
                //}
            }

            //pnlAttachment.Visible = config.UseAttachmentSetting;

            //divHistoryDelete.Visible = (enableContentVersioning && isAdmin);

            //pnlHistory.Visible = enableContentVersioning;

            //if (enableContentVersioning)
            //{
            //    SetupHistoryRestoreScript();
            //}
            divHistoryDelete.Visible = isAdmin;

            pnlHistory.Visible = true;

            SetupHistoryRestoreScript();

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
            var site = CacheHelper.GetCurrentSiteSettings();
            return new JavaScriptSerializer().Serialize(ArticleReferenceBO.GetList(site.SiteId, -1));
        }

        [WebMethod(EnableSession = true)]
        [HttpPost]
        public static bool DeleteFile(int itemid)
        {
            return FileAttachments.Delete(itemid);
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
        protected string StatePublic(object state)
        {
            if (state == null)
            {
                return string.Empty;
            }

            var result = "<span class='iFail'><i class='fa fa-ban' aria-hidden='true'></i></span>";

            if (bool.Parse(state.ToString()))
            {
                result = "<span class='iSuccess'><i class='fa fa-check' aria-hidden='true'></i></span>";
            }
            else
            {
                result = "<span class='iFail'><i class='fa fa-ban' aria-hidden='true'></i></span>";
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
                + "\"" + SiteRoot + "/UIUtils/BlogUrlSuggestService.ashx" + "\", "
                + "document.getElementById('" + lboxCategories.ClientID + "'), "
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

        protected string LoadCategoryHtml(int selected = 0)
        {
            List<ListItem> listItem = new List<ListItem>();
            var html = new StringBuilder();
            html.Append("<select class='ddlCategory form-control'>");
            html.Append("<option value=''>--Chọn chuyên mục cha--</option>");
            var listCategory = CoreCategory.GetChildren(siteSettings.ArticleCategory).ToList();
            foreach (var item in listCategory)
            {

                if (item.ItemID == selected)
                {
                    html.Append("<option value='" + item.ItemID + "' selected>" + item.Name + "</option>");
                }
                else
                {
                    html.Append("<option value='" + item.ItemID + "'>" + item.Name + "</option>");
                }
            }
            html.Append("</select>");
            return html.ToString();
        }

        protected List<ListItem> LoadCategoryChild(int parentId)
        {
            if (parentId > 0)
            {
                var listCategory = CoreCategory.GetChildren(parentId);
                List<ListItem> listItem = new List<ListItem>();
                if (listCategory != null && listCategory.Count > 0)
                {
                    listItem = listCategory.Select(x => new ListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
                }

                for (int i = 0; i < listItem.Count; i++)
                {
                    List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(listItem[i].Value));
                    if (children.Count <= 0) continue;
                    string prefix = string.Empty;
                    while (listItem[i].Text.StartsWith("|"))
                    {
                        prefix += listItem[i].Text.Substring(0, 3);
                        listItem[i].Text = listItem[i].Text.Remove(0, 3);
                    }
                    listItem[i].Text = prefix + listItem[i].Text;
                    int index = 1;
                    foreach (CoreCategory child in children)
                    {
                        ListItem list = new ListItem
                        {
                            Text = prefix + @"|--" + child.Name,
                            Value = child.ItemID.ToString(),
                        };
                        listItem.Insert(listItem.IndexOf(listItem[i]) + index, list);
                        index++;
                    }
                }
                return listItem;

            }
            return new List<ListItem>();
        }

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static PagedList<ArticleReferenceBO> GetPageReferenceBO(int pageIndex = 1, int pageSize = 9, int category = 0, string keyword = "", int typeSearch = 0)
        {
            if (category == -1)
            {
                category = 0;
            }
            DateTime? searchDate = null;
            if (typeSearch > 0)
            {
                if (typeSearch == TypeSearchArticleConstant.OneYear)
                {
                    searchDate = DateTime.Now.AddYears(-1);
                }
                else if (typeSearch == TypeSearchArticleConstant.OneMonth)
                {
                    searchDate = DateTime.Now.AddMonths(-1);
                }
                else if (typeSearch == TypeSearchArticleConstant.ThreeMonth)
                {
                    searchDate = DateTime.Now.AddMonths(-3);
                }
                else if (typeSearch == TypeSearchArticleConstant.SixMonth)
                {
                    searchDate = DateTime.Now.AddMonths(-6);
                }
                else if (typeSearch == TypeSearchArticleConstant.NineMonth)
                {
                    searchDate = DateTime.Now.AddMonths(-9);
                }
            }

            return ArticleReferenceBO.GetPageReference(pageIndex, pageSize, category, keyword, searchDate);
        }


    }

    public class ArticleCategoryDto
    {
        public long ArticleID { get; set; }
        public int CategoryParentID { get; set; }
        public int CategoryChildID { get; set; }
        public bool IsHotCatParent { get; set; }
        public bool IsHotCatChild { get; set; }
        public bool IsNotDisplayParent { get; set; }
    }

    public class AtachmentFile
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
}
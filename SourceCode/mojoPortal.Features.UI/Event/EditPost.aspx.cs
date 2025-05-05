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
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventFeature.UI
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
        protected EventConfiguration config = new EventConfiguration();
        private int pageNumber = 1;
        private const int pageSize = 10;
        private int totalPages = 1;
        private Guid restoreGuid = Guid.Empty;
        private Event _event;
        private Event articleSync;
        private bool enableContentVersioning;
        protected bool isAdmin;
        readonly ContentMetaRespository metaRepository = new ContentMetaRespository();
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private int catId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private bool isUserPost;
        private bool isApprover;
        private string dateTimeFormat;
        public SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
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

            btnUpdate.Click += btnUpdate_Click;
            btnUpdate3.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnDelete3.Click += btnDelete_Click;

            btnDeleteImg.Click += btnDeleteImg_Click;

            btnSaveAndPreview.Click += btnSaveAndPreview_Click;

            grdHistory.RowCommand += grdHistory_RowCommand;
            grdHistory.RowDataBound += grdHistory_RowDataBound;
            pgrHistory.Command += pgrHistory_Command;
            btnRestoreFromGreyBox.Click += btnRestoreFromGreyBox_Click;
            btnDeleteHistory.Click += btnDeleteHistory_Click;
            //chkIsApproved.CheckedChanged += //chkIsApproved_CheckedChanged;
            SiteUtils.SetupEditor(edContent);
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

            //if (!UserCanEditModule(moduleId) && !isUserPost && !isApprover)
            if (CommonEvent.AccessManageEvent ||!WebUser.IsInRole("Admins"))
            {
                //continude
            }
            else
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
            if (!Page.IsPostBack)
            {
                PopulateLabels();
                PopulateControls();
            }
        }

        protected virtual void PopulateControls()
        {
            if (_event != null)
            {
                string selectedValue = _event.CategoryID.ToString();

                //dpBeginDate.ShowTime = true;

                //dpBeginDate.Text = timeZone != null ? _event.StartDate.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(_event.StartDate.ToString(dateTimeFormat));

                if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                {
                    dpBeginDate.Text = string.Format("{0:MM/dd/yyyy HH:mm}", _event.StartDate);
                    if (_event.EndDate.HasValue)
                    {
                        //dpEndDate.Text = timeZone != null ? _event.EndDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(_event.EndDate.Value.ToString(dateTimeFormat));
                        dpEndDate.Text = string.Format("{0:MM/dd/yyyy HH:mm}", _event.EndDate);
                    }
                }
                else
                {
                    dpBeginDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}", _event.StartDate);
                    if (_event.EndDate.HasValue)
                    {
                        //dpEndDate.Text = timeZone != null ? _event.EndDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(_event.EndDate.Value.ToString(dateTimeFormat));
                        dpEndDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}", _event.EndDate);
                    }
                }

                if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                {
                    if (_event.StartTime.HasValue)
                    {
                        dpStartTime.Text = string.Format("{0:MM/dd/yyyy HH:mm}", _event.StartTime);

                    }
                    if (_event.EndTime.HasValue)
                    {
                        //dpEndDate.Text = timeZone != null ? _event.EndDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(_event.EndDate.Value.ToString(dateTimeFormat));
                        dpEndTime.Text = string.Format("{0:MM/dd/yyyy HH:mm}", _event.EndTime);
                    }
                }
                else
                {
                    if (_event.StartTime.HasValue)
                    {
                        dpStartTime.Text = string.Format("{0:dd/MM/yyyy HH:mm}", _event.StartTime);
                    }
                    if (_event.EndTime.HasValue)
                    {
                        //dpEndDate.Text = timeZone != null ? _event.EndDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(_event.EndDate.Value.ToString(dateTimeFormat));
                        dpEndTime.Text = string.Format("{0:dd/MM/yyyy HH:mm}", _event.EndTime);
                    }
                }




                txtPlace.Text = _event.Location;
                txtTitle.Text = _event.Title;
                txtSummary.Text = _event.Summary;
                txtItemUrl.Text = _event.ItemUrl;
                edContent.Text = _event.Description;
                //txtTag.Text = _event.Tag;
                txtMetaDescription.Text = _event.MetaDescription;
                if (!string.IsNullOrEmpty(_event.CreatedByUser))
                {
                    txtAuthor.Text = _event.CreatedByUser;
                }
                if (!string.IsNullOrEmpty(_event.ImageUrl))
                {
                    divImage.Visible = true;
                    imgView.ImageUrl = "~/" + ConfigurationManager.AppSettings["EventImagesFolder"] + _event.ImageUrl;
                }
                else { divImage.Visible = false; }

                txtMetaKeywords.Text = _event.MetaKeywords;

                if (_event.IsPublished.HasValue)
                {
                    chkIsPublished.Checked = _event.IsPublished.Value;
                }
                chkIsAllowComment.Checked = _event.AllowComment;
                chkIsHot.Checked = _event.IsHot;


                // show preview button for saved drafts
                if ((_event.IsApproved.HasValue && !_event.IsApproved.Value) || (_event.StartDate > DateTime.UtcNow))
                {
                    btnSaveAndPreview.Visible = true;
                }


                //BindHistory();
            }
            else
            {
                chkIsPublished.Checked = true;
                //dpBeginDate.Text = DateTime.UtcNow.ToLocalTime(timeZone).ToString(dateTimeFormat);// DateTimeHelper.LocalizeToCalendar(DateTime.UtcNow.ToString("g"));
                if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                {
                    dpBeginDate.Text = string.Format("{0:MM/dd/yyyy HH:mm}", DateTime.Now);

                    dpStartTime.Text = string.Format("{0:MM/dd/yyyy HH:mm}", DateTime.Now);
                }
                else
                {
                    dpBeginDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);

                    dpStartTime.Text = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                }
                dpEndDate.Text = string.Empty;
                btnDelete.Visible = false;
                btnDelete3.Visible = false;
                pnlHistory.Visible = false;
                if (Request.IsAuthenticated && siteUser != null)
                {
                    txtAuthor.Text = siteUser.Name;
                }



            }

            if ((txtItemUrl.Text.Length == 0) && (txtTitle.Text.Length > 0))
            {
                string friendlyUrl = txtTitle.Text.UrlRewriteDefault();

                txtItemUrl.Text = @"~/" + friendlyUrl;
            }

            if (_event != null)
            {
                hdnTitle.Value = txtTitle.Text;
            }

        }
        void btnDeleteImg_Click(object sender, ImageClickEventArgs e)
        {
            if (_event == null) { _event = new Event(itemId); }

            DeleteImageFromServer();
            divImage.Visible = false;
            _event.ImageUrl = string.Empty;
            _event.Save();
            if (articleSync == null) return;
            articleSync.ImageUrl = string.Empty;
            articleSync.Save();
        }


        protected virtual void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate("save_event");
            if (!Page.IsValid) return;
            if (!Save()) return;
            string url = SiteRoot + "/event/editpost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
                         _event.ItemID;
            if (itemId > -1 || !config.UseAttachmentSetting)
            {
                //if (hdnReturnUrl.Value.Length > 0 && hdnReturnUrl.Value != url)
                //{
                //    WebUtils.SetupRedirect(this, hdnReturnUrl.Value);
                //    return;
                //}
                //WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
                WebUtils.SetupRedirect(this, SiteRoot + _event.ItemUrl.Replace("~/", "/"));
            }
            else
            {
                WebUtils.SetupRedirect(this, url);
            }
        }

        void btnSaveAndPreview_Click(object sender, EventArgs e)
        {
            Page.Validate("article");
            if ((!Page.IsValid) || (!ParamsAreValid())) return;
            if (!Save()) return;
            WebUtils.SetupRedirect(this, SiteRoot + _event.ItemUrl.Replace("~/", "/"));
        }



        private bool ParamsAreValid()
        {
            try
            {
                DateTime.Parse(dpBeginDate.Text);
            }
            catch (FormatException)
            {
                lblErrorMessage.Text = EventResources.ParseDateFailureMessage;
                return false;
            }
            catch (ArgumentNullException)
            {
                lblErrorMessage.Text = EventResources.ParseDateFailureMessage;
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
            if (_event == null) { _event = new Event(itemId); }
            string currentDescription = _event.Description;
            string currentTitle = _event.Title;
            Module module = GetModule(moduleId);
            if (moduleId > 0)
            {
                _event.ModuleID = moduleId;
                //_event.ModuleGuid = module.ModuleGuid;
            }

            if (siteUser == null) { return false; }
            _event.UserGuid = siteUser.UserGuid;
            _event.LastModUserGuid = siteUser.UserGuid;
            _event.SiteID = siteSetting.SiteId;
            _event.Location = txtPlace.Text;

            _event.Title = txtTitle.Text;
            _event.Summary = txtSummary.Text;
            _event.Description = edContent.Text;
            _event.IsHot = chkIsHot.Checked;
            //_event.IsApproved = chkIsApproved.Checked;
            _event.ApprovedGuid = siteUser.UserGuid;
            _event.AllowComment = chkIsAllowComment.Checked;
            //_event.Tag = txtTag.Text;
            _event.MetaDescription = txtMetaDescription.Text;
            _event.MetaKeywords = txtMetaKeywords.Text;
            _event.CreatedByUser = txtAuthor.Text;

            if (!string.IsNullOrEmpty(dpStartTime.Text))
            {
                DateTime startTime = DateTime.Parse(dpStartTime.Text, CultureInfo.CurrentCulture);
                _event.StartTime = startTime;
            }
            else
            {
                _event.StartTime = null;
            }


            if (!string.IsNullOrEmpty(dpStartTime.Text))
            {
                DateTime endTime = DateTime.Parse(dpEndTime.Text, CultureInfo.CurrentCulture);
                _event.EndTime = endTime;
            }
            else
            {
                _event.EndTime = null;
            }



            if (isApprover)
            {
                DateTime localTime = DateTime.Parse(dpBeginDate.Text, CultureInfo.CurrentCulture);
                //_event.StartDate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                _event.StartDate = localTime;
                if (!string.IsNullOrEmpty(dpEndDate.Text))
                {
                    DateTime localEndTime = DateTime.Parse(dpEndDate.Text, CultureInfo.CurrentCulture);
                    //_event.EndDate = timeZone != null ? localEndTime.ToUtc(timeZone) : localEndTime.AddHours(-timeOffset);
                    _event.EndDate = localEndTime;
                }
                else
                {
                    _event.EndDate = null;
                }
                _event.IsPublished = chkIsPublished.Checked;
            }
            //if (chkIsApproved.Checked)
            if (_event.IsApproved.HasValue && _event.IsApproved.Value)
            {
                _event.ApprovedDate = DateTime.Now;
                _event.ApprovedGuid = siteUser.UserGuid;
            }
            //Xuất bản nội dung tin bài khi tin bài đã được phê duyệt
            //if (isUserPost && !isApprover && _event.IsApproved.HasValue && _event.IsApproved.Value)
            //{
            //    _event.IsPublished = chkIsPublished.Checked;
            //}
            _event.IsPublished = true;
            if (_event.IsPublished.HasValue && _event.IsPublished.Value)
            {
                _event.PublishedDate = DateTime.Now;
                _event.PublishedGuid = siteUser.UserGuid;
            }


            if (txtItemUrl.Text.Length == 0)
            {
                txtItemUrl.Text = SuggestUrl();
            }

            String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", String.Empty));
            FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

            if (
                ((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != _event.EventGuid))
                && (_event.ItemUrl != txtItemUrl.Text)
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

            string oldUrl = _event.ItemUrl.Replace("~/", string.Empty);
            string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));

            _event.ItemUrl = "~/" + newUrl;

            string fileName;
            if (!SaveImageUrl(out fileName))
            {
                return false;
            }
            if (!fileName.Equals(string.Empty))
            {
                _event.ImageUrl = fileName;
            }

            //todo build fts for article
            var fulltext = _event.Title + " " + _event.Summary + " " + _event.Description;
            fulltext = fulltext.ConvertToFTS();
            if (!string.IsNullOrEmpty(fulltext))
            {
                _event.FTS = fulltext;
            }
            else
            {
                _event.FTS = (_event.Title + _event.Summary).ConvertToVN();
            }
            _event.Save();
            pageId = Convert.ToInt32(WebConfigSettings.PageEvent);
            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                {
                    FriendlyUrl newFriendlyUrl = new FriendlyUrl
                    {
                        SiteId = siteSettings.SiteId,
                        SiteGuid = siteSettings.SiteGuid,
                        PageGuid = _event.EventGuid,
                        Url = friendlyUrlString,
                        RealUrl = "~/event/viewpost.aspx?pageid="
                                  + pageId.ToInvariantString()
                                  + "&mid=" + _event.ModuleID.ToInvariantString()
                                  + "&ItemID=" + _event.ItemID.ToInvariantString()
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
                    if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == _event.EventGuid))
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
                    realUrl = "~/event/viewpost.aspx?pageid="
                 + pageId.ToInvariantString()
                 + "&mid=" + _event.ModuleID.ToInvariantString()
                 + "&ItemID=" + _event.ItemID.ToInvariantString();
                    FriendlyUrl updateFriendlyUrl = new FriendlyUrl(friendlyUrl.UrlId);
                    if (updateFriendlyUrl != null)
                    {
                        updateFriendlyUrl.RealUrl = realUrl;
                        updateFriendlyUrl.Save();
                    }
                }
            }




            //if (!friendlyUrl.FoundFriendlyUrl)
            //{
            //    if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
            //    {
            //        FriendlyUrl newFriendlyUrl = new FriendlyUrl
            //        {
            //            SiteId = siteSettings.SiteId,
            //            SiteGuid = siteSettings.SiteGuid,
            //            PageGuid = _event.EventGuid,
            //            Url = friendlyUrlString,
            //            RealUrl = "~/event/viewpost.aspx?pageid="
            //                      + pageId.ToInvariantString()
            //                      + "&mid=" + _event.ModuleID.ToInvariantString()
            //                      + "&ItemID=" + _event.ItemID.ToInvariantString()
            //        };

            //        newFriendlyUrl.Save();
            //    }

            //    //if post was renamed url will change, if url changes we need to redirect from the old url to the new with 301
            //    if ((oldUrl.Length > 0) && (newUrl.Length > 0) && (!SiteUtils.UrlsMatch(oldUrl, newUrl)))
            //    {
            //        //worry about the risk of a redirect loop if the page is restored to the old url again
            //        // don't create it if a redirect for the new url exists
            //        if (
            //            (!RedirectInfo.Exists(siteSettings.SiteId, oldUrl))
            //            && (!RedirectInfo.Exists(siteSettings.SiteId, newUrl))
            //            )
            //        {
            //            RedirectInfo redirect = new RedirectInfo
            //            {
            //                SiteGuid = siteSettings.SiteGuid,
            //                SiteId = siteSettings.SiteId,
            //                OldUrl = oldUrl,
            //                NewUrl = newUrl
            //            };
            //            redirect.Save();
            //        }
            //        // since we have created a redirect we don't need the old friendly url
            //        FriendlyUrl oldFriendlyUrl = new FriendlyUrl(siteSettings.SiteId, oldUrl);
            //        if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == _event.EventGuid))
            //        {
            //            FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
            //        }

            //    }
            //}

            // new item posted so ping services
            if ((itemId == -1) && (_event.IsApproved.HasValue && _event.IsApproved.Value) && (_event.StartDate <= DateTime.UtcNow))
            {
                QueuePings();
            }

            CurrentPage.UpdateLastModifiedTime();


            //SaveSyncPost();
            CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
            SiteUtils.QueueIndexing();
            //EmailNewPost();
            return true;
        }
        #region SaveSyncPost
        #endregion
        private void EmailNewPost()
        {
            if (config.EmailNewPost == null || config.EmailNewPost.Count == 0) return;
            SmtpSettings smtpSettings = SiteUtils.GetSmtpSettings();
            string fromAddress = siteSettings.DefaultEmailFromAddress;
            StringBuilder message = new StringBuilder();
            if (_event != null)
            {
                if (articleSync != null)
                {
                    message.Append(String.Format(ArticleResources.SubjectEmailNewPostBody, siteUser.Name,
                                                 settingsSync.SiteName, "<a href=" +
                                                 Page.ResolveUrl(ImageSiteRoot + settingsSync.SiteFolderName +
                                                                 articleSync.ItemUrl.Replace("~", string.Empty)) + ">" + articleSync.Title + "</a>",
                                                 "<a href=" +
                                                 Page.ResolveUrl(ImageSiteRoot + settingsSync.SiteFolderName + "/event/EditPost.aspx?pageid=" + loadedPageId + "&mid=" + articleSync.ModuleID + "&itemid=" + articleSync.ItemID) + ">",
                                                 "</a>", "<p>", "</p>"));
                }
                else
                {
                    message.Append(String.Format(ArticleResources.SubjectEmailNewPostBodyNoSync, siteUser.Name,
                                                 siteSettings.SiteName,
                                                 "<a href=" +
                                                 Page.ResolveUrl(SiteRoot + _event.ItemUrl.Replace("~", string.Empty)) +
                                                 ">" + _event.Title + "</a>", "<p>", "</p>"));
                }
            }
            string subject = ArticleResources.SubjectEmailNewPost;
            if (_event != null)
            {
                subject = _event.Title;
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
                "~/" + ConfigurationManager.AppSettings["EventImagesFolder"]);
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
                    string path = Server.MapPath("~/" + ConfigurationManager.AppSettings["EventImagesFolder"]);
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
                    if (!_event.ImageUrl.Equals(string.Empty))
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
            string thumbnailImageURL = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["EventImagesFolder"] + _event.ImageUrl;
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

        void blog_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            IndexBuilderProvider indexBuilder = IndexBuilderManager.Providers["ArticleIndexBuilderProvider"];
            if (indexBuilder != null)
            {
                indexBuilder.ContentChangedHandler(sender, e);
            }
        }

        #region History

        private void BindHistory()
        {
            if (!enableContentVersioning) { return; }

            if ((_event == null) || (_event.ItemID == -1))
            {
                pnlHistory.Visible = false;
                return;
            }

            List<ContentHistory> history = ContentHistory.GetPage(_event.EventGuid, pageNumber, pageSize, out totalPages);

            pgrHistory.ShowFirstLast = true;
            pgrHistory.PageSize = pageSize;
            pgrHistory.PageCount = totalPages;
            pgrHistory.Visible = (totalPages > 1);

            grdHistory.DataSource = history;
            grdHistory.DataBind();

            btnDeleteHistory.Visible = (grdHistory.Rows.Count > 0);
            pnlHistory.Visible = (grdHistory.Rows.Count > 0);

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
            if (_event == null) { return; }

            ContentHistory.DeleteByContent(_event.EventGuid);
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
            if (_event != null)
            {
                if (!_event.ImageUrl.Equals(string.Empty))
                {
                    DeleteImageFromServer();
                }
                Event.Delete(_event.ItemID);
                //if (articleSync != null)
                //{
                //    articleSync.ContentChanged += blog_ContentChanged;
                //    articleSync.Delete();
                //    FriendlyUrl.DeleteByPageGuid(articleSync.EventGuid);
                //}
                FriendlyUrl.DeleteByPageGuid(_event.EventGuid, siteSetting.SiteId, _event.ItemUrl);
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
            if (itemId > 0)
            {
                heading.Text = "Cập nhật sự kiện";
            }
            else
            {
                heading.Text = "Thêm mới sự kiện";
            }
            progressBar.AddTrigger(btnUpdate);

            litContentTab.Text = @"<a href='#tabContent'>" + ArticleResources.ContentTab + @"</a>";

            litMetaTab.Text = @"<a href='#tabMeta'>" + ArticleResources.MetaTab + @"</a>";


            if (isUserPost && !isApprover)
            {
                if (_event != null && !_event.IsApproved.HasValue)
                {
                    pnlIsPublished.Visible = false;
                }
                else if (_event != null && _event.IsApproved.HasValue)
                {
                    if (_event.IsApproved.Value)
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
                    pnlIsPublished.Visible = false;
                }
            }

            //if (UserCanEditModule(moduleId) || isApprover)
            if (isApprover)
            {
                //chkIsApproved.Checked = true;
                pnlIsPublished.Visible = true;
            }


            btnDeleteImg.ImageUrl = "~/Data/SiteImages/delete.gif";
            btnDeleteImg.ToolTip = ArticleResources.ArticleDeleteLinkText;

            edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
            //edContent.WebEditor.ToolBar = ToolBar.Newsletter;
            edContent.WebEditor.Height = config.EditorHeight;


            btnUpdate.Text = ArticleResources.BlogEditUpdateButton;
            SiteUtils.SetButtonAccessKey(btnUpdate, ArticleResources.BlogEditUpdateButtonAccessKey);
            btnUpdate3.Text = ArticleResources.BlogEditUpdateButton;
            btnSaveAndPreview.Text = ArticleResources.SaveAndPreviewButton;

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

            rfvTitle.ErrorMessage = ArticleResources.TitleRequiredWarning;
            reqStartDate.ErrorMessage = ArticleResources.BlogBeginDateRequiredHelp;
            //reqEndDate.ErrorMessage = ArticleResources.EndDateRequireTitle;


            reqStartTime.ErrorMessage = ArticleResources.BlogBeginDateRequiredHelp;
            reqEndTime.ErrorMessage = ArticleResources.EndDateRequireTitle;


            dpBeginDate.ClockHours = ConfigurationManager.AppSettings["ClockHours"];
            dpEndDate.ClockHours = ConfigurationManager.AppSettings["ClockHours"];
            regexUrl.ErrorMessage = ArticleResources.FriendlyUrlRegexWarning;

            grdHistory.Columns[0].HeaderText = ArticleResources.CreatedDateGridHeader;
            grdHistory.Columns[1].HeaderText = ArticleResources.ArchiveDateGridHeader;

            btnRestoreFromGreyBox.ImageUrl = Page.ResolveUrl("~/Data/SiteImages/1x1.gif");
            btnRestoreFromGreyBox.AlternateText = @" ";

            btnDeleteHistory.Text = ArticleResources.DeleteAllHistoryButton;
            UIHelper.AddConfirmationDialog(btnDeleteHistory, ArticleResources.DeleteAllHistoryWarning);

            lblImageUrlError.Text = ArticleResources.ImageUrlErrorLabel + ConfigurationManager.AppSettings["AllowedImageFileExtensions"]
                + @" -- " + ArticleResources.ImageUrlErrorLabelFileSize + ConfigurationManager.AppSettings["AllowedImageSize"] + @" KB";

        }

        private void LoadSettings()
        {
            if ((WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; }

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new EventConfiguration(getModuleSettings);

            if (CommonEvent.AccessManageEvent)
            {
                isUserPost = true;
                isApprover = true;
            }


            lnkCancel.NavigateUrl = SiteUtils.GetCurrentPageUrl();


            if (itemId > -1)
            {
                _event = new Event(itemId);
                //if (_event.ModuleID != moduleId) { _event = null; }
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

        private void LoadParams()
        {
            timeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            siteId = siteUser.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("ItemID", -1);
            catId = WebUtils.ParseInt32FromQueryString("catid", -1);
            restoreGuid = WebUtils.ParseGuidFromQueryString("r", restoreGuid);
            cacheDependencyKey = "Module-" + moduleId.ToInvariantString();
            virtualRoot = WebUtils.GetApplicationRoot();
            dateTimeFormat = config.DateTimeFormat.ToString();
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
                dpBeginDate.Enabled = true;
                dpEndDate.Enabled = true;
            }
            else
            {
                dpBeginDate.Enabled = false;
                dpEndDate.Enabled = false;
            }
        }

    }
}
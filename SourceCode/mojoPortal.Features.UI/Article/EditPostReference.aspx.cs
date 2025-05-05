using ArticleFeature.Business;
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class EditPostReference : mojoBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EditPost));
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int itemIdRoot = -1;
        protected int languageId = -1;
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
        private ArticleReference article;
        private ArticleReference articleSync;
        private Article articleRoot;
        private bool enableContentVersioning;
        protected bool isAdmin;
        readonly ContentMetaRespository metaRepository = new ContentMetaRespository();
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private int catId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private bool isUserPost;
        private bool isPublisher;
        private string dateTimeFormat;
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
            chkIsApproved.CheckedChanged += chkIsApproved_CheckedChanged;
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

            if (!UserCanEditModule(moduleId) && !isUserPost && !isPublisher)
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
                PopulateCategories();
                PopulateControls();
            }
        }

        protected virtual void PopulateControls()
        {
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

                //dpBeginDate.ShowTime = true;

                dpBeginDate.Text = timeZone != null ? article.StartDate.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.StartDate.ToString(dateTimeFormat));
                if (article.EndDate.HasValue)
                {
                    dpEndDate.Text = timeZone != null ? article.EndDate.Value.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(article.EndDate.Value.ToString(dateTimeFormat));
                }
                itemIdRoot = article.RootArticleID;
                txtTitle.Text = article.Title;
                txtSummary.Text = article.Summary;
                txtItemUrl.Text = article.ItemUrl;
                edContent.Text = article.Description;
                txtTag.Text = article.Tag;
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

                txtMetaKeywords.Text = article.MetaKeywords;
                chkIsApproved.Checked = article.IsApproved;
                chkIsHome.Checked = article.IsHome;
                chkIsAllowComment.Checked = article.AllowComment;
                chkIsHot.Checked = article.IsHot;
                chkIsHome.Checked = article.IsHome;

                if (restoreGuid != Guid.Empty)
                {
                    ContentHistory rHistory = new ContentHistory(restoreGuid);
                    if (rHistory.ContentGuid == article.ArticleGuid)
                    {
                        edContent.Text = rHistory.ContentText;
                    }

                }
                // show preview button for saved drafts
                if ((!article.IsApproved) || (article.StartDate > DateTime.UtcNow)) { btnSaveAndPreview.Visible = true; }

                BindHistory();
            }
            else
            {
                if (articleRoot != null)
                {
                    if (!string.IsNullOrEmpty(articleRoot.ImageUrl))
                    {
                        divImage.Visible = true;
                        imgView.ImageUrl = "~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"] + articleRoot.ImageUrl;
                    }
                }
                dpBeginDate.Text = DateTime.UtcNow.ToLocalTime(timeZone).ToString(dateTimeFormat);// DateTimeHelper.LocalizeToCalendar(DateTime.UtcNow.ToString("g"));
                dpEndDate.Text = string.Empty;
                btnDelete.Visible = false;
                btnDelete3.Visible = false;
                pnlHistory.Visible = false;
                if (Request.IsAuthenticated && siteUser != null)
                {
                    txtAuthor.Text = siteUser.Name;
                }


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

        }
        void btnDeleteImg_Click(object sender, ImageClickEventArgs e)
        {
            if (article == null) { article = new ArticleReference(itemId); }

            DeleteImageFromServer();
            divImage.Visible = false;
            article.ImageUrl = string.Empty;
            article.Save();
            if (articleSync == null) return;
            articleSync.ImageUrl = string.Empty;
            articleSync.Save();
        }


        protected virtual void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate("article");
            if (!Page.IsValid) return;
            if (!Save()) return;
            string url = SiteRoot + "/article/editpostReference.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" +
                         article.ItemID;
            if (itemId > -1 || !config.UseAttachmentSetting)
            {
                //if (hdnReturnUrl.Value.Length > 0 && hdnReturnUrl.Value != url)
                //{
                //    WebUtils.SetupRedirect(this, hdnReturnUrl.Value);
                //    return;
                //}
                //WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
                WebUtils.SetupRedirect(this, SiteRoot + article.ItemUrl.Replace("~/", "/"));
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
            WebUtils.SetupRedirect(this, SiteRoot + article.ItemUrl.Replace("~/", "/"));
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
                    if (languageId == LanguageConstant.EN)
                    {
                        ListItem list = new ListItem
                        {
                            Text = coreCategory.NameEN,
                            Value = item
                        };
                        ddlCategories.Items.Add(list);
                    }
                    else
                    {
                        ListItem list = new ListItem
                        {
                            Text = coreCategory.Name,
                            Value = item
                        };
                        ddlCategories.Items.Add(list);
                    }
                }
            }
            PopulateChildNode(ddlCategories);
            ddlCategories.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
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
                    if (languageId == LanguageConstant.EN)
                    {
                        ListItem list = new ListItem
                        {
                            Text = prefix + @"|--" + child.NameEN,
                            Value = child.ItemID.ToString()
                        };
                        root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    }
                    else
                    {
                        ListItem list = new ListItem
                        {
                            Text = prefix + @"|--" + child.Name,
                            Value = child.ItemID.ToString()
                        };
                        root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    }
                    index++;
                }
            }
        }

        private bool ParamsAreValid()
        {
            try
            {
                DateTime.Parse(dpBeginDate.Text);
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
            if (article == null) { article = new ArticleReference(itemId); }
            string currentDescription = article.Description;
            string currentTitle = article.Title;
            Module module = GetModule(moduleId);
            if (module == null) { return false; }

            if (siteUser == null) { return false; }
            article.UserGuid = siteUser.UserGuid;
            article.LastModUserGuid = siteUser.UserGuid;
            article.SiteID = siteId;
            article.ModuleID = moduleId;
            article.LangID = languageId;
            article.RootArticleID = itemIdRoot;
            article.ModuleGuid = module.ModuleGuid;
            DateTime localTime = DateTime.Parse(dpBeginDate.Text, CultureInfo.CurrentCulture);
            article.StartDate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
            if (!string.IsNullOrEmpty(dpEndDate.Text))
            {
                DateTime localEndTime = DateTime.Parse(dpEndDate.Text, CultureInfo.CurrentCulture);
                article.EndDate = timeZone != null ? localEndTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
            }
            else
            {
                article.EndDate = null;
            }
            if (articleRoot != null)
            {
                article.ImageUrl = articleRoot.ImageUrl;
            }
            article.Title = txtTitle.Text;
            article.Summary = txtSummary.Text;
            article.Description = edContent.Text;
            article.IsHot = chkIsHot.Checked;
            article.IsHome = chkIsHome.Checked;
            article.IsApproved = chkIsApproved.Checked;
            article.ApprovedGuid = siteUser.UserGuid;
            article.AllowComment = chkIsAllowComment.Checked;
            article.Tag = txtTag.Text;
            article.MetaDescription = txtMetaDescription.Text;
            article.MetaKeywords = txtMetaKeywords.Text;
            article.CreatedByUser = txtAuthor.Text;

            if (txtItemUrl.Text.Length == 0)
            {
                txtItemUrl.Text = SuggestUrl();
            }

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

            string fileName;
            if (!SaveImageUrl(out fileName))
            {
                return false;
            }
            if (!fileName.Equals(string.Empty))
            {
                article.ImageUrl = fileName;
            }

            if (enableContentVersioning)
            {
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
            }
            article.CategoryID = int.Parse(ddlCategories.SelectedValue.ToString());
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
                                  + "&ReferItemID=" + article.ItemID.ToInvariantString()
                                  + "&Type=" + languageId.ToInvariantString()
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

            // new item posted so ping services
            if ((itemId == -1) && (article.IsApproved) && (article.StartDate <= DateTime.UtcNow))
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
                if (!article.ImageUrl.Equals(string.Empty))
                {
                    DeleteImageFromServer();
                }
                FriendlyUrl.DeleteByPageGuid(article.ArticleGuid, siteSettings.SiteId, article.ItemUrl);

                Article.Delete(article.ItemID);
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

            progressBar.AddTrigger(btnUpdate);

            litContentTab.Text = @"<a href='#tabContent'>" + ArticleResources.ContentTab + @"</a>";

            litMetaTab.Text = @"<a href='#tabMeta'>" + ArticleResources.MetaTab + @"</a>";


            if (isUserPost && !isPublisher)
            {
                chkIsApproved.Checked = false;
                pnlIsApproved.Visible = false;
            }

            if (UserCanEditModule(moduleId) || isPublisher)
            {
                chkIsApproved.Checked = true;
                pnlIsApproved.Visible = true;
            }


            btnDeleteImg.ImageUrl = "~/Data/SiteImages/delete.gif";
            btnDeleteImg.ToolTip = ArticleResources.ArticleDeleteLinkText;

            //edContent.WebEditor.ToolBar = ToolBar.FullWithTemplates;
            edContent.WebEditor.ToolBar = ToolBar.Newsletter;
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

            rfvCategory.ErrorMessage = ArticleResources.CategoryRequired;
            rfvTitle.ErrorMessage = ArticleResources.TitleRequiredWarning;
            reqStartDate.ErrorMessage = ArticleResources.BlogBeginDateRequiredHelp;
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
            config = new ArticleConfiguration(getModuleSettings);

            if (WebUser.IsInRoles(config.RolePost) || WebUser.IsInRoles(config.RolePublished))
            {
                isUserPost = true;
            }

            if (WebUser.IsInRoles(config.RolePublished))
            {
                isPublisher = true;
            }

            lnkCancel.NavigateUrl = SiteUtils.GetCurrentPageUrl();

            enableContentVersioning = config.EnableContentVersioning;

            if ((siteSettings.ForceContentVersioning) || (WebConfigSettings.EnforceContentVersioningGlobally))
            {
                enableContentVersioning = true;
            }
            if (itemIdRoot > -1)
            {
                articleRoot = new Article(itemIdRoot);
                if (articleRoot.ModuleID != moduleId) { articleRoot = null; }
            }
            if (itemId > -1)
            {
                article = new ArticleReference(itemId);
                if (article.ModuleID != moduleId) { article = null; }
                if (article != null) { itemIdRoot = article.RootArticleID; }
            }


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
            itemIdRoot = WebUtils.ParseInt32FromQueryString("ItemIDRoot", -1);
            languageId = WebUtils.ParseInt32FromQueryString("LangID", -1);
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
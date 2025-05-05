using ArticleFeature.Business;
using EventFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventFeature.UI
{
    public partial class EventView : UserControl
    {

        #region Properties

        private Hashtable moduleSettings;
        protected EventConfiguration config = new EventConfiguration();
        protected EventConfiguration Config = new EventConfiguration();
        private Event _event;
        private List<Event> otherEvents;
        private Module module;
        protected string DeleteLinkImage = "~/Data/SiteImages/" + WebConfigSettings.DeleteLinkImage;
        private const string newWindowMarkup = "onclick=\"window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=no,dependent=no');return false;\"";
        protected int PageId = -1;
        protected int ModuleId = -1;
        protected int ItemId = -1;
        protected int ItemIdRefer = -1;
        protected int langId = -1;
        protected int LangID = -1;
        protected int type = -1;
        protected string OrtherArticle = string.Empty;
        protected string DatePost = string.Empty;
        protected string ItemUrl = string.Empty;
        protected bool AllowComments;
        public SiteSettings siteSettings;
        protected string CommentDateTimeFormat;
        protected bool parametersAreInvalid;
        protected Double TimeOffset;
        private TimeZoneInfo timeZone;

        protected bool IsEditable;
        protected string EditContentImage = ConfigurationManager.AppSettings["EditContentImage"];
        protected string GmapApiKey = string.Empty;

        protected string blogAuthor = string.Empty;
        protected string SiteRoot = string.Empty;
        protected string ImageSiteRoot = string.Empty;
        private mojoBasePage basePage;

        private int categoryID;
        private string DisqusSiteShortName = string.Empty;
        private string IntenseDebateAccountId = string.Empty;

        //protected string EditContentImage = WebConfigSettings.EditContentImage;
        //protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        //protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkTooltip = ArticleResources.BlogEditEntryLink;
        protected string EditLinkImageUrl = string.Empty;
        protected string RegexRelativeImageUrlPatern = @"^/.*[_a-zA-Z0-9]+\.(png|jpg|jpeg|gif|PNG|JPG|JPEG|GIF)$";
        private int pageNumber = 1;
        private int totalPages = 1;
        #endregion

        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;
            //btnPostComment.Click += btnPostComment_Click;
            //dlComments.ItemCommand += dlComments_ItemCommand;
            //dlComments.ItemDataBound += dlComments_ItemDataBound;
            //dpOtherArticles.PreRender += dpOtherArticles_PreRender;
            base.OnInit(e);
            //EnableViewState = UserCanEditPage();
            basePage = Page as mojoBasePage;
            if (basePage == null) return;
            SiteRoot = basePage.SiteRoot;
            ImageSiteRoot = basePage.ImageSiteRoot;
        }

        #endregion

        private void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            Page.EnableViewState = true;
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            LoadParams();

            LoadSettings();

            PopulateLoadEvent();
            PopulateControls();
            LoadEvent();
        }
        private void SetupRedirect()
        {
            if (ItemId > 0)
            {
                int count = Article.GetCountByEvent(ItemId);
                if (count == 1)
                {
                    Article article = new Article(ItemId, true);
                    if (article != null)
                    {
                        string url = SiteRoot + "/Article/ViewPost.aspx?pageid=" + PageId.ToInvariantString()
                                        + "&ItemID=" + article.ItemID.ToInvariantString()
                                        + "&mid=" + ModuleId.ToInvariantString();
                        WebUtils.SetupRedirect(this, url);
                    }
                }
            }
        }
        private void LoadEvent()
        {
            if (ItemId > -1)
            {
                Event ev = new Event(ItemId);
                var allAccess = false;
                if (CommonEvent.AccessManageEvent || WebUser.IsInRole("Admins"))
                {
                    allAccess = true;
                }
                else
                {
                    if (ev.IsPublished.HasValue && ev.IsPublished.Value && ((ev.EndDate == null || (ev.EndDate.HasValue && ev.EndDate.Value > DateTime.Now))))
                    {
                        allAccess = true;
                    }
                }
                if (allAccess == false)
                {
                    SiteUtils.RedirectToDefault();
                }

                //image1.ImageUrl = EventUtils.FormatImageDialog(ConfigurationManager.AppSettings["EventImagesFolder"], ev.ImageUrl);
                //image1.Visible = config.ShowImage;
                //image1.CssClass = "rimg" + ModuleId + ev.ItemID;
                //image1.ToolTip = ev.Title;
                lblTime.Text = "Thời gian";
                if (ev.EndTime.HasValue)
                {
                    if (ev.EndTime.Value > DateTime.Now)
                    {
                        lblTime.Text = "Thời gian kết thúc";
                    }
                    else
                    {
                        lblTime.Text = "Dự kiến kết thúc";
                    }
                }



                HyperLink1.ToolTip = ev.Title;
                HyperLink1.Text = ev.Title;
                HyperLink1.NavigateUrl = "javascript:void(0)";
                //EventUtils.FormatBlogTitleUrl(SiteRoot, ev.ItemUrl, ev.ItemID, PageId, ModuleId);
                HyperLink1.Visible = config.UseLinkForHeading;

                HyperLink2.Text = EditLinkText;
                HyperLink2.ToolTip = EditLinkTooltip;
                HyperLink2.Visible = IsEditable;
                //HyperLink2.ImageUrl = EditLinkImageUrl;
                HyperLink2.NavigateUrl = SiteRoot + "/event/editpost.aspx?pageid=" + PageId + "&ItemID=" +
                       ItemId + "&mid=" + ModuleId;

                Literal1.Text = ev.Title;
                Literal1.Visible = !config.UseLinkForHeading;

                lblCreatedByUser.Text = FormatPostAuthor(ev.CreatedDate.ToString());
                if (ev.StartTime.HasValue)
                {
                    lblStartDate.Text = FormatArticleDate(ev.StartTime.Value);
                }
                if (ev.EndDate.HasValue)
                {
                    lblEndDate.Text = FormatArticleDate(ev.EndTime.Value);
                }
                lblDiaDiem.Text = ev.Location;
                lblSummary.Text = ev.Summary;
                literDescription.Text = ev.Description;

                lblSummary.Text = ev.Summary;
                lblMoreLink.Text = EventUtils.FormatReadMoreLink(config, ev.ItemUrl, ev.ItemID, SiteRoot, PageId, ModuleId);
            }

        }
        private void PopulateLoadEvent()
        {
            if (ItemId > -1)
            {
                rptEventRecent.DataSource = Event.GetTopSkipId(4, siteSettings.SiteId, ItemId);
                rptEventRecent.DataBind();
            }
        }
        protected string BuildEditUrl(int itemID)
        {
            return SiteRoot + "/Event/EditPost.aspx?pageid=" + PageId + "&ItemID=" +
                       itemID + "&mid=" + ModuleId;

        }
        protected string ImgLanguage()
        {
            string Imgurl = string.Empty;

            Imgurl = "~/Data/SiteImages/flags/en.gif";

            return Imgurl;
        }
        protected string FormatImgUrlLanguage(string code)
        {
            string Imgurl = "~/Data/SiteImages/flags/" + code + ".gif";
            return Imgurl;
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            if (startDate != null)
            {
                try
                {
                    //return timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
                    return string.Format("{0:dd/MM/yyyy HH:mm}", startDate);
                }
                catch { }
            }
            return "";
        }

        protected string FormatPostAuthor(string userGuid)
        {
            //return ArticleUtils.FormatPostAuthor(userGuid, config);
            if (config.ShowAuthorSignature)
            {
                return userGuid;
            }
            else return string.Empty;
        }


        void dpOtherArticles_PreRender(object sender, EventArgs e)
        {
            //BindOtherArticles();
        }

        protected virtual void PopulateControls()
        {
            if (IsEditable)
            {
            }
            basePage.Title = SiteUtils.FormatPageTitle(basePage.SiteInfo, _event.Title);
            basePage.MetaDescription = _event.MetaDescription;
            basePage.MetaKeywordCsv = _event.MetaKeywords;


            //basePage.AdditionalMetaMarkup = article.CompiledMeta;
            if (basePage.AnalyticsSection.Length == 0)
            {
                basePage.AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "blog");
            }

            bindOrtherList();

            if (Page.Header == null) { return; }

            Literal link = new Literal
            {
                ID = "articleurl",
                Text = @"<link rel='canonical' href='"
                       + SiteRoot
                       + _event.ItemUrl.Replace("~/", "/")
                       + @"' />"
            };

            Page.Header.Controls.Add(link);


        }

        private void bindOrtherList()
        {
            //rptOrtherArticle.DataSource = Event.GetArticleTopOrther(categoryID, ItemId, config.ShowEventDetailDisplay, false);

            //rptOrtherArticle.DataBind();
        }
        protected string BuildDownloadLink(string id, string name)
        {
            string innerMarkup = name;
            if (config.UseAttachmentDownloadIconSetting)
            {
                innerMarkup = "<img src='" + ImageSiteRoot + "/Data/SiteImages/Download.gif' alt='" + EventResources.DownloadLink + "' />";
            }

            return "<a href='" + SiteRoot + "/event/download.aspx?pageid=" + PageId.ToInvariantString()
                + "&amp;mid=" + ModuleId.ToInvariantString()
                + "&amp;fileid=" + id + "' "
                + "title='" + name + "' "
                + newWindowMarkup
                + ">"
                + innerMarkup
                + "</a>";
        }




        private void PopulateLabels()
        {

        }

        private void LoadSettings()
        {
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            OrtherArticle = EventResources.OrtherArticle;
            DatePost = EventResources.DatePost;

            if (CommonEvent.AccessManageEvent)
            {
                IsEditable = true;
            }

            if (ItemId > 0)
            {
                _event = new Event(ItemId);
                _event = new Event(ItemId);
                _event.HitCount = _event.HitCount < 0 ? 0 : _event.HitCount;
                _event.HitCount += 1;
                _event.Save();
            }
            module = basePage.GetModule(ModuleId);

            RegexRelativeImageUrlPatern = SecurityHelper.RegexRelativeImageUrlPatern;

            moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);

            config = new EventConfiguration(moduleSettings);

            GmapApiKey = SiteUtils.GetGmapApiKey();

            DisqusSiteShortName = config.DisqusSiteShortName.Length > 0 ? config.DisqusSiteShortName : basePage.SiteInfo.DisqusSiteShortName;

            IntenseDebateAccountId = config.IntenseDebateAccountId.Length > 0 ? config.IntenseDebateAccountId : basePage.SiteInfo.IntenseDebateAccountId;
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            //if (config.InstanceCssClass.Length > 0)
            //{
            //pnlArticle.CssClass += config.InstanceCssClass.Contains(" ")
            //                        ? " " + config.InstanceCssClass.Remove(config.InstanceCssClass.IndexOf(" ")) +
            //                          "-detail"
            //                        : " " + config.InstanceCssClass + "-detail";
            //}

            CommentDateTimeFormat = config.DateTimeFormat;


        }

        private string GetExcerpt(Event item)
        {
            item.Description = SecurityHelper.RemoveMarkup(item.Description);
            return (item.Description.Length > config.ExcerptLength) ? UIHelper.CreateExcerpt(item.Description, config.ExcerptLength, config.ExcerptSuffix) : _event.Description;
        }

        protected string FormatCommentDate(DateTime startDate)
        {
            string result = " (";
            result += timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(CommentDateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            result += ")";
            return result;
        }

        protected string FormatBlogUrl(string itemUrl, int itemId)
        {
            if (itemUrl.Length > 0)
                return SiteRoot + itemUrl.Replace("~", string.Empty);

            return SiteRoot + "/event/ViewPost.aspx?pageid=" + PageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + ModuleId.ToInvariantString();

        }





        /// <summary>
        /// Handles the item command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void dlComments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteComment")
            {
                //event.DeleteBlogComment(int.Parse(e.CommandArgument.ToString()));
                WebUtils.SetupRedirect(this, Request.RawUrl);

            }
        }


        void dlComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ImageButton btnDelete = e.Item.FindControl("btnDelete") as ImageButton;
            UIHelper.AddConfirmationDialog(btnDelete, EventResources.BlogDeleteCommentWarning);
        }




        protected virtual void SetupRssLink()
        {
            if (WebConfigSettings.DisableBlogRssMetaLink) { return; }

            if (module != null)
            {
                if (Page.Master != null)
                {
                    Control head = Page.Master.FindControl("Head1");
                    if (head != null)
                    {

                        Literal rssLink = new Literal
                        {
                            Text = @"<link rel=""alternate"" type=""application/rss+xml"" title="""
                                   + module.ModuleTitle + @""" href="""
                                   + GetRssUrl() + @""" />"
                        };

                        head.Controls.Add(rssLink);

                    }

                }
            }

        }

        private string GetRssUrl()
        {
            if (config.FeedburnerFeedUrl.Length > 0) return config.FeedburnerFeedUrl;

            return SiteRoot + "/blog" + ModuleId.ToInvariantString() + "rss.aspx";

        }
        protected bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["EventImagesFolder"] + imageUrl;
                filePath = filePath.Replace("/", "\\");
                if (File.Exists(filePath))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
        private void LoadParams()
        {
            WebUtils.GetApplicationRoot();
            TimeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            PageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            ModuleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            ItemId = WebUtils.ParseInt32FromQueryString("ItemID", -1);
            ItemIdRefer = WebUtils.ParseInt32FromQueryString("ReferItemID", -1);
            type = WebUtils.ParseInt32FromQueryString("Type", -1);

            if (PageId == -1) parametersAreInvalid = true;
            if (ModuleId == -1) parametersAreInvalid = true;
            if (ItemId == -1 && ItemIdRefer == -1) parametersAreInvalid = true;
            if (!basePage.UserCanViewPage(ModuleId)) { parametersAreInvalid = true; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //if ((Page.IsPostBack) && (!pnlFeedback.Visible))
            //{
            //    //WebUtils.SetupRedirect(this, Request.RawUrl);
            //    //return;
            //}

            base.Render(writer);
        }

        protected string FormatTooltip(string title, string content)
        {
            string result;
            if (config.UseTooltipSettings)
            {
                if (content.Length > config.TooltipMaxCharSettings)
                {
                    content = UIHelper.CreateExcerpt(content, config.TooltipMaxCharSettings, "...");
                }
                result = "<div class='tooltip'><div class='title-l'></div><div class='title-m'>" + title + "</div><div class='title-r'></div><div class='body'>" + content + "</div></div>";
            }
            else
            {
                result = title;
            }
            return result;
        }

        protected string FormatBlogTitleUrl(string itemUrl, int itemId)
        {
            if (itemUrl.Length > 0)
                return SiteRoot + itemUrl.Replace("~", string.Empty);

            return SiteRoot + "/event/ViewPost.aspx?pageid=" + PageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + ModuleId.ToInvariantString();

        }
    }
}
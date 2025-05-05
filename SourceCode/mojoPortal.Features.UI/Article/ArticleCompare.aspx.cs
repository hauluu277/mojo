using System;
using System.Globalization;
using mojoPortal.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using ArticleFeature.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using System.Collections;
using Helpers;
using mojoPortal.Web.BlogUI;

namespace ArticleFeature.UI
{
    public partial class ArticleCompare : mojoDialogBasePage
    {
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private Guid historyGuid = Guid.Empty;
        protected ArticleConfiguration config = new ArticleConfiguration();
        protected Double timeOffset;
        protected TimeZoneInfo timeZone;
        protected string currentFloat = "left";
        protected string historyFloat = "right";
        private bool RoleAccess = false;
        //private Module module = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            LoadParams();
            if (!UserCanEditModule(moduleId))
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }

            LoadSettings();
            PopulateLabels();
            PopulateControls();

        }

        private void PopulateControls()
        {
            //if (moduleId == -1) { return; }
            if (itemId == -1) { return; }
            //if (module == null) { return; }
            if (historyGuid == Guid.Empty) { return; }

            Article article = new Article(itemId);
            if (article.ModuleID != moduleId) { return; }

            ContentHistory history = new ContentHistory(historyGuid);
            if (history.ContentGuid != article.ArticleGuid) { return; }

            litCurrentHeading.Text = article.LastModUtc.ToString("dd/MM/yyyy HH:mm:ss");


            if (BlogConfiguration.UseHtmlDiff)
            {
                HtmlDiff diffHelper = new HtmlDiff(history.ContentText, article.Description);
                litCurrentVersion.Text = diffHelper.Build();
            }
            else
            {
                litCurrentVersion.Text = article.Description;
            }
            litHistoryHead.Text = history.CreatedUtc.ToString("dd/MM/yyyy HH:mm:ss");

            litHistoryVersion.Text = history.ContentText;
            btnRestore.Visible = RoleAccess;
            string onClick = "top.window.LoadHistoryInEditor('" + historyGuid + "');  return false;";
            btnRestore.Attributes.Add("onclick", onClick);

        }

        void btnRestore_Click(object sender, EventArgs e)
        {
            // this should only fire if javascript is disabled because we put a client side on click
            string redirectUrl = SiteUtils.GetNavigationSiteRoot() + "/Article/EditPost.aspx?mid=" + moduleId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&pageid=" + pageId.ToInvariantString() + "&r=" + historyGuid;

            Response.Redirect(redirectUrl);
        }




        private void PopulateLabels()
        {
            btnRestore.Text = ArticleResources.RestoreToEditorButton;
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(getModuleSettings);
            timeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();

            if (CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
            {
                currentFloat = "right";
                historyFloat = "left";

            }


        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            itemId = WebUtils.ParseInt32FromQueryString("ItemID", itemId);
            historyGuid = WebUtils.ParseGuidFromQueryString("h", historyGuid);
            if (WebUser.IsInRoles(config.RoleApproved))
            {
                RoleAccess = true;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnRestore.Click += btnRestore_Click;
        }
    }
}

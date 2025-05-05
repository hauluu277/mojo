using mojoPortal.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using QuestionAnswerFeatures.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionAnswersFeature.UI
{
    public partial class ListQAForUser : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private bool userCanEdit;
        private int countOfDrafts;
        private int pageNumber = 1;
        QuestionAnswerConfiguration config = new QuestionAnswerConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            Load += Page_Load;
            base.OnInit(e);
        }

        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabel();
            PopulateControls();

        }
        private void PopulateLabel()
        {

        }
        private void PopulateControls()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Danh sách hỏi đáp");
            moduleTitle.ModuleInstance = GetModule(moduleId);

            QuestionAnswerUser.PageId = pageId;
            QuestionAnswerUser.ModuleId = moduleId;
            QuestionAnswerUser.Config = config;
        }


        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            LoadSideContent(true, true);
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new QuestionAnswerConfiguration(settings);
        }

    }
}
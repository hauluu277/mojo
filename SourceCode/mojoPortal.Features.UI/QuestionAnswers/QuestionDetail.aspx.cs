using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuestionAnswerFeatures.Business;
using mojoPortal.Web.Framework;
using mojoPortal.Business;
using System.Collections;
using QuestionAnswerFeatures.Business;
using Resources;

namespace QuestionAnswerFeatures.UI
{
    public partial class QuestionDetail : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private int itemId = -1;
        private bool userCanEdit;
        private int countOfDrafts;
        private int pageNumber = 1;
        private QuestionAnswer QA = new QuestionAnswer();
        private QuestionAnswerConfiguration config = new QuestionAnswerConfiguration();

        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;
            base.OnInit(e);
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!UserCanViewPage(moduleId))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage();
            //    return;
            //}
            LoadParams();
            LoadSettings();
            PopulateControls();

        }
        private void PopulateLables()
        {
            var question = new QuestionAnswer(itemId);

            if (question != null)
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, question.Question);
            }
            else
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, "Hỏi đáp");
            }
        }
        private void PopulateControls()
        {
            QuestionDetailRecentList.ModuleID = moduleId;
            QuestionDetailRecentList.PageID = pageId;
            QuestionDetailRecentList.Config = config;
            QuestionDetailRecentList.SiteRoot = SiteRoot;
            QuestionDetailRecentList.SiteSetting = siteSettings;
            if (!IsPostBack)
            {
                QuestionAnswer.UpdateView(itemId);
                QA = new QuestionAnswer(itemId);
                QuestionDetailRecentList.QA = QA;

                if (QA != null)
                {
                    Title = SiteUtils.FormatPageTitle(siteSettings, QA.Question);
                }
                else
                {
                    Title = SiteUtils.FormatPageTitle(siteSettings, SwirlingQuestionResource.DetailContentLabel);
                }

            }
        }
        private void LoadSettings()
        {
            LoadSideContent(false, true);
            userCanEdit = UserCanEditModule(moduleId);
            moduleTitle.Visible = false;
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            itemId = WebUtils.ParseInt32FromQueryString("itemId", itemId);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new QuestionAnswerConfiguration(settings);
        }
    }
}
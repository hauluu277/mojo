using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using log4net;
using mojoPortal.Business;
using Resources;
using mojoPortal.Features;

namespace QuestionAnswerFeatures.UI
{
    public partial class PostQuestionModule : SiteModuleControl
    {
        QuestionAnswerConfiguration config = new QuestionAnswerConfiguration();
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParam();
            LoadSettings();
            PopulateControl();
        }
        private void PopulateControl()
        {
            PostQuestion.ModuleID = ModuleId;
            PostQuestion.PageID = PageId;
            PostQuestion.SiteRoot = SiteRoot;
            pnlOuterWrap.CssClass = config.ClassCustom;
        }
        private void LoadParam()
        {

        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new QuestionAnswerConfiguration(getModuleSettings);
        }
    }
}
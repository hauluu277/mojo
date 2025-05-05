using mojoPortal.Business;
using mojoPortal.Web;
using QuestionAnswerFeatures.Business;
using QuestionAnswerFeatures.UI;
using Resources;
using SurveyFeature.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionAnswerFeature.UI
{
    public partial class DialogQuestionModule : SiteModuleControl
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
            PopulateLabel();
            if (!IsPostBack)
            {
                PopulateControl();
            }
        }
        private void PopulateLabel()
        {
            pnlOuterWrap.CssClass = config.ClassCustom;
            hplLink.NavigateUrl = SiteRoot + "/hoi-dap";
            if (string.IsNullOrEmpty(config.TitleSetting))
            {
                hplLink.Text = SwirlingQuestionResource.QuestionAnswer;
            }
            else
            {
                hplLink.Text = config.TitleSetting;
            }

        }
        private void PopulateControl()
        {
            List<QuestionAnswerFeatures.Business.QuestionAnswer> listQA = QuestionAnswerFeatures.Business.QuestionAnswer.GetTopQuestion(siteSettings.SiteId, config.ShowNumberQuestion);
            rptQuestion.DataSource = listQA;
            rptQuestion.DataBind();
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
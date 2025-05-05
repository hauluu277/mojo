using mojoPortal.Business;
using mojoPortal.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using Resources;

namespace QuestionAnswerFeatures.UI
{
    public partial class QuestionAnswerModule : SiteModuleControl
    {
        QuestionAnswerConfiguration config = new QuestionAnswerConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabel();
            PopulateControl();
        }
        private void PopulateControl()
        {
            QuestionAnswerRecenlist.PageID = PageId;
            QuestionAnswerRecenlist.ModuleID = ModuleId;
            QuestionAnswerRecenlist.SiteSetting = siteSettings;
            QuestionAnswerRecenlist.Config = config;
            QuestionAnswerRecenlist.SiteRoot = SiteRoot;
        }
        private void PopulateLabel()
        {
            TitleControl.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    TitleControl.Visible = true;
                    //TitleControl.EditUrl = SiteRoot + "/dang-tin-hoi-dap";
                    //TitleControl.EditText = SwirlingQuestionResource.DangTinTitle;
                }
            }
            //TitleControl.Visible = config.ShowTitle;
            if (IsEditable)
            {
                TitleControl.ForceShowExtraMarkup = true;
                TitleControl.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/QuestionAnswers/managepost.aspx?pageid=" + PageId.ToInvariantString()
                    + "&amp;mid=" + ModuleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='" + SwirlingQuestionResource.QuanTriHoiDapTitle + "'>" + SwirlingQuestionResource.QuanTriHoiDapTitle + "</a>"
                    ;
            }
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
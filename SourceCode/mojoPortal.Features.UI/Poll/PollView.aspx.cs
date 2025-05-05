using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using Resources;
using mojoPortal.Web;
using PollFeature.Business;
using System.Web.UI;
using log4net;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using System.Collections;
using mojoPortal.Business;


namespace PollFeature.UI
{
    public partial class PollView : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PollEdit));

        private int pageId = -1;
        private int moduleId = -1;
        private mojoPortal.Business.Module currentModule = null;
        private Guid pollGuid = Guid.Empty;
        private Double timeOffset = 0;
        private TimeZoneInfo timeZone = null;
        private bool isPublish = false;
        private bool isApprove = false;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected PollConfiguration config = new PollConfiguration();
        private SiteUser currentUser = null;
        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Load += new EventHandler(Page_Load);
            btnSaveComment.Click += btnSaveComment_Click;
            SuppressPageMenu();

        }
        private void btnSaveComment_Click(object sender, EventArgs e)
        {
            PollOpinion pollOpinion = new PollOpinion();
            pollOpinion.SiteID = SiteId;
            pollOpinion.PollGuid = pollGuid;
            pollOpinion.Opinion = txtComment.Text;
            if (currentUser != null)
            {
                pollOpinion.CreateByUser = currentUser.UserId;
            }
            pollOpinion.Save();
            string url = siteSettings.SiteRoot + "/Poll/PollView.aspx?PollGuid=" + Eval("PollGuid") + "&pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, url);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SecurityHelper.DisableBrowserCache();
            LoadSettings();
            if (UserCanEditModule(moduleId, Poll.FeatureGuid) || isPublish || isApprove) { }
            else
            {
                SiteUtils.RedirectToEditAccessDeniedPage();
                return;
            }
            PopulateLabels();
            PopulateControls();
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, PollResources.PollDetailTitle);
            heading.Text = PollResources.PollDetailTitle;
            if (pollGuid == Guid.Empty)
            {
                heading.Text = PollResources.PollAddNewLabel;
            }
            btnSaveComment.Text = PollResources.PollEditSaveButton;
            lblComment.Text = PollResources.LeadersForComment;
            lblListComment.Text = Resources.PollResources.PollListComent;
        }
        private void LoadSettings()
        {
            timeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            pollGuid = WebUtils.ParseGuidFromQueryString("PollGuid", Guid.Empty);

            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            currentUser = SiteUtils.GetCurrentSiteUser();


            currentModule = GetModule(moduleId, Poll.FeatureGuid);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new PollConfiguration(moduleSettings);
            if (!IsPostBack)
            {
                AddClassToBody("polledit");
            }
            if (WebUser.IsInRoles(config.RolePublish))
            {
                isPublish = true;
            }
            if (WebUser.IsInRoles(config.RoleApprove))
            {
                isApprove = true;
            }
        }

        private void PopulateControls()
        {
            if (currentModule == null) return;

            if (IsPostBack) return;
            if (Page.IsCallback) return;

            Poll poll = new Poll(pollGuid);

            txtQuestion.Text = poll.Question;
            chkAllowViewingResultsBeforeVoting.Checked = poll.AllowViewingResultsBeforeVoting;
            chkAnonymousVoting.Checked = poll.AnonymousVoting;
            chkShowOrderNumbers.Checked = poll.ShowOrderNumbers;
            chkShowResultsWhenDeactivated.Checked = poll.ShowResultsWhenDeactivated;
            CboxPublish.Checked = poll.IsPublish;
            CboxApprove.Checked = poll.IsApprove;
            textComment.Text = poll.Comment;
            if (timeZone != null)
            {
                //lblTimeAcctive.Text = DateTimeHelper.LocalizeToCalendar(poll.ActiveFrom.ToLocalTime(timeZone).ToString("g"));
                //lblToTime.Text = PollResources.PollEditToLabel;
                //lblActiveTo.Text = DateTimeHelper.LocalizeToCalendar(poll.ActiveTo.ToLocalTime(timeZone).ToString("g"));
                //if (timeZone != null)
                //{
                //    poll.ActiveFrom = poll.ActiveFrom.ToLocalTime(timeZone);
                //}
                //else
                //{
                //    poll.ActiveFrom = poll.ActiveFrom.AddHours(timeOffset);
                //}
                //DateTime DateActive = Convert.ToDateTime(poll.ActiveFrom.ToString());
                //DateTime DateNow = DateTime.Parse(DateTime.Now.ToString());
                //if (DateActive < DateNow)
                //{
                //    lblTimeError.Visible = true;
                //    lblTimeError.Text = PollResources.PollTimeError;
                //}
                lblTimeAcctive.Text = String.Format("{0:dd/MM/yyyy HH:mm}", poll.ActiveFrom.ToString());
                lblToTime.Text = String.Format("{0:dd/MM/yyyy HH:mm}", poll.ActiveTo.ToString());
                if (poll.ActiveTo < DateTime.Now)
                {
                    lblTimeError.Text = PollResources.PollTimeError;
                }
            }


            List<PollOption> pollOptions = PollOption.GetOptionsByPollGuid(pollGuid);
            ListItem li;
            foreach (PollOption option in pollOptions)
            {
                li = new ListItem(option.Answer, option.OptionGuid.ToString());
                lbOptions.Items.Add(li);
            }
            //List<PollOpinion> pollOpinion = new List<PollOpinion>();
            //pollOpinion = PollOpinion.GetByPoll(pollGuid);
            //if (pollOpinion != null)
            //{
            //  rptOpinion.DataSource = pollOpinion;
            //    rptOpinion.DataBind();
            // }

        }
        protected bool CheckCount()
        {
            List<PollOpinion> pollOpinion = new List<PollOpinion>();
            pollOpinion = PollOpinion.GetByPoll(pollGuid);
            if (pollOpinion != null && pollOpinion.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
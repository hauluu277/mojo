/// Author:       			Christian Fredh
/// Created:      			2007-04-25
/// Last Modified:			2010-03-30 Joe Audette

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using mojoPortal.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using log4net;
using PollFeature.Business;
using PollFeature.UI.Helpers;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using System.Collections;

namespace PollFeature.UI
{
    public partial class PollModule : SiteModuleControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PollModule));

        private Poll poll;
        private SiteUser currentUser = null;
        private bool userHasVoted = false;
        private bool showMyPollHistoryLink = false;
        private string resultBarColor = "#3082af";
        private string instanceCssClass = string.Empty;

        private PollConfiguration config = new PollConfiguration();
        private bool isPublish = false;
        private bool isApprove = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);
            this.rblOptions.SelectedIndexChanged += new EventHandler(rblOptions_SelectedIndexChanged);
            this.btnShowResults.Click += new EventHandler(btnShowResults_Click);
            this.btnBackToVote.Click += new EventHandler(btnBackToVote_Click);
            this.dlResults.ItemCreated += new DataListItemEventHandler(dlResults_ItemCreated);
            this.rptResults.ItemDataBound += new RepeaterItemEventHandler(rptResults_ItemDataBound);
            RegisterAsyncPostBackControls();
#if NET35
           if (WebConfigSettings.DisablePageViewStateByDefault) {Page.EnableViewState = true; }
#endif
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current == null) { return; }

            LoadSettings();

            PopulateLabels();
            PopulateControls();
        }

        private void PopulateControls()
        {
            LoadPoll();
            if (!string.IsNullOrEmpty(poll.Question))
            {
                hplAll.Text = poll.Question;
            }
            lnkMyPollHistory.Visible = showMyPollHistoryLink && Request.IsAuthenticated;
            lblQuestion.Text = poll.Question;
            lnkAll.Text = PollResources.PollViewAll;
            lnkAll.NavigateUrl = siteSettings.SiteRoot + "/Poll/ListPoll.aspx?mid=" + ModuleId + "&pageid=" + PageId;
            lnkAll.Visible = false;
            if (Page.IsPostBack) return;
            if (Page.IsAsync) return;

            ShowVoting();
            //if (Convert.ToDateTime(poll.ActiveTo) <= DateTime.Now)
            //{
            //    ShowResult();
            //}
        }



        private void ShowVoting()
        {
            LoadPoll();

            lblMessage.Text = String.Empty;
            rblOptions.Visible = true;
            dlResults.Visible = false;
            btnBackToVote.Visible = false;
            btnShowResults.Visible = true;
            img_result.Visible = true;
            if (poll.PollGuid == Guid.Empty)
            {
                lblMessage.Text = PollResources.PollNoActivePollLabel;
                btnShowResults.Visible = false;
                img_result.Visible = false;
                return;
            }

            if (!poll.IsActive && poll.ShowResultsWhenDeactivated)
            {
                lblMessage.Text = PollResources.PollDeactivatedLabel;
                userHasVoted = true;
                ShowResult();
                return;
            }

            if (!poll.IsActive)
            {
                lblMessage.Text = PollResources.PollNoActivePollLabel;
                lblQuestion.Text = String.Empty;
                btnShowResults.Visible = false;
                img_result.Visible = false;
                return;
            }

            if (currentUser != null)
            {
                userHasVoted = poll.UserHasVoted(currentUser);
            }
            else
            {
                userHasVoted = CookieHelper.CookieExists(poll.PollGuid.ToString());
            }

            if (userHasVoted)
            {
                ShowResult();
                return;
            }

            if (!poll.AnonymousVoting && !Request.IsAuthenticated)
            {
                rblOptions.Enabled = false;
                lblMessage.Text = PollResources.PollMustLoginToVoteLabel;
            }

            if (!userHasVoted && !poll.AllowViewingResultsBeforeVoting)
            {
                btnShowResults.Visible = false;
                img_result.Visible = false;
            }

            //IDataReader reader;
            List<PollOption> pollOptions = PollOption.GetOptionsByPollGuidBy(poll.PollGuid);

            if (poll.ShowOrderNumbers)
            {
                //reader = PollOption.GetOptionsByPollGuid(poll.PollGuid);
                DataTable table = new DataTable();
                table.Columns.Add("OptionGuid");
                table.Columns.Add("Answer");
                foreach (PollOption option in pollOptions)
                {
                    table.Rows.Add(
                        option.OptionGuid.ToString(),
                        option.Order.ToString(CultureInfo.InvariantCulture) + ". " + option.Answer);
                }
                //reader.Close();

                rblOptions.DataSource = table;
                rblOptions.DataBind();
            }
            else
            {
                //reader = PollOption.GetOptionsByPollGuid(poll.PollGuid);
                rblOptions.DataSource = pollOptions;
                rblOptions.DataBind();
                //reader.Close();
            }
        }

        private void ShowResult()
        {
            //LoadPoll();
            poll = new Poll(ModuleId);

            if (currentUser != null)
            {
                userHasVoted = poll.UserHasVoted(currentUser);
            }
            else
            {
                userHasVoted = CookieHelper.CookieExists(poll.PollGuid.ToString());
            }

            if (userHasVoted) { lblVotingStatus.Text = PollResources.AlreadyVotedMessage; }

            rblOptions.Visible = false;
            dlResults.Visible = true;
            btnBackToVote.Visible = false;
            if (Request.IsAuthenticated)
            {
                btnBackToVote.Visible = !userHasVoted;
            }
            else
            {
                if (Convert.ToDateTime(poll.ActiveTo) >= DateTime.Now && userHasVoted == false)
                {
                    btnBackToVote.Visible = true;
                }
            }
            btnShowResults.Visible = false;
            lblMessage.Text = PollResources.PollTotalVotesLabel + poll.TotalVotes;
            //IDataReader reader = PollOption.GetOptionsByPollGuid(poll.PollGuid);
            List<PollOption> pollOptions = PollOption.GetOptionsByPollGuidBy(poll.PollGuid);
            dlResults.DataSource = pollOptions;
            dlResults.DataBind();
            //reader.Close();

            //rptResults.DataSource = PollOption.GetOptionsByPollGuid(poll.PollGuid);
            //rptResults.DataBind();
        }

        private void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this is where the actual voting happens

            if (String.IsNullOrEmpty(rblOptions.SelectedValue)) return;

            LoadPoll();
            userHasVoted = true;
            Guid selectedOptionGuid = new Guid(rblOptions.SelectedValue);
            PollOption selectedOption = new PollOption(selectedOptionGuid);

            Guid userGuid = Guid.Empty;

            if (currentUser != null)
            {
                userGuid = currentUser.UserGuid;
            }
            else
            {
                // Set a cookie and use it to keep anonymous polls from being too easy
                // to skew. Of course they can still do it
                if (poll.AnonymousVoting)
                {
                    CookieHelper.SetCookie(poll.PollGuid.ToString(), poll.PollGuid.ToString(), true);


                }

            }

            selectedOption.IncrementVotes(userGuid);

            ShowResult();
            pnlPollUpdate.Update();
        }

        private void dlResults_ItemCreated(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header) return;
            if (e.Item.ItemType == ListItemType.Footer) return;


            Control spnResultImage = e.Item.FindControl("spnResultImage");
            Guid optionGuid = new Guid(dlResults.DataKeys[e.Item.ItemIndex].ToString());
            PollOption option = new PollOption(optionGuid);

            PollUIHelper.AddResultBarToControl(option, resultBarColor, spnResultImage);
        }

        void rptResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header) return;
            if (e.Item.ItemType == ListItemType.Footer) return;

            Control spnResultImage = e.Item.FindControl("spnResultImage");
            HiddenField hdnID = (HiddenField)e.Item.FindControl("hdnID");
            PollOption option = new PollOption(new Guid(hdnID.Value));

            PollUIHelper.AddResultBarToControl(option, resultBarColor, spnResultImage);


        }

        private void btnBackToVote_Click(object sender, EventArgs e)
        {
            ShowVoting();
            pnlPollUpdate.Update();
        }



        private void btnShowResults_Click(object sender, EventArgs e)
        {
            ShowResult();
            pnlPollUpdate.Update();
        }

        private void LoadPoll()
        {
            if (poll == null) poll = new Poll(ModuleId);
        }




        private void RegisterAsyncPostBackControls()
        {


            if (ScriptController != null)
            {
                ScriptController.RegisterAsyncPostBackControl(btnShowResults);
                ScriptController.RegisterAsyncPostBackControl(rblOptions);
                ScriptController.RegisterAsyncPostBackControl(btnBackToVote);
            }
            else
            {
                log.Error("ScriptController was null");
            }
        }

        private void PopulateLabels()
        {
            hplAll.Text = config.TitleSetting;
            hplAll.NavigateUrl = siteSettings.SiteRoot + "/Poll/ListPoll.aspx?mid=" + ModuleId + "&pageid=" + PageId;
            btnBackToVote.Text = PollResources.PollBackToVoteButton;
            btnBackToVote.ToolTip = PollResources.PollBackToVoteToolTip;

            btnShowResults.Text = PollResources.PollShowResultsButton;
            btnShowResults.ToolTip = PollResources.PollShowResultsToolTip;


            lnkMyPollHistory.Text = PollResources.PollMyPollHistoryButton;
            lnkMyPollHistory.NavigateUrl = siteSettings.SiteRoot + "/Poll/MyPollHistory.aspx?pageid="
                + PageId.ToString(CultureInfo.InvariantCulture)
                + "&mid=" + ModuleId.ToString(CultureInfo.InvariantCulture);
            moduleTitle.Visible = false;
            if (currentUser != null)
            {
                if (currentUser.IsInRoles("Admins") || isPublish || isApprove)
                {
                    moduleTitle.Visible = true;
                }
            }


            if (IsEditable || isApprove || isPublish)
            {
                moduleTitle.EditText = PollResources.PollModuleEditText;
                moduleTitle.EditUrl = siteSettings.SiteRoot + "/Poll/PollChoose.aspx";
                moduleTitle.LiteralExtraMarkup =
                    "&nbsp;<span class='modulelinks'><a href='"
                    + SiteRoot
                    + "/Poll/managepost.aspx?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='"
                    + BlogResources.Administration + "'>"
                    + BlogResources.Administration + "</a></span>"
                    ;
            }
        }







        private void LoadSettings()
        {
            currentUser = SiteUtils.GetCurrentSiteUser();

            try
            {
                // this keeps the action from changing during ajax postback in folder based sites
                SiteUtils.SetFormAction(Page, Request.RawUrl);
            }
            catch (MissingMethodException)
            {
                //this method was introduced in .NET 3.5 SP1
            }

            showMyPollHistoryLink
                = ConfigHelper.GetBoolProperty("PollShowMyPollHistoryButtonSetting_Default", showMyPollHistoryLink);

            showMyPollHistoryLink = WebUtils.ParseBoolFromHashtable(
                Settings, "PollShowMyPollHistoryButtonSetting", showMyPollHistoryLink);

            if (Settings.Contains("ResultBarColor"))
            {
                resultBarColor = Settings["ResultBarColor"].ToString();
            }

            if (Settings.Contains("CustomCssClassSetting"))
            {
                instanceCssClass = Settings["CustomCssClassSetting"].ToString();
            }
            if (instanceCssClass.Length > 0) { pnlOuterWrap.SetOrAppendCss(instanceCssClass); }
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new PollConfiguration(moduleSettings);
            if (WebUser.IsInRoles(config.RoleApprove))
            {
                isApprove = true;
            }
            if (WebUser.IsInRoles(config.RolePublish))
            {
                isPublish = true;
            }
        }

        protected String GetOptionResultText(Object oOrder, Object oAnswer, Object oVotes)
        {
            int order = (int)oOrder;
            String answer = oAnswer as String;
            int votes = (int)oVotes;

            String orderNumber = String.Empty;
            if (poll.ShowOrderNumbers)
            {
                orderNumber = order.ToString() + ". ";
            }

            String votesText = (votes == 1) ? PollResources.PollVoteText : PollResources.PollVotesText;

            // TODO: Some pattern based resource...
            String text = orderNumber + answer + ", " + votes + " " + votesText;

            poll = new Poll(ModuleId);
            if (poll.TotalVotes != 0)
            {
                int percent = (int)((double)votes * 100 / poll.TotalVotes + 0.5);
                text += " (" + percent + " %)";
            }

            return text;
        }

        private String GetOptionText(Object oOrder, Object oAnswer)
        {
            int order = (int)oOrder;
            String answer = oAnswer as String;

            String orderNumber = String.Empty;
            if (poll.ShowOrderNumbers)
            {
                orderNumber = order.ToString() + ". ";
            }

            String text = orderNumber + answer;

            return text;
        }
    }
}

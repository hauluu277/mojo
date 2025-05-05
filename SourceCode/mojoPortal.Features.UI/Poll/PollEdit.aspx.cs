/// Author:                     Christian Fredh
/// Created:                    2007-07-25
///	Last Modified:              2013-04-09
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

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
using System.Linq;

namespace PollFeature.UI
{
    public partial class PollEdit : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PollEdit));

        private int pageId = -1;
        private int moduleId = -1;
        private mojoPortal.Business.Module currentModule = null;
        private Guid pollGuid = Guid.Empty;
        private Double timeOffset = 0;
        private TimeZoneInfo timeZone = null;
        private bool isPublish = false;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected PollConfiguration config = new PollConfiguration();
        private bool isApprove = false;
        private List<PollOption> lstOption = new List<PollOption>();
        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Load += new EventHandler(Page_Load);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnAddOption.Click += new EventHandler(btnAddOption_Click);
            this.cvOptionsLessThanTwo.ServerValidate += new ServerValidateEventHandler(cvOptionsLessThanTwo_ServerValidate);
            this.btnUp.Click += new ImageClickEventHandler(btnUp_Click);
            this.btnDown.Click += new ImageClickEventHandler(btnDown_Click);
            this.btnDeleteOption.Click += new ImageClickEventHandler(btnDeleteOption_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.btnEdit.Click += new ImageClickEventHandler(btnEdit_Click);
            this.btnActivateDeactivate.Click += new EventHandler(btnActivateDeactivate_Click);


            SuppressPageMenu();

        }

        #endregion

        private void Page_Load(object sender, EventArgs e)
        {

            SecurityHelper.DisableBrowserCache();

            LoadSettings();

            //if (!UserCanEditModule(moduleId, Poll.FeatureGuid))
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //    return;
            //}
            if (UserCanEditModule(moduleId, Poll.FeatureGuid) || isPublish || isApprove) { }
            else
            {
                SiteUtils.RedirectToEditAccessDeniedPage();
                return;
            }
            PopulateLabels();
            PopulateControls();
        }

        private void PopulateControls()
        {
            if (currentModule == null) return;

            lnkPageCrumb.Text = currentModule.ModuleTitle;

            lnkPolls.Text = string.Format(CultureInfo.InvariantCulture,
                PollResources.ChooseActivePollFormatString,
                currentModule.ModuleTitle);

            if (IsPostBack) return;
            if (Page.IsCallback) return;
            if (isPublish)
            {
                pnPublish.Visible = true;
                pnAnswerPublish.Visible = true;
            }
            if (isApprove)
            {
                pnApprove.Visible = true;
                pnPublish.Visible = true;
                pnAnswerPublish.Visible = true;
                pnAnswerApprove.Visible = true;
            }
            if (pollGuid == Guid.Empty)
            {
                //if (Session["opntions"] == null)
                //{
                Session["opntions"] = new List<PollOption>();
                //}
                btnDelete.Visible = false;
                btnActivateDeactivate.Visible = false;
                btnAddNewPoll.Visible = false;
                //if (timeZone != null)
                //{
                //    dpActiveFrom.Text = DateTimeHelper.LocalizeToCalendar(DateTime.UtcNow.ToLocalTime(timeZone).ToString("g"));
                //    dpActiveTo.Text = DateTimeHelper.LocalizeToCalendar(DateTime.UtcNow.AddYears(1).ToLocalTime(timeZone).ToString("g"));
                //}
                //else
                //{
                //dpActiveFrom.Text = DateTimeHelper.LocalizeToCalendar(DateTime.UtcNow.AddHours(timeOffset).ToString("g"));
                //dpActiveTo.Text = DateTimeHelper.LocalizeToCalendar(DateTime.UtcNow.AddYears(1).AddHours(timeOffset).ToString("g"));
                dpActiveFrom.Text = DateTime.Now.ToString();
                dpActiveTo.Text = DateTime.Now.ToString();
                //}


                //return;
            }
            else
            {
                Poll poll = new Poll(pollGuid);

                txtQuestion.Text = poll.Question;
                chkAllowViewingResultsBeforeVoting.Checked = poll.AllowViewingResultsBeforeVoting;
                chkAnonymousVoting.Checked = poll.AnonymousVoting;
                chkShowOrderNumbers.Checked = poll.ShowOrderNumbers;
                chkShowResultsWhenDeactivated.Checked = poll.ShowResultsWhenDeactivated;
                txtComment.Text = poll.Comment;
                if (isPublish)
                {
                    //pnPublish.Visible = true;
                    CboxPublish.Checked = poll.IsPublish;
                }
                if (isApprove)
                {
                    //pnPublish.Visible = true;
                    //pnApprove.Visible = true;
                    CboxPublish.Checked = poll.IsPublish;
                    cboxApprove.Checked = poll.IsApprove;

                }
                //if (timeZone != null)
                //{
                //    dpActiveFrom.Text = DateTimeHelper.LocalizeToCalendar(poll.ActiveFrom.ToLocalTime(timeZone).ToString("g"));
                //    dpActiveTo.Text = DateTimeHelper.LocalizeToCalendar(poll.ActiveTo.ToLocalTime(timeZone).ToString("g"));
                //}
                //else
                //{
                //dpActiveFrom.Text = DateTimeHelper.LocalizeToCalendar(poll.ActiveFrom.AddHours(timeOffset).ToString("g"));
                //dpActiveTo.Text = DateTimeHelper.LocalizeToCalendar(poll.ActiveTo.AddHours(timeOffset).ToString("g"));
                //}
                dpActiveFrom.Text = poll.ActiveFrom.ToString();
                dpActiveTo.Text = poll.ActiveTo.ToString();

                List<PollOption> pollOptions = PollOption.GetOptionsByPollGuid(pollGuid);
                ListItem li;
                foreach (PollOption option in pollOptions)
                {
                    if (option.IsApprove && option.IsPublish)
                    {
                        li = new ListItem(option.Answer + "(Duyệt, Hiển thị)", option.OptionGuid.ToString());
                        lbOptions.Items.Add(li);
                    }
                    else if (!option.IsApprove && !option.IsPublish)
                    {
                        li = new ListItem(option.Answer + "(Chưa duyệt, Chưa hiển thị)", option.OptionGuid.ToString());
                        lbOptions.Items.Add(li);
                    }
                    else if (option.IsApprove && !option.IsPublish)
                    {
                        li = new ListItem(option.Answer + "(Duyệt, Chưa hiển thị)", option.OptionGuid.ToString());
                        lbOptions.Items.Add(li);
                    }
                    else if (!option.IsApprove && option.IsPublish)
                    {
                        li = new ListItem(option.Answer + "(Chưa duyệt, Hiển thị)", option.OptionGuid.ToString());
                        lbOptions.Items.Add(li);
                    }
                    PollOption po = new PollOption();
                    po.Answer = option.Answer;
                    po.IsApprove = option.IsApprove;
                    po.IsPublish = option.IsPublish;
                    po.OptionGuid = option.OptionGuid;
                    po.Order = option.Order;
                    lstOption.Add(po);
                }
                Session["options"] = lstOption;

                if (poll.Activated)
                {
                    btnActivateDeactivate.Text = PollResources.PollEditDeactivateButton;
                    btnActivateDeactivate.ToolTip = PollResources.PollEditDeactivateToolTip;
                    btnActivateDeactivate.CommandName = "Deactivate";
                }
                else
                {
                    btnActivateDeactivate.Text = PollResources.PollEditActivateButton;
                    btnActivateDeactivate.ToolTip = PollResources.PollEditActivateToolTip;
                    btnActivateDeactivate.CommandName = "Activate";
                }
                btnActivateDeactivate.CommandArgument = poll.PollGuid.ToString();
            }

            lblStartDeactivated.Visible = false;
            chkStartDeactivated.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("poll");
            if (Page.IsValid)
            {
                if (Session["options"] == null) return;
                Poll poll = new Poll(pollGuid);
                poll.SiteGuid = siteSettings.SiteGuid;
                poll.Question = txtQuestion.Text;
                poll.AnonymousVoting = chkAnonymousVoting.Checked;
                poll.AllowViewingResultsBeforeVoting = chkAllowViewingResultsBeforeVoting.Checked;
                poll.ShowOrderNumbers = chkShowOrderNumbers.Checked;
                poll.ShowResultsWhenDeactivated = chkShowResultsWhenDeactivated.Checked;
                if (isApprove)
                {
                    poll.IsPublish = CboxPublish.Checked;
                    poll.IsApprove = cboxApprove.Checked;
                }
                else if (isPublish)
                {
                    poll.IsPublish = CboxPublish.Checked;
                    poll.IsApprove = poll.IsApprove;
                }
                else
                {
                    poll.IsApprove = poll.IsApprove;
                    poll.IsPublish = poll.IsPublish;
                }
                poll.SiteID = SiteId;
                poll.Comment = txtComment.Text;
                //if (dpActiveFrom.Text.Length > 0 && poll.ActiveFrom >= DateTime.Now)
                //{
                //    // You can't change date if poll has started.

                //    // TODO: promt user if invalid format/date

                //    DateTime activeFrom;
                //    DateTime.TryParse(dpActiveFrom.Text, out activeFrom);

                //    if (timeZone != null)
                //    {
                //        activeFrom = activeFrom.ToUtc(timeZone);
                //    }
                //    else
                //    {
                //        activeFrom = activeFrom.AddHours(-timeOffset);
                //    }

                //    poll.ActiveFrom = activeFrom;
                //}
                poll.ActiveFrom = Convert.ToDateTime(dpActiveFrom.Text);
                poll.ActiveTo = Convert.ToDateTime(dpActiveTo.Text);
                //if (dpActiveTo.Text.Length > 0)
                //{
                //    // TODO: promt user if invalid format/date
                //    DateTime activeTo;
                //    DateTime.TryParse(dpActiveTo.Text, out activeTo);

                //    if (timeZone != null)
                //    {
                //        activeTo = activeTo.ToUtc(timeZone);
                //    }
                //    else
                //    {
                //        activeTo = activeTo.AddHours(-timeOffset);
                //    }

                //    // Make time 23:59:59
                //    //activeTo = activeTo.AddHours(23).AddMinutes(59).AddSeconds(59);

                //    // You can't change to past date.
                //    if (activeTo >= DateTime.Now)
                //    {
                //        poll.ActiveTo = activeTo;
                //    }
                //}

                if (chkStartDeactivated.Checked)
                {
                    // This only happens when new poll.
                    poll.Deactivate();
                }
                else
                {
                    poll.Activate();
                }

                poll.Save();
                //PollOption.DeleteByPollGuid(poll.PollGuid);
                // Get options
                PollOption option;
                int order = 1;
                lstOption = (List<PollOption>)Session["options"];
                foreach (ListItem item in lbOptions.Items)
                {
                    //if (item.Text == item.Value)
                    //{
                    //    option = new PollOption();
                    //}
                    //else
                    //{
                    //    if (item.Value.Length == 36)
                    //    {
                    //        option = new PollOption(new Guid(item.Value));
                    //    }
                    //    else
                    //    {
                    //        option = new PollOption();
                    //    }
                    //}
                    PollOption op = lstOption.Where(x => x.OptionGuid.Equals(Guid.Parse(item.Value))).FirstOrDefault();
                    if (op != null)
                    {
                        option = new PollOption(new Guid(item.Value));
                        //option.OptionGuid = Guid.NewGuid();
                        option.IsApprove = op.IsApprove;
                        option.IsPublish = op.IsPublish;
                        option.PollGuid = poll.PollGuid;
                        option.Answer = op.Answer;
                        option.Order = order++;
                        option.Save();
                    }
                }

                WebUtils.SetupRedirect(this,
                    siteSettings.SiteRoot + "/Poll/ManagePost.aspx"
                    + "?pageid=" + pageId.ToInvariantString()
                    + "&mid=" + moduleId.ToInvariantString()
                    );

            }
        }



        private void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            Poll poll = new Poll(new Guid(btnActivateDeactivate.CommandArgument));
            if (btnActivateDeactivate.CommandName == "Activate")
            {
                poll.Activate();
                poll.Save();
                WebUtils.SetupRedirect(this,
                    siteSettings.SiteRoot + "/Poll/PollEdit.aspx?PollGuid=" + pollGuid.ToString()
                    + "&pageid=" + pageId.ToInvariantString()
                    + "&mid=" + moduleId.ToInvariantString()
                    );
            }
            else if (btnActivateDeactivate.CommandName == "Deactivate")
            {
                poll.Deactivate();
                poll.Save();
                WebUtils.SetupRedirect(this,
                    siteSettings.SiteRoot + "/Poll/PollEdit.aspx?PollGuid=" + pollGuid.ToString()
                    + "&pageid=" + pageId.ToInvariantString()
                    + "&mid=" + moduleId.ToInvariantString()
                    );
            }
        }

        private void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (lbOptions.SelectedItem == null) return;
            if (Session["options"] == null) return;
            lstOption = (List<PollOption>)Session["options"];
            PollOption po = lstOption.Where(x => x.OptionGuid.Equals(Guid.Parse(lbOptions.SelectedValue))).FirstOrDefault();
            if (po != null)
            {
                if (isPublish)
                {
                    cboxAnswerPublish.Checked = po.IsPublish;
                }
                if (isApprove)
                {
                    cboxAnswerApprove.Checked = po.IsApprove;
                    cboxAnswerPublish.Checked = po.IsPublish;
                }
                String itemText = lbOptions.SelectedItem.Text;
                String itemValue = lbOptions.SelectedItem.Value;

                txtNewOption.Text = po.Answer;
                btnAddOption.CommandArgument = itemValue;
                btnAddOption.Text = PollResources.PollEditOptionsSaveButton;
            }
            else
            {
                return;
            }
        }

        private void btnAddOption_Click(object sender, EventArgs e)
        {
            txtNewOption.Text = txtNewOption.Text.Trim();

            if (txtNewOption.Text.Length == 0) return;
            //if (Session["options"] == null) return;
            lstOption = (List<PollOption>)Session["options"];
            if (lstOption == null) lstOption = new List<PollOption>();
            if (String.IsNullOrEmpty(btnAddOption.CommandArgument))
            {
                if (lbOptions.Items.FindByText(txtNewOption.Text) != null) return;
                Guid g = Guid.NewGuid();
                //ListItem li = new ListItem(txtNewOption.Text, g.ToString());
                //lbOptions.Items.Add(li);
                ListItem li;
                PollOption op = new PollOption();

                if (isPublish)
                {
                    op.IsPublish = cboxAnswerPublish.Checked;
                }
                if (isApprove)
                {
                    op.IsApprove = cboxAnswerApprove.Checked;
                    op.IsPublish = cboxAnswerPublish.Checked;
                }

                if (op.IsApprove && op.IsPublish)
                {
                    li = new ListItem(txtNewOption.Text + "(Duyệt, Hiển thị)", g.ToString());
                    lbOptions.Items.Add(li);
                }
                else if (!op.IsApprove && !op.IsPublish)
                {
                    li = new ListItem(txtNewOption.Text + "(Chưa duyệt, Chưa hiển thị)", g.ToString());
                    lbOptions.Items.Add(li);
                }
                else if (op.IsApprove && !op.IsPublish)
                {
                    li = new ListItem(txtNewOption.Text + "(Duyệt, Chưa hiển thị)", g.ToString());
                    lbOptions.Items.Add(li);
                }
                else if (!op.IsApprove && op.IsPublish)
                {
                    li = new ListItem(txtNewOption.Text + "(Chưa duyệt, Hiển thị)", g.ToString());
                    lbOptions.Items.Add(li);
                }
                op.OptionGuid = g;
                op.Answer = txtNewOption.Text;
                lstOption.Add(op);
                Session["options"] = lstOption;
            }
            else
            {
                PollOption po = lstOption.Where(x => x.OptionGuid.Equals(Guid.Parse(btnAddOption.CommandArgument))).FirstOrDefault();

                ListItem itemToEdit = lbOptions.Items.FindByValue(btnAddOption.CommandArgument);
                if (po == null) return;

                PollOption op = new PollOption();
                string text = txtNewOption.Text;
                if (isPublish)
                {
                    op.IsPublish = cboxAnswerPublish.Checked;
                }
                if (isApprove)
                {
                    op.IsPublish = cboxAnswerPublish.Checked;
                    op.IsApprove = cboxAnswerApprove.Checked;
                }
                op.OptionGuid = po.OptionGuid;
                op.Answer = txtNewOption.Text;
                op.IsApprove = cboxAnswerApprove.Checked;
                op.IsPublish = cboxAnswerPublish.Checked;
                if (op.IsApprove && op.IsPublish)
                {
                    text = txtNewOption.Text + "(Duyệt, Hiển thị)";
                }
                else if (!op.IsApprove && !op.IsPublish)
                {
                    text = txtNewOption.Text + "(Chưa duyệt, Chưa hiển thị)";
                }
                else if (op.IsApprove && !op.IsPublish)
                {
                    text = txtNewOption.Text + "(Duyệt, Chưa hiển thị)";
                }
                else if (!op.IsApprove && op.IsPublish)
                {
                    text = txtNewOption.Text + "(Chưa duyệt, Hiển thị)";
                }
                if (itemToEdit != null)
                {
                    itemToEdit.Text = text;
                }
                lstOption.Remove(po);
                lstOption.Add(op);
                Session["options"] = lstOption;
                btnAddOption.CommandArgument = null;
                btnAddOption.Text = PollResources.PollEditOptionsAddButton;
            }
            cboxAnswerApprove.Checked = false;
            cboxAnswerPublish.Checked = false;
            txtNewOption.Text = String.Empty;
        }

        private void cvOptionsLessThanTwo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (lbOptions.Items.Count >= 2);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (pollGuid != Guid.Empty)
            {
                Poll poll = new Poll(pollGuid);
                poll.Delete();

                WebUtils.SetupRedirect(this,
                    siteSettings.SiteRoot + "/Poll/ManagePost.aspx"
                    + "?pageid=" + pageId.ToInvariantString()
                    + "&mid=" + moduleId.ToInvariantString()
                    );
            }
            txtNewOption.Text = "";
            cboxAnswerPublish.Checked = false;
            cboxAnswerApprove.Checked = false;
        }

        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, PollResources.PollEditLabel);
            heading.Text = PollResources.PollEditLabel;
            if (pollGuid == Guid.Empty)
            {
                heading.Text = PollResources.PollAddNewLabel;
                Title = SiteUtils.FormatPageTitle(siteSettings, PollResources.PollAddNewLabel);
            }

            lnkPageCrumb.Text = CurrentPage.PageName;
            lnkPageCrumb.NavigateUrl = SiteUtils.GetCurrentPageUrl();
            lnkManagePoll.Text = "Administration";
            lnkManagePoll.NavigateUrl = siteSettings.SiteRoot
                    + "/Poll/managepost.aspx?pageid=" + pageId.ToInvariantString()
                    + "&mid=" + moduleId.ToInvariantString()
                    ;
            lnkPolls.NavigateUrl = SiteRoot + "/Poll/PollChoose.aspx?pageid="
                + pageId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();

            btnAddNewPoll.Visible = false;
            //btnAddNewPoll.Text = PollResources.PollEditAddNewPollLabel;
            //btnAddNewPoll.ToolTip = PollResources.PollEditAddNewPollToolTip;

            btnViewPolls.Visible = false;
            //btnViewPolls.Text = PollResources.PollEditViewPollsLabel;
            //btnViewPolls.ToolTip = PollResources.PollEditViewPollsToolTip;
            if (isApprove == true)
            {
                txtComment.Enabled = true;
            }
            btnSave.Text = PollResources.PollEditSaveButton;
            btnSave.ToolTip = PollResources.PollEditSaveToolTip;

            btnAddOption.Text = PollResources.PollEditOptionsAddButton;
            btnAddOption.ToolTip = PollResources.PollEditOptionsAddToolTip;

            btnDelete.Text = PollResources.PollEditDeleteButton;
            btnDelete.ToolTip = PollResources.PollEditDeleteToolTip;
            UIHelper.AddConfirmationDialog(btnDelete, PollResources.PollEditDeleteConfirmMessage);

            btnUp.AlternateText = PollResources.PollEditOptionsUpAlternateText;
            btnUp.ToolTip = PollResources.PollEditOptionsUpAlternateText;
            btnUp.ImageUrl = ImageSiteRoot + "/Data/SiteImages/up.gif";

            btnDown.AlternateText = PollResources.PollEditOptionsDownAlternateText;
            btnDown.ToolTip = PollResources.PollEditOptionsDownAlternateText;
            btnDown.ImageUrl = ImageSiteRoot + "/Data/SiteImages/dn.gif";

            btnEdit.AlternateText = PollResources.PollEditOptionsEditAlternateText;
            btnEdit.ToolTip = PollResources.PollEditOptionsEditAlternateText;
            btnEdit.ImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;

            btnDeleteOption.AlternateText = PollResources.PollEditOptionsDeleteAlternateText;
            btnDeleteOption.ToolTip = PollResources.PollEditOptionsDeleteAlternateText;
            btnDeleteOption.ImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            UIHelper.AddConfirmationDialog(btnDeleteOption, PollResources.PollEditOptionsDeleteConfirmMessage);

            chkAllowViewingResultsBeforeVoting.ToolTip = PollResources.PollEditAllowViewingResultsBeforeVotingToolTip;
            chkAnonymousVoting.ToolTip = PollResources.PollEditAnonymousVotingToolTip;
            chkShowOrderNumbers.ToolTip = PollResources.PollEditShowOrderNumbersToolTip;
            chkShowResultsWhenDeactivated.ToolTip = PollResources.PollEditShowResultsWhenDeactivatedToolTip;
            chkStartDeactivated.ToolTip = PollResources.PollEditStartDeactivatedToolTip;

            cvOptionsLessThanTwo.ErrorMessage = PollResources.PollEditLessThanTwoOptionsErrorMesssage;
            reqQuestion.ErrorMessage = PollResources.PollEditQuestionEmptyErrorMessage;
        }

        private void LoadSettings()
        {
            timeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            pollGuid = WebUtils.ParseGuidFromQueryString("PollGuid", Guid.Empty);

            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);


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

        private void btnDown_Click(Object sender, ImageClickEventArgs e)
        {
            if (lbOptions.SelectedItem == null) return;
            if (lbOptions.SelectedIndex == lbOptions.Items.Count - 1) return;

            ListItem selectedItem = lbOptions.SelectedItem;
            ListItem swapItem = lbOptions.Items[lbOptions.SelectedIndex + 1];

            String tmpText = selectedItem.Text;
            String tmpValue = selectedItem.Value;

            selectedItem.Text = swapItem.Text;
            selectedItem.Value = swapItem.Value;

            swapItem.Text = tmpText;
            swapItem.Value = tmpValue;

            lbOptions.SelectedIndex++;
        }

        private void btnUp_Click(Object sender, ImageClickEventArgs e)
        {
            if (lbOptions.SelectedItem == null) return;
            if (lbOptions.SelectedIndex == 0) return;

            ListItem selectedItem = lbOptions.SelectedItem;
            ListItem swapItem = lbOptions.Items[lbOptions.SelectedIndex - 1];

            String tmpText = selectedItem.Text;
            String tmpValue = selectedItem.Value;

            selectedItem.Text = swapItem.Text;
            selectedItem.Value = swapItem.Value;

            swapItem.Text = tmpText;
            swapItem.Value = tmpValue;

            lbOptions.SelectedIndex--;
        }

        private void btnDeleteOption_Click(Object sender, ImageClickEventArgs e)
        {
            if (lbOptions.SelectedItem == null) return;

            // TODO: What should happen if user deletes options so only 0 or 1 are left,
            // and then presses cancel or something? Better if nothing happens before 
            // pressing Save but how to store deleted options for later?

            // If Text == Value option hasn't been saved yet, just remove it from list.
            if (lbOptions.SelectedItem.Text != lbOptions.SelectedItem.Value)
            {
                Guid optionGuid = new Guid(lbOptions.SelectedItem.Value);
                PollOption option = new PollOption(optionGuid);
                option.Delete();
            }

            lbOptions.Items.Remove(lbOptions.SelectedItem);
        }
    }
}

using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using Resources;

namespace mojoPortal.Web.UI
{
    public class ModuleTitleControlCustom : WebControl, INamingContainer
    {
        #region Constructors

        public ModuleTitleControlCustom()
        {
            //if (this.Site != null && this.Site.DesignMode) 
            //{
            //    this.Visible = false;
            //    return; 
            //}
            if (HttpContext.Current == null) { return; }

            EnsureChildControls();


        }

        #endregion

        #region Control Declarations

        protected Literal litModuleTitle;
        protected HyperLink lnkModuleSettings;
        protected HyperLink lnkModuleEdit;
        protected ImageButton ibPostDraftContentForApproval;
        protected ImageButton ibApproveContent;
        protected HyperLink lnkRejectContent;
        protected ImageButton ibCancelChanges;
        protected ClueTipHelpLink statusLink;

        #endregion
        /// <summary>
        /// TinLT: "Use this to set header in Center Column same as Left and Right Column, combo with UIHelperCustom.cs in mojoPortal.Web.Framework"
        /// </summary>
        private string literalExtraMarkup = string.Empty;
        private string publishButton = string.Empty;//TinLT: "Publish Button"
        private string featureSettingsButton = string.Empty;//TinLT: "Feature Settings Button"
        private bool disabledModuleSettingsLink = false;
        private bool showPublishButton = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowPublishButton"]);
        private bool showFeatureSettingsButton = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowFeatureSettingsButton"]);
        private Module module = null;

        private string editUrl = string.Empty;
        private string editText = string.Empty;
        //private bool useHTag = true;
        private bool canEdit = false;
        private bool forbidModuleSettings = false;
        private bool showEditLinkOverride = false;
        private bool enableWorkflow = false;
        private SiteModuleControl siteModule = null;
        private ContentWorkflowStatus workflowStatus = ContentWorkflowStatus.None;
        private string siteRoot = string.Empty;
        private bool isAdminEditor = false;
        private bool useHeading = true;
        private string header = string.Empty;
        private string columnId = UIHelper.CenterColumnId;
        private string artHeader = UIHelper.ArtisteerBlockHeader;
        private string artHeadingCss = UIHelper.ArtPostHeader;
        private int loadedModuleId = -1;
        private int loadedPageId = -1;
        private bool useJQueryUI = false;

        public bool UseJQueryUI
        {
            get { return useJQueryUI; }
            set { useJQueryUI = value; }

        }

        #region Public Properties

        public Module ModuleInstance
        {
            get { return module; }
            set { module = value; }
        }

        public int LoadedModuleId
        {
            get { return loadedModuleId; }
            set { loadedModuleId = value; }
        }

        public int LoadedPageId
        {
            get { return loadedPageId; }
            set { loadedPageId = value; }
        }

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        public string LiteralExtraMarkup
        {
            get { return literalExtraMarkup; }
            set { literalExtraMarkup = value; }
        }

        public string EditUrl
        {
            get { return editUrl; }
            set { editUrl = value; }


        }

        public string EditText
        {
            get { return editText; }
            set { editText = value; }

        }

        public bool UseHeading
        {
            get { return useHeading; }
            set { useHeading = value; }

        }

        public bool DisabledModuleSettingsLink
        {
            get { return disabledModuleSettingsLink; }
            set { disabledModuleSettingsLink = value; }
        }

        public bool CanEdit
        {
            get { return canEdit; }
            set { canEdit = value; }

        }

        public bool IsAdminEditor
        {
            get { return isAdminEditor; }
            set { isAdminEditor = value; }

        }

        public bool ShowEditLinkOverride
        {
            get { return showEditLinkOverride; }
            set { showEditLinkOverride = value; }

        }

        public ContentWorkflowStatus WorkflowStatus
        {
            get { return workflowStatus; }
            set { workflowStatus = value; }
        }

        public bool ShowPublishButton
        {
            get { return showPublishButton; }
            set { showPublishButton = value; }
        }

        public bool ShowFeatureSettingsButton
        {
            get { return showFeatureSettingsButton; }
            set { showFeatureSettingsButton = value; }
        }


        private bool renderArtisteer = false;

        public bool RenderArtisteer
        {
            get { return renderArtisteer; }
            set { renderArtisteer = value; }
        }

        private bool useLowerCaseArtisteerClasses = false;

        public bool UseLowerCaseArtisteerClasses
        {
            get { return useLowerCaseArtisteerClasses; }
            set { useLowerCaseArtisteerClasses = value; }
        }

        #endregion

        private SiteModuleControl GetParentAsSiteModelControl(Control child)
        {
            if (HttpContext.Current == null) { return null; }

            if (child.Parent == null)
            {
                return null;
            }
            else if (child.Parent is SiteModuleControl)
            {
                return child.Parent as SiteModuleControl;
            }
            else
            {
                return GetParentAsSiteModelControl(child.Parent);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {

            if (HttpContext.Current == null)
            {
                writer.Write("[" + this.ID + "]");
                return;
            }
            else
            {
                if ((useHeading) && (renderArtisteer))
                {

                    writer.Write("<div class=\"" + artHeader + "\">\n");

                    if ((artHeader == UIHelperCustom.ArtisteerBlockHeader) || (artHeader == UIHelperCustom.ArtisteerBlockHeaderLower))
                    {
                        writer.Write("<div class=\"l\"></div>");
                        writer.Write("<div class=\"m\">");
                        writer.Write("<div class=\"t\">");
                    }

                }
                else if (useJQueryUI)
                {
                    writer.Write("<div class=\"ui-widget-header ui-corner-top\">");
                }

                if (module != null)
                {
                    writer.Write("<a id='module" + module.ModuleId.ToString(CultureInfo.InvariantCulture) + "'></a>");
                }

                string headingTag = WebConfigSettings.ModuleTitleTag;

                if ((useHeading) && (headingTag.Length > 0))
                {
                    writer.WriteBeginTag(headingTag);
                    writer.WriteAttribute("class", artHeadingCss + " moduletitle");
                    writer.Write(HtmlTextWriter.TagRightChar);
                }


                litModuleTitle.RenderControl(writer);

                if (CanEdit)
                {
                    if (!forbidModuleSettings)
                    {
                        writer.Write(HtmlTextWriter.SpaceChar);
                        lnkModuleSettings.RenderControl(writer);
                    }

                    if (ibCancelChanges != null && ibCancelChanges.Visible)
                    {
                        writer.Write(HtmlTextWriter.SpaceChar);
                        ibCancelChanges.RenderControl(writer);
                    }

                    if (ibPostDraftContentForApproval != null && ibPostDraftContentForApproval.Visible)
                    {
                        writer.Write(HtmlTextWriter.SpaceChar);
                        ibPostDraftContentForApproval.RenderControl(writer);
                    }

                    if (lnkRejectContent != null && lnkRejectContent.Visible)
                    {
                        writer.Write(HtmlTextWriter.SpaceChar);
                        lnkRejectContent.RenderControl(writer);
                    }

                    if (ibApproveContent != null && ibApproveContent.Visible)
                    {
                        writer.Write(HtmlTextWriter.SpaceChar);
                        ibApproveContent.RenderControl(writer);
                    }

                    if (statusLink != null && statusLink.Visible)
                    {
                        writer.Write(HtmlTextWriter.SpaceChar);
                        statusLink.ToolTip = Resource.WorkflowStatus;
                        statusLink.RenderControl(writer);
                    }
                }

                if (
                    (lnkModuleEdit != null)
                    && (EditUrl != null)
                    && (EditText != null)
                    )
                {
                    writer.Write(HtmlTextWriter.SpaceChar);
                    lnkModuleEdit.RenderControl(writer);
                }

                //TinLT: "Render publish link"
                if (publishButton.Length > 0 && showPublishButton)
                {
                    writer.Write(publishButton);
                }
                if (featureSettingsButton.Length > 0 && showFeatureSettingsButton)
                {
                    writer.Write(featureSettingsButton);
                }
                //TinLT: "End render publish link"

                if (literalExtraMarkup.Length > 0)
                {
                    writer.Write(literalExtraMarkup);
                }

                if ((useHeading) && (headingTag.Length > 0))
                {
                    writer.WriteEndTag(headingTag);
                }

                if ((useHeading) && (renderArtisteer))
                {
                    writer.Write("</div>");
                    if ((artHeader == UIHelperCustom.ArtisteerBlockHeader) || (artHeader == UIHelperCustom.ArtisteerBlockHeaderLower))
                    {
                        writer.Write("</div>");
                        writer.Write("<div class=\"r\"></div>");
                        writer.Write("<div class=\"r2\"></div>");
                        writer.Write("</div>");
                    }
                }
                else if (useJQueryUI)
                {
                    writer.Write("</div>");
                }


            }


        }

        void ibApproveContent_Click(object sender, ImageClickEventArgs e)
        {
            SiteModuleControl siteModule = GetParentAsSiteModelControl(this);
            if (siteModule == null) { return; }
            if (!(siteModule is IWorkflow)) { return; }

            IWorkflow workflow = siteModule as IWorkflow;
            workflow.Approve();

        }

        protected void ibPostDraftContentForApproval_Click(object sender, ImageClickEventArgs e)
        {
            SiteModuleControl siteModule = GetParentAsSiteModelControl(this);
            if (siteModule == null) { return; }
            if (!(siteModule is IWorkflow)) { return; }

            IWorkflow workflow = siteModule as IWorkflow;
            workflow.SubmitForApproval();

        }

        protected void ibCancelChanges_Click(object sender, ImageClickEventArgs e)
        {
            SiteModuleControl siteModule = GetParentAsSiteModelControl(this);
            if (siteModule == null) { return; }
            if (!(siteModule is IWorkflow)) { return; }

            IWorkflow workflow = siteModule as IWorkflow;
            workflow.CancelChanges();

        }



        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);
            if (HttpContext.Current == null) { return; }

            if ((useHeading) && (renderArtisteer))
            {
                columnId = this.GetColumnId();

                if (useLowerCaseArtisteerClasses)
                {
                    artHeader = UIHelperCustom.ArtisteerPostMetaHeaderLower;
                    artHeadingCss = UIHelperCustom.ArtPostHeaderLower;

                }

                switch (columnId)
                {
                    case UIHelper.LeftColumnId:
                    case UIHelper.RightColumnId:

                        if (useLowerCaseArtisteerClasses)
                        {
                            if ((artHeader == UIHelperCustom.ArtisteerPostMetaHeader) || (artHeader == UIHelperCustom.ArtisteerPostMetaHeaderLower))
                            {
                                artHeader = UIHelperCustom.ArtisteerBlockHeaderLower;
                            }
                        }
                        else
                        {
                            if (artHeader == UIHelperCustom.ArtisteerPostMetaHeader)
                            {
                                artHeader = UIHelperCustom.ArtisteerBlockHeader;
                            }
                        }

                        artHeadingCss = string.Empty;

                        break;

                    case UIHelper.CenterColumnId:
                    default:


                        if (useLowerCaseArtisteerClasses)
                        {
                            if ((artHeader == UIHelperCustom.ArtisteerPostMetaHeader) || (artHeader == UIHelperCustom.ArtisteerPostMetaHeaderLower))
                            {
                                artHeader = UIHelperCustom.ArtisteerBlockHeaderLower;
                            }
                        }
                        else
                        {
                            if (artHeader == UIHelperCustom.ArtisteerPostMetaHeader)
                            {
                                artHeader = UIHelperCustom.ArtisteerBlockHeader;
                            }
                        }

                        artHeadingCss = string.Empty;

                        break;

                }
            }

            Initialize();


        }

        private void Initialize()
        {
            if (HttpContext.Current == null) { return; }



            siteModule = GetParentAsSiteModelControl(this);

            bool useTextLinksForFeatureSettings = true;
            mojoBasePage basePage = Page as mojoBasePage;
            if (basePage != null)
            {
                useTextLinksForFeatureSettings = basePage.UseTextLinksForFeatureSettings;
            }

            if (siteModule != null)
            {
                module = siteModule.ModuleConfiguration;
                CanEdit = siteModule.IsEditable;
                enableWorkflow = siteModule.EnableWorkflow;
                forbidModuleSettings = siteModule.ForbidModuleSettings;

            }

            if (module != null)
            {
                if (module.ShowTitle)
                {
                    litModuleTitle.Text = module.ModuleTitle.Contains("</span>") &&
                                          !module.ModuleTitle.Contains("script")
                                              ? module.ModuleTitle
                                              : Page.Server.HtmlEncode(module.ModuleTitle);
                }
                else
                {
                    useHeading = false;
                }

                if (CanEdit)
                {
                    siteRoot = SiteUtils.GetNavigationSiteRoot();
                    publishButton = "<a class='modulepublishlink' href=" + siteRoot + "/Admin/PublishContent.aspx?mid=" + module.ModuleId.ToInvariantString() + "><img src='" + Page.ResolveUrl("~/Data/Icon16x16/YellowPin.png") + "' alt='Publish' title='Publish'></img></a>";
                    featureSettingsButton = "<a class='featuresettingslink' href=" + siteRoot + "/Admin/FeatureSettings.aspx?mid=" + module.ModuleId.ToInvariantString() + "><img src='" + Page.ResolveUrl("~/Data/Icon16x16/MagicWand.png") + "' alt='Settings' title='Feature Settings'></img></a>";
                    if (!disabledModuleSettingsLink)
                    {
                        lnkModuleSettings.Visible = true;
                        lnkModuleSettings.Text = Resource.SettingsLink;
                        lnkModuleSettings.ToolTip = Resource.ModuleEditSettings;

                        if (!useTextLinksForFeatureSettings)
                        {
                            lnkModuleSettings.ImageUrl = Page.ResolveUrl("~/Data/SiteImages/" + WebConfigSettings.EditPropertiesImage);
                        }
                        else
                        {
                            // if its a text link make it small like the edit link
                            lnkModuleSettings.CssClass = "ModuleEditLink";
                        }



                        lnkModuleSettings.NavigateUrl = siteRoot
                            + "/Admin/ModuleSettings.aspx?mid=" + module.ModuleId.ToInvariantString()
                            + "&pageid=" + module.PageId.ToInvariantString();

                        if ((enableWorkflow) && (siteModule != null) && (siteModule is IWorkflow))
                        {
                            SetupWorkflowControls();

                        }

                    }

                }

                if (
                    ((CanEdit) || (ShowEditLinkOverride))
                    && ((EditText != null) && (editUrl.Length > 0)))
                {

                    lnkModuleEdit.Text = EditText;
                    if (this.ToolTip.Length > 0)
                    {
                        lnkModuleEdit.ToolTip = this.ToolTip;
                    }
                    else
                    {
                        lnkModuleEdit.ToolTip = EditText;
                    }
                    if (!loadedModuleId.Equals(-1) && !loadedPageId.Equals(-1))
                    {
                        lnkModuleEdit.NavigateUrl = EditUrl
                            + "?pageid=" + loadedPageId.ToInvariantString()
                            + "&mid=" + loadedModuleId.ToInvariantString();
                    }
                    else
                    {
                        lnkModuleEdit.NavigateUrl = EditUrl
                            + "?pageid=" + module.PageId.ToInvariantString()
                            + "&mid=" + module.ModuleId.ToInvariantString();
                    }

                    if (!useTextLinksForFeatureSettings)
                    {
                        lnkModuleEdit.ImageUrl = Page.ResolveUrl("~/Data/SiteImages/" + WebConfigSettings.EditContentImage);
                    }

                }

            }
            else
            {
                litModuleTitle.Text = header;
            }

        }

        private void SetupWorkflowControls()
        {
            if (HttpContext.Current == null) { return; }

            if (siteModule == null) { return; }
            if (module == null) { return; }


            CmsPage cmsPage = this.Page as CmsPage;
            if ((cmsPage != null) && (cmsPage.ViewMode == PageViewMode.WorkInProgress))
            {
                switch (workflowStatus)
                {
                    case ContentWorkflowStatus.Draft:

                        ibPostDraftContentForApproval.ImageUrl = Page.ResolveUrl(WebConfigSettings.RequestApprovalImage);
                        ibPostDraftContentForApproval.ToolTip = Resource.RequestApprovalToolTip;
                        ibPostDraftContentForApproval.Visible = true;
                        statusLink.HelpKey = "workflowstatus-draft-help";
                        break;

                    case ContentWorkflowStatus.AwaitingApproval:

                        //if (WebUser.IsAdminOrContentAdminOrContentPublisher)
                        if (
                            (cmsPage.CurrentPage != null)
                            && (
                            (isAdminEditor || WebUser.IsInRoles(cmsPage.CurrentPage.EditRoles)) || (WebUser.IsInRoles(this.module.AuthorizedEditRoles))
                            )
                            )
                        {
                            //add in the reject and approve links:                                            
                            ibApproveContent.ImageUrl = Page.ResolveUrl(WebConfigSettings.ApproveContentImage);
                            ibApproveContent.Visible = true;
                            ibApproveContent.ToolTip = Resource.ApproveContentToolTip;

                            lnkRejectContent.NavigateUrl =
                                siteRoot
                                + "/Admin/RejectContent.aspx?mid=" + module.ModuleId.ToInvariantString()
                                + "&pageid=" + module.PageId.ToInvariantString();

                            lnkRejectContent.ImageUrl = Page.ResolveUrl(WebConfigSettings.RejectContentImage);
                            lnkRejectContent.ToolTip = Resource.RejectContentToolTip;
                            lnkRejectContent.Visible = true;
                        }

                        statusLink.HelpKey = "workflowstatus-awaitingapproval-help";

                        break;

                    case ContentWorkflowStatus.ApprovalRejected:
                        statusLink.HelpKey = "workflowstatus-rejected-help";
                        break;


                }

                if (
                    (workflowStatus != ContentWorkflowStatus.Cancelled)
                    && (workflowStatus != ContentWorkflowStatus.Approved)
                    && (workflowStatus != ContentWorkflowStatus.None)
                    )
                {
                    //allow changes to be cancelled:                                            
                    ibCancelChanges.ImageUrl = Page.ResolveUrl(WebConfigSettings.CancelContentChangesImage);
                    ibCancelChanges.ToolTip = Resource.CancelChangesToolTip;
                    ibCancelChanges.Visible = true;
                }

            }
        }


        protected override void CreateChildControls()
        {
            if (HttpContext.Current == null) { return; }

            litModuleTitle = new Literal();
            //this.Controls.Add(litModuleTitle);
            lnkModuleSettings = new HyperLink();
            lnkModuleSettings.CssClass = "modulesettingslink";
            //this.Controls.Add(lnkModuleSettings);

            lnkModuleEdit = new HyperLink();
            //this.Controls.Add(lnkModuleEdit);
            lnkModuleEdit.CssClass = "ModuleEditLink";
            lnkModuleEdit.SkinID = "plain";


            ibPostDraftContentForApproval = new ImageButton();
            ibPostDraftContentForApproval.ID = "lbPostDraftContentForApproval";
            ibPostDraftContentForApproval.CssClass = "ModulePostDraftForApprovalLink";
            ibPostDraftContentForApproval.SkinID = "plain";
            ibPostDraftContentForApproval.Visible = false;
            ibPostDraftContentForApproval.Click += new ImageClickEventHandler(ibPostDraftContentForApproval_Click);
            this.Controls.Add(ibPostDraftContentForApproval);

            ibApproveContent = new ImageButton();
            ibApproveContent.ID = "ibApproveContent";
            ibApproveContent.CssClass = "ModuleApproveContentLink";
            ibApproveContent.SkinID = "plain";
            ibApproveContent.Visible = false;
            ibApproveContent.Click += new ImageClickEventHandler(ibApproveContent_Click);
            this.Controls.Add(ibApproveContent);

            lnkRejectContent = new HyperLink();
            lnkRejectContent.ID = "ibRejectContent";
            lnkRejectContent.CssClass = "ModuleRejectContentLink";
            lnkRejectContent.SkinID = "plain";
            lnkRejectContent.Visible = false;

            ibCancelChanges = new ImageButton();
            ibCancelChanges.ID = "ibCancelChanges";
            ibCancelChanges.CssClass = "ModuleCancelChangesLink";
            ibCancelChanges.SkinID = "plain";
            ibCancelChanges.Visible = false;
            UIHelper.AddConfirmationDialog(ibCancelChanges, Resource.CancelContentChangesButtonWarning);
            ibCancelChanges.Click += new ImageClickEventHandler(ibCancelChanges_Click);
            this.Controls.Add(ibCancelChanges);

            statusLink = new ClueTipHelpLink();

            this.Controls.Add(statusLink);


        }



    }
}

/// Author:				
/// Created:			2004-08-22
/// Last Modified:	    2013-04-07
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using Resources;
using mojoPortal.Web.UI;
using System.Linq;

namespace mojoPortal.Web.Admin.WebBuilder
{

    public partial class PageBuilder : mojoBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CmsPage));

        private HyperLink lnkEditPageSettings = new HyperLink();
        private HyperLink lnkEditPageContent = new HyperLink();

        private int skinPageID = -1;
        private int skinID = -1;

        //private bool isAdmin = false;
        //private bool isContentAdmin = false;
        //private bool isSiteEditor = false;
        //private bool allowPageOverride = true;
        private int countOfIWorkflow = 0;
        //private bool userCanEditPage = false;

        private bool forceShowWorkflow = false;

        override protected void OnPreInit(EventArgs e)
        {

            //SetMasterInBasePage = false;

            AllowSkinOverride = true;
            base.OnPreInit(e);

            if (WebConfigSettings.SetMaintainScrollPositionOnPostBackTrueOnCmsPages)
            {
                MaintainScrollPositionOnPostBack = true;
            }

            //try
            //{
            //    SetupMasterPage();

            //}
            //catch (HttpException ex)
            //{
            //    log.Error(SiteUtils.GetIP4Address() + " - Error setting master page. Will try settingto default skin", ex);
            //    SetupFailsafeMasterPage();
            //}


            //StyleSheetCombiner styleCombiner = (StyleSheetCombiner)Master.FindControl("StyleSheetCombiner");
            //if (styleCombiner != null) { styleCombiner.AllowPageOverride = allowPageOverride; }


        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);
        }

        void Page_Load(object sender, EventArgs e)
        {
            LoadParam();
            LoadPage();
        }

        private void LoadParam()
        {
            skinPageID = WebUtils.ParseInt32FromQueryString("skinpage", skinPageID);
            skinID = WebUtils.ParseInt32FromQueryString("skinid", skinID);
        }

        private void LoadPage()
        {

            var moduleForPage = CoreSkinPageDefault.GetAll().Where(x => x.SkinPageID == skinPageID).OrderBy(x => x.ModuleOrder).ToList();
            if (moduleForPage.Any())
            {
                foreach (var item in moduleForPage)
                {
                    ModuleDefinition moduleDefinition = new ModuleDefinition(item.ModuleID);


                    Module module = new Module(item.ItemID,true);
                    module.PaneName = item.PaneName;

                    Control parent = this.MPContent;

                    if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "leftpane"))
                    {
                        parent = this.MPLeftPane;

                    }

                    if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "rightpane"))
                    {
                        parent = this.MPRightPane;

                    }

                    if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "toppane"))
                    {
                        if (MPTopPane != null)
                        {
                            parent = this.MPTopPane;
                        }
                        else
                        {
                            log.Error("Content is assigned to toppane placeholder but it does not exist in layout.master so using center.");
                            parent = this.MPContent;
                        }

                    }

                    if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "bottompane"))
                    {
                        if (MPBottomPane != null)
                        {
                            parent = this.MPBottomPane;
                        }
                        else
                        {
                            log.Error("Content is assigned to bottompane placeholder but it does not exist in layout.master so using center.");
                            parent = this.MPContent;
                        }

                    }

                    try
                    {
                        Control c = Page.LoadControl("~/" + module.ControlSource);
                        if (c == null) { continue; }

                        if (c is SiteModuleControl)
                        {
                            SiteModuleControl siteModule = (SiteModuleControl)c;
                            siteModule.SiteId = siteSettings.SiteId;
                            siteModule.ModuleConfiguration = module;

                            if (siteModule is IWorkflow)
                            {
                                if (WebUser.IsInRoles(module.AuthorizedEditRoles)) { forceShowWorkflow = true; }
                                if (WebUser.IsInRoles(module.DraftEditRoles)) { forceShowWorkflow = true; }

                                countOfIWorkflow += 1;
                            }
                        }

                        parent.Controls.Add(c);
                    }
                    catch (HttpException ex)
                    {
                        log.Error("failed to load control " + moduleDefinition.ControlSrc, ex);
                    }

                    parent.Visible = true;
                    parent.Parent.Visible = true;


                } //end foreach
            }

            SetupAdminLinks();

            if ((!WebConfigSettings.DisableExternalCommentSystems) && (siteSettings != null) && (CurrentPage != null) && (CurrentPage.EnableComments))
            {
                switch (siteSettings.CommentProvider)
                {
                    case "disqus":

                        if (siteSettings.DisqusSiteShortName.Length > 0)
                        {
                            DisqusWidget disqus = new DisqusWidget();
                            disqus.SiteShortName = siteSettings.DisqusSiteShortName;
                            disqus.WidgetPageUrl = WebUtils.ResolveServerUrl(SiteUtils.GetCurrentPageUrl());
                            if (disqus.WidgetPageUrl.StartsWith("https"))
                            {
                                disqus.WidgetPageUrl = disqus.WidgetPageUrl.Replace("https", "http");
                            }
                            disqus.RenderWidget = true;
                            MPContent.Controls.Add(disqus);
                        }

                        break;

                    case "intensedebate":

                        if (siteSettings.IntenseDebateAccountId.Length > 0)
                        {
                            IntenseDebateDiscussion d = new IntenseDebateDiscussion();
                            d.AccountId = siteSettings.IntenseDebateAccountId;
                            d.PostUrl = SiteUtils.GetCurrentPageUrl();
                            MPContent.Controls.Add(d);

                        }



                        break;

                    case "facebook":
                        FacebookCommentWidget fbComments = new FacebookCommentWidget();
                        fbComments.AutoDetectUrl = true;
                        MPContent.Controls.Add(fbComments);

                        break;

                }




            }

            if (WebConfigSettings.HidePageViewModeIfNoWorkflowItems && (countOfIWorkflow == 0))
            {
                HideViewSelector();
            }

            // (to show the last mnodified time of a page we may have this control in layout.master, but I set it invisible by default
            // because we only want to show it on content pages not edit pages
            // since Default.aspx.cs is the handler for content pages, we look for it here and make it visible.
            Control pageLastMod = Master.FindControl("pageLastMod");
            if (pageLastMod != null) { pageLastMod.Visible = true; }

        }





        private void LoadSettings()
        {
            //isAdmin = WebUser.IsAdmin;
            //if (!isAdmin) { isContentAdmin = WebUser.IsContentAdmin; }

            // isSiteEditor = SiteUtils.UserIsSiteEditor();

            //userCanEditPage = WebUser.IsInRoles(CurrentPage.EditRoles);

            //if((isContentAdmin && !isAdmin)&&(CurrentPage.EditRoles == "Admins;"))
            //{
            //    userCanEditPage = false;
            //}

            //if ((userCanEditPage) && (WebConfigSettings.EnableDragDropPageLayout))
            //{
            //    ScriptConfig.IncludeInterface = true;
            //    SetupDragDropScript();
            //}

            //we need it enabled always in .NET 4 in order for viewstatemode to work
            //#if NET35
            //            if (WebConfigSettings.DisablePageViewStateByDefault)
            //            {
            //                this.EnableViewState = false;
            //            }
            //#endif
        }


        /// <summary>
        /// this is just an experiment looking into possibilities of drag drop page re-arrangement
        /// </summary>
        private void SetupDragDropScript()
        {
            StringBuilder script = new StringBuilder();

            script.Append("$(document).ready(");
            script.Append("function()");
            script.Append("{");

            script.Append("$('div.panelwrapper').each(function(){");
            script.Append("$(this).Draggable();");
            script.Append("});");

            script.Append("$('div.cmszone').each(function(){");
            //script.Append("$(this).Sortable(");
            script.Append("$(this).Droppable(");
            script.Append("{");
            script.Append("accept:'panelwrapper'");
            script.Append(",helperclass:'sortHelper'");
            script.Append(",activeclass:'cmsdropactive'");
            script.Append(",hoverclass:'cmsdrophover'");
            script.Append(",tolerance:'intersect'");


            // Droppable
            script.Append(",ondrop:	function (drag)");
            script.Append("{");
            script.Append("ModuleDrop(this,drag);");
            script.Append("}");

            // sortable
            ////script.Append(",handle:'div.panelwrapper'");
            //script.Append(",onChange:function (drag)");
            //script.Append("{");
            //script.Append("ModuleDrop(this,drag);");
            //script.Append("}");
            //script.Append(",onStart:function ()");
            //script.Append("{");
            //script.Append("$.iAutoscroller.start(this, document.getElementsByTagName('body'));");
            //script.Append("}");
            //script.Append(",onStop:function ()");
            //script.Append("{");
            //script.Append("$.iAutoscroller.stop();");
            //script.Append("}");



            script.Append("}");
            script.Append(");");//end sortable


            script.Append("}");
            script.Append(");");//end each

            script.Append("});");//end document.ready



            script.Append("function ModuleDrop(droppable, dragable)");
            script.Append("{");

            script.Append("alert(droppable);");
            script.Append("alert(dragable);");

            script.Append("}");




            Page.ClientScript.RegisterStartupScript(typeof(Page),
                   "droppanels", "\n<script type=\"text/javascript\">"
                   + script.ToString() + "</script>");

        }


        private void EnsurePageAndSite()
        {
            if (CurrentPage == null)
            {
                int siteCount = SiteSettings.SiteCount();


                if (siteCount == 0)
                {
                    // no site data so redirect to setup
                    HttpContext.Current.Response.Redirect(WebUtils.GetSiteRoot() + "/Setup/Default.aspx");
                }


            }


        }

        private bool RedirectIfNeeded()
        {

            //if (
            //    (!isAdmin)
            //    && ((!isContentAdmin) || (isContentAdmin && (CurrentPage.AuthorizedRoles == "Admins;")))
            //    && ((!isSiteEditor) || (isSiteEditor && (CurrentPage.AuthorizedRoles == "Admins;")))
            //    && (!WebUser.IsInRoles(CurrentPage.AuthorizedRoles))
            //    )
            if (!UserCanViewPage())
            {
                if (!Request.IsAuthenticated)
                {
                    if (WebConfigSettings.UseRawUrlForCmsPageLoginRedirects)
                    {
                        SiteUtils.RedirectToLoginPage(this);
                    }
                    else
                    {
                        SiteUtils.RedirectToLoginPage(this, SiteUtils.GetCurrentPageUrl());
                    }
                    return true;

                }
                else
                {
                    SiteUtils.RedirectToAccessDeniedPage(this);
                    return true;
                }
            }

            return false;

        }



        private void EnforceSecuritySettings()
        {
            if (CurrentPage.PageId == -1) { return; }

            if (!CurrentPage.AllowBrowserCache)
            {
                SecurityHelper.DisableBrowserCache();
            }

            bool useSsl = false;

            if (SiteUtils.SslIsAvailable())
            {
                if (WebConfigSettings.ForceSslOnAllPages || siteSettings.UseSslOnAllPages || CurrentPage.RequireSsl)
                {
                    useSsl = true;
                }
            }

            if (useSsl)
            {
                SiteUtils.ForceSsl();
            }
            else
            {
                SiteUtils.ClearSsl();
            }



        }

        private void SetupAdminLinks()
        {

            // 2010-01-04 made it possible to add these links directly in layout.master so they can be arranged and styled easier
            if (Page.Master.FindControl("lnkPageContent") == null)
            {
                this.MPPageEdit.Controls.Add(new PageEditFeaturesLink());
            }

            if (Page.Master.FindControl("lnkPageSettings") == null)
            {
                this.MPPageEdit.Controls.Add(new PageEditSettingsLink());
            }

            SetupWorkflowControls(forceShowWorkflow);

        }








    }
}

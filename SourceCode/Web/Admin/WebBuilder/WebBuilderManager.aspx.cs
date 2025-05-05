/// Author:					Joe Audette
/// Created:				2008-06-22
/// Last Modified:			2011-03-21
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using mojoPortal.Business.WebHelpers;
using Resources;
using mojoPortal.Web.Components;
using mojoPortal.Business;
using System.Linq;
using System.Web.Services;
using System.Web.Http;
using mojoPortal.Web.Framework;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using static mojoPortal.Web.Components.SiteContants;

namespace mojoPortal.Web.AdminUI
{

    public partial class WebBuilderManager : NonCmsBasePage
    {
        private bool isAdmin = false;
        private bool isContentAdmin = false;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();

            if ((!isAdmin) && (!isContentAdmin))
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }

            if (!siteSettings.IsServerAdminSite)
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;

            }

            PopulateLabels();
            PopulateControls();

        }


        private void PopulateControls()
        {
            BindTemplate();
        }
        protected string GetUrlTemplate(string siteId)
        {
            var siteSub = new SiteSettings(siteId.ToIntOrZero());

            return string.Format("{0}/{1}/home", siteSub.SiteRoot, siteSub.UrlSiteMap);
        }
        protected string GetUrlSkinPage(int skinID)
        {
            return string.Format("{0}/Admin/WebBuilder/SkinPageManager.aspx?skins={1}", siteSettings.SiteRoot, skinID);
        }
        private void BindTemplate()
        {
            rptSkin.DataSource = SiteSettings.GetListTemplate();
            rptSkin.DataBind();
        }

        [WebMethod]
        [HttpPost]
        public static void DeleteTemplate(int id)
        {
            if (WebConfigSettings.AllowDeletingChildSites)
            {
                SiteSettings selectedSite = new SiteSettings(id);
                if ((selectedSite != null) && (!selectedSite.IsServerAdminSite))
                {
                    try
                    {
                        DeleteSiteContent(selectedSite.SiteId);
                        CommentRepository commentRepository = new CommentRepository();
                        commentRepository.DeleteBySite(selectedSite.SiteGuid);

                    }
                    catch (Exception ex)
                    {
                        //log.Error("error deleting site content ", ex);
                    }

                    SiteSettings.Delete(selectedSite.SiteId);
                    //WebUtils.SetupRedirect(this, SiteRoot + "/Admin/SiteList.aspx");
                }
            }
        }
        static void DeleteSiteContent(int siteId)
        {
            if (siteId == -1) { return; }

            foreach (SitePreDeleteHandlerProvider contentDeleter in SitePreDeleteHandlerProviderManager.Providers)
            {

                try
                {
                    contentDeleter.DeleteSiteContent(siteId);
                }
                catch (Exception ex)
                {
                    //log.Error("SiteSettings.aspx.cs.DeleteSiteContent ", ex);
                }

            }

            //if (WebConfigSettings.DeleteSiteFolderWhenDeletingSites)
            //{
            //    FolderDeleteTask task = new FolderDeleteTask();
            //    task.SiteGuid = siteSettings.SiteGuid;
            //    SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
            //    if (currentUser != null)
            //    {
            //        task.QueuedBy = currentUser.UserGuid;
            //    }
            //    task.FolderToDelete = Server.MapPath("~/Data/Sites/" + siteId.ToInvariantString() + "/");
            //    task.QueueTask();

            //    WebTaskManager.StartOrResumeTasks();
            //}

        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản trị template subPortal");

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkWebBuilder.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderMenu.aspx";
            lnkWebBuilder.Text = "Xây dựng subPortal";

            lnkCurrentPage.Text = "Quản trị template subPortal";
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/WebBuilder/SkinManager.aspx";

            heading.Text = "Danh sách template subPortal";
        }

        private void LoadSettings()
        {
            isAdmin = WebUser.IsAdmin;
            isContentAdmin = WebUser.IsContentAdmin;

            AddClassToBody("administration");
            AddClassToBody("coredata");
        }


        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            SuppressMenuSelection();
            SuppressPageMenu();

        }

        #endregion
    }
}
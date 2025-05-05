using System;
using System.Collections;
using mojoPortal.Business;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using mojoPortal.Features;
using System.Web.Hosting;
using System.Web;
using System.Web.Script.Serialization;
using mojoPortal.FileSystem;
using System.IO;
using System.Globalization;
using SurveyFeature.Business;
using System.Text;
using log4net;
using MediaAlbumFeature.Business;
using MediaGroupFeature.Business;

namespace AudioFeature.UI
{
    public partial class ViewList : mojoBasePage
    {

        private static IFileSystem fileSystem = null;
        private static Page page = new Page();
        protected int pageId = -1;
        protected int moduleId = -1;
        private bool userCanEdit;
        private int countOfDrafts;
        private int pageNumber = 1;
        private int groupMediaId = -1;
        private MediaConfiguration config = new MediaConfiguration();

        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            LoadParams();
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
            LoadSettings();
            PopulateLabels();
            PopulateControls();

        }

        private void PopulateControls()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Danh sách audio");

            audioListControl.ModuleId = moduleId;
            audioListControl.config = config;
            audioListControl.PageId = pageId;

            if (config.ShowLink)
            {
                md_AudioGroup media = new md_AudioGroup(groupMediaId);
                MetaDescription = string.Format(
                    CultureInfo.InvariantCulture,
                    ForumResources.ForumThreadMetaDescriptionFormat,
                    SecurityHelper.RemoveMarkup(media.NameGroup));
            }
        }

        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Thư viện ảnh");
            TitleControl.Visible = false;
            TitleControl.ForceShowExtraMarkup = false;
        }
        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            LoadSideContent(true, false);
            //userCanEdit = UserCanEditModule(moduleId);
            ////if (userCanEdit) { countOfDrafts = Article.CountOfDrafts(moduleId); }
            //pnlWrapper.CssClass += config.ViewListCssClass != string.Empty
            //                           ? " " + config.ViewListCssClass
            //                           : " " + config.InstanceCssClass;
        }
        [System.Web.Services.WebMethod]
        public static void ViewCount(int id)
        {
            MediaAlbum.UpdateViews(id);
        }
        [System.Web.Services.WebMethod]
        public static void Download(string filePath)
        {

            try
            {
                filePath = "novideo.jpg";

                System.IO.FileStream fs = null;
                string path = HostingEnvironment.ApplicationPhysicalPath + @"Data\File\Media\" + filePath;
                fs = System.IO.File.Open(path, System.IO.FileMode.Open);
                long fileSize = fs.Length;

                byte[] btFile = new byte[(int)fileSize];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" + filePath);
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.BinaryWrite(btFile);
                HttpContext.Current.Response.WriteFile(path);
                HttpContext.Current.Response.End();
                fs = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            groupMediaId = WebUtils.ParseInt32FromQueryString("groupMediaId", groupMediaId);
            config = new MediaConfiguration(settings);
            AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "blog");
        }

    }

}
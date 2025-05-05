using System;
using System.IO;
using System.Globalization;
using System.Text;
using System.Web;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using log4net;
using mojoPortal.Web;
using ArticleFeature.Business;

namespace ArticleFeature.UI
{

    public partial class AttachmentDownload : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AttachmentDownload));

        #region OnInit
        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;

            base.OnInit(e);
        }
        #endregion

        private int pageID = -1;
        private int moduleID = -1;
        private int fileID = -1;
        private int siteId = -1;
        private int articleID = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private ArticleAttachment attachment;



        protected void Page_Load(object sender, EventArgs e)
        {
            SecurityHelper.DisableDownloadCache();
            LoadAndCheckParams();
            //Server.ScriptTimeout

            //if (
            //    (LoadAndCheckParams())
            //    && (UserHasPermission())
            //    )
            //if (LoadAndCheckParams())
            //{
            attachment = new ArticleAttachment(fileID);
            DownloadFile();
            //}
            //else
            //{
            //    if (!Request.IsAuthenticated)
            //    {
            //        SiteUtils.RedirectToLoginPage(this);
            //        return;
            //    }
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}

        }

        private void DownloadFile()
        {
            if ((CurrentPage == null) || (attachment == null)) return;
            //string downloadPath = Page.Server.MapPath(WebUtils.GetApplicationRoot() + "/Data/Sites/" + siteId + "/media/ArticleAttachments/" + attachment.ServerFileName);

            Article article = new Article(articleID);
            var chuyenmuc = new CoreCategory(article.CategoryID);
            var ngaydang = string.Format("{0:yyyyMMdd}", article.CreatedDate);
            string downloadPath = Page.Server.MapPath(WebUtils.GetApplicationRoot() + "/Data/Sites/" + article.SiteId + "/media/ArticleAttachments/" + chuyenmuc.Name.ConvertToFTS().Replace(" ", "")  + "/" + attachment.ServerFileName);

            if (File.Exists(downloadPath))
            {
                FileInfo fileInfo = new FileInfo(downloadPath);
                Page.Response.AppendHeader("Content-Length", fileInfo.Length.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                log.Error("Shared File Not Found. User tried to download file " + downloadPath);
                return;
            }

            // ReSharper disable PossibleNullReferenceException
            string fileType = Path.GetExtension(attachment.FileName).Replace(".", string.Empty);
            // ReSharper restore PossibleNullReferenceException

            string mimeType = SiteUtils.GetMimeType(fileType);
            Page.Response.ContentType = mimeType;

            if (SiteUtils.IsNonAttacmentFileType(fileType))
            {
                //this will display the pdf right in the browser
                Page.Response.AddHeader("Content-Disposition", "filename=\"" + HttpUtility.UrlEncode(attachment.FileName, Encoding.UTF8) + "\"");
            }
            else
            {
                // other files just use file save dialog
                Page.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(attachment.FileName, Encoding.UTF8) + "\"");
            }

            //Page.Response.AddHeader("Content-Length", documentFile.DocumentImage.LongLength.ToString());

            try
            {
                Page.Response.Buffer = false;
                Page.Response.BufferOutput = false;
                if (Page.Response.IsClientConnected)
                {
                    Page.Response.TransmitFile(downloadPath);
                    ArticleAttachment a = new ArticleAttachment(fileID);
                    a.Save();
                }

            }
            catch (HttpException) { }
            Page.Response.End();
        }

        //private bool UserHasPermission()
        //{
        //    bool result = false;

        //    if (
        //        (CurrentPage != null)
        //        && (WebUser.IsInRoles(CurrentPage.AuthorizedRoles))
        //        )
        //    {
        //        bool moduleIsOnPage = false;

        //        foreach (Module m in CurrentPage.Modules)
        //        {
        //            if (m.ModuleId == moduleID)
        //            {
        //                moduleIsOnPage = true;
        //                // user has page viewpermission but not module view permission so his module is no visible on the page for this user.
        //                if (!WebUser.IsInRoles(m.ViewRoles)) { return false; }

        //            }

        //        }

        //        if (moduleIsOnPage)
        //        {
        //            attachment = new ArticleAttachment(fileID);

        //            if (attachment.ModuleID == moduleID) result = true;
        //        }

        //    }

        //    return result;

        //}

        private bool LoadAndCheckParams()
        {
            bool result = true;
            siteId = WebUtils.ParseInt32FromQueryString("siteid", -1);
            pageID = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleID = WebUtils.ParseInt32FromQueryString("mid", -1);
            fileID = WebUtils.ParseInt32FromQueryString("fileid", -1);
            articleID = WebUtils.ParseInt32FromQueryString("articleid", -1);
            if (siteId == -1) result = false;
            if (pageID == -1) result = false;
            if (moduleID == -1) result = false;
            if (fileID == -1) result = false;
            if (articleID == -1) result = false;
            return result;

        }


    }
}

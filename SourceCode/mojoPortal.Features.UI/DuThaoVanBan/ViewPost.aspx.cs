using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DuThaoVanBanFeature.UI
{
    public partial class ViewPost : mojoBasePage
    {
        private int moduleId = -1;

        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;
            LoadPanels();
            base.OnInit(e);
        }

        private void LoadPanels()
        {
            LoadSideContent(true, true);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
        }

        #endregion

        private void Page_Load(object sender, EventArgs e)
        {
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            var item = WebUtils.ParseInt32FromQueryString("item", -1);
            var DuThao = new DuThaoVanBan(item);
            if (DuThao != null)
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, DuThao.Title);

            }
            else
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, "Chi tiết dự thảo văn bản");

            }
        }

        protected override void OnError(EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            if ((lastError == null) || (!(lastError is NullReferenceException)) || !Page.IsPostBack) return;
            if (!lastError.StackTrace.Contains("Recaptcha")) return;
            Server.ClearError();
            WebUtils.SetupRedirect(this, Request.RawUrl);
        }
    }
}
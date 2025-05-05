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

namespace EventFeature.UI
{
    public partial class ViewPost : mojoBasePage
    {
        private int moduleId = -1;
        EventConfiguration config = new EventConfiguration();

        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
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
            //moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            //Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            //config = new EventConfiguration(settings);
            LoadSideContent(false, true);
        }

        #endregion

        private void Page_Load(object sender, EventArgs e)
        {
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            pnlContainer.ModuleId = moduleId;
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
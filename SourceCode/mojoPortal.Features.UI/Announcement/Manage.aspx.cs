using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnnouncementFeatures.UI
{
    public partial class Manage : mojoBasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            LoadLabel();
            ManageControl.SiteRoot = SiteRoot;
        }
        private void LoadLabel()
        {
            heading.Text = "Quản lý danh sách các thông báo";
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản lý danh sách các thông báo");
        }
    }
}
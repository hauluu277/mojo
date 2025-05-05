//using DVCFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features.Business.SwirlingQuestion;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using SwirlingQuestionFeature.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DuThaoVanBanFeature.UI
{
    public partial class ThongKeYKien : System.Web.UI.Page
    {
        private int pageId = -1;
        private int moduleId = -1;
        private int itemID = -1;
        private SiteUser currentUser = null;
        private int state = -1;
        private bool? DuyetYKien = null;
        private bool? XuatBanYKien = null;
        private int countChuatiepNhan = 0;
        private int duthaoId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageDuThaoVanBan))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            LoadParams();

            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            DuThaoVanBan duthao = new DuThaoVanBan(duthaoId);
            int YKienDaDuyet = CommentsDraft.GetCount(duthaoId, 1, -1);
            int YKienDaXuatBan = CommentsDraft.GetCount(duthaoId, -1, 1);
            int YKienChuaXuatBan = CommentsDraft.GetCount(duthaoId, -1, 0);
            int YKien = CommentsDraft.GetCount(duthaoId, -1, -1);
            int YKienChuaDuyet = CommentsDraft.GetCount(duthaoId, 0, -1);
            lblYKienDaDuyet.Text = YKienDaDuyet.ToString();
            lblYKienDaXuatBan.Text = YKienDaXuatBan.ToString();
            lblYKienChuaDuyet.Text = YKienChuaDuyet.ToString();
            lblYKienChuaXuatBan.Text = YKienChuaXuatBan.ToString();
            lblDuThao.Text = duthao.Title;
            lblYKien.Text = YKien.ToString();
        }      
        private void PopulateLabels()
        {

            this.Title = "Thống kê ý kiến góp ý dự thảo";
         

        }

        private void LoadSettings()
        {

            currentUser = SiteUtils.GetCurrentSiteUser();
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            duthaoId = WebUtils.ParseInt32FromQueryString("duthaoid", duthaoId);
        }


        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion
    }
}
using mojoPortal.Business;
using mojoPortal.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using Resources;

namespace HomeCategoryFeature.UI
{
    public partial class HomeRightModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabel();
            if (!IsPostBack)
            {
                PopulateControl();
            }
        }
        private void PopulateControl()
        {
            rptCategory.DataSource = HomeCategory.GetAll().Where(x => x.SiteID == SiteId).OrderBy(x => x.OrderBy).Take(5).ToList();
            rptCategory.DataBind();
        }
        private void PopulateLabel()
        {
            TitleControl.Visible = true;
            if (IsEditable)
            {
                TitleControl.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/ModuleBuilder/HomeCategory/ManagePost.aspx?pageid=" + PageId.ToInvariantString()
                    + "&amp;mid=" + ModuleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='"
                    + BlogResources.Administration + "'>"
                    + BlogResources.Administration + "</a>"
                    ;
            }
        }
        private void LoadParam()
        {

        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
        }
    }
}
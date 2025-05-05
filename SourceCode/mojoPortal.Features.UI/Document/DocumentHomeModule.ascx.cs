using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using log4net;
using mojoPortal.Business;
using Resources;
using mojoPortal.Features;
using System;
using mojoPortal.Business.WebHelpers;
using System.Collections;

namespace DocumentFeature.UI
{
    public partial class DocumentHomeModule : SiteModuleControl
    {

        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        DocumentConfiguration config = new DocumentConfiguration();

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
            PopulateLabels();
            PopulateControls();
        }
        private void PopulateControls()
        {
            DocumentHome.PageId = PageId;
            DocumentHome.ModuleId = ModuleId;
        }

        private void PopulateLabels()
        {
            pnlOuterWrap.CssClass = config.CssCustom;
            TitleControl.Visible = false;
            if (siteUser != null)
            {
                if (IsEditable)
                {
                    TitleControl.Visible = true;
                }
            }
            //if (IsEditable)
            //{
            //    TitleControl.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/document/managepost.aspx?pageid=" + PageId.ToInvariantString()
            //        + "&amp;mid=" + ModuleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='"
            //        + BlogResources.Administration + "'>"
            //        + BlogResources.Administration + "</a>"
            //        ;
        }


        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DocumentConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
        }
    }
}
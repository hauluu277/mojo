using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Features.UI.Gold_Price
{

    public partial class Gold_Price_DisplayModule : System.Web.UI.Page
    {

        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        #region OnInit
        override protected void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(Page_Load);
            base.OnInit(e);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateLabels();
            PopulateControls();
        }

        private void PopulateControls()
        {
            TitleControl.EditUrl = siteSetting.SiteRoot + "/Gold_Price/Gold_Price_DisplayEdit.aspx";
            TitleControl.Visible = true;
             
        }
        private void PopulateLabels()
        {
            TitleControl.EditText = "Edit";
        }


    }
}
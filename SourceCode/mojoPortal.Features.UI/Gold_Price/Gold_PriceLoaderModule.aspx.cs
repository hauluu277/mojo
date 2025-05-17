using ArticleFeature.UI;
using Brettle.Web.NeatUpload;
using CKFinder.Connector;
using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using mojoPortal.Business;
using System.Web.UI.WebControls;
using mojoPortal.Features.UI.Gold_Price;

namespace Gold_PriceFeatures.UI
{
    public partial class Gold_PriceLoaderModule : mojoBasePage
    {

        private Module module;
        private int moduleId = -1;
        protected Gold_PriceConfiguration config = new Gold_PriceConfiguration();

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        public Gold_PriceConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings(); 
            PopulateLabels();

            PopulateControls();
            pnlOuterWrap.CssClass = Config.ModuleGold_PriceCssCustome;

        }
        private void PopulateControls()
        {
            if (!string.IsNullOrEmpty(Config.ModuleGold_PriceCssCustome))
            {
                Gold_Price_DisplayModuleControls tabLoader = (Gold_Price_DisplayModuleControls)LoadControl("~/Gold_Price/Controls/Gold_PriceDisplayManagerControls.ascx");

                placeHolder.Controls.Add(tabLoader);
            } 
            else
            {
                PostListLoader postListLoader = (PostListLoader)LoadControl("~/Gold_Price/Controls/Gold_PriceDisplayManagerControls.ascx"); 
                placeHolder.Controls.Add(postListLoader);
            }
        }
        private void PopulateLabels()
        { 

        }

        private void LoadSettings()
        { 
        }
    }
}
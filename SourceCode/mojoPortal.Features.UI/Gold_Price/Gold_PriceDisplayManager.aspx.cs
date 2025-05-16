using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Features.UI; 
using ArticleFeature.UI;
namespace Gold_PriceFeatures.UI
{
    public partial class Gold_PriceDisplayManager : mojoBasePage
    {
        //readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        /*private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;*/
        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();




            //if (!userCanEdit)
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //}

            PopulateLabels();

            PopulateControls();

        }
        private void PopulateControls()
        {
            /*PostList.ModuleId = moduleId;
            PostList.PageId = pageId;
            PostList.SiteRoot = SiteRoot;*/
        }
        private void PopulateLabels()
        {
            Title = "Giá vàng ";
            TitleControl.Visible = false;
            

        }

        private void LoadSettings()
        {
            //userCanEdit = UserCanEditModule(moduleId);
            //pnlContainer.ModuleId = moduleId; 
        } 
    }
} 
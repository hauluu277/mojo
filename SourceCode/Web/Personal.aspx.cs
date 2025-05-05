// Author:					    
// Created:				        2005-06-26
//	Last Modified:              2013-01-17
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software. 

using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.SearchIndex;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArticleFeature.Business;

namespace mojoPortal.Web.UI.Pages
{

    public partial class Personal : NonCmsBasePage
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Personal));
        private string id = string.Empty;



        #region OnInit
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);

            SuppressMenuSelection();
            SuppressPageMenu();

            if (WebConfigSettings.ShowLeftColumnOnSearchResults) { StyleCombiner.AlwaysShowLeftColumn = true; }
            if (WebConfigSettings.ShowRightColumnOnSearchResults) { StyleCombiner.AlwaysShowRightColumn = true; }
        }




        #endregion

        private void Page_Load(object sender, EventArgs e)
        {
            if (SiteUtils.SslIsAvailable()) { SiteUtils.ForceSsl(); }

            LoadSettings();


            if (siteSettings == null)
            {
                siteSettings = CacheHelper.GetCurrentSiteSettings();
            }

            PopulateLabels();
            string primarySearchProvider = SiteUtils.GetPrimarySearchProvider();

            if (!IsPostBack)
            {
                SetupHeightLightScript();
            }
        }


        private void SetupHeightLightScript()
        {


            StringBuilder script = new StringBuilder();



            script.Append("\n<script type='text/javascript'>");
            script.Append("$(document).ready(function(){");
            script.Append("$.ajax({");
            script.Append("type: 'GET',");
            script.Append("url: '/PersonalArea/Personal/DetailPersonal',");
            script.Append("data: {'id': '" + id + "'},");
            script.Append("success: successAjaxResponse,");
            script.Append("error: failureAjaxResponse");
            script.Append("});");
            script.Append("});");
            script.Append("</script>");

            Page.ClientScript.RegisterStartupScript(typeof(Page), "ScriptPersonal", script.ToString());
        }

        private void LoadSettings()
        {
            id = WebUtils.ParseStringFromQueryString("id", id);
            DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo = new DAOTAO_CaNhanInfo(id);
            id = dAOTAO_CaNhanInfo.NhanSuID.ToString();
        }

        private void PopulateLabels()
        {
            if (siteSettings == null) return;

            Title = SiteUtils.FormatPageTitle(siteSettings, "Thông tin cá nhân");
            //heading.Text = Resource.SearchPageTitle;
            MetaDescription = string.Format(CultureInfo.InvariantCulture, "Thông tin cá nhân", siteSettings.SiteName);

        }
    }
}

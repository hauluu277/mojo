// Author:					HiNet
// Created:					2014-9-24
// Last Modified:			2014-9-24
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using log4net;
using mojoPortal.Business;
using Resources;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using System.Text;

namespace OrganizationFeature.UI
{

    public partial class OrganizationModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        // FeatureGuid 8169280b-5720-4301-965a-20a09701b9f7

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
            //pnlOuterWrap.CssClass = config.ModuleHotCssCustome;
        }

        private void PopulateControls()
        {
            TitleControl.Visible = true;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    TitleControl.Visible = true;
                }
            }
            TitleControl.ShowEditLinkOverride = false;
            StringBuilder scriptFunc = new StringBuilder();
            scriptFunc.Append("$(document).ready(function(){");
            scriptFunc.Append("CallAjaxLoading('get','/OrganizationArea/Organization/OrganizationPublish',{siteId:" + siteSetting.SiteId + "},true,function(rs){");
            scriptFunc.Append("$('#product_div').html(rs);");
            scriptFunc.Append("});");
            scriptFunc.Append("});");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadOrganization", scriptFunc.ToString(), true);

        }


        private void PopulateLabels()
        {
            TitleControl.Visible = true;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    if (siteUser.IsInRoles("Admins"))
                    {
                        TitleControl.DisabledModuleSettingsLink = false;
                    }
                    else
                    {
                        TitleControl.DisabledModuleSettingsLink = true;
                    }
                    TitleControl.Visible = true;
                }
            }

            //TitleControl.EditUrl = SiteRoot + "/article/editpost.aspx";
            //TitleControl.EditText = BlogResources.BlogAddPostLabel;
            //if (IsEditable)
            //{
            //    TitleControl.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/article/managepost.aspx?pageid=" + PageId.ToInvariantString()
            //        + "&amp;mid=" + ModuleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='"
            //        + ArticleResources.AdministratorLabel + "'>"
            //        + ArticleResources.AdministratorLabel + "</a>"
            //        + "&nbsp;&nbsp;&nbsp;<a href='"
            //        + SiteRoot
            //        + "/article/ManageAllPost.aspx?pageid=" + PageId.ToInvariantString()
            //        + "&amp;mid=" + ModuleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='"
            //        + ArticleResources.AllAdministratorLabel + "'>"
            //        + ArticleResources.AllAdministratorLabel + "</a>"
            //        ;
            //}
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);

        }


    }
}
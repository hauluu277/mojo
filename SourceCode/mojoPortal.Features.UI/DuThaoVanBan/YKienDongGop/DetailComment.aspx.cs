// Author:					Trieubv
// Created:					2015-12-15
// Last Modified:			2015-12-15
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
using System.Configuration;
using System.Data;
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
using mojoPortal.Business.WebHelpers;
using Resources;



namespace DuThaoVanBanFeature.UI
{

    public partial class DetailComment : mojoBasePage
    {
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private Guid featureGuid = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();

            // one of these may be usefull
            //if (!UserCanViewPage(moduleId, featureGuid))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}
            //if (!UserCanEditModule(moduleId, featureGuid))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}

            LoadSettings();
            PopulateLabels();
            PopulateControls();

        }

        private void PopulateControls()
        {
            CommentsDraft cmd = new CommentsDraft(itemId);
            if(cmd!=null)
            {
                litName.Text = cmd.Name;
                litEmail.Text = cmd.Email;
                litAddress.Text = cmd.Address;
                litPhone.Text = cmd.Mobile;
                litContent.Text = cmd.Comment;
                litApprove.Text = cmd.IsApproved == true ? "Đã duyệt" : "Chưa duyệt";
                litPublic.Text = cmd.IsPublished == true ? "Đã xuất bản" : "Chưa xuất bản";
            }
        }


        private void PopulateLabels()
        {

        }

        private void LoadSettings()
        {
            LoadSideContent(false, true);
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            itemId = WebUtils.ParseInt32FromQueryString("item", itemId);
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

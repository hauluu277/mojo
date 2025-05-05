/// Author:				    
/// Created:			    2004-08-26
///	Last Modified:		    2012-09-24
/// 
/// 2007-04-13   Alexander Yushchenko: made it WebControl instead of UserControl.
/// 2007-07-05   Alexander Yushchenko: added option to render as a simple heading.
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.	

using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;

namespace mojoPortal.Web.UI
{

    public class GenderPageID : WebControl
    {

        protected override void Render(HtmlTextWriter writer)
        {
            if (HttpContext.Current == null)
            {
                writer.Write("[" + this.ID + "]");
                return;
            }

            DoRender(writer);


        }

        private void DoRender(HtmlTextWriter writer)
        {
            SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (siteSettings == null) return;
            var listPageId = string.Empty;
            var pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            if (pageId > 0)
            {
                listPageId = string.Join(",", PageSettings.GetPageListParent(1, pageId).ToArray());
            }
            writer.Write("<input type='hidden' id='page_list' value='" + listPageId + "'/>");

        }

    }
}

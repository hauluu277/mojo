using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls.Common
{
    public partial class YearCuocDieuTraControl : System.Web.UI.UserControl
    {
        private readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        private string siteRoot = SiteUtils.GetNavigationSiteRoot();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += new EventHandler(Page_Load);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                pnlCuocDieuTra.Visible = false;
                var sites = new SiteSettings(siteSettings.SiteId);
                var lstData = new List<string>();
                var listSite = new List<SiteSettings>();
                if (sites.ParentID.HasValue && sites.ParentID.Value > 0)
                {
                    var listSiteByParent = SiteSettings.GetListByParent(sites.ParentID.Value);
                    listSite.AddRange(listSiteByParent);
                    lstData = listSite.Select(x => x.SiteName).ToList();
                    if (listSite != null && listSite.Any())
                    {
                        var siteParent = new SiteSettings(sites.ParentID.Value);
                        if (siteParent.Nam > 0)
                        {
                            listSite.Add(siteParent);
                            lstData = listSite.Select(x => x.SiteName).ToList();
                        }
                    }
                }
                else
                {
                    var listSiteByParent = SiteSettings.GetListByParent(sites.SiteId);
                    listSite.AddRange(listSiteByParent);
                    lstData = listSite.Select(x => x.SiteName).ToList();
                    listSite.Add(sites);
                }
                lstData = listSite.Select(x => x.SiteName).ToList();
                if (listSite != null && listSite.Any())
                {
                    foreach (var item in listSite)
                    {
                        item.UrlSiteMap = ("/" + item.UrlSiteMap + "/home").Replace("//", "/");
                    }
                    var selected= ("/" + sites.UrlSiteMap + "/home").Replace("//", "/");
                    listSite = listSite.OrderBy(x => x.Nam).ToList();
                    var getIndexSelected = listSite.Where(x => x.UrlSiteMap == selected).FirstOrDefault();
                    var index = 0;
                    if (getIndexSelected != null)
                    {
                        index = listSite.IndexOf(getIndexSelected);
                    }

                    ddlYear.DataTextField = "SiteName";
                    ddlYear.DataValueField = "UrlSiteMap";
                    ddlYear.DataSource = listSite;
                    ddlYear.DataBind();
                    ddlYear.SelectedIndex = index;
                    pnlCuocDieuTra.Visible = true;
                }
            }

        }
    }
}
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
using mojoPortal.Features;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;
namespace WeatherFeature.UI
{
    public partial class WeatherModule : SiteModuleControl
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
            PopulateControl();
        }
        private void PopulateControl()
        {
            string ContryVietNam = "99F791E7-7343-42E8-8C19-3C41068B5F8D";

            List<GeoZone> ListGeoZone = GeoZone.GetByCountryIsWeather(Guid.Parse(ContryVietNam)).Where(x => x.Is_Weather == true).ToList();
            try
            {
                DateTime currenttime = DateTime.Now;
                Area.LoadWeather(ListGeoZone);

                if (string.IsNullOrEmpty(Area.TimeUpdate) || currenttime.Subtract(Convert.ToDateTime(Area.TimeUpdate)).TotalHours >= 1)
                {
                    Area.WriteWeather(ListGeoZone);
                    Area.LoadWeather(ListGeoZone);
                }
                else
                {
                    Area.LoadWeather(ListGeoZone);
                }
                literWeather.Text = Area.Str;
            }
            catch
            {
                literWeather.Text = "";
            }
        }
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            //config = new MenuConfiguration(getModuleSettings);
        }
    }
}
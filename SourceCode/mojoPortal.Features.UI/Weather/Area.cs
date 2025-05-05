using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using mojoPortal.Business;

namespace WeatherFeature.UI
{
    public class WeatherInfo
    {
        public City city { get; set; }
        public List<List> list { get; set; }
        public string LastUpdate { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public string country { get; set; }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class List
    {
        public Temp temp { get; set; }
        public int humidity { get; set; }
        public List<Weather> weather { get; set; }
    }
    public class Area
    {
        private string areaName;
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }
        private string areaCode;
        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }
        public Area() { }
        public Area(string Areaname, string Areacode)
        {
            areaName = Areaname;
            areaCode = Areacode;
        }
        private static string str = String.Empty;
        public static string Str
        {
            get { return str; }
            set { str = value; }
        }
        private static string timeupdate = string.Empty;
        public static string TimeUpdate
        {
            get { return timeupdate; }
            set { timeupdate = value; }
        }
        public static void WriteWeather(List<GeoZone> ListGeoZone = null)
        {
            if (ListGeoZone != null && ListGeoZone.Count > 0)
            {
                List<Area> ListArea = new List<Area>();
                ListArea = ListGeoZone.Select(x => new Area { AreaName = x.Name, AreaCode = x.Code }).ToList();
                string appId = "d8092e6a35d232f682e8fc25f17bc33e";
                List<WeatherInfo> ListWeather = new List<WeatherInfo>();
                try
                {

                    foreach (var item in ListArea)
                    {
                        string url = "http://api.openweathermap.org/data/2.5/forecast/daily?id=" + item.AreaCode + "&units=metric&cnt=1&APPID=" + appId + "";
                        using (WebClient client = new WebClient())
                        {
                            string json = client.DownloadString(url);

                            WeatherInfo weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherInfo>(json);
                            var area = ListArea.Where(x => x.AreaCode == item.areaCode).FirstOrDefault();
                            if (area != null)
                            {
                                weatherInfo.city.name = area.AreaName;
                            }
                            weatherInfo.LastUpdate = DateTime.Now.ToString();
                            ListWeather.Add(weatherInfo);
                        }
                    }
                    Area.TimeUpdate = DateTime.Now.ToString();
                    string a = (new JavaScriptSerializer()).Serialize(ListWeather.ToArray()).ToString();
                    //write string to file
                    System.IO.File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath("~/Weather/") + "HiportWeather.js", a, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    str = ex.Message;
                }
            }
            //Area[] area = new Area[3];
            //area[0] = new Area("Đông Hà", 1582926);
            //area[1] = new Area("Hà Nội", 1581129);
            //area[2] = new Area("TP Hồ Chí Minh", 1580578);

        }
        public static void LoadWeather(List<GeoZone> ListGeoZone = null)
        {
            str = string.Empty;

            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Weather/HiportWeather.js")))
            {
                WriteWeather(ListGeoZone);
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();

            StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Weather/HiportWeather.js"));
            string jsonString = sr.ReadToEnd();
            sr.Dispose();
            sr.Close();
            sr = null;

            List<WeatherInfo> movieInfos = ser.Deserialize<List<WeatherInfo>>(jsonString);


            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //var obj = serializer.Deserialize<WeatherInfo>(File.ReadAllText("~/Weather/HiportWeather.js"));

            foreach (var item in movieInfos)
            {

                str += "<div class='main-weather'>";
                str += "<div class='name-city'> " + item.city.name + "</div>";
                str += "<div class='weather-img'> <img src='" + string.Format("http://openweathermap.org/img/w/{0}.png", item.list[0].weather[0].icon) + "'/></div>";
                str += "<div class='weather-temperature'>" + string.Format("{0}°С", Math.Round(item.list[0].temp.day, 1)) + "</div>";
                //+ "&deg;" + string.Format("{0}°С", Math.Round(item.list[0].temp.max, 1))
                str += "</div>";
            }
            try
            {
                Area.TimeUpdate = movieInfos[0].LastUpdate;

            }
            catch
            {

                Area.TimeUpdate = null;
            }
            Area.Str = str;
        }
    }
}
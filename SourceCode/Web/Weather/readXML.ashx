<%@ WebHandler Language="C#" Class="readXML" %>
using System;
using System.Web;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using mojoPortal.Business;

public class readXML : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string LocationId = context.Request.QueryString["id"].ToString();
        LocationId = LocationId.Trim();
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load("http://weather.yahooapis.com/forecastrss?w=" + LocationId + "&u=c");
        XmlNodeList xNodelst = xdoc.DocumentElement.SelectNodes("channel");
        string ContentHTML = "";
        GeoZone geo = GeoZone.GetByCode(LocationId);

        foreach (XmlNode xNode in xNodelst)
        {
            //xNode["yweather:location"].Attributes["city"].InnerText + "</b></span></div>";
            ContentHTML += "<div class='weather-all'><div class='weatherItems'><span class='spName'>Khu vực</span>:</span><span class='font-city'>" + geo.Name + "</span></div>";
            ContentHTML += "<div class='weatherItems'><span class='img-weather'>" + getImageWeather(LocationId) + "</span><span class='temperature'>" + xNode["yweather:wind"].Attributes["chill"].InnerText + "&deg;" + xNode["yweather:units"].Attributes["temperature"].InnerText + "</span> </div>";
            ContentHTML += "</br><span class='status-weather'><span class='spName'>Thời tiết</span>: " + GetStatusWeather(xNode["item"].ChildNodes[5].Attributes["text"].InnerText.ToUpper()) + "</span>";
            //ContentHTML += "<div class='weatherItems'><span class='spName'>Mặt trời:</span><span class='spValue'>Mọc - " + xNode["yweather:astronomy"].Attributes["sunrise"].InnerText + ", Lặn - " + xNode["yweather:astronomy"].Attributes["sunset"].InnerText + "</span></div>";
            //ContentHTML += "<div class='weatherItems'><span class='spName'>Nhiệt độ:</span><span class='spValue'></div>";
            ContentHTML += "<div class='weatherItems'><span class='spName'>Độ ẩm</span>:<span class='spValue'>" + xNode["yweather:atmosphere"].Attributes["humidity"].InnerText + "%</span></div>";
            ContentHTML += "<div class='weatherItems'><span class='spName'>Hướng gió:</span><span class='spValue'>" + getWindDirection(Convert.ToInt32(xNode["yweather:wind"].Attributes["direction"].InnerText)) + ",&nbsp;" + xNode["yweather:wind"].Attributes["speed"].InnerText + "&nbsp;" + xNode["yweather:units"].Attributes["speed"].InnerText + "</span></div>";
            ContentHTML += "<div class='weatherItems'>" + getDateFormat(xNode["lastBuildDate"].InnerText) + "</div>";

        }

        context.Response.Write(ContentHTML);
    }
    //Trạng thái thời tiết
    private string GetStatusWeather(string status)
    {
        switch (status)
        {
            case "MOSTLY CLOUDY":
                return "Trời nhiều mây";
            case "FAIR":
                return "Trời đẹp";
            case "CLOUDY":
                return "Trời âm u";
            case "LIGHT RAIN":
                return "Trời có mưa nhỏ";
            case "MIST":
                return "Trời có sương mù";
            case "PARTLY CLOUDY":
                return "Trời nắng";
            default:
                return status;
        }
    }
    // Định dạng ngày giờ
    private string getDateFormat(string date)
    {
        if (!String.IsNullOrEmpty(date))
        {
            //DateTime d = Convert.ToDateTime(date);
            string fm = date.Replace("ICT", ""); ;
            DateTime dt = Convert.ToDateTime(fm);
            string Datefm = String.Format("{0:dd/MM/yyyy}", dt);
            return "<span class='spName'>Cập nhập</span>: " + String.Format("{0:HH:mm}", dt) + " ngày " + Datefm;
        }
        return "";
    }


    //Lấy hình ảnh
    private string getImageWeather(string locationId)
    {
        try
        {
            string ContentHTML = "";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("http://weather.yahooapis.com/forecastrss?w=" + locationId + "&u=c");
            XmlNode xNode = xdoc.DocumentElement.SelectSingleNode("channel/item/description");
            string matchString = Regex.Match(xNode.InnerText, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            ContentHTML = "<img alt='thuyvk.com' src='" + matchString + "'/>";
            return ContentHTML;
        }
        catch
        {
            return null;
        }
    }
    //Lấy hướng gió
    private string getWindDirection(int w)
    {
        try
        {
            string direct = "";
            if (w == 0)
            {
                direct = "Không xác định";
            }
            else if ((w >= 355 && w < 360) || (w > 0 && w <= 5))
            {
                direct = "Bắc";
            }
            else if (w > 5 && w <= 40)
            {
                direct = "Bắc đông bắc";
            }
            else if (w > 40 && w <= 50)
            {
                direct = "Đông bắc";
            }
            else if (w > 50 && w <= 85)
            {
                direct = "Đông đông bắc";
            }
            else if (w > 85 && w <= 95)
            {
                direct = "Đông";
            }
            else if (w > 95 && w <= 130)
            {
                direct = "Đông đông nam";
            }
            else if (w > 130 && w <= 140)
            {
                direct = "Đông nam";
            }
            else if (w > 140 && w <= 175)
            {
                direct = "Nam đông nam";
            }
            else if (w > 175 && w <= 185)
            {
                direct = "Nam";
            }
            else if (w > 185 && w <= 220)
            {
                direct = "Nam tây nam";
            }
            else if (w > 220 && w <= 230)
            {
                direct = "Tây nam";
            }
            else if (w > 230 && w <= 265)
            {
                direct = "Tây tây nam";
            }
            else if (w > 265 && w <= 275)
            {
                direct = "Tây";
            }
            else if (w > 275 && w <= 310)
            {
                direct = "Tây tây bắc";
            }
            else if (w > 310 && w <= 320)
            {
                direct = "Tây bắc";
            }
            else if (w > 320 && w < 355)
            {
                direct = "Bắc tây bắc";
            }
            return direct;
        }
        catch
        {
            return "không xác định";
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
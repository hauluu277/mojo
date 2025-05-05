using mojoPortal.Service.Business;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace mojoPortal.Web
{
    public partial class ckns : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string data = createXML();
            //Clear page
            Response.ContentType = "text/xml";
            Response.Write(data);
            Response.End();
        }
        public string createXML()
        {
 
            var result = string.Empty;
            XElement xRoot = new XElement("reports");
            XElement reportDuToan = new XElement("report-dutoan");
            xRoot.Add(reportDuToan);
            XElement title = new XElement("title", "Dự toán ngân sách đã được cấp có thẩm quyền giao");
            reportDuToan.Add(title);
            XElement link = new XElement("link", ConfigurationManager.AppSettings["domain"] + "cong-khai-ngan-sach");
            reportDuToan.Add(link);
            XElement description = new XElement("description", "Dự toán ngân sách đã được cấp có thẩm quyền giao");
            reportDuToan.Add(description);
            XElement pubDate = new XElement("pubDate", string.Format("{0: ddd, dd/MM,yyyy HH:mm}", DateTime.Now));
            reportDuToan.Add(pubDate);
            XElement generator = new XElement("generator", "monre_ad");
            reportDuToan.Add(generator);

            //generate công khai ngân sách
            var year = WebUtils.ParseInt32FromQueryString("year", -1);
            var cateid = WebUtils.ParseInt32FromQueryString("cateid", -1);
            BaoCaoBusiness baoCaoBusiness = new BaoCaoBusiness(new mojoportal.Service.UoW.UnitOfWork());
            var listData = baoCaoBusiness.context.md_BaoCao.Where(x => x.IsPublish == true && x.NamChuKyBaoCao == year && x.LinhVucID == cateid).OrderByDescending(x => x.NgayCongBo).ToList();

            foreach (var item in listData)
            {
                XElement xItem = new XElement("item");
                reportDuToan.Add(xItem);

                XElement xTitle = new XElement("title", item.TenBaoCao);
                xItem.Add(xTitle);

                XElement xLink = new XElement("link", ConfigurationManager.AppSettings["domain"] + (!string.IsNullOrEmpty(item.ItemUrl) ? item.ItemUrl.Replace("~", string.Empty) : string.Empty));
                xItem.Add(xLink);

                XElement xDescription = new XElement("description", item.TenBaoCao);
                xItem.Add(xDescription);


                XElement reportyear = new XElement("reportyear", item.NamChuKyBaoCao);
                xItem.Add(reportyear);

                XElement qdDate = new XElement("qdDate", item.CreatedDate);
                xItem.Add(qdDate);

                XElement qdnumber = new XElement("qdnumber", item.SoQuyetDinhCongBo);
                xItem.Add(qdnumber);

                XElement xPubDate = new XElement("pubDate", item.NgayCongBo);
                xItem.Add(xPubDate);

                if (!string.IsNullOrEmpty(item.PathFile))
                {
                    XElement file = new XElement("files", ConfigurationManager.AppSettings["domain"] + item.PathFile);
                    xItem.Add(file);
                }
                else
                {
                    XElement file = new XElement("files");
                    xItem.Add(file);
                }

                XElement guid = new XElement("guid", item.ItemID);
                xItem.Add(guid);
            }
            result = xRoot.ToString();
            return result;
        }
    }
}
using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using MySql.Data.MySqlClient;
using SurveyFeature.Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace mojoportal.ConvertData
{
    internal class Program
    {
        private static string connStrMysql = "server=localhost;user=root;database=db_bentre;port=3306;password=12345678";
        private static MySqlConnection connMysql = new MySqlConnection(connStrMysql);

        private static string connStrSqlSv = "data source=192.168.1.11;initial catalog=PortalBenTre_2908; User Id=admin; Password=12345678;MultipleActiveResultSets=true";
        private static SqlConnection connSqlSv = new SqlConnection(connStrSqlSv);
        static void Main(string[] args)
        {
            //Mapping();
            //Mapping2();
            //Mapping3();
            //MappingArticleFile();
            MappingFileDocument();
        }
        private static void MappingFileDocument()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL + SQL Server...");
                connMysql.Open();

                connSqlSv.Open();
                var sqlQuery = "select * from md_Document where FilePath like '%?%'";
                SqlCommand command = new SqlCommand(sqlQuery, connSqlSv);

                var listDocumentFile = new List<DocumentFile>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var summary = reader[4].ToString();
                        var id = int.Parse(reader[0].ToString());
                        var filePath = reader[9].ToString();

                        listDocumentFile.Add(new DocumentFile()
                        {
                            filePath = filePath,
                            id = id,
                            summary = summary
                        });
                    }
                }

                var queryGetOld = new StringBuilder();
                queryGetOld.Append("SELECT * FROM db_bentre.sgdjos_docman where dmname in (");
                var count = listDocumentFile.Count() - 1;
                foreach (var item in listDocumentFile)
                {
                    if (listDocumentFile.IndexOf(item) == count)
                    {
                        queryGetOld.Append("N'" + item.summary + "'");
                    }
                    else
                    {
                        queryGetOld.Append("N'" + item.summary + "',");
                    }
                }
                queryGetOld.Append(")");

                MySqlCommand cmd = new MySqlCommand(queryGetOld.ToString(), connMysql);
                MySqlDataReader rdr = cmd.ExecuteReader();

                //read the data
 
                while (rdr.Read())
                {
                    var filename = rdr[6].ToString();
                    var dmname = rdr[2].ToString();
                    var getData = listDocumentFile.Where(x => x.summary.Equals(dmname)).FirstOrDefault();
                    if(getData != null)
                    {
                        //insert CORE_DocumentFile
                        var queryUpdate = string.Format("INSERT INTO core_DocumentFile (DocumentID,NameFile) VALUES ({0},N'{1}')", getData.id, filename);

                        SqlCommand commandUpdate = new SqlCommand(queryUpdate, connSqlSv);
                        commandUpdate.ExecuteScalar();
                    }
                };

            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            connMysql.Close();
            connSqlSv.Close();
            Console.WriteLine("Done");
            Console.Read();
        }
        class DocumentFile
        {
            public int id { get; set; }
            public string summary { get; set; }
            public string filePath { get; set; }

            public string dmname { get; set; }
            public string filename { get; set; }
        }
        private static void MappingArticleFile()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL + SQL Server...");
                connSqlSv.Open();

                var sqlQuery = "select * from md_FileAttachment where FilePath like '%?%'";
                SqlCommand command = new SqlCommand(sqlQuery, connSqlSv);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = int.Parse(reader[0].ToString());
                        var fileName = reader[3].ToString();
                        var filePath = reader[4].ToString();
                        var path = filePath.Split('_')[0];

                        var fileUpdate = path + "_" + fileName;
                        var queryUpdate = string.Format("update md_FileAttachment set FilePath = N'{0}' where ItemId={1}", fileUpdate, id);

                        SqlCommand commandUpdate = new SqlCommand(queryUpdate, connSqlSv);
                        commandUpdate.ExecuteScalar();

                    }
                }


            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            connSqlSv.Close();
            Console.WriteLine("Done");
            Console.Read();
        }

        private static void Mapping()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL + SQL Server...");
                connMysql.Open();
                connSqlSv.Open();

                string sql = "select * from sgdjos_docman where catid = 9427";
                MySqlCommand cmd = new MySqlCommand(sql, connMysql);
                MySqlDataReader rdr = cmd.ExecuteReader();

                var listUser = new Dictionary<int, string>();
                listUser.Add(191, "912EF0FE-EFCB-4D21-8672-79594A1EC864");
                listUser.Add(297, "F9D5B4BE-9240-45D5-BD34-7529B91BD631");
                listUser.Add(336, "4B55C106-4680-4A8C-B78F-DB2B08665856");
                listUser.Add(354, "7AE9AC57-2FFA-462A-9B2D-B6168D2F4575");
                listUser.Add(358, "50DDA3A9-5DC8-40CB-8789-041151A4F354");
                listUser.Add(359, "C9A97F16-6FED-4803-8403-1226E5D283C2");
                listUser.Add(365, "B6FC9C0A-E375-4829-A8CE-FB137216ABF5");
                listUser.Add(368, "F2125720-F8B1-44B4-A12E-62BC25EC8ED2");
                listUser.Add(1375, "572442DE-1463-41BD-8AD9-E2CE5FECE58B");
                listUser.Add(10028, "A1990A4E-5389-4BA3-9EDB-E3C1EA3B6869");
                listUser.Add(10039, "C23885F3-7391-41DE-9FCB-0C2B581C54E6");
                listUser.Add(10040, "87BBD460-5B62-4722-B748-2A91ED03A37F");
                listUser.Add(10049, "009AC751-ABCB-49B1-A344-C8E424D5E08B");
                listUser.Add(10051, "EB0C8C2D-3ED4-4E07-B18B-2EA54BF4E0B7");
                listUser.Add(10052, "0B7B9F03-C2CF-458B-AC42-FAB3044690EC");
                listUser.Add(10053, "D151773B-910E-4769-9068-2BE1A65A5412");
                listUser.Add(10054, "2CEF8627-8A0E-4251-B5B3-B5E244E8C86C");
                listUser.Add(10055, "2571DEB7-5B2A-424D-BFE2-8467907215FD");
                listUser.Add(10056, "928268FD-1989-46F5-8E1C-D0C2DEFA56E5");
                listUser.Add(10057, "67E14ABE-A384-4901-BF89-0EA93870E217");
                listUser.Add(10058, "E64F6C62-46BC-4D38-A5E7-E9292913F14B");
                listUser.Add(10059, "B6264DC5-9425-4DA9-9DFE-25DA41EAA49E");
                listUser.Add(10060, "DE88318E-CE4A-492E-95DE-69B9722D79BA");
                listUser.Add(10061, "B672C2EC-BFF4-4534-97B3-70AA8C18CB65");
                listUser.Add(10062, "182E2771-5324-4573-BC3E-DA1777039EDA");
                listUser.Add(10063, "63F58621-F37B-4BA6-B76B-90D89728D80D");
                listUser.Add(10064, "8434EFE8-DCF7-4E1F-A967-75630E987417");
                listUser.Add(10065, "8424AE99-56F3-4233-8E6A-B1B1C59D08D8");
                listUser.Add(10066, "C644F5DC-27A3-4B7B-BF86-00BCAEED8B71");
                listUser.Add(10067, "E52B324D-0985-4C2E-81DA-E85520D2FDDB");
                listUser.Add(10068, "27F717D1-601F-42DB-A59C-38FE59740C26");
                listUser.Add(10069, "DDC3129F-B2BB-44C9-8A4E-6E1C73D50A7A");
                listUser.Add(10070, "849FA279-8EA5-46B9-8EC0-3703E9AF3B49");
                listUser.Add(10071, "5C821A07-20D0-4FB2-A731-F5E2F2E2AA7D");
                listUser.Add(10072, "A4E686CD-B5FF-4799-8478-F5FF617B7171");
                listUser.Add(10073, "DD449A0E-1474-4992-92E5-6AF7DF70B4B3");
                listUser.Add(10074, "0707107F-2ACD-406D-B370-14F3BFCCBDD8");
                listUser.Add(10075, "9297B7FD-CA62-4FE7-9630-6E8783A46CC5");
                listUser.Add(10076, "6E6808B2-4839-4A8D-8C5A-EF65A07E2179");
                listUser.Add(10077, "E88B5F27-642E-4F84-AF01-DEFC07133E2D");
                listUser.Add(10078, "037ACBF6-BAB1-4068-93B8-78EA53E9E36C");
                listUser.Add(10079, "8C95CDB8-0134-4801-BB4F-9755B3FA5E5E");
                listUser.Add(10080, "9A3E3347-5F5F-4583-938F-8B1D3A5704D4");
                listUser.Add(10081, "37A6BEF1-162D-47CB-A239-09EBF4BA508F");
                listUser.Add(10082, "118418A3-9C4C-4F51-84F4-2AF9C2CEF812");
                listUser.Add(10083, "686DBEB5-1308-4994-B7C3-8F1F42F77D16");

                //read the data
                var siteSettings = new SiteSettings(1);
                while (rdr.Read())
                {
                    var ModuleID = -1;
                    var PageID = 1;
                    var SiteID = 261;
                    var Summary = rdr[2].ToString().Replace("\"", "&#34;").Replace("'", "&#39;");
                    var ContentDoc = rdr[3].ToString().Replace("'", "&#39;");
                    var Type = int.Parse(rdr[1].ToString()); //LinhVuc
                    var CoQuanID = 8255;
                    var LoaiVB = 1;

                    var strUrl = Summary.Length > 120 ? Summary.Substring(0, 120) : Summary;
                    var url = "~/" + SiteUtils.SuggestFriendlyUrlFix(strUrl, siteSettings);

                    if (Summary.ToLower().Contains("công văn") || Summary.ToLower().Contains("cv"))
                    {
                        LoaiVB = 946;
                    }
                    else if (Summary.ToLower().Contains("quyết định") || Summary.ToLower().Contains("qđ"))
                    {
                        LoaiVB = 947;
                    }
                    else if (Summary.ToLower().Contains("kế hoạch"))
                    {
                        LoaiVB = 965;
                    }
                    else if (Summary.ToLower().Contains("báo cáo"))
                    {
                        LoaiVB = 948;
                    }
                    else if (Summary.ToLower().Contains("thông báo"))
                    {
                        LoaiVB = 951;
                    }
                    else if (Summary.ToLower().Contains("thông tư"))
                    {
                        LoaiVB = 950;
                    }
                    else if (Summary.ToLower().Contains("chỉ thị"))
                    {
                        LoaiVB = 949;
                    }
                    else if (Summary.ToLower().Contains("nghị quyết") || Summary.ToLower().Contains("nq"))
                    {
                        LoaiVB = 9325;
                    }
                    else if (Summary.ToLower().Contains("hướng dẫn"))
                    {
                        LoaiVB = 954;
                    }

                    //regex ngày tháng
                    string pattern = @"\d{1,2}\/\d{1,2}\/\d{2,4}";
                    var strDate = Regex.Match(Summary, pattern).Value;
                    DateTime? dateTime = null;
                    try
                    {
                        if (string.IsNullOrEmpty(strDate))
                        {
                            dateTime = DateTime.Parse(strDate);
                        }
                    }
                    catch
                    {

                        dateTime = null;
                    }
                    var filePath = rdr[6].ToString();
                    var IsApproved = rdr[12].ToString().Contains("True") ? 1 : 0;
                    var ApprovedDate = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(rdr[4].ToString()));
                    var LangID = -1;
                    var FTS = ConvertToVN(Summary);
                    var UserId = int.Parse(rdr[16].ToString());
                    var CreatedByUserGuid = Guid.Parse(listUser[UserId]);
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("INSERT INTO md_Document ");
                    strBuilder.Append("(ModuleID, PageID, SiteID, Summary, FilePath, CoQuanID, LoaiVB, LinhVuc, ItemUrl, IsApproved, ApprovedDate, LangID, FTS, ContentDoc, Type, CreatedByUserGuid) VALUES ");
                    strBuilder.Append("(" + ModuleID + ", " + PageID + ", " + SiteID + ", N'" + Summary + "', '" + filePath + "', " + CoQuanID + ", " + LoaiVB + ", " + Type + ", '" + url + "', " + IsApproved + ", '" + ApprovedDate + "', " + LangID + ", '" + FTS + "', N'" + rdr[0].ToString() + "', " + Type + ", '" + CreatedByUserGuid + "')");
                    string sqlQuery = strBuilder.ToString();
                    SqlCommand command = new SqlCommand(sqlQuery, connSqlSv);
                    command.ExecuteNonQuery();
                    Console.WriteLine(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            connMysql.Close();
            connSqlSv.Close();
            Console.WriteLine("Done");
            Console.Read();
        }

        private static void Mapping2()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL + SQL Server...");
                connMysql.Open();
                connSqlSv.Open();

                string sql = "select * from sgdjos_content where catid = 9461";
                MySqlCommand cmd = new MySqlCommand(sql, connMysql);
                MySqlDataReader rdr = cmd.ExecuteReader();
                var listUser = new Dictionary<int, string>();
                listUser.Add(191, "912EF0FE-EFCB-4D21-8672-79594A1EC864");
                listUser.Add(297, "F9D5B4BE-9240-45D5-BD34-7529B91BD631");
                listUser.Add(336, "4B55C106-4680-4A8C-B78F-DB2B08665856");
                listUser.Add(354, "7AE9AC57-2FFA-462A-9B2D-B6168D2F4575");
                listUser.Add(358, "50DDA3A9-5DC8-40CB-8789-041151A4F354");
                listUser.Add(359, "C9A97F16-6FED-4803-8403-1226E5D283C2");
                listUser.Add(365, "B6FC9C0A-E375-4829-A8CE-FB137216ABF5");
                listUser.Add(368, "F2125720-F8B1-44B4-A12E-62BC25EC8ED2");
                listUser.Add(1375, "572442DE-1463-41BD-8AD9-E2CE5FECE58B");
                listUser.Add(10028, "A1990A4E-5389-4BA3-9EDB-E3C1EA3B6869");
                listUser.Add(10039, "C23885F3-7391-41DE-9FCB-0C2B581C54E6");
                listUser.Add(10040, "87BBD460-5B62-4722-B748-2A91ED03A37F");
                listUser.Add(10049, "009AC751-ABCB-49B1-A344-C8E424D5E08B");
                listUser.Add(10051, "EB0C8C2D-3ED4-4E07-B18B-2EA54BF4E0B7");
                listUser.Add(10052, "0B7B9F03-C2CF-458B-AC42-FAB3044690EC");
                listUser.Add(10053, "D151773B-910E-4769-9068-2BE1A65A5412");
                listUser.Add(10054, "2CEF8627-8A0E-4251-B5B3-B5E244E8C86C");
                listUser.Add(10055, "2571DEB7-5B2A-424D-BFE2-8467907215FD");
                listUser.Add(10056, "928268FD-1989-46F5-8E1C-D0C2DEFA56E5");
                listUser.Add(10057, "67E14ABE-A384-4901-BF89-0EA93870E217");
                listUser.Add(10058, "E64F6C62-46BC-4D38-A5E7-E9292913F14B");
                listUser.Add(10059, "B6264DC5-9425-4DA9-9DFE-25DA41EAA49E");
                listUser.Add(10060, "DE88318E-CE4A-492E-95DE-69B9722D79BA");
                listUser.Add(10061, "B672C2EC-BFF4-4534-97B3-70AA8C18CB65");
                listUser.Add(10062, "182E2771-5324-4573-BC3E-DA1777039EDA");
                listUser.Add(10063, "63F58621-F37B-4BA6-B76B-90D89728D80D");
                listUser.Add(10064, "8434EFE8-DCF7-4E1F-A967-75630E987417");
                listUser.Add(10065, "8424AE99-56F3-4233-8E6A-B1B1C59D08D8");
                listUser.Add(10066, "C644F5DC-27A3-4B7B-BF86-00BCAEED8B71");
                listUser.Add(10067, "E52B324D-0985-4C2E-81DA-E85520D2FDDB");
                listUser.Add(10068, "27F717D1-601F-42DB-A59C-38FE59740C26");
                listUser.Add(10069, "DDC3129F-B2BB-44C9-8A4E-6E1C73D50A7A");
                listUser.Add(10070, "849FA279-8EA5-46B9-8EC0-3703E9AF3B49");
                listUser.Add(10071, "5C821A07-20D0-4FB2-A731-F5E2F2E2AA7D");
                listUser.Add(10072, "A4E686CD-B5FF-4799-8478-F5FF617B7171");
                listUser.Add(10073, "DD449A0E-1474-4992-92E5-6AF7DF70B4B3");
                listUser.Add(10074, "0707107F-2ACD-406D-B370-14F3BFCCBDD8");
                listUser.Add(10075, "9297B7FD-CA62-4FE7-9630-6E8783A46CC5");
                listUser.Add(10076, "6E6808B2-4839-4A8D-8C5A-EF65A07E2179");
                listUser.Add(10077, "E88B5F27-642E-4F84-AF01-DEFC07133E2D");
                listUser.Add(10078, "037ACBF6-BAB1-4068-93B8-78EA53E9E36C");
                listUser.Add(10079, "8C95CDB8-0134-4801-BB4F-9755B3FA5E5E");
                listUser.Add(10080, "9A3E3347-5F5F-4583-938F-8B1D3A5704D4");
                listUser.Add(10081, "37A6BEF1-162D-47CB-A239-09EBF4BA508F");
                listUser.Add(10082, "118418A3-9C4C-4F51-84F4-2AF9C2CEF812");
                listUser.Add(10083, "686DBEB5-1308-4994-B7C3-8F1F42F77D16");

                //read the data

                var ModuleID = -1;
                var SiteID = 262;
                var siteSettings = new SiteSettings(SiteID);
                var module = new Module(ModuleID);
                var LangID = -1;
                var DicFile = new Dictionary<int, int>();
                while (rdr.Read())
                {
                    var CategoryID = int.Parse(rdr[9].ToString());
                    var Title = rdr[1].ToString().Replace("'", "&#39;");
                    var Summary = rdr[4].ToString();
                    var Description = "";
                    //var Description = HttpUtility.HtmlEncode(Summary + " " + rdr[5].ToString());

                    string ImageUrl = Regex.Match(Description, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    var HitCount = int.Parse(rdr[28].ToString());

                    var strUrl = Title.Length > 120 ? Title.Substring(0, 120) : Title;
                    var url = "~/" + SiteUtils.SuggestFriendlyUrlFix(strUrl, siteSettings);

                    //var filePath = rdr[6].ToString();
                    var IsApproved = rdr[12].ToString().Contains("True") ? 1 : 0;
                    var ApprovedDate = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(rdr[17].ToString()));
                    var CreateDateArticle = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(rdr[10].ToString()));

                    var FTS = ConvertToVN(Title);
                    var UserId = int.Parse(rdr[11].ToString());
                    var UserGuid = Guid.Parse(listUser[UserId]);
                    var SapoFTS = "";
                    var ArticleGuid = Guid.NewGuid();
                    var IsPublished = int.Parse(rdr[6].ToString()) == 1 ? 1 : 0;

                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("INSERT INTO md_Articles ");
                    strBuilder.Append("(ModuleID, " +
                        "SiteID, " +
                        "CategoryID, " +
                        "Title, " +
                        "Description, " +
                        "ImageUrl, " +
                        "IsApproved, " +
                        "ApprovedDate, " +
                        "PublishedDate, " +
                        "CreateDateArticle, " +
                        "LangID, " +
                        "FTS, " +
                        "UserGuid, " +
                        "ItemUrl, " +
                        "MetaIdentifier, " +
                        "TitleFTS, " +
                        "SapoFTS, " +
                        "IsCongThanhVien, " +
                        "ViTriHienThiNgayDang, " +
                        "IsHienThiTacGia, " +
                        "IsPublished, " +
                        "ArticleGuid," +
                        "StartDate," +
                        "ModuleGuid," +
                        "CreatedDate," +
                        "ApprovedGuid," +
                        "AllowComment," +
                        "PublishedGuid," +
                        "MetaTitle) VALUES ");
                    strBuilder.Append("(" +
                        "" + ModuleID + ", " +
                        "" + SiteID + ", " +
                        "" + CategoryID + ", " +
                        "N'" + Title + "', " +
                        "N'" + Description + "', " +
                        "'" + ImageUrl + "', " +
                        "1, " +
                        "'" + ApprovedDate + "', " +
                        "'" + ApprovedDate + "', " +
                        "'" + CreateDateArticle + "', " +
                        "" + LangID + ", " +
                        "'" + FTS + "', " +
                        "'" + UserGuid + "'," +
                        "'" + url + "'," +
                        "'" + url + "'," +
                        "'" + FTS + "'," +
                        "'" + SapoFTS + "'," +
                        "0," +
                        "'left'," +
                        "1," +
                        "" + IsPublished + "," +
                        "'" + ArticleGuid + "', " +
                        "'" + ApprovedDate + "', " +
                        "'" + Guid.NewGuid() + "', " +
                        "'" + ApprovedDate + "', " +
                        "'" + Guid.NewGuid() + "', " +
                        "1, " +
                        "'" + Guid.NewGuid() + "', " +
                        "'" + Title + "')");
                    string sqlQuery = strBuilder.ToString() + " Select Scope_Identity()";
                    SqlCommand command = new SqlCommand(sqlQuery, connSqlSv);
                    int id = Convert.ToInt32(command.ExecuteScalar());

                    var RealUrl = "~/Article/ViewPost.aspx?pageid=-1&mid=-1"
                                      + "&ItemID=" + id.ToString(CultureInfo.InvariantCulture);
                    var ItemGuid = Guid.NewGuid();
                    //lưu đường dẫn Friendly
                    StringBuilder strFriendlyUrl = new StringBuilder();
                    strFriendlyUrl.Append("INSERT INTO mp_FriendlyUrls ");
                    strFriendlyUrl.Append("(SiteId, SiteGuid, PageGuid, ItemGuid, FriendlyUrl, RealUrl) VALUES ");
                    strFriendlyUrl.Append("(" + SiteID + ", '" + siteSettings.SiteGuid + "', '" + ArticleGuid + "', '" + ItemGuid + "', '" + url.Replace("~/", "") + "', '" + RealUrl + "')");
                    string sqlUrl = strFriendlyUrl.ToString();
                    SqlCommand cmdUrl = new SqlCommand(sqlUrl, connSqlSv);
                    cmdUrl.ExecuteNonQuery();

                    //Lưu danh mục
                    StringBuilder strCat = new StringBuilder();
                    strCat.Append("INSERT INTO md_ArticleCategory ");
                    strCat.Append("(ArticleID, CategoryID, SiteID) VALUES ");
                    strCat.Append("(" + id + ", '" + CategoryID + "', '" + SiteID + "')");
                    string sqlCat = strCat.ToString();

                    Console.WriteLine(rdr[0].ToString());

                    SqlCommand cmdCat = new SqlCommand(sqlCat, connSqlSv);
                    cmdCat.ExecuteNonQuery();

                    //Lưu tệp đính kèm
                    var idContent = int.Parse(rdr[0].ToString());
                    DicFile.Add(idContent, id);
                }
                rdr.Close();

                //Lưu tệp đính kèm
                foreach (KeyValuePair<int, int> entry in DicFile)
                {
                    string sqlAttachment = "select * from sgdjos_attachments where article_id = " + entry.Key;
                    MySqlCommand cmdAttachment = new MySqlCommand(sqlAttachment, connMysql);
                    MySqlDataReader rdrAttachment = cmdAttachment.ExecuteReader();
                    if (rdrAttachment.HasRows)
                    {
                        while (rdrAttachment.Read())
                        {
                            var FileName = rdrAttachment[1].ToString();
                            var StrPath = rdrAttachment[2].ToString();
                            string[] pathArr = StrPath.Split('/');
                            var FilePath = "Data/File/" + pathArr.Last();

                            var TypeItem = 2;
                            var FileExtensions = Path.GetExtension(FilePath);
                            var ObjectID = entry.Value;
                            var DownloadCount = int.Parse(rdrAttachment[17].ToString());
                            var CreatedDate = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(rdrAttachment[15].ToString()));

                            StringBuilder strFile = new StringBuilder();
                            strFile.Append("INSERT INTO md_FileAttachment ");
                            strFile.Append("(ObjectID, TypeItem, FileName, FilePath, FileExtensions, DownloadCount, CreatedDate) VALUES ");
                            strFile.Append("(" + ObjectID + ", " + TypeItem + ", N'" + FileName + "', N'" + FilePath + "', '" + FileExtensions + "', '" + DownloadCount + "', '" + CreatedDate + "')");
                            string sqlFile = strFile.ToString();
                            Console.WriteLine(entry.Key);
                            SqlCommand cmdFile = new SqlCommand(sqlFile, connSqlSv);
                            cmdFile.ExecuteNonQuery();
                        }
                    }
                    rdrAttachment.Close();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            connMysql.Close();
            connSqlSv.Close();
            Console.WriteLine("Done");
            Console.Read();
        }

        private static void Mapping3()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL + SQL Server...");
                connMysql.Open();
                connSqlSv.Open();
                var idDanhMuc = 9427;
                string sql = "select * from sgdjos_content where catid = " + idDanhMuc;
                MySqlCommand cmd = new MySqlCommand(sql, connMysql);
                MySqlDataReader rdr = cmd.ExecuteReader();

                var listUser = new Dictionary<int, string>();
                listUser.Add(191, "912EF0FE-EFCB-4D21-8672-79594A1EC864");
                listUser.Add(297, "F9D5B4BE-9240-45D5-BD34-7529B91BD631");
                listUser.Add(336, "4B55C106-4680-4A8C-B78F-DB2B08665856");
                listUser.Add(354, "7AE9AC57-2FFA-462A-9B2D-B6168D2F4575");
                listUser.Add(358, "50DDA3A9-5DC8-40CB-8789-041151A4F354");
                listUser.Add(359, "C9A97F16-6FED-4803-8403-1226E5D283C2");
                listUser.Add(365, "B6FC9C0A-E375-4829-A8CE-FB137216ABF5");
                listUser.Add(368, "F2125720-F8B1-44B4-A12E-62BC25EC8ED2");
                listUser.Add(1375, "572442DE-1463-41BD-8AD9-E2CE5FECE58B");
                listUser.Add(10028, "A1990A4E-5389-4BA3-9EDB-E3C1EA3B6869");
                listUser.Add(10039, "C23885F3-7391-41DE-9FCB-0C2B581C54E6");
                listUser.Add(10040, "87BBD460-5B62-4722-B748-2A91ED03A37F");
                listUser.Add(10049, "009AC751-ABCB-49B1-A344-C8E424D5E08B");
                listUser.Add(10051, "EB0C8C2D-3ED4-4E07-B18B-2EA54BF4E0B7");
                listUser.Add(10052, "0B7B9F03-C2CF-458B-AC42-FAB3044690EC");
                listUser.Add(10053, "D151773B-910E-4769-9068-2BE1A65A5412");
                listUser.Add(10054, "2CEF8627-8A0E-4251-B5B3-B5E244E8C86C");
                listUser.Add(10055, "2571DEB7-5B2A-424D-BFE2-8467907215FD");
                listUser.Add(10056, "928268FD-1989-46F5-8E1C-D0C2DEFA56E5");
                listUser.Add(10057, "67E14ABE-A384-4901-BF89-0EA93870E217");
                listUser.Add(10058, "E64F6C62-46BC-4D38-A5E7-E9292913F14B");
                listUser.Add(10059, "B6264DC5-9425-4DA9-9DFE-25DA41EAA49E");
                listUser.Add(10060, "DE88318E-CE4A-492E-95DE-69B9722D79BA");
                listUser.Add(10061, "B672C2EC-BFF4-4534-97B3-70AA8C18CB65");
                listUser.Add(10062, "182E2771-5324-4573-BC3E-DA1777039EDA");
                listUser.Add(10063, "63F58621-F37B-4BA6-B76B-90D89728D80D");
                listUser.Add(10064, "8434EFE8-DCF7-4E1F-A967-75630E987417");
                listUser.Add(10065, "8424AE99-56F3-4233-8E6A-B1B1C59D08D8");
                listUser.Add(10066, "C644F5DC-27A3-4B7B-BF86-00BCAEED8B71");
                listUser.Add(10067, "E52B324D-0985-4C2E-81DA-E85520D2FDDB");
                listUser.Add(10068, "27F717D1-601F-42DB-A59C-38FE59740C26");
                listUser.Add(10069, "DDC3129F-B2BB-44C9-8A4E-6E1C73D50A7A");
                listUser.Add(10070, "849FA279-8EA5-46B9-8EC0-3703E9AF3B49");
                listUser.Add(10071, "5C821A07-20D0-4FB2-A731-F5E2F2E2AA7D");
                listUser.Add(10072, "A4E686CD-B5FF-4799-8478-F5FF617B7171");
                listUser.Add(10073, "DD449A0E-1474-4992-92E5-6AF7DF70B4B3");
                listUser.Add(10074, "0707107F-2ACD-406D-B370-14F3BFCCBDD8");
                listUser.Add(10075, "9297B7FD-CA62-4FE7-9630-6E8783A46CC5");
                listUser.Add(10076, "6E6808B2-4839-4A8D-8C5A-EF65A07E2179");
                listUser.Add(10077, "E88B5F27-642E-4F84-AF01-DEFC07133E2D");
                listUser.Add(10078, "037ACBF6-BAB1-4068-93B8-78EA53E9E36C");
                listUser.Add(10079, "8C95CDB8-0134-4801-BB4F-9755B3FA5E5E");
                listUser.Add(10080, "9A3E3347-5F5F-4583-938F-8B1D3A5704D4");
                listUser.Add(10081, "37A6BEF1-162D-47CB-A239-09EBF4BA508F");
                listUser.Add(10082, "118418A3-9C4C-4F51-84F4-2AF9C2CEF812");
                listUser.Add(10083, "686DBEB5-1308-4994-B7C3-8F1F42F77D16");

                //read the data


                var ModuleID = 14273; //thay
                var SiteID = 262;
                var module = new Module(ModuleID);
                var siteSettings = new SiteSettings(SiteID);
                var LangID = -1;
                var PageID = 6363; // thay
                var DicFile = new Dictionary<int, int>();
                while (rdr.Read())
                {
                    var Type = idDanhMuc; //LinhVuc
                    var CoQuanID = string.Empty;//

                    var CategoryID = int.Parse(rdr[9].ToString());
                    var Title = rdr[1].ToString().Replace("'", "&#39;");
                    var Summary = rdr[4].ToString();
                    var Description = "";
                    //var Description = HttpUtility.HtmlEncode(Summary + " " + rdr[5].ToString());

                    string ImageUrl = Regex.Match(Description, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    var HitCount = int.Parse(rdr[28].ToString());

                    var strUrl = Title.Length > 120 ? Title.Substring(0, 120) : Title;
                    var url = "~/" + SiteUtils.SuggestFriendlyUrlFix(strUrl, siteSettings);

                    //var filePath = rdr[6].ToString();
                    var IsApproved = 1;
                    var ApprovedDate = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(rdr[17].ToString()));
                    var CreateDateArticle = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(rdr[10].ToString()));

                    var FTS = ConvertToVN(Title);
                    var UserId = int.Parse(rdr[11].ToString());
                    var UserGuid = Guid.Parse(listUser[UserId]);


                    var ArticleGuid = Guid.NewGuid();
                    var IsPublished = int.Parse(rdr[6].ToString()) == 1 ? 1 : 0;

                    var LoaiVB = 1;

                    if (Title.ToLower().Contains("công văn") || Title.ToLower().Contains("cv"))
                    {
                        LoaiVB = 946;
                    }
                    else if (Title.ToLower().Contains("quyết định") || Title.ToLower().Contains("qđ"))
                    {
                        LoaiVB = 947;
                    }
                    else if (Title.ToLower().Contains("kế hoạch"))
                    {
                        LoaiVB = 965;
                    }
                    else if (Title.ToLower().Contains("báo cáo"))
                    {
                        LoaiVB = 948;
                    }
                    else if (Title.ToLower().Contains("thông báo"))
                    {
                        LoaiVB = 951;
                    }
                    else if (Title.ToLower().Contains("thông tư"))
                    {
                        LoaiVB = 950;
                    }
                    else if (Title.ToLower().Contains("chỉ thị"))
                    {
                        LoaiVB = 949;
                    }
                    else if (Title.ToLower().Contains("nghị quyết") || Title.ToLower().Contains("nq"))
                    {
                        LoaiVB = 9325;
                    }
                    else if (Title.ToLower().Contains("hướng dẫn"))
                    {
                        LoaiVB = 954;
                    }

                    //regex ngày tháng
                    string pattern = @"\d{1,2}\/\d{1,2}\/\d{2,4}";
                    var strDate = Regex.Match(Summary, pattern).Value;
                    DateTime? dateTime = null;
                    try
                    {
                        if (string.IsNullOrEmpty(strDate))
                        {
                            dateTime = DateTime.Parse(strDate);
                        }
                    }
                    catch
                    {
                        dateTime = null;
                    }

                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("INSERT INTO md_Document ");
                    strBuilder.Append("(ModuleID, " +
                        "PageID," +
                        " SiteID, " +
                        "Summary, " +
                        "FilePath, " +
                        "CoQuanID, " +
                        "LoaiVB, " +
                        "LinhVuc, " +
                        "ItemUrl, " +
                        "IsApproved, " +
                        "ApprovedDate, " +
                        "LangID, " +
                        "FTS, " +
                        "ContentDoc, " +
                        "Type, " +
                        "CreatedByUserGuid) VALUES ");
                    strBuilder.Append("(" + ModuleID + ", " +
                        "" + PageID + ", " +
                        "" + SiteID + ", " +
                        "N'" + Title + "', " +
                        "'', " +
                        //"" + CoQuanID + ", " +
                        "null, " +
                        "" + LoaiVB + ", " +
                        "" + Type + ", " +
                        "'" + url + "', " +
                        "" + IsApproved + ", " +
                        "'" + ApprovedDate + "', " +
                        "" + LangID + ", " +
                        "'" + FTS + "', " +
                        "N'" + Description + "', " +
                        "" + Type + ", " +
                        "'" + UserGuid + "')");
                    string sqlQuery = strBuilder.ToString() + " Select Scope_Identity()";
                    SqlCommand command = new SqlCommand(sqlQuery, connSqlSv);
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    //Lưu tệp đính kèm
                    var idContent = int.Parse(rdr[0].ToString());
                    DicFile.Add(idContent, id);
                }
                rdr.Close();

                foreach (KeyValuePair<int, int> entry in DicFile)
                {
                    string sqlAttachment = "select * from sgdjos_attachments where article_id = " + entry.Key;
                    MySqlCommand cmdAttachment = new MySqlCommand(sqlAttachment, connMysql);
                    MySqlDataReader rdrAttachment = cmdAttachment.ExecuteReader();
                    if (rdrAttachment.HasRows)
                    {
                        while (rdrAttachment.Read())
                        {
                            var FileName = rdrAttachment[1].ToString();
                            var StrPath = rdrAttachment[2].ToString();
                            string[] pathArr = StrPath.Split('/');
                            var FilePath = "Data/File/" + pathArr.Last();

                            var TypeItem = 2;
                            var FileExtensions = Path.GetExtension(FilePath);
                            var ObjectID = entry.Value;
                            var DownloadCount = int.Parse(rdrAttachment[17].ToString());
                            var CreatedDate = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(rdrAttachment[15].ToString()));

                            StringBuilder strFile = new StringBuilder();
                            strFile.Append("INSERT INTO md_FileAttachment ");
                            strFile.Append("(ObjectID, TypeItem, FileName, FilePath, FileExtensions, DownloadCount, CreatedDate) VALUES ");
                            strFile.Append("(" + ObjectID + ", " + TypeItem + ", N'" + FileName + "', N'" + FilePath + "', '" + FileExtensions + "', '" + DownloadCount + "', '" + CreatedDate + "')");
                            string sqlFile = strFile.ToString();
                            Console.WriteLine(entry.Key);
                            SqlCommand cmdFile = new SqlCommand(sqlFile, connSqlSv);
                            cmdFile.ExecuteNonQuery();

                            StringBuilder strDoc = new StringBuilder();
                            strDoc.Append("UPDATE md_Document SET FilePath = '" + FilePath + "' WHERE ItemID = " + entry.Value);
                            string sqlDoc = strDoc.ToString();
                            SqlCommand cmdDoc = new SqlCommand(sqlDoc, connSqlSv);
                            cmdDoc.ExecuteNonQuery();
                        }
                    }
                    rdrAttachment.Close();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            connMysql.Close();
            connSqlSv.Close();
            Console.WriteLine("Done");
            Console.Read();
        }

        private static string ConvertToVN(string chucodau)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            while ((index = chucodau.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(chucodau[index]);
                chucodau = chucodau.Replace(chucodau[index], ReplText[index2]);
            }
            return chucodau;
        }
    }
}

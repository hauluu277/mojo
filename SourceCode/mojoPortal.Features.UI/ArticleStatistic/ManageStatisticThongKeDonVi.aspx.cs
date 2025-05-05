using AjaxControlToolkit.Bundling;
using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.UI.Article.Components;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class ManageStatisticThongKeDonVi : mojoBasePage
    {
        protected ArticleConfiguration config = new ArticleConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private static int siteCategory = -1;
        private static TimeZoneInfo _timeZone;
        protected static Double _timeOffset;
        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            Load += Page_Load;

            base.OnInit(e);
            LoadSettings();
        }





        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!ArticleHelper.HasRoleArticle())
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();



            //if (!userCanEdit)
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //}

            PopulateLabels();


            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            BindCategory();
            BindMonthAndYear();
        }
        private List<CoreCategory> BindDataCategory(int categoryParentId)
        {
            List<CoreCategory> categories = new List<CoreCategory>();

            CoreCategory defaultCat = new CoreCategory(categoryParentId);
            categories.Add(defaultCat);
            PopulateChildItem(categories, 0);
            categories.Remove(defaultCat);
            return categories;
        }
        private static List<CoreCategory> BindStaticDataCategory()
        {
            List<CoreCategory> categories = new List<CoreCategory>();

            CoreCategory defaultCat = new CoreCategory(siteCategory);
            categories.Add(defaultCat);
            PopulateStaticChildItem(categories, 0);
            categories.Remove(defaultCat);
            return categories;
        }

        private static List<SiteUser> ListUser(int siteId)
        {
            var lstdata = new List<SiteUser>();

            return lstdata;
        }

        private void BindCategory()
        {
            var listSite = SiteSettings.GetListAllSite();
            var listCategory = BindDataCategory(siteSettings.ArticleCategory);
            var listAuthor = SiteUser.GetUserBySite(SiteId);


            


        }
        private void BindMonthAndYear()
        {
            List<ListItem> listYear = new List<ListItem>();
            var yearCurrent = DateTime.Now.Year;
            for (int i = 2015; i <= yearCurrent; i++)
            {
                listYear.Add(new ListItem { Value = i.ToString(), Text = "Năm " + i, Selected = (yearCurrent == i) });
            }
            
            //ddlYear.Items.Insert(0, new ListItem { Text = "Chọn Năm", Value = "0" });
            List<ListItem> listMonth = new List<ListItem>();
            var monthCurrent = DateTime.Now.Month;
            for (int i = 1; i <= 12; i++)
            {
                listMonth.Add(new ListItem { Value = i.ToString(), Text = string.Format("Tháng {0}", i) });
            }
            
            //ddlMonth.Items.Insert(0, new ListItem { Text = "Chọn tháng", Value = "0" });
        }


        [WebMethod]
        public static List<object> StaticArticleTab1(int year = 0, int month = 0, string categories = "", string siteid = "")
        {
            List<object> iData = new List<object>();
            List<string> labels = new List<string>();
            List<int> data = new List<int>();
            string title = string.Empty;
            var yearCurrent = DateTime.Now.Year;
            var yearMonth = string.Empty;
            var totalArticle = new List<StatisticArticle>();
            //Nếu chọn năm và tháng thì thống kê tin bài theo các ngày trong tháng của năm đã chọn

            var listArticle = ArticleStatisticBO.GetStaticForTab1(year, month, categories, siteid);
            var totalDay = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= totalDay; day++)
            {
                DateTime dateCurrent = new DateTime(year, month, day, 0, 0, 0);
                labels.Add(string.Format("{0:dddd - dd/MM/yyyy}", dateCurrent));
                data.Add(listArticle.Where(x => x.startDate.HasValue && x.startDate.Value == dateCurrent).Count());
            }
            title = string.Format("Thống kê số lượng tin bài đăng trong tháng {0} năm {1}", month, year);

            iData.Add(labels);
            iData.Add(data);
            iData.Add(title);
            //new JavaScriptSerializer().Serialize(totalArticle);
            return iData;
        }




        /// <summary>
        /// Thống kê tin bài theo danh mục lọc theo datetime
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<object> ArticaleTotalForTab2(string startDate = "", string endDate = "", string categories = "", string userGuid = "", string siteid = "")
        {
            List<object> iData = new List<object>();
            List<int> data = new List<int>();
            List<string> labels = new List<string>();
            List<ArticleStatisticBO> lstArticle = new List<ArticleStatisticBO>();
            DateTime? _startDate = null;
            DateTime? _endDate = null;
            System.Text.StringBuilder _title = new System.Text.StringBuilder();

            _title.Append("Thông kê tin bài");
            var lstCategory = BindStaticDataCategory();
            if (!string.IsNullOrEmpty(startDate))
            {
                _startDate = startDate.ToDateTime();
                _title.Append(string.Format(" từ ngày {0}", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                _endDate = endDate.ToDateTime();
                _title.Append(string.Format(" đến ngày {0}", endDate));
            }
            if (!string.IsNullOrEmpty(categories))
            {
                var lstRemoveCategory = categories.ToListInt();
                lstCategory = lstCategory.Where(x => lstRemoveCategory.Contains(x.ItemID)).ToList();
            }

            var lstSettingSite = SiteSettings.GetSiteListTemplate();
            if (!string.IsNullOrEmpty(siteid))
            {
                var lstsiteid = siteid.ToListInt();
                lstSettingSite = lstSettingSite.Where(x => lstsiteid.Contains(x.SiteId)).ToList();
            }

            var lstUserAll = new List<SiteUser>();

            if (!string.IsNullOrEmpty(userGuid))
            {
                var lstuserGuid = userGuid.ToListInt();
                foreach (var item in lstuserGuid)
                {
                    var UserAll = SiteUser.GetUserById(item);
                    lstUserAll.Add(UserAll);
                }
            }

            lstArticle = ArticleStatisticBO.StatisticTotalAllSite(siteid, _startDate, _endDate);
            if (!string.IsNullOrEmpty(categories))
            {
                foreach (var item in lstCategory)
                {
                    labels.Add(item.Name);
                    data.Add(lstArticle.Where(x => x.CategoryID == item.ItemID).Count());
                }
            }

            if (!string.IsNullOrEmpty(siteid))
            {
                foreach (var item in lstCategory)
                {
                    labels.Add(item.Name);
                    data.Add(lstArticle.Where(x => x.CategoryID == item.ItemID).Count());
                }
            }

            iData.Add(labels);
            iData.Add(data);
            iData.Add(_title.ToString());
            return iData;
        }


        [WebMethod]
        public static List<object> ArticaleTotalCountForSite(string startDate = "", string endDate = "", string siteid = "")
        {
            List<object> iData = new List<object>();
            List<int> data = new List<int>();
            List<string> labels = new List<string>();
            List<ArticleStatisticBO> lstArticle = new List<ArticleStatisticBO>();
            DateTime? _startDate = null;
            DateTime? _endDate = null;
            System.Text.StringBuilder _title = new System.Text.StringBuilder();

            _title.Append("Thông kê tin bài");
            var lstCategory = BindStaticDataCategory();
            if (!string.IsNullOrEmpty(startDate))
            {
                _startDate = startDate.ToDateTime();
                _title.Append(string.Format(" từ ngày {0}", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                _endDate = endDate.ToDateTime();
                _title.Append(string.Format(" đến ngày {0}", endDate));
            }
            var listData = new List<ListItem>();
            var listSiteID = siteid;
            if (string.IsNullOrEmpty(siteid))
            {
                listSiteID = string.Join(",", SiteSettings.GetAllSiteID().ToArray());
            }
            var staticArticle = SiteSettings.GetStaticSite(listSiteID, startDate.ToDateTime(), endDate.ToDateTime());
            foreach (var item in staticArticle)
            {
                labels.Add(item.SiteName);
                data.Add(item.CountArticleID);
            }
            iData.Add(labels);
            iData.Add(data);
            iData.Add(_title.ToString());
            return iData;
        }

        [WebMethod]
        public static List<object> ArticaleTotalCountForAuthor(string startDate = "", string endDate = "", string userGuid = "", string siteid = "1")
        {
            List<object> iData = new List<object>();
            List<int> data = new List<int>();
            List<string> labels = new List<string>();
            List<ArticleStatisticBO> lstArticle = new List<ArticleStatisticBO>();
            DateTime? _startDate = null;
            DateTime? _endDate = null;
            System.Text.StringBuilder _title = new System.Text.StringBuilder();

            _title.Append("Thông kê tin bài");
            var lstCategory = BindStaticDataCategory();
            if (!string.IsNullOrEmpty(startDate))
            {
                _startDate = startDate.ToDateTime();
                _title.Append(string.Format(" từ ngày {0}", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                _endDate = endDate.ToDateTime();
                _title.Append(string.Format(" đến ngày {0}", endDate));
            }
            var listData = new List<ListItem>();
            var listUserId = userGuid;

            var siteID = Int32.Parse(siteid);
            if (string.IsNullOrEmpty(userGuid))
            {
                listUserId = string.Join(",", SiteUser.GetAllUser(siteID).ToArray());
            }
            var staticArticle = SiteUser.GetStaticUser(listUserId, startDate.ToDateTime(), endDate.ToDateTime());
            foreach (var item in staticArticle)
            {
                labels.Add(item.Name);
                data.Add(item.CountArticleID);
            }
            iData.Add(labels);
            iData.Add(data);
            iData.Add(_title.ToString());
            return iData;
        }

        [WebMethod]
        public static List<object> ArticaleTotalCountForCatalogs(string startDate = "", string endDate = "", string categories = "", string siteid = "1")
        {
            List<object> iData = new List<object>();
            List<int> data = new List<int>();
            List<string> labels = new List<string>();
            List<ArticleStatisticBO> lstArticle = new List<ArticleStatisticBO>();
            DateTime? _startDate = null;
            DateTime? _endDate = null;
            System.Text.StringBuilder _title = new System.Text.StringBuilder();

            _title.Append("Thống kê tin bài");
            var lstCategory = BindStaticDataCategory();
            if (!string.IsNullOrEmpty(startDate))
            {
                _startDate = startDate.ToDateTime();
                _title.Append(string.Format(" từ ngày {0}", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                _endDate = endDate.ToDateTime();
                _title.Append(string.Format(" đến ngày {0}", endDate));
            }
            var listData = new List<ListItem>();
            var listcatagories = categories;
            if (siteid == "")
            {
                siteid = "1";
            }
            var siteID = Int32.Parse(siteid);
            if (string.IsNullOrEmpty(categories))
            {
                listcatagories = string.Join(",", Category.GetAllCatagory(siteID));
            }
            var staticArticle = Category.GetStaticCatagory(listcatagories, startDate.ToDateTime(), endDate.ToDateTime(), siteID);
            foreach (var item in staticArticle)
            {
                labels.Add(item.Name);
                data.Add(item.CountArticleID);
            }
            iData.Add(labels);
            iData.Add(data);
            iData.Add(_title.ToString());
            return iData;
        }

        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Thống kê tin bài");
            
            //TitleControl.Visible = false;
            //TitleControl.ModuleInstance = GetModule(moduleId);
            //TitleControl.EditText = "Thống kê tin bài";
            //if (siteUser.IsInRoles("Admins"))
            //{
            //    TitleControl.Visible = true;
            //}

        }
        private static void PopulateStaticChildItem(List<CoreCategory> root, int itemId)
        {
            for (int i = 0; i < root.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(root[i].ItemID);
                if (children.Count <= 0) continue;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    if (child.ItemID.Equals(itemId)) continue;

                    child.Name = child.Name;
                    root.Insert(root.IndexOf(root[i]) + index, child);
                    index++;
                }
            }
        }

        private void PopulateChildItem(List<CoreCategory> root, int itemId)
        {
            for (int i = 0; i < root.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(root[i].ItemID);
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root[i].Name.StartsWith("|"))
                {
                    prefix += root[i].Name.Substring(0, 3);
                    root[i].Name = root[i].Name.Remove(0, 3);
                }
                root[i].Name = prefix + root[i].Name;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    if (child.ItemID.Equals(itemId)) continue;

                    child.Name = prefix + @"|--" + child.Name;
                    root.Insert(root.IndexOf(root[i]) + index, child);
                    index++;
                }
            }
        }
        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            pnlContainer.ModuleId = moduleId;
            siteCategory = siteSettings.ArticleCategory;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(moduleSettings);
            _timeZone = SiteUtils.GetUserTimeZone();
            _timeOffset = SiteUtils.GetUserTimeOffset();

        }

    }

}
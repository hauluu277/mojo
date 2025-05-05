using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LichCongTacFeature.UI;

namespace LichCongTacFeature.UI
{
    public partial class RecentList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected LichCongTacConfiguration config = new LichCongTacConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int week = -1;
        private int year = -1;
        private int lnk = -1;

        protected int currentWeek = -1;
        //protected int dayId = -1;
        protected int currentYear = DateTime.Now.Year;
        private string keyword = string.Empty;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private int i = 0;
        private SiteSettings siteSettings;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        protected int langId = 1;
        protected int langRefer = 1;
        public Dictionary<int, string> DayWeek = new Dictionary<int, string>();

        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public LichCongTacConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }

        public bool IsEditable { get; set; }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            //EnableViewState = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadParams();
            LoadSettings();
            PopulateDay();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
            var next = currentWeek;
            var prev = currentWeek;
            var week_ = 0;
            List<WeekMapDate> lstweek = Ultilities.GetWeeksByYear(currentYear);
            try
            {
                week_ = lstweek.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).FirstOrDefault().Week;

            }
            catch (Exception)
            {
                week_ = Ultilities.WeekOfYearISO8601(DateTime.Now); ;
            }


            lnkPre.NavigateUrl = siteSetting.SiteRoot + "/lichcongtac/ViewList.aspx?pageid=" + pageId + "&mid=" + moduleId + "&week=" + (prev - 1) + "&year=" + currentYear + "&lnk=" + 0;
            lnkNext.NavigateUrl = siteSetting.SiteRoot + "/lichcongtac/ViewList.aspx?pageid=" + pageId + "&mid=" + moduleId + "&week=" + (next + 1) + "&year=" + currentYear + "&lnk=" + 1;
            if (week_ == currentWeek)
            {
                hplSlide.NavigateUrl = siteSetting.SiteRoot + "/lichcongtac/SlideList.aspx";
            }
            else
            {
                hplSlide.NavigateUrl = siteSetting.SiteRoot + "/lichcongtac/SlideList.aspx?pageid=" + pageId + "&mid=" + moduleId + "&week=" + currentWeek + "&year=" + currentYear + "&lnk=" + 0;
            }
        }

        public string NgayCongTac(int itemID)
        {
            string date = string.Empty;
            LichCongTac lct = new LichCongTac(itemID);
            if (lct != null)
            {
                if (!string.IsNullOrEmpty(lct.StartDate.ToString()))
                {
                    date = lct.StartDate.ToString("dd/MM/yyyy");
                }
                if (!string.IsNullOrEmpty(lct.EndDate.ToString()))
                {
                    if (lct.EndDate.Value.ToString("dd/MM/yyyy") != lct.StartDate.ToString("dd/MM/yyyy"))
                    {
                        date += " - " + lct.EndDate.Value.ToString("dd/MM/yyyy");
                    }
                }
            }
            return date;
        }
        private void PopulateDay()
        {
            DayWeek.Add(LichCongTacConstant.ThuHaiID, LichCongTacConstant.ThuHai);
            DayWeek.Add(LichCongTacConstant.ThuBaID, LichCongTacConstant.ThuBa);
            DayWeek.Add(LichCongTacConstant.ThuTuID, LichCongTacConstant.ThuTu);
            DayWeek.Add(LichCongTacConstant.ThuNamID, LichCongTacConstant.ThuNam);
            DayWeek.Add(LichCongTacConstant.ThuSauID, LichCongTacConstant.ThuSau);
            DayWeek.Add(LichCongTacConstant.ThuBayID, LichCongTacConstant.ThuBay);
            DayWeek.Add(LichCongTacConstant.ChuNhatID, LichCongTacConstant.ChuNhat);

        }
        private void PoupulateBindDay()
        {
            rptDay.DataSource = DayWeek;
            rptDay.DataBind();
            Repeater1.DataSource = DayWeek;
            Repeater1.DataBind();
            Repeater2.DataSource = DayWeek;
            Repeater2.DataBind();
        }
        private void PopulateControls()
        {
            //List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(currentYear);
            //foreach (var item in lstWeek)
            //{
            //    if (item.Week == currentWeek)
            //    {
            //        lblbedate.Text = "Từ ngày " + item.StartDate.ToString("dd/MM/yyyy") + " đến ngày " + item.EndDate.ToString("dd/MM/yyyy");
            //    }
            //}

            List<WeekMapDate> lstweek = Ultilities.GetWeeksByYear(currentYear);
            WeekMapDate weekmapdate = lstweek.Where(x => x.Week == currentWeek).FirstOrDefault();
            if (weekmapdate == null)
            {
                weekmapdate = new WeekMapDate();
            }
            lblbedate.Text = "Tuần thứ " + currentWeek + " - từ ngày " + weekmapdate.StartDate.ToString("dd/MM/yyyy") + " đến ngày " + weekmapdate.EndDate.ToString("dd/MM/yyyy");
            lblweek.Text = "Tuần " + currentWeek + "  " + currentYear;
            PoupulateBindDay();
            //BindDocument();
        }

        protected List<LichCongTac> BindDocument(int dayId)
        {
            List<LichCongTac> listLich = new List<LichCongTac>();
            List<LichCongTac> reader = LichCongTac.GetPage(siteSettings.SiteId, moduleId, currentWeek, currentYear, dayId, "", keyword, pageNumber, config.PageSize, out totalPages);
            LichCongTac lich = new LichCongTac();
            List<WeekMapDate> lstweek = Ultilities.GetWeeksByYear(currentYear);
            WeekMapDate weekmapdate = lstweek.Where(x => x.Week == currentWeek).FirstOrDefault();
            if (weekmapdate == null)
            {
                weekmapdate = new WeekMapDate();
            }
            foreach (var item in reader)
            {
                if (!string.IsNullOrEmpty(item.BuoiChieu))
                {
                    lich.BuoiChieu += "<div>" + item.BuoiChieu + "</div>";
                }
                if (!string.IsNullOrEmpty(item.BuoiSang))
                {
                    lich.BuoiSang += "<div>" + item.BuoiSang + "</div>";
                }
                if (!string.IsNullOrEmpty(item.BuoiToi))
                {
                    lich.BuoiToi += "<div>" + item.BuoiToi + "</div>";
                }
                if (!string.IsNullOrEmpty(item.DiaDiem))
                {
                    lich.DiaDiem += "<div>" + item.DiaDiem + "</div>";
                }

                lich.CreateBy = item.CreateBy;
                lich.DateCreate = item.DateCreate;
                lich.DayID = item.DayID;
                lich.EndDate = item.EndDate;
                lich.EndTime = item.EndTime;
                lich.EndWeek = item.EndWeek;
                lich.FTS += item.FTS;
                lich.ItemID = item.ItemID;
                lich.ModuleID = item.ModuleID;
                lich.Nam = item.Nam;
                lich.PageID = item.PageID;
                lich.SiteID = item.SiteID;
                lich.StartDate = item.StartDate;
                lich.StartTime = item.StartTime;
                lich.StartWeek = item.StartWeek;
                lich.Week = item.Week;
            }
            if (reader.Count == 0)
            {
                lich.StartDate = dayId == 1 ? weekmapdate.StartDate.AddDays(6) : weekmapdate.StartDate.AddDays(dayId - 2);
            }
            lich.Thu = DayWeek.Where(x => x.Key == dayId).FirstOrDefault().Value;
            listLich.Add(lich);
            //rptArticles.DataSource = reader;
            //rptArticles.DataBind();
            string pageUrl = siteSetting.SiteRoot + "/lichcongtac/ViewList.aspx"
               + "?pageid=" + PageId.ToInvariantString()
               + "&mid=" + ModuleId.ToInvariantString()
               + "&week=" + currentWeek.ToInvariantString()
               + "&year=" + currentYear.ToInvariantString()
               + "lnk=" + lnk.ToInvariantString()
               + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
            return listLich;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            currentWeek = WebUtils.ParseInt32FromQueryString("week", -1);
            currentYear = WebUtils.ParseInt32FromQueryString("year", -1);
            lnk = WebUtils.ParseInt32FromQueryString("lnk", -1);
            if (currentYear < 0)
            {
                currentYear = DateTime.Now.Year;
            }
            if (currentWeek < 0)
            {
                List<WeekMapDate> lstweek = Ultilities.GetWeeksByYear(currentYear);
                try
                {
                    currentWeek = lstweek.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).FirstOrDefault().Week;

                }
                catch (Exception)
                {
                    currentWeek = Ultilities.WeekOfYearISO8601(DateTime.Now);
                }

            }

            if (lnk == 0)
            {
                //currentWeek--;
                if (currentWeek < 1)
                {
                    currentYear--;
                    List<WeekMapDate> lstweek = Ultilities.GetWeeksByYear(currentYear);
                    currentWeek = lstweek.Count;
                }
            }
            if (lnk == 1)
            {
                //currentWeek++;
                List<WeekMapDate> lstweek = Ultilities.GetWeeksByYear(currentYear);
                if (currentWeek > lstweek.Count)
                {
                    currentYear++;
                    currentWeek = 1;
                }
            }
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new LichCongTacConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
    }
}
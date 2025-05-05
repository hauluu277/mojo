// Author:					HiNet
// Created:					2015-3-19
// Last Modified:			2015-3-19
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace LichCongTacFeature.UI
{

    public partial class EditPost : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private LichCongTac LichCongTac;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string dateTimeFormat;
        public Dictionary<int, string> DayWeek = new Dictionary<int, string>();

        // replace this with your own feature guid or make a static property on one of your business objects
        // like MyFeature.FeatureGuid, then you can use that instead of this variable
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
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }

            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageLichCongTac))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            //if (!roleAdmin)
            //{
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}
            LoadSettings();
            PopulateDay();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }

        }
        private void BindCategoryAuthor()
        {
            var parent = CoreCategory.GetByCode(1, WebConfigSettings.DM_LANHDAO);
            if (parent != null)
            {
                lboxCategoryAuthor.DataSource = CoreCategory.GetChildren(parent.ItemID);
                lboxCategoryAuthor.DataTextField = "Name";
                lboxCategoryAuthor.DataValueField = "ItemID";
                lboxCategoryAuthor.DataBind();
            }

        }
        private void PopulateControls()
        {
            PopulateYear();
            PopulateWeek();
            BindCategoryAuthor();
            //PoupulateBindDay();
            int currentYear = DateTime.UtcNow.Year;
            ddlYear.SelectedValue = currentYear.ToString();
            if (LichCongTac != null)
            {
                if (!string.IsNullOrEmpty(LichCongTac.CategoryAuthor))
                {
                    var listCategory = LichCongTac.CategoryAuthor.Split(' ');
                    foreach (ListItem item in lboxCategoryAuthor.Items)
                    {
                        if (listCategory.Contains(item.Value))
                        {
                            item.Selected = true;
                        }
                    }
                }
                
                ddlYear.SelectedValue = LichCongTac.Nam.ToString();
                ddlWeek.SelectedValue = LichCongTac.Week.ToString();
                //dsdlDay.SelectedValue = LichCongTac.DayID.ToString();
                txtStartWeek.Text = LichCongTac.StartWeek.ToString("dd/MM/yyyy");
                txtEndWeek.Text = LichCongTac.EndWeek.ToString("dd/MM/yyyy");
                dpDateStart.Text = LichCongTac.StartDate.ToString("dd/MM/yyyy");
                //if (!string.IsNullOrEmpty(LichCongTac.EndDate.ToString()))
                //{
                //    dpEndDate.Text = LichCongTac.EndDate.Value.ToString("dd/MM/yyyy");
                //}
                edBuoiSang.Text = LichCongTac.BuoiSang;
                edBuoiChieu.Text = LichCongTac.BuoiChieu;
                edBuoiToi.Text = LichCongTac.BuoiToi;
                btnDel.Visible = true;
                edAddress.Text = LichCongTac.DiaDiem;
            }
            else
            {
                btnDel.Visible = false;
            }
        }
        private void PopulateDay()
        {
            DayWeek.Add(LichCongTacConstant.DefaultID, LichCongTacConstant.Default);
            DayWeek.Add(LichCongTacConstant.ChuNhatID, LichCongTacConstant.ChuNhat);
            DayWeek.Add(LichCongTacConstant.ThuHaiID, LichCongTacConstant.ThuHai);
            DayWeek.Add(LichCongTacConstant.ThuBaID, LichCongTacConstant.ThuBa);
            DayWeek.Add(LichCongTacConstant.ThuTuID, LichCongTacConstant.ThuTu);
            DayWeek.Add(LichCongTacConstant.ThuNamID, LichCongTacConstant.ThuNam);
            DayWeek.Add(LichCongTacConstant.ThuSauID, LichCongTacConstant.ThuSau);
            DayWeek.Add(LichCongTacConstant.ThuBayID, LichCongTacConstant.ThuBay);

        }
        //private void PoupulateBindDay()
        //{
        //    ddlDay.DataValueField = "Key";
        //    ddlDay.DataTextField = "Value";
        //    ddlDay.DataSource = DayWeek;
        //    ddlDay.DataBind();
        //}
        private void PopulateYear()
        {
            List<YearMapDate> lstYear = new List<YearMapDate>();
            if (LichCongTac != null && LichCongTac.Nam > 0)
            {
                int i = LichCongTac.Nam;
                int b = i + 10;
                for (int a = i; a < b; a++)
                {
                    YearMapDate nam = new YearMapDate();
                    nam.Year = a;
                    nam.NameYear = "Năm " + a;
                    lstYear.Add(nam);
                }
                ddlYear.DataValueField = "Year";
                ddlYear.DataTextField = "NameYear";
                ddlYear.DataSource = lstYear;
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseYearLabel + "--", "0"));
            }
            else
            {
                int i = DateTime.Now.Year;
                int b = i + 10;
                for (int a = i; a < b; a++)
                {
                    YearMapDate nam = new YearMapDate();
                    nam.Year = a;
                    nam.NameYear = "Năm " + a;
                    lstYear.Add(nam);
                }
                ddlYear.DataValueField = "Year";
                ddlYear.DataTextField = "NameYear";
                ddlYear.DataSource = lstYear;
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseYearLabel + "--", "0"));
            }
        }
        private void PopulateWeek()
        {
            if (LichCongTac != null && LichCongTac.Week > 0)
            {
                int year = LichCongTac.Nam;
                List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(year);
                var currentWeek = LichCongTac.Week;

                ddlWeek.DataValueField = "Week";
                ddlWeek.DataTextField = "NameWeek";
                ddlWeek.DataSource = lstWeek;
                ddlWeek.DataBind();
                ddlWeek.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseWeekLabel + "--", "0"));
                ddlWeek.SelectedValue = currentWeek.ToString();
            }
            else
            {
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(year);
                var currentWeek = 0;
                try
                {
                    currentWeek = lstWeek.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).FirstOrDefault().Week;

                }
                catch (Exception)
                {
                    currentWeek = Ultilities.WeekOfYearISO8601(DateTime.Now); ;
                }

                //currentWeek = Ultilities.GetIso8601WeekOfYear(DateTime.Now);
                ddlWeek.DataValueField = "Week";
                ddlWeek.DataTextField = "NameWeek";
                ddlWeek.DataSource = lstWeek;
                ddlWeek.DataBind();
                ddlWeek.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseWeekLabel + "--", "0"));
                ddlWeek.SelectedValue = currentWeek.ToString();
                //WeekMapDate weekmapdate = Ultilities.GetStartAndEndOfWeek(currentWeek, year);
                foreach (var item in lstWeek)
                {
                    if (item.Week == currentWeek)
                    {
                        txtStartWeek.Text = item.StartDate.ToString("dd/MM/yyyy");
                        txtEndWeek.Text = item.EndDate.ToString("dd/MM/yyyy");
                    }
                }
            }

        }
        private bool Save()
        {
            Page.Validate("LichCongTac");
            if (!Page.IsValid) return false;
            if (LichCongTac == null) { LichCongTac = new LichCongTac(itemId); }
            {
                LichCongTac.ModuleID = moduleId;
                LichCongTac.PageID = pageId;
                LichCongTac.SiteID = siteId;
                LichCongTac.BuoiSang = edBuoiSang.Text;
                LichCongTac.BuoiChieu = edBuoiChieu.Text;
                LichCongTac.BuoiToi = edBuoiToi.Text;
                if (lboxCategoryAuthor.SelectedItem != null && lboxCategoryAuthor.SelectedValue != "")
                {
                    var selected = new List<string>();
                    foreach (ListItem item in lboxCategoryAuthor.Items)
                    {
                        if (item.Selected)
                        {
                            selected.Add(item.Value);
                        }
                    }

                    LichCongTac.CategoryAuthor = string.Join(" ",selected.ToArray());
                }
                int week = Convert.ToInt32(ddlWeek.SelectedValue);
                int year = Convert.ToInt32(ddlYear.SelectedValue);
                LichCongTac.Week = Convert.ToInt32(ddlWeek.SelectedValue);
                LichCongTac.Nam = Convert.ToInt32(ddlYear.SelectedValue);
                DateTime localTime = DateTime.Parse(dpDateStart.Text, CultureInfo.CurrentCulture);
                DateTime date = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                LichCongTac.StartDate = date;
                int dayId = 0;
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    dayId = LichCongTacConstant.ChuNhatID;
                }
                else if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    dayId = LichCongTacConstant.ThuHaiID;
                }
                else if (date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    dayId = LichCongTacConstant.ThuBaID;
                }
                else if (date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    dayId = LichCongTacConstant.ThuTuID;
                }
                else if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    dayId = LichCongTacConstant.ThuNamID;
                }
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    dayId = LichCongTacConstant.ThuSauID;
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    dayId = LichCongTacConstant.ThuBayID;
                }
                LichCongTac.DayID = dayId;
                //LichCongTac.DayID = int.Parse(ddlDay.SelectedValue);    
                //if (!string.IsNullOrEmpty(dpEndDate.Text))
                //{
                //    DateTime localEndTime = DateTime.Parse(dpEndDate.Text, CultureInfo.CurrentCulture);
                //    LichCongTac.EndDate = timeZone != null ? localEndTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                //}
                List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(year);
                foreach (var item in lstWeek)
                {
                    if (item.Week == week)
                    {
                        LichCongTac.StartWeek = item.StartDate;
                        LichCongTac.EndWeek = item.EndDate;
                    }
                }
                //WeekMapDate weekmapdate = Ultilities.GetStartAndEndOfWeek(week, year);
                //LichCongTac.StartWeek = weekmapdate.StartDate;
                //LichCongTac.EndWeek = weekmapdate.EndDate;
                //DateTime localStartTimeWeek = DateTime.Parse(txtStartWeek.Text, CultureInfo.CurrentCulture);
                //LichCongTac.StartWeek = timeZone != null ? localStartTimeWeek.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                //DateTime localEndTimeWeek = DateTime.Parse(txtEndWeek.Text, CultureInfo.CurrentCulture);
                //LichCongTac.EndWeek = timeZone != null ? localEndTimeWeek.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                LichCongTac.DateCreate = DateTime.UtcNow;
                LichCongTac.CreateBy = siteUser.SiteId;
                LichCongTac.Nam = Convert.ToInt32(ddlYear.SelectedValue);
                LichCongTac.FTS = edBuoiSang.Text.ConvertToFTS() + edBuoiChieu.Text.ConvertToFTS() + edBuoiToi.Text.ConvertToFTS();
                LichCongTac.DiaDiem = edAddress.Text;
                LichCongTac.Save();
            }
            return true;
        }

        private void PopulateLabels()
        {
            //edBuoiSang.WebEditor.ToolBar = ToolBar.Newsletter;
            edBuoiSang.WebEditor.Height = 200;

            //edBuoiChieu.WebEditor.ToolBar = ToolBar.Newsletter;
            edBuoiChieu.WebEditor.Height = 200;

            //edBuoiToi.WebEditor.ToolBar = ToolBar.Newsletter;
            edBuoiToi.WebEditor.Height = 200;

            edAddress.WebEditor.Height = 200;

            btnSubmit.Text = SoLuuTruHoSoResource.btnSubmit;
            btnCancel.Text = SoLuuTruHoSoResource.btnCancel;
            btnDel.Text = SoLuuTruHoSoResource.btnDel;
            rfvNam.ErrorMessage = LichCongTacResources.YearRequiredLabel;
            rfvWeek.ErrorMessage = LichCongTacResources.WeekRequiredLabel;
            //rfvDay.ErrorMessage = LichCongTacResources.DayRequiredLabel;
            rfvStartDate.ErrorMessage = LichCongTacResources.StartDateRequiredLabel;
            if (itemId > 0)
            {
                legendProperty.InnerText = "Cập nhập lịch làm việc";
            }
            else
            {
                legendProperty.InnerText = "Thêm mới lịch làm việc";
            }
            btnCancel.PostBackUrl = "/LichCongTac/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (itemId > -1)
            {
                LichCongTac = new LichCongTac(itemId);
                //if (LichCongTac.ModuleID != moduleId) { LichCongTac = null; }
            }
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            siteId = siteUser.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
        }
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            WebUtils.SetupRedirect(this, "/LichCongTac/ManagePost.aspx");
        }
        private void btnDel_Click(object sender, System.EventArgs e)
        {
            if (LichCongTac != null)
            {
                LichCongTac.Delete(itemId);
            }
            WebUtils.SetupRedirect(this, "/LichCongTac/ManagePost.aspx");
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            if (year > 0)
            {
                List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(year);
                ddlWeek.DataValueField = "Week";
                ddlWeek.DataTextField = "NameWeek";
                ddlWeek.DataSource = lstWeek;
                ddlWeek.DataBind();
                ddlWeek.Items.Insert(0, new ListItem("--" + LichCongTacResources.ChooseWeekLabel + "--", "0"));
                txtStartWeek.Text = string.Empty;
                txtEndWeek.Text = string.Empty;
            }
        }
        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int week = Convert.ToInt32(ddlWeek.SelectedValue);
            //WeekMapDate weekmapdate= Ultilities.GetStartAndEndOfWeek(week, year);
            List<WeekMapDate> lstWeek = Ultilities.GetWeeksByYear(year);
            foreach (var item in lstWeek)
            {
                if (item.Week == week)
                {
                    txtStartWeek.Text = item.StartDate.ToString("dd/MM/yyyy");
                    txtEndWeek.Text = item.EndDate.ToString("dd/MM/yyyy");
                }
            }
            //txtStartWeek.Text = weekmapdate.StartDate.ToString("dd/MM/yyyy");
            //txtEndWeek.Text = weekmapdate.EndDate.ToString("dd/MM/yyyy");
        }
        #region OnInit
        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (!Save()) return;
            WebUtils.SetupRedirect(this, "/LichCongTac/ManagePost.aspx");
        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnDel.Click += new EventHandler(btnDel_Click);
            ddlYear.SelectedIndexChanged += new EventHandler(ddlYear_SelectedIndexChanged);
            ddlWeek.SelectedIndexChanged += new EventHandler(ddlWeek_SelectedIndexChanged);
            SiteUtils.SetupEditor(edBuoiSang);
            SiteUtils.SetupEditor(edBuoiChieu);
            SiteUtils.SetupEditor(edBuoiToi);
            SiteUtils.SetupEditor(edAddress);
        }

        #endregion
    }
}
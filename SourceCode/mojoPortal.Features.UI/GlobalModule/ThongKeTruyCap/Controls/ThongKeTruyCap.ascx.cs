using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.UI.GlobalModule.ThongKeTruyCap;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonBusiness;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Newtonsoft.Json;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThongKeTruyCapFeature.UI
{
    public partial class ThongKeTruyCap : UserControl
    {
        #region Properties
        private int moduleId = -1;
        public SiteSettings siteSettings;

        public ThongKeTruyCapConfiguration config { get; set; }
        protected int langId = 1;
        protected int KieuGiaoDien = 1;
        protected int tatCaLuotTruyCap = 0;
        protected int tuanNay = 0;
        protected int thangNay = 0;
        protected int homNay = 0;
        protected int progressTatCa = 0;
        protected int progressHomNay = 0;
        protected int progressTuanNay = 0;
        protected int progressOnline = 0;
        protected int progressThangNay = 0;


        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        #endregion

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadSettings();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateControls()
        {
            lbl_OnlineHienTai.Text = Application["LuotOnline"].ToString();
            ThongKeTruyCapBusiness thongKeTruyCapBusiness = new ThongKeTruyCapBusiness(new mojoportal.Service.UoW.UnitOfWork());
            var tongTatCa = thongKeTruyCapBusiness.GetByType(ThongKeTruyCapConstant.Tong);
            tatCaLuotTruyCap = tongTatCa.Total.GetValueOrDefault(0);

            var trongThang= thongKeTruyCapBusiness.GetByType(ThongKeTruyCapConstant.TrongThang);
            thangNay = trongThang.Total.GetValueOrDefault(0);

            var trongNgay = thongKeTruyCapBusiness.GetByType(ThongKeTruyCapConstant.TrongNgay);
            homNay = trongNgay.Total.Value;

            var trongTuan = thongKeTruyCapBusiness.GetByType(ThongKeTruyCapConstant.TrongTuan);
            tuanNay = trongTuan.Total.Value;


            progressTatCa = tatCaLuotTruyCap > 0 ? 100 : 0;
            progressOnline = Application["LuotOnline"].ToIntOrZero() > 0 ? 100 : 0;


            double a = (homNay * 100 / thangNay);
            progressHomNay = (int)Math.Floor(a);
            double b = tuanNay * 100 / thangNay;
            progressTuanNay = (int)Math.Floor(b);


            progressThangNay = 100;



            lbl_ThangNay.Text = thangNay.ToString();
            lbl_ThangNay2.Text = thangNay.ToString();


            lbl_HomNay.Text = homNay.ToString();
            lbl_TuanNay.Text = tuanNay.ToString();
            lbl_TatCa.Text = tatCaLuotTruyCap.ToString();
            lbl_TatCa2.Text = tatCaLuotTruyCap.ToString();
            lbl_HomNay2.Text = homNay.ToString();
            lbl_TuanNay2.Text = tuanNay.ToString();
            lbl_OnlineHienTai2.Text = Application["LuotOnline"].ToString();
        }

        protected virtual void LoadSettings()
        {
            //Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            //config = new ThongKeTruyCapConfiguration(getModuleSettings);
            //siteSettings = CacheHelper.GetCurrentSiteSettings();
            KieuGiaoDien = config.SetGiaoDien;
        }

    }
}
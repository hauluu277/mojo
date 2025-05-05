// Author:					HiNet
// Created:					2015-3-12
// Last Modified:			2015-3-12
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

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
using mojoPortal.Features;
using log4net;
using mojoPortal.Business;
using Resources;
using mojoPortal.Business.WebHelpers;

namespace BannerFeature.UI
{

    public partial class BannerView : UserControl
    {
        // FeatureGuid c4097fe9-bffd-4113-acef-97f2332e1f05
        private int pageId = -1;
        private int moduleId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        protected int width;
        protected bool isheight = false;
        protected bool IsHorizontal = false;
        protected bool isNotHeight = false;
        protected bool IsVertical = false;
        protected bool isSlideTop = false;
        protected bool isSlideRotator = false;
        protected bool isSlideBottom = false;
        protected int slideSetting = 0;
        protected int timeDisplayRotator = 1600;
        protected int timeChangeRotator = 400;
        protected int Number;
        protected int b = 0;
        private int a = 0;
        protected int moduleSetting = -1;
        protected int pageSetting = -1;
        decimal sumWidth = 0;
        ArrayList lstWidth = new ArrayList();
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string EditLinkText = ConsultResources.EditLinkText;
        protected string EditLinkTooltip = ConsultResources.EditLinkTooltip;
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private string bxScriptFile = "jquery.flexslider.js";
        private BannerConfiguration config = new BannerConfiguration();
        protected int widthSlideTop = 1;
        protected int heightSlideTop = 1;
        protected int numberImage = 1;
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }
        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }
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
        public bool IsEditable { get; set; }
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupMainScript();
            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateControls()
        {
            hplTieuDe.Text = config.HeadBanner;

            BindBanner();

        }


        private void PopulateLabels()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            timeChangeIMG.Value = config.TimeChangeImage.ToString();
            timeDisplayIMG.Value = config.SpeedSlide.ToString();
            widthIMG.Value = config.BannerWidthSlideTop.ToString();
            heightIMG.Value = config.BannerHeightSlideTop.ToString();
        }

        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new BannerConfiguration(moduleSettings);
            IsHorizontal = config.Horizontal;
            isheight = config.HeightSetting;
            try
            {
                slideSetting = int.Parse(config.SliderSetting);

            }
            catch
            {

                slideSetting = 0;
            }

            numberImage = config.BannerNumber;
            widthSlideTop = config.BannerWidthSlideTop;
            heightSlideTop = config.BannerHeightSlideTop;
            if (isheight == false) { isNotHeight = true; }
            if (IsHorizontal == false) { IsVertical = true; }
        }
        private void BindBanner()
        {
            b = 0;
            List<Banner> banner = new List<Banner>();
            banner = Banner.GetBannerByConfig(siteSetting.SiteId, moduleId, pageId, config.BannerNumber);
            foreach (var item in banner)
            {
                if (!string.IsNullOrEmpty(item.Width))
                {
                    lstWidth.Add(int.Parse(item.Width));
                }
            }
            if (slideSetting == BannerConstant.NoSlide)
            {
                rptBanner.DataSource = banner;
                rptBanner.DataBind();
            }
            else if (slideSetting == BannerConstant.FullWidth_AnimatedTouch)
            {

                rptSlideFullwidth.DataSource = banner;
                rptSlideFullwidth.DataBind();
            }
            else if (slideSetting == BannerConstant.OWL_KhoaPhong)
            {

                rptOwlKhoaPhong.DataSource = banner;
                rptOwlKhoaPhong.DataBind();
            }
            else if (slideSetting == BannerConstant.OWL_DoiTac)
            {
                lblNameSlider.Text = config.NameSetting;
                rptOwlDoiTac.DataSource = banner;
                rptOwlDoiTac.DataBind();
            }
            else
            {
                rptJssor.DataSource = banner;
                rptJssor.DataBind();
            }
        }
        /// <summary>
        /// type==false is flash
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public string BuildFlashObject(bool type, string filepath, decimal width, bool visible1, bool visible2)
        {

            if (type == false)
            {
                if (config.HeightSetting == true)
                {
                    //string obj_format = "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0' width='200' height='200'><param name='movie' value='~/Data/Images/Banner/{0}' /><embed width='200' height='200' src='~/Data/Images/Banner/{0}' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer'></embed></object>";
                    string obj_format = "<embed width='" + Width(visible1, visible2, a++, width) + "' height='" + Height() + "'  align='top' quality='high' wmode='opaque' allowscriptaccess='always' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' src='{0}/Data/Images/Banner/{1}'></embed>";
                    return string.Format(obj_format, SiteRoot, filepath);
                }
                else
                {
                    string obj_format = "<embed width='" + Width(visible1, visible2, a++, width) + "' align='top' quality='high' wmode='opaque' allowscriptaccess='always' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' src='{0}/Data/Images/Banner/{1}'></embed>";
                    return string.Format(obj_format, SiteRoot, filepath);
                }
                //<object data='<%#"~/Data/Images/Banner/"+Eval("FilePath") %>'></object>height='"+Height()+"'

            }
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visible1">dat banner theo hang ngang?</param>
        /// <param name="visible2">dat chieu cao cho banner?</param>
        /// <param name="i">i la so thu tu banner</param>
        /// <param name="width">do rong cua banner i</param>
        /// <returns></returns>
        public decimal Width(bool visible1, bool visible2, int i, object width)
        {
            decimal widthConvert = 0;
            if (width != null)
            {
                widthConvert = Convert.ToDecimal(width);
            }
            if (visible1 == true)
            {
                decimal bannerWidth;
                int numberBanner;
                decimal tyle = 1;
                if (config.Horizontal == true)
                {
                    decimal sumWidthBanner = Convert.ToDecimal((config.BannerWidth) - (config.BannerNumberRow - 1) * 2);
                    numberBanner = config.BannerNumberRow;
                    if (lstWidth.Count >= numberBanner)
                    {
                        if (i % numberBanner == 0)
                        {
                            sumWidth = 0;
                            int k = (i / numberBanner) + 1;
                            int sizeNumber = k * numberBanner;
                            if (lstWidth.Count >= sizeNumber)
                            {
                                for (int j = i; j < sizeNumber; j++)
                                {
                                    if (!string.IsNullOrEmpty(lstWidth[j].ToString()))
                                    {
                                        sumWidth += Convert.ToDecimal(lstWidth[j]);
                                    }
                                }

                            }
                            else
                            {
                                for (int j = i; j < lstWidth.Count; j++)
                                {
                                    if (!string.IsNullOrEmpty(lstWidth[j].ToString()))
                                    {
                                        sumWidth += Convert.ToDecimal(lstWidth[j]);
                                    }
                                }
                                if (sumWidth < sumWidthBanner)
                                {
                                    sumWidth = sumWidthBanner;
                                }
                            }
                        }
                        tyle = sumWidthBanner / sumWidth;
                    }
                    else
                    {
                        for (int j = i; j < lstWidth.Count; j++)
                        {
                            if (!string.IsNullOrEmpty(lstWidth[j].ToString()))
                            {
                                sumWidth += Convert.ToDecimal(lstWidth[j]);
                            }
                        }
                        if (sumWidth < sumWidthBanner)
                        {
                            tyle = 1;
                        }
                        else
                        {
                            tyle = sumWidth / sumWidthBanner;
                        }
                    }
    
                    bannerWidth = widthConvert * tyle;
                    return bannerWidth;
                }
                else
                {
                    bannerWidth = Convert.ToDecimal(config.BannerWidth);
                    return bannerWidth;
                }
            }
            else
            {
                b = b - 1;
                return widthConvert;
            }
            //decimal bannerWidth;
            //if (config.Horizontal == true)
            //{
            //    bannerWidth = Convert.ToDecimal((config.BannerWidth) - ((config.BannerNumber) * 5)) / Convert.ToDecimal(config.BannerNumber);
            //    return bannerWidth;
            //}
            //else
            //{
            //    bannerWidth = Convert.ToDecimal(config.BannerWidth);
            //    return bannerWidth;
            //}
        }
        public int Height()
        {
            return config.BannerHeight;
        }
        private void SetupMainScript()
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
                    "flexslider", "\n<script  src=\""
                    + Page.ResolveUrl("~/ClientScript/jqmojo/" + bxScriptFile) + "\" type=\"text/javascript\" ></script>");
        }
        public bool VisbleBannerImage(bool IsImage, string StartDate, string EndDate)
        {
            if (IsImage == true)
            {
                DateTime? _StartDate = null;
                DateTime? _EndDate = null;
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    _StartDate = Convert.ToDateTime(StartDate);
                    _EndDate = Convert.ToDateTime(EndDate);
                    if (_StartDate <= DateTime.Now && _EndDate >= DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public string Target(bool target)
        {
            string blank = string.Empty;
            if (target == true)
            {
                return blank = "_blank";
            }
            return string.Empty;
        }
        //public bool Display(bool type)
        //{
        //    if (type == true)
        //        return true;
        //    else if (type == false)
        //        return false;
        //    else
        //        return true;
        //}
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features.UI.Article.Components
{
    public class ArticleNewConfiguration
    {
        public ArticleNewConfiguration()
        { }

        public ArticleNewConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("ArticleCategoryConfigSetting"))
                articleCategoryConfig = settings["ArticleCategoryConfigSetting"].ToString();

            if (settings.Contains("SetKieuHieuUngSetting") && settings["SetKieuHieuUngSetting"].ToString() != "")
            {
                setKieuHieuUng = settings["SetKieuHieuUngSetting"].ToString();
            }

            if (settings.Contains("ThoiGianChuyenDongSetting") && settings["ThoiGianChuyenDongSetting"].ToString() != "")
            {
                setThoiGianChuyenDong = Convert.ToInt32(settings["ThoiGianChuyenDongSetting"]);
            }


            if (settings.Contains("LayTinTuCongThanhVienSetting") && settings["LayTinTuCongThanhVienSetting"].ToString() != "")
            {
                isLayTinTuCongThanhVien = Convert.ToBoolean(settings["LayTinTuCongThanhVienSetting"]);
            }


            if (settings.Contains("ShowSapoSetting") && settings["ShowSapoSetting"].ToString() != "")
            {
                isShowSapo = Convert.ToBoolean(settings["ShowSapoSetting"]);
            }

            if (settings.Contains("NumberArticleLimitSetting") && settings["NumberArticleLimitSetting"].ToString() != "")
            {
                numberArticleLimit = Convert.ToInt32(settings["NumberArticleLimitSetting"]);
            }
        }

        public string setKieuHieuUng = string.Empty;
        public string SetKieuHieuUng
        {
            get { return setKieuHieuUng; }
        }

        public int setThoiGianChuyenDong = 0;
        public int SetThoiGianChuyenDong
        {
            get { return setThoiGianChuyenDong; }

        }

        private bool isLayTinTuCongThanhVien = false;
        public bool IsLayTinTuCongThanhVien
        {
            get { return isLayTinTuCongThanhVien; }
        }

        private bool isShowSapo = false;
        public bool IsShowSapo
        {
            get { return isShowSapo; }
        }

        private string articleCategoryConfig;
        public string ArticleCategoryConfig
        {
            get { return articleCategoryConfig; }
        }


        private int numberArticleLimit = 5;
        public int NumberArticleLimit { get { return numberArticleLimit; } }
    }
}
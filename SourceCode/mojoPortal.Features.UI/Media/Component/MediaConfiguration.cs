using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public class MediaConfiguration
    {
        public MediaConfiguration() { }
        public MediaConfiguration(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }

            pageSize = WebUtils.ParseInt32FromHashtable(settings, "MediaSettingPageSize", pageSize);
            pageSizeModule = WebUtils.ParseInt32FromHashtable(settings, "MediaSettingPageSizeModule", pageSizeModule);
            numberVideoShow = WebUtils.ParseInt32FromHashtable(settings, "NumberVideoShowSettings", numberVideoShow);

            showPager = WebUtils.ParseBoolFromHashtable(settings, "MediaShowPagerInListSetting", showPager);
            if (settings.Contains("RolePublishSetting"))
            {
                roles = settings["RolePublishSetting"].ToString();
                rolePublish = roles.Split(';');
            }
            if (settings.Contains("MediaCategoryConfigSetting"))
                mediaCategoryConfig = settings["MediaCategoryConfigSetting"].ToString();
            if (settings.Contains("RoleApproveSetting"))
            {
                roles = settings["RoleApproveSetting"].ToString();
                roleApprove = roles.Split(';');
            }
            if (settings.Contains("MediaCategorySetting"))
            {
                categoryGallerySetting = settings["MediaCategorySetting"].ToString();
            }
            showLink = WebUtils.ParseBoolFromHashtable(settings, "MediaSettingShowLink", showLink);
            topVideoSetting = WebUtils.ParseInt32FromHashtable(settings, "TopVideoSetting", topVideoSetting);
            if (settings.Contains("ModuleCssCustomeSetting"))
                moduleCssCustome = settings["ModuleCssCustomeSetting"].ToString();

            if (settings.Contains("NameTitleSetting"))
                nameTitle = settings["NameTitleSetting"].ToString();

            if (settings.Contains("TextLinkVideoSetting"))
                textLinkVideo = settings["TextLinkVideoSetting"].ToString();

            if (settings.Contains("SloganSchoolSetting"))
                sloganSchool = settings["SloganSchoolSetting"].ToString();


            numberGallery = WebUtils.ParseInt32FromHashtable(settings, "NumberGallerySetting", NumberGallery);
            useTabSetting = WebUtils.ParseInt32FromHashtable(settings, "UseTabSetting", useTabSetting);

            if (settings.Contains("GallerySelectSetting"))
                gallerySelectSetting = settings["GallerySelectSetting"].ToString();


            galleryHienThiSetting = WebUtils.ParseInt32FromHashtable(settings, "GalleryHienThiSetting", galleryHienThiSetting);

            kieuHienThiSetting = WebUtils.ParseInt32FromHashtable(settings, "KieuHienThiSetting", kieuHienThiSetting);

            numberShowSetting = WebUtils.ParseInt32FromHashtable(settings, "NumberShowSetting", numberShowSetting);

            thoiGianChaySetting = WebUtils.ParseInt32FromHashtable(settings, "ThoiGianChaySetting", thoiGianChaySetting);
            chonChuyenMucSetting = WebUtils.ParseInt32FromHashtable(settings, "ChonChuyenMucSetting", chonChuyenMucSetting);
        }

        private int chonChuyenMucSetting = 0;
        public int ChonChuyenMucSetting
        {
            get { return chonChuyenMucSetting; }
        }


        private int thoiGianChaySetting = 1000;
        public int ThoiGianChaySetting
        {
            get { return thoiGianChaySetting; }
        }


        private int numberShowSetting = 5;
        public int NumberShowSetting
        {
            get { return numberShowSetting; }
        }

        private int kieuHienThiSetting = 0;
        public int KieuHienThiSetting
        {
            get { return kieuHienThiSetting; }
        }

        private int galleryHienThiSetting = 0;
        public int GalleryHienThiSetting
        {
            get { return galleryHienThiSetting; }
        }

        private string galleryCategorySetting = string.Empty;
        public string GalleryCategorySetting
        {
            get { return galleryCategorySetting; }
        }

        private int useTabSetting = 1;
        public int UseTabSetting
        {
            get { return useTabSetting; }
        }
        private string categoryGallerySetting = string.Empty;
        public string CategoryGallerySetting
        {
            get { return categoryGallerySetting; }
        }
        private string gallerySelectSetting = string.Empty;
        public string GallerySelectSetting
        {
            get { return gallerySelectSetting; }
        }
        private int numberGallery = 4;
        public int NumberGallery
        {
            get { return numberGallery; }
        }
        private int topVideoSetting = 8;
        public int TopVideoSetting
        {
            get { return topVideoSetting; }
            set { topVideoSetting = value; }
        }


        private string roles = string.Empty;
        private IList rolePublish;
        public IList RolePublish { get { return rolePublish; } }

        private IList roleApprove;
        public IList RoleApprove { get { return roleApprove; } }
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
        }

        private int pageSizeModule = 12;
        public int PageSizeModule
        {
            get { return pageSizeModule; }
            set { pageSizeModule = value; }
        }
        private int numberVideoShow = 5;
        public int NumberVideoShow
        {
            get { return numberVideoShow; }
        }
        private string mediaCategoryConfig;
        public string MediaCategoryConfig
        {
            get { return mediaCategoryConfig; }
        }
        private bool showPager = true;

        public bool ShowPager
        {
            get { return showPager; }
        }
        private bool showLink = true;
        public bool ShowLink
        {
            get { return showLink; }
        }
        string moduleCssCustome = "";
        public string ModuleCssCustome
        {
            get { return moduleCssCustome; }
        }
        private string textLinkVideo;
        public string TextLinkVideo
        {
            get { return textLinkVideo; }
        }
        private string sloganSchool;
        public string SloganSchool
        {
            get { return sloganSchool; }
            set { sloganSchool = value; }
        }
        private string nameTitle = "Thư viện ảnh";   /*mặc định*/
        public string NameTitle
        {
            get { return nameTitle; }
            set { nameTitle = value; }
        }
    }
}
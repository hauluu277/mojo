// Author:                  
// Created:                 2006-02-03
// Last Modified:           2017-05-30
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.
// 2011-03-03 implemented better support for page hierarchy in initial content
// 2013-06-24 / Thomas Nicolaïdès : moduleSettings / moduleGuidxxxx handling (tni-20130624)

using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Linq;
using System.Collections;
using mojoPortal.Web;
using System.Drawing;
using ArticleFeature.Business;
using AutoMapper;
using VideoIntroduceFeatures.Business;
using mojoportal.CoreHelpers;

namespace mojoPortal.Web
{
    public sealed class mojoSetup
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(mojoSetup));

        public const string DefaultPageEncoding = "<meta http-equiv=\"Content-Type\" content=\"application/xhtml+xml; charset=utf-8\" />";


        public static void CreateNewSiteDataAndSkinPage(SiteSettings siteSettings)
        {
            //CreateRequiredRolesAndAdminUser(siteSettings);
            CreateDefaultSiteFolders(siteSettings.SiteId, siteSettings.TemplateSite);
            CreateOrRestoreSiteSkins(siteSettings.SiteId, siteSettings.TemplateSite);
            SetupSkinPageContent(siteSettings);
            CacheHelper.ResetSiteMapCache();
        }
        /// <summary>
        /// Tạo danh sách pages của skin template đã chọn và thiết lập các tính năng đã chọn lên trang
        /// </summary>
        public static void SetupSkinPageContent(SiteSettings siteSettings)
        {
            //var abc = Mapper.Map<coreMenu, coreMenuConvert>(menu);
            //var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
            //var mapper = config.CreateMapper();

            int siteID = siteSettings.TemplateSite;
            int categoryArticle = -1;
            var siteSettingSkin = new SiteSettings(siteSettings.TemplateSite);
            #region Lưu menu forsite
            var listMenuConvert = new List<coreMenuConvert>();
            var listMenuOld = coreMenu.GetBySite(siteID);
            if (listMenuOld != null && listMenuOld.Count > 0)
            {
                foreach (var menu in listMenuOld)
                {
                    coreMenuConvert coreMenuConvert = new coreMenuConvert();
                    coreMenuConvert.IdOld = menu.ItemID;
                    coreMenuConvert.IdParentId = menu.ParentID;
                    coreMenuConvert.typeMenu = menu.TypeMenu;

                    menu.ItemID = -1;
                    menu.SiteID = siteSettings.SiteId;
                    menu.Save();


                    coreMenuConvert.IdNew = menu.ItemID;
                    listMenuConvert.Add(coreMenuConvert);
                }

                //Cập nhật menu parent for site mới
                foreach (var item in listMenuOld)
                {
                    if (item.ParentID > 0)
                    {
                        var categoryVersion = listMenuConvert.Where(x => x.IdOld == item.ItemID).FirstOrDefault();
                        if (categoryVersion != null)
                        {
                            coreMenu category = new coreMenu(categoryVersion.IdNew);
                            if (category.ItemID > 0)
                            {
                                var parent = listMenuConvert.Where(x => x.IdOld == item.ParentID).FirstOrDefault();
                                if (parent != null)
                                {
                                    category.ParentID = parent.IdNew;
                                    category.Save();
                                }
                            }
                        }
                    }
                }
            }
            #endregion



            #region Lưu CoreCategory for site 
            var listMenuTypeCategory = listMenuOld.Where(x => x.TypeMenu == MenuTypeLinkConstant.Category).ToList();
            var listCoreCategory = CoreCategory.GetBySite(siteID);
            var listCategoryVersion = new List<CategoryVersion>();
            if (listCoreCategory.Any())
            {
                foreach (var item in listCoreCategory)
                {
                    //Copy category for site mới
                    CoreCategory category = GenericData<CoreCategory>.CloneData(item);
                    category.ItemID = -1;
                    category.SiteID = siteSettings.SiteId;
                    category.Save();
                    //Get Category Article SiteSetting
                    if (siteSettingSkin.ArticleCategory > 0)
                    {
                        if (item.ItemID == siteSettingSkin.ArticleCategory)
                        {
                            categoryArticle = category.ItemID;
                        }
                    }
                    //Lưu category site mới và site cũ
                    listCategoryVersion.Add(new CategoryVersion { CategoryNew = category.ItemID, CategoryOld = item.ItemID, CategoryOldParent = item.ParentID });

                    //cập nhật category for menu
                    var listMenuUpdate = listMenuTypeCategory.Where(x => x.ItemLink == item.ItemID).ToList();
                    if (listMenuUpdate != null && listMenuUpdate.Any())
                    {
                        foreach (var menu in listMenuUpdate)
                        {
                            menu.ItemLink = category.ItemID;
                            menu.Save();
                        }
                    }
                }
                //Cập nhật category parent for site mới
                foreach (var item in listCoreCategory)
                {
                    if (item.ParentID > 0)
                    {
                        var categoryVersion = listCategoryVersion.Where(x => x.CategoryOld == item.ItemID).FirstOrDefault();
                        if (categoryVersion != null)
                        {
                            CoreCategory category = new CoreCategory(categoryVersion.CategoryNew);
                            if (category.ItemID > 0)
                            {
                                var parent = listCategoryVersion.Where(x => x.CategoryOld == item.ParentID).FirstOrDefault();
                                if (parent != null)
                                {
                                    category.ParentID = parent.CategoryNew;
                                    category.Save();
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            //Update category article for new site
            var listMenuTypePage = listMenuOld.Where(x => x.TypeMenu == MenuTypeLinkConstant.Page).ToList();
            if (categoryArticle > 0)
            {
                siteSettings.ArticleCategory = categoryArticle;
                siteSettings.Save();
            }
            #region Lưu page for new site
            List<PageVersion> ListPageVersion = new List<PageVersion>();
            var listPage = PageSettings.GetList(siteID);
            if (listPage.Any())
            {
                foreach (var item in listPage)
                {
                    PageSettings pageSetting = GenericData<PageSettings>.CloneData(item);
                    if (!string.IsNullOrEmpty(item.CategoryConfig))
                    {
                        var updateSetting = string.Empty;
                        var categorySetting = item.CategoryConfig.ToListIntV2();
                        if (categorySetting != null)
                        {
                            foreach (var valueSetting in categorySetting)
                            {
                                var categoryVersion = listCategoryVersion.Where(x => x.CategoryOld == valueSetting).FirstOrDefault();
                                if (categoryVersion != null)
                                {
                                    updateSetting += categoryVersion.CategoryNew + "-";
                                }
                            }
                        }
                        pageSetting.CategoryConfig = updateSetting;
                    }
                    pageSetting.PageId = -1;
                    pageSetting.PageGuid = Guid.NewGuid();
                    pageSetting.SiteGuid = siteSettings.SiteGuid;
                    pageSetting.SiteId = siteSettings.SiteId;
                    pageSetting.Save();

                    //Lưu page version
                    ListPageVersion.Add(new PageVersion { PageNew = pageSetting.PageId, PageNewGuid = pageSetting.PageGuid, PageOld = item.PageId, PageOldParent = pageSetting.ParentId });

                    //Cập nhật page cho menu
                    var listMenuUpdate = listMenuTypePage.Where(x => x.ItemLink == item.PageId).ToList();
                    if (listMenuUpdate != null && listMenuUpdate.Count > 0)
                    {
                        foreach (var menu in listMenuUpdate)
                        {
                            menu.ItemLink = pageSetting.PageId;
                            menu.Save();
                        }
                    }

                    //Lưu url for new page
                    if (!FriendlyUrl.Exists(siteSettings.SiteId, pageSetting.Url))
                    {
                        if (!WebPageInfo.IsPhysicalWebPage(pageSetting.Url))
                        {
                            FriendlyUrl friendlyUrl = new FriendlyUrl();
                            friendlyUrl.SiteId = siteSettings.SiteId;
                            friendlyUrl.SiteGuid = siteSettings.SiteGuid;
                            friendlyUrl.PageGuid = pageSetting.PageGuid;
                            friendlyUrl.Url = pageSetting.Url.Replace("~/", string.Empty);
                            friendlyUrl.RealUrl = "~/Default.aspx?pageid=" + pageSetting.PageId.ToInvariantString();
                            friendlyUrl.Save();
                        }
                    }

                }
                //Update page parentID for new page
                foreach (var item in listPage)
                {
                    if (item.ParentId > 0)
                    {
                        var pageVersion = ListPageVersion.Where(x => x.PageOld == item.PageId).FirstOrDefault();
                        if (pageVersion != null)
                        {
                            PageSettings page = new PageSettings(pageVersion.PageNewGuid);
                            if (page.PageId > 0)
                            {
                                var parent = ListPageVersion.Where(x => x.PageOld == item.ParentId).FirstOrDefault();
                                if (parent != null)
                                {
                                    page.ParentId = parent.PageNew;
                                    page.ParentGuid = parent.PageNewGuid;
                                    page.Save();
                                }
                            }
                        }
                    }

                    #region Lưu Module for new site
                    var listModule = Module.GetByPage(item.PageId);
                    if (listModule.Any())
                    {
                        foreach (var m in listModule)
                        {
                            Module module = GenericData<Module>.CloneData(m);
                            module.ModuleId = -1;
                            module.ModuleGuid = Guid.NewGuid();
                            module.PageId = ListPageVersion.Where(x => x.PageOld == item.PageId).FirstOrDefault().PageNew;
                            module.SiteGuid = siteSettings.SiteGuid;
                            module.SiteId = siteSettings.SiteId;
                            module.SaveCreate();


                            //Lưu module setting đã được thiết lập
                            #region Lưu module setting đã được thiết lập
                            List<CustomModuleSetting> customSettingValues = ModuleSettings.GetCustomSettings(m.ModuleId);
                            foreach (CustomModuleSetting customSetting in customSettingValues)
                            {
                                var updateSetting = customSetting.SettingValue;

                                //update data for ISettingControl
                                if (customSetting.ControlType.Equals("ISettingControl"))
                                {
                                    if (customSetting.ControlSrc.Contains("/Article/Controls/ArticleCategorySetting.ascx"))
                                    {
                                        if (!string.IsNullOrEmpty(customSetting.SettingValue))
                                        {
                                            var categorySetting = customSetting.SettingValue.ToListIntV2();
                                            if (categorySetting != null)
                                            {
                                                //Thay thế category cũ cho category mới for setting value
                                                updateSetting = string.Empty;
                                                foreach (var valueSetting in categorySetting)
                                                {
                                                    var categoryVersion = listCategoryVersion.Where(x => x.CategoryOld == valueSetting).FirstOrDefault();
                                                    if (categoryVersion != null)
                                                    {
                                                        updateSetting += categoryVersion.CategoryNew + "-";
                                                    }

                                                }
                                            }
                                        }
                                    }
                                    if (customSetting.ControlSrc.Contains("/GlobalModule/GlobalControl/CatgorySelectorSetting.ascx"))
                                    {
                                        if (!string.IsNullOrEmpty(customSetting.SettingValue))
                                        {
                                            var categoryOld = customSetting.SettingValue.ToIntOrZero();
                                            var categoryNew = listCategoryVersion.Where(x => x.CategoryOld == categoryOld).Select(x => x.CategoryNew).FirstOrDefault();
                                            updateSetting = categoryNew.ToString();
                                        }
                                    }

                                }
                                ModuleSettings.CreateModuleSetting(
                              module.ModuleGuid,
                              module.ModuleId,
                              customSetting.SettingName,
                              updateSetting,
                              customSetting.SettingControlType,
                              customSetting.SettingValidationRegex,
                              customSetting.ControlSrc,
                              customSetting.HelpKey,
                              customSetting.SortOrder);
                            }
                            #endregion
                            //Lưu dữ liệu mặc định for web builder 
                            #region Lưu dữ liệu mặc định for web builder 

                            //Lưu dữ liệu for module - nội dung HTML
                            # region Lưu dữ liệu for module - nội dung HTML
                            if (m.FeatureGuid.Equals(Guid.Parse("881e4e00-93e4-444c-b7b0-6672fb55de10")))
                            {
                                HtmlRepository repository = new HtmlRepository();
                                HtmlContent html = repository.Fetch(m.ModuleId);
                                if (html != null)
                                {
                                    //html.ItemGuid = Guid.NewGuid();
                                    html.ItemId = -1;
                                    html.SiteId = siteSettings.SiteId;
                                    html.ModuleId = module.ModuleId;
                                    html.ModuleGuid = module.ModuleGuid;
                                    repository.Save(html);
                                }
                            }
                            #endregion
                            //Lưu dữ liệu for module - banner
                            #region Lưu dữ liệu for module - banner
                            if (m.FeatureGuid.Equals(Guid.Parse("b53a0cb2-c795-4d8e-804d-0348679ec816")))
                            {
                                var listBanner = Banner.GetByModule(m.ModuleId);
                                if (listBanner.Any())
                                {
                                    foreach (var bannerOld in listBanner)
                                    {

                                        Banner banner = GenericData<Banner>.CloneData(bannerOld);
                                        banner.ItemID = -1;
                                        banner.CreatedDate = DateTime.Now;
                                        banner.ModuleID = module.ModuleId;
                                        banner.PageID = module.PageId;


                                        //banner.Path = bannerOld.Path;
                                        #region lưu image for new module of web builder
                                        string oldPathImage
                                         = HttpContext.Current.Server.MapPath(
                                         "~/" + ConfigurationManager.AppSettings["BannerImagesFolder"] + bannerOld.Path);

                                        if (File.Exists(oldPathImage))
                                        {

                                            var index = bannerOld.Path.LastIndexOf('.');
                                            //var extend = bannerOld.Path.Substring(0, index);
                                            //var fileExtend = bannerOld.Path.Substring(index + 1);
                                            var extendName = bannerOld.Path.Substring(index).ToLower();
                                            string path = Guid.NewGuid().ToString().Replace("-", string.Empty) + extendName;

                                            string nameIMG = string.Format("~/{0}{1}", ConfigurationManager.AppSettings["BannerImagesFolder"], path);
                                            string newPathImage = HttpContext.Current.Server.MapPath(nameIMG);

                                            Bitmap bm = Image.FromFile(oldPathImage) as Bitmap;
                                            switch (extendName)
                                            {
                                                case ".jpg":
                                                    bm.Save(newPathImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                    break;
                                                case ".png":
                                                    bm.Save(newPathImage, System.Drawing.Imaging.ImageFormat.Png);
                                                    break;
                                                case ".gif":
                                                    bm.Save(newPathImage, System.Drawing.Imaging.ImageFormat.Gif);
                                                    break;
                                                case ".bmp":
                                                    bm.Save(newPathImage, System.Drawing.Imaging.ImageFormat.Bmp);
                                                    break;
                                                default:
                                                    bm.Save(newPathImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                    break;
                                            }

                                            bm.Dispose();
                                            banner.Path = path;
                                        }
                                        #endregion

                                        banner.SiteID = siteSettings.SiteId;
                                        banner.Save();
                                    }
                                }
                            }
                            #endregion


                            //Lưu dữ liệu module Article
                            #region Lưu dữ liệu module Article
                            var listFeatureArticle = new List<Guid>();
                            //Danh sách tin bài giới thiệu
                            listFeatureArticle.Add(Guid.Parse("a1d59d7b-9af6-42b0-bde8-ded75d99d2eb"));
                            //Quản trị bài viết
                            listFeatureArticle.Add(Guid.Parse("8bdb1450-91e5-4cb0-af1a-2427d7e7e611"));
                            //Tin bài giới thiệu
                            listFeatureArticle.Add(Guid.Parse("c1ffced7-9d0d-4fef-be9e-aa81efb08a3d"));
                            //Tin bài nổi bật
                            listFeatureArticle.Add(Guid.Parse("f29a0a9e-93a4-4c83-8b0c-0cacefefb069"));


                            if (listFeatureArticle.Contains(m.FeatureGuid))
                            {
                                var listArticle = Article.GetByModule(siteID, m.ModuleId);
                                if (listArticle.Any())
                                {
                                    foreach (var articleOld in listArticle)
                                    {
                                        Article article = GenericData<Article>.CloneData(articleOld);
                                        article.ItemID = -1;
                                        article.SiteId = siteSettings.SiteId;
                                        article.ModuleID = module.ModuleId;
                                        article.ModuleGuid = module.ModuleGuid;
                                        if (!string.IsNullOrEmpty(article.ImageUrl) && !article.ImageUrl.Contains("http"))
                                        {
                                            #region lưu image for new module of web builder
                                            string oldPathImage
                                             = HttpContext.Current.Server.MapPath(
                                             "~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"] + articleOld.ImageUrl);
                                            if (File.Exists(oldPathImage))
                                            {
                                                string nameIMG = string.Format("~/{0}{1}.jpg", ConfigurationManager.AppSettings["ArticleImagesFolder"], Guid.NewGuid().ToString().Replace("-", string.Empty));
                                                string newPathImage = HttpContext.Current.Server.MapPath(nameIMG);

                                                Bitmap bm = Image.FromFile(oldPathImage) as Bitmap;
                                                bm.Save(newPathImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                bm.Dispose();
                                            }
                                            #endregion
                                        }
                                        var categoryVersion = listCategoryVersion.Where(x => x.CategoryOld == articleOld.CategoryID).FirstOrDefault();
                                        if (categoryVersion != null)
                                        {
                                            article.CategoryID = categoryVersion.CategoryNew;

                                        }
                                        string articleUrl = SuggestUrl(articleOld.Title.Replace("~/", string.Empty), siteSettings);
                                        article.ItemUrl = "~/" + articleUrl;
                                        article.Save();

                                        //lưu article category
                                        var lstCategoryArticle = ArticleCategory.GetList(articleOld.ItemID);
                                        if (lstCategoryArticle != null && lstCategoryArticle.Count > 0)
                                        {
                                            foreach (var category in lstCategoryArticle)
                                            {
                                                ArticleCategory articleCategory = new ArticleCategory();
                                                articleCategory.ItemID = -1;
                                                articleCategory.SiteID = siteSettings.SiteId;
                                                articleCategory.ArticleID = article.ItemID;
                                                var categoryNew = listCategoryVersion.Where(x => x.CategoryOld == (int)category.CategoryID).FirstOrDefault();
                                                if (categoryNew != null)
                                                {
                                                    articleCategory.CategoryID = categoryNew.CategoryNew;
                                                }
                                                articleCategory.Save();
                                            }

                                        }

                                        FriendlyUrl newFriendlyUrl = new FriendlyUrl
                                        {
                                            SiteId = siteSettings.SiteId,
                                            SiteGuid = siteSettings.SiteGuid,
                                            PageGuid = article.ArticleGuid,
                                            Url = articleUrl,
                                            RealUrl = "~/Article/ViewPost.aspx?pageid="
                                        + module.PageId.ToInvariantString()
                                        + "&mid=" + module.ModuleId.ToInvariantString()
                                        + "&ItemID=" + article.ItemID.ToInvariantString()
                                        };

                                        newFriendlyUrl.Save();
                                    }
                                }
                            }
                            #endregion
                            #endregion
                        }
                    }
                    #endregion
                }
            }

            #endregion
            #region Lưu dữ liệu module video introdure
            //Lưu dữ liệu module video introdure
            var lstVideoIntroOld = VideoIntroduce.GetBySite(siteID);
            if (lstVideoIntroOld != null && lstVideoIntroOld.Any())
            {
                foreach (var item in lstVideoIntroOld)
                {
                    VideoIntroduce video = GenericData<VideoIntroduce>.CloneData(item);
                    video.ItemID = -1;
                    video.SiteID = siteSettings.SiteId;
                    video.Save();
                }
            }

            #endregion




            //lưu cập nhật danh sách tin bài không thuộc module nào
            var lstArticleOld = Article.GetByModule(siteID, -1);

            foreach (var articleOld in lstArticleOld)
            {
                Article article = GenericData<Article>.CloneData(articleOld);
                article.ItemID = -1;
                article.SiteId = siteSettings.SiteId;
                article.ModuleID = -1;
                article.ModuleGuid = Guid.NewGuid();
                if (!string.IsNullOrEmpty(article.ImageUrl) && !article.ImageUrl.Contains("http"))
                {
                    #region lưu image for new module of web builder
                    string oldPathImage
                 = HttpContext.Current.Server.MapPath(
                 "~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"] + articleOld.ImageUrl);
                    if (File.Exists(oldPathImage))
                    {
                        string nameIMG = string.Format("~/{0}{1}.jpg", ConfigurationManager.AppSettings["ArticleImagesFolder"], Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string newPathImage = HttpContext.Current.Server.MapPath(nameIMG);

                        Bitmap bm = Image.FromFile(oldPathImage) as Bitmap;
                        bm.Save(newPathImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                        bm.Dispose();
                    }
                    #endregion
                }
                var categoryVersion = listCategoryVersion.Where(x => x.CategoryOld == articleOld.CategoryID).FirstOrDefault();
                if (categoryVersion != null)
                {
                    article.CategoryID = categoryVersion.CategoryNew;
                }
                string articleUrl = SuggestUrl(articleOld.Title.Replace("~/", string.Empty), siteSettings);
                article.ItemUrl = "~/" + articleUrl;
                article.Save();

                //lưu category
                var lstCategoryArticle = ArticleCategory.GetList(articleOld.ItemID);
                if (lstCategoryArticle != null && lstCategoryArticle.Count > 0)
                {
                    foreach (var category in lstCategoryArticle)
                    {
                        ArticleCategory articleCategory = new ArticleCategory();
                        articleCategory.SiteID = siteSettings.SiteId;
                        articleCategory.ArticleID = article.ItemID;
                        var categoryNew = listCategoryVersion.Where(x => x.CategoryOld == (int)category.CategoryID).FirstOrDefault();
                        if (categoryNew != null)
                        {
                            articleCategory.CategoryID = categoryNew.CategoryNew;
                        }
                        articleCategory.Save();
                    }

                }

                FriendlyUrl newFriendlyUrl = new FriendlyUrl
                {
                    SiteId = siteSettings.SiteId,
                    SiteGuid = siteSettings.SiteGuid,
                    PageGuid = article.ArticleGuid,
                    Url = articleUrl,
                    RealUrl = "~/Article/ViewPost.aspx?ItemID=" + article.ItemID.ToInvariantString()
                };

                newFriendlyUrl.Save();
            }

            //Cập nhật lại IsLinhVucDieuTra
            //Cập nhật lại IsTinTuc
            //Dựa vào ArticleCategory và CoreChuDe trong thiết lập websie
            ReloadCategory(siteSettings.SiteId);

        }
        #region File System Tests
        private static void ReloadCategory(int siteId)
        {
            var setting = new SiteSettings(siteId);
            //cập nhật cho tin tức
            if (setting.ArticleCategory > 0)
            {
                var listCategoryId = string.Join(",", CoreCategory.GetListChildrenID(setting.ArticleCategory).ToArray());
                CoreCategory.UpdateMultiple(siteId, listCategoryId, true, false);
            }
            //cập nhật cho lĩnh vực điều tra
            if (setting.CoreChuDe > 0)
            {
                var listCategoryId = string.Join(",", CoreCategory.GetListChildrenID(setting.CoreChuDe).ToArray());
                CoreCategory.UpdateMultiple(siteId, listCategoryId, false, true);
            }
        }
        private static void TestForWritableDataDirectory()
        {
            try
            {
                TouchTestFile();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception("You need to make the Data folder and all its children writable by the ASP.NET worker process. ASPNET on XP, IISWPG on Win2003", ex);
            }
        }

        public static bool DataFolderIsWritable()
        {
            bool result = false;

            try
            {
                TouchTestFile();
                result = true;
            }
            catch (UnauthorizedAccessException) { }
            //catch (ArgumentNullException) { }
            //catch (NotSupportedException) { }
            //catch (ArgumentException) { }
            //catch (FileNotFoundException) { }

            return result;
        }

        public static void TouchTestFile(String pathToFile)
        {
            if (!WebConfigSettings.FileSystemIsWritable)
            {
                return;
            }

            if (pathToFile != null)
            {
                if (File.Exists(pathToFile))
                {
                    File.SetLastWriteTimeUtc(pathToFile, DateTime.UtcNow);
                }
                else
                {
                    StreamWriter streamWriter = File.CreateText(pathToFile);
                    streamWriter.Close();
                }
            }
        }

        public static void TouchTestFile()
        {
            if (!WebConfigSettings.FileSystemIsWritable)
            {
                return;
            }

            if (HttpContext.Current != null)
            {
                String pathToTestFile = HttpContext.Current.Server.MapPath("~/Data/test.config");
                TouchTestFile(pathToTestFile);

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Data/Sites/1/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Data/Sites/1/"));
                }

                pathToTestFile = HttpContext.Current.Server.MapPath("~/Data/Sites/1/test.config");
                TouchTestFile(pathToTestFile);

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Data/systemfiles/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Data/systemfiles/"));
                }

                if (WebConfigSettings.UseCacheDependencyFiles)
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Data/Sites/1/systemfiles/")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Data/Sites/1/systemfiles/"));
                    }

                    pathToTestFile = HttpContext.Current.Server.MapPath("~/Data/Sites/1/systemfiles/test.config");
                    TouchTestFile(pathToTestFile);
                }

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Data/Sites/1/GalleryImages/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Data/Sites/1/GalleryImages/"));
                }

                pathToTestFile = HttpContext.Current.Server.MapPath("~/Data/Sites/1/GalleryImages/test.config");
                TouchTestFile(pathToTestFile);

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Data/Sites/1/FolderGalleries/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Data/Sites/1/FolderGalleries/"));
                }

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Data/Sites/1/SharedFiles/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Data/Sites/1/SharedFiles/"));
                }

                pathToTestFile = HttpContext.Current.Server.MapPath("~/Data/Sites/1/SharedFiles/test.config");
                TouchTestFile(pathToTestFile);

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Data/Sites/1/SharedFiles/History/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Data/Sites/1/SharedFiles/History/"));
                }

                pathToTestFile = HttpContext.Current.Server.MapPath("~/Data/Sites/1/SharedFiles/History/test.config");
                TouchTestFile(pathToTestFile);

                if (!WebConfigSettings.DisableSearchIndex)
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Data/Sites/1/index/")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Data/Sites/1/index/"));
                    }

                    pathToTestFile = HttpContext.Current.Server.MapPath("~/Data/Sites/1/index/test.config");
                    TouchTestFile(pathToTestFile);

                    if (File.Exists(pathToTestFile))
                    {
                        File.Delete(pathToTestFile);
                    }
                }
            }
        }

        public static void EnsureFolderGalleryFolder(SiteSettings siteSettings)
        {
            if (HttpContext.Current == null) return;

            string path = "~/Data/Sites/" + siteSettings.SiteId.ToString(CultureInfo.InvariantCulture) + "/FolderGalleries/";

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            }
        }

        #endregion

        #region System Probing

        public static bool UpgradeIsNeeded()
        {
            bool result = false;

            Version dbCodeVersion = DatabaseHelper.DBCodeVersion();
            Version dbSchemaVersion = DatabaseHelper.DBSchemaVersion();
            if (dbCodeVersion > dbSchemaVersion) result = true;

            return result;
        }

        public static bool RunningInFullTrust()
        {
            bool result = false;

            AspNetHostingPermissionLevel currentTrustLevel = SecurityHelper.GetCurrentTrustLevel();

            if (currentTrustLevel == AspNetHostingPermissionLevel.Unrestricted)
            {
                result = true;
            }

            return result;
        }

        #endregion

        #region Schema Upgrade methods

        public static void DoSchemaUpgrade(String overrideConnectionInfo)
        {
            log.Debug("mojoSetup entered DoSchemaUpgrade");
            bool canAlterSchema = DatabaseHelper.CanAlterSchema(overrideConnectionInfo);

            if ((HttpContext.Current != null) && canAlterSchema)
            {
                Version currentSchemaVersion = DatabaseHelper.DBSchemaVersion();

                Guid appID = DatabaseHelper.GetApplicationId();
                String pathToScriptFolder =
                    HttpContext.Current.Server.MapPath("~/Setup/SchemaUpgradeScripts/" +
                        DatabaseHelper.DBPlatform().ToLowerInvariant() +
                        "/" +
                        DatabaseHelper.GetApplicationName().ToLowerInvariant() +
                        "/"
                    );

                if (Directory.Exists(pathToScriptFolder))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(pathToScriptFolder);
                    FileInfo[] scriptFiles = directoryInfo.GetFiles("*.config");

                    foreach (FileInfo fileInfo in scriptFiles)
                    {
                        Version scriptVersion = DatabaseHelper.ParseVersionFromFileName(fileInfo.Name);

                        if ((scriptVersion != null) && (scriptVersion > currentSchemaVersion) && !DatabaseHelper.SchemaScriptHasBeenRun(appID, fileInfo.Name))
                        {
                            DatabaseHelper.RunScript(appID, fileInfo, overrideConnectionInfo);
                            DoPostScriptTasks(scriptVersion, overrideConnectionInfo);
                        }
                    }
                }
                else
                {
                    log.Error("mojoSetup.DoSchemaUpgrade Error could not find path: " + pathToScriptFolder);
                }
            }
            else
            {
                log.Error("mojoSetup.DoSchemaUpgrade Error: no httpcontext or no permission to alter schema. ");
            }
        }

        public static void DoPostScriptTasks(Version scriptVersion, String overrideConnectionInfo)
        {
            if (scriptVersion == new Version(2, 2, 3, 0))
            {
                DatabaseHelper.DoVersion2230PostUpgradeTasks(overrideConnectionInfo);
            }

            if (scriptVersion == new Version(2, 2, 3, 4))
            {
                DatabaseHelper.DoVersion2234PostUpgradeTasks(overrideConnectionInfo);
            }

            if (scriptVersion == new Version(2, 2, 4, 7))
            {
                DatabaseHelper.DoVersion2247PostUpgradeTasks(overrideConnectionInfo);
            }

            if (scriptVersion == new Version(2, 2, 5, 3))
            {
                DatabaseHelper.DoVersion2253PostUpgradeTasks(overrideConnectionInfo);
            }

            if (scriptVersion == new Version(2, 3, 2, 0))
            {
                DatabaseHelper.DoVersion2320PostUpgradeTasks(overrideConnectionInfo);
            }

            //if (scriptVersion == new Version(2, 3, 9, 5))
            //{
            //    DatabaseHelper.DoVersion2320PostUpgradeTasks(overrideConnectionInfo);
            //}
        }

        #endregion

        #region Create Initial Data

        public static SiteSettings CreateNewSite()
        {
            String templateFolderPath = GetMessageTemplateFolder();
            String templateFolder = templateFolderPath;

            SiteSettings newSite = new SiteSettings();

            newSite.SiteName = GetMessageTemplate(templateFolder, "InitialSiteNameContent.config");
            newSite.Skin = WebConfigSettings.DefaultInitialSkin;

            newSite.Logo = GetMessageTemplate(templateFolder, "InitialSiteLogoContent.config");

            newSite.AllowHideMenuOnPages = false;
            newSite.AllowNewRegistration = true;
            newSite.AllowPageSkins = false;
            newSite.AllowUserFullNameChange = false;
            newSite.AllowUserSkins = false;
            newSite.AutoCreateLdapUserOnFirstLogin = true;
            //newSite.DefaultFriendlyUrlPattern = SiteSettings.FriendlyUrlPattern.PageNameWithDotASPX;
            newSite.EditorSkin = SiteSettings.ContentEditorSkin.normal;
            //newSite.EncryptPasswords = false;
            newSite.Icon = String.Empty;
            newSite.IsServerAdminSite = true;
            newSite.ReallyDeleteUsers = true;
            newSite.SiteLdapSettings.Port = 389;
            newSite.SiteLdapSettings.RootDN = String.Empty;
            newSite.SiteLdapSettings.Server = String.Empty;
            newSite.UseEmailForLogin = true;
            newSite.UseLdapAuth = false;
            newSite.UseSecureRegistration = false;
            newSite.UseSslOnAllPages = WebConfigSettings.SslIsRequiredByWebServer;
            //newSite.CreateInitialDataOnCreate = false;

            newSite.AllowPasswordReset = true;
            newSite.AllowPasswordRetrieval = true;
            //0 = clear, 1= hashed, 2= encrypted
            newSite.PasswordFormat = WebConfigSettings.InitialSitePasswordFormat;

            newSite.RequiresQuestionAndAnswer = true;
            newSite.MaxInvalidPasswordAttempts = 10;
            newSite.PasswordAttemptWindowMinutes = 5;
            newSite.RequiresUniqueEmail = true;
            newSite.MinRequiredNonAlphanumericCharacters = 0;
            newSite.MinRequiredPasswordLength = 7;
            newSite.PasswordStrengthRegularExpression = String.Empty;
            newSite.DefaultEmailFromAddress = GetMessageTemplate(templateFolder, "InitialEmailFromContent.config");
            newSite.EnableMyPageFeature = false;

            newSite.Save();

            return newSite;
        }

        public static void CreateRequiredRolesAndAdminUser(SiteSettings site)
        {
            Role adminRole = new Role();
            adminRole.RoleName = "Admins";
            adminRole.SiteId = site.SiteId;
            adminRole.SiteGuid = site.SiteGuid;
            adminRole.Save();
            adminRole.RoleName = "Administrators";
            adminRole.Save();

            Role roleAdminRole = new Role();
            roleAdminRole.RoleName = "Role Admins";
            roleAdminRole.SiteId = site.SiteId;
            roleAdminRole.SiteGuid = site.SiteGuid;
            roleAdminRole.Save();
            roleAdminRole.RoleName = "Role Administrators";
            roleAdminRole.Save();

            Role contentAdminRole = new Role();
            contentAdminRole.RoleName = "Content Administrators";
            contentAdminRole.SiteId = site.SiteId;
            contentAdminRole.SiteGuid = site.SiteGuid;
            contentAdminRole.Save();

            Role authenticatedUserRole = new Role();
            authenticatedUserRole.RoleName = "Authenticated Users";
            authenticatedUserRole.SiteId = site.SiteId;
            authenticatedUserRole.SiteGuid = site.SiteGuid;
            authenticatedUserRole.Save();

            Role contentPublisherRole = new Role();
            contentPublisherRole.RoleName = "Content Publishers";
            contentPublisherRole.SiteId = site.SiteId;
            contentPublisherRole.SiteGuid = site.SiteGuid;
            contentPublisherRole.Save();

            Role contentAuthorRole = new Role();
            contentAuthorRole.RoleName = "Content Authors";
            contentAuthorRole.SiteId = site.SiteId;
            contentAuthorRole.SiteGuid = site.SiteGuid;
            contentAuthorRole.Save();

            Role newsletterAdminRole = new Role();
            newsletterAdminRole.RoleName = "Newsletter Administrators";
            newsletterAdminRole.SiteId = site.SiteId;
            newsletterAdminRole.SiteGuid = site.SiteGuid;
            newsletterAdminRole.Save();


            Role articleEditor = new Role();
            articleEditor.RoleName = "Biên tập tin bài";
            articleEditor.SiteId = site.SiteId;
            articleEditor.SiteGuid = site.SiteGuid;
            articleEditor.Save();

            Role articleApprove = new Role();
            articleApprove.RoleName = "Kiểm duyệt và xuất bản tin bài";
            articleApprove.SiteId = site.SiteId;
            articleApprove.SiteGuid = site.SiteGuid;
            articleApprove.Save();

            Role articleManager = new Role();
            articleManager.RoleName = "Quản lý tin bài";
            articleManager.SiteId = site.SiteId;
            articleManager.SiteGuid = site.SiteGuid;
            articleManager.Save();


            Role videoManager = new Role();
            videoManager.RoleName = "Quản lý video";
            videoManager.SiteId = site.SiteId;
            videoManager.SiteGuid = site.SiteGuid;
            videoManager.Save();

            // if using related sites mode there is a problem if we already have user admin@admin.com
            // and we create another one in the child site with the same email and login so we need to make it different
            // we could just skip creating this user since in related sites mode all users come from the first site
            // but then if the config were changed to not related sites mode there would be no admin user
            // so in related sites mode we create one only as a backup in case settings are changed later
            int countOfSites = SiteSettings.SiteCount();
            string siteDifferentiator = string.Empty;
            if ((countOfSites >= 1) && WebConfigSettings.UseRelatedSiteMode)
            {
                if (site.SiteId > 1)
                {
                    siteDifferentiator = site.SiteId.ToInvariantString();
                }
            }

            mojoMembershipProvider membership = Membership.Provider as mojoMembershipProvider;
            bool overridRelatedSiteMode = true;
            SiteUser adminUser = new SiteUser(site, overridRelatedSiteMode);
            adminUser.Email = "admin" + siteDifferentiator + "@admin.com";
            adminUser.Name = "Admin";
            adminUser.LoginName = "admin" + siteDifferentiator;
            adminUser.Password = "admin";

            if (membership != null)
            {
                adminUser.Password = membership.EncodePassword(site, adminUser, "admin");
            }

            adminUser.PasswordQuestion = "What is your user name?";
            adminUser.PasswordAnswer = "admin";
            adminUser.Save();

            Role.AddUser(adminRole.RoleId, adminUser.UserId, adminRole.RoleGuid, adminUser.UserGuid);
        }

        private static SiteUser EnsureAdminUser(SiteSettings site)
        {
            // if using related sites mode there is a problem if we already have user admin@admin.com
            // and we create another one in the child site with the same email and login so we need to make it different
            // we could just skip creating this user since in related sites mode all users come from the first site
            // but then if the config were changed to not related sites mode there would be no admin user
            // so in related sites mode we create one only as a backup in case settings are changed later
            int countOfSites = SiteSettings.SiteCount();
            string siteDifferentiator = string.Empty;

            if ((countOfSites >= 1) && WebConfigSettings.UseRelatedSiteMode)
            {
                siteDifferentiator = site.SiteId.ToString(CultureInfo.InvariantCulture);
            }

            mojoMembershipProvider membership = Membership.Provider as mojoMembershipProvider;
            bool overridRelatedSiteMode = true;
            SiteUser adminUser = new SiteUser(site, overridRelatedSiteMode);
            adminUser.Email = "admin" + siteDifferentiator + "@admin.com";
            adminUser.Name = "Admin";
            adminUser.LoginName = "admin" + siteDifferentiator;
            bool userExists = false;

            if (site.UseEmailForLogin)
            {
                userExists = SiteUser.EmailExistsInDB(site.SiteId, adminUser.Email);
            }
            else
            {
                userExists = SiteUser.LoginExistsInDB(site.SiteId, adminUser.LoginName);
            }

            if (!userExists)
            {
                adminUser.Password = "admin";

                if (membership != null)
                {
                    adminUser.Password = membership.EncodePassword(site, adminUser, "admin");
                }

                adminUser.PasswordQuestion = "What is your user name?";
                adminUser.PasswordAnswer = "admin";
                adminUser.Save();

                //Role.AddUser(adminRole.RoleId, adminUser.UserId, adminRole.RoleGuid, adminUser.UserGuid);
            }
            else
            {
                if (site.UseEmailForLogin)
                {
                    adminUser = new SiteUser(site, adminUser.Email);
                }
                else
                {
                    adminUser = new SiteUser(site, adminUser.LoginName);
                }
            }

            return adminUser;
        }

        public static void EnsureRolesAndAdminUser(SiteSettings site)
        {
            SiteUser adminUser = EnsureAdminUser(site);

            if (!Role.Exists(site.SiteId, "Admins"))
            {
                Role adminRole = new Role();
                adminRole.RoleName = "Admins";
                adminRole.SiteId = site.SiteId;
                adminRole.SiteGuid = site.SiteGuid;
                adminRole.Save();
                adminRole.RoleName = "Administrators";
                adminRole.Save();

                Role.AddUser(adminRole.RoleId, adminUser.UserId, adminRole.RoleGuid, adminUser.UserGuid);

            }

            if (!Role.Exists(site.SiteId, "Role Admins"))
            {
                Role roleAdminRole = new Role();
                roleAdminRole.RoleName = "Role Admins";
                roleAdminRole.SiteId = site.SiteId;
                roleAdminRole.SiteGuid = site.SiteGuid;
                roleAdminRole.Save();
                roleAdminRole.RoleName = "Role Administrators";
                roleAdminRole.Save();
            }

            if (!Role.Exists(site.SiteId, "Content Administrators"))
            {
                Role contentAdminRole = new Role();
                contentAdminRole.RoleName = "Content Administrators";
                contentAdminRole.SiteId = site.SiteId;
                contentAdminRole.SiteGuid = site.SiteGuid;
                contentAdminRole.Save();
            }

            if (!Role.Exists(site.SiteId, "Authenticated Users"))
            {
                Role authenticatedUserRole = new Role();
                authenticatedUserRole.RoleName = "Authenticated Users";
                authenticatedUserRole.SiteId = site.SiteId;
                authenticatedUserRole.SiteGuid = site.SiteGuid;
                authenticatedUserRole.Save();
            }

            if (!Role.Exists(site.SiteId, "Content Publishers"))
            {
                Role contentPublisherRole = new Role();
                contentPublisherRole.RoleName = "Content Publishers";
                contentPublisherRole.SiteId = site.SiteId;
                contentPublisherRole.SiteGuid = site.SiteGuid;
                contentPublisherRole.Save();
            }

            if (!Role.Exists(site.SiteId, "Content Authors"))
            {
                Role contentAuthorRole = new Role();
                contentAuthorRole.RoleName = "Content Authors";
                contentAuthorRole.SiteId = site.SiteId;
                contentAuthorRole.SiteGuid = site.SiteGuid;
                contentAuthorRole.Save();
            }

            if (!Role.Exists(site.SiteId, "Newsletter Administrators"))
            {
                Role newsletterAdminRole = new Role();
                newsletterAdminRole.RoleName = "Newsletter Administrators";
                newsletterAdminRole.SiteId = site.SiteId;
                newsletterAdminRole.SiteGuid = site.SiteGuid;
                newsletterAdminRole.Save();
            }
        }

        #endregion

        #region CreateNewSiteData(SiteSettings siteSettings)

        public static void CreateNewSiteData(SiteSettings siteSettings)
        {
            CreateRequiredRolesAndAdminUser(siteSettings);
            CreateDefaultSiteFolders(siteSettings.SiteId);
            CreateOrRestoreSiteSkins(siteSettings.SiteId);

            if (PageSettings.GetCountOfPages(siteSettings.SiteId) == 0)
            {
                SetupDefaultContentPages(siteSettings);
            }
        }

        public static void SetupDefaultContentPages(SiteSettings siteSettings)
        {
            ContentPageConfiguration appPageConfig = ContentPageConfiguration.GetConfig();

            foreach (ContentPage contentPage in appPageConfig.ContentPages)
            {
                CreatePage(siteSettings, contentPage, null);
            }

            CacheHelper.ResetSiteMapCache();
        }

        public static void CreatePage(SiteSettings siteSettings, ContentPage contentPage, PageSettings parentPage)
        {
            PageSettings pageSettings = new PageSettings();
            pageSettings.PageGuid = Guid.NewGuid();

            if (parentPage != null)
            {
                pageSettings.ParentGuid = parentPage.PageGuid;
                pageSettings.ParentId = parentPage.PageId;
            }

            pageSettings.SiteId = siteSettings.SiteId;
            pageSettings.SiteGuid = siteSettings.SiteGuid;
            pageSettings.AuthorizedRoles = contentPage.VisibleToRoles;
            pageSettings.EditRoles = contentPage.EditRoles;
            pageSettings.DraftEditOnlyRoles = contentPage.DraftEditRoles;
            pageSettings.CreateChildPageRoles = contentPage.CreateChildPageRoles;
            pageSettings.MenuImage = contentPage.MenuImage;
            pageSettings.PageMetaKeyWords = contentPage.PageMetaKeyWords;
            pageSettings.PageMetaDescription = contentPage.PageMetaDescription;

            CultureInfo uiCulture = Thread.CurrentThread.CurrentUICulture;

            if (WebConfigSettings.UseCultureOverride)
            {
                uiCulture = SiteUtils.GetDefaultUICulture(siteSettings.SiteId);
            }

            if (contentPage.ResourceFile.Length > 0)
            {
                pageSettings.PageName = ResourceHelper.GetResourceString(contentPage.ResourceFile, contentPage.Name, uiCulture, false);

                if (contentPage.Title.Length > 0)
                {
                    pageSettings.PageTitle = ResourceHelper.GetResourceString(contentPage.ResourceFile, contentPage.Title, uiCulture, false);
                }
            }
            else
            {
                pageSettings.PageName = contentPage.Name;
                pageSettings.PageTitle = contentPage.Title;
            }

            pageSettings.PageOrder = contentPage.PageOrder;
            pageSettings.Url = contentPage.Url;
            pageSettings.RequireSsl = contentPage.RequireSsl;
            pageSettings.ShowBreadcrumbs = contentPage.ShowBreadcrumbs;

            pageSettings.BodyCssClass = contentPage.BodyCssClass;
            pageSettings.MenuCssClass = contentPage.MenuCssClass;
            pageSettings.IncludeInMenu = contentPage.IncludeInMenu;
            pageSettings.IsClickable = contentPage.IsClickable;
            pageSettings.IncludeInSiteMap = contentPage.IncludeInSiteMap;
            pageSettings.IncludeInChildSiteMap = contentPage.IncludeInChildPagesSiteMap;
            pageSettings.AllowBrowserCache = contentPage.AllowBrowserCaching;
            pageSettings.ShowChildPageBreadcrumbs = contentPage.ShowChildPageBreadcrumbs;
            pageSettings.ShowHomeCrumb = contentPage.ShowHomeCrumb;
            pageSettings.ShowChildPageMenu = contentPage.ShowChildPagesSiteMap;
            pageSettings.HideAfterLogin = contentPage.HideFromAuthenticated;
            pageSettings.EnableComments = contentPage.EnableComments;

            pageSettings.Save();

            if (!FriendlyUrl.Exists(siteSettings.SiteId, pageSettings.Url))
            {
                if (!WebPageInfo.IsPhysicalWebPage(pageSettings.Url))
                {
                    FriendlyUrl friendlyUrl = new FriendlyUrl();
                    friendlyUrl.SiteId = siteSettings.SiteId;
                    friendlyUrl.SiteGuid = siteSettings.SiteGuid;
                    friendlyUrl.PageGuid = pageSettings.PageGuid;
                    friendlyUrl.Url = pageSettings.Url.Replace("~/", string.Empty);
                    friendlyUrl.RealUrl = "~/Default.aspx?pageid=" + pageSettings.PageId.ToInvariantString();
                    friendlyUrl.Save();
                }
            }

            foreach (ContentPageItem pageItem in contentPage.PageItems)
            {
                // tni-20130624: moduleGuidxxxx handling
                Guid moduleGuid2Use = Guid.Empty;
                bool updateModule = false;

                Module findModule = null;

                if (pageItem.ModuleGuidToPublish != Guid.Empty)
                {
                    Module existingModule = new Module(pageItem.ModuleGuidToPublish);

                    if (existingModule.ModuleGuid == pageItem.ModuleGuidToPublish && existingModule.SiteId == siteSettings.SiteId)
                    {
                        Module.Publish(
                            pageSettings.PageGuid,
                            existingModule.ModuleGuid,
                            existingModule.ModuleId,
                            pageSettings.PageId,
                            pageItem.Location,
                            pageItem.SortOrder,
                            DateTime.UtcNow,
                            DateTime.MinValue
                        );

                        // tni: I assume there's nothing else to do now so let's go to the next content... 
                        continue;
                    }
                }
                else if (pageItem.ModuleGuid != Guid.Empty)
                {
                    findModule = new Module(pageItem.ModuleGuid);
                    if (findModule.ModuleGuid == Guid.Empty)
                    {
                        // Module does not exist, we can create new one with the specified Guid 
                        moduleGuid2Use = pageItem.ModuleGuid;
                    }

                    if (findModule.ModuleGuid == pageItem.ModuleGuid && findModule.SiteId == siteSettings.SiteId)
                    {
                        // The module already exist, we'll update existing one
                        updateModule = true;
                        moduleGuid2Use = findModule.ModuleGuid;
                    }
                }

                ModuleDefinition moduleDef = new ModuleDefinition(pageItem.FeatureGuid);

                // this only adds if its not already there
                try
                {
                    SiteSettings.AddFeature(siteSettings.SiteGuid, pageItem.FeatureGuid);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }

                if (moduleDef.ModuleDefId > -1)
                {
                    Module module = null;

                    if (updateModule && (findModule != null))
                    {
                        module = findModule;
                    }
                    else
                    {
                        module = new Module();
                        module.ModuleGuid = moduleGuid2Use;
                    }

                    module.SiteId = siteSettings.SiteId;
                    module.SiteGuid = siteSettings.SiteGuid;
                    module.PageId = pageSettings.PageId;
                    module.ModuleDefId = moduleDef.ModuleDefId;
                    module.FeatureGuid = moduleDef.FeatureGuid;
                    module.PaneName = pageItem.Location;

                    if (contentPage.ResourceFile.Length > 0)
                    {
                        module.ModuleTitle = ResourceHelper.GetResourceString(contentPage.ResourceFile, pageItem.ContentTitle, uiCulture, false);
                    }
                    else
                    {
                        module.ModuleTitle = pageItem.ContentTitle;
                    }

                    module.ModuleOrder = pageItem.SortOrder;
                    module.CacheTime = pageItem.CacheTimeInSeconds;
                    module.Icon = moduleDef.Icon;
                    module.ShowTitle = pageItem.ShowTitle;
                    module.AuthorizedEditRoles = pageItem.EditRoles;
                    module.DraftEditRoles = pageItem.DraftEditRoles;
                    module.ViewRoles = pageItem.ViewRoles;
                    module.IsGlobal = pageItem.IsGlobal;
                    module.HeadElement = pageItem.HeadElement;
                    module.HideFromAuthenticated = pageItem.HideFromAuthenticated;
                    module.HideFromUnauthenticated = pageItem.HideFromAnonymous;

                    module.Save();

                    if ((pageItem.Installer != null) && (pageItem.ConfigInfo.Length > 0))
                    {
                        //this is the newer implementation for populating feature content
                        pageItem.Installer.InstallContent(module, pageItem.ConfigInfo);
                    }
                    else
                    {
                        // legacy implementation for backward compatibility
                        if ((pageItem.FeatureGuid == HtmlContent.FeatureGuid) && pageItem.ContentTemplate.EndsWith(".config"))
                        {
                            HtmlContent htmlContent = new HtmlContent();
                            htmlContent.ModuleId = module.ModuleId;
                            htmlContent.Body = ResourceHelper.GetMessageTemplate(uiCulture, pageItem.ContentTemplate);
                            htmlContent.ModuleGuid = module.ModuleGuid;
                            HtmlRepository repository = new HtmlRepository();
                            repository.Save(htmlContent);
                        }
                    }

                    // tni-20130624: handling module settings
                    foreach (KeyValuePair<string, string> item in pageItem.ModuleSettings)
                    {
                        ModuleSettings.UpdateModuleSetting(module.ModuleGuid, module.ModuleId, item.Key, item.Value);
                    }
                }
            }

            foreach (ContentPage childPage in contentPage.ChildPages)
            {
                CreatePage(siteSettings, childPage, pageSettings);
            }
        }

        private static void EnsureSiteFeature(Guid siteGuid, Guid featureGuid)
        {
            if ((siteGuid == Guid.Empty) || (featureGuid == Guid.Empty)) return;

            SiteSettings.AddFeature(siteGuid, featureGuid);
        }

        #endregion

        #region Site Folder Creation

        public static bool CreateDefaultSiteFolders(int siteId, int siteIdSource = 0)
        {
            return CreateDefaultSiteFolders(siteId, true, siteIdSource);
        }

        public static bool CreateDefaultSiteFolders(int siteId, bool includeStandardFiles, int siteIdSource = 0)
        {
            if (HttpContext.Current == null)
            {
                return false;
            }

            string siteFolderPath = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/Sites/") + siteId.ToInvariantString() + Path.DirectorySeparatorChar;
            string sourceFilesPath = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/");
            DirectoryInfo dir;
            FileInfo[] theFiles;

            if (!Directory.Exists(siteFolderPath))
            {
                Directory.CreateDirectory(siteFolderPath);
            }

            if (!Directory.Exists(siteFolderPath + "systemfiles"))
            {
                Directory.CreateDirectory(siteFolderPath + "systemfiles");
            }

            if (!Directory.Exists(siteFolderPath + "media"))
            {
                Directory.CreateDirectory(siteFolderPath + "media");
            }

            //if (!Directory.Exists(siteFolderPath + "banners"))
            //{
            //    Directory.CreateDirectory(siteFolderPath + "banners");
            //}

            //if (!Directory.Exists(siteFolderPath + "flash"))
            //{
            //    Directory.CreateDirectory(siteFolderPath + "flash");
            //}

            //if (!Directory.Exists(siteFolderPath + "GalleryImages"))
            //{
            //    Directory.CreateDirectory(siteFolderPath + "GalleryImages");
            //}

            //if (!Directory.Exists(siteFolderPath + "GalleryImages" + Path.DirectorySeparatorChar + "EditorUploadImages"))
            //{
            //    Directory.CreateDirectory(siteFolderPath + "GalleryImages" + Path.DirectorySeparatorChar + "EditorUploadImages");
            //}

            //if (!Directory.Exists(siteFolderPath + "GalleryImages" + Path.DirectorySeparatorChar + "FullSizeImages"))
            //{
            //    Directory.CreateDirectory(siteFolderPath + "GalleryImages" + Path.DirectorySeparatorChar + "FullSizeImages");
            //}

            //if (!Directory.Exists(siteFolderPath + "GalleryImages" + Path.DirectorySeparatorChar + "Thumbnails"))
            //{
            //    Directory.CreateDirectory(siteFolderPath + "GalleryImages" + Path.DirectorySeparatorChar + "Thumbnails");
            //}

            //if (!Directory.Exists(siteFolderPath + "GalleryImages" + Path.DirectorySeparatorChar + "WebImages"))
            //{
            //    Directory.CreateDirectory(siteFolderPath + "GalleryImages" + Path.DirectorySeparatorChar + "WebImages");
            //}
            if (WebConfigSettings.SiteLogoUseMediaFolder)
            {
                if (!Directory.Exists(siteFolderPath + "media" + Path.DirectorySeparatorChar + "logos"))
                {
                    Directory.CreateDirectory(siteFolderPath + "media" + Path.DirectorySeparatorChar + "logos");
                }
            }
            else
            {
                if (!Directory.Exists(siteFolderPath + "logos"))
                {
                    Directory.CreateDirectory(siteFolderPath + "logos");
                }
            }

            if (WebConfigSettings.HtmlFragmentUseMediaFolder)
            {
                if (!Directory.Exists(siteFolderPath + "media" + Path.DirectorySeparatorChar + "htmlfragments"))
                {
                    Directory.CreateDirectory(siteFolderPath + "media" + Path.DirectorySeparatorChar + "htmlfragments");
                }
            }
            else
            {
                if (!Directory.Exists(siteFolderPath + "htmlfragments"))
                {
                    Directory.CreateDirectory(siteFolderPath + "htmlfragments");
                }
            }

            if (!Directory.Exists(siteFolderPath + "index"))
            {
                Directory.CreateDirectory(siteFolderPath + "index");
            }

            if (!Directory.Exists(siteFolderPath + "SharedFiles"))
            {
                Directory.CreateDirectory(siteFolderPath + "SharedFiles");
            }

            if (!Directory.Exists(siteFolderPath + "SharedFiles" + Path.DirectorySeparatorChar + "History"))
            {
                Directory.CreateDirectory(siteFolderPath + "SharedFiles" + Path.DirectorySeparatorChar + "History");
            }

            if (!Directory.Exists(siteFolderPath + "skins"))
            {
                Directory.CreateDirectory(siteFolderPath + "skins");
            }

            if (WebConfigSettings.XmlUseMediaFolder)
            {
                if (!Directory.Exists(siteFolderPath + "media" + Path.DirectorySeparatorChar + "xml"))
                {
                    Directory.CreateDirectory(siteFolderPath + "media" + Path.DirectorySeparatorChar + "xml");
                }

                if (!Directory.Exists(siteFolderPath + "media" + Path.DirectorySeparatorChar + "xsl"))
                {
                    Directory.CreateDirectory(siteFolderPath + "media" + Path.DirectorySeparatorChar + "xsl");
                }
            }
            else
            {
                if (!Directory.Exists(siteFolderPath + "xml"))
                {
                    Directory.CreateDirectory(siteFolderPath + "xml");
                }

                if (!Directory.Exists(siteFolderPath + "xsl"))
                {
                    Directory.CreateDirectory(siteFolderPath + "xsl");
                }
            }

            if (includeStandardFiles)
            {
                //nếu là nhân bản site 
                var sourceFilePathGeneric = sourceFilesPath + Path.DirectorySeparatorChar + "logos";
                sourceFilesPath = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/Sites/") + siteIdSource.ToString(CultureInfo.InvariantCulture) + Path.DirectorySeparatorChar;
                var sourceFilesPathSite1 = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/Sites/") + 1 + Path.DirectorySeparatorChar;
                if (Directory.Exists(sourceFilesPathSite1 + Path.DirectorySeparatorChar + "logos"))
                {
                    dir = new DirectoryInfo(sourceFilesPathSite1 + Path.DirectorySeparatorChar + "logos");

                    theFiles = dir.GetFiles();
                    string destinationFolder;

                    if (WebConfigSettings.SiteLogoUseMediaFolder)
                    {
                        destinationFolder = siteFolderPath
                        + Path.DirectorySeparatorChar + "media"
                        + Path.DirectorySeparatorChar + "logos"
                        + Path.DirectorySeparatorChar;
                    }
                    else
                    {
                        destinationFolder = siteFolderPath
                        + Path.DirectorySeparatorChar + "logos"
                        + Path.DirectorySeparatorChar;
                    }

                    foreach (FileInfo f in theFiles)
                    {
                        if (!File.Exists(destinationFolder + f.Name))
                        {
                            File.Copy(f.FullName, destinationFolder + f.Name);
                        }
                    }
                }

                if (Directory.Exists(sourceFilesPath + Path.DirectorySeparatorChar + "xml"))
                {
                    dir = new DirectoryInfo(sourceFilesPath + Path.DirectorySeparatorChar + "xml");

                    theFiles = dir.GetFiles();
                    string destinationFolder;

                    if (WebConfigSettings.XmlUseMediaFolder)
                    {
                        destinationFolder = siteFolderPath
                        + Path.DirectorySeparatorChar + "media"
                        + Path.DirectorySeparatorChar + "xml"
                        + Path.DirectorySeparatorChar;
                    }
                    else
                    {
                        destinationFolder = siteFolderPath
                        + Path.DirectorySeparatorChar + "xml"
                        + Path.DirectorySeparatorChar;
                    }

                    foreach (FileInfo f in theFiles)
                    {
                        if (!File.Exists(destinationFolder + f.Name))
                        {
                            File.Copy(f.FullName, destinationFolder + f.Name);
                        }
                    }
                }

                if (Directory.Exists(sourceFilesPath + Path.DirectorySeparatorChar + "xsl"))
                {
                    dir = new DirectoryInfo(sourceFilesPath + Path.DirectorySeparatorChar + "xsl");

                    theFiles = dir.GetFiles();
                    string destinationFolder;

                    if (WebConfigSettings.XmlUseMediaFolder)
                    {
                        destinationFolder = siteFolderPath + Path.DirectorySeparatorChar + "media" + Path.DirectorySeparatorChar + "xsl" + Path.DirectorySeparatorChar;
                    }
                    else
                    {
                        destinationFolder = siteFolderPath + Path.DirectorySeparatorChar + "xsl" + Path.DirectorySeparatorChar;
                    }

                    foreach (FileInfo f in theFiles)
                    {
                        if (!File.Exists(destinationFolder + f.Name))
                        {
                            File.Copy(f.FullName, destinationFolder + f.Name);
                        }
                    }
                }

                if (Directory.Exists(sourceFilesPath + Path.DirectorySeparatorChar + "htmlfragments"))
                {
                    dir = new DirectoryInfo(sourceFilesPath + Path.DirectorySeparatorChar + "htmlfragments");

                    theFiles = dir.GetFiles();
                    string destinationFolder;

                    if (WebConfigSettings.HtmlFragmentUseMediaFolder)
                    {
                        destinationFolder = siteFolderPath
                        + Path.DirectorySeparatorChar + "media"
                        + Path.DirectorySeparatorChar + "htmlfragments"
                        + Path.DirectorySeparatorChar;
                    }
                    else
                    {
                        destinationFolder = siteFolderPath
                        + Path.DirectorySeparatorChar + "htmlfragments"
                        + Path.DirectorySeparatorChar;

                    }

                    foreach (FileInfo f in theFiles)
                    {
                        if (!File.Exists(destinationFolder + f.Name))
                        {
                            File.Copy(f.FullName, destinationFolder + f.Name);
                        }
                    }
                }
            }

            EnsureAdditionalSiteFolders(siteId);

            return true;
        }

        public static void EnsureAdditionalSiteFolders()
        {
            DataTable dataTable = SiteSettings.GetSiteIdList();
            foreach (DataRow row in dataTable.Rows)
            {
                int siteId = Convert.ToInt32(row["SiteID"]);
                EnsureAdditionalSiteFolders(siteId);

            }
        }

        public static void EnsureAdditionalSiteFolders(int siteId)
        {
            string siteFolderPath =
                HttpContext.Current.Server.MapPath(GetApplicationRoot() +
                "/Data/Sites/" +
                siteId.ToString(CultureInfo.InvariantCulture) + "/");

            EnsureTemplateImageFolder(siteFolderPath);
            EnsureUserFilesFolder(siteFolderPath);
        }

        public static void EnsureTemplateImageFolder(string siteFolderPath)
        {
            try
            {
                if (Directory.Exists(siteFolderPath + "htmltemplateimages"))
                {
                    return;
                }
                else
                {
                    Directory.CreateDirectory(siteFolderPath + "htmltemplateimages");
                }

                string sourceFilesPath = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/");

                DirectoryInfo dir;
                FileInfo[] theFiles;

                if (Directory.Exists(sourceFilesPath + Path.DirectorySeparatorChar + "htmltemplateimages"))
                {
                    dir = new DirectoryInfo(sourceFilesPath + Path.DirectorySeparatorChar + "htmltemplateimages");

                    theFiles = dir.GetFiles();
                    string destinationFolder =
                        siteFolderPath +
                        Path.DirectorySeparatorChar +
                        "htmltemplateimages" +
                        Path.DirectorySeparatorChar;

                    foreach (FileInfo f in theFiles)
                    {
                        if (!File.Exists(destinationFolder + f.Name))
                        {
                            File.Copy(f.FullName, destinationFolder + f.Name);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                log.Error("failed to ensure content template image folder or files", ex);
            }
        }

        public static void EnsureUserFilesFolder(string siteFolderPath)
        {
            if (Directory.Exists(siteFolderPath + "userfiles"))
            {
                return;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(siteFolderPath + "userfiles");
                }
                catch (IOException ex)
                {
                    log.Error("failed to ensure user files folder", ex);
                }
            }
        }

        public static void EnsureSkins(int siteId)
        {
            if (HttpContext.Current == null) { return; }

            string skinFolderPath = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/Sites/")
                + siteId.ToString(CultureInfo.InvariantCulture)
                + Path.DirectorySeparatorChar
                + "skins";

            if (!Directory.Exists(skinFolderPath))
            {
                CreateOrRestoreSiteSkins(siteId);
            }
        }

        public static bool CreateOrRestoreSiteSkins(int siteId, int siteIdSource = 0)
        {
            if (HttpContext.Current == null) { return false; }

            string siteFolderPath = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/Sites/") + siteId.ToString(CultureInfo.InvariantCulture) + Path.DirectorySeparatorChar;
            string sourceFilesPath = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/");
            DirectoryInfo dir;
            DirectoryInfo dirDest;

            if (!Directory.Exists(siteFolderPath + "skins"))
            {
                Directory.CreateDirectory(siteFolderPath + "skins");
            }

            if (Directory.Exists(sourceFilesPath + Path.DirectorySeparatorChar + "skins"))
            {
                dirDest = new DirectoryInfo(siteFolderPath + Path.DirectorySeparatorChar + "skins");
                //trường hợp là gender site
                if (siteIdSource > 0)
                {
                    sourceFilesPath = HttpContext.Current.Server.MapPath(GetApplicationRoot() + "/Data/Sites/") + siteIdSource.ToString(CultureInfo.InvariantCulture) + Path.DirectorySeparatorChar;

                    dir = new DirectoryInfo(sourceFilesPath + Path.DirectorySeparatorChar + "skins");
                }
                else
                {
                    dir = new DirectoryInfo(sourceFilesPath + Path.DirectorySeparatorChar + "skins");
                }

                CopySkinFilesRecursively(dir, dirDest);

                //theDirectories = dir.GetDirectories();
                //DirectoryInfo[] theSubDirectories;
                //foreach (DirectoryInfo d in theDirectories)
                //{
                //    // don't want .svn files
                //    if (!d.Name.StartsWith("."))
                //    {
                //        try
                //        {
                //            dirDestination.CreateSubdirectory(d.Name);
                //            theFiles = d.GetFiles();
                //            foreach (FileInfo f in theFiles)
                //            {
                //                try
                //                {
                //                    File.Copy(
                //                        f.FullName,
                //                        dirDestination.FullName + Path.DirectorySeparatorChar
                //                        + d.Name + Path.DirectorySeparatorChar + f.Name, true);
                //                }
                //                catch (UnauthorizedAccessException) { }
                //                catch (System.IO.IOException) { }
                //                //catch (System.IO.DirectoryNotFoundException) { }

                //            }

                //            //added 2010-02-20 to get the Images folder beneath Artisteer skins
                //            theSubDirectories = d.GetDirectories();
                //            foreach (DirectoryInfo sub in theSubDirectories)
                //            {
                //                if (sub.Name.StartsWith(".")) { continue; } //.svn files
                //                dirDestination.CreateSubdirectory(d.Name + Path.DirectorySeparatorChar + sub.Name);
                //                theFiles = sub.GetFiles();
                //                foreach (FileInfo f in theFiles)
                //                {
                //                    try
                //                    {
                //                        File.Copy(
                //                            f.FullName,
                //                            dirDestination.FullName + Path.DirectorySeparatorChar
                //                            + d.Name + Path.DirectorySeparatorChar + sub.Name + Path.DirectorySeparatorChar + f.Name, true);
                //                    }
                //                    catch (UnauthorizedAccessException) { }
                //                    catch (System.IO.IOException) { }
                //                    //catch (System.IO.DirectoryNotFoundException) { }

                //                }
                //            }

                //        }
                //        catch (System.Security.SecurityException ex)
                //        {
                //            log.Error("error trying to copy skins into site skins folder ", ex);
                //        }


                //    }
                //}
            }

            return true;
        }

        public static void CopySkinFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            if (!source.Name.StartsWith(".")) // Make sure to not copy .git, .svn, .vs, etc, folders from the Data/Sites/skins folder
            {
                foreach (DirectoryInfo dir in source.GetDirectories())
                {
                    if (!dir.Name.StartsWith(".")) // Make sure to not copy .git, .svn, .vs, etc, folders from the current skin folder
                    {
                        CopySkinFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
                    }
                }

                foreach (FileInfo file in source.GetFiles())
                {
                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                }
            }
        }

        #endregion

        #region Newsletter Setup

        public static void CreateDefaultLetterTemplates(Guid siteGuid)
        {
            if (HttpContext.Current == null) return;

            string pathToTemplates = HttpContext.Current.Server.MapPath("~/Data/emailtemplates");

            if (!Directory.Exists(pathToTemplates)) return;

            DirectoryInfo dir = new DirectoryInfo(pathToTemplates);
            FileInfo[] templates = dir.GetFiles("*.config");
            foreach (FileInfo template in templates)
            {
                LetterHtmlTemplate emailTemplate = new LetterHtmlTemplate();
                emailTemplate.SiteGuid = siteGuid;
                emailTemplate.Title = template.Name.Replace(".config", string.Empty);
                StreamReader contentStream = template.OpenText();
                emailTemplate.Html = contentStream.ReadToEnd();
                contentStream.Close();
                emailTemplate.Save();
            }
        }

        #endregion

        #region Helper Methods

        public static string GetMessageTemplate(String templateFolder, String templateFile)
        {
            if (templateFile != null)
            {
                string culture = ConfigurationManager.AppSettings["Culture"];
                if (culture == null)
                {
                    culture = "en-US-";
                }

                string messageFile;

                if (File.Exists(templateFolder + culture + "-" + templateFile))
                {
                    messageFile = templateFolder + culture + "-" + templateFile;
                }
                else
                {
                    messageFile = templateFolder + "en-US-" + templateFile;

                }

                FileInfo file = new FileInfo(messageFile);
                StreamReader sr = file.OpenText();
                string message = sr.ReadToEnd();
                sr.Close();
                return message;
            }
            else
            {
                return String.Empty;
            }
        }

        public static String GetMessageTemplateFolder()
        {
            string result = String.Empty;

            if (HttpContext.Current != null)
            {
                result = HttpContext.Current.Server.MapPath(GetApplicationRoot()
                    + "/Data/MessageTemplates") + Path.DirectorySeparatorChar;
            }
            return result;
        }


        public static string GetApplicationRoot()
        {
            if (HttpContext.Current.Request.ApplicationPath.Length == 1)
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.ApplicationPath;
            }
        }

        // Returns hostname[:port] to use when constructing the site root URL.
        private static string GetHost(string protocol)
        {
            string serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            string serverPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];

            // Most proxies add an X-Forwarded-Host header which contains the original Host header
            // including any non-default port.

            string forwardedHosts = HttpContext.Current.Request.Headers["X-Forwarded-Host"];
            if (forwardedHosts != null)
            {
                // If the request passed thru multiple proxies, they will be separated by commas.
                // We only care about the first one.
                string forwardedHost = forwardedHosts.Split(',')[0];
                string[] serverAndPort = forwardedHost.Split(':');
                serverName = serverAndPort[0];
                serverPort = null;
                if (serverAndPort.Length > 1)
                {
                    serverPort = serverAndPort[1];

                }
            }

            // Only include a port if it is not the default for the protocol and MapAlternatePort = true
            // in the config file.
            if ((protocol == "http" && serverPort == "80")
                || (protocol == "https" && serverPort == "443"))
            {
                serverPort = null;
            }

            // added to fix issue reported by user running normal on port 80 but ssl on port 472
            if (protocol == "https" && serverPort == "80")
            {
                //string blnMapSSLPort = ConfigurationManager.AppSettings["MapAlternateSSLPort"];
                if (WebConfigSettings.MapAlternateSSLPort)
                {
                    string alternatSSLPort = ConfigurationManager.AppSettings["AlternateSSLPort"];
                    if (alternatSSLPort != null)
                    {
                        serverPort = alternatSSLPort;
                    }

                }
            }

            string host = serverName;

            if (serverPort != null)
            {
                //string mapPortSetting = ConfigurationManager.AppSettings["MapAlternatePort"];
                if (WebConfigSettings.MapAlternatePort)
                {
                    host += ":" + serverPort;
                }
            }
            return host;
        }

        public static string GetSiteRoot()
        {
            string protocol = "http";
            if (HttpContext.Current.Request.ServerVariables["HTTPS"] == "on")
            {
                protocol += "s";
            }

            string host = GetHost(protocol);
            return protocol + "://" + host + GetApplicationRoot();
        }

        public static string GetHostName()
        {
            string serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToLower();

            return serverName;
        }

        public static string GetSecureSiteRoot()
        {
            string protocol = "https";
            string host = GetHost(protocol);
            return protocol + "://" + host + GetApplicationRoot();
        }

        public static string GetVirtualRoot()
        {
            string serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];

            return "/" + serverName + GetApplicationRoot();
        }
        #endregion
        public static string SuggestUrl(string text, SiteSettings siteSettings)
        {
            return SiteUtils.SuggestFriendlyUrl(text, siteSettings);
        }
    }
    /// <summary>
    /// Lưu page skin và page setting lưu trong db
    /// </summary>
    public class PageStore
    {
        public int PageSkinID { get; set; }
        public int PageSkinParent { get; set; }
        public int PageID { get; set; }
        public Guid PageGuid { get; set; }
    }

    public class CategoryParent
    {
        public int CategoryID { get; set; }
        public int SkinCategoryID { get; set; }
        public int SkinCategoryParentID { get; set; }
    }

    public class CategoryVersion
    {
        public int CategoryOld { get; set; }
        public int CategoryOldParent { get; set; }
        public int CategoryNew { get; set; }
    }
    public class PageVersion
    {
        public int PageOld { get; set; }
        public int PageOldParent { get; set; }
        public int PageNew { get; set; }
        public Guid PageNewGuid { get; set; }
    }
}
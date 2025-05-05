using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Controls.google;
using mojoPortal.Web.Framework;

namespace mojoPortal.Features
{
    /// <summary>
    /// encapsulates the feature instance configuration loaded from module settings into a more friendly object
    /// </summary>
    public class ArticleConfiguration
    {
        public ArticleConfiguration()
        { }

        public ArticleConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("ArticleCategoryConfigSetting"))
                articleCategoryConfig = settings["ArticleCategoryConfigSetting"].ToString();

            useExcerpt = WebUtils.ParseBoolFromHashtable(settings, "BlogUseExcerptSetting", useExcerpt);

            useHotArticle = WebUtils.ParseBoolFromHashtable(settings, "UseHotArticleSetting", useHotArticle);

            useMostViewArticle = WebUtils.ParseBoolFromHashtable(settings, "UseMostViewArticleSetting", useMostViewArticle);

            useTabArticle = WebUtils.ParseBoolFromHashtable(settings, "UseTabArticleSetting", useTabArticle);
            useTabArticle2 = WebUtils.ParseBoolFromHashtable(settings, "UseTabArticle2Setting", useTabArticle2);
            useTabArticle3 = WebUtils.ParseBoolFromHashtable(settings, "UseTabArticle3Setting", useTabArticle3);
            useListArticle = WebUtils.ParseBoolFromHashtable(settings, "UseListArticleSetting", useListArticle);

            useMultiTabArticle = WebUtils.ParseBoolFromHashtable(settings, "UseMultiTabArticleSetting", useMultiTabArticle);

            numberCategoriesLimit = WebUtils.ParseInt32FromHashtable(settings, "NumberCategoriesLimitSetting", numberCategoriesLimit);
            numberArticleLimit = WebUtils.ParseInt32FromHashtable(settings, "NumberArticleLimitSetting", NumberArticleLimit);

            useSlideArticle = WebUtils.ParseBoolFromHashtable(settings, "UseSlideArticleSetting", useSlideArticle);

            showArticleHotDisplay = WebUtils.ParseInt32FromHashtable(settings, "ShowArticleHotDisplaySetting", showArticleHotDisplay);
            showArticleHotRight = WebUtils.ParseInt32FromHashtable(settings, "ShowArticleHotRightSetting", showArticleHotRight);

            showArticleHotTab = WebUtils.ParseInt32FromHashtable(settings, "ShowArticleHotTabSetting", showArticleHotTab);

            showArticleDisplay = WebUtils.ParseInt32FromHashtable(settings, "ShowArticleDisplaySetting", showArticleDisplay);

            showArticleDetailDisplay = WebUtils.ParseInt32FromHashtable(settings, "ShowArticleDetailDisplaySetting", showArticleDetailDisplay);
            showNewsDisplay = WebUtils.ParseInt32FromHashtable(settings, "ShowNewsDisplaySetting", showNewsDisplay);

            showDescriptionDisplay = WebUtils.ParseBoolFromHashtable(settings, "ShowDescriptionDisplaySetting", showDescriptionDisplay);

            showIsHot = WebUtils.ParseBoolFromHashtable(settings, "ShowIsHotSetting", showIsHot);

            useClassOrther = WebUtils.ParseBoolFromHashtable(settings, "UseClassOrtherSetting", useClassOrther);

            slideTimeTransition = WebUtils.ParseInt32FromHashtable(settings, "SlideTimeTransitionSetting", slideTimeTransition);

            mainSlideExcerpt = WebUtils.ParseInt32FromHashtable(settings, "MainSlideExcerptSetting", mainSlideExcerpt);

            mainSlideTitleMaxChar = WebUtils.ParseInt32FromHashtable(settings, "MainSlideTitleMaxCharSetting", mainSlideTitleMaxChar);

            useImageDialog = WebUtils.ParseBoolFromHashtable(settings, "ImageDialogSetting", useImageDialog);

            syncPostPublished = WebUtils.ParseBoolFromHashtable(settings, "SyncPostPublishedSetting", syncPostPublished);

            showImageInViewPostSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowImageInViewPostSetting", showImageInViewPostSetting);
            showImageMorePost = WebUtils.ParseBoolFromHashtable(settings, "ShowImageMorePostsSetting", showImageMorePost);

            if (settings.Contains("ArticleLoaderSelectorSetting"))
            {
                articleCategoriesSelected = settings["ArticleLoaderSelectorSetting"].ToString();
            }

            if (settings.Contains("GoTopSetting"))
            {
                goToTop = settings["GoTopSetting"].ToString();
            }

            if (settings.Contains("ArticleModuleSelectorSetting"))
            {
                articleModuleSelectorSetting = settings["ArticleModuleSelectorSetting"].ToString();
            }

            if (settings.Contains("EmailNewPostSetting"))
            {
                emailReceiveNewPost = settings["EmailNewPostSetting"].ToString().Trim();
                emailNewPost = emailReceiveNewPost.SplitOnChar('|');
            }

            useExcerptInFeed = WebUtils.ParseBoolFromHashtable(settings, "UseExcerptInFeedSetting", useExcerptInFeed);

            socialInMainArticle = WebUtils.ParseBoolFromHashtable(settings, "SocialInMainArticleSetting", socialInMainArticle);

            titleOnly = WebUtils.ParseBoolFromHashtable(settings, "BlogShowTitleOnlySetting", titleOnly);

            showArticleMostRead = WebUtils.ParseBoolFromHashtable(settings, "ShowArticleMostReadSetting", showArticleMostRead);

            showPager = WebUtils.ParseBoolFromHashtable(settings, "ArticleShowPagerInListSetting", showPager);

            googleMapIncludeWithExcerpt = WebUtils.ParseBoolFromHashtable(settings, "GoogleMapIncludeWithExcerptSetting", googleMapIncludeWithExcerpt);

            enableContentRating = WebUtils.ParseBoolFromHashtable(settings, "EnableContentRatingSetting", enableContentRating);

            enableRatingComments = WebUtils.ParseBoolFromHashtable(settings, "EnableRatingCommentsSetting", enableRatingComments);

            accordionMode = WebUtils.ParseBoolFromHashtable(settings, "AccordionModeSetting", accordionMode);

            excerptLength = WebUtils.ParseInt32FromHashtable(settings, "BlogExcerptLengthSetting", excerptLength);

            if (settings.Contains("BlogExcerptSuffixSetting"))
            {
                excerptSuffix = settings["BlogExcerptSuffixSetting"].ToString();
            }

            if (settings.Contains("ShowBlogMoreLinkText"))
            {
                showMoreLinkText = bool.Parse(settings["ShowBlogMoreLinkText"].ToString());
            }

            if (settings.Contains("BlogMoreLinkText"))
            {
                moreLinkText = settings["BlogMoreLinkText"].ToString();
            }

            if (settings.Contains("BlogAuthorSetting"))
            {
                blogAuthor = settings["BlogAuthorSetting"].ToString();
            }

            if (settings.Contains("CustomCssClassSetting"))
            {
                instanceCssClass = settings["CustomCssClassSetting"].ToString();
            }
            if (settings.Contains("CustomViewListCssClassSetting"))
            {
                viewListCssClass = settings["CustomViewListCssClassSetting"].ToString();
            }

            if (settings.Contains("ArticleDateTimeFormat"))
            {
                string format = settings["ArticleDateTimeFormat"].ToString().Trim();
                if (format.Length > 0)
                {
                    try
                    {
#pragma warning disable 168
                        string d = DateTime.Now.ToString(format, CultureInfo.CurrentCulture);
#pragma warning restore 168
                        dateTimeFormat = format;
                    }
                    catch (FormatException) { }
                }

            }
            if (settings.Contains("TitleSetting"))
            {
                titleSetting = settings["TitleSetting"].ToString();
            }

            otherArticlesDetailPageSizeSetting = WebUtils.ParseInt32FromHashtable(settings, "OtherArticlesDetailPageSizeSetting", otherArticlesDetailPageSizeSetting);

            showBottomPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowBottomPanelSetting", showBottomPanelSetting);

            showTopPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowTopPanelSetting", showTopPanelSetting);

            showLeftPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowLeftPanelSetting", showLeftPanelSetting);

            showRightPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowRightPanelSetting", showRightPanelSetting);

            useTagCloudForCategories = WebUtils.ParseBoolFromHashtable(settings, "BlogUseTagCloudForCategoriesSetting", useTagCloudForCategories);

            showCalendar = WebUtils.ParseBoolFromHashtable(settings, "BlogShowCalendarSetting", showCalendar);

            showCategories = WebUtils.ParseBoolFromHashtable(settings, "BlogShowCategoriesSetting", showCategories);

            showArchives = WebUtils.ParseBoolFromHashtable(settings, "BlogShowArchiveSetting", showArchives);

            showStatistics = WebUtils.ParseBoolFromHashtable(settings, "BlogShowStatisticsSetting", showStatistics);

            showFeedLinks = WebUtils.ParseBoolFromHashtable(settings, "BlogShowFeedLinksSetting", showFeedLinks);

            showAddFeedLinks = WebUtils.ParseBoolFromHashtable(settings, "BlogShowAddFeedLinksSetting", showAddFeedLinks);

            navigationOnRight = WebUtils.ParseBoolFromHashtable(settings, "BlogNavigationOnRightSetting", navigationOnRight);

            allowComments = WebUtils.ParseBoolFromHashtable(settings, "BlogAllowComments", allowComments);

            useLinkForHeading = WebUtils.ParseBoolFromHashtable(settings, "BlogUseLinkForHeading", useLinkForHeading);

            showPostAuthor = WebUtils.ParseBoolFromHashtable(settings, "ShowPostAuthorSetting", showPostAuthor);

            showAuthorOccupation = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorOccupationSetting", showAuthorOccupation);

            showAuthorYahoo = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorYahooSetting", showAuthorYahoo);

            showAuthorWebsiteUrl = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorWebsiteUrlSetting", showAuthorWebsiteUrl);

            showAuthorSignature = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorSignatureSetting", showAuthorSignature);
            showImageRight = WebUtils.ParseBoolFromHashtable(settings, "ShowImageRightSetting", showImageRight);

            if (settings.Contains("GoogleMapInitialMapTypeSetting"))
            {
                string gmType = settings["GoogleMapInitialMapTypeSetting"].ToString();
                try
                {
                    mapType = (MapType)Enum.Parse(typeof(MapType), gmType);
                }
                catch (ArgumentException) { }
            }

            googleMapHeight = WebUtils.ParseInt32FromHashtable(settings, "GoogleMapHeightSetting", googleMapHeight);

            googleMapWidth = WebUtils.ParseInt32FromHashtable(settings, "GoogleMapWidthSetting", googleMapWidth);


            googleMapEnableMapType = WebUtils.ParseBoolFromHashtable(settings, "GoogleMapEnableMapTypeSetting", googleMapEnableMapType);

            googleMapEnableZoom = WebUtils.ParseBoolFromHashtable(settings, "GoogleMapEnableZoomSetting", googleMapEnableZoom);

            googleMapShowInfoWindow = WebUtils.ParseBoolFromHashtable(settings, "GoogleMapShowInfoWindowSetting", googleMapShowInfoWindow);

            googleMapEnableLocalSearch = WebUtils.ParseBoolFromHashtable(settings, "GoogleMapEnableLocalSearchSetting", googleMapEnableLocalSearch);

            googleMapEnableDirections = WebUtils.ParseBoolFromHashtable(settings, "GoogleMapEnableDirectionsSetting", googleMapEnableDirections);

            googleMapInitialZoom = WebUtils.ParseInt32FromHashtable(settings, "GoogleMapInitialZoomSetting", googleMapInitialZoom);

            pageSize = WebUtils.ParseInt32FromHashtable(settings, "BlogEntriesToShowSetting", pageSize);

            if (settings.Contains("OdiogoFeedIDSetting"))
            {
                odiogoFeedId = settings["OdiogoFeedIDSetting"].ToString();
            }

            if (settings.Contains("OdiogoPodcastUrlSetting"))
                odiogoPodcastUrl = settings["OdiogoPodcastUrlSetting"].ToString();

            if (settings.Contains("BlogFeedburnerFeedUrl"))
            {
                feedburnerFeedUrl = settings["BlogFeedburnerFeedUrl"].ToString().Trim();
            }

            if (settings.Contains("DisqusSiteShortName"))
            {
                disqusSiteShortName = settings["DisqusSiteShortName"].ToString();
            }

            if (settings.Contains("CommentSystemSetting"))
            {
                commentSystem = settings["CommentSystemSetting"].ToString();
            }

            if (settings.Contains("IntenseDebateAccountId"))
            {
                intenseDebateAccountId = settings["IntenseDebateAccountId"].ToString();
            }

            allowWebSiteUrlForComments = WebUtils.ParseBoolFromHashtable(settings, "AllowWebSiteUrlForComments", allowWebSiteUrlForComments);

            hideDetailsFromUnauthencticated = WebUtils.ParseBoolFromHashtable(settings, "HideDetailsFromUnauthencticated", hideDetailsFromUnauthencticated);

            if (settings.Contains("BlogCopyrightSetting"))
            {
                copyright = settings["BlogCopyrightSetting"].ToString();
            }

            enableContentVersioning = WebUtils.ParseBoolFromHashtable(settings, "BlogEnableVersioningSetting", enableContentVersioning);

            defaultCommentDaysAllowed = WebUtils.ParseInt32FromHashtable(settings, "BlogCommentForDaysDefault", defaultCommentDaysAllowed);

            if (settings.Contains("BlogEditorHeightSetting"))
            {
                editorHeight = Unit.Parse(settings["BlogEditorHeightSetting"].ToString());

            }

            useCaptcha = WebUtils.ParseBoolFromHashtable(settings, "BlogUseCommentSpamBlocker", useCaptcha);

            requireAuthenticationForComments = WebUtils.ParseBoolFromHashtable(settings, "RequireAuthenticationForComments", requireAuthenticationForComments);

            notifyOnComment = WebUtils.ParseBoolFromHashtable(settings, "ContentNotifyOnComment", notifyOnComment);

            if (settings.Contains("BlogAuthorEmailSetting"))
            {
                notifyEmail = settings["BlogAuthorEmailSetting"].ToString();
            }

            useFacebookLikeButton = WebUtils.ParseBoolFromHashtable(settings, "UseFacebookLikeButton", useFacebookLikeButton);

            useFacebookShareButton = WebUtils.ParseBoolFromHashtable(settings, "UseFacebookShareButton", useFacebookShareButton);

            if (settings.Contains("FacebookLikeButtonTheme"))
            {
                facebookLikeButtonTheme = settings["FacebookLikeButtonTheme"].ToString().Trim();
            }

            facebookLikeButtonShowFaces = WebUtils.ParseBoolFromHashtable(settings, "FacebookLikeButtonShowFaces", facebookLikeButtonShowFaces);

            facebookLikeButtonWidth = WebUtils.ParseInt32FromHashtable(settings, "FacebookLikeButtonWidth", facebookLikeButtonWidth);

            facebookLikeButtonHeight = WebUtils.ParseInt32FromHashtable(settings, "FacebookLikeButtonHeight", facebookLikeButtonHeight);

            showTweetThisLink = WebUtils.ParseBoolFromHashtable(settings, "ShowTweetThisLink", showTweetThisLink);

            useGoogleBookmarkButton = WebUtils.ParseBoolFromHashtable(settings, "UseGoogleBookmarkButton", useGoogleBookmarkButton);

            showImage = WebUtils.ParseBoolFromHashtable(settings, "ImageShowSetting", showImage);

            if (settings.Contains("ImageCustomCssClassSetting"))
            {
                imageCssClass = settings["ImageCustomCssClassSetting"].ToString();
            }

            showOtherArticles = WebUtils.ParseBoolFromHashtable(settings, "OtherArticlesShowSetting", showOtherArticles);

            otherArticlesUsePaging = WebUtils.ParseBoolFromHashtable(settings, "OtherArticlesPagingSetting", otherArticlesUsePaging);

            otherArticlesShowMoreLinkSetting = WebUtils.ParseBoolFromHashtable(settings, "OtherArticlesShowMoreLinkSetting", otherArticlesShowMoreLinkSetting);

            if (settings.Contains("OtherArticlesMoreLinkSetting"))
            {
                otherArticlesMoreLinkSetting = settings["OtherArticlesMoreLinkSetting"].ToString();
            }

            if (settings.Contains("OtherArticlesMoreLinkTextSetting"))
            {
                otherArticlesMoreLinkTextSetting = settings["OtherArticlesMoreLinkTextSetting"].ToString();
            }

            autoScrollSetting = WebUtils.ParseBoolFromHashtable(settings, "AutoScrollSetting", autoScrollSetting);

            autoScrollVerticalSetting = WebUtils.ParseBoolFromHashtable(settings, "AutoScrollVerticalSetting", autoScrollVerticalSetting);

            autoScrollCircularSetting = WebUtils.ParseBoolFromHashtable(settings, "AutoScrollCircularSetting", autoScrollCircularSetting);

            autoScrollSpeedSetting = WebUtils.ParseInt32FromHashtable(settings, "AutoScrollSpeedSetting", autoScrollSpeedSetting);

            if (settings.Contains("AutoScrollHeightWrapperSetting"))
            {
                autoScrollHeightWrapperSetting = settings["AutoScrollHeightWrapperSetting"].ToString();
            }

            if (settings.Contains("AutoScrollWidthWrapperSetting"))
            {
                autoScrollWidthWrapperSetting = settings["AutoScrollWidthWrapperSetting"].ToString();
            }

            if (settings.Contains("AutoScrollEasingSetting"))
            {
                autoScrollEasingSetting = settings["AutoScrollEasingSetting"].ToString();
            }

            autoScrollTimeSetting = WebUtils.ParseInt32FromHashtable(settings, "AutoScrollTimeSetting", autoScrollTimeSetting);

            autoScrollItemsSetting = WebUtils.ParseInt32FromHashtable(settings, "AutoScrollItemsSetting", autoScrollItemsSetting);

            maxNumberOfCharactersInTitleSetting = WebUtils.ParseInt32FromHashtable(settings, "MaxNumberOfCharactersInTitleSetting", maxNumberOfCharactersInTitleSetting);

            maxNumberOfCharactersInMainOthers = WebUtils.ParseInt32FromHashtable(settings, "MaxNumberOfCharactersInMainOthersSetting", maxNumberOfCharactersInMainOthers);

            maxNumberOfCharactersInDetailOthers = WebUtils.ParseInt32FromHashtable(settings, "MaxNumberOfCharactersInDetailOthersSetting", maxNumberOfCharactersInDetailOthers);

            showEditInPost = WebUtils.ParseBoolFromHashtable(settings, "ShowEditInPostSetting", showEditInPost);

            useTooltipSettings = WebUtils.ParseBoolFromHashtable(settings, "UseTooltipSettings", useTooltipSettings);

            if (settings.Contains("UseTooltipCssSettings"))
            {
                useTooltipCssSettings = settings["UseTooltipCssSettings"].ToString();
            }

            tooltipMaxCharSettings = WebUtils.ParseInt32FromHashtable(settings, "TooltipMaxCharSettings", tooltipMaxCharSettings);

            if (settings.Contains("TooltipEffectSettings"))
            {
                tooltipEffectSettings = settings["TooltipEffectSettings"].ToString();
            }

            tooltipDelaySettings = WebUtils.ParseInt32FromHashtable(settings, "TooltipDelaySettings", tooltipDelaySettings);

            otherArticlesPageSizeSetting = WebUtils.ParseInt32FromHashtable(settings, "OtherArticlesPageSizeSetting", otherArticlesPageSizeSetting);

            otherArticlesPagingSetting = WebUtils.ParseBoolFromHashtable(settings, "OtherArticlesPagingSetting", otherArticlesPagingSetting);

            useAttachmentSetting = WebUtils.ParseBoolFromHashtable(settings, "UseAttachmentSetting", useAttachmentSetting);

            useAttachmentDownloadIconSetting = WebUtils.ParseBoolFromHashtable(settings, "UseAttachmentDownloadIconSetting", useAttachmentDownloadIconSetting);

            showAttachmentLabelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowAttachmentLabelSetting", showAttachmentLabelSetting);

            useOverrideUrl = WebUtils.ParseBoolFromHashtable(settings, "UseOverrideUrlSetting", useOverrideUrl);

            if (settings.Contains("OtherArticleSetting"))
            {
                otherArticle = settings["OtherArticleSetting"].ToString();
            }

            showNumberModuleSetting = WebUtils.ParseInt32FromHashtable(settings, "ShowNumberModuleSetting", showNumberModuleSetting);

            useReverse = WebUtils.ParseBoolFromHashtable(settings, "UseReverseSetting", useReverse);

            if (settings.Contains("ShowEventInDetailSetting"))
            {
                showEventInDetailSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowEventInDetailSetting", showEventInDetailSetting);
            }

            if (settings.Contains("ShowTagInDetailSetting"))
            {
                showTagInDetailSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowTagInDetailSetting", showTagInDetailSetting);
            }
            requireApprovalForComments = WebUtils.ParseBoolFromHashtable(settings, "RequireApprovalForComments", requireApprovalForComments);
            allowCommentTitle = WebUtils.ParseBoolFromHashtable(settings, "AllowCommentTitle", allowCommentTitle);
            allowedEditMinutesForUnModeratedPosts = WebUtils.ParseInt32FromHashtable(settings, "AllowedEditMinutesForUnModeratedPosts", allowedEditMinutesForUnModeratedPosts);
            //if (settings.Contains("ShowTagInPostListSetting"))
            //{
            //    showTagInPostListSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowTagInPostListSetting", showTagInPostListSetting);
            //}

            //if (settings.Contains("RolePublishedSetting"))
            //{
            //    roles = settings["RolePublishedSetting"].ToString();
            //    rolePublished = roles.Split(';');
            //}

            if (settings.Contains("RoleApprovedSetting"))
            {
                roles = settings["RoleApprovedSetting"].ToString();
                roleApproved = roles.Split(';');
            }

            if (settings.Contains("RolePostSetting"))
            {
                roles = settings["RolePostSetting"].ToString();
                rolePost = roles.Split(';');
            }

            if (settings.Contains("RolePropertySetting"))
            {
                roles = settings["RolePropertySetting"].ToString();
                roleProperty = roles.Split(';');
            }
            showHorizontal = WebUtils.ParseBoolFromHashtable(settings, "ShowHorizontalSetting", showHorizontal);
            showTitle = WebUtils.ParseBoolFromHashtable(settings, "ShowTitleSetting", showTitle);
            showHotNew = WebUtils.ParseBoolFromHashtable(settings, "ShowHotNewSetting", showHotNew);
            showViewMoreSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowViewMoreSetting", showViewMoreSetting);
            showViewMoreEnglish = WebUtils.ParseBoolFromHashtable(settings, "ShowViewMoreEnglishSetting", showViewMoreEnglish);


            showImgArticleHot = WebUtils.ParseBoolFromHashtable(settings, "ShowImgArticleHotSetting", showHotNew);

            if (settings.Contains("ModuleHotRightCssCustomeSetting"))
                moduleRightCssCustome = settings["ModuleHotRightCssCustomeSetting"].ToString();

            if (settings.Contains("ModuleHotCssCustomeSetting"))
                moduleHotCssCustome = settings["ModuleHotCssCustomeSetting"].ToString();

            if (settings.Contains("ModuleDisplayCssCustomeSetting"))
                moduleDisplayCssCustome = settings["ModuleDisplayCssCustomeSetting"].ToString();
            if (settings.Contains("CategoryHotSetting"))
            {
                categoryHot = settings["CategoryHotSetting"].ToString();
            }
            if (settings.Contains("TitleArticleHotSetting"))
            {
                titleArticleHot = settings["TitleArticleHotSetting"].ToString();
            }

            displayTypeSetting = WebUtils.ParseInt32FromHashtable(settings, "DisplayTypeSetting", displayTypeSetting);
            homeDisplayTypeSetting = WebUtils.ParseInt32FromHashtable(settings, "ArticleHomeTypeDisplaySetting", homeDisplayTypeSetting);

            #region configuration module HomeList
            if (settings.Contains("HomeListSelectorOneSetting"))
            {
                homeListSelectorOneSetting = settings["HomeListSelectorOneSetting"].ToString();
            }
            if (settings.Contains("HomeListSelectorMutipleSetting"))
            {
                homeListSelectorMutipleSetting = settings["HomeListSelectorMutipleSetting"].ToString();
            }
            #endregion

            isDeleteSetting = WebUtils.ParseBoolFromHashtable(settings, "IsDeleteSetting", isDeleteSetting);
            tabSelectorSetting = WebUtils.ParseInt32FromHashtable(settings, "TabSelectorSetting", tabSelectorSetting);
            if (settings.Contains("LichCongTacCategorySetting"))
            {
                lichCongTacCategorySetting = settings["LichCongTacCategorySetting"].ToString();
            }
            if (settings.Contains("ThongBaoCategorySetting"))
            {
                thongBaoCategorySetting = settings["ThongBaoCategorySetting"].ToString();
            }
            numberThongBaoDisplaySetting = WebUtils.ParseInt32FromHashtable(settings, "NumberThongBaoDisplaySetting", numberThongBaoDisplaySetting);
            pageArticleSetting = WebUtils.ParseInt32FromHashtable(settings, "PageArticleSetting", pageArticleSetting);
            if (settings.Contains("ThongBaoCanBoCategorySetting"))
            {
                thongBaoCanBoCategorySetting = settings["ThongBaoCanBoCategorySetting"].ToString();
            }

            if (settings.Contains("ThongBaoNguoiHocCategorySetting"))
            {
                thongBaoNguoiHocCategorySetting = settings["ThongBaoNguoiHocCategorySetting"].ToString();
            }

            if (settings.Contains("PageThongBaoNguoiHocSetting"))
            {
                pageThongBaoNguoiHocSetting = settings["PageThongBaoNguoiHocSetting"].ToString();
            }

            if (settings.Contains("PageThongBaoCanBoSetting"))
            {
                pageThongBaoCanBoSetting = settings["PageThongBaoCanBoSetting"].ToString();
            }

            if (settings.Contains("TitleOrtherSetting"))
            {
                titleOrtherSetting = settings["TitleOrtherSetting"].ToString();
            }
            if (settings.Contains("UrlTitleSetting"))
            {
                urlTitleSetting = settings["UrlTitleSetting"].ToString();
            }
            isArticleClubName = WebUtils.ParseBoolFromHashtable(settings, "IsArticleClubNameSetting", isArticleClubName);
            if (settings.Contains("ArticleClubNameSetting"))
            {
                articleClubName = settings["ArticleClubNameSetting"].ToString();
            }
            if (settings.Contains("TypeThemeSetting"))
            {
                typeThemeSetting = settings["TypeThemeSetting"].ToString();
            }
            categorySetting = WebUtils.ParseInt32FromHashtable(settings, "CategorySetting", categorySetting);

        }
        private int categorySetting;
        public int CategorySetting
        {
            get { return categorySetting; }
        }
        private string typeThemeSetting = ArticleTabListTypeConstant.ShowImage;

        public string TypeThemeSetting
        {
            get { return typeThemeSetting; }
        }

        private string titleOrtherSetting = "TIN HOẠT ĐỘNG";
        public string TitleOrtherSetting
        {
            get { return titleOrtherSetting; }
        }
        private string urlTitleSetting = "/tin-hoat-dong";
        public string UrlTitleSetting
        {
            get { return urlTitleSetting; }
        }
        private string titleArticleHot = "TIN TỨC";
        public string TitleArticleHot
        {
            get { return titleArticleHot; }
        }

        private string pageThongBaoNguoiHocSetting = string.Empty;
        public string PageThongBaoNguoiHocSetting
        {
            get { return pageThongBaoNguoiHocSetting; }
        }
        private string pageThongBaoCanBoSetting = string.Empty;
        public string PageThongBaoCanBoSetting
        {
            get { return pageThongBaoCanBoSetting; }
        }


        private int pageArticleSetting = 0;
        public int PageArticleSetting
        {
            get
            {
                return pageArticleSetting;
            }

        }
        private int numberThongBaoDisplaySetting = 3;
        public int NumberThongBaoDisplaySetting
        {
            get { return numberThongBaoDisplaySetting; }
        }

        private string thongBaoCanBoCategorySetting = string.Empty;
        public string ThongBaoCanBoCategorySetting
        {
            get { return thongBaoCanBoCategorySetting; }
        }
        private string thongBaoNguoiHocCategorySetting = string.Empty;
        public string ThongBaoNguoiHocCategorySetting
        {
            get { return thongBaoNguoiHocCategorySetting; }
        }


        private string thongBaoCategorySetting = string.Empty;
        public string ThongBaoCategorySetting
        {
            get { return thongBaoCategorySetting; }
        }

        private string lichCongTacCategorySetting = string.Empty;
        public string LichCongTacCategorySetting
        {
            get { return lichCongTacCategorySetting; }
        }

        private bool showViewMoreSetting = true;
        public bool ShowViewMoreSetting
        {
            get { return showViewMoreSetting; }
        }
        private bool showViewMoreEnglish = false;
        public bool ShowViewMoreEnglish
        {
            get { return showViewMoreEnglish; }
        }

        private int homeDisplayTypeSetting = ArticleHomeConstant.Type_1;
        public int HomeDisplayTypeSetting
        {
            get { return homeDisplayTypeSetting; }
        }

        private int displayTypeSetting = DisplaySettingConstant.DisplayType_Khac;
        public int DisplayTypeSetting
        {
            get { return displayTypeSetting; }
        }

        private bool isDeleteSetting = false;
        public bool IsDeleteSetting
        {
            get { return isDeleteSetting; }
        }
        private string categoryHot = string.Empty;
        public string CategoryHot { get { return categoryHot; } }
        private bool showHotNew = false;
        public bool ShotHotNew
        {
            get { return showHotNew; }
        }
        private bool showTitle = true;
        public bool ShowTitle
        {
            get { return showTitle; }
        }
        private bool showHorizontal = false;
        public bool ShowHorizontal
        {
            get { return showHorizontal; }
        }
        private bool sortCommentsDescending = false;
        public bool SortCommentsDescending
        {
            get { return sortCommentsDescending; }
        }
        private int allowedEditMinutesForUnModeratedPosts = 10;

        public int AllowedEditMinutesForUnModeratedPosts
        {
            get { return allowedEditMinutesForUnModeratedPosts; }
        }
        private bool allowCommentTitle = true;
        public bool AllowCommentTitle
        {
            get { return allowCommentTitle; }
        }

        private bool requireApprovalForComments = false;

        public bool RequireApprovalForComments
        {
            get { return requireApprovalForComments; }
        }
        public static bool UseLegacyCommentSystem
        {
            get { return ConfigHelper.GetBoolProperty("Article:UseLegacyCommentSystem", false); }
        }
        private string roles = string.Empty;
        private IList rolePublished;
        public IList RolePublished { get { return rolePublished; } }

        private IList roleApproved;
        public IList RoleApproved { get { return roleApproved; } }

        private IList rolePost;
        public IList RolePost { get { return rolePost; } }

        private IList roleProperty;
        public IList RoleProperty { get { return roleProperty; } }

        private bool useAttachmentSetting = true;
        public bool UseAttachmentSetting
        {
            get { return useAttachmentSetting; }
        }
        private bool isArticleClubName = false;
        public bool IsArrticelClubName
        {
            get { return isArticleClubName; }
        }
        private string articleClubName = string.Empty;
        public string ActicleClubName
        {
            get { return ActicleClubName; }
            set { ActicleClubName = value; }
        }

        private bool showAttachmentLabelSetting;
        public bool ShowAttachmentLabelSetting { get { return showAttachmentLabelSetting; } }

        private bool useAttachmentDownloadIconSetting;
        public bool UseAttachmentDownloadIconSetting { get { return useAttachmentDownloadIconSetting; } }

        private bool useHotArticle;
        public bool UseHotArticle { get { return useHotArticle; } }

        private bool useMostViewArticle;
        public bool UseMostViewArticle { get { return useMostViewArticle; } }

        private bool useTabArticle;
        public bool UseTabArticle { get { return useTabArticle; } }
        private bool useTabArticle2;
        public bool UseTabArticle2 { get { return useTabArticle2; } }
        private bool useTabArticle3;
        public bool UseTabArticle3 { get { return useTabArticle3; } }
        private bool useListArticle;
        public bool UseListArticle { get { return useListArticle; } }

        private bool useMultiTabArticle;
        public bool UseMultiTabArticle { get { return useMultiTabArticle; } }

        private bool useSlideArticle;
        public bool UseSlideArticle { get { return useSlideArticle; } }

        private int slideTimeTransition = 5000;
        public int SlideTimeTransition { get { return slideTimeTransition; } }

        private int mainSlideExcerpt = 500;
        public int MainSlideExcerpt { get { return mainSlideExcerpt; } }

        private int mainSlideTitleMaxChar = 100;
        public int MainSlideTitleMaxChar { get { return mainSlideTitleMaxChar; } }

        private string otherArticle = string.Empty;
        public string OtherArticle { get { return otherArticle; } }

        private string titleSetting = string.Empty;
        public string TitleSetting { get { return titleSetting; } }

        private bool showTweetThisLink;
        public bool ShowTweetThisLink { get { return showTweetThisLink; } }

        private bool useGoogleBookmarkButton = true;
        public bool UseGoogleBookmarkButton { get { return useGoogleBookmarkButton; } }

        private bool useFacebookLikeButton;
        public bool UseFacebookLikeButton { get { return useFacebookLikeButton; } }

        private bool useFacebookShareButton = true;
        public bool UseFacebookShareButton { get { return useFacebookShareButton; } }

        private int facebookLikeButtonHeight = 35;
        public int FacebookLikeButtonHeight { get { return facebookLikeButtonHeight; } }

        private int facebookLikeButtonWidth = 450;
        public int FacebookLikeButtonWidth { get { return facebookLikeButtonWidth; } }

        private bool facebookLikeButtonShowFaces;
        public bool FacebookLikeButtonShowFaces { get { return facebookLikeButtonShowFaces; } }

        private string facebookLikeButtonTheme = "light";
        public string FacebookLikeButtonTheme { get { return facebookLikeButtonTheme; } }

        private string notifyEmail = string.Empty;
        public string NotifyEmail { get { return notifyEmail; } }

        private bool notifyOnComment;
        public bool NotifyOnComment { get { return notifyOnComment; } }

        private bool requireAuthenticationForComments;
        public bool RequireAuthenticationForComments { get { return requireAuthenticationForComments; } }

        private bool useCaptcha = true;
        public bool UseCaptcha { get { return useCaptcha; } }

        private Unit editorHeight = Unit.Parse("350");
        public Unit EditorHeight { get { return editorHeight; } }

        private bool enableContentVersioning = true;
        public bool EnableContentVersioning { get { return enableContentVersioning; } }

        private int defaultCommentDaysAllowed = 90;
        public int DefaultCommentDaysAllowed { get { return defaultCommentDaysAllowed; } }

        private string copyright = string.Empty;
        public string Copyright { get { return copyright; } }

        private bool hideDetailsFromUnauthencticated;
        public bool HideDetailsFromUnauthencticated { get { return hideDetailsFromUnauthencticated; } }

        private bool allowWebSiteUrlForComments = true;
        public bool AllowWebSiteUrlForComments { get { return allowWebSiteUrlForComments; } }

        private string commentSystem = "internal";
        public string CommentSystem { get { return commentSystem; } }

        private string intenseDebateAccountId = string.Empty;
        public string IntenseDebateAccountId
        {
            get { return intenseDebateAccountId; }
        }

        private string disqusSiteShortName = string.Empty;

        public string DisqusSiteShortName
        {
            get { return disqusSiteShortName; }
        }

        private string feedburnerFeedUrl = string.Empty;

        public string FeedburnerFeedUrl
        {
            get { return feedburnerFeedUrl; }
        }

        private string odiogoFeedId = string.Empty;

        public string OdiogoFeedId
        {
            get { return odiogoFeedId; }
        }

        private string odiogoPodcastUrl = string.Empty;

        public string OdiogoPodcastUrl
        {
            get { return odiogoPodcastUrl; }
        }
        private int showArticleHotDisplay = 7;
        public int ShowArticleHotDisplay
        {
            get { return showArticleHotDisplay; }
        }
        private int showArticleHotRight = 7;
        public int ShowArticleHotRight
        {
            get { return showArticleHotRight; }
        }


        private int showArticleHotTab = 3;
        public int ShowArticleHotTab
        {
            get { return showArticleHotTab; }
        }
        private int showArticleDisplay = 3;
        public int ShowArticleDisplay
        {
            get { return showArticleDisplay; }
        }
        private int showArticleDetailDisplay = 5;
        public int ShowArticleDetailDisplay
        {
            get { return showArticleDetailDisplay; }
        }
        private int showNewsDisplay = 1;
        public int ShowNewsDisplay
        {
            get { return showNewsDisplay; }
        }

        private bool showArticleMostRead;
        public bool ShowArticleMostRead
        {
            get { return showArticleMostRead; }
        }
        private int pageSize = 15;

        public int PageSize
        {
            get { return pageSize; }
        }

        private int googleMapInitialZoom = 13;

        public int GoogleMapInitialZoom
        {
            get { return googleMapInitialZoom; }
        }

        private bool googleMapEnableDirections;

        public bool GoogleMapEnableDirections
        {
            get { return googleMapEnableDirections; }
        }

        private bool googleMapEnableLocalSearch;

        public bool GoogleMapEnableLocalSearch
        {
            get { return googleMapEnableLocalSearch; }
        }

        private bool googleMapShowInfoWindow;

        public bool GoogleMapShowInfoWindow
        {
            get { return googleMapShowInfoWindow; }
        }

        private bool googleMapEnableZoom;

        public bool GoogleMapEnableZoom
        {
            get { return googleMapEnableZoom; }
        }

        private bool googleMapEnableMapType;

        public bool GoogleMapEnableMapType
        {
            get { return googleMapEnableMapType; }
        }

        private int googleMapWidth = 500;

        public int GoogleMapWidth
        {
            get { return googleMapWidth; }
        }

        private int googleMapHeight = 300;

        public int GoogleMapHeight
        {
            get { return googleMapHeight; }
        }

        private MapType mapType = MapType.G_SATELLITE_MAP;

        public MapType GoogleMapType
        {
            get { return mapType; }
        }

        private bool showPostAuthor;

        public bool ShowPostAuthor
        {
            get { return showPostAuthor; }
        }

        private bool showAuthorOccupation;

        public bool ShowAuthorOccupation
        {
            get { return showAuthorOccupation; }
        }

        private bool showAuthorYahoo;

        public bool ShowAuthorYahoo
        {
            get { return showAuthorYahoo; }
        }

        private bool showAuthorWebsiteUrl;

        public bool ShowAuthorWebsiteUrl
        {
            get { return showAuthorWebsiteUrl; }
        }

        private bool showAuthorSignature;

        public bool ShowAuthorSignature
        {
            get { return showAuthorSignature; }
        }
        private bool showImageRight;

        public bool ShowImageRight
        {
            get { return showImageRight; }
        }

        private bool useLinkForHeading = true;

        public bool UseLinkForHeading
        {
            get { return useLinkForHeading; }
        }

        private bool allowComments = true;

        public bool AllowComments
        {
            get { return allowComments; }
        }

        private bool navigationOnRight;

        public bool NavigationOnRight
        {
            get { return navigationOnRight; }
        }

        private bool showAddFeedLinks;

        public bool ShowAddFeedLinks
        {
            get { return showAddFeedLinks; }
        }

        private bool showFeedLinks;

        public bool ShowFeedLinks
        {
            get { return showFeedLinks; }
        }

        private bool showStatistics;

        public bool ShowStatistics
        {
            get { return showStatistics; }
        }

        private bool showArchives;

        public bool ShowArchives
        {
            get { return showArchives; }
        }

        private bool showCategories;

        public bool ShowCategories
        {
            get { return showCategories; }
        }

        private bool showCalendar;

        public bool ShowCalendar
        {
            get { return showCalendar; }
        }

        private bool useTagCloudForCategories;

        public bool UseTagCloudForCategories
        {
            get { return useTagCloudForCategories; }
        }

        private string dateTimeFormat = "dd/MM/yyyy";

        public string DateTimeFormat
        {
            get { return dateTimeFormat; }
        }

        private string articleCategoryConfig;
        public string ArticleCategoryConfig
        {
            get { return articleCategoryConfig; }
        }

        private bool useExcerpt;

        public bool UseExcerpt
        {
            get { return useExcerpt; }
        }

        private string articleCategoriesSelected = string.Empty;

        public string ArticleCategoriesSelected
        {
            get { return articleCategoriesSelected; }
        }

        private bool useExcerptInFeed;

        public bool UseExcerptInFeed
        {
            get { return useExcerptInFeed; }
        }

        private bool titleOnly;

        public bool TitleOnly
        {
            get { return titleOnly; }
        }

        private bool showPager = true;

        public bool ShowPager
        {
            get { return showPager; }
        }

        private bool googleMapIncludeWithExcerpt;

        public bool GoogleMapIncludeWithExcerpt
        {
            get { return googleMapIncludeWithExcerpt; }
        }

        private bool enableContentRating;

        public bool EnableContentRating
        {
            get { return enableContentRating; }
        }

        private bool enableRatingComments;

        public bool EnableRatingComments
        {
            get { return enableRatingComments; }
        }

        private int excerptLength = 250;

        public int ExcerptLength
        {
            get { return excerptLength; }
        }

        private string excerptSuffix = "...";

        public string ExcerptSuffix
        {
            get { return excerptSuffix; }
        }

        private bool showMoreLinkText = false;

        public bool ShowMoreLinkText
        {
            get { return showMoreLinkText; }
        }

        private string moreLinkText = "read more";

        public string MoreLinkText
        {
            get { return moreLinkText; }
        }

        private string blogAuthor = string.Empty;

        public string BlogAuthor
        {
            get { return blogAuthor; }
        }

        private string instanceCssClass = string.Empty;

        public string InstanceCssClass
        {
            get { return instanceCssClass; }
        }

        private string viewListCssClass = string.Empty;

        public string ViewListCssClass
        {
            get { return viewListCssClass; }
        }

        private bool showImage = true;

        public bool ShowImage
        {
            get { return showImage; }
        }

        private string imageCssClass = string.Empty;

        public string ImageCssClass
        {
            get { return imageCssClass; }
        }

        private bool showOtherArticles = true;

        public bool ShowOtherArticles
        {
            get { return showOtherArticles; }
        }

        private bool otherArticlesUsePaging;

        public bool OtherArticlesUsePaging
        {
            get { return otherArticlesUsePaging; }
        }

        private bool otherArticlesShowMoreLinkSetting;

        public bool OtherArticlesShowMoreLinkSetting
        {
            get { return otherArticlesShowMoreLinkSetting; }
        }

        private string otherArticlesMoreLinkSetting = string.Empty;

        public string OtherArticlesMoreLinkSetting
        {
            get { return otherArticlesMoreLinkSetting; }
        }

        private string otherArticlesMoreLinkTextSetting = string.Empty;
        public string OtherArticlesMoreLinkTextSetting
        {
            get { return otherArticlesMoreLinkTextSetting; }
        }

        private bool autoScrollSetting;

        public bool AutoScrollSetting
        {
            get { return autoScrollSetting; }
        }

        private bool autoScrollVerticalSetting;

        public bool AutoScrollVerticalSetting
        {
            get { return autoScrollVerticalSetting; }
        }

        private bool autoScrollCircularSetting;

        public bool AutoScrollCircularSetting
        {
            get { return autoScrollCircularSetting; }
        }

        private int autoScrollSpeedSetting = 5000;

        public int AutoScrollSpeedSetting
        {
            get { return autoScrollSpeedSetting; }
        }

        private string autoScrollHeightWrapperSetting = "100%";

        public string AutoScrollHeightWrapperSetting
        {
            get { return autoScrollHeightWrapperSetting; }
        }

        private string autoScrollWidthWrapperSetting = "100%";

        public string AutoScrollWidthWrapperSetting
        {
            get { return autoScrollWidthWrapperSetting; }
        }

        private string autoScrollEasingSetting = "linear";

        public string AutoScrollEasingSetting
        {
            get { return autoScrollEasingSetting; }
        }

        private int autoScrollTimeSetting = 1000;

        public int AutoScrollTimeSetting
        {
            get { return autoScrollTimeSetting; }
        }

        private int autoScrollItemsSetting = 1;

        public int AutoScrollItemsSetting
        {
            get { return autoScrollItemsSetting; }
        }

        private bool useImageDialog;
        public bool UseImageDialog
        {
            get { return useImageDialog; }
        }

        private int maxNumberOfCharactersInTitleSetting = 100;

        public int MaxNumberOfCharactersInTitleSetting
        {
            get { return maxNumberOfCharactersInTitleSetting; }
        }

        private int maxNumberOfCharactersInMainOthers = 50;

        public int MaxNumberOfCharactersInMainOthers
        {
            get { return maxNumberOfCharactersInMainOthers; }
        }

        private int maxNumberOfCharactersInDetailOthers = 100;

        public int MaxNumberOfCharactersInDetailOthers
        {
            get { return maxNumberOfCharactersInDetailOthers; }
        }

        private bool useTooltipSettings;

        public bool UseTooltipSettings
        {
            get { return useTooltipSettings; }
        }

        private string useTooltipCssSettings = "tooltip";

        public string UseTooltipCssSettings
        {
            get { return useTooltipCssSettings; }
        }

        private int tooltipMaxCharSettings = 250;

        public int TooltipMaxCharSettings
        {
            get { return tooltipMaxCharSettings; }
        }

        private string tooltipEffectSettings = "slide";

        public string TooltipEffectSettings
        {
            get { return tooltipEffectSettings; }
        }

        private int tooltipDelaySettings;

        public int TooltipDelaySettings
        {
            get { return tooltipDelaySettings; }
        }

        private bool otherArticlesPagingSetting = true;

        public bool OtherArticlesPagingSetting
        {
            get { return otherArticlesPagingSetting; }
        }

        private int otherArticlesPageSizeSetting = 5;

        public int OtherArticlesPageSizeSetting
        {
            get { return otherArticlesPageSizeSetting; }
        }

        private bool showBottomPanelSetting = true;

        public bool ShowBottomPanelSetting
        {
            get { return showBottomPanelSetting; }
        }

        private bool showTopPanelSetting = true;

        public bool ShowTopPanelSetting
        {
            get { return showTopPanelSetting; }
        }

        private bool showLeftPanelSetting = true;

        public bool ShowLeftPanelSetting
        {
            get { return showLeftPanelSetting; }
        }

        private bool showRightPanelSetting = true;

        public bool ShowRightPanelSetting
        {
            get { return showRightPanelSetting; }
        }

        private bool accordionMode;

        public bool AccordionMode
        {
            get { return accordionMode; }
        }

        private int showNumberModuleSetting;
        public int ShowNumberModuleSetting
        {
            get { return showNumberModuleSetting; }
        }

        private bool showImageInViewPostSetting = false;
        public bool ShowImageInViewPostSetting
        {
            get { return showImageInViewPostSetting; }
        }
        private bool useClassOrther = false;
        public bool UseClassOrther
        {
            get { return useClassOrther; }
        }
        private bool showDescriptionDisplay = true;
        public bool ShowDescriptionDisplay
        {
            get { return showDescriptionDisplay; }
        }

        private bool showIsHot = true;
        public bool ShowIsHot
        {
            get { return showIsHot; }
        }
        private string articleModuleSelectorSetting = string.Empty;
        public string ArticleModuleSelectorSetting
        {
            get { return articleModuleSelectorSetting; }
        }

        private string goToTop = "Go top";
        public string GoToTop
        {
            get { return goToTop; }
        }

        private bool socialInMainArticle;
        public bool SocialInMainArticle
        {
            get { return socialInMainArticle; }
        }

        private int otherArticlesDetailPageSizeSetting = 10;
        public int OtherArticlesDetailPageSizeSetting
        {
            get { return otherArticlesDetailPageSizeSetting; }
        }

        private bool syncPostPublished;
        public bool SyncPostPublished { get { return syncPostPublished; } }

        private bool showEditInPost;
        public bool ShowEditInPost { get { return showEditInPost; } }

        private bool useReverse;
        public bool UseReverse { get { return useReverse; } }

        private int numberCategoriesLimit = 5;
        public int NumberCategoriesLimit { get { return numberCategoriesLimit; } }


        private int numberArticleLimit = 5;
        public int NumberArticleLimit { get { return numberArticleLimit; } }

        private string emailReceiveNewPost = string.Empty;
        private List<string> emailNewPost;
        public List<string> EmailNewPost { get { return emailNewPost; } }

        private bool useOverrideUrl;
        public bool UseOverrideUrl { get { return useOverrideUrl; } }

        private bool showEventInDetailSetting;
        public bool ShowEventInDetailSetting { get { return showEventInDetailSetting; } }

        private bool showTagInDetailSetting;
        public bool ShowTagInDetailSetting { get { return showTagInDetailSetting; } }
        private bool showImageMorePost = false;
        public bool ShowImageMorePost
        {
            get { return showImageMorePost; }
        }

        string moduleRightCssCustome = "";
        public string ModuleRightCssCustome
        {
            get { return moduleRightCssCustome; }
        }
        string moduleHotCssCustome = "";
        public string ModuleHotCssCustome
        {
            get { return moduleHotCssCustome; }
        }

        string moduleDisplayCssCustome = "";
        public string ModuleDisplayCssCustome
        {
            get { return moduleDisplayCssCustome; }
        }
        //private bool showTagInPostListSetting;
        //public bool ShowTagInPostListSetting { get { return showTagInPostListSetting; } }
        private bool showImgArticleHot = false;
        public bool ShowImgArticleHot
        {
            get { return showImgArticleHot; }
        }
        private int tabSelectorSetting = 1;
        public int TabSelectorSetting
        {
            get { return tabSelectorSetting; }
        }

        #region Configuration module HomeList
        //Chọn 1 danh mục cho tin bài
        private string homeListSelectorOneSetting = string.Empty;
        public string HomeListSelectorOneSetting
        {
            get { return homeListSelectorOneSetting; }
        }
        //Chọn nhiều danh mục cho tin bài
        private string homeListSelectorMutipleSetting = string.Empty;
        public string HomeListSelectorMutipleSetting
        {
            get { return homeListSelectorMutipleSetting; }
        }

        #endregion
    }
}
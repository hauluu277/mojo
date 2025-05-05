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
    public class EventConfiguration
    {
        public EventConfiguration()
        { }

        public EventConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("EventCategoryConfigSetting"))
                eventCategoryConfig = settings["EventCategoryConfigSetting"].ToString();

            useExcerpt = WebUtils.ParseBoolFromHashtable(settings, "EventUseExcerptSetting", useExcerpt);

            useHotEvent = WebUtils.ParseBoolFromHashtable(settings, "UseHotEventSetting", useHotEvent);

            useMostViewEvent = WebUtils.ParseBoolFromHashtable(settings, "UseMostViewEventSetting", useMostViewEvent);

            useTabEvent = WebUtils.ParseBoolFromHashtable(settings, "UseTabEventSetting", useTabEvent);

            useMultiTabEvent = WebUtils.ParseBoolFromHashtable(settings, "UseMultiTabEventSetting", useMultiTabEvent);

            numberCategoriesLimit = WebUtils.ParseInt32FromHashtable(settings, "NumberCategoriesLimitSetting", numberCategoriesLimit);

            useSlideEvent = WebUtils.ParseBoolFromHashtable(settings, "UseSlideEventSetting", useSlideEvent);

            showEventHotDisplay = WebUtils.ParseInt32FromHashtable(settings, "ShowEventHotDisplaySetting", showEventHotDisplay);

            showEventHotTab = WebUtils.ParseInt32FromHashtable(settings, "ShowEventHotTabSetting", showEventHotTab);

            showEventDisplay = WebUtils.ParseInt32FromHashtable(settings, "ShowEventDisplaySetting", showEventDisplay);

            showEventDetailDisplay = WebUtils.ParseInt32FromHashtable(settings, "ShowEventDetailDisplaySetting", showEventDetailDisplay);
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

            if (settings.Contains("EventLoaderSelectorSetting"))
            {
                eventCategoriesSelected = settings["EventLoaderSelectorSetting"].ToString();
            }

            if (settings.Contains("GoTopSetting"))
            {
                goToTop = settings["GoTopSetting"].ToString();
            }

            if (settings.Contains("EventModuleSelectorSetting"))
            {
                eventModuleSelectorSetting = settings["EventModuleSelectorSetting"].ToString();
            }

            if (settings.Contains("EmailNewPostSetting"))
            {
                emailReceiveNewPost = settings["EmailNewPostSetting"].ToString().Trim();
                emailNewPost = emailReceiveNewPost.SplitOnChar('|');
            }

            useExcerptInFeed = WebUtils.ParseBoolFromHashtable(settings, "UseExcerptInFeedSetting", useExcerptInFeed);

            socialInMainEvent = WebUtils.ParseBoolFromHashtable(settings, "SocialInMainEventSetting", socialInMainEvent);

            titleOnly = WebUtils.ParseBoolFromHashtable(settings, "EventShowTitleOnlySetting", titleOnly);

            showPager = WebUtils.ParseBoolFromHashtable(settings, "EventShowPagerInListSetting", showPager);

            googleMapIncludeWithExcerpt = WebUtils.ParseBoolFromHashtable(settings, "GoogleMapIncludeWithExcerptSetting", googleMapIncludeWithExcerpt);

            enableContentRating = WebUtils.ParseBoolFromHashtable(settings, "EnableContentRatingSetting", enableContentRating);

            enableRatingComments = WebUtils.ParseBoolFromHashtable(settings, "EnableRatingCommentsSetting", enableRatingComments);

            accordionMode = WebUtils.ParseBoolFromHashtable(settings, "AccordionModeSetting", accordionMode);

            excerptLength = WebUtils.ParseInt32FromHashtable(settings, "EventExcerptLengthSetting", excerptLength);

            if (settings.Contains("EventExcerptSuffixSetting"))
            {
                excerptSuffix = settings["EventExcerptSuffixSetting"].ToString();
            }

            if (settings.Contains("ShowEventMoreLinkText"))
            {
                showMoreLinkText = bool.Parse(settings["ShowEventMoreLinkText"].ToString());
            }

            if (settings.Contains("EventMoreLinkText"))
            {
                moreLinkText = settings["EventMoreLinkText"].ToString();
            }

            if (settings.Contains("EventAuthorSetting"))
            {
                eventAuthor = settings["EventAuthorSetting"].ToString();
            }

            if (settings.Contains("CustomCssClassSetting"))
            {
                customCssClassSetting = settings["CustomCssClassSetting"].ToString();
            }
            if (settings.Contains("CustomViewListCssClassSetting"))
            {
                viewListCssClass = settings["CustomViewListCssClassSetting"].ToString();
            }

            if (settings.Contains("EventDateTimeFormat"))
            {
                string format = settings["EventDateTimeFormat"].ToString().Trim();
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

            otherEventsDetailPageSizeSetting = WebUtils.ParseInt32FromHashtable(settings, "OtherEventsDetailPageSizeSetting", otherEventsDetailPageSizeSetting);

            showBottomPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowBottomPanelSetting", showBottomPanelSetting);

            showTopPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowTopPanelSetting", showTopPanelSetting);

            showLeftPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowLeftPanelSetting", showLeftPanelSetting);

            showRightPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowRightPanelSetting", showRightPanelSetting);

            useTagCloudForCategories = WebUtils.ParseBoolFromHashtable(settings, "EventUseTagCloudForCategoriesSetting", useTagCloudForCategories);

            showCalendar = WebUtils.ParseBoolFromHashtable(settings, "EventShowCalendarSetting", showCalendar);

            showCategories = WebUtils.ParseBoolFromHashtable(settings, "EventShowCategoriesSetting", showCategories);

            showArchives = WebUtils.ParseBoolFromHashtable(settings, "EventShowArchiveSetting", showArchives);

            showStatistics = WebUtils.ParseBoolFromHashtable(settings, "EventShowStatisticsSetting", showStatistics);

            showFeedLinks = WebUtils.ParseBoolFromHashtable(settings, "EventShowFeedLinksSetting", showFeedLinks);

            showAddFeedLinks = WebUtils.ParseBoolFromHashtable(settings, "EventShowAddFeedLinksSetting", showAddFeedLinks);

            navigationOnRight = WebUtils.ParseBoolFromHashtable(settings, "EventNavigationOnRightSetting", navigationOnRight);

            allowComments = WebUtils.ParseBoolFromHashtable(settings, "EventAllowComments", allowComments);

            useLinkForHeading = WebUtils.ParseBoolFromHashtable(settings, "EventUseLinkForHeading", useLinkForHeading);

            showPostAuthor = WebUtils.ParseBoolFromHashtable(settings, "ShowPostAuthorSetting", showPostAuthor);

            showAuthorOccupation = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorOccupationSetting", showAuthorOccupation);

            showAuthorYahoo = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorYahooSetting", showAuthorYahoo);

            showAuthorWebsiteUrl = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorWebsiteUrlSetting", showAuthorWebsiteUrl);

            showAuthorSignature = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorSignatureSetting", showAuthorSignature);

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

            pageSize = WebUtils.ParseInt32FromHashtable(settings, "EventEntriesToShowSetting", pageSize);

            if (settings.Contains("OdiogoFeedIDSetting"))
            {
                odiogoFeedId = settings["OdiogoFeedIDSetting"].ToString();
            }

            if (settings.Contains("OdiogoPodcastUrlSetting"))
                odiogoPodcastUrl = settings["OdiogoPodcastUrlSetting"].ToString();

            if (settings.Contains("EventFeedburnerFeedUrl"))
            {
                feedburnerFeedUrl = settings["EventFeedburnerFeedUrl"].ToString().Trim();
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

            if (settings.Contains("EventCopyrightSetting"))
            {
                copyright = settings["EventCopyrightSetting"].ToString();
            }

            enableContentVersioning = WebUtils.ParseBoolFromHashtable(settings, "EventEnableVersioningSetting", enableContentVersioning);

            defaultCommentDaysAllowed = WebUtils.ParseInt32FromHashtable(settings, "EventCommentForDaysDefault", defaultCommentDaysAllowed);

            if (settings.Contains("EventEditorHeightSetting"))
            {
                editorHeight = Unit.Parse(settings["EventEditorHeightSetting"].ToString());

            }

            useCaptcha = WebUtils.ParseBoolFromHashtable(settings, "EventUseCommentSpamBlocker", useCaptcha);

            requireAuthenticationForComments = WebUtils.ParseBoolFromHashtable(settings, "RequireAuthenticationForComments", requireAuthenticationForComments);

            notifyOnComment = WebUtils.ParseBoolFromHashtable(settings, "ContentNotifyOnComment", notifyOnComment);

            if (settings.Contains("EventAuthorEmailSetting"))
            {
                notifyEmail = settings["EventAuthorEmailSetting"].ToString();
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

            showOtherEvents = WebUtils.ParseBoolFromHashtable(settings, "OtherEventsShowSetting", showOtherEvents);

            otherEventsUsePaging = WebUtils.ParseBoolFromHashtable(settings, "OtherEventsPagingSetting", otherEventsUsePaging);

            otherEventsShowMoreLinkSetting = WebUtils.ParseBoolFromHashtable(settings, "OtherEventsShowMoreLinkSetting", otherEventsShowMoreLinkSetting);

            if (settings.Contains("OtherEventsMoreLinkSetting"))
            {
                otherEventsMoreLinkSetting = settings["OtherEventsMoreLinkSetting"].ToString();
            }

            if (settings.Contains("OtherEventsMoreLinkTextSetting"))
            {
                otherEventsMoreLinkTextSetting = settings["OtherEventsMoreLinkTextSetting"].ToString();
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

            otherEventsPageSizeSetting = WebUtils.ParseInt32FromHashtable(settings, "OtherEventsPageSizeSetting", otherEventsPageSizeSetting);

            otherEventsPagingSetting = WebUtils.ParseBoolFromHashtable(settings, "OtherEventsPagingSetting", otherEventsPagingSetting);

            useAttachmentSetting = WebUtils.ParseBoolFromHashtable(settings, "UseAttachmentSetting", useAttachmentSetting);

            useAttachmentDownloadIconSetting = WebUtils.ParseBoolFromHashtable(settings, "UseAttachmentDownloadIconSetting", useAttachmentDownloadIconSetting);

            showAttachmentLabelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowAttachmentLabelSetting", showAttachmentLabelSetting);

            useOverrideUrl = WebUtils.ParseBoolFromHashtable(settings, "UseOverrideUrlSetting", useOverrideUrl);

            if (settings.Contains("OtherEventSetting"))
            {
                otherEvent = settings["OtherEventSetting"].ToString();
            }

            showNumberModuleSetting = WebUtils.ParseInt32FromHashtable(settings, "ShowNumberModuleSetting", showNumberModuleSetting);
            showNumberEventHot = WebUtils.ParseInt32FromHashtable(settings, "ShowNumberEventHotSetting", showNumberEventHot);

            useReverse = WebUtils.ParseBoolFromHashtable(settings, "UseReverseSetting", useReverse);

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
            if (settings.Contains("TitleSetting"))
            {
                title = settings["TitleSetting"].ToString();
            }
            if (settings.Contains("EventUrlSetting"))
            {
                eventUrl = settings["EventUrlSetting"].ToString();
            }
        }
        private string eventUrl = string.Empty;
        public string EventUrl
        {
            get { return eventUrl; }
        }
        private string title = string.Empty;
        public string Title
        {
            get { return title; }
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

        private bool useAttachmentSetting;
        public bool UseAttachmentSetting
        {
            get { return useAttachmentSetting; }
        }
        private int showNumberEventHot = 5;
        public int ShowNumberEventHot
        {
            get { return showNumberEventHot; }
        }
        private bool showAttachmentLabelSetting;
        public bool ShowAttachmentLabelSetting { get { return showAttachmentLabelSetting; } }

        private bool useAttachmentDownloadIconSetting;
        public bool UseAttachmentDownloadIconSetting { get { return useAttachmentDownloadIconSetting; } }

        private bool useHotEvent;
        public bool UseHotEvent { get { return useHotEvent; } }

        private bool useMostViewEvent;
        public bool UseMostViewEvent { get { return useMostViewEvent; } }

        private bool useTabEvent;
        public bool UseTabEvent { get { return useTabEvent; } }

        private bool useMultiTabEvent;
        public bool UseMultiTabEvent { get { return useMultiTabEvent; } }

        private bool useSlideEvent;
        public bool UseSlideEvent { get { return useSlideEvent; } }

        private int slideTimeTransition = 5000;
        public int SlideTimeTransition { get { return slideTimeTransition; } }

        private int mainSlideExcerpt = 500;
        public int MainSlideExcerpt { get { return mainSlideExcerpt; } }

        private int mainSlideTitleMaxChar = 100;
        public int MainSlideTitleMaxChar { get { return mainSlideTitleMaxChar; } }

        private string otherEvent = string.Empty;
        public string OtherEvent { get { return otherEvent; } }

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

        private bool enableContentVersioning;
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
        private int showEventHotDisplay = 7;
        public int ShowEventHotDisplay
        {
            get { return showEventHotDisplay; }
        }

        private int showEventHotTab = 3;
        public int ShowEventHotTab
        {
            get { return showEventHotTab; }
        }
        private int showEventDisplay = 3;
        public int ShowEventDisplay
        {
            get { return showEventDisplay; }
        }
        private int showEventDetailDisplay = 5;
        public int ShowEventDetailDisplay
        {
            get { return showEventDetailDisplay; }
        }
        private int showNewsDisplay = 1;
        public int ShowNewsDisplay
        {
            get { return showNewsDisplay; }
        }
        private int pageSize = 10;

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

        private bool useLinkForHeading = true;

        public bool UseLinkForHeading
        {
            get { return useLinkForHeading; }
        }

        private bool allowComments;

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

        private string dateTimeFormat = string.Empty;

        public string DateTimeFormat
        {
            get { return dateTimeFormat; }
        }

        private string eventCategoryConfig;
        public string EventCategoryConfig
        {
            get { return eventCategoryConfig; }
        }

        private bool useExcerpt;

        public bool UseExcerpt
        {
            get { return useExcerpt; }
        }

        private string eventCategoriesSelected = string.Empty;

        public string EventCategoriesSelected
        {
            get { return eventCategoriesSelected; }
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

        private string eventAuthor = string.Empty;

        public string EventAuthor
        {
            get { return eventAuthor; }
        }

        private string customCssClassSetting = string.Empty;

        public string CustomCssClassSetting
        {
            get { return customCssClassSetting; }
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

        private bool showOtherEvents = true;

        public bool ShowOtherEvents
        {
            get { return showOtherEvents; }
        }

        private bool otherEventsUsePaging;

        public bool OtherEventsUsePaging
        {
            get { return otherEventsUsePaging; }
        }

        private bool otherEventsShowMoreLinkSetting;

        public bool OtherEventsShowMoreLinkSetting
        {
            get { return otherEventsShowMoreLinkSetting; }
        }

        private string otherEventsMoreLinkSetting = string.Empty;

        public string OtherEventsMoreLinkSetting
        {
            get { return otherEventsMoreLinkSetting; }
        }

        private string otherEventsMoreLinkTextSetting = string.Empty;
        public string OtherEventsMoreLinkTextSetting
        {
            get { return otherEventsMoreLinkTextSetting; }
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

        private bool otherEventsPagingSetting = true;

        public bool OtherEventsPagingSetting
        {
            get { return otherEventsPagingSetting; }
        }

        private int otherEventsPageSizeSetting = 5;

        public int OtherEventsPageSizeSetting
        {
            get { return otherEventsPageSizeSetting; }
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

        private bool showImageInViewPostSetting = true;
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
        private string eventModuleSelectorSetting = string.Empty;
        public string EventModuleSelectorSetting
        {
            get { return eventModuleSelectorSetting; }
        }

        private string goToTop = "Go top";
        public string GoToTop
        {
            get { return goToTop; }
        }

        private bool socialInMainEvent;
        public bool SocialInMainEvent
        {
            get { return socialInMainEvent; }
        }

        private int otherEventsDetailPageSizeSetting = 10;
        public int OtherEventsDetailPageSizeSetting
        {
            get { return otherEventsDetailPageSizeSetting; }
        }

        private bool syncPostPublished;
        public bool SyncPostPublished { get { return syncPostPublished; } }

        private bool showEditInPost;
        public bool ShowEditInPost { get { return showEditInPost; } }

        private bool useReverse;
        public bool UseReverse { get { return useReverse; } }

        private int numberCategoriesLimit = 5;
        public int NumberCategoriesLimit { get { return numberCategoriesLimit; } }

        private string emailReceiveNewPost = string.Empty;
        private List<string> emailNewPost;
        public List<string> EmailNewPost { get { return emailNewPost; } }

        private bool useOverrideUrl;
        public bool UseOverrideUrl { get { return useOverrideUrl; } }
    }
}
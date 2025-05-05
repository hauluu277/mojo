<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostList.ascx.cs" Inherits="ArticleFeature.UI.PostListLoader" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="blogwrapper">
    <asp:Panel ID="divblog" runat="server" CssClass="blogcenter-rightnav" SkinID="plain">
        <asp:Panel ID="pnlScrollable" runat="server" CssClass="itemwrapper">
            <div class="items">
                <asp:Repeater ID="rptBlogs" runat="server" SkinID="Blog" EnableViewState="False">
                    <ItemTemplate>
                        <asp:Panel ID="pnlBlogItem" runat="server" CssClass="blogitem">
                            <h3 class="blogtitle">
                                <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                    ToolTip='<%# Eval("Title") %>' Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                    Visible='<%# Config.UseLinkForHeading %>' NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                </asp:HyperLink><asp:Literal ID="litTitle" runat="server" Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                    Visible='<%# !Config.UseLinkForHeading %>' />&nbsp;
                                <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>"
                                    ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                                    Visible="<%# IsEditable && !Config.ShowEditInPost %>" CssClass="ModuleEditLink" /></h3>
                            <div class="blogdate">
                                <span class="blogauthor">
                                    <%# FormatPostAuthor(DataBinder.Eval(Container.DataItem, "UserGuid").ToString())%></span>
                                <span class="bdate">
                                    <%# FormatBlogDate(Convert.ToDateTime(Eval("StartDate"))) %></span>
                            </div>
                            <asp:Panel ID="pnlPost" runat="server" Visible='<%# !Config.TitleOnly %>' CssClass="post">
                                <portal:mojoRating runat="server" ID="Rating" Enabled='<%# EnableContentRating %>'
                                    ContentGuid='<%# new Guid(Eval("ArticleGuid").ToString()) %>' AllowFeedback='false' />
                                <mp:OdiogoItem ID="od1" runat="server" OdiogoFeedId='<%# Config.OdiogoFeedId %>'
                                    ItemId='<%# DataBinder.Eval(Container.DataItem,"ItemID") %>' ItemTitle='<%# Eval("Title") %>' />
                                <div class="blogtext">
                                    <div class='<%# "hideme mojo-dialog " + ModuleId + Eval("ItemID") %>' title='<%# Eval("Title") %>'>
                                        <asp:Image ID="image2" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                            Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' />
                                    </div>
                                    <div class="image-wrapper">
                                        <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                            <asp:Image ID="image1" runat="server" ImageUrl='<%# ArticleUtils.FormatImageArticle(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                                Visible='<%# Config.ShowImage %>' CssClass='<%# "img" + ModuleId + Eval("ItemID") %>'
                                                ToolTip='<%# DataBinder.Eval(Container.DataItem,"Title") %>' />
                                        </a>
                                    </div>
                                    <asp:Literal ID="ltrScriptDialog" runat="server" Visible='<%# Config.UseImageDialog %>'
                                        Text='<%# ArticleUtils.FormatDialogScript(ModuleId.ToString(), Eval("ItemID").ToString()) %>' />
                                    <div class="body">
                                        <asp:HyperLink ID="hplEditPost" runat="server" EnableViewState="false" Text="<%# EditLinkText %>"
                                            ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                                            Visible="<%# IsEditable && Config.ShowEditInPost %>" CssClass="ModuleEditLink" />
                                        <%--<%# ArticleUtils.FormatBlogEntry(Eval("Description").ToString(), Eval("Abstract").ToString(), Config)%>--%>
                                    </div>
                                    <%# ArticleUtils.FormatReadMoreLink(Config, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), SiteRoot, PageId, ModuleId)%>
                                </div>
                                <goog:LocationMap ID="gmap" runat="server" Visible='<%# ((Eval("Location").ToString().Length > 0)&&(ShowGoogleMap)) %>'
                                    Location='<%# Eval("Location") %>' GMapApiKey='<%# GmapApiKey %>' EnableMapType='<%# Config.GoogleMapEnableMapType %>'
                                    EnableZoom='<%# Config.GoogleMapEnableZoom %>' ShowInfoWindow='<%# Config.GoogleMapShowInfoWindow %>'
                                    EnableLocalSearch='<%# Config.GoogleMapEnableLocalSearch %>' EnableDrivingDirections='<%# Config.GoogleMapEnableDirections %>'
                                    GmapType='<%# Config.GoogleMapType %>' ZoomLevel='<%# Config.GoogleMapInitialZoom %>'
                                    MapHeight='<%# Config.GoogleMapHeight %>' MapWidth='<%# Config.GoogleMapWidth %>'>
                                </goog:LocationMap>
                                
                            </asp:Panel>
                            <div class="bloghit">
                                    <portal:TweetThisLink ID="tt1" runat="server" Visible='<%# Config.ShowTweetThisLink && Config.SocialInMainArticle %>'
                                        UrlToTweet='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                        TitleToTweet='<%# DataBinder.Eval(Container.DataItem,"Title") %>' />
                                    <portal:FacebookLikeButton ID="fbl1" runat="server" Visible='<%# Config.UseFacebookLikeButton && Config.SocialInMainArticle %>'
                                        UrlToLike='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                        ColorScheme='<%# Config.FacebookLikeButtonTheme %>' ShowFaces='<%# Config.FacebookLikeButtonShowFaces %>'
                                        WidthInPixels='<%# Config.FacebookLikeButtonWidth %>' HeightInPixels='<%# Config.FacebookLikeButtonHeight %>' />
                                </div>
                                <asp:Panel ID="pnlComment" runat="server" CssClass="comment" Visible='<%# ShowCommentCounts %>'>
                                        <%# DataBinder.Eval(Container.DataItem,"CommentCount") + " " + FeedBackLabel%>
                                    </asp:Panel>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlBlogPager" runat="server" CssClass="blogpager">
            <portal:mojoCutePager ID="pgr" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlOthersArticle" runat="server" CssClass="otherpanel">
            <asp:Panel ID="pnlOtherHeader" runat="server" CssClass="otherheader">
                <asp:Label ID="lblOtherHeader" runat="server" />
            </asp:Panel>
            <asp:UpdatePanel ID="upOthersArticle" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <mp:mojoGridView ID="gvOthersArticle" runat="server" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false"
                        CssClass="otheritems">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class='<%# "item-wrapper tooltipable" + PureModuleId %>'>
                                        <asp:HyperLink ID="hplOtherTitle" runat="server" title="" ToolTip='<%# ArticleUtils.FormatTooltip(Eval("Title").ToString(), Eval("Description").ToString(), Config) %>'
                                            CssClass="link" Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInMainOthers) %>'
                                            NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'/>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </mp:mojoGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="pnlMoreLink" runat="server" CssClass="morelinksettings">
            <div class="more-link-l">
            </div>
            <div class="more-link-m">
                <asp:HyperLink ID="hplMoreLink" runat="server" />
            </div>
            <div class="more-link-r">
            </div>
        </asp:Panel>
        <div class="cleared">
        </div>
    </asp:Panel>
    <div class="blogcopyright">
        <asp:Label ID="lblCopyright" runat="server" />
    </div>
</div>

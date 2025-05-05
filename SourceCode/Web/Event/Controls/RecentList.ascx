<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RecentList.ascx.cs" Inherits="EventFeature.UI.RecentList" %>
<%@ Import Namespace="EventFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="blogwrapper">
    <asp:Panel ID="divblog" runat="server" CssClass="Eventcenter-rightnav" SkinID="plain">
        <asp:Panel ID="pnlScrollable" runat="server">
            <div class="list-Event">
                <asp:Repeater ID="rptRecentEvents" runat="server" SkinID="Blog" EnableViewState="False">
                    <ItemTemplate>
                        <asp:Panel ID="pnlBlogItem" runat="server" CssClass="list-Event__blogitem">
                            <asp:Panel ID="pnlImage" runat="server" Visible='<%# Config.ShowImage %>' CssClass="post">
                                <asp:Panel ID="showImage" Visible='<%#ShowImage(Eval("ImageUrl").ToString())%>' runat="server">
                                    <div class='img-Event' title='<%# Eval("Title") %>'>
                                        <asp:Image ID="image2" Width="100%" Height="100%" runat="server" ImageUrl='<%# EventUtils.FormatImageDialog(ConfigurationManager.AppSettings["EventImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                            Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' />
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                            <div class="<%# Config.ShowImage ? "info-Event" : "info-Event-full" %>">
                                <h3 class="Event-title">
                                    <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                        ToolTip='<%# Eval("Title") %>' Text='<%# EventUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                        Visible='<%# Config.UseLinkForHeading %>' NavigateUrl='<%# EventUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                    </asp:HyperLink>
                                    <asp:Literal ID="litTitle" runat="server" Text='<%# EventUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                        Visible='<%# !Config.UseLinkForHeading %>' />
                                    <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>"
                                        ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                                        Visible="<%# IsEditable && Config.ShowEditInPost %>" CssClass="ModuleEditLink" />
                                </h3>
                                <asp:Panel ID="pnlPost" runat="server" Visible='<%# !Config.TitleOnly %>' CssClass="post">
                                    <div class="Event-date">
                                        <span class="Event-author"><b><i class="fa fa-clock-o"></i>
                                            <%# FormatPostAuthor(DataBinder.Eval(Container.DataItem, "CreatedByUser").ToString())%></b></span>
                                        <span class="bdate">
                                          <%# FormatEventDate(Convert.ToDateTime(Eval("StartDate"))) %> - </span>
                                        <span class="bdate">
                                            <%# FormatEventDate(Convert.ToDateTime(Eval("EndDate"))) %> | </span>
                                        <span class="location">
                                          <i class="fa fa-map-marker"></i> <%#Eval("Location") %>
                                        </span>
                                    </div>

                                   <div class="sapo">
                                        <%# EventUtils.FormatBlogEntry(Eval("Summary").ToString(), string.Empty, Config)%>
                                   </div>

                                    <%# EventUtils.FormatReadMoreLink(Config, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), SiteRoot, PageId, ModuleId)%>
                                    <div class="bloghit">
                                        <portal:TweetThisLink ID="tt1" runat="server" Visible='<%# Config.ShowTweetThisLink && Config.SocialInMainEvent %>'
                                            UrlToTweet='<%# EventUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                            TitleToTweet='<%# DataBinder.Eval(Container.DataItem,"Title") %>' />
                                        <portal:FacebookLikeButton ID="fbl1" runat="server" Visible='<%# Config.UseFacebookLikeButton && Config.SocialInMainEvent %>'
                                            UrlToLike='<%# EventUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                            ColorScheme='<%# Config.FacebookLikeButtonTheme %>' ShowFaces='<%# Config.FacebookLikeButtonShowFaces %>'
                                            WidthInPixels='<%# Config.FacebookLikeButtonWidth %>' HeightInPixels='<%# Config.FacebookLikeButtonHeight %>' />
                                    </div>
                                    <div class="Event-commentlink">
                                        <asp:HyperLink ID="Hyperlink2" runat="server" EnableViewState="false" Text='<%# FeedBackLabel + "(" + DataBinder.Eval(Container.DataItem,"CommentCount") + ")" %>'
                                            Visible='<%# Config.AllowComments && ShowCommentCounts %>' NavigateUrl='<%# EventUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                            CssClass="Event-commentlink"></asp:HyperLink>
                                        <asp:HyperLink ID="Hyperlink1" runat="server" EnableViewState="false" Text='<%# FeedBackLabel %>'
                                            Visible='<%# Config.AllowComments && !ShowCommentCounts %>' NavigateUrl='<%# EventUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                            CssClass="Event-commentlink"></asp:HyperLink>
                                    </div>
                                </asp:Panel>
                            </div>
                           
                        </asp:Panel>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlEventPager" runat="server" CssClass="blogpager">
            <portal:mojoCutePager ID="pgr" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlOthersEvent" runat="server" CssClass="otherpanel">
            <div class="otherheader">
                <asp:Label ID="lblOtherHeader" runat="server" />
            </div>
            <asp:UpdatePanel ID="upOthersEvent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <mp:mojoGridView ID="gvOthersEvent" runat="server" AutoGenerateColumns="false"
                        CssClass="otheritems">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class='<%# "item-wrapper tooltipable" + ModuleId %>'>
                                        <asp:HyperLink ID="hplOtherTitle" runat="server" title="" ToolTip='<%# EventUtils.FormatTooltip(Eval("Title").ToString(), Eval("Description").ToString(), Config) %>'
                                            CssClass="link" Text='<%# Eval("Title") %>' NavigateUrl='<%# EventUtils.FormatBlogTitleUrl(siteSettings.SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' />
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
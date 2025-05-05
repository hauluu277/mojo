<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RecentList.ascx.cs" Inherits="ArticleFeature.UI.RecentList" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Register TagPrefix="article" TagName="FeedLinks" Src="~/Article/Controls/FeedLinks.ascx" %>
<style type="text/css">
    .left-menu {
        width: 255px;
        float: left;
        border: 1px solid white;
        margin-right: 20px;
        margin-bottom: 30px;
    }

        .left-menu h3 {
            background: #0078d7;
            color: white;
            margin: 0;
            padding: 8px;
            font-size: 16px;
            text-align: center;
            line-height: 25px;
            width: 100%;
            float: left;
            border-bottom: 1px solid white;
        }

            .left-menu h3 a, .left-menu h3 a:hover {
                color: white !important;
            }

        .left-menu ul {
            list-style: none;
        }

            .left-menu ul.article_list {
                list-style: none;
                width: 100%;
                float: left;
            }

                .left-menu ul.article_list li a {
                    color: black;
                }

                .left-menu ul.article_list li {
                    width: 100%;
                    float: left;
                    border-bottom: 1px solid #ddd;
                    font-size: 14px;
                    padding: 10px;
                }

    .content-article {
        width: calc(100% - 275px);
        float: left;
    }

    @media all and (max-width: 480px) and (min-width: 320px) {
        .left-menu {
            width: 100%;
        }

        .content-article {
            width: 100%;
        }
    }

    @media all and (max-width: 768px) and (min-width: 480px) {
        .left-menu {
            display: none;
        }

        .content-article {
            width: 100%;
        }
    }

    .article-thongbao:first-child {
        padding-top: 0;
    }
</style>
<div class="blogwrapper">
    <asp:Panel ID="divNav" runat="server" CssClass="blognavright" SkinID="plain">
        <article:FeedLinks ID="Feeds" runat="server" />
    </asp:Panel>
    <asp:Panel ID="divblog" runat="server" CssClass="articlecenter-rightnav" SkinID="plain">
        <asp:Panel ID="pnlScrollable" runat="server">

            <asp:Panel runat="server" ID="pnlContentArticle">
                <asp:Panel ID="pnlDisplayType_ShowImage" runat="server">
                    <asp:Repeater ID="rptArticleType_ShowImage" runat="server">
                        <ItemTemplate>
                            <div class="article-thongbao">
                                <div runat="server" visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' class='img-article' title='<%# Eval("Title") %>' tooltip='<%# Eval("Title") %>'>
                                    <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <asp:Image ID="image3" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                            Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' />
                                    </a>
                                </div>
                                <div class="<%# Config.ShowImage ? "info-article" : "info-article-full" %>">
                                    <h3 class="article-title">
                                        <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                            ToolTip='<%# Eval("Title") %>' Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                            Visible='<%# Config.UseLinkForHeading %>' NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>"
                                            ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                                            Visible="<%# IsEditable && Config.ShowEditInPost %>" CssClass="ModuleEditLink" />
                                    </h3>
                                    <asp:Panel ID="pnlPost" runat="server" Visible='<%# !Config.TitleOnly %>' CssClass="post">
                                        <div class="article-date">
                                            <span class="article-author"><b></b></span>
                                            <div class="detail-muted">
                                                <ul class="list-unstyled list-inline">
                                                    <li>
                                                        <img src="/Data/Images/special/post-clock.png" />
                                                        <%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %>
                                                    </li>

                                                    <li>
                                                        <em class="fa fa-eye">&nbsp;</em>
                                                        Đã xem: <%#Eval("HitCount") %>
                                                    </li>
                                                    <li runat="server" visible='<%# Config.ShowAuthorSignature %>'>
                                                        <em class="fa fa-user">&nbsp;</em>
                                                        <%# DataBinder.Eval(Container.DataItem, "CreatedByUser").ToString()%>
                                                    </li>
                                                    <%--  <li><em class="fa fa-comment-o">&nbsp;</em> Phản hồi: 0</li>--%>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="summary-article">
                                            <%# ArticleUtils.FormatBlogEntry(Eval("Summary").ToString(), string.Empty, Config)%>
                                        </div>
                                        <%# ArticleUtils.FormatReadMoreLink(Config, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), SiteRoot, PageId, ModuleId)%>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>


                <asp:Panel ID="pnlDisplayType_HideImage" runat="server">
                    <asp:Repeater ID="rptArticleType_HideImage" runat="server">
                        <ItemTemplate>
                            <div class="article-thongbao">
                                <div class="<%# Config.ShowImage ? "info-article" : "info-article-full" %>">
                                    <h3 class="article-title">
                                        <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                            ToolTip='<%# Eval("Title") %>' Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                            Visible='<%# Config.UseLinkForHeading %>' NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>"
                                            ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                                            Visible="<%# IsEditable && Config.ShowEditInPost %>" CssClass="ModuleEditLink" />
                                    </h3>
                                    <asp:Panel ID="pnlPost" runat="server" Visible='<%# !Config.TitleOnly %>' CssClass="post">
                                        <div class="article-date">
                                            <span class="article-author"><b></b></span>
                                            <div class="detail-muted">
                                                <ul class="list-unstyled list-inline">
                                                    <li>
                                                        <img src="/Data/Images/special/post-clock.png" />
                                                        <%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %>
                                                    </li>

                                                    <li>
                                                        <em class="fa fa-eye">&nbsp;</em>
                                                        Đã xem: <%#Eval("HitCount") %>
                                                    </li>
                                                    <li runat="server" visible='<%# Config.ShowAuthorSignature %>'>
                                                        <em class="fa fa-user">&nbsp;</em>
                                                        <%# DataBinder.Eval(Container.DataItem, "CreatedByUser").ToString()%>
                                                    </li>
                                                    <%--  <li><em class="fa fa-comment-o">&nbsp;</em> Phản hồi: 0</li>--%>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="summary-article">
                                            <%# ArticleUtils.FormatBlogEntry(Eval("Summary").ToString(), string.Empty, Config)%>
                                        </div>
                                        <%# ArticleUtils.FormatReadMoreLink(Config, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), SiteRoot, PageId, ModuleId)%>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnlArticlePager" runat="server" CssClass="blogpager">
                    <portal:mojoCutePager ID="pgr" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</div>

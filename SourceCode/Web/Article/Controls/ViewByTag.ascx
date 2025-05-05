<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ViewByTag.ascx.cs" Inherits="ArticleFeature.UI.ViewByTag" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="blogwrapper">
    <asp:Panel ID="divblog" runat="server" CssClass="articlecenter-rightnav" SkinID="plain">
        <asp:Panel ID="pnlScrollable" runat="server">
            <div class="list-article">
                <asp:Repeater ID="rptRecentArticles" runat="server" SkinID="Blog" EnableViewState="False">
                    <ItemTemplate>
                        <div class="article-thongbao">
                            <div runat="server"  class='img-article' title='<%# Eval("Title") %>' tooltip='<%# Eval("Title") %>'>
                                <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                    <asp:Image ID="image3" runat="server" visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                         CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' />
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
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlArticlePager" runat="server" CssClass="blogpager">
            <portal:mojoCutePager ID="pgr" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlOthersArticle" runat="server" CssClass="otherpanel">
            <div class="otherheader">
                <asp:Label ID="lblOtherHeader" runat="server" />
            </div>
            <asp:UpdatePanel ID="upOthersArticle" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <mp:mojoGridView ID="gvOthersArticle" runat="server" AutoGenerateColumns="false"
                        CssClass="otheritems">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class='<%# "item-wrapper tooltipable" + ModuleId %>'>
                                        <asp:HyperLink ID="hplOtherTitle" runat="server" title="" ToolTip='<%# ArticleUtils.FormatTooltip(Eval("Title").ToString(), Eval("Description").ToString(), Config) %>'
                                            CssClass="link" Text='<%# Eval("Title") %>' NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' />
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
<style>
    .img-article{
        width:auto;
        padding-right:0;
    }

    .list-article {
        width: 100%;
        float: left;
    }

    .info-article {
        width: 78%;
        float: left;
        padding-left: 2%;
    }

        .info-article a {
            color: #3B4E93;
        }

    .info-article-full {
        width: 100%;
        float: left;
    }

    .line_dot {
        border-bottom: 1px dotted #999;
        margin-top: 5px;
        margin-bottom: 5px;
        width: 100%;
    }

    .article-date {
        font-size: 12px;
        color: #CCC;
    }

    .article-author {
        font-weight: bold;
    }
</style>

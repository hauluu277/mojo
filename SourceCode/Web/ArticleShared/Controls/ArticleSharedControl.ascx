<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleSharedControl.ascx.cs" Inherits="ArticleFeature.UI.ArticleSharedControl" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="article-shared modulecontent level2-article-shared">
    <asp:Repeater ID="rptArticle" runat="server">
        <ItemTemplate>
            <div class="article-thongbao  level2-article-thongbao">
                <div runat="server" visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' class='img-article level2-img-article' title='<%# Eval("Title") %>' tooltip='<%# Eval("Title") %>'>
                    <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                        <asp:Image ID="image3" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                            Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' />
                    </a>
                </div>
                <div class="<%# Config.ShowImage ? "info-article" : "info-article-full" %>">
                    <asp:Panel ID="pnlSiteName" runat="server" Visible="<%#config.ShowSiteNameSetting %>">
                        <span style="color: red"><%#Eval("SiteName") %></span>
                    </asp:Panel>
                    <h3 class="article-title level2-article-title">
                        <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                            ToolTip='<%# Eval("Title") %>' Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                            NavigateUrl='<%# string.Format("{0}/Article/ViewDetail.aspx?pageid={1}&mid={2}&itemid={3}",SiteRoot ,PageId,ModuleId,Eval("ItemID")) %>'>
                        </asp:HyperLink>
                        <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>"
                            ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                            Visible="false" CssClass="ModuleEditLink" />
                    </h3>

                    <asp:Panel ID="pnlPost" runat="server" CssClass="post">
                        <div class="article-date level2-article-date">
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
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlArticlePager" runat="server" CssClass="blogpager level2-blogpager">
        <portal:mojoCutePager ID="pgr" runat="server" />
    </asp:Panel>
</div>

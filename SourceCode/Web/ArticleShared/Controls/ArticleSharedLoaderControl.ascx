<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleSharedLoaderControl.ascx.cs" Inherits="ArticleFeature.UI.ArticleSharedLoaderControl" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="article-shared han-article-shared">
    <i class="fa fa-plus-circle">  <asp:HyperLink ID="hplTieuDe" runat="server" Text="Chưa có thiết lập" CssClass="han-title-vip"></asp:HyperLink></i>
    <asp:Repeater ID="rptArticle" runat="server">
        <ItemTemplate>
            <div class="article-thongbao han-article-thongbao">
                <div runat="server" visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' class='img-article' title='<%# Eval("Title") %>' tooltip='<%# Eval("Title") %>'>
                    <a href="<%# string.Format("{0}/Article/ViewDetail.aspx?pageid={1}&mid={2}&itemid={3}",SiteRoot ,PageId,ModuleId,Eval("ItemID")) %>">
                        <asp:Image ID="image3" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                            Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' />
                    </a>
                </div>
                <div class="<%# Config.ShowImage ? "info-article" : "info-article-full" %>">
                    <h3 class="article-title-loader han-article-title">
                        <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                            ToolTip='<%# Eval("Title") %>' Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                            NavigateUrl='<%# string.Format("{0}/Article/ViewDetail.aspx?pageid={1}&mid={2}&itemid={3}",SiteRoot ,PageId,ModuleId,Eval("ItemID")) %>'>
                        </asp:HyperLink>
                        <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>"
                            ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                            Visible="<%# IsEditable %>" CssClass="ModuleEditLink hide" />
                    </h3>
   <%--                 <asp:Panel ID="pnlSiteName" runat="server" Visible="<%#config.ShowSiteNameSetting %>">
                        <span><%#Eval("SiteName") %></span>
                    </asp:Panel>--%>
                    <asp:Panel ID="pnlPost" runat="server" CssClass="post">
                        <div class="article-date han-article-date">
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
                        <%--<div class="summary-article han-summary-article">
                            <%# Eval("Summary")%>
                        </div>--%>
                    </asp:Panel>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

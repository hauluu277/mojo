<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CategoryPageControl.ascx.cs" Inherits="ArticleFeature.UI.CategoryPageControl" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<div class="blogwrapper">
    <asp:Panel ID="divblog" runat="server" CssClass="articlecenter-rightnav" SkinID="plain">
        <asp:Panel ID="pnlArticle" runat="server">
                <div class="list-article listTin">
                    <asp:Repeater ID="rptArticle" runat="server" SkinID="Blog" EnableViewState="False">
                        <ItemTemplate>
                            <asp:Panel ID="pnFirst" CssClass="listTin__First" runat="server" Visible='<%#Container.ItemIndex==0 %>'>
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:Panel ID="Panel2" runat="server" Visible='<%# Config.ShowImage %>' CssClass="post listTin__dep">
                                        <asp:Panel ID="pnlShowIMG" runat="server" Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>'>
                                            <div class='img-article2' title='<%# Eval("Title") %>'>
                                                <asp:Image ID="image1" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                                    Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' />
                                            </div>
                                        </asp:Panel>
                                        <div class="listTin_Noibat">
                                            <h3 class="article-title">
                                                <asp:HyperLink SkinID="BlogTitle" ID="HyperLink1" runat="server" EnableViewState="false"
                                                    ToolTip='<%# Eval("Title") %>' Text='<%# Eval("Title").ToString() %>'
                                                    Visible='<%# Config.UseLinkForHeading %>' NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                                </asp:HyperLink>
                                                <asp:HyperLink ID="HyperLink2" runat="server" EnableViewState="false"
                                                    ToolTip="<%# EditLinkTooltip %>" NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                                                    Visible="<%# IsEditable && Config.ShowEditInPost %>" CssClass="ModuleEditLink" />
                                            </h3>
                                            <asp:Panel ID="Panel3" runat="server" Visible='<%# !Config.TitleOnly %>' CssClass="post motaTeaser">
                                                <div class="article-date">
                                                    <span class="article-author"><b></b></span>
                                                    <div class="detail-muted">
                                                        <ul class="list-unstyled list-inline">
                                                            <li>
                                                                <em class="fa fa-clock-o">&nbsp;</em>
                                                                <%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %>
                                                            </li>
                                                            <li>
                                                                <em class="fa fa-eye">&nbsp;</em>
                                                                <%#Resources.ArticleResources.Views %>: <%#Eval("HitCount") %>
                                                            </li>
                                                            <li runat="server" visible='<%# Config.ShowAuthorSignature %>'>
                                                                <em class="fa fa-user">&nbsp;</em>
                                                                <%# DataBinder.Eval(Container.DataItem, "CreatedByUser").ToString()%>
                                                            </li>
                                                            <%--         <li><em class="fa fa-comment-o">&nbsp;</em> Phản hồi: 0</li>--%>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                    <div class="<%# Config.ShowImage ? "info-article2" : "info-article-full" %>">

                                        <%# Eval("Summary").ToString()%>
                                </asp:Panel>
                                </div>
                                <div class="clear">
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pn" runat="server" CssClass="listTin__other col-sm-6" Visible='<%#Container.ItemIndex >0 %>'>
                                <asp:Panel ID="pnlBlogItem" runat="server">
                                    <asp:Panel ID="pnlImage" runat="server" Visible='<%# Config.ShowImage %>' CssClass="post">
                                        <div class='img-article' title='<%# Eval("Title") %>'>
                                            <asp:Panel ID="showImage" Visible='<%#ShowImage(Eval("ImageUrl").ToString())%>' runat="server">
                                                <asp:Image ID="image2" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                                    Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' />
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                    <div class="<%# Config.ShowImage ? "info-article lisTin__item" : "info-article-full" %>">
                                        <h3 class="article-title">
                                            <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                                title='<%#Eval("Title") %>'><%#Eval("Title") %></a>

                                        </h3>
                                        <asp:HyperLink ID="editLink" runat="server" EnableViewState="false"
                                            ToolTip="Chỉnh sửa" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                                            Visible="<%# IsEditable && Config.ShowEditInPost %>" CssClass="ModuleEditLink" />
                                        <asp:Panel ID="pnlPost" runat="server" Visible='<%# !Config.TitleOnly %>' CssClass="post motaTeaser">
                                            <div class="article-date">
                                                <span class="article-author"><b></b></span>
                                                <div class="detail-muted">
                                                    <ul class="list-unstyled list-inline">
                                                        <li>
                                                            <em class="fa fa-clock-o">&nbsp;</em>
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

                                            <div class="motaTeaser__content">
                                                <%# Eval("Summary")%>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlArticlePager" runat="server" CssClass="blogpager">
                <portal:mojoCutePager ID="pgr" runat="server" />
            </asp:Panel>
    </asp:Panel>
</div>

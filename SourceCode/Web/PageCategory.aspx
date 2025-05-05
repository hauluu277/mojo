<%@ Page Language="c#" ValidateRequest="false" MaintainScrollPositionOnPostback="true"
    EnableViewStateMac="false" CodeBehind="PageCategory.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.UI.PageCategory" %>

<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="/" Text="Trang chủ" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" Text="Tìm kiếm tin tức" />
    </portal:AdminCrumbContainer>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper searchresults">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
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
                    </style>
                    <asp:Panel ID="pnlLeftMenu" runat="server">
                        <ul>
                            <asp:Repeater ID="rptLeftCategory" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <h3>
                                            <a href='<%# string.Format("{0}{1}",SiteRoot, Eval("Description")) %>' title='<%#Eval("Name") %>'><%#Eval("Name") %></a>
                                        </h3>
                                        <ul class="article_list">
                                            <asp:Repeater runat="server" ID="rptArticle" DataSource='<%#LoadArticle(Eval("ItemID")) %>'>
                                                <ItemTemplate>
                                                    <li>
                                                        <a href="<%# string.Format("{0}{1}",SiteRoot,Eval("ItemUrl").ToString().Replace("~",string.Empty)) %>" title="<%#Eval("Title") %>"><%#Eval("Title") %></a>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </asp:Panel>
                    <asp:Panel ID="pnlShowListCategory" runat="server">
                
                        <h3 class="title-category-child">
                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                        </h3>
                        <div class="list-category-child">
                            <ul>
                                <asp:Repeater ID="rptCategory" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>" title="<%#Eval("Name") %>">
                                                <%#Eval("Name") %> (<%#Eval("TotalArticle") %>)
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlContentArticle">
                        <asp:Repeater ID="rptArticle" runat="server">
                            <ItemTemplate>
                                <div class="article-thongbao">
                                    <div runat="server" visible='<%#ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>' class='img-article' title='<%# Eval("Title") %>' tooltip='<%# Eval("Title") %>'>
                                        <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), 0, 0) %>">
                                            <asp:Image ID="image3" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                        </a>
                                    </div>
                                    <div class="info-article-full">
                                        <h3 class="article-title">
                                            <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                                ToolTip='<%# Eval("Title") %>' Text='<%# Eval("Title") %>' NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), 0, 0) %>'>
                                            </asp:HyperLink>
                                            <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>"
                                                ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>'
                                                Visible="<%# IsEditable %>" CssClass="ModuleEditLink" />
                                        </h3>
                                        <asp:Panel ID="pnlPost" runat="server" CssClass="post">
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
                                                        <li runat="server">
                                                            <em class="fa fa-user">&nbsp;</em>
                                                            <%# DataBinder.Eval(Container.DataItem, "CreatedByUser").ToString()%>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="summary-article">
                                                <%# Eval("Summary")%>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="pnlArticlePager" runat="server" CssClass="blogpager">
                            <portal:mojoCutePager ID="pgr" runat="server" />
                        </asp:Panel>
                    </asp:Panel>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared">
            </portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

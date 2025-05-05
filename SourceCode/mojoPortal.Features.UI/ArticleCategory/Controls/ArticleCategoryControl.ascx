<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleCategoryControl.ascx.cs" Inherits="ArticleFeature.UI.ArticleCategoryControl" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style>
    .top-menu-3ck {
        width: 100%;
        float: left;
        position: relative;
    }

    .content-menu-3ck {
        position: -webkit-sticky;
        position: sticky;
        top: 60px;
    }
</style>
<div class="container bck-menu col-md-3 content-menu-3ck">
    <div class="panel-group " id="accordion_1">
        <asp:Repeater ID="rptMenuCategory" runat="server">
            <ItemTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading <%#Container.ItemIndex == 0?"more":string.Empty  %>">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion_1" href="#category_<%#Eval("ItemID") %>"><%#Eval("Name") %></a>
                        </h4>
                    </div>
                    <div id="category_<%#Eval("ItemID") %>" class="panel-collapse collapse <%#Container.ItemIndex == 0?"in":string.Empty  %>">
                        <div class="panel-body menu-category-article">
                            <ul>
                                <asp:Repeater ID="rptCategoryChild" runat="server" DataSource='<%#LoadCategoryChild(Convert.ToInt32(Eval("ItemID")))%>'>
                                    <ItemTemplate>
                                        <li>
                                            <a href='javascript:scrollToID(category_<%#Eval("ItemID") %>)' title='<%#Eval("Name") %>'><%#Eval("Name") %></a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <%--           <asp:Repeater ID="rptArticleMenu" runat="server" DataSource='<%#LoadArticle(Eval("ItemID"))%>'>
                                    <ItemTemplate>
                                        <li>
                                            <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                                title='<%#Eval("Title") %>'><%#Eval("Title") %></a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>--%>
                            </ul>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<script type="text/javascript">
    function scrollToID(id, speed) {
        var offSet = 70;
        var obj = $(id).offset();
        var targetOffset = $(id).offset().top - offSet;
        $('html,body').animate({ scrollTop: targetOffset }, speed);
    }

    $("#accordion_1 .panel-title a").click(function () {
        var heading = $(this).parent().parent();
        if (heading.hasClass("more")) {
            $("#accordion_1 .panel-heading").removeClass("more");
        } else {
            $("#accordion_1 .panel-heading").removeClass("more");
            heading.addClass("more");
        }


    });
</script>
<div class="bck-article-content col-md-9">
    <h2><span>THÔNG TIN BA CÔNG KHAI</span></h2>
    <ul>
        <asp:Repeater ID="rptCategoryParent" runat="server">
            <ItemTemplate>
                <li><%#Eval("Name") %></li>
                <asp:Repeater ID="rptContentCategory" runat="server" DataSource='<%#LoadCategoryChild(Convert.ToInt32(Eval("ItemID"))) %>'>
                    <ItemTemplate>
                        <div class="bck-category-item" id="category_<%#Eval("ItemID") %>">
                            <div class="bck-category-head">
                                <div>
                                    <h3><%#Eval("Name") %></h3>
                                </div>
                            </div>
                            <div class="bck-article-list">
                                <asp:Repeater ID="rptArticleContent" runat="server" DataSource='<%#LoadArticle(Eval("ItemID"))%>'>
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                                title='<%#Eval("Title") %>'><%#Eval("Title") %></a>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate></ul></FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

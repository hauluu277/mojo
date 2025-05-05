<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TabListSchool.ascx.cs" Inherits="ArticleFeature.UI.TabListSchool" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .xlink {
        font-size: 13px;
        /*font-family:Semibold;*/
    }
</style>
<div style="margin-bottom: 0px;">
    <%--Hiển thị tin hot tin hoạt động của khoa--%>
    <asp:Panel ID="pnlUseTab5" runat="server">
        <div class="ban-tin-truong">
            <div class="header">
                <h2>
                    <a href="<%=SiteRoot+"/tin-hoat-dong" %>" title="Bản tin trường">Bản tin trường</a>
                </h2>
            </div>
            <div class="ban-tin-truong-content">
                <asp:Repeater ID="rptTab_5" runat="server">
                    <ItemTemplate>
                        <div class="ban-tin-truong-item">
                            <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" title="<%#Eval("Title") %>" />
                            <div class="ban-tin-truong-date">
                                <%#FormartDateTime(Eval("StartDate")) %>
                            </div>
                            <h3>
                                <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                    <%#Eval("Title") %>
                                </a>
                            </h3>
                            <div class="ban-tin-truong-summary">
                                <%#Eval("Summary") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </asp:Panel>

    <%--Hiển thị danh sách kiểu dọc và các danh mục con--%>
    <asp:Panel ID="pnlUseTab4" runat="server">
        <div class="articletab-all">
            <div class="btt-wrap">
                <asp:Panel runat="server" ID="Panel3" CssClass="btt-head">
                    <asp:Panel ID="pnlHideLink" runat="server" CssClass="tab-left">
                        <div class="btt-parentTab">
                            <asp:HyperLink runat="server" ID="lnkCategory4" Text="Chưa có thiết lập" CssClass="categoryTitle"></asp:HyperLink>
                        </div>
                    </asp:Panel>
                    <div class="btt-tab-right">
                        <asp:Panel runat="server" ID="nbchTab2" CssClass="nbchTabs">
                            <ul class="tab-child">
                                <asp:Repeater ID="rptTabs2" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <h2>
                                                <a href="<%#SiteRoot+Eval("Description") %>" id="<%# Eval("ItemID").ToString() + ModuleId.ToString() %>" title="<%# Eval("Name").ToString() %>">
                                                    <asp:Label ID="litHeading" runat="server" EnableViewState="false" Text='<%# ArticleUtils.FormatBlogTitle(Eval("Name").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'></asp:Label>
                                                </a>
                                            </h2>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <div class="nbccTabContent">
                    <div class="leftContent">
                        <asp:Panel ID="pnHighlight" runat="server" CssClass="article-left">
                            <div class="articletab-left">
                                <div class="width100">
                                    <asp:Image ID="imgHeightlight" runat="server" Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' />
                                    <div class="hot-title">
                                        <h3>
                                            <asp:HyperLink ID="hplHeightLight" runat="server"></asp:HyperLink>
                                        </h3>
                                        <div class="more-info">
                                            <span id="lblDateHeightLight" runat="server"></span>|
                                            <asp:HyperLink ID="hplCategory" runat="server"></asp:HyperLink>

                                        </div>
                                    </div>
                                    <div class="Sumary" id="lblSumaryHeightLight" runat="server"></div>
                                </div>
                            </div>
                            <%-- todo: bat dau them 3 tin duoi tin noi bat --%>
                            <div class="articletab-right">
                                <div class="articleTitle">
                                    <ul class="ulOrtherArticle">
                                        <asp:Repeater ID="rptArticles" runat="server" SkinID="Blog" EnableViewState="False">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Panel ID="pnlHasImg" runat="server" CssClass="width100 tt-hasimg">
                                                        <asp:Image ID="imgHot" Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                                        <div class="articletab-right-right">
                                                            <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                                                <%#Eval("Title") %></a>
                                                            <div class="articletab-date"><%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %> | <a href='<%#SiteRoot +Eval("CategoryUrl") %>'><%#Eval("CategoryName") %></a></div>
                                                        </div>
                                                    </asp:Panel>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </asp:Panel>
    <%--Hết hiển thị danh sách kiểu dọc và các danh mục con--%>

    <asp:Panel ID="mp_modulecontent" runat="server">
        <div id="nbcwrap<%=ModuleId.ToString() %>" class="nbcwrap">
            <asp:Panel runat="server" ID="nbchead" CssClass="nbchead">
                <%--<asp:Label ID="lblPageTitle" runat="server" CssClass="nbchTitle"></asp:Label>--%>
                <div class="parentlink">
                    <div class="parentTab">
                        <asp:HyperLink runat="server" ID="lnkCategory" Text="Chưa có thiết lập" CssClass="nbchTitle"></asp:HyperLink>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="nbcContent" CssClass="nbcContent">
                <%--Hiển thị tab kiểu 1--%>
                <div class="leftContent tab1type" runat="server" id="TabType1" visible="false">
                    <div class="articleTab2-left">
                        <div class="tab1Left-content">
                            <a href='<%=lnkHotArticle_HRef %>'>
                                <img src='<%=lnkHotArticle_Src %>' alt='<%=lnkHotArticle_Title %>' />
                            </a>
                            <p class="hot-title">
                                <a href='<%=lnkHotArticle_HRef %>'><%=lnkHotArticle_Title %></a>
                            </p>
                            <p class="Sumary">
                                <%= lnkHotArticle_Summary %>
                            </p>
                            <div class="tipnarrow"></div>
                        </div>
                    </div>
                    <div class="articleTab1-right">
                        <ul>
                            <asp:Repeater ID="rptTab_1" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%# Eval("Title") %></a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <%--Kết thúc hiển thị tab kiểu 1--%>
                <%--Hiển thị tab kiểu 2--%>
                <div class="leftContent" runat="server" id="TabType2" visible="false">
                    <asp:Panel ID="pnlFirst" runat="server" Visible='<%#(rptTab_2.Items.Count ==0) %>' CssClass="articleTab2-left">
                        <div class="tab2Left-content">
                            <a href='<%=lnkHotArticle_HRef %>'>
                                <img src='<%=lnkHotArticle_Src %>' alt='<%=lnkHotArticle_Title %>' />
                            </a>
                            <p class="hot-title">
                                <a href='<%=lnkHotArticle_HRef %>'><%=lnkHotArticle_Title %></a>
                                <%--<asp:HyperLink ID="lnkHotArticle" runat="server"></asp:HyperLink>--%>
                            </p>
                            <p class="Sumary">
                                <%= lnkHotArticle_Summary %>
                                <%--<asp:Literal ID="literDescript" runat="server"></asp:Literal>--%>
                            </p>
                            <div class="tipnarrow"></div>
                        </div>
                    </asp:Panel>
                    <div class="articleTab2-right">
                        <ul>
                            <asp:Repeater ID="rptTab_2" runat="server">
                                <ItemTemplate>
                                    <%-- todo: bat dau them 3 tin duoi tin noi bat --%>
                                    <li>
                                        <div class="articleTab2-image">
                                            <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                                <asp:Image ID="image21" runat="server" AlternateText='<%#Eval("Title") %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                                    CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' />
                                            </a>
                                        </div>
                                        <div class="articleTab2-content">
                                            <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%# Eval("Title").ToString() %></a>
                                        </div>
                                    </li>
                                    <%-- todo:ket thuc them 3 tin duoi tin noi bat --%>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <%--Kết thúc tab kiểu 2--%>
                <%--Hiển thị tab kiểu 3--%>
                <asp:Repeater ID="rptTab_3" runat="server">
                    <ItemTemplate>
                        <div>
                            <div class="tab-3">
                                <asp:Image ID="image28" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                    CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' />
                                <div class="tab-3-content">
                                    <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <%#Eval("Title") %>
                                    </a>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <%--Kết thúc tab kiểu 3--%>
                <%--Hiển thị tab kiểu 4--%>
                <asp:Repeater ID="rptTab_4" runat="server">
                    <ItemTemplate>
                        <div class="nbccTabContent" id="<%# "content"+ Eval("ItemID").ToString() + ModuleId.ToString() %>" style="<%# Container.ItemIndex == 0 ? "display: block;": "" %>">
                            <div class="leftContent">
                                <asp:Panel ID="pnlFirst" runat="server" Visible='<%#(rptTab_4.Items.Count ==0) %>' CssClass="width100">
                                    <div class="tabcontentFirst">
                                        <asp:Image ID="image28" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                            CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' />
                                        <p class="hot-title"><a title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%#Eval("Title") %></a></p>
                                        <%--<span><%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %></span>--%>
                                        <p class="Sumary"><%#Eval("Summary") %></p>
                                        <div class="tipnarrow"></div>
                                    </div>
                                </asp:Panel>
                                <%-- todo: bat dau them 3 tin duoi tin noi bat --%>
                                <div class="articleTitle">
                                    <ul class="ulOrtherArticle">
                                        <asp:Panel ID="pnlSecond" runat="server" Visible='<%#(rptTab_2.Items.Count >00) %>' CssClass="width100">
                                            <li>
                                                <h2 class="xlink">
                                                    <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%#Eval("Title") %></a><span class="nbcupdatedate">(<%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %>)</span>
                                                </h2>
                                            </li>
                                        </asp:Panel>
                                    </ul>
                                </div>
                                <%-- todo:ket thuc them 3 tin duoi tin noi bat --%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <%--Kết thúc tab kiểu 4--%>
            </asp:Panel>
            <div class="clear"></div>
        </div>
    </asp:Panel>

    <script>
        $(function () {
            <%--$("#nbcwrap<%=ModuleId.ToString()%> .nbccTabContent:first-child").slideDown();--%>
            $("#nbcwrap<%=ModuleId.ToString()%> .nbchTabs ul li a").click(function () {
                $("#nbcwrap<%=ModuleId.ToString()%> .nbchTabs ul li a").removeClass("active");
                $(this).addClass("active");
                $("#nbcwrap<%=ModuleId.ToString()%> .nbccTabContent").slideUp();
                $("#nbcwrap<%=ModuleId.ToString()%> #content" + $(this).attr("id")).slideDown();

            });
            $('ul.ulcontent li a').mouseover(function () {
                $(this).next().next().show();
            }).mouseout(function () {
                $(this).next().next().hide();
            });

            $('ul.ulOrtherArticle li a').mouseover(function () {
                $(this).next().next().show();
            }).mouseout(function () {
                $(this).next().next().hide();
            });

        });

        $(".list_sub_cat").click(function (event) {
            $(this).next().slideDown(); event.stopPropagation();
            $(".sub_cat_item").hide();
        });

        $("body").not(".list_sub_cat ul").click(function () {
            $(".sub_cat_item").hide();
        });

    </script>
</div>


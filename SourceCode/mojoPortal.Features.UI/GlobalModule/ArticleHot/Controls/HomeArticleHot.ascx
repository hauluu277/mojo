<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HomeArticleHot.ascx.cs" Inherits="ArticleFeature.UI.HomeArticleHot" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%--Hiển thị tin nổi bật trên trang chủ--%>
<asp:Panel ID="pnlType1" runat="server">
    <section class="awe-section-8" id="demos">
        <section class="constructo-latest-article-section section-padding">
            <div class="">
                <div class="large-12 columns tintucNoibat">
                    <div class="center-img-tintucNoibat">
                        <%-- slides tin bài --%>
                        <asp:Repeater ID="rptArticleSlider" runat="server">
                            <ItemTemplate>
                                <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                    <source src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" media="(max-width: 480px)">
                                    <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" alt="<%#Eval("Title") %>">
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="tin-noi" id="animTinNoiBat" style="display: none;">
                       <%-- danh sách tin ở dưới --%>
                        <asp:Repeater ID="rptArticle" runat="server">
                            <ItemTemplate>
                                <div class="col-sm-6 item">
                                    <div class="constructo-single-news">
                                        <div class="news-img">
                                            <div class="tin-noi-img">
                                                <picture>
                                                    <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                                        <source src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" media="(max-width: 480px)">
                                                        <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" alt="<%#Eval("Title") %>">
                                                    </a>
                                                </picture>
                                            </div>
                                            <div class="tin-noi-nd">
                                                <div class="nd-tin">
                                                    <div class="backg-nd">
                                                        <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" tabindex="0"><%#Eval("Title") %></a>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </section>
    </section>
</asp:Panel>

<script>
    $(document).ready(function () {
        var animHome = "<%=HieuUngTin%>";
        var delayHome = <%=ThoiGianChuyenDong%>;
        startHomeHotAnim("#animTinNoiBat", animHome, delayHome);
        function startHomeHotAnim(el, anim, delayHome) {
            setTimeout(function () {
                $(el).css("display", "block")
                $(el).addClass(anim + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                    //$(this).removeClass(anim + ' animated');
                });
            }, delayHome * 1000);
        };
    });
</script>


<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HomeArticleHot.ascx.cs" Inherits="ArticleFeature.UI.HomeArticleHot" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%--Hiển thị tin nổi bật trên trang chủ--%>
<asp:Panel ID="pnlType1" runat="server">
    <section class="awe-section-8" id="demos">
        <section class="constructo-latest-article-section section-padding">
            <div class="">
                <div class="columns tintucNoibat container">
                    <div class="col-lg-9 center-img-tintucNoibat fix_pl-0">
                        <%-- slides tin bài --%>
                        <div class="owl-carousel col-lg-12 p-0 content_tintucnoibat" id="owl_article_noibat">
                            <asp:Repeater ID="rptArticleSlider" runat="server">
                                <ItemTemplate>
                                    <div class="item item_slide col-lg-12 col-sm-12">
                                        <div class="col-lg-8 col-md-8 col-sm-12 img_tinnoibat-box p-0">
                                            <img class="img_tinnoibat" src='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' alt='<%#Eval("Title") %>' />
                                        </div>
                                        <div class="col-lg-4 col-md-4 item_slide-text">
                                            <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                                <span class="item_slide-text-top"><%#Eval("Title") %> </span>
                                            </a>
                                            <div class="item_slide-text-bot"><%#Eval("Summary") %></div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                    <div class="tin-noi col-12 col-lg-9" id="animTinNoiBat" style="display: none;">
                        <%-- danh sách tin ở dưới --%>
                        <asp:Repeater ID="rptArticle" runat="server">
                            <ItemTemplate>
                                <div class="col-sm-6 item col-sm-12 tinnoibat_bot">
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
                                                        <div>
                                                            <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" tabindex="0"><%#Eval("Title") %></a>
                                                            <div class="cds-content__right-time"><%#string.Format("{0:dd/MM/yyyy}", Eval("StartDate")) %></div>
                                                        </div>
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
    <script>
        $(document).ready(function () {
            if (detectMob() == false) {
                $('#owl_article_noibat').owlCarousel({
                    loop: true,
                    margin: 10,
                    autoplay: true,
                    autoPlaySpeed: 5000,
                    autoPlayTimeout: 5000,
                    autoplayHoverPause: true,
                    responsive: {
                        0: {
                            items: 1
                        },
                    },
                    nav: true,
                    navText: ['<i class="owl-prev-icon"></i>', '<i class="owl-next-icon"></i>'],
                });
            } else {
                $("#owl_article_noibat").hide();
            }
            var animHome = "<%=HieuUngTin%>";
              var delayHome = <%=ThoiGianChuyenDong%>;
            startHomeHotAnim("#animTinNoiBat", animHome, delayHome);
            function startHomeHotAnim(el, anim, delayHome) {
                console.log(delayHome * 1000)
                setTimeout(function () {
                    $(el).css("display", "block")
                    $(el).addClass(anim + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                        //$(this).removeClass(anim + ' animated');
                    });
                }, delayHome * 1000);
            };
        });

    </script>

</asp:Panel>


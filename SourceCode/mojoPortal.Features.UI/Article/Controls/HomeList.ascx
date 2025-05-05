<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HomeList.ascx.cs" Inherits="ArticleFeature.UI.HomeList" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>


<link href="../../Data/plugins/Gallery-Image-fillter-Filterizr/index.css" rel="stylesheet" />
<link href="../../Data/plugins/lightgallery/css/lightgallery.min.css" rel="stylesheet" />

<section class="awe-section-5">
    <section class="constructo-project-section section-padding">
        <div class="container">
                <div class="col-xs-12 text-center">
                    <div class="section-title">
                        <h2>SẢN PHẨM CỦA CHÚNG TÔI</h2>
                        <p>HiNET phát triển phần mềm ứng dụng cho khối Chính phủ hướng đến mục tiêu tăng cường năng lực và thúc đẩy phát triển Chính phủ điện tử tại Việt Nam</p>
                        <div class="line">
                            <img src="//bizweb.dktcdn.net/100/273/169/themes/624353/assets/line.png?1513997755585" alt="doanhnghiep">
                        </div>
                    </div>
                </div>
                <div class="width100 row padding-top">
                    <div class="col-xs-12 text-center">
                        <div class="isotop-nav">
                            <ul class="homelist">
                                <%--<li class="current active" data-filter="all">Tất cả</li>--%>
                                <asp:Repeater ID="rptArticleCategory" runat="server">
                                    <ItemTemplate>
                                        <li <%# rptArticleCategory.Items.Count == 0 ? "class='current active'" : string.Empty %> data-filter="<%#Eval("ItemID") %>"><%#Eval("name") %></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row isotop-active">
                    <div class="filtr-container mdb-lightbox">
                        <asp:Repeater ID="rptArticle" runat="server">
                            <ItemTemplate>

                                <div class="col-xs-6 col-sm-4  thiet-ke-noi-that  kien-truc filtr-item" data-category="<%#Eval("CategoryID") %>" style="position: absolute; left: 0px; top: 60px;">
                                    <div class="constructo-single-project">
                                        <div class="project-img" data-src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" data-sub-html="<%#Eval("Title") %>">
                                            <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" alt="<%#Eval("Title") %>">
                                            <div class="pro-hover">
                                                <a href="//bizweb.dktcdn.net/100/273/169/themes/624353/assets/al_image_1.jpg?1513997755585" class="fa magnifiq fa-search"></a>
                                            </div>
                                        </div>
                                        <div class="pro-title">
                                            <div class="pro-dat">
                                                <p>15</p>
                                            </div>
                                            <h4>
                                                <a title="<%#Eval("Title") %>" href="<%#ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" title="<%#Eval("Title") %>">
                                                    <%#Eval("Title") %>
                                                </a>

                                            </h4>
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
<script src="../../Data/plugins/Gallery-Image-fillter-Filterizr/jquery.filterizr.min.js"></script>
<script src="../../Data/plugins/Gallery-Image-fillter-Filterizr/controls.js"></script>
<%--<script src="../../Data/plugins/lightgallery/js/picturefill.min.js"></script>--%>
<script src="../../Data/plugins/lightgallery/js/lightgallery.min.js"></script>
<script src="../../Data/plugins/lightgallery/js/lg-fullscreen.min.js"></script>
<script src="../../Data/plugins/lightgallery/js/lg-thumbnail.min.js"></script>
<script src="../../Data/plugins/lightgallery/js/lg-autoplay.min.js"></script>
<script src="../../Data/plugins/lightgallery/js/lg-video.min.js"></script>
<%--<script src="js/lg-video.js"></script>--%>
<script src="../../Data/plugins/lightgallery/js/lg-zoom.min.js"></script>
<script src="../../Data/plugins/lightgallery/js/lg-hash.min.js"></script>
<script src="../../Data/plugins/lightgallery/js/lg-pager.min.js"></script>
<script src="../../Data/plugins/lightgallery/js/jquery.mousewheel.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        var fillter = $(".filtr-item:first-child").attr("data-category");
        $('.filtr-container').filterizr('filter', fillter);
    });
    //light gallery
    //$(".mdb-lightbox").find("img").each(function () {
    //    $(this).parent().attr("data-src", $(this).attr("src"));
    //    $(this).parent().attr("data-sub-html", $(this).attr("alt"));
    //});
    //$(".mdb-lightbox").lightGallery({ selector: "figure a" }); 
    $(".mdb-lightbox").lightGallery({ selector: ".project-img" });
    </script>

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SlideAlbumList.ascx.cs" Inherits="MediaFeature.UI.SlideAlbumList" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<asp:Panel ID="pnlGallery" runat="server">
    <link href="/Data/plugins/lightgallery/css/lightgallery.min.css" rel="stylesheet" />
    <link href="/Data/plugins/lightslider/css/lightslider.css" rel="stylesheet" />
    <link href="/Data/js/Carousel/assets/css/owl.carousel.min.css" rel="stylesheet">
    <link href="/Data/js/Carousel/assets/css/owl.theme.default.min.css" rel="stylesheet">


    <script src="/Data/js/Carousel/assets/js/owl.carousel.js"></script>
    <script src="../../Data/js/scrollview.js"></script>
    <%--<script src="/Data/plugins/lightslider/js/lightslider.js"></script>--%>
    <style type="text/css">
        .demo-gallery > ul {
            margin-bottom: 0;
        }

            .demo-gallery > ul > li {
                text-align: center;
                padding-bottom: 15px;
                border-bottom: 1px solid #aaa;
                margin-bottom: 15px;
                float: left;
                width: 100%;
            }

                .demo-gallery > ul > li a {
                    border: 3px solid #FFF;
                    border-radius: 3px;
                    display: block;
                    overflow: hidden;
                    position: relative;
                    text-align: center;
                }

                    .demo-gallery > ul > li a > img {
                        -webkit-transition: -webkit-transform 0.15s ease 0s;
                        -moz-transition: -moz-transform 0.15s ease 0s;
                        -o-transition: -o-transform 0.15s ease 0s;
                        transition: transform 0.15s ease 0s;
                        -webkit-transform: scale3d(1, 1, 1);
                        transform: scale3d(1, 1, 1);
                        height: auto;
                        text-align: center;
                        display: inline-block;
                        width: auto;
                    }

                    .demo-gallery > ul > li a:hover > img {
                        -webkit-transform: scale3d(1.1, 1.1, 1.1);
                        transform: scale3d(1.1, 1.1, 1.1);
                    }

                    .demo-gallery > ul > li a:hover .demo-gallery-poster > img {
                        opacity: 1;
                    }

                    .demo-gallery > ul > li a .demo-gallery-poster {
                        background-color: rgba(0, 0, 0, 0.1);
                        bottom: 0;
                        left: 0;
                        position: absolute;
                        right: 0;
                        top: 0;
                        -webkit-transition: background-color 0.15s ease 0s;
                        -o-transition: background-color 0.15s ease 0s;
                        transition: background-color 0.15s ease 0s;
                    }

                        .demo-gallery > ul > li a .demo-gallery-poster > img {
                            left: 50%;
                            margin-left: -10px;
                            margin-top: -10px;
                            opacity: 0;
                            position: absolute;
                            top: 50%;
                            -webkit-transition: opacity 0.3s ease 0s;
                            -o-transition: opacity 0.3s ease 0s;
                            transition: opacity 0.3s ease 0s;
                        }

                    .demo-gallery > ul > li a:hover .demo-gallery-poster {
                        background-color: rgba(0, 0, 0, 0.5);
                    }

        .demo-gallery .justified-gallery > a > img {
            -webkit-transition: -webkit-transform 0.15s ease 0s;
            -moz-transition: -moz-transform 0.15s ease 0s;
            -o-transition: -o-transform 0.15s ease 0s;
            transition: transform 0.15s ease 0s;
            -webkit-transform: scale3d(1, 1, 1);
            transform: scale3d(1, 1, 1);
            height: 100%;
            width: 100%;
        }

        .demo-gallery .justified-gallery > a:hover > img {
            -webkit-transform: scale3d(1.1, 1.1, 1.1);
            transform: scale3d(1.1, 1.1, 1.1);
        }

        .demo-gallery .justified-gallery > a:hover .demo-gallery-poster > img {
            opacity: 1;
        }

        .demo-gallery .justified-gallery > a .demo-gallery-poster {
            background-color: rgba(0, 0, 0, 0.1);
            bottom: 0;
            left: 0;
            position: absolute;
            right: 0;
            top: 0;
            -webkit-transition: background-color 0.15s ease 0s;
            -o-transition: background-color 0.15s ease 0s;
            transition: background-color 0.15s ease 0s;
        }

            .demo-gallery .justified-gallery > a .demo-gallery-poster > img {
                left: 50%;
                margin-left: -10px;
                margin-top: -10px;
                opacity: 0;
                position: absolute;
                top: 50%;
                -webkit-transition: opacity 0.3s ease 0s;
                -o-transition: opacity 0.3s ease 0s;
                transition: opacity 0.3s ease 0s;
            }

        .demo-gallery .justified-gallery > a:hover .demo-gallery-poster {
            background-color: rgba(0, 0, 0, 0.5);
        }

        .demo-gallery .video .demo-gallery-poster img {
            height: 48px;
            margin-left: -24px;
            margin-top: -24px;
            opacity: 0.8;
            width: 48px;
        }

        .demo-gallery.dark > ul > li a {
            border: 3px solid #04070a;
        }

        .home .demo-gallery {
            padding-bottom: 80px;
        }

        .lightgallery {
            list-style: none;
        }
    </style>
    <style type="text/css">
        .all-list ul li {
            float: left;
            width: calc(25% - 5px);
            list-style: none;
            padding: 0;
            margin: 0;
            border-bottom: 5px solid white;
            border-right: 5px solid white;
            overflow: hidden;
        }

        .manhdeptrai {
            margin-bottom: 10px;
        }

        #tableAnhThumb .owl-carousel.owl-rtl .owl-item {
            float: left !important;
        }

        #tableAnhThumb .owl-nav .owl-next {
            height: 120px !important;
            margin-top: -36px !important;
            margin-right: -35px !important;
        }

        #tableAnhThumb .owl-nav button span {
            font-size: 75px !important;
        }

        #tableAnhThumb .owl-nav .owl-prev {
            height: 120px !important;
            margin-top: -36px !important;
            margin-left: -35px !important;
        }

        #tableAnhThumb .owl-item {
            width: 170px !important;
            height: 120px;
            padding: 10px;
            margin: 0 !important;
        }

            #tableAnhThumb .owl-item img {
                width: 100% !important;
            }

        #tableAnhThumb {
            background: #daf8ff;
            width: 96%;
            margin: auto;
        }

            #tableAnhThumb .owl-nav button:focus {
                border: 0 !important;
                box-shadow: none !important;
                outline: none !important;
            }

            #tableAnhThumb .owl-theme .owl-nav [class*=owl-]:hover {
                color: black !important;
            }

            #tableAnhThumb .owl-carousel {
                margin: 0 !important;
            }

        .DetailImageSlide > p {
            margin-bottom: 0 !important;
        }

        .itemLbImage {
            height: auto;
            padding-bottom: 15px;
        }

            .itemLbImage img {
                object-fit: cover;
                /*width: 100%;*/
                display: inline;
            }

        .author_gallery {
            text-align: right;
            font-size: 17px;
        }

        #lightgallery {
            display: block !important;
            list-style: none;
        }

            #lightgallery .h3Slide {
                float: left;
                width: 100%;
                overflow: hidden;
                text-overflow: ellipsis;
                display: -webkit-box;
                -webkit-line-clamp: 3; /* number of lines to show */
                -webkit-box-orient: vertical;
            }

                #lightgallery .h3Slide.lg-sub-html h4 {
                    font-size: 16px;
                }
    </style>

    <div class="warrperSilderIMG">
        <div class="DetailImageSlide">
            <p>
                <asp:Label ID="lblIMGofLibrary" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblGroupName" runat="server" CssClass="slideTitle"></asp:Label>
                <asp:HyperLink ID="hplEdit" runat="server" CssClass="ModuleEditLink" ToolTip="Edit" Text="Chỉnh sửa"></asp:HyperLink>
            </p>
            <span>
                <asp:Label ID="lblSapoLibrary" runat="server"></asp:Label>
            </span>
        </div>
        <%-- <div id="divContainer" style="overflow:auto;border: 1px solid #ddd;margin-bottom:5px;height:auto;width:100%;padding: 15px;text-align: center;">
             <div class="grNextImage">
                        <a href="javascript:void(0)" onclick="ChangeImagePre()" title="Ảnh trước"><i class="fa fa-chevron-left" aria-hidden="true"></i></a>
                        <a href="javascript:void(0)" onclick="ChangeImageNext()" title="Ảnh kế tiếp"><i class="fa fa-chevron-right" aria-hidden="true"></i></a>
                    </div>
            <div class="box-imgaeSlide">
                <img id="imageContent" style="border: 1px solid rgb(230, 230, 230);width: 70%;object-fit: contain; cursor: default; display: inline;" onload="ShowThumb()" src="<%# Eval("ImageUrl") %>"/>
            
            </div>
        </div>--%>

        <%--Nav ảnh--%>
        <div class="lightSlideImageLeft">
            <div class="lightSlideImageContent">
            </div>
            <div class="lightSlideImage">
                <div class="item">
                    <div class="clearfix" style="max-height: 400px;">
                        <ul id="content-slider" class="content-slider">
                            <asp:Repeater ID="rptSlider" runat="server">
                                <ItemTemplate>
                                    <li data-thumb='<%# Eval("FilePath") %>' data-src='<%# Eval("FilePath") %>' title='<%#Eval("Description") %>' style="cursor: pointer">
                                        <img data-id="<%#Container.ItemIndex + 1%>" src='<%# Eval("FilePath") %>' alt='<%#Eval("Description") %>' />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>

        </div>


        <div class="demo-gallery">
            <ul id="lightgallery" class="row">
                <asp:Repeater ID="dtlData" runat="server">
                    <ItemTemplate>
                        <li class="itemLbImage" data-responsive="<%# Eval("FilePath") %> 375, <%# Eval("FilePath") %> 480, <%# Eval("FilePath") %> 800" data-src="<%# Eval("FilePath") %>" data-sub-html="<h4 class='h3Slide'><%#Eval("Description") %></h4>">
                            <img class="img-responsive" src="<%# Eval("FilePath") %>">
                            <h4 class='h3Slide'><%#Eval("Description") %></h4>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="author_gallery">
                <asp:Label ID="lblAuthor" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblDateCreate" runat="server" CssClass="slideDate"></asp:Label>
            </div>
        </div>

        <!-- #endregion Jssor Slider End -->
        <%--Thư viện khác--%>
        <div class="lightSlideImageOther">
            <div class="slideOtherTitle">
                <p>
                    <asp:Label ID="lblOrtherLibrary" runat="server"></asp:Label>
                </p>
            </div>

            <div class="all-list">
                <ul>
                    <asp:Repeater ID="rptGroupMedia" runat="server">
                        <ItemTemplate>
                            <li>
                                <div class="gallery-list">
                                    <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                        <div class="gallery-img">
                                            <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                        </div>
                                        <div class="des-item">
                                            <%--  <%#Eval("CreatedDate") %>--%>
                                            <span class="des-fix">
                                                <%#Eval("NameGroup") %>
                                            </span>
                                            </span>
                                        </div>
                                    </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#lightgallery').lightGallery({ hash: false });
        });
    </script>

    <script src="/Data/plugins/lightgallery/js/picturefill.min.js"></script>
    <script src="/Data/plugins/lightgallery/js/lightgallery.min.js"></script>
    <script src="/Data/plugins/lightgallery/js/lg-fullscreen.min.js"></script>
    <script src="/Data/plugins/lightgallery/js/lg-thumbnail.min.js"></script>
    <script src="/Data/plugins/lightgallery/js/lightgallery-all.min.js"></script>
    <%--<script src="js/lg-video.js"></script>--%>
    <%--<script src="/Data/plugins/lightgallery/js/lg-autoplay.min.js"></script>--%>
    <script src="/Data/plugins/lightgallery/js/lg-zoom.min.js"></script>
    <script src="/Data/plugins/lightgallery/js/lg-hash.min.js"></script>
    <script src="/Data/plugins/lightgallery/js/lg-pager.min.js"></script>
    <script src="/Data/plugins/lightgallery/js/jquery.mousewheel.min.js"></script>
    <asp:Label ID="GalleryNull" runat="server" Visible="false"></asp:Label>
    <asp:HiddenField runat="server" ID="hfFeatured" ClientIDMode="Static" />
    <asp:HiddenField ID="hfView" runat="server" ClientIDMode="Static" />
</asp:Panel>
<asp:Panel ID="pnlCategoryChild" runat="server">
    <style type="text/css">
        .all-list ul li {
            width: calc(25% - 10px);
        }
    </style>
    <div class="all-list">
        <div class="gallery">
            <asp:Label ID="lblCategory" runat="server"></asp:Label>
        </div>
        <ul>
            <asp:Repeater ID="rptCategoryChild" runat="server">
                <ItemTemplate>
                    <li>
                        <div class="gallery-list">
                            <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                <div class="gallery-img">
                                    <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                </div>
                                <div class="des-item">
                                    <%--  <%#Eval("CreatedDate") %>--%>
                                    <span class="des-fix">
                                        <%#Eval("NameGroup") %>
                                    </span>
                                    </span>
                                </div>
                            </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <asp:Label ID="Categorynull" runat="server" Visible="false"></asp:Label>
</asp:Panel>


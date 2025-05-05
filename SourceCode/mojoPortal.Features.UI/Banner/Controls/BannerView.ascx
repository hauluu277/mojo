<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="BannerView.ascx.cs" Inherits="BannerFeature.UI.BannerView" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Import Namespace="BannerFeature.UI" %>
<div class="clearfix"></div>
<div class="view__banner__box">

    <%-- Thiết lập cài đặt slider banner ---%>
    <asp:HiddenField ID="timeDisplayIMG" runat="server" />
    <asp:HiddenField ID="timeChangeIMG" runat="server" />
    <asp:HiddenField ID="widthIMG" runat="server" />
    <asp:HiddenField ID="heightIMG" runat="server" />
    <% if (slideSetting == BannerFeature.UI.BannerConstant.OWL_KhoaPhong || slideSetting == BannerFeature.UI.BannerConstant.OWL_DoiTac)
        {%>


    <%} %>
    <%if (slideSetting == BannerFeature.UI.BannerConstant.FullWidth_AnimatedTouch)
        { %>
    <link href="/ClientScript/Animated-Touch-friendly-Slider-Plugin/css/style.css" rel="stylesheet" />
    <link href="/ClientScript/Animated-Touch-friendly-Slider-Plugin/icons/entypo.css" rel="stylesheet" />
    <div class="view_banner">

        <div class='o-sliderContainer' id="pbSliderWrap0" style="margin-top: 0;">
            <div class='o-slider' id='pbSlider0'>
                <asp:Repeater ID="rptSlideFullwidth" runat="server">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#bool.Parse(Eval("NoClick").ToString())==true?"":SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                            <div class="o-slider--item" data-image="<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()).Replace("~","") %>">
                                <%--<div class="o-slider-textWrap">
                                    <h1 class="o-slider-title" <%# string.IsNullOrEmpty(Eval("Name").ToString())?"style='display:none'":string.Empty %>><%#Eval("Name") %></h1>
                                    <span class="a-divider"></span>
                                    <h2 class="o-slider-subTitle" <%#  string.IsNullOrEmpty(Eval("Description").ToString())?"style='display:none'":string.Empty %>><%#Eval("Description") %></h2>
                                    <span class="a-divider"></span>
                                    <p class="o-slider-paragraph">This is a sub paragraph This is a sub paragraph This is a sub paragraph This is a sub paragraph This is a sub paragraph This is a sub paragraph This is a sub paragraph </p>
                                </div>--%>
                            </div>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
        <script src="/ClientScript/Animated-Touch-friendly-Slider-Plugin/js/hammer.min.js"></script>
        <script src="/ClientScript/Animated-Touch-friendly-Slider-Plugin/js/slider.js"></script>
        <script>
            $(document).ready(function () {
                var timeChange = parseInt($("#<%=timeChangeIMG.ClientID%>").val());
                var timeDisplay = parseInt($("#<%=timeDisplayIMG.ClientID%>").val());
                $('#pbSlider0').pbTouchSlider({
                    slider_Wrap: '#pbSliderWrap0',
                    slider_Item_Width: 100,
                    slider_Threshold: 10,
                    slider_Speed: timeChange,
                    slider_Ease: 'ease-out',
                    slider_Drag: false,
                    slider_auto: true,
                    slider_time: timeDisplay,
                    slider_Arrows: {
                        enabled: true
                    },
                    slider_Dots: {
                        class: '.o-slider-pagination',
                        enabled: true,
                        preview: true
                    },
                    slider_Breakpoints: {
                        default: {
                            height: 551
                        },
                        tablet: {
                            height: 350,
                            media: 1024
                        },
                        smartphone: {
                            height: 250,
                            media: 768
                        }
                    }
                });
            });
        </script>
    </div>
    <%}
        else if (slideSetting == BannerFeature.UI.BannerConstant.OWL_DoiTac)
        { %>
    <div class="view_banner__doitac">

        <div class="container">
            <h2 class="page-header">
                <asp:Label runat="server" ID="lblNameSlider"></asp:Label>
            </h2>
            <div class="owl-carousel owl-theme" id="owl_DoiTac">
                <asp:Repeater ID="rptOwlDoiTac" runat="server">
                    <ItemTemplate>
                        <div class="item">
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#bool.Parse(Eval("NoClick").ToString())==true?"":SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                                <asp:Image ID="Image1" Width='100%' Height='<%#Height() %>' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()).Replace("~","") %>' />
                            </asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <script>
                $(document).ready(function () {
                    var owlDoiTac = $('#owl_DoiTac');
                    owlDoiTac.owlCarousel({
                        items: 6,
                        loop: true,
                        margin: 10,
                        autoplay: true,
                        autoplayTimeout: parseInt($("#<%=timeDisplayIMG.ClientID%>").val()),
                        autoplayHoverPause: true,
                        responsiveClass: true,
                        responsive: {
                            0: {
                                items: 2,
                                nav: true
                            },
                            600: {
                                items: 3,
                                nav: false
                            },
                            1000: {
                                items: 6,
                                nav: true,
                                loop: false
                            }
                        }
                    });
                });
            </script>
        </div>
    </div>
    <%}
        else if (slideSetting == BannerFeature.UI.BannerConstant.OWL_KhoaPhong)
        { %>
    <div class="view_banner__phongban">

        <div class="container">
            <h2 class="page-header">
                <asp:HyperLink ID="hplTieuDe" runat="server"></asp:HyperLink>
            </h2>
            <div class="owl-carousel owl-theme" id="owl_KhoaPhong">
                <asp:Repeater ID="rptOwlKhoaPhong" runat="server">
                    <ItemTemplate>
                        <div class="item">
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#bool.Parse(Eval("NoClick").ToString())==true?"":SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                                <asp:Image ID="Image1" Width='100%' Height='<%#Height() %>' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()).Replace("~","") %>' />
                            </asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <script>
                $(document).ready(function () {
                    var owlKhoaPhong = $('#owl_KhoaPhong');
                    owlKhoaPhong.owlCarousel({
                        items: 7,
                        nav: true,
                        loop: true,
                        margin: 10,
                        autoplay: true,
                        autoplayTimeout: parseInt($("#<%=timeDisplayIMG.ClientID%>").val()),
                        autoplayHoverPause: true,
                         responsiveClass: true,
                        responsive: {
                            0: {
                                items: 3,
                                nav: true
                            },
                            600: {
                                items: 5,
                                nav: false
                            },
                            1000: {
                                items: 7,
                                nav: true,
                                loop: false
                            }
                        }
                    });
                });
            </script>
            <div>
            </div>
        </div>
    </div>

    <%}
        else if (slideSetting == BannerFeature.UI.BannerConstant.FullWidth_Jssor)
        { %>
    <script src="/Data/js/full-width-slider/jssor.slider-27.5.0.min.js"></script>

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var timeChange = parseInt($("#<%=timeChangeIMG.ClientID%>").val());
            var timeDisplay = parseInt($("#<%=timeDisplayIMG.ClientID%>").val());
            var jssor_1_options = {
                $AutoPlay: true,
                $SlideDuration: timeChange,
                $SlideDuration: timeDisplay,
                $SlideEasing: $Jease$.$OutQuint,
                $ArrowNavigatorOptions: {
                    $Class: $JssorArrowNavigator$
                },
                $BulletNavigatorOptions: {
                    $Class: $JssorBulletNavigator$
                }
            };

            var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

            /*responsive code begin*/
            /*you can remove responsive code if you don't want the slider scales while window resizing*/
            function ScaleSlider() {
                var refSize = jssor_1_slider.$Elmt.parentNode.clientWidth;
                if (refSize) {
                    refSize = Math.min(refSize, 1920);
                    jssor_1_slider.$ScaleWidth(refSize);
                }
                else {
                    window.setTimeout(ScaleSlider, 30);
                }
            }
            ScaleSlider();
            $(window).bind("load", ScaleSlider);
            $(window).bind("resize", ScaleSlider);
            $(window).bind("orientationchange", ScaleSlider);
            /*responsive code end*/
        });
    </script>
    <style>
        .jssorb05 {
            position: absolute;
        }

            .jssorb05 div, .jssorb05 div:hover {
                position: absolute;
                width: 10px !important;
                height: 10px !important;
                background: #333;
                overflow: hidden;
                border-radius: 10px;
                cursor: pointer;
            }

            .jssorb05 .av {
                background-color: white;
            }

            .jssorb05 div {
                background-position: -7px -7px;
            }

                .jssorb05 div:hover, .jssorb05 .av:hover {
                    background-position: -37px -7px;
                }

            .jssorb05 .av {
                background-position: -67px -7px;
            }

            .jssorb05 .dn, .jssorb05 .dn:hover {
                background-position: -97px -7px;
            }

        .jssora22l, .jssora22r {
            display: block;
            position: absolute;
            /* size of arrow element */
            width: 40px;
            height: 58px;
            cursor: pointer;
            background: url('/Data/js/full-width-slider/a22.png') center center no-repeat;
            overflow: hidden;
        }

        .jssora22l {
            background-position: -10px -31px;
        }

        .jssora22r {
            background-position: -70px -31px;
        }

        .jssora22l:hover {
            background-position: -130px -31px;
        }

        .jssora22r:hover {
            background-position: -190px -31px;
        }

        .jssora22l.jssora22ldn {
            background-position: -250px -31px;
        }

        .jssora22r.jssora22rdn {
            background-position: -310px -31px;
        }

        .jssora22l.jssora22lds {
            background-position: -10px -31px;
            opacity: .3;
            pointer-events: none;
        }

        .jssora22r.jssora22rds {
            background-position: -70px -31px;
            opacity: .3;
            pointer-events: none;
        }

        .slidestop img {
            margin: 0;
            height: 520px !important;
        }

        .block {
            display: block;
        }
    </style>
    <div id="jssor_1" style="position: relative; margin: 0 auto; top: 0px; left: 0px; width: 1600px; height: 520px; overflow: hidden; visibility: hidden;">
        <!-- Loading Screen -->
        <div data-u="loading" style="position: absolute; top: 0px; left: 0px; background-color: rgba(0,0,0,0.7);">
            <div style="filter: alpha(opacity=70); opacity: 0.7; position: absolute; display: block; top: 0px; left: 0px; width: 100%; height: 100%;"></div>
            <div style="position: absolute; display: block; background: url('/Data/js/full-width-slider/loading.gif') no-repeat center center; top: 0px; left: 0px; width: 100%; height: 100%;"></div>
        </div>
        <div class="slidestop" data-u="slides" style="cursor: default; position: relative; top: 0px; left: 0px; width: 1600px; height: 520px; overflow: hidden;">
            <asp:Repeater ID="rptJssor" runat="server">
                <ItemTemplate>
                    <div class="abc">
                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="block" NavigateUrl='<%#bool.Parse(Eval("NoClick").ToString())==true?"":SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                        <img alt='<%#Eval("Name") %>' src='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()).Replace("~","") %>' />
                        </asp:HyperLink>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <!-- Bullet Navigator -->
        <div data-u="navigator" class="jssorb05" style="bottom: 16px; right: 16px;" data-autocenter="1">
            <!-- bullet navigator item prototype -->
            <div data-u="prototype" style="width: 16px; height: 16px;"></div>
        </div>
        <!-- Arrow Navigator -->
        <span data-u="arrowleft" class="jssora22l" style="top: 0px; left: 8px; width: 40px; height: 58px;" data-autocenter="2"></span>
        <span data-u="arrowright" class="jssora22r" style="top: 0px; right: 8px; width: 40px; height: 58px;" data-autocenter="2"></span>
    </div>

   <%-- <script type="text/javascript">jssor_1_slider_init();</script>--%>
    <%}
        else
        { %>
    <asp:Repeater runat="server" ID="rptBanner">
        <ItemTemplate>
            <%if (IsHorizontal)
                { %>

            <div class="link_edit" style='width: <%# Width(IsHorizontal, isheight, b++, Convert.ToDecimal(Eval("Width"))).ToString().Replace(",", ".")+"%" %>'>
                <%--ImageUrl='<%# EditLinkImageUrl %>'--%>
                <asp:HyperLink ID="lnkEdit" runat="server" CssClass="editBanner" Text="<%# EditLinkText %>" ToolTip="<%# EditLinkTooltip %>" Visible='<%#IsEditable %>'
                    NavigateUrl='<%#SiteRoot+"/Banner/EditPost.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>'></asp:HyperLink>
                <asp:HyperLink ID="linkAdvertise" runat="server" NavigateUrl='<%#bool.Parse(Eval("NoClick").ToString())==true?"":SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                    <%if (isheight == true)
                        {%>
                    <asp:Image ID="imgBanner" Height='<%#Height() %>' Width="100%" Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' /><%} %>
                    <%else
                        { %><asp:Image ID="Image2" Width="100%" Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' /><%} %>
                    <%#BuildFlashObject(Convert.ToBoolean(Eval("IsImage")),Eval("Path").ToString(), Convert.ToDecimal(Eval("Width")),IsHorizontal,isNotHeight) %>
                </asp:HyperLink>
            </div>
            <%} %>
            <%else
                { %><div class="banner_vertical">
                    <%-- ImageUrl='<%# EditLinkImageUrl %>'--%>
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="editBanner" Text="<%# EditLinkText %>" ToolTip="<%# EditLinkTooltip %>" Visible='<%#IsEditable %>'
                        NavigateUrl='<%#SiteRoot+"/Banner/EditPost.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>'></asp:HyperLink>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#bool.Parse(Eval("NoClick").ToString())==true?"":SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                        <%if (isheight == true)
                            {%>
                        <asp:Image ID="Image1" Width='100%' Height='<%#Height() %>' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' /><%} %>
                        <%else
                            { %><asp:Image ID="Image3" Width='100%' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' /><%} %>
                        <%#BuildFlashObject(Convert.ToBoolean(Eval("IsImage")),Eval("Path").ToString(), Convert.ToDecimal(Eval("Width")), IsVertical, isNotHeight) %>
                    </asp:HyperLink>
                </div>
            <%} %>
        </ItemTemplate>
    </asp:Repeater>
    <%} %>
</div>

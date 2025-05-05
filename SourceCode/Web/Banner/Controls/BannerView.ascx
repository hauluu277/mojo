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
            <div class="owl-carousel owl-theme banner-doitac">
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
                    var owlDoiTac = $('.banner-doitac');
                    owlDoiTac.owlCarousel({
                        items: 5,
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
                            400: {
                                items: 2,
                                nav: false
                            },
                            600: {
                                items: 2,
                                nav: false
                            },
                            1000: {
                                items: 5,
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
<%--    <link href="/Data/plugins/assets/owl.carousel.min.css" rel="stylesheet" />
    <link href="/Data/plugins/assets/owl.theme.default.min.css" rel="stylesheet" />
    <script src="/Data/plugins/owl.carousel.js"></script>--%>
    <div class="view_banner__phongban">

        <div class="container">
            <h2 class="page-header">
                <asp:HyperLink ID="hplTieuDe" runat="server"></asp:HyperLink>
            </h2>
            <div class="row">
                <div class="owl-carousel owl-theme banner-khoaphong">
                    <asp:Repeater ID="rptOwlKhoaPhong" runat="server">
                        <ItemTemplate>
                            <div class="item">
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#bool.Parse(Eval("NoClick").ToString())==true?"":SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                                    <asp:Image ID="Image1" Width='100%' Height='<%#Height() %>' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()).Replace("~","") %>' />
                                    <span>
                                        <asp:Literal ID="des" runat="server" Text='<%#Eval("Name")%>'></asp:Literal>
                                    </span>
                                </asp:HyperLink>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <script>
                $(document).ready(function () {
                    var owlKhoaPhong = $('.banner-khoaphong');
                    owlKhoaPhong.owlCarousel({
                        items: 5,
                        nav: true,
                        navText: ["<div class='nav-btn prev-slide'></div>", "<div class='nav-btn next-slide'></div>"],
                        nav: true,
                        loop: true,
                        margin: 20,
                        dots: false,
                        autoplay: true,
                        autoplayTimeout: parseInt($("#<%=timeDisplayIMG.ClientID%>").val()),
                        autoplayHoverPause: true,
                        responsiveClass: true,
                        responsive: {
                            0: {
                                items: 1,
                                nav: true
                            },
                            400: {
                                items: 1,
                                nav: false
                            },
                            600: {
                                items: 2,
                                nav: false
                            },
                            768: {
                                items: 2,
                                nav: false
                            },
                            992: {
                                items: 3, // Hiển thị 3 ảnh khi màn hình từ 992px đến 1180px
                                nav: false,
                                loop: true
                            },
                            992: {
                                items: 3, // Hiển thị 3 ảnh khi màn hình từ 992px đến 1180px
                                nav: false,
                                loop: true
                            },
                            1181: {
                                items: 5,
                                nav: false,
                                loop: true// Hiển thị 3 ảnh khi màn hình lớn hơn hoặc bằng 1180px
                            },


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
    <link href="/Data/plugins/unslider/css/unslider.css" rel="stylesheet" />
    <link href="/Data/plugins/unslider/css/unslider-dots.css" rel="stylesheet" />
    <%--<link href="/Data/plugins/unslider/css/mobile.css" rel="stylesheet" />--%>
    <script src="https://cdn.jsdelivr.net/npm/jquery.event.move@1.3.6/js/jquery.event.move.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/jquery.event.swipe@0.5.4/js/jquery.event.swipe.min.js"></script>
    <script src="/Data/plugins/unslider/js/unslider-min.js"></script>

    <div class="banner">
        <ul>
            <asp:Repeater ID="rptJssor" runat="server">
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="block" NavigateUrl='<%#bool.Parse(Eval("NoClick").ToString())==true?"":SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                        <img alt='<%#Eval("Name") %>' src='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()).Replace("~","") %>' />
                            <div class="inner">
						<h1>Open-source.</h1>
						<p>Everything to do with Unslider is hosted on GitHub.</p>
						<a class="btn" href="//github.com/idiot/unslider" rel="nofollow">Contribute</a>
					</div>
                        </asp:HyperLink>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <!-- The HTML -->

    <script type="text/javascript">
        var timeChange = parseInt($("#<%=timeChangeIMG.ClientID%>").val());
        var timeDisplay = parseInt($("#<%=timeDisplayIMG.ClientID%>").val());
        jQuery(document).ready(function ($) {
            $('.banner').unslider({
                speed: timeDisplay,               //  The speed to animate each slide (in milliseconds)
                delay: timeChange,
                autoplay: true,//  The delay between slide animations (in milliseconds)
                complete: function () { },  //  A function that gets called after every slide animation
                keys: true,               //  Enable keyboard (left, right) arrow shortcuts
                dots: true,               //  Display dot navigation
                fluid: true              //  Support responsive design. May break non-responsive designs
            });
        });
    </script>

    <%-- <script type="text/javascript">jssor_1_slider_init();</script>--%>
    <%}
        else
        { %>
    <asp:Repeater runat="server" ID="rptBanner">
        <ItemTemplate>
            <%if (IsHorizontal)
                { %>

            <div class="link_edit" style='width: <%# Width(IsHorizontal, isheight, b++, Eval("Width")).ToString().Replace(",", ".")+"%" %>'>
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

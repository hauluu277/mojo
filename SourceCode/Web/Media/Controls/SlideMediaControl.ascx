<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SlideMediaControl.ascx.cs" Inherits="MediaFeature.UI.SlideMediaControl" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<link href="../../Data/Sites/113/skins/framework/css/slick.css" rel="stylesheet" />
<link href="../../Data/Sites/113/skins/framework/css/slick-theme.css" rel="stylesheet" />
<div class="breadcrumb" style="display:none">
    <div class="breadcrumb-item">
        <asp:Label ID="lblDictionaryTitle" runat="server" ViewStateMode="Disabled" EnableViewState="false"></asp:Label>
    </div>
    <div class="form-search" style="display: none">
        <div class="search-wrapper cf">
            <input type="text" id="txtSearch2" runat="server" ViewStateMode="Disabled" EnableViewState="false" placeholder="Search here...">
            <button id="btnSearch2" runat="server" ViewStateMode="Disabled" EnableViewState="false" onserverclick="btnSearch2_Click">Search</button>
        </div>
    </div>
</div>
<div class="back-han-gallery">
<div class="all-list han-all-list container">
    <div class="gallery">
        <asp:HyperLink ID="hplTitle" runat="server" ViewStateMode="Disabled" EnableViewState="false"></asp:HyperLink>
    </div>
    <ul class="trung-tam-gallery-slider">
        <asp:Repeater ID="dtlData" runat="server" ViewStateMode="Disabled" EnableViewState="false">
            <ItemTemplate>
                <li>
                    <div class="main-data">
                        <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                            <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                        </a>
                        <div class="des-item">
                            <span class="text-left">
    <%--                            <span style="font-size: 12px; color: #a2a2a2;" id="createDate_<%#Eval("ItemID") %>">
                                    <%#Eval("CreatedDate") %>
                                </span>--%>
                                <span class="des-fix">
                                    <%#Eval("NameGroup") %>
                                </span>
                            </span>
                        </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
    </div>
<asp:Panel ID="pnlDonViPager" runat="server" ViewStateMode="Disabled" EnableViewState="false" CssClass="ArticlePager">
    <portal:mojoCutePager ID="pgrDanhBa" runat="server" ViewStateMode="Disabled" EnableViewState="false" />
</asp:Panel>
<asp:Label ID="DanhBanull" runat="server" ViewStateMode="Disabled" EnableViewState="false" Visible="false"></asp:Label>
<script src="https://code.jquery.com/jquery.js"></script>
<script src="../../Data/Sites/113/skins/framework/js/Gallery-Image-fillter-Filterizr/slick.js"></script>
<script type="text/javascript">
    $('.trung-tam-gallery-slider').slick({
        dots: true,
        slidesToShow: 3,
        slidesToScroll: 3,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    infinite: true,
                    dots: false
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
        });
    </script>


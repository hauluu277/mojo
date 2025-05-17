<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LoadGold_Price.ascx.cs" Inherits="Gold_PriceFeatures.UI.LoadGold_Price" %>
<%@ Import Namespace="Gold_PriceFeatures.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<style>
    

.swiper-slide.gold-item {
    background: #f5f5f5;
    border-radius: 4px;
    padding: 8px 12px;
    font-size: 18px !important;
    white-space: nowrap;
    margin-right: 10px;
    flex-direction: row !important;
    height: 50px !important;
}
.mySwiper-gold{
    height : 50px !important;
    width: 100%;
}
.gold-item > strong{
    font-size : 18px !important;
}
</style>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper blogmodule">
                <portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
                    
                    <div class="modulecontent">
                        <asp:Panel ID="pnlPostList" runat="server">
                            <asp:Literal ID="litModuleLinks" runat="server" EnableViewState="false" />
                            <div class="swiper-container swiper-container-gold">

                                <swiper-container class="mySwiper-gold">
                                    <asp:Repeater ID="rptQuestion" runat="server">
                                        <itemtemplate>
                                            <swiper-slide class="gold-item">
    <a href="/gia-vang">
        <strong>Giá vàng <%# Eval("TenLoaiVang") %></strong>
    </a>
    &nbsp;
    <strong>Mua :</strong> <%# Eval("GiaMuaHomNay") %> triệu đồng/lượng &nbsp;
    <strong>Bán :</strong> <%# Eval("GiaBanHomNay") %> triệu đồng/lượng
</swiper-slide>
                                        </itemtemplate>
                                    </asp:Repeater>
                                </swiper-container>
                            </div>
                        </asp:Panel>


                    </div>
                    <div class="cleared">
                    </div>
                </portal:mojoPanel>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

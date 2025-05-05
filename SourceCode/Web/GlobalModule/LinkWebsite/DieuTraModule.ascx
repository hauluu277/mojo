<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DieuTraModule.ascx.cs" Inherits="LinkWebsiteFeature.UI.DieuTraModule" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper blogmodule">
                <portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
                <div class="dieutrathongke">
                    <div class="container">
                        <div class="cleared">
                            <div>
                                <div class="tieude-dieutra">
                                    <asp:Label ID="lblDieuTraThongKe" runat="server"></asp:Label>
                                </div>
                                <div>
                                    <fieldset style="border: 1px solid white; width: 100%; border-radius: 5px;">
                                        <legend class="tieude-tongdieutra">
                                            <asp:Label ID="lblTongDieuTra" runat="server"></asp:Label>
                                        </legend>
                                        <div class="tong-dk">
                                            <asp:Repeater ID="rptTongDieuTra" runat="server">
                                                <HeaderTemplate>
                                                    <ul>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="tong-dieu-tra class-pd">
                                                        <a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>"  target="<%#Convert.ToBoolean( Eval("TargetBlank")) == true ?"_blank":"" %>">
                                                            <div class="bg-color-tongdieutra">
                                                                <div class="tongdieutra-img">
                                                                    <img src="<%#Eval("PathIMG") %>" alt="<%#Eval("Name") %>" />
                                                                </div>
                                                                <div class="tongdieutra-text">
                                                                    <a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>"  target="<%#Convert.ToBoolean( Eval("TargetBlank")) == true ?"_blank":"" %>">
                                                                        <%# mojoPortal.Business.WebHelpers.Ultilities.LoadTitle(Eval("Name"),Eval("SubName")) %>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </a>
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate></ul></FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div>
                                <div class="linhvuc-dt">
                                    <asp:Label ID="lblLinhVucDieuTra" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12">
                                    <asp:Repeater ID="rptLinhVuc" runat="server">
                                        <ItemTemplate>
                                            <div class="linhvuc-col">
                                                <div class="linhvuc-col-left">
                                                    <a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>" target="<%#Convert.ToBoolean( Eval("TargetBlank")) == true ?"_blank":"" %>">
                                                        <img src="<%#Eval("PathIMG") %>" />
                                                    </a>
                                                </div>
                                                <div class="linhvuc-col-right">
                                                    <a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>"  target="<%#Convert.ToBoolean( Eval("TargetBlank")) == true ?"_blank":"" %>">
                                                        <%# mojoPortal.Business.WebHelpers.Ultilities.LoadTitle(Eval("Name"),Eval("SubName")) %>
                                                    </a>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>


<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MediaCategoryModule.ascx.cs" Inherits="MediaCategoryFeature.UI.MediaCategoryModule" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper blogmodule">
                <portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
                <div class="cleared bg-hoidap-tc">
                    <div class="tieude-hoidap-tc">
                    <asp:Label ID="lblTitleCategory" runat="server"></asp:Label>
                        </div>
                    <div class="audio-hoidap-tc">
                        <asp:Literal ID="literMedia" runat="server"></asp:Literal>
                    </div>
                </div>
            </asp:Panel>
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

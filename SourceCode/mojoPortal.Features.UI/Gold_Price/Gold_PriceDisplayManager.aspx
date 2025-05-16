<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Gold_PriceDisplayManager.aspx.cs" Inherits="Gold_PriceFeatures.UI.Gold_PriceDisplayManager" %>

<%@ Register Src="~/Gold_Price/Controls/Gold_PriceDisplayManagerControls.ascx" TagPrefix="portal" TagName="PostList" %>



<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:ModuleTitleControl
            EditText="Add"
            EditUrl="~/Gold_Price/Gold_Price_DisplayEdit.aspx"
            ShowEditButton="true"
            runat="server"
            ID="TitleControl" />
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
                    <portal:PostList runat="server" id="PostList" />
                </portal:mojoPanel>
                <div class="cleared">
                </div>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>

</asp:Content>

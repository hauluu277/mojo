<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageAudio.aspx.cs" Inherits="mojoPortal.Features.UI.Audio.ManageAudio" %>

<%@ Register Src="~/Audio/Controls/CategoriesListAudio.ascx" TagPrefix="portal" TagName="CategoriesListAudio" %>


<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:ModuleTitleControl EditText="Add" Visible="false" EditUrl="~/menu/editpost.aspx" runat="server" ID="TitleControl" />
        <portal:HeadingControl ID="heading" runat="server" />
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
                    <portal:CategoriesListAudio runat="server" id="CategoriesListAudio" />
                </portal:mojoPanel>
                <div class="cleared">
                </div>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</asp:Content>

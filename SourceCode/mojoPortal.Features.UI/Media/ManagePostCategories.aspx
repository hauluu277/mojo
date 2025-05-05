<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ManagePostCategories.aspx.cs" Inherits="MediaFeature.UI.ManagePostCategories" %>

<%@ Register Src="~/Media/Controls/CategoriesList.ascx" TagPrefix="portal" TagName="CategoriesList" %>



<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
<portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
    <portal:ModuleTitleControl EditText="Add" EditUrl="~/menu/editpost.aspx" runat="server" ID="TitleControl" />
    <portal:HeadingControl ID="heading" runat="server" />
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
                <portal:CategoriesList runat="server" id="CategoriesList" />
            </portal:mojoPanel>
            <div class="cleared">
            </div>
        </asp:Panel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:mojoPanel>
</portal:ModulePanel>
    </asp:content>

<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ViewListMediaAlbum.aspx.cs" Inherits="AudioFeature.UI.ViewListMediaAlbum" %>

<%@ Register Namespace="mojoPortal.Web.ForumUI" Assembly="mojoPortal.Features.UI" TagPrefix="forum" %>
<%@ Register Src="~/Audio/Controls/SlideAlbumList.ascx" TagPrefix="portal" TagName="SlideAlbumList" %>

<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
<portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
    <portal:ModuleTitleControl EditText="Add" EditUrl="~/document/editpost.aspx" runat="server" ID="TitleControl" />
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
                <portal:SlideAlbumList runat="server" id="SlideAlbumList" />
            </portal:mojoPanel>
            <div class="cleared">
            </div>
        </asp:Panel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:mojoPanel>
</portal:ModulePanel>
	</asp:content>

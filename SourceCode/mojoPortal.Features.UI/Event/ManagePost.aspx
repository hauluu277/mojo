<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ManagePost.aspx.cs" Inherits="EventFeature.UI.ManagePost" %>

<%@ Register TagPrefix="event" TagName="PostList" Src="~/Event/Controls/PostList.ascx" %>
<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
<portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
    <portal:ModuleTitleControl EditText="Add" EditUrl="~/event/editpost.aspx" runat="server" ID="TitleControl" />
    <portal:HeadingControl ID="heading" runat="server" />
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
                <event:PostList ID="eventList" runat="server" />
            </portal:mojoPanel>
            <div class="cleared">
            </div>
        </asp:Panel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:mojoPanel>
</portal:ModulePanel>
</asp:content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="AnnouncementFeatures.UI.Manage" %>

<%@ Register Src="~/Announcement/Control/ManageControl.ascx" TagPrefix="portal" TagName="ManageControl" %>



<%--<%@ Register Src="~/Admission/Control/ManageControl.ascx" TagPrefix="portal" TagName="ManageControl" %>--%>


<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
<portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
     <portal:HeadingControl ID="heading" runat="server" />
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
<portal:ManageControl runat="server" id="ManageControl" />
        </portal:mojoPanel>
    </portal:ModulePanel>
    </asp:content>

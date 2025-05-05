<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false"
    CodeBehind="ViewList.aspx.cs" Inherits="AnnouncementFeatures.UI.ViewList" %>

<%@ Register Src="~/Announcement/Control/AnnouncementRecenlist.ascx" TagPrefix="portal" TagName="AnnouncementRecenlist" %>


<%--<%@ Register Src="~/Admission/Control/RecentListControl.ascx" TagPrefix="portal" TagName="RecentListControl" %>--%>



<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper articlemodule">
            <portal:ModuleTitleControl ID="moduleTitle" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
                <div class="modulecontent">
                   <%-- <portal:RecentListControl runat="server" ID="RecentListControl" />--%>
                    <portal:AnnouncementRecenlist runat="server" ID="AnnouncementRecenlist" />
                </div>
            </portal:mojoPanel>
            <div class="cleared">
            </div>
        </asp:Panel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:mojoPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
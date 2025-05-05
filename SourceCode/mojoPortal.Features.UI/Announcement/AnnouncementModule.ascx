<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnnouncementModule.ascx.cs" Inherits="AnnouncementFeatures.UI.AnnouncementModule" %>
<%@ Register Src="~/Announcement/Control/AnnouncementRecenlist.ascx" TagPrefix="portal" TagName="AnnouncementRecenlist" %>






<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <div class="modulecontent">
                <portal:AnnouncementRecenlist runat="server" id="AnnouncementRecenlist" />
            </div>
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:ModulePanel>
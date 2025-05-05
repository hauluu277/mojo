<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoListModule.ascx.cs" Inherits="VideoIntroduceFeatures.UI.VideoListModule" %>
<%@ Register Src="~/VideoIntroduce/Controls/VideoListControl.ascx" TagPrefix="portal" TagName="VideoListControl" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
            <portal:ModuleTitleControl ID="TitleControl" runat="server" />
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
                <div class="modulecontent">
                    <portal:VideoListControl runat="server" id="VideoListControl" />
                </div>
            </portal:mojoPanel>
            <div class="cleared">
            </div>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

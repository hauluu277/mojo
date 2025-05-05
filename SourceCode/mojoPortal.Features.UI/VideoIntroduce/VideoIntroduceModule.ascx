<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoIntroduceModule.ascx.cs" Inherits="VideoIntroduceFeatures.UI.VideoIntroduceModule" %>
<%@ Register Src="~/VideoIntroduce/Controls/VideoIntroduceRecenlist.ascx" TagPrefix="portal" TagName="VideoIntroduceRecenlist" %>



<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <div class="modulecontent">
                <portal:VideoIntroduceRecenlist runat="server" ID="VideoIntroduceRecenlist" />
            </div>
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:ModulePanel>

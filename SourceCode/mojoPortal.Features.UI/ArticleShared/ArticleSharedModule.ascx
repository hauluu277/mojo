<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleSharedModule.ascx.cs" Inherits="ArticleFeature.UI.ArticleSharedModule" %>
<%@ Register Src="~/ArticleShared/Controls/ArticleSharedControl.ascx" TagPrefix="portal" TagName="ArticleSharedControl" %>

<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <portal:ArticleSharedControl runat="server" id="ArticleSharedControl" />
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

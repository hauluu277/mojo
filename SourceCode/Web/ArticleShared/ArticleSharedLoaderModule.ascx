<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleSharedLoaderModule.ascx.cs" Inherits="ArticleFeature.UI.ArticleSharedLoaderModule" %>
<%@ Register Src="~/ArticleShared/Controls/ArticleSharedLoaderControl.ascx" TagPrefix="portal" TagName="ArticleSharedLoaderControl" %>
<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <portal:ArticleSharedLoaderControl runat="server" id="ArticleSharedLoaderControl" />
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

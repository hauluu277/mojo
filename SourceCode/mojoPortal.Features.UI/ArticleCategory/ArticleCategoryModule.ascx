<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleCategoryModule.ascx.cs" Inherits="ArticleFeature.UI.ArticleCategoryModule" %>
<%@ Register Src="~/ArticleCategory/Controls/ArticleCategoryControl.ascx" TagPrefix="portal" TagName="ArticleCategoryControl" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper blogmodule">
                <portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
                    <div class="modulecontent">
                        <portal:ArticleCategoryControl runat="server" id="ArticleCategoryControl" />
                    </div>
                </portal:mojoPanel>
                <div class="cleared">
                </div>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

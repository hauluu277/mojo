<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleCongThanhVienModule.ascx.cs" Inherits="ArticleFeature.UI.ArticleCongThanhVienModule" %>
<%@ Register Src="~/Article/Controls/RecentListCongThanhVien.ascx" TagPrefix="portal" TagName="RecentListCongThanhVien" %>

<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <div class="modulecontent">
                <asp:Panel ID="NormalMode" CssClass="NormalMode" runat="server">
                    <portal:RecentListCongThanhVien runat="server" id="recentList" />
                </asp:Panel>
            </div>
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

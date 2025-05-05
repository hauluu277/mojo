<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ManageArticle.aspx.cs" Inherits="ArticleFeature.UI.ManageArticle" %>

<%@ Register Src="~/Article/Controls/ManageArticleControl.ascx" TagPrefix="portal" TagName="ManageArticleControl" %>



<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
            <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" />
            <portal:AdminCrumbSeparator ID="litLinkSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
            <asp:HyperLink ID="lnkAdminArtile" runat="server" CssClass="breadcrumbs" NavigateUrl="~/Admin/Article/ArticleMenu.aspx" />
            <portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
            <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" />
        </portal:AdminCrumbContainer>
        <portal:ModuleTitleControl EditText="Add" EditUrl="~/article/postarticle.aspx" runat="server" ID="TitleControl" />
        <portal:HeadingControl ID="heading" runat="server" />
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
                    <portal:ManageArticleControl runat="server" id="ManageArticleControl" />
                </portal:mojoPanel>
                <div class="cleared">
                </div>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</asp:Content>

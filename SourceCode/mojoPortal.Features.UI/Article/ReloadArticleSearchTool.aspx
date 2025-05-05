<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReloadArticleSearchTool.aspx.cs" Inherits="ArticleFeature.UI.ReloadArticleSearchTool" %>

<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
<portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
     <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
      <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="litLinkSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
      <asp:HyperLink ID="lnkAdminArtile" runat="server" CssClass="breadcrumbs" NavigateUrl="~/Admin/Article/ArticleMenu.aspx"  /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
      <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" />
    </portal:AdminCrumbContainer>
    <portal:ModuleTitleControl EditText="Add" EditUrl="~/article/postarticle.aspx" runat="server" ID="TitleControl" />
     <portal:HeadingControl ID="heading" runat="server" />
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
    </portal:mojoPanel>
</portal:ModulePanel>
	</asp:content>

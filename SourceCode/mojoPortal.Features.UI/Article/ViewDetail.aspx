<%@ Page Language="c#" CodeBehind="ViewDetail.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" Inherits="ArticleFeature.UI.ViewDetail" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/Article/Controls/ArticleViewSite.ascx" TagPrefix="portal" TagName="ArticleViewSite" %>


<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" />
            <portal:ArticleViewSite runat="server" id="ArticleViewSite" />
            <mp:CornerRounderBottom ID="cbottom1" runat="server" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

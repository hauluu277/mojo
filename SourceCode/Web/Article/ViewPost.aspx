<%@ Page Language="c#" CodeBehind="ViewPost.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" Inherits="ArticleFeature.UI.ViewPost" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="article" TagName="ArticleView" Src="~/Article/Controls/ArticleView.ascx" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" />
            <article:ArticleView id="articleView" runat="server" />
            <mp:CornerRounderBottom ID="cbottom1" runat="server" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

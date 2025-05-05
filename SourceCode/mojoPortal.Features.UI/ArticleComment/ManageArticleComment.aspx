<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ManageArticleComment.aspx.cs" Inherits="ArticleFeature.UI.ManageArticleComment" %>

<%@ Register Src="~/ArticleComment/Control/ManageCommentControl.ascx" TagPrefix="portal" TagName="ManageCommentControl" %>

<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
<portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
    <portal:ModuleTitleControl EditText="Add" EditUrl="~/article/editpost.aspx" runat="server" ID="TitleControl" />
     <portal:HeadingControl ID="heading" runat="server" />
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
            <portal:ManageCommentControl runat="server" id="ManageCommentControl" />
            </portal:mojoPanel>
            <div class="cleared">
            </div>
        </asp:Panel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:mojoPanel>
</portal:ModulePanel>
    
	</asp:content>

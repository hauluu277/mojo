<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false"
    CodeBehind="ViewTag.aspx.cs" Inherits="ArticleFeature.UI.ViewTag" %>

<%@ Register TagPrefix="uc1" TagName="ViewTagList" Src="~/Article/Controls/ViewByTag.ascx" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper articlemodule">
            <%--<portal:ModuleTitleControl ID="moduleTitle" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />--%>
            <h2 class="art-postheader moduletitle">
                <asp:Label runat="server" ID="lblTagName" CssClass="art-postheadericon"></asp:Label>
            </h2>
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">

                <div class="modulecontent">
                    <uc1:ViewTagList ID="articleListByTag" runat="server" />
                </div>
            </portal:mojoPanel>
            <div class="cleared">
            </div>
        </asp:Panel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:mojoPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

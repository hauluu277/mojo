<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="ListVideoIntroduce.aspx.cs" Inherits="VideoIntroduceFeatures.UI.ListVideoIntroduce" %>

<%@ Import Namespace="mojoPortal.Web" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Register Src="~/VideoIntroduce/Controls/VideoIntroduceRecenlist.ascx" TagPrefix="portal" TagName="VideoIntroduceRecenlist" %>

<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:ModuleTitleControl EditText="Add" EditUrl="~/menu/editpost.aspx" runat="server" ID="TitleControl" />
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
                    <portal:VideoIntroduceRecenlist runat="server" ID="VideoIntroduceRecenlist" />
                </portal:mojoPanel>
                <div class="cleared">
                </div>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ViewListMediaGroup.aspx.cs" Inherits="MediaFeature.UI.ViewListMediaGroup" %>

<%@ Register Src="~/Media/Controls/MediaGroupList.ascx" TagPrefix="portal" TagName="MediaGroupList" %>


<asp:content contentplaceholderid="leftContent" id="MPLeftPane" runat="server" />
<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper articlemodule">
            <portal:ModuleTitleControl ID="moduleTitle" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
                <div class="modulecontent">
                <portal:MediaGroupList runat="server" id="MediaGroupList" />
                </div>
            </portal:mojoPanel>
            <div class="cleared">
            </div>
        </asp:Panel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:mojoPanel>
</asp:content>
<asp:content contentplaceholderid="rightContent" id="MPRightPane" runat="server" />
<asp:content contentplaceholderid="pageEditContent" id="MPPageEdit" runat="server" />


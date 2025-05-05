<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DuThaoVanBanModule.ascx.cs" Inherits="DuThaoVanBanFeature.UI.DuThaoVanBanModule" %>
<%@ Register Src="~/DuThaoVanBan/Controls/RecentList.ascx" TagPrefix="portal" TagName="RecentList" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper DuThaoVanBanFeature">
<%--<portal:ModuleTitleControl runat="server" id="TitleControl" />--%>
<portal:ModuleTitleControlCustom ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

<portal:RecentList runat="server" id="RecentList" />

</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
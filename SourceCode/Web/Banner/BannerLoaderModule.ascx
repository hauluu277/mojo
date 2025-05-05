<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="BannerLoaderModule.ascx.cs" Inherits="BannerFeature.UI.BannerLoaderModule" %>
<%@ Register Src="~/Banner/Controls/BannerLoaderView.ascx" TagPrefix="portal" TagName="BannerLoaderView" %>


<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Banner">
<portal:ModuleTitleControl runat="server" id="TitleControl" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

    <portal:BannerLoaderView runat="server" id="BannerLoaderView" />

</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

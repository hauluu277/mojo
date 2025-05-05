<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="BannerModule.ascx.cs" Inherits="BannerFeature.UI.BannerModule" %>
<%@ Register Src="~/Banner/Controls/BannerView.ascx" TagPrefix="portal" TagName="BannerView" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server" CssClass="outerwrap">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Banner">
<portal:ModuleTitleControl runat="server" id="TitleControl" />

<portal:BannerView runat="server" id="BannerView" />

<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

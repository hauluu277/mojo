<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="BannerUseModule.ascx.cs" Inherits="BannerFeature.UI.BannerUseModule" %>
<%@ Register Src="~/Banner/Controls/BannerUse.ascx" TagPrefix="portal" TagName="BannerUse" %>


<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper BannerUseModule">
<portal:ModuleTitleControl runat="server" id="TitleControl" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

    <portal:BannerUse runat="server" id="BannerUse" />

</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
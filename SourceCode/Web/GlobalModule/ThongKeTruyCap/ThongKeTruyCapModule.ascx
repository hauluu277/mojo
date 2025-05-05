<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ThongKeTruyCapModule.ascx.cs" Inherits="ThongKeTruyCapFeature.UI.ThongKeTruyCapModule" %>
<%@ Register Src="~/GlobalModule/ThongKeTruyCap/Controls/ThongKeTruyCap.ascx" TagPrefix="portal" TagName="ThongKeTruyCap" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper statitics">
        <portal:ModuleTitleControl runat="server" ID="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <portal:ThongKeTruyCap runat="server" id="thongKeTruyCapControl" />
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

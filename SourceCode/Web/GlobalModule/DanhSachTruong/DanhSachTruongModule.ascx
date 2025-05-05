<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DanhSachTruongModule.ascx.cs" Inherits="DanhSachTruongFeature.UI.DanhSachTruongModule" %>
<%@ Register Src="~/GlobalModule/DanhSachTruong/Controls/DanhSachTruongList.ascx" TagPrefix="portal" TagName="DanhSachTruongList" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Document">
        <portal:ModuleTitleControl runat="server" ID="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <portal:DanhSachTruongList runat="server" id="DanhSachTruongList" />
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DocumentHomeModule.ascx.cs" Inherits="DocumentFeature.UI.DocumentHomeModule" %>
<%@ Register Src="~/Document/Controls/DocumentHome.ascx" TagPrefix="portal" TagName="DocumentHome" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Document">
        <portal:ModuleTitleControl runat="server" id="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <portal:DocumentHome runat="server" id="DocumentHome" />
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
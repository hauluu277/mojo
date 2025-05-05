<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MediaNewModule.ascx.cs" Inherits="MediaFeature.UI.MediaNewModule" %>
<%@ Register Src="~/Media/Controls/MediaNewControl.ascx" TagPrefix="portal" TagName="MediaNewControl" %>


<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Document">
        <portal:ModuleTitleControl runat="server" ID="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <portal:MediaNewControl runat="server" id="MediaNewControl" />
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

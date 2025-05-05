<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EventHotGroupModule.ascx.cs" Inherits="EventFeature.UI.EventHotGroupModule" %>
<%@ Register Src="~/Event/Controls/EventHotListGroup.ascx" TagPrefix="portal" TagName="EventHotListGroup" %>




<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Document">
        <portal:ModuleTitleControl runat="server" id="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <portal:EventHotListGroup runat="server" id="EventHotListGroup" />
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

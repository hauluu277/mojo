<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="IframeModule.ascx.cs" Inherits="mojoPortal.Features.UI.IframeModule" %>
<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <asp:Panel ID="pnlWrapper" runat="server" CssClass="panelwrapper IframeModule">
            <portal:ModuleTitleControl runat="server" ID="TitleControl" UseHeading="false" />
            <asp:Panel ID="pnlIframeModule" runat="server" CssClass="modulecontent">
                <asp:Literal ID="litFrame" runat="server" />

            </asp:Panel>
        </asp:Panel>
    </portal:InnerWrapperPanel>
</portal:ModulePanel>


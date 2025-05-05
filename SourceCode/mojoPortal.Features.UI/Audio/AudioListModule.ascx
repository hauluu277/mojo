<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AudioListModule.ascx.cs" Inherits="AudioFeature.UI.AudioListModule" %>

<%@ Import Namespace="mojoPortal.Features" %>
<%@ Register Src="~/Audio/Controls/ListAudioControl.ascx" TagPrefix="portal" TagName="ListAudioControl" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <portal:ListAudioControl runat="server" id="listAudioControl" />
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>

    <%--end Dánh ách lãnh đạo--%>



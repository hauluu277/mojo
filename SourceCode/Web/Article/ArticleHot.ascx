<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleHot.ascx.cs" Inherits="ArticleFeature.UI.ArticleHotModule" %>
<%@ Register Src="~/Article/Controls/HotList.ascx" TagPrefix="portal" TagName="HotList" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ArticleHot">
        <portal:ModuleTitleControl  runat="server" id="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <portal:HotList runat="server" id="HotList" />
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HomeListModule.ascx.cs" Inherits="ArticleFeature.UI.HomeListModule" %>
<%@ Register Src="~/Article/Controls/HomeList.ascx" TagPrefix="portal" TagName="HomeList" %>


<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server" CssClass="outerwrap">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ArticleHot">
        <portal:ModuleTitleControl EditUrl="~/ArticleHot/ArticleHotEdit.aspx" runat="server" id="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <portal:HomeList runat="server" id="HomeList" />
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
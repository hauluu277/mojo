<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HomeArticleHotModule.ascx.cs" Inherits="ArticleFeature.UI.HomeArticleHotModule" %>
<%@ Register Src="~/GlobalModule/ArticleHot/Controls/HomeArticleHot.ascx" TagPrefix="portal" TagName="HomeArticleHot" %>


<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ArticleHot">
        <portal:ModuleTitleControl EditUrl="~/ArticleHot/ArticleHotEdit.aspx" runat="server" id="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
            <portal:HomeArticleHot runat="server" id="HomeArticleHot" />
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
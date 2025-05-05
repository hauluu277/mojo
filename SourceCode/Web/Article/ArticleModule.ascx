<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleModule.ascx.cs" Inherits="ArticleFeature.UI.ArticleModule" %>
<%@ Register TagPrefix="article" TagName="RecentList" Src="~/Article/Controls/RecentList.ascx" %>
<%--<%@ Register TagPrefix="article" TagName="PostListAccordion" Src="~/Article/Controls/PostListAccordion.ascx" %>--%>
<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
<%--        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <asp:Panel ID="NormalMode" CssClass="NormalMode" runat="server">
                    <article:RecentList ID="recentList" runat="server" />
                </asp:Panel>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>--%>
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <div class="modulecontent">
                <asp:Panel ID="NormalMode" CssClass="NormalMode" runat="server">
                    <article:recentlist id="recentList" rel="recentList" runat="server" />
                </asp:Panel>
                <%--<asp:Panel ID="AccordionMode" runat="server">
                    <article:postlistaccordion id="postListAccordion" rel="postListAccordion" cssclass="postListAccordion" runat="server" />
                </asp:Panel>--%>
            </div>
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EventModule.ascx.cs" Inherits="EventFeature.UI.EventModule" %>
<%@ Register TagPrefix="Event" TagName="recentlist" Src="~/Event/Controls/RecentList.ascx" %>
<%--<%@ Register TagPrefix="Event" TagName="PostListAccordion" Src="~/Event/Controls/PostListAccordion.ascx" %>--%>
<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
<%--        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <asp:Panel ID="NormalMode" CssClass="NormalMode" runat="server">
                    <Event:RecentList ID="recentList" runat="server" />
                </asp:Panel>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>--%>
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="EventContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <div class="modulecontent">
                <asp:Panel ID="NormalMode" CssClass="NormalMode" runat="server">
                    <Event:recentlist id="recentList" rel="recentList" runat="server" />
                </asp:Panel>
                <%--<asp:Panel ID="AccordionMode" runat="server">
                    <Event:postlistaccordion id="postListAccordion" rel="postListAccordion" cssclass="postListAccordion" runat="server" />
                </asp:Panel>--%>
            </div>
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

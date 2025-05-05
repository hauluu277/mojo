<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
    CodeBehind="WebBuilderMenu.aspx.cs" Inherits="mojoPortal.Web.AdminUI.WebBuilderMenu" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="litLinkSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" />
    </portal:AdminCrumbContainer>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper adminmenu">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <ul class="simplelist">
                        <%--Tạo website--%>
                        <li id="liBuilderWebsite" runat="server">
                            <asp:HyperLink ID="lnkBuilderWebsite" runat="server" CssClass="lnkWebBuilder" />
                        </li>
                        <%--Danh sách template website--%>
                        <li id="liListTemplate" runat="server">
                            <asp:HyperLink ID="lnkListTemplate" runat="server" CssClass="lnkSkinSetting" />
                        </li>
                        <%--Danh sách danh mục mặc định của web builder--%>
                        <li style="display:none">
                            <asp:HyperLink ID="lnkCategoryTemplate" runat="server" CssClass="linkArticleMenu"></asp:HyperLink>
                        </li>
                        <asp:Literal ID="litSupplementalLinks" runat="server" />
                    </ul>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

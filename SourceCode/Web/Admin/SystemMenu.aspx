<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
    CodeBehind="SystemMenu.aspx.cs" Inherits="mojoPortal.Web.AdminUI.SystemMenu" %>

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
                        <%-- thiết lập website --%>
                        <li id="liSiteSettings" runat="server">
                            <asp:HyperLink ID="lnkSiteSettings" runat="server" CssClass="lnkSiteSettings" />
                        </li>
                        <%-- danh sách site --%>
                        <li id="liSiteList" runat="server">
                            <asp:HyperLink ID="lnkSiteList" runat="server" CssClass="lnkSiteList" />
                        </li>
                        <%-- xây dựng website --%>
                        <li id="liSettingWebBuilder" runat="server">
                            <asp:HyperLink ID="lnkSettingWebBuilder" runat="server" CssClass="lnkWebBuilder" />
                        </li>

                        <%-- quản lý vai trò --%>
                        <li id="liRoleAdmin" runat="server">
                            <asp:HyperLink ID="lnkRoleAdmin" runat="server" CssClass="lnkRoleAdmin" />
                        </li>
                        <%-- quyền hạn --%>
                        <li id="liPermissions" runat="server">
                            <asp:HyperLink ID="lnkPermissionAdmin" runat="server" CssClass="lnkPermissionAdmin" />
                        </li>
                        <%-- danh sách thành viên --%>

                        <li id="liMemberList" runat="server">
                            <asp:HyperLink ID="lnkMemberList" runat="server" CssClass="lnkMemberList" />
                        </li>
                        <%-- thêm người dùng --%>
                        <li id="liAddUser" runat="server">
                            <asp:HyperLink ID="lnkAddUser" runat="server" CssClass="lnkAddUser" />
                        </li>
                        <%-- thêm chỉnh sửa trang --%>
                        <li id="liPageTree" runat="server">
                            <asp:HyperLink ID="lnkPageTree" runat="server" CssClass="lnkPageTree" />
                        </li>
                        <%-- quản lý nội dung --%>

                        <li id="liContentManager" runat="server">
                            <asp:HyperLink ID="lnkContentManager" runat="server" CssClass="lnkContentManager" />
                        </li>
                        <%-- công cụ nâng cao --%>
                        <li id="liAdvancedTools" runat="server">
                            <asp:HyperLink ID="lnkAdvancedTools" runat="server" CssClass="lnkAdvancedTools" />
                        </li>
                        <%-- nhật ký hệ thống --%>
                        <li id="liLogViewer" runat="server">
                            <asp:HyperLink ID="lnkLogViewer" runat="server" CssClass="lnkLogViewer" />
                        </li>
                        <%-- thông tin hệ thống --%>
                        <li id="liServerInfo" runat="server">
                            <asp:HyperLink ID="lnkServerInfo" runat="server" CssClass="lnkServerInfo" />
                        </li>
                        <%-- Hoạt động người dùng --%>
                        <li id="liHoatDongNguoiDung" runat="server">
                            <asp:HyperLink ID="hplHoatDongNguoiDung" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- Cấu hình SSO --%>
                        <li id="liSettingSSO" runat="server">
                            <asp:HyperLink ID="linkSettingSSO" runat="server" CssClass="lnkCategoryManager cblink cboxElement" />
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

<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
    CodeBehind="ArticleMenu.aspx.cs" Inherits="mojoPortal.Web.AdminUI.ArticleMenu" %>

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
                        <%--Danh sách tin bài--%>
                        <li id="liListArticle" runat="server">
                            <asp:HyperLink ID="lnkListArticle" runat="server" CssClass="lnkListArticle" />
                        </li>
                        <%--Thống kê tin bài--%>
                        <li id="liStatisticalArticle" runat="server">
                            <asp:HyperLink ID="lnkStatisticalArticle" runat="server" CssClass="lnkStatisticalArticle" />
                        </li>
                        <%--Lịch sử cập nhật tin bài--%>
                        <li id="liHistoryArticle" runat="server" visible="false">
                            <asp:HyperLink ID="lnkHistoryArticle" runat="server" CssClass="lnkHistoryArticle" />
                        </li>
                        <%--Bình luận tin bài--%>
                        <li id="liCommentArticle" runat="server" visible="false">
                            <asp:HyperLink ID="hplCommentArticle" runat="server" CssClass="lnkHistoryArticle" />
                        </li>
                        <%-- Quản trị chuyên mục tin--%>
                        <li id="liCategoryArticle" runat="server">
                            <asp:HyperLink ID="hplCategoryArticle" runat="server" CssClass="lnkStatisticalArticle" />
                        </li>

                        <%-- Quản trị tin cổng thành viên --%>
                        <li id="liArticleCongThanhVien" runat="server">
                            <asp:HyperLink ID="hplArticleCongThanhVien" runat="server" CssClass="lnkStatisticalArticle" />
                        </li>

                        <%-- Báo cáo tin bài --%>
                        <li id="liBaoCaoTinBai" runat="server">
                            <asp:HyperLink ID="hplBaoCaoTinBai" runat="server" CssClass="lnkStatisticalArticle" />
                        </li>

                        <%-- Thống kê bài viết --%>
                        <li id="liThongKeBaiViet" runat="server">
                            <asp:HyperLink ID="hplThongKeBaiViet" runat="server" CssClass="lnkStatisticalArticle" />
                        </li>

                        <%-- tag tin bài --%>
                        <li id="liDictionary" runat="server">
                            <asp:HyperLink ID="lnkDictionaryManager" runat="server" CssClass="lnkCategoryManager" />
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

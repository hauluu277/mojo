<%@ Page Language="c#" CodeBehind="ManageStatisticThongKeDonVi.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" Inherits="ArticleFeature.UI.ManageStatisticThongKeDonVi" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
            <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="/" Text="Trang chủ" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Admin/AdminMenu.aspx" Text="Quản trị" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator2" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
            <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" Text="Thống kê bài viết cổng thành viên" />
        </portal:AdminCrumbContainer>
        <div id="Box-Thong-Ke-Don-Vi">
        </div>

        <script type="text/javascript">
            $(document).ready(function () {
                CallAjaxLoading("get", "/BaoCaoArea/BaoCaoArticleDonVi/Index", null, true, function (rs) {
                    $('#Box-Thong-Ke-Don-Vi').html(rs)
                });
            });
        </script>
        <script src="https://cdn.amcharts.com/lib/5/index.js"></script>
        <script src="https://cdn.amcharts.com/lib/5/xy.js"></script>
        <script src="https://cdn.amcharts.com/lib/5/themes/Animated.js"></script>
    </portal:ModulePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

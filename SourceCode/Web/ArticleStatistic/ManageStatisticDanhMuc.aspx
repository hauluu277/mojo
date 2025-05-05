<%@ Page Language="c#" CodeBehind="ManageStatisticDanhMuc.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" Inherits="ArticleFeature.UI.ManageStatisticDanhMuc" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
            <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="/" Text="Trang chủ" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Admin/AdminMenu.aspx" Text="Quản trị" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator2" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
            <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" Text="Báo cáo tin bài" />
        </portal:AdminCrumbContainer>
        <div id="Box-Thong-Ke-Danh-Muc">
        </div>
        <script src="https://cdn.amcharts.com/lib/5/index.js"></script>
        <script src="https://cdn.amcharts.com/lib/5/xy.js"></script>
        <script src="https://cdn.amcharts.com/lib/5/themes/Animated.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                CallAjaxLoading("get", "/BaoCaoArea/BaoCaoArticleDanhMuc/Index", null, true, function (rs) {
                    $('#Box-Thong-Ke-Danh-Muc').html(rs)
                });
            });
        </script>

    </portal:ModulePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

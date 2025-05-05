<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="KeKhai.aspx.cs" Inherits="BieuMauThongTinFeatures.UI.KeKhai" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
     <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="/" Text="Trang chủ" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" Text="Biểu mẫu thông tin" />

    </portal:AdminCrumbContainer>
    <div id="content_unit">
    </div>
    <script>
        $(document).ready(function () {
            var urlParams = new URLSearchParams(window.location.search);
            var idBieuMau = urlParams.get('idBieuMau');

            CallAjaxLoading("get", "/BieuMauThongTinArea/BieuMauThongTin/KeKhaiBieuMauThongTin?idBieuMau=" + idBieuMau, null, true, function (rs) {
                $("#content_unit").html(rs);
            });
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />


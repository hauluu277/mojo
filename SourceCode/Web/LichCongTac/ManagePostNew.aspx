<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="ManagePostNew.aspx.cs" Inherits="LichCongTacFeature.UI.ManagePostNew" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="Content1" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="/" Text="Trang chủ" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb active" Text="Lịch công tác" />
    </portal:AdminCrumbContainer>
       <div id="content-lichCongtac">
        
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            CallAjaxLoading("get", "/QLLichLamViecArea/QLLichLamViec/index", null, true, function (rs) {
                $("#content-lichCongtac").html(rs);
            });
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />



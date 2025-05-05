<%@ Page Language="c#" CodeBehind="ManageStatisticDanhMuc.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" Inherits="ArticleFeature.UI.ManageStatisticDanhMuc" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <div id="Box-Thong-Ke-Danh-Muc">
            
        </div>
        <script type="text/javascript">
            $.ajax({
                url: '/BaoCaoArea/BaoCaoArticleDonVi/Index',
                method: 'GET',
                success: function (rs) {
                    $('#Box-Thong-Ke-Danh-Muc').html(rs)
                }
            })
        </script>
    </portal:ModulePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="ManagerPost.aspx.cs" Inherits="WebserviceFeatures.UI.ManagerPost" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:BreadCrumbControl ID="BreadCrumbControl" runat="server" />
    <div id="content_unit">
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            CallAjaxLoading("get", "/DieuTraArea/DieuTra/Index", null, true, function (rs) {
                $("#content_unit").html(rs);
            });
        });
        
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />


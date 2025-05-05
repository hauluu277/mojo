<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="ManageClient.aspx.cs" Inherits="SSOFeatures.UI.ManageClient" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:BreadCrumbControl ID="BreadCrumbControl" runat="server" />
    <div id="content_client">
    </div>

    <style>
        .dropdown-menu {
            width: 200px !important;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            CallAjaxLoading("get", "/clientarea/client/index", null, true, function (rs) {
                $("#content_client").html(rs);
            });
        });

    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />


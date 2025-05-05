<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="LayTinBaiClient.aspx.cs" Inherits="SSOFeatures.UI.LayTinBaiClient" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
     <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:BreadCrumbControl ID="BreadCrumbControl" runat="server" />
    <div id="content_client">
         <div id="report_div" runat="server">

        </div>
    </div>
    <script type="text/javascript">
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />


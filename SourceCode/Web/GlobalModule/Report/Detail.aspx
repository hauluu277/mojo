<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="Detail.aspx.cs" Inherits="ReportFeatures.UI.Detail" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:BreadCrumbControl ID="BreadCrumbControl" runat="server" />
    <div id="report" runat="server">
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />


<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="PageBuilder.aspx.cs" Inherits="mojoPortal.Web.Admin.WebBuilder.PageBuilder" %>

<%-- @ OutputCache Duration="120" VaryByParam="*"  --%>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" ></asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server"></asp:Content>

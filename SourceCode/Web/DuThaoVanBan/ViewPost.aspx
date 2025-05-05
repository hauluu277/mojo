<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false"
    CodeBehind="ViewPost.aspx.cs" Inherits="DuThaoVanBanFeature.UI.ViewPost" %>

<%@ Register Src="~/DuThaoVanBan/Controls/DuThaoView.ascx" TagPrefix="portal" TagName="DuThaoView" %>



<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:DuThaoView runat="server" id="DuThaoView" />
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" >

</asp:Content>


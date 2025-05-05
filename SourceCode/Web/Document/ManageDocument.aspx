<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ManageDocument.aspx.cs" Inherits="DocumentFeature.UI.ManageDocument" %>

<%@ Register Src="~/Document/Controls/ManageControl.ascx" TagPrefix="portal" TagName="ManageControl" %>

<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
<portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
     <portal:BreadCrumbControl ID="BreadCrumbControl" runat="server" />
   <portal:ManageControl runat="server" id="ManageControl" />
</portal:ModulePanel>
    
	</asp:content>

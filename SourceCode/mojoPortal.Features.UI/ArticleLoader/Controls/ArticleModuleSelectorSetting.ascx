<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleModuleSelectorSetting.ascx.cs" Inherits="mojoPortal.Web.UI.ArticleModuleSelectorSetting" %>
<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:CheckBoxList ID="ddlListModule" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

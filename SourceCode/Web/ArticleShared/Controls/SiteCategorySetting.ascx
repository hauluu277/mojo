<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteCategorySetting.ascx.cs" Inherits="ArticleFeature.UI.SiteCategorySetting" %>
<asp:Repeater ID="rptSite" runat="server">
    <ItemTemplate>
        <div class="width100 article-shared-setting">
            <h3>Danh sách chuyên mục thuộc Site - <%#Eval("SiteName") %></h3>
            <asp:HiddenField ID="hdfSiteID" runat="server" Value='<%#Eval("SiteID") %>' />
            <asp:HiddenField ID="hdfCategoryArticle" runat="server" Value='<%#Eval("ArticleCategoryID") %>' />
            <asp:ListBox ID="lboxCategory" Width="100%" Height="250" runat="server"></asp:ListBox>
            </div>
    </ItemTemplate>
</asp:Repeater>

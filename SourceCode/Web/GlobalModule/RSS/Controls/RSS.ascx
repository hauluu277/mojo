<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RSS.ascx.cs" Inherits="RSSFeature.UI.RSS" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:HeadingControl ID="heading" runat="server" />

<div class="width100">
    <h3 class="legend-title">RSS của Sở Giáo dục và Đào tạo tỉnh Bến Tre</h3>
    <div class="module-rss">
        <asp:Repeater runat="server" ID="rptLinkRss">
            <ItemTemplate>
                <div class="d-block">
                    <a  href="<%#Eval("Value") %>"><span class="text-primary"><%#Eval("Text") %></span> - <%#Eval("Value") %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

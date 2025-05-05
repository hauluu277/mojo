<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RSS.ascx.cs" Inherits="RSSFeature.UI.RSS" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:HeadingControl ID="heading" runat="server" />

<div class="">
    <div class="module">
        <asp:Repeater runat="server" ID="rptLinkRss">
            <ItemTemplate>
                <div class="d-block">
                    <a href="<%#Eval("Value") %>"><%#Eval("Text") %>: <%#Eval("Value") %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

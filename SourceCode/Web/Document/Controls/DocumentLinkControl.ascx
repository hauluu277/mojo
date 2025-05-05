<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DocumentLinkControl.ascx.cs" Inherits="DocumentFeature.UI.DocumentLinkControl" %>
<%@ Import Namespace="mojoPortal.Features" %>


<div class="vanban-search">
    <div class="vanban-header">Văn bản quản lý</div>
    <div class="vanban-content">
        <h3>Lĩnh vực</h3>
        <ul>
            <asp:Repeater ID="rptLinhVuc" runat="server">
                <ItemTemplate>
                    <li <%# GetActiveLinhVuc(int.Parse(Eval("ItemID").ToString())) %>>
                        <a href="<%# formartLinhVucUrl(int.Parse(Eval("ItemID").ToString())) %>" title="<%#Eval("Name") %>"><%#Eval("Name") %></a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="vanban-content">
        <h3>Cơ quan ban hành</h3>
        <ul>
            <asp:Repeater ID="rptCoQuanBanHanh" runat="server">
                <ItemTemplate>
                    <li <%# GetActiveCoQuan(int.Parse(Eval("ItemID").ToString())) %>>
                        <a href="<%# formartCoQuanUrl(int.Parse(Eval("ItemID").ToString())) %>" title="<%#Eval("Name") %>"><%#Eval("Name") %></a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="vanban-content">
        <h3>Loại văn bản</h3>
        <ul>
            <asp:Repeater ID="rptLoaiVanBan" runat="server">
                <ItemTemplate>
                    <li <%# GetActiveLoaiVanBan(int.Parse(Eval("ItemID").ToString())) %>>
                        <a href="<%# formartLoaiVBUrl(int.Parse(Eval("ItemID").ToString())) %>" title="<%#Eval("Name") %>"><%#Eval("Name") %></a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>

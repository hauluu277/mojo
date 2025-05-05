<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DocumentLinkControl.ascx.cs" Inherits="DocumentFeature.UI.DocumentLinkControl" %>
<%@ Import Namespace="mojoPortal.Features" %>

<style>
    .vanban-search {
        width: 100%;
        float: left;
    }

    .vanban-header {
        background: #0078d7;
        color: white;
        text-align: center;
        font-size: 20px;
        padding: 10px 0;
        text-transform: uppercase;
        margin-bottom: 1px;
    }

    .vanban-content {
        width: 100%;
        float: left;
    }

        .vanban-content ul {
            list-style: none;
        }

        .vanban-content h3 {
            color: #fff;
            padding-left: 20px;
            padding: 10px;
            background: #439bf1;
            margin: 0;
            margin-bottom: 1px;
            font-size: 16px;
            text-transform: uppercase;
        }

        .vanban-content ul li {
            width: 100%;
            float: left;
            background: #f6f5f5;
            color: white;
            padding: 7px;
            border-bottom: 1px solid white;
            margin-bottom: 1px;
        }

            .vanban-content ul li a {
                color: black;
                font-size: 14px;
            }

            .vanban-content ul li:hover {
                background: #0078d7;
            }

                .vanban-content ul li:hover a {
                    color: white !important
                }

    .vanban-active {
        background: #0078d7 !important;
    }

        .vanban-active a {
            color: white !important;
        }
</style>
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

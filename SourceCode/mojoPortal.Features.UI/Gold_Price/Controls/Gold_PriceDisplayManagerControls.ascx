<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Gold_PriceDisplayManagerControls.ascx.cs" Inherits="Gold_PriceFeatures.UI.Gold_PriceDisplayManagerControls" %> 
<%@ Import Namespace="Gold_PriceFeatures.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .author {
        padding: 0 5px;
        font-size: 11px;
        color: #C0C0C0;
        font-style: italic;
        font-weight: bold;
    }

    .myTable th {
        background-color: #eeeeee;
        color: #444444;
        padding: 5px;
        text-align: left;
        border: none;
        border-collapse: collapse;
    }

    .myTable tr td {
        border: 1px solid rgba(128, 128, 128, 0.19);
        vertical-align: top;
        line-height: 20px;
        border-collapse: collapse;
        padding: 5px;
    }

    .myTable tr:nth-child(even) {
        background: rgb(250,250,250);
    }

    .myTable tr:nth-child(odd) {
        background: #FFF;
    }

    .tableheader {
        width: 100%;
        margin: 0 auto;
    }

    table {
        border-collapse: collapse;
    }

    .myTable select {
        width: 250px;
    }
</style>
<fieldset runat="server" style="display: none">
    <legend id="legendQuestionAnswer" runat="server"></legend>
</fieldset>
<div class="panel panel-border-title">
    <div class="panel-heading">
        <div>Tiêu chí tìm kiếm</div>
    </div> 
</div>
<div id="toolbar-box">
    <div class="tool-btn"> 
    </div>
</div>
<asp:Panel ID="pnlPostList" runat="server">
    <asp:Repeater ID="rptQuestion" runat="server" SkinID="Blog">
        <HeaderTemplate>
            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%; text-align: center">
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" />
                        </th>
                        <th class="tbl-header" style="width: 35%">
                            Tên loại vàng
                        </th>
                        <th class="tbl-header">
                            Giá bán hôm nay
                        </th>
                        <th class="tbl-header">
                            Giá mua hôm nay
                        </th>
                        <th class="tbl-header">
                            Giá bán hôm trước
                        </th>
                        <th class="tbl-header">
                            Giá mua hôm trước
                        </th>  
                        <th class="tbl-header">
                            Thời gian cập nhật
                        </th> 
                        <th style="width: 5%" class="tbl-header"></th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="text-align: center">
                    <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("ItemID") %>' Visible="false"></asp:Literal>
                    <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                </td>
                <td>
                    <div class="name-title">
                        <%#Eval("TenLoaiVang") %>
                    </div>
                </td>
                <td style="text-align: center">
                    <%#Eval("GiaBanHomNay") %>
                </td>
                <td style="text-align: center">
                    <%#Eval("GiaMuaHomNay") %>

                </td>
                <td style="text-align: center">
                    <%#Eval("GiaBanHomTruoc") %>
                </td>
                <td style="text-align: center">
                    <%#Eval("GiaMuaHomTruoc") %>

                </td> 
                <td style="text-align: center">
                    <%#Eval("UpdateDate") %> 
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
        </table>
        </FooterTemplate>
    </asp:Repeater>  
</asp:Panel>

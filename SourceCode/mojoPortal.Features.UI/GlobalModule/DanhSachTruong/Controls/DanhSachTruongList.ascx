<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DanhSachTruongList.ascx.cs" Inherits="DanhSachTruongFeature.UI.DanhSachTruongList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:HeadingControl ID="heading" runat="server" />

<div class="col-sm-12">
    <div class="module">
        <div class="module-table-body">
            <asp:Repeater ID="rptArticles" runat="server" SkinID="Article">
                <HeaderTemplate>
                    <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                        <tr>
                            <th>STT</th>
                            <th class="tbl-header">Tên trường
                            </th>
                            <th class="tbl-header">Địa chỉ
                            </th>
                            <th class="tbl-header">Website</th>
                        </tr>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#rptArticles.Items.Count + 1%>
                        </td>
                        <td>
                            <%#Eval("Code") %>
                        </td>
                        <td>
                            <%#Eval("Sumary") %>
                        <td>
                            <%#Eval("Description") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

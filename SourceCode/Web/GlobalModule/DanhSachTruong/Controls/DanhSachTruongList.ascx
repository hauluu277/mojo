<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DanhSachTruongList.ascx.cs" Inherits="DanhSachTruongFeature.UI.DanhSachTruongList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:HeadingControl ID="heading" runat="server" />
<div class="col-sm-12">
    <div class="module">
        <div class="module-table-body">
            <asp:Repeater ID="rptArticles" runat="server" SkinID="Article">
                <HeaderTemplate>
                    <table class="table table-striped table-bordered table-hover table-condensed ds-truong" style="width: 100%">
                        <tr>
                            <th>STT</th>
                            <th></th>
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
                            <img alt="<%#Eval("Name") %>" src="<%#Eval("PathIMG") %>" />
                        </td>
                        <td>
                            <%#Eval("Name") %>
                        </td>
                        <td>
                            <%#Eval("Sumary") %>
                        <td>
                            <a href="<%#Eval("Description") %>" target="_blank"><%#Eval("Description") %></a>
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

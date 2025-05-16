<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Gold_Price_DisplayModuleControls.ascx.cs" Inherits="mojoPortal.Features.UI.Gold_Price.Gold_Price_DisplayModuleControls" %>

<asp:Panel ID="pnlGiaVang" runat="server" CssClass="container">
    <link href="/Data/plugins/accordion/css/style.css" rel="stylesheet" />

    <div class="content_10 fix_p-0 fix_mt-hoidap">
        <div class="col-md-12 col-lg-7 content_10-fix-p">
            <h1 class="content_10-left-title">Giá Vàng</h1>
            
            <div class="accordion">
                <asp:Repeater ID="rptGiaVang" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Loại vàng</th>
                                    <th>Giá mua hôm nay</th>
                                    <th>Giá bán hôm nay</th>
                                    <th>Giá mua hôm trước</th>
                                    <th>Giá bán hôm trước</th>
                                    <th>Ngày cập nhật</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("TenLoaiVang") %></td>
                            <td><%# Eval("GiaMuaHomNay", "{0:N0}") %></td>
                            <td><%# Eval("GiaBanHomNay", "{0:N0}") %></td>
                            <td><%# Eval("GiaMuaHomTruoc", "{0:N0}") %></td>
                            <td><%# Eval("GiaBanHomTruoc", "{0:N0}") %></td>
                            <td><%# Eval("CreatedDate", "{0:dd/MM/yyyy}") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <!-- end accordion -->
        </div>
    </div>
    
    <script src="/Data/plugins/accordion/js/accordion.js"></script>
</asp:Panel>

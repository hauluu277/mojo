<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ThongKeTruyCap.ascx.cs" Inherits="ThongKeTruyCapFeature.UI.ThongKeTruyCap" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="width100">
    <div class="module">
        <div runat="server" id="thongKeTruyCap">
            <% if (KieuGiaoDien == 1)
                { %>
            <table class="tbl-thongke">
                <tr>
                    <th style="border: none !important; border-bottom: 1px solid #e0e0e0;">Online hiện tại:</th>
                    <td>
                        <asp:Label ID="lbl_OnlineHienTai" runat="server">0</asp:Label></td>
                </tr>
                <tr>
                    <th style="border: none !important; border-bottom: 1px solid #e0e0e0;">Truy cập hôm nay:</th>
                    <td>
                        <asp:Label ID="lbl_HomNay" runat="server">0</asp:Label></td>
                </tr>
                <tr>
                    <th style="border: none !important; border-bottom: 1px solid #e0e0e0;">Truy cập tuần này:</th>
                    <td>
                        <asp:Label ID="lbl_TuanNay" runat="server">0</asp:Label></td>
                </tr>
                <tr>
                    <th style="border: none !important; border-bottom: 1px solid #17a2b8;">Truy cập tháng này:</th>
                    <td>
                        <asp:Label ID="lbl_ThangNay" runat="server">0</asp:Label></td>
                </tr>
                <tr>
                    <th style="border: none !important; border-bottom: 1px solid #e0e0e0;">Tất cả lượt truy cập:</th>
                    <td>
                        <asp:Label ID="lbl_TatCa" runat="server">0</asp:Label></td>
                </tr>
            </table>
            <% }
                else
                { %>
            <table class="tbl-thongke">
                <tbody>
                    <tr>
                        <td style="width: 70%; text-align: left;">Tất cả lượt truy cập</td>
                        <td style="text-align: right; font-weight: bold" class="number-total">
                            <asp:Label ID="lbl_TatCa2" runat="server">0</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="progress">
                                <div class="progress-bar progress-bar-danger progress-bar-total" style="width: <%=progressTatCa%>%;"></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70%; text-align: left;">Truy cập hôm nay</td>
                        <td style="text-align: right; font-weight: bold" class="number-reading">
                            <asp:Label ID="lbl_HomNay2" runat="server">0</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="progress">
                                <div class="progress-bar progress-bar-success progress-bar-reading" style="width: <%=progressHomNay%>%;"></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70%; text-align: left;">Truy cập tuần này</td>
                        <td style="text-align: right; font-weight: bold" class="number-writing">
                            <asp:Label ID="lbl_TuanNay2" runat="server">0</asp:Label>
                        </td>
                    </tr>


                    <tr>

                    <tr>
                        <td colspan="2">
                            <div class="progress">
                                <div class="progress-bar progress-bar-info progress-bar-writing" style="width: <%=progressTuanNay%>%;"></div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 70%; text-align: left;">Truy cập tháng này</td>
                        <td style="text-align: right; font-weight: bold" class="number-writing">
                            <asp:Label ID="lbl_ThangNay2" runat="server">0</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="progress">
                                <div class="progress-bar progress-bar-secondary progress-bar-writing" style="width: <%=progressThangNay%>%;"></div>
                            </div>
                        </td>
                    </tr>


                    <tr>
                        <td style="width: 70%; text-align: left;">Online hiện tại</td>
                        <td style="text-align: right; font-weight: bold" class="number-idle">
                            <asp:Label ID="lbl_OnlineHienTai2" runat="server">0</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="progress">
                                <div class="progress-bar progress-bar-warning progress-bar-idle" style="width: <%=progressOnline%>%;"></div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <% } %>
        </div>
    </div>
</div>

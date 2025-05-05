<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DetailThuTuchanhChinh.ascx.cs" Inherits="ThuTucHanhChinhFeature.UI.DetailThuTuchanhChinh" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:HeadingControl ID="heading" runat="server" />
<style>
    .CountLinhVuc ul {
        list-style: none;
    }

        .CountLinhVuc ul li {
            width: 33%;
            float: left;
        }

    table tr th {
        width: 30%;
    }
</style>
<link href="/Data/plugins/tree/tree.css" rel="stylesheet" />
<script src="/Data/plugins/tree/tree.js"></script>

<script src="/Data/plugins/jstree/jstree.min.js"></script>
<%--<link href="/Data/plugins/jstree/themes/default/style.min.css" rel="stylesheet" />--%>
<div class="col-sm-3">
    <div class="left-thutuc">
        <h3>Cơ quan thực hiện</h3>
        <div id="tree">
        </div>
    </div>

    <div class="left-thutuc">
        <h3>Thống kê dịch vụ công</h3>
        <div id="ThongKeDuoiTree">
            <asp:Repeater ID="rptThongKeDVC" runat="server">
                <ItemTemplate>
                    <div class="ThongKeDuoiTree-row"><span><%#Eval("Name") %>:</span> <span class="ThongKeDuoiTree-row-1"><%#Eval("Sumary") %></span></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<script>

</script>
<div class="col-sm-9">
    <div class="module">
        <div class="module-table-body">
            <p class="text-right">
                <a id="hplDichVuCong" runat="server" class="btn btn-success" target="_blank">Nộp hồ sơ trực tuyến</a>
            </p>
            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                <tr>
                    <th style="width: 150px">Tên thủ tục</th>
                    <td>
                        <asp:Label ID="lblTenThuTuc" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Lĩnh vực</th>
                    <td>
                        <asp:Label ID="lblLinhVuc" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Cơ quan thực hiện</th>
                    <td>Sở giáo dục và đào tạo</td>
                </tr>
                <tr>
                    <th>Cách thức thực hiện</th>
                    <td>
                        <asp:Repeater ID="rptArticles" runat="server" SkinID="Article">
                            <ItemTemplate>
                                <div>
                                    <%#Eval("Name") %>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <th>Đối tượng thực hiện</th>
                    <td>
                        <asp:Literal ID="liDoiTuongThucHien" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>Trình tự thực hiện</th>
                    <td>
                        <asp:Label ID="lblTrinhTuThucHien" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Thời hạn giải quyết</th>
                    <td>
                        <asp:Label ID="lblThoiHanGiaiQuyet" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Phí</th>
                    <td>
                        <asp:Label ID="lblPhi" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Lệ phí</th>
                    <td>
                        <asp:Label ID="lblLePhi" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Thành phần hồ sơ</th>
                    <td>
                        <asp:Literal ID="literThanhPhanHS" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>Số lượng hồ sơ</th>
                    <td>
                        <asp:Label ID="lblSoLuongHoSo" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Yêu cầu - điều kiện</th>
                    <td>
                        <asp:Label ID="lblYeuCauDieuKien" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Căn cứ pháp lý</th>
                    <td>
                        <asp:Label ID="lblCanCuPhapLy" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Biểu mẫu đính kèm</th>
                    <td>
                        <asp:Panel ID="pnlFileBieuMau" runat="server">
                            <p>
                                <strong><i class="fa fa-paperclip"></i>File mẫu: </strong>
                            </p>
                            <ul>
                                <asp:Repeater ID="rptBieuMau" runat="server">
                                    <ItemTemplate>
                                        <li style="display: flex">
                                            <%#Eval("TenMau") %>
                                            <asp:ImageButton ImageUrl="/Data/Images/download-icon.png" ToolTip="Tải xuống" ID="img" runat="server" CommandName="CountDownload" CommandArgument='<%# Eval("ItemId") %>' />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <th>Kết quả thực hiện</th>
                    <td>
                        <asp:Label ID="lblKetQuaThucHien" runat="server"></asp:Label></td>
                </tr>
            </table>


        </div>
    </div>
</div>

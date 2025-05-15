<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gold_Price_DisplayEdit.aspx.cs" Inherits="mojoPortal.Features.UI.Gold_Price.Gold_Price_DisplayEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CHỈNH SỬA GIÁ VÀNG</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Chỉnh sửa giá vàng</h2>

            <asp:HiddenField ID="hfItemID" runat="server" />

            <table>
                <tr>
                    <td>Tên loại vàng:</td>
                    <td><asp:TextBox ID="txtTenLoaiVang" runat="server" Width="300px" /></td>
                </tr>
                <tr>
                    <td>Giá mua hôm nay:</td>
                    <td><asp:TextBox ID="txtGiaMuaHomNay" runat="server" /></td>
                </tr>
                <tr>
                    <td>Giá bán hôm nay:</td>
                    <td><asp:TextBox ID="txtGiaBanHomNay" runat="server" /></td>
                </tr>
                <tr>
                    <td>Giá mua hôm trước:</td>
                    <td><asp:TextBox ID="txtGiaMuaHomTruoc" runat="server" /></td>
                </tr>
                <tr>
                    <td>Giá bán hôm trước:</td>
                    <td><asp:TextBox ID="txtGiaBanHomTruoc" runat="server" /></td>
                </tr>
                <tr>
                    <td>Ngân hàng:</td>
                    <td><asp:TextBox ID="txtNganHang" runat="server" Width="300px" /></td>
                </tr>

                <%-- Tháng 1 đến tháng 12 --%>
                <tr><td>Tháng 1:</td><td><asp:TextBox ID="txtThang1" runat="server" /></td></tr>
                <tr><td>Tháng 3:</td><td><asp:TextBox ID="txtThang3" runat="server" /></td></tr>
                <tr><td>Tháng 6:</td><td><asp:TextBox ID="txtThang6" runat="server" /></td></tr>
                <tr><td>Tháng 9:</td><td><asp:TextBox ID="txtThang9" runat="server" /></td></tr>
                <tr><td>Tháng 12:</td><td><asp:TextBox ID="txtThang12" runat="server" /></td></tr>

                <%-- Meta Info --%> 

                <%-- Nút lưu --%>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy" PostBackUrl="~/Gold_Price/Gold_Price_DisplayModule.aspx" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

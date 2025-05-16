<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gold_Price_DisplayEdit.aspx.cs" Inherits="mojoPortal.Features.UI.Gold_Price.Gold_Price_DisplayEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CHỈNH SỬA GIÁ VÀNG</title>
</head>
<body> 
        <div>
            <h2>Chỉnh sửa giá vàng</h2>

            <asp:HiddenField ID="hfItemID" runat="server" />

            <div class="form-group">
                <label>Tên loại vàng:</label>
                <asp:TextBox ID="txtTenLoaiVang" runat="server" CssClass="form-control" />
            </div>

           

            <!-- Các trường giá -->
            <div class="form-group">
                <label>Giá mua hôm nay:</label>
                <asp:TextBox ID="txtGiaMuaHomNay" runat="server" CssClass="form-control price" />
            </div>

            <div class="form-group">
                <label>Giá bán hôm nay:</label>
                <asp:TextBox ID="txtGiaBanHomNay" runat="server" CssClass="form-control price" />
            </div>
            <!-- Các trường giá -->
            <div class="form-group">
                <label>Giá mua hôm nay:</label>
                <asp:TextBox ID="txtGiaMuaHomTruoc" runat="server" CssClass="form-control price" />
            </div>

            <div class="form-group">
                <label>Giá bán hôm nay:</label>
                <asp:TextBox ID="txtGiaBanHomTruoc" runat="server" CssClass="form-control price" />
            </div>

            <!-- Thêm các trường khác tương tự (GiaMuaHomTruoc, GiaBanHomTruoc, Thang1, Thang3,...) -->

            <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" CssClass="btn btn-primary" />
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />
        </div> 
</body>
</html>

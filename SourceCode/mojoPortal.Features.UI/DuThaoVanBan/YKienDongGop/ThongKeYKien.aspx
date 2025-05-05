<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ThongKeYKien.aspx.cs" Inherits="DuThaoVanBanFeature.UI.ThongKeYKien" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%
        var fileName = "Thong-ke-y-kien-gop-y-du-thao.xls";
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "inline; filename = " + fileName);
    %>
    <style type="text/css">
        body {
            font-family: Times New Roman, Helvetica, sans-serif;
            font-size: 12pt;
            color: #000;
        }

        #page-container {
            width: 980px;
            margin: 0 auto;
            padding: 0;
        }

        table.reporttable {
            border: 0;
            border-collapse: collapse;
            width: 100%;
        }

            table.reporttable tr,
            table.reporttable td,
            table.reporttable th {
                border: 0;
            }

            table.reporttable td {
                padding: 5px;
            }
    </style>
</head>
<body>
    <div id="page-container">
        <table class="reporttable">
            <tr>
                <td style="width:55%">
                    <div style="text-align: center; text-transform: uppercase;">
                        <asp:Label ID="Label1" runat="server" Text="BỘ THÔNG TIN VÀ TRUYỀN THÔNG"></asp:Label><br />
                        <asp:Label ID="lblQuanHuyen" runat="server" Text="CỤC TIN HỌC HÓA"></asp:Label><br />
                        <asp:Label ID="Label6" runat="server" Text="--------"></asp:Label>
                    </div>
                </td>
                <td>
                    <div style="text-align: center; font-weight: bold;">
                        <asp:Label ID="Label2" runat="server" Text="CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM"></asp:Label><br />
                        <asp:Label ID="Label3" runat="server" Text="Độc lập - Tự do - Hạnh phúc"></asp:Label><br />
                        <asp:Label ID="Label4" runat="server" Text="---------"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        <table class="reporttable">
            <tr>
                <td colspan="2">
                    <div style="text-align: center; font-weight: bold;">
                        <asp:Label ID="lblTenGiayBienNhan" runat="server" Text="Thống kê ý kiến góp ý dự thảo văn bản"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblDuThao" runat="server"></asp:Label>
                </td>         
            </tr>
            <tr>
                <td>                    
                    Ý kiến chưa duyệt
                </td>
                <td>
                    <asp:Label ID="lblYKienChuaDuyet" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>                    
                    Ý kiến đã duyệt
                </td>
                <td>
                    <asp:Label ID="lblYKienDaDuyet" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ý kiến chưa xuất bản</td>
                <td>
                    <asp:Label ID="lblYKienChuaXuatBan" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ý kiến đã xuất bản</td>
                <td>
                    <asp:Label ID="lblYKienDaXuatBan" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tổng số ý kiến</td>
                <td>
                    <asp:Label ID="lblYKien" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr style="height:20px;"></tr>
            <tr>
                <td>
                    <div style="text-align: center; text-transform: uppercase; font-weight: bold; margin-top: 20px;">
                        <asp:Label ID="Label5" runat="server" Text="NGƯỜI NỘP"></asp:Label>
                    </div>
                    <div style="text-align: center; margin-top: 5px;">
                        <i>(Ký và ghi họ tên)</i>
                    </div>
                </td>
                <td>
                    <div style="text-align: center; text-transform: uppercase; font-weight: bold; margin-top: 20px;">
                        <asp:Label ID="Label8" runat="server" Text="NGƯỜI NHẬN"></asp:Label><br />
                    </div>
                    <div style="text-align: center; margin-top: 5px;">
                        <i>(Ký, ghi họ tên và đóng dấu)</i>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

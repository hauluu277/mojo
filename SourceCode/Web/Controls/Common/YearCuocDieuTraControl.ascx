<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="YearCuocDieuTraControl.ascx.cs" Inherits="mojoPortal.Web.Controls.Common.YearCuocDieuTraControl" %>
<asp:Panel ID="pnlCuocDieuTra" runat="server">
    <div style="background: #f6f8fb; width: 100%; float: left; height: 40px;">
        <div class="container text-right" style="line-height: 39px; font-size: 18px;">
            Năm thực hiện
            <asp:DropDownList ID="ddlYear" Width="350" runat="server"></asp:DropDownList>
        </div>
    </div>
    <script type="text/javascript">
        $("#<%=ddlYear.ClientID%>").change(function () {
             window.location.href = this.value;
        })
    </script>
</asp:Panel>

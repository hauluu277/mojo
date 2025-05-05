<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostList.ascx.cs" Inherits="LogSystemFeature.UI.PostList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<link href="/ClientScript/fastselect/fontcss.css" rel="stylesheet" />
<link href="/ClientScript/fastselect/build.min.css" rel="stylesheet" />
<link href="/ClientScript/fastselect/fastselect.min.css" rel="stylesheet" />
<script src="/ClientScript/fastselect/fastselect.standalone.js"></script>
<link rel="stylesheet" href="/Data/js/bootstrap-multiselect/bootstrap-multiselect.css" />
<link href="../Data/js/bootstrap-datepicker-1.6.4-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<script src="../Data/js/bootstrap-datepicker-1.6.4-dist/js/bootstrap-datepicker.min.js"></script>
<style type="text/css">
    .fstElement {
        height: 32px;
        width: 100%;
    }

    .fstResultItem {
        text-align: left;
    }

    .fstToggleBtn {
        width: 100%;
        min-width: 472px;
        text-align: left;
    }

    .form-control, input[type=text].forminput, select.forminput {
        border-radius: 0;
    }

    label {
        font-weight: normal;
    }

    #dialogCat select, #dialog select {
        height: 32px;
    }

    .search-label {
        width: 100%;
        float: left;
    }

    .form-control, input[type=text].forminput, select.forminput {
        width: 100%;
    }
</style>
<fieldset class="fieldset">
    <legend class="legend">Tìm kiếm lịch sử hoạt động</legend>

    <div class="col-sm-6 from-group">
        <label class="search-label">Tên đăng nhập</label>
        <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
    </div>
    <div class="col-sm-6 from-group">
        <label class="search-label">Họ tên</label>
        <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
    </div>
    <div class="clearfix"></div>
    <div class="col-sm-6 from-group">
        <label class="search-label">Ngày</label>
        <input type="text" class="col-sm-5 form-control date" id="txtDateFrom" runat="server" />
        <div class="col-sm-1"></div>
        <input type="text" class="col-sm-5 form-control date" id="txtDateTo" runat="server" />
    </div>
    <div class="col-sm-6 from-group">
        <label class="search-label">Đăng nhập lần đầu</label>
        <input type="text" class="col-sm-5 form-control date" id="txtStartLoginFrom" runat="server" />
        <div class="col-sm-1"></div>
        <input type="text" class="col-sm-5 form-control date" id="txtStartLoginTo" runat="server" />
    </div>
    <div class="clearfix"></div>
    <div class="col-sm-6 from-group">
        <label class="search-label">Đăng nhập lần cuối</label>
        <input type="text" class="col-sm-5 form-control date" id="txtEndLoginFrom" runat="server" />
        <div class="col-sm-1"></div>
        <input type="text" class="col-sm-5 form-control date" id="txtEndLoginTo" runat="server" />
    </div>
    <div class="col-sm-6 from-group">
        <label class="search-label"></label>
        <portal:mojoButton ID="btnSearch" runat="server" />
    </div>
</fieldset>
<div class="module">
    <div class="module-table-body">
        <asp:Label ID="lblTotalArticle" CssClass="red" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Repeater ID="rptArticles" runat="server" SkinID="Article">
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover table-condensed">
                    <tr>
                        <th>Tài khoản</th>
                        <th>Họ tên</th>
                        <th>Số lần đăng nhập</th>
                        <th>Ngày</th>
                        <th>Đăng nhập lần đầu</th>
                        <th>Đăng nhập lần cuối</th>
                    </tr>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align: center"><%#Eval("LoginName") %></td>
                    <td><%#Eval("FullName") %></td>
                    <td><%#Eval("CountLogin") %></td>
                    <td><%#string.Format("{0:dd/MM/yyyy HH:mm}",Eval("CreatedDate")) %></td>
                    <td><%#string.Format("{0:dd/MM/yyyy HH:mm}",Eval("StartLogin")) %></td>
                    <td><%#string.Format("{0:dd/MM/yyyy HH:mm}",Eval("EndLogin")) %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Panel ID="pnlLogSystem" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrLogSystem" runat="server" />
        </asp:Panel>
    </div>
</div>
<script>
    $(document).ready(function () {
<%--        $("#<%=ddlCategories.ClientID%>").fastselect({ placeholder: "Danh mục tin bài" });
        $("#<%=ddlApproveStatus.ClientID%>").fastselect();
        $("#<%=ddlIsHome.ClientID%>").fastselect();
        $("#<%=ddlIsHot.ClientID%>").fastselect();
        $("#<%=ddlPublishStatus.ClientID%>").fastselect();--%>
        $('.date')
            .datepicker({
                format: 'dd/mm/yyyy',
                startDate: '01/01/2010',
                endDate: '12/30/2050'
            });
    });

</script>

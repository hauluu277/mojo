<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostListAnswer.ascx.cs" Inherits="QuestionAnswerFeatures.UI.PostListAnswer" %>

<%@ Import Namespace="QuestionAnswersFeatures.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .author {
        padding: 0 5px;
        font-size: 11px;
        color: #C0C0C0;
        font-style: italic;
        font-weight: bold;
    }

    .myTable th {
        background-color: #eeeeee;
        color: #444444;
        padding: 5px;
        text-align: left;
        border: none;
        border-collapse: collapse;
    }

    .myTable tr td {
        border: 1px solid rgba(128, 128, 128, 0.19);
        vertical-align: top;
        line-height: 20px;
        border-collapse: collapse;
        padding: 5px;
    }

    .myTable tr:nth-child(even) {
        background: rgb(250,250,250);
    }

    .myTable tr:nth-child(odd) {
        background: #FFF;
    }

    .tableheader {
        width: 100%;
        margin: 0 auto;
    }

    table {
        border-collapse: collapse;
    }

    .myTable select {
        width: 250px;
    }

    .questiondetail {
        min-height: 100px;
        width: 100%;
        float: left;
        padding: 12px 0;
    }

        .questiondetail h2 {
            font-weight: bold;
            font-size: 17px;
            margin-bottom: 10px;
            color: #333;
            border-bottom: 1px solid rgb(241,241,241);
        }

    .infosender span {
        line-height: 25px;
        width: 100%;
        float: left;
    }

    .listAnswer {
        width: 100%;
        font-weight: bold;
        font-size: 17px;
        margin-bottom: 10px;
        color: #333;
        border-bottom: 1px solid rgb(241,241,241);
    }
</style>
<fieldset runat="server" style="display: none">
    <legend id="legendQuestionAnswer" runat="server"></legend>
</fieldset>

<div class="panel panel-border-title">
    <div class="panel-heading">
        <div>Tiêu chí tìm kiếm</div>
    </div>
    <div class="panel-body">
        <asp:UpdatePanel ID="RencenListUpdatePanel" runat="server">
            <ContentTemplate>
                <table class="tableheader">
                    <tr style="height: 20px"></tr>
                    <tr>
                        <td>
                            <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="ddlStatus" CssClass="lbl-fix" ConfigKey="StatusTitle" ResourceFile="SwirlingQuestionResource" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" Width="300" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px"></tr>
                    <tr>
                        <td></td>
                        <td>
                            <portal:mojoButton ID="btnSearch" runat="server" />
                        </td>
                    </tr>
                    <tr style="height: 20px"></tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div id="toolbar-box">
    <div class="tool-btn">
        <portal:mojoButton ID="btnAnswer" SkinID="ButtonSuccess" runat="server" />
        <portal:mojoButton ID="btnRemoveAll" SkinID="ButtonDanger" runat="server" />
        <portal:mojoButton ID="btnQuestionManage" Text="Quản trị câu hỏi" SkinID="ButtonPrimary" runat="server" />
    </div>
</div>
<div class="questiondetail">
    <h2>Nội dung câu hỏi</h2>
    <div class="infosender">
        <asp:Label ID="lblNameSender" runat="server"></asp:Label>
        <asp:Label ID="lblEmailSender" runat="server"></asp:Label>
        <asp:Label ID="lblDateSend" runat="server"></asp:Label>
        <br />
        Nội dung: 
            <br />
        <p>
            <asp:Literal ID="literContentQuestion" runat="server"></asp:Literal>
        </p>
    </div>
    <asp:Literal ID="literQuestion" runat="server"></asp:Literal>
</div>
<asp:Panel ID="pnlPostList" runat="server">
    <h2 class="listAnswer">Danh sách câu trả lời</h2>
    <br />
    <asp:Repeater ID="rptQuestion" runat="server" SkinID="Blog">
        <HeaderTemplate>
            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%; text-align: left">
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" />
                        </th>
                        <th class="tbl-header" style="width: 15%">
                            <%#Resources.SwirlingQuestionResource.NguoiGuiTitle %>
                        </th>
                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.EmailLienHeTitle %>
                        </th>
                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.NoiDungTitle%>
                        </th>
                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.NgayGuiTitle%>
                        </th>

                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.StatusTitle%>
                        </th>
                        <th style="width: 5%" class="tbl-header"></th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("ItemID") %>' Visible="false"></asp:Literal>
                    <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                </td>
                <td>
                    <div class="name-title">
                        <%#Eval("Name") %>
                    </div>
                </td>

                <td>
                    <%#Eval("Email") %>

                </td>
                <td>
                    <div class="answerdetail">
                        <%#Eval("AnswerName") %>
                    </div>
                </td>
                <td>
                    <%#string.Format("{0:dd/MM/yyyy HH:mm}",Eval("CreateDate")) %>
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ibtnChangeState" runat="server" ImageUrl='<%# mojoPortal.Features.QuestionAnswerUtils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsApprove").ToString())) %>'
                        CommandName="EditStateItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# StateLink %>"
                        CausesValidation="false" />
                </td>
                <td>
                    <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                        CommandName="EditItem" CommandArgument='<%# Eval("ItemID") %>'
                        CausesValidation="false" />

                    <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                        CommandName="DeleteItem" CommandArgument='<%# Eval("ItemID") %>'
                        CausesValidation="false" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
        </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlQuestion" runat="server" CssClass="ArticlePager">
        <portal:mojoCutePager ID="pgrQuestion" runat="server" />
    </asp:Panel>
    <script>
        function DoCheck(control, check) {
            for (var i = 0; i < control.length; i++) {
                if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('chk') != -1) {
                    control[i].checked = check;
                }
            }
        };
        function DoCheckAll(obj) {
            var control = document.getElementsByTagName("input");
            DoCheck(control, obj.checked);
        };

        function CheckItem(obj) {
            var control = document.getElementsByTagName("input");
            var controlChkAll;
            var allControl = 0;
            var checkedControl = 0;
            for (var i = 0; i < control.length; i++) {
                if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('chk') != -1) {
                    allControl++;
                    if (control[i].checked == true) {
                        checkedControl++;
                    }
                }
                if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('checkAll') != -1) {
                    controlChkAll = control[i];
                }
            }
            if (allControl == checkedControl) {
                controlChkAll.checked = true;
            }
            else {
                controlChkAll.checked = false;
            }
        }
        function ConfirmDeleteAll() {
            var isTrue = confirm("Dữ liệu sẽ bị xóa vĩnh viễn. Bạn có thực sự muốn xóa?");
            if (isTrue) {
                //ChangeAction('hdAction', 'delete');
                //document.getElementById('adminForm').submit();
                //alert("chức năng đang hoàn thiện, vui lòng thử lại sau");
                return true;
            }

            return false;
        }
    </script>
</asp:Panel>


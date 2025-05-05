<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostList.ascx.cs" Inherits="QuestionAnswersFeatures.UI.PostList" %>
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
                    <tr>
                        <td>
                            <mp:SiteLabel ID="drl" runat="server" ForControl="ddlLinhVuc" CssClass="lbl-fix" ConfigKey="LinhVucTitle" ResourceFile="SwirlingQuestionResource" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLinhVuc" Width="300" AutoPostBack="true" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px"></tr>
                    <tr>
                        <td>
                            <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="ddlStatus" CssClass="lbl-fix" ConfigKey="StatusTitle" ResourceFile="SwirlingQuestionResource" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" Width="300" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="ddlOrderBy" CssClass="lbl-fix" ConfigKey="OrderByTitle" ResourceFile="SwirlingQuestionResource" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOrderBy" Width="300" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px"></tr>
                    <tr>
                        <td>
                            <mp:SiteLabel ID="lblKeyword" runat="server" CssClass="lbl-fix" ConfigKey="KeywordTitle" ResourceFile="SwirlingQuestionResource" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearch" CssClass="recentlist-textbox-fix" Width="300" MaxLength="220" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <portal:mojoButton ID="btnSearch" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div id="toolbar-box">
    <div class="tool-btn">
        <portal:mojoButton ID="btnRemoveAll" SkinID="ButtonDanger" runat="server" OnClientClick="ConfirmDeleteAll();" />

    </div>
</div>
<asp:Panel ID="pnlPostList" runat="server">
    <asp:Repeater ID="rptQuestion" runat="server" SkinID="Blog">
        <HeaderTemplate>
            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%; text-align: center">
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" />
                        </th>
                        <th class="tbl-header" style="width: 35%">
                            <%#Resources.SwirlingQuestionResource.CauHoiTitle %>
                        </th>
                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.LinhVucTitle %>
                        </th>
                        <th class="tbl-header">
                            <%--<%#Resources.SwirlingQuestionResource.LoaiLinhVucTitle%>--%>
                            Phân công
                        </th>
                        <%--                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.KhuVucQuanTamTitle%>
                        </th>--%>

                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.LuotXemTitle%>
                        </th>
                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.TraLoiTitle%>
                        </th>
                        <th class="tbl-header">
                            <%#Resources.SwirlingQuestionResource.AnswerApproveTitle%>
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
                <td style="text-align: center">
                    <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("ItemID") %>' Visible="false"></asp:Literal>
                    <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                </td>
                <td>
                    <div class="name-title">
                        <asp:HyperLink ID="hplDetailQuestion" runat="server" ToolTip='<%#Eval("Question") %>' NavigateUrl='<%#QuestionAnswerUtils.FormatDetailQuestionUrl(SiteRoot,int.Parse(Eval("PageID").ToString()),Eval("ItemUrl").ToString(),int.Parse(Eval("ItemID").ToString()),false,string.Empty) %>'><%#Eval("Question") %></asp:HyperLink>
                        <p class="author">
                            Đăng bởi:<%#Eval("Name") %>
                            <br />
                            Ngày: <%#string.Format("{0:dd/MM/yyyy HH:mm}",Eval("CreateDate")) %>
                        </p>
                    </div>
                </td>
                <td style="text-align: center">
                    <%#Eval("CategoryName") %>
                </td>
                <td style="text-align: center">
                    <%#Eval("DepartmentName") %>

                </td>
                <%--                <td style="text-align: center">
                    <%#Eval("CityName") %>
                </td>--%>
                <td style="text-align: center"><%#Eval("Views") %></td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# AnswerLinkImageUrl %>'
                        CommandName="AnswerItem" CommandArgument='<%# Eval("ItemID") %>'
                        CausesValidation="false" />
                </t>
                <td style="text-align: center">
                    <%# Eval("TotalAnswer")+"/"+Eval("TotalAnswerApproved") %>
                    <asp:ImageButton ID="ibtnDetailAnswer" runat="server" ImageUrl='<%# DetailAnswerIMG %>'
                        CommandName="DetailAnswerItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="Xem danh sách câu trả lời"
                        CausesValidation="false" />
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

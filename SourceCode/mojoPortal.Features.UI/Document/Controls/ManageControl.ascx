<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ManageControl.ascx.cs" Inherits="DocumentFeature.UI.ManageControl" %>
<%@ Import Namespace="mojoPortal.Features" %>

<div class="panel panel-border-title">
    <div class="panel-heading">
        <div>Tìm kiếm văn bản</div>
    </div>
    <div class="panel-body">
        <div class="bottom-spacing1">
            <div class="search-item">
                <mp:SiteLabel ID="lblCat" runat="server" ForControl="ddlLinhVuc" CssClass="search-label"
                    ConfigKey="LinhVucHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlLinhVuc" runat="server" CssClass="ddlSettingLabel1"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="ddlLoaiVB" CssClass="search-label"
                    ConfigKey="LoaiVBHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlLoaiVB" runat="server" CssClass="ddlSettingLabel1"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="ddlCoQuan" CssClass="search-label"
                    ConfigKey="CoQuanIDHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlCoQuan" runat="server" CssClass="ddlSettingLabel1"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <mp:SiteLabel ID="lblStatus" runat="server" ForControl="ddlStatus" CssClass="search-label"
                    ConfigKey="ArticleEditStatusLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlStatus" runat="server" CssClass="ddlSettingLabel1"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label"
                    ConfigKey="ArticleEditKeywordLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:TextBox ID="txtKeyword" Width="100%" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="search-item">
                <label class="search-label">Nhóm văn bản</label>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlNhomVB" runat="server" CssClass="ddlSettingLabel1"></asp:DropDownList>
                </div>
            </div>
            <div class="searchSubmit">
                <portal:mojoButton ID="btnSearch" runat="server" />
            </div>
        </div>
    </div>
</div>
<%-- begin toolbar-box --%>
<div id="toolbar-box">
    <div class="tool-btn">
        <portal:mojoButton ID="btnaddnew" SkinID="ButtonPrimary" runat="server" OnClick="btnaddnew_Click" />
        <portal:mojoButton ID="btnDelAll" runat="server" SkinID="ButtonDanger" OnClick="btnDelAll_Click" />
    </div>
</div>
<%-- end toolbar-box --%>
<div class="module">
    <div class="module-table-body">
        <asp:Literal ID="literTotalVanBan" runat="server"></asp:Literal>
        <asp:Repeater ID="rptArticles" runat="server" SkinID="Article" OnItemCommand="rptArticles_ItemCommand" OnItemDataBound="rptArticles_ItemDataBound">
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                    <tr>
                        <th style="width: 5%; text-align: center">
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" /></th>
                        <th style="width: 25%" class="tbl-header">
                            <%#Resources.DocumentResources.SummaryDocHeaderLabel %>
                        </th>
                        <th style="width: 12%" class="tbl-header">
                            <%#Resources.DocumentResources.SignHeaderLabel %>
                        </th>
                        <th style="width: 10%" class="tbl-header">
                            <%#Resources.DocumentResources.LinhVucHeaderLabel %>
                        </th>
                        <th style="width: 11%" class="tbl-header">
                            <%#Resources.DocumentResources.LoaiVBHeaderLabel %>
                        </th>
                        <th style="width: 14%" class="tbl-header">
                            <%#Resources.DocumentResources.CoQuanIDHeaderLabel %>
                        </th>
                        <th style="width: 5%" class="tbl-header">
                            <%#Resources.DocumentResources.HeaderDocumentApprove %>
                        </th>
                        <th style="width: 7%" class="tbl-header"></th>
                    </tr>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align: center">
                        <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("ItemID") %>' Visible="false"></asp:Literal>
                        <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                    </td>
                    <td>
                        <div style="float: left; width: 100%;">
                            <%# Eval("Summary") %>
                            <p class="author">
                                Đăng bởi:
                                <%#Eval("CreatedByUser") %>
                            </p>
                    </td>
                    <td><%#Eval("Sign") %></td>
                    <td><%#Eval("LinhVucName") %></td>
                    <td><%#Eval("LoaiVBName") %></td>
                    <td><%#Eval("CoQuanName") %></td>
                    <td style="text-align: center">
                        <asp:ImageButton ID="ibtnChangeState" runat="server" ImageUrl='<%# mojoPortal.Features.DocumentUltils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsApproved").ToString())) %>'
                            CommandName="EditStateItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# StateLink %>"
                            CausesValidation="false" />
                    </td>
                    <td style="text-align: center">
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                            CommandName="EditItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# EditLinkText %>"
                            CausesValidation="false" />

                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                            CommandName="DeleteItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# DeleteLinkText %>"
                            CausesValidation="false" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlArticlePager" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrArticle" runat="server" />
        </asp:Panel>
    </div>
</div>
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
            alert("chức năng đang hoàn thiện, vui lòng thử lại sau");
        }

        return false;
    }
</script>
<style>
    .search-box {
        width: 100%;
        float: left;
    }

    .searchSubmit {
        float: left;
        margin-top: 15px;
    }

    .search-item {
        float: left;
        width: 45%;
        margin-right: 5%;
        margin-bottom: 5px;
    }

    .search-label {
        float: left;
        min-width: 150px;
        display: block;
    }

    .article-title a {
        font-size: 13px;
        color: #0a8acb;
        font-weight: bold;
        text-decoration: none;
    }

        .article-title a:hover {
            text-decoration: underline;
        }

    .author {
        font-size: 11px;
        color: #C0C0C0;
        font-style: italic;
        font-weight: bold;
    }



    .bottom-spacing {
        margin-bottom: 20px;
    }


    div.module {
        float: left;
        margin-bottom: 20px;
        width: 100%;
    }



    .align-center {
        text-align: center;
    }

    a, a:visited {
        text-decoration: none;
    }



    .tool-btn {
        float: right;
    }

    .clr {
        clear: both;
        height: 0;
        overflow: hidden;
    }
</style>

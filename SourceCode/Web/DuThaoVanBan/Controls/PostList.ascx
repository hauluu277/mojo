<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostList.ascx.cs" Inherits="DuThaoVanBanFeature.UI.PostList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%-- begin toolbar-box --%>
<asp:Panel ID="pnBtnAdmin" runat="server">
</asp:Panel>
<%-- end toolbar-box --%>
<div class="search-box">
    <fieldset>
        <legend runat="server" id="legendSearchProperty" class="legend-title"></legend>
        <div class="bottom-spacing">
            <div class="form-group">
                <div class="col-sm-12 col-md-6">
                    <mp:SiteLabel ID="lblCat" runat="server" ForControl="ddlLinhVuc" CssClass="search-label"
                        ConfigKey="LinhVucHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                    <div class="search-control">
                        <asp:DropDownList Width="60%" Height="29" ID="ddlLinhVuc" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-12 col-md-6">
                    <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="ddlLoaiVB" CssClass="search-label"
                        ConfigKey="LoaiVBHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                    <div class="search-control">
                        <asp:DropDownList Width="60%" Height="29" ID="ddlLoaiVB" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12 col-md-6">
                    <mp:SiteLabel ID="lblStatus" runat="server" ForControl="ddlStatus" CssClass="search-label"
                        ConfigKey="ArticleEditStatusLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                    <div class="search-control">
                        <asp:DropDownList Width="60%" Height="29" ID="ddlStatus" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-12 col-md-6">
                    <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label"
                        ConfigKey="ArticleEditKeywordLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                    <div class="search-control">
                        <asp:TextBox ID="txtKeyword" Width="58%" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group" style="margin-top: 30px;">
                    <div class="col-sm-12 text-right">
                        <portal:mojoButton ID="btnSearch" runat="server" />
                        <portal:mojoButton ID="btnaddnew" runat="server" OnClick="btnaddnew_Click" />
                        <portal:mojoButton ID="btnDelAll" runat="server" OnClick="btnDelAll_Click" />
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<div class="module">
    <div class="module-table-body">
        <asp:Repeater ID="rptArticles" runat="server" SkinID="Article" OnItemCommand="rptArticles_ItemCommand" OnItemDataBound="rptArticles_ItemDataBound">
            <HeaderTemplate>
                <table class="tablesorter" id="myTable">
                    <tr>
                        <th style="width: 5%">
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" /></th>
                        <th style="width: 20%" class="tbl-header">Tiêu đề</th>
                        <th style="width: 35%" class="tbl-header">
                            <%#Resources.DocumentResources.SummaryDocHeaderLabel %>
                        </th>
                        <th style="width: 10%" class="tbl-header">
                            <%#Resources.DocumentResources.LinhVucHeaderLabel %>
                        </th>
                        <th style="width: 10%" class="tbl-header">
                            <%#Resources.DocumentResources.LoaiVBHeaderLabel %>
                        </th>
                        <th>Thời gian</th>
                        <%if (isAdmin)
                            { %>
                        <th style="width: 5%" class="tbl-header">
                            <%#Resources.DocumentResources.HeaderDocumentApprove %>
                        </th>
                        <th style="width: 5%" class="tbl-header"></th>
                        <%} %>
                        <th style="width: 7%" class="tbl-header"></th>
                    </tr>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("ItemID") %>' Visible="false"></asp:Literal>
                        <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                    </td>
                    <td>
                        <%# Eval("Title") %>
                    </td>
                    <td>
                        <div style="float: left; width: 100%;">
                            <p class="author">
                                Đăng bởi:
                                <%#Eval("CreatedByUser") %>
                            </p>
                            <%# Eval("Summary") %>
                    </td>
                    <td><%#Eval("LinhVucName") %></td>
                    <td><%#Eval("LoaiVBName") %></td>
                    <td><%#string.Format("{0} - {1}",string.Format("{0:dd/MM/yyyy}",Eval("StartDate")), string.Format("{0:dd/MM/yyyy}", Eval("EndDate"))) %></td>
                    <%if (isAdmin)
                        { %>
                    <td>
                        <asp:ImageButton ID="ibtnChangeState" runat="server" ImageUrl='<%# mojoPortal.Features.DocumentUltils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsPublic").ToString())) %>'
                            CommandName="EditStateItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# StateLink %>"
                            CausesValidation="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                            CommandName="EditItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# EditLinkText %>"
                            CausesValidation="false" />

                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                            CommandName="DeleteItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# DeleteLinkText %>"
                            CausesValidation="false" />
                    </td>
                    <%} %>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" EnableViewState="false" Text="D/s ý kiến" NavigateUrl='<%#UrlComments(Convert.ToInt32(Eval("ItemID"))) %>' Visible="true" /> (<%#Eval("TotalYKien") %>)
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
        float: right;
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


    table, caption, tbody, thead, tr, th, td {
        border: 0 none;
        font-size: 100%;
        outline: 0 none;
        vertical-align: baseline;
    }

    .bottom-spacing {
        margin-bottom: 20px;
    }


    div.module {
        float: left;
        margin-bottom: 20px;
        width: 100%;
    }


        div.module div.module-table-body {
            float: left;
            padding: 0;
            width: 100%;
        }

        div.module table {
            border: 1px solid #d9d9d9;
            margin: 0 0 10px;
            width: 100%;
        }

            div.module table th {
                background-color: #eeeeee;
                border: 1px solid #d9d9d9;
                /*border-bottom: 1px solid #d9d9d9;*/
                /*border-right: 1px solid #d9d9d9;*/
                color: #444444;
                padding: 5px;
                text-align: left;
            }

    .tablesorter tr:hover {
        background: #e7f6fa !important;
    }

    div.module table td {
        border: 1px solid #d9d9d9;
        background-color: #ffffff;
        /*border-right: 1px solid #d9d9d9;*/
        padding: 5px;
    }

    .align-center {
        text-align: center;
    }

    a, a:visited {
        text-decoration: none;
    }

    table {
        border-collapse: collapse;
        border-spacing: 0;
    }

        table.tablesorter {
            text-align: left;
            width: 100%;
        }

            table.tablesorter thead tr .tbl-header {
                background-image: url("/Data/SiteImages/article-icon/bg.gif");
                background-position: right center;
                background-repeat: no-repeat;
                cursor: pointer;
            }

            table.tablesorter tbody td {
                background-color: #fff;
                color: #3d3d3d;
                padding: 4px;
                vertical-align: top;
            }

            table.tablesorter tbody tr.odd td {
                background-color: #f1f5fa;
            }

            table.tablesorter tbl-thead tr .headerSortUp {
                background-image: url("/Data/SiteImages/article-icon/asc.gif");
            }

            table.tablesorter tbl-thead tr .headerSortDown {
                background-image: url("/Data/SiteImages/article-icon/desc.gif");
            }

            table.tablesorter tbl-thead tr .headerSortDown, table.tablesorter tbl-thead tr .headerSortUp {
                background-color: #dddddd;
            }

    div#toolbar-box {
        background: none repeat scroll 0 0 #fbfbfb;
        margin-top: 10px;
        margin-bottom: 10px;
        float: left;
        padding: 2%;
        width: 96%;
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

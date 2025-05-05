<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CommentsList.ascx.cs" Inherits="DuThaoVanBanFeature.UI.CommentsList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%-- begin toolbar-box --%>
<div id="toolbar-box">
    <div class="tool-btn">
        <portal:mojoButton ID="btnDelAll" runat="server" OnClick="btnDelAll_Click" />
        <portal:mojoButton ID="btnExport" runat="server" OnClick="btnExport_Click" />
        <portal:mojoButton ID="btnBack" runat="server" OnClick="btnBack_Click" />
    </div>
</div>
<%-- end toolbar-box --%>
<div class="search-box">
    <fieldset>
        <legend runat="server" id="legendSearchProperty"></legend>
        <div class="bottom-spacing">
            <div class="search-item">
                <mp:SiteLabel ID="lblStatus" runat="server" ForControl="ddlStatus" CssClass="search-label"
                    ConfigKey="ArticleEditStatusLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlStatus" Height="29" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label"
                    ConfigKey="ArticleEditKeywordLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="searchSubmit">
                <portal:mojoButton ID="btnSearch" runat="server" />
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
                         <th style="width: 15%" class="tbl-header"><%#Resources.DuThaoVanBanResources.NameHeaderLabel%></th>
                        <th style="width: 10%" class="tbl-header">
                            <%#Resources.DuThaoVanBanResources.EmailHeaderLabel %>
                        </th>
                        <th style="width: 15%" class="tbl-header">
                            <%#Resources.DuThaoVanBanResources.AddressLabel %>
                        </th>
                        <th style="width: 11%" class="tbl-header">
                            <%#Resources.DuThaoVanBanResources.MobileLabel %>
                        </th>
                        <th style="width: 29%" class="tbl-header">
                            <%#Resources.DuThaoVanBanResources.CommentHeaderLabel %>
                        </th>
                        <th style="width: 5%" class="tbl-header">
                            <%#Resources.DuThaoVanBanResources.HeaderDocumentApprove %>
                        </th>
                        <th style="width: 5%" class="tbl-header">
                            <%#Resources.DuThaoVanBanResources.HeaderDocumentPublic %>
                        </th>
                        <th style="width: 5%" class="tbl-header"></th>
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
                        <asp:HyperLink ID="lnkDetail" runat="server" NavigateUrl='<%#formatLinkDeatail(Convert.ToInt32(Eval("ItemID"))) %>'><%# Eval("Name") %></asp:HyperLink>
                    </td>
                    <td>
                        <%# Eval("Email") %>
                    </td>
                    <td><%#Eval("Address") %></td>
                    <td><%#Eval("Mobile") %></td>
                    <td><%#formatContent(Eval("Comment").ToString()) %></td>
                    <td>
                        <asp:ImageButton ID="ibtnApprove" runat="server" ImageUrl='<%# mojoPortal.Features.DocumentUltils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsApproved").ToString())) %>'
                            CommandName="ApproveItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# ChangeApproveText %>"
                            CausesValidation="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtnPublic" runat="server" ImageUrl='<%# mojoPortal.Features.DocumentUltils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsPublished").ToString())) %>'
                            CommandName="PublicItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# ChangePublicText %>"
                            CausesValidation="false" />
                    </td>
                    <td>
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
    .search-box{
        width:100%;
        float:left;
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

            div.module table tr:hover {
                background-color: #e7f6fa;
            }

            div.module table td {
                background-color: #ffffff;
                border: 1px solid #d9d9d9;
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

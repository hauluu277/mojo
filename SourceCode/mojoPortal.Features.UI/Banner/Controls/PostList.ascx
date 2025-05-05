<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostList.ascx.cs" Inherits="BannerFeature.UI.PostList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%-- begin toolbar-box --%>
<style type="text/css">
    .tablesorter img {
        width: 100%;
        max-width: 350px;
    }

</style>

<%-- end toolbar-box --%>
<div class="panel panel-border-title">
    <fieldset style="display: none">
        <legend runat="server" id="legendSearchProperty"></legend>
    </fieldset>
    <div class="panel-heading">
        <div>Tiêu chí tìm kiếm</div>
    </div>
    <div class="panel-body">
        <div class="search-item">
            <mp:SiteLabel ID="lblStatus" runat="server" ForControl="ddlStatus" CssClass="search-label"
                ConfigKey="ArticleEditStatusLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
            <div class="search-control">
                <asp:DropDownList Width="100%" ID="ddlStatus" Height="29px" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="search-item">
            <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label"
                ConfigKey="ArticleEditKeywordLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
            <div class="search-control">
                <asp:TextBox ID="txtKeyword" Width="100%" Height="32" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="searchSubmit">
            <portal:mojoButton ID="btnSearch" runat="server" />
        </div>
    </div>
</div>
<div id="toolbar-box">
    <div class="tool-btn">
        <portal:mojoButton ID="btnaddnew" SkinID="ButtonPrimary" runat="server" OnClick="btnaddnew_Click" />
        <portal:mojoButton ID="btnDelAll" runat="server" SkinID="ButtonDanger" OnClick="btnDelAll_Click" />
    </div>
</div>
<div class="module">
    <div class="module-table-body">
        <asp:Repeater ID="rptBanner" runat="server" SkinID="Article" OnItemCommand="rptBanner_ItemCommand" OnItemDataBound="rptBanner_ItemDataBound">
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover table-condensed">
                    <tr>
                        <th style="width: 5%; text-align: center">
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" /></th>
                        <th style="width: 13%" class="tbl-header">Tiêu đề
                        </th>
                        <th style="width: 40%" class="tbl-header">Banner
                        </th>
                        <th style="width: 7%" class="tbl-header">Lượt click
                        </th>
                        <th style="width: 7%" class="tbl-header">Thứ tự
                        </th>
                        <th style="width: 7%" class="tbl-header">Đã duyệt?
                        </th>
                        <th>URL</th>
                        <th style="width: 5%" class="tbl-header"></th>
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
                        <%#Eval("Name") %>
                    </td>
                    <td>
                        <div style="float: left; width: 25%; margin-right: 2%;" class='<%# "article-logo" + ModuleId + Eval("ItemID") %>' title='<%# Eval("Name") %>'>
                            <asp:Image ID="imgBanner" Width="500" Visible='<%#Eval("IsImage")%>' runat="server" ImageUrl='<%#"~/Data/Images/Banner/" + Eval("Path")%>' />
                            <%#BuildFlashObject(Convert.ToBoolean(Eval("IsImage")),Eval("Path").ToString()) %>
                        </div>
                    </td>
                    <td style="text-align: center">
                        <%#Eval("HitCount") %>
                    </td>
                    <td>
                        <%#Eval("Priority") %>
                    </td>
                    <td style="text-align: center">
                        <%--<asp:HyperLink ID="approveLink" runat="server" EnableViewState="false" Text=""
                            ToolTip="" ImageUrl='<%# mojoPortal.Features.BannerUtils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsPublic").ToString())) %>' NavigateUrl="#" Visible="true" />--%>
                        <asp:ImageButton ID="ibtnApprove" runat="server" ImageUrl='<%# mojoPortal.Features.BannerUtils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsPublic").ToString())) %>'
                            CommandName="ApproveItem" CommandArgument='<%# Eval("ItemID") %>'
                            CausesValidation="false" />
                    </td>
                    <td>
                        <%#Eval("Link") %>
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
    //function ConfirmDeleteAll() {
    //    var isTrue = confirm("Dữ liệu sẽ bị xóa vĩnh viễn. Bạn có thực sự muốn xóa?");
    //    if (isTrue) {
    //        //ChangeAction('hdAction', 'delete');
    //        //document.getElementById('adminForm').submit();
    //        alert("chức năng đang hoàn thiện, vui lòng thử lại sau");
    //    }

    //    return false;
    //}
</script>
<style>
    .search-box {
        width: 100%;
        float: left;
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


    div#toolbar-box {
        background: none repeat scroll 0 0 #fbfbfb;
        margin-top: 10px;
        margin-bottom: 10px;
        float: left;
        padding: 2%;
        width: 100%;
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

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostList.ascx.cs" Inherits="VideoIntroduceFeatures.UI.PostList" %>

<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .postlisttbl {
        width: 80%;
        margin: 0 auto;
    }

        .postlisttbl label {
            width: 100%;
            text-align: left;
        }
</style>
<div class="col-sm-12" style="padding: 0">
    <fieldset class="fieldset">
        <legend class="legend">Tiêu chí tìm kiếm video</legend>
        <table class="postlisttbl">
            <tr>
                <td>
                    <label>Trạng thái</label>
                    <asp:DropDownList ID="drlStatus" Height="32" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <label>Từ khóa</label>
                    <asp:TextBox ID="txtSearch" MaxLength="350" runat="server"></asp:TextBox>
                </td>
                <td style="display: none">
                    <label>Kiểu video</label>
                    <asp:DropDownList ID="drlTypeVideoPlayer" Height="32" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr style="height:20px;"></tr>
            <tr>

                <td colspan="2" class="text-center">
                    <asp:Button ID="btnSearch" CssClass="btn btn-default" runat="server" />

                </td>
            </tr>
        </table>

    </fieldset>
    <div id="toolbar-box" style="padding: 20px">
        <div class="tool-btn">
            <portal:mojoButton ID="btnAddNew" SkinID="ButtonPrimary" runat="server" />
            <portal:mojoButton ID="btnRemoveAll" SkinID="ButtonDanger" runat="server" />
            <portal:mojoButton ID="btnViewVideo" runat="server" SkinID="ButtonWarning" />
        </div>
    </div>
</div>
<asp:Panel ID="pnlPostList" runat="server">
    <asp:Repeater ID="rptMedia" runat="server" SkinID="Blog">
        <HeaderTemplate>
            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                <tr style="height: 40px">
                    <th style="width: 5%; text-align: center">
                        <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" />
                    </th>
                    <th class="tbl-header">Tiêu đề
                    </th>
                    <th class="tbl-header">Kiểu video
                    </th>
                    <th class="tbl-header">Video
                    </th>
                    <th class="tbl-header">Nổi bật
                    </th>
                    <th class="tbl-header">Xuất bản
                    </th>
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
                    <div class="name-title">
                        <a href='<%#VideoIntroduceUtils.VideoDetailItemUrl(SiteRoot,PageId,ModuleId,int.Parse(Eval("ItemID").ToString()), Eval("ItemUrl").ToString()) %>' title='<%#Eval("Title") %>'>
                            <%#Eval("Title") %>
                        </a>
                    </div>
                </td>
                <td>
                    <video style="width:200px; height: 100px;" controls src="<%#Eval("ItemUrl") %>" >

                    </video>
                    
                </td>
                <td>
                    <%#GetStateTypeVideo(int.Parse(Eval("TypePlayer").ToString()))%>

                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="imagebuttonIsHot" runat="server" ImageUrl='<%# mojoPortal.Features.VideoIntroduceUtils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsHot").ToString())) %>'
                        CommandName="EditIsHotState" CommandArgument='<%# Eval("ItemID") %>' ToolTip='<%# GetStateIsHot(bool.Parse(DataBinder.Eval(Container.DataItem,"IsHot").ToString())) %>'
                        CausesValidation="false" />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="btnChangePublic" runat="server" ImageUrl='<%# mojoPortal.Features.VideoIntroduceUtils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsPublic").ToString())) %>'
                        CommandName="EditPublic" CommandArgument='<%# Eval("ItemID") %>' ToolTip='<%# GetState(bool.Parse(DataBinder.Eval(Container.DataItem,"IsPublic").ToString())) %>'
                        CausesValidation="false" />

                </td>
                <td style="text-align: center">
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
    <asp:Panel ID="pnlVideoPager" runat="server" CssClass="ArticlePager">
        <portal:mojoCutePager ID="pgrVideo" runat="server" />
    </asp:Panel>
</asp:Panel>
<asp:Label ID="Videonull" runat="server" Visible="false"></asp:Label>
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

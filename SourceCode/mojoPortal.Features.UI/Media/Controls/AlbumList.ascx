<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AlbumList.ascx.cs" Inherits="MediaFeature.UI.AlbumList" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .fieldset input[type=text], .fieldset select {
        min-width: 90%;
        max-width: 90%;
    }
</style>
<div class="width100">
    <fieldset class="fieldset">
        <legend class="legend">Tìm kiếm thư viện ảnh</legend>
        <div class="col-sm-6 form-group">
            <label>Hiển thị</label>
            <asp:DropDownList ID="drlPublish" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
        </div>
        <div class="col-sm-6 form-group">
            <label>Sắp xếp theo</label>
            <asp:DropDownList ID="drlSearchBy" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
        </div>
        <div class="col-sm-6 form-group">
            <label>Từ khóa</label>
            <asp:TextBox ID="txtSearch" CssClass="recentlist-textbox-fix" MaxLength="220" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-6 form-group">
            <label></label>
            <portal:mojoButton ID="btnSearch" runat="server" />
        </div>
    </fieldset>

    <div style="padding: 20px 20px 20px 20px; margin: 10px 0; border: 1px solid #ddd;"
        class="width100 bg-info text-right">
        <portal:mojoButton ID="btnManageCategories" runat="server" />
        <portal:mojoButton ID="btnAddNewMediaAlbum" runat="server" />
        <portal:mojoButton ID="btnAddMultiple" runat="server" />
        <portal:mojoButton ID="btnRemoveAll" runat="server" />
    </div>
</div>

<asp:Panel ID="pnlPostList" runat="server">
    <asp:Repeater ID="rptMedia" runat="server" SkinID="Blog">
        <HeaderTemplate>
            <table id="myTable" style="width: 100%" class="table table-bordered">
                <tr>
                    <th style="width: 5%">
                        <asp:Panel runat="server" ID="Panel1" Visible='<%#AccessDelete(Convert.ToInt32(Eval("CreateByID")),Convert.ToInt32(Eval("Step"))) %>'>
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" />
                        </asp:Panel>
                    </th>
                    <th class="tbl-header" style="width: 15%">Thư viện ảnh
                    </th>
                    <th class="tbl-header" style="width: 15%; padding-left: 1%">
                        <%#Resources.MediaResources.MediaAlbumDescriptionLable.Replace(":","") %>
                    </th>
                    <th class="tbl-header" style="width: 15%">
                        <%#Resources.MediaResources.ImageTitle%>
                    </th>
                    <%--           <th class="tbl-header">
                        <%#Resources.MediaResources.MediaAlbumFeaturedTitle%>
                    </th>--%>
                    <th class="tbl-header">
                        <%#Resources.MediaResources.TotalViewsTitle%>
                    </th>
                    <th class="tbl-header">
                        <%#Resources.MediaResources.PublishTitle%>
                    </th>

                    <th style="width: 7%" class="tbl-header">
                        <%#Resources.MediaResources.MoveTitle%>
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
                <td style="vertical-align: top;">
                    <asp:ImageButton ID="ibnEditGroup" runat="server" Visible="false" ImageUrl='<%# EditLinkImageUrl %>'
                        CommandName="EditGroup" CommandArgument='<%# Eval("GroupMediaID") %>'
                        CausesValidation="false" />
                    <%#Eval("GroupName") %>
                </td>
                <td style="vertical-align: top; width: 30%; overflow: hidden">
                    <%#Eval("Description") %>
                </td>
                <td style="vertical-align: top">
                    <img width="90" height="90" src='<%# Eval("FilePath")%>' />
                </td>
                <%--                <td style="text-align: center; vertical-align: top">
                    <asp:ImageButton ID="btnFeatured" runat="server" ImageUrl='<%# mojoPortal.Features.ArticleUtils.ImageApprove(DataBinder.Eval(Container.DataItem,"IsPublish") !=null ? DataBinder.Eval(Container.DataItem,"Featured").ToString() : string.Empty) %>'
                        CommandName="Featured" CommandArgument='<%# Eval("ItemID") %>'
                        CausesValidation="false" />
                </td>--%>

                <td style="text-align: center; vertical-align: top">
                    <%#Eval("TotalView") %>
                </td>
                <td style="text-align: center; vertical-align: top">
                    <asp:ImageButton ID="btnPublish" runat="server" ImageUrl='<%# mojoPortal.Features.ArticleUtils.ImageApprove(DataBinder.Eval(Container.DataItem,"IsPublish") !=null ? DataBinder.Eval(Container.DataItem,"IsPublish").ToString() : string.Empty) %>'
                        CommandName="IsPublish" CommandArgument='<%# Eval("ItemID") %>'
                        CausesValidation="false" />
                </td>
                <td style="vertical-align: top;">
                    <asp:Panel runat="server" ID="Panel1" Visible='<%#AccessDelete(Convert.ToInt32(Eval("CreateByID")),Convert.ToInt32(Eval("Step"))) %>'>
                        <asp:ImageButton ID="btnUp"
                            ToolTip='<%# Resources.MediaResources.MoveDataTop %>'
                            AlternateText='<%# Resources.MediaResources.MoveDataTop %>'
                            ImageUrl="~/Data/SiteImages/up.gif"
                            CommandName="up"
                            runat="server"
                            CausesValidation="False"
                            CommandArgument='<%# Eval("ItemID")%>' />
                        <asp:ImageButton ID="btnDown"
                            ToolTip='<%# Resources.MediaResources.MoveDataBottom %>'
                            AlternateText='<%# Resources.MediaResources.MoveDataBottom %>'
                            ImageUrl="~/Data/SiteImages/dn.gif"
                            CommandName="down"
                            runat="server"
                            CausesValidation="False"
                            CommandArgument='<%# Eval("ItemID")%>' />
                    </asp:Panel>
                </td>
                <td style="text-align: center; vertical-align: top">
                    <%-- PostBackUrl='<%# MediaUtils.FormatEditMediaAlbumTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'!--%>
                    <asp:Panel runat="server" ID="Panel3" Visible='<%#AccessEdit(Convert.ToInt32(Eval("CreateByID")),Convert.ToInt32(Eval("Step"))) %>'>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                            CommandName="EditItem" CommandArgument='<%# Eval("ItemID") %>'
                            CausesValidation="false" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel2" Visible='<%#AccessDelete(Convert.ToInt32(Eval("CreateByID")),Convert.ToInt32(Eval("Step"))) %>'>
                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                            CommandName="DeleteItem" CommandArgument='<%# Eval("ItemID") %>'
                            CausesValidation="false" />
                    </asp:Panel>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
        </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlDonViPager" runat="server" CssClass="ArticlePager">
        <portal:mojoCutePager ID="pgrDanhBa" runat="server" />
    </asp:Panel>
</asp:Panel>
<asp:Label ID="DanhBanull" runat="server" Visible="false"></asp:Label>
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

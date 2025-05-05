<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CategoriesListAudio.ascx.cs" Inherits="AudioFeature.UI.CategoriesListAudio" %>

<%@ Import Namespace="AudioFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<div class="width100">
    <fieldset class="fieldset">
        <legend class="legend">Tìm kiếm Audio</legend>
        <table class="width100" id="haan_srearch_gallery">
            <tr>
                <td>
                    <label class="setting-label">Trạng thái phát hành</label>
                    <asp:DropDownList ID="drlPublish" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>

                </td>

                <td>
                    <label class="setting-label">Từ khóa</label>
                    <asp:TextBox ID="txtSearch" CssClass="recentlist-textbox-fix" MaxLength="220" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr style="height: 10px"></tr>
            <tr>
                <td>
                    <label class="setting-label">Người đăng</label>
                    <asp:TextBox ID="txtCreateByUser" runat="server"></asp:TextBox>
                </td>
                <td>
                    <label class="setting-label">Ngày đăng</label>
                    <mp:DatePickerControl ID="dpCreateDate" CssClass="recentlist-textbox-fix" MaxLength="220" runat="server" />

                </td>
            </tr>
            <tr style="height: 40px"></tr>
            <tr>
                <td colspan="5" class="text-right">
                    <portal:mojoButton ID="btnSearch" runat="server" />
                    <portal:mojoButton ID="btnReload" runat="server" />
                    <portal:mojoButton ID="btnAddNewMediaGroup" SkinID="ButtonPrimary" runat="server" />
                </td>
            </tr>
        </table>
    </fieldset>
    <div style="padding: 20px 20px 20px 20px; margin: 10px 0; border: 1px solid #ddd; display: none"
        class="width100 bg-info text-right">
        <portal:mojoButton ID="btnManageData" runat="server" Visible="false" />
        <portal:mojoButton ID="btnRemoveAll" runat="server" />
    </div>
</div>
<asp:Panel ID="pnlPostList" runat="server">
    <asp:Repeater ID="rptMedia" runat="server" SkinID="Blog">
        <HeaderTemplate>
            <table id="myTable" style="width: 100%; float: left; margin-top: 20px" class="table table-bordered">
                <tr>
                    <th style="width: 5%; display: none">
                        <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" />
                    </th>
                    <th class="tbl-header">Phóng sự ảnh
                    </th>
                    <th class="tbl-header">Ảnh đại diện</th>
                    <th class="tbl-header">Xuất bản</th>
                    <th style="width: 7%" class="tbl-header">
                        <%#Resources.MediaResources.MoveTitle%>
                    </th>
                    <th style="width: 5%" class="tbl-header"></th>
                </tr>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="vertical-align: top; display: none">
                    <asp:Panel runat="server" ID="all" Visible='<%#AccessDelete(Convert.ToInt32(Eval("CreateByID")),Convert.ToInt32(Eval("Step"))) %>'>
                        <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("ItemID") %>' Visible="false"></asp:Literal>
                        <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                    </asp:Panel>
                </td>
                <td id="haan_thuvienanh_row" style="vertical-align: top">
                    <a href="<%#Eval("ItemUrl").ToString().Replace("~",string.Empty) %>"><%#Eval("NameGroup") %></a>
                    <p class="haan_ngng"><span>Ngày đăng: </span><%# FormatTrainingtDate(Convert.ToDateTime(Eval("CreatedDate"))) %></p>
                    <p class="haan_ngng"><span>Người đăng: </span><%# Eval("CreatedByUser") %></p>


                </td>
                <td style="vertical-align: top">
                    <img src="/Data/File/Media/<%#Eval("FilePath") %>" width="100" height="100" />
                </td>
                <td style="text-align: center; vertical-align: top">
                    <asp:ImageButton ID="btnPublish" runat="server" ImageUrl='<%# mojoPortal.Features.ArticleUtils.ImageApprove(DataBinder.Eval(Container.DataItem,"IsPublish") !=null ? DataBinder.Eval(Container.DataItem,"IsPublish").ToString() : string.Empty) %>'
                        CommandName="IsPublish" CommandArgument='<%# Eval("ItemID") %>'
                        CausesValidation="false" Visible="false" />
                    <%# mojoPortal.Features.ArticleUtils.PublishedText(DataBinder.Eval(Container.DataItem,"IsPublish") !=null ? DataBinder.Eval(Container.DataItem,"IsPublish").ToString() : string.Empty) %>
                </td>
                <td style="text-align: center; vertical-align: top">
                    <asp:Panel runat="server" ID="Panel1" Visible='<%#AccessDelete(Convert.ToInt32(Eval("CreateByID")),Convert.ToInt32(Eval("Step"))) %>'>
                        <asp:ImageButton ID="btnUp"
                            ToolTip='<%# Resources.MediaResources.MoveCategoryTop %>'
                            AlternateText='<%# Resources.MediaResources.MoveCategoryTop %>'
                            ImageUrl="~/Data/SiteImages/up.gif"
                            CommandName="up"
                            runat="server"
                            CausesValidation="False"
                            CommandArgument='<%# Eval("ItemID")%>' />
                        <asp:ImageButton ID="btnDown"
                            ToolTip='<%# Resources.MediaResources.MoveCategoryBottom %>'
                            AlternateText='<%# Resources.MediaResources.MoveCategoryBottom %>'
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

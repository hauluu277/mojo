<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ManageCommentControl.ascx.cs" Inherits="ArticleFeature.UI.ManageCommentControl" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Import Namespace="mojoPortal.Business.WebHelpers" %>
<link href="/Data/plugins/select/fastselect/fontcss.css" rel="stylesheet" />
<link href="/Data/plugins/select/fastselect/build.min.css" rel="stylesheet" />
<link href="/Data/plugins/select/fastselect/fastselect.min.css" rel="stylesheet" />
<script src="/Data/plugins/select/fastselect/fastselect.standalone.js"></script>
<link rel="stylesheet" href="/Data/plugins/select/bootstrap-multiselect/bootstrap-multiselect.css" />
<link href="/Data/plugins/datepicker/bootstrap-datepicker-1.6.4-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<script src="/Data/plugins/datepicker/bootstrap-datepicker-1.6.4-dist/js/bootstrap-datepicker.min.js"></script>
<script src="/Data/plugins/Format/mask-number/jquery.masknumber.min.js"></script>
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

    .form-vertical .col-sm-12 {
        padding: 0;
    }

    img.tbl_img {
        width: 25%;
        float: left;
        padding-right: 2%;
    }

    .with75 {
        width: 75%;
        float: left;
    }
</style>
<div class="panel panel-border-title">
    <div class="panel-heading">
        <div>Tiêu chí tìm kiếm</div>
    </div>
    <div class="panel-body">
        <div class="form-vertical">
            <div class="form-group col-sm-12">

                <div class="col-sm-4">
                    <label class="width100">Danh mục</label>
                    <asp:DropDownList Width="100%" AutoPostBack="true" ID="ddlCategories" runat="server"></asp:DropDownList>
                </div>
                <div class="col-sm-4">
                    <div class="col-sm-6" style="padding: 0; padding-right: 5px">
                        <label class="width100">Trạng thái</label>
                        <asp:DropDownList Width="100%" AutoPostBack="true" ID="ddlState" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-sm-6" style="padding: 0; padding-left: 5px;">
                        <label class="width100">Hiển thị</label>
                        <asp:DropDownList Width="100%" AutoPostBack="true" ID="ddlPublishStatus" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <asp:UpdatePanel ID="pnlArtcleUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-4">
                            <label>Tin bài</label>
                            <asp:DropDownList Width="100%" ID="ddlArticle" runat="server"></asp:DropDownList>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlCategories" />
                        <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlState" />
                        <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlPublishStatus" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="form-group col-sm-12">
                <div class="col-sm-4">
                    <label class="width100">Ngày bình luận</label>
                    <input type="text" class="form-control date width200" style="width: 47%" name="date" id="txtStartDate" runat="server" />
                    <span style="float: left">&nbsp;-&nbsp;</span>
                    <input type="text" class="form-control date width200" style="width: 47%" name="date" runat="server" id="txtEndDate" />
                </div>
                <div class="col-sm-4">
                    <label>Bình luận</label>
                    <asp:DropDownList Width="100%" ID="ddlModeration" runat="server"></asp:DropDownList>
                </div>
                <div class="col-sm-4">
                    <label>Từ khóa</label>
                    <asp:TextBox ID="txtKeyword" Width="100%" Height="32" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-sm-12 text-center">
                <portal:mojoButton ID="btnSearch" runat="server" />
            </div>
        </div>
    </div>
</div>
<div class="module">
    <div class="module-table-body">
        <asp:UpdatePanel ID="pnlReloadComment" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Literal ID="literTotalArticle" runat="server"></asp:Literal>
                <asp:Repeater ID="rptArticles" runat="server" SkinID="Article" OnItemCommand="rptArticles_ItemCommand" OnItemDataBound="rptArticles_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered table-hover table-condensed">
                            <tr>
                                <th style="width: 10px">#</th>
                                <th style="width: 25%" class="tbl-header">Tin bài
                                </th>
                                <th style="width: 15%" class="tbl-header">Danh mục
                                </th>
                                <th style="width: 40%">Bình luận</th>
                                <th style="width: 15%" class="tbl-header">Họ tên</th>
                                <th style="width: 5%" class="tbl-header">Xuất bản
                                </th>
                                <th style="width: 20px"></th>
                            </tr>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#rptArticles.Items.Count +1 %></td>
                            <td>
                                <div class="article-title">
                                    <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ArticleItemUrl").ToString(), Convert.ToInt32(Eval("ArticleID")), PageId, ModuleId) %>' title='<%# Eval("ArticleName") %>'><%# Eval("ArticleName") %></a>
                                </div>
                            </td>
                            <td><%# Eval("CategoryName") %>
                                <%--<span class="text-primary">Lượt xem: <%#Eval("TotalView")%></span>--%>
                            </td>
                            <td><%#Eval("UserComment") %></td>
                            <td>
                                <%#Eval("UserName") %>
                                <br />
                                <span>
                                    <i class="fa fa-clock-o" aria-hidden="true"></i><%#string.Format("{0:dd/MM/yyyy HH:mm}",Eval("createdUtc")) %>
                                </span>
                            </td>
                            <td style="text-align: center">
                                 <asp:ImageButton ID="ibtnStatus" runat="server" ImageUrl='<% StatusComment(Eval("ModerationStatus")) %>'
                                    CommandName="StatusItem" CommandArgument='<%# Eval("Guid") %>' ToolTip="<%# EditLinkText %>"
                                    CausesValidation="false" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                                    CommandName="EditItem" CommandArgument='<%# Eval("Guid") %>' ToolTip="<%# EditLinkText %>"
                                    CausesValidation="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                                    CommandName="DeleteItem" CommandArgument='<%# Eval("Guid") %>' ToolTip="<%# DeleteLinkText %>"
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
                <div class="modal fade" id="modalComment" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Chỉnh sửa bình luận</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-2">Họ tên: </label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblHoTen" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-2">Email: </label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-2">Ngày đăng: </label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblCreateDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-2">Bình luận</label>
                                    <div class="col-sm-8">
                                        <%--<asp:TextBox ID="txtComment" runat="server" Rows="5" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>--%>
                                        <mpe:EditorControl ID="edComment" runat="server"></mpe:EditorControl>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:HiddenField ID="hdfCommentGuid" runat="server" />
                                <asp:Button ID="btnSavecomment" runat="server" CssClass="btn btn-success" Text="Cập nhật" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<script>
        function UpdateCommentReponse(result) {
        $("#modalComment").removeClass("in");
        $(".modal-backdrop").remove();
        $("body").removeClass("modal-open");
        $("#modalComment").modal("hide");
        if (result) {
            NotifySuccess("Cập nhật bình luận thành công!");
        } else {
            NotifyError("Bình luận không tồn tại!");
        }
    }
    function ModalUpdateComment() {
        $("#modalComment").modal("show");
    }
    $(document).ready(function () {
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
        $('.date').datepicker({
            format: 'dd/mm/yyyy',
            startDate: '01/01/2010',
            endDate: '12/30/2050'
        });
    });
</script>

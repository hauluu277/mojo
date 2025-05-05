<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostAllList.ascx.cs" Inherits="ArticleFeature.UI.PostAllList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<link href="/ClientScript/fastselect/fontcss.css" rel="stylesheet" />
<link href="/ClientScript/fastselect/build.min.css" rel="stylesheet" />
<link href="/ClientScript/fastselect/fastselect.min.css" rel="stylesheet" />
<script src="/ClientScript/fastselect/fastselect.standalone.js"></script>
<link rel="stylesheet" href="/Data/js/bootstrap-multiselect/bootstrap-multiselect.css" />
<link href="../Data/js/bootstrap-datepicker-1.6.4-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<script src="../Data/js/bootstrap-datepicker-1.6.4-dist/js/bootstrap-datepicker.min.js"></script>
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

    label {
        font-weight: normal;
    }
</style>
<fieldset class="fieldset">
    <legend class="legend">Tìm kiếm tin bài</legend>
    <%--Website--%>
    <div class="search-item">
        <label>Danh sách website</label>
        <asp:ListBox ID="lboxSiteTab_2" cssClaass="form-control" AutoPostBack="true" runat="server"></asp:ListBox>
    </div>
    <%--Danh mục--%>
    <div class="search-item">
        <label>Danh mục</label>
        <asp:UpdatePanel ID="pnlUpdateCategoryTab_2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ListBox ID="lboxCategoryTab_2" cssClaass="form-control" runat="server"></asp:ListBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lboxSiteTab_2" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Tác giả--%>
    <div class="search-item">
        <label class="search-label">Tác giả</label>
        <div class="search-control" style="float: left;">
            <asp:UpdatePanel ID="pnlUpdateCategoryTab_3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ListBox ID="lboxAuthorTab_3" cssClaass="form-control" runat="server"></asp:ListBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lboxSiteTab_2" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--Trạng thái--%>
    <div class="search-item">
        <mp:SiteLabel ID="lblStatus" runat="server" ForControl="ddlApproveStatus" CssClass="search-label"
            ConfigKey="ArticleEditApproveStatusLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
        <div class="search-control" style="float: left;">
            <asp:DropDownList Width="100%" ID="ddlApproveStatus" runat="server"></asp:DropDownList>
        </div>
    </div>
    <%--Hiển thị trang chủ--%>
    <div class="search-item">
        <mp:SiteLabel ID="SiteLabel6" runat="server" ForControl="ddlIsHome" CssClass="search-label"
            ConfigKey="IsHomeLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
        <div class="search-control" style="float: left;">
            <asp:DropDownList Width="100%" ID="ddlIsHome" runat="server"></asp:DropDownList>
        </div>
    </div>
    <%--Nổi bật--%>
    <div class="search-item">
        <mp:SiteLabel ID="SiteLabel7" runat="server" ForControl="ddlIsHot" CssClass="search-label"
            ConfigKey="IsHotLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
        <div class="search-control" style="float: left;">
            <asp:DropDownList Width="100%" ID="ddlIsHot" runat="server"></asp:DropDownList>
        </div>
    </div>
    <%--Ngày bắt đầu--%>
    <div class="search-item">
        <mp:SiteLabel ID="SiteLabel9" runat="server" ForControl="ddlIsHot" CssClass="search-label"
            ConfigKey="StartDateLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
        <input type="text" class="form-control date width200" style="width: 423px; float: left" name="date" id="txtStartDate" runat="server" />
        <span class="input-group-addon add-on addcontrol" style="width: 50px; float: left; height: 34px;">
            <i class="fa fa-calendar" aria-hidden="true"></i>
        </span>
    </div>
    <%--Ngày kết thúc--%>
    <div class="search-item">
        <mp:SiteLabel ID="SiteLabel10" runat="server" ForControl="ddlIsHot" CssClass="search-label"
            ConfigKey="EndDateLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
        <input type="text" class="form-control date width200" style="width: 423px; float: left" name="date" runat="server" id="txtEndDate" />
        <span class="input-group-addon add-on addcontrol" style="width: 50px; float: left; height: 34px;">
            <i class="fa fa-calendar" aria-hidden="true"></i>
        </span>
    </div>
    <%--Trang thái xuất bản--%>
    <div class="search-item">
        <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="ddlPublishStatus" CssClass="search-label"
            ConfigKey="ArticleEditPublishStatusLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
        <div class="search-control" style="float: left;">
            <asp:DropDownList Width="100%" ID="ddlPublishStatus" runat="server"></asp:DropDownList>
        </div>
    </div>
    <%--Từ khóa--%>
    <div class="search-item">
        <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label"
            ConfigKey="ArticleEditKeywordLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
        <div class="search-control">
            <asp:TextBox ID="txtKeyword" Width="90%" Height="34" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="searchSubmit wf100">
        <portal:mojoButton ID="btnSearch" runat="server" />
        <button type="button" class="btn btn-default" onclick="ReloadPage()">Tải lại</button>
    </div>
</fieldset>

<%-- begin toolbar-box --%>
<div id="toolbar-box">
    <div class="tool-btn">
        <portal:mojoButton ID="btnaddnew" runat="server" SkinID="ButtonPrimary" OnClick="btnaddnew_Click" />
        <input type="button" class="btn btn-success hidden" value="Chuyển trạng thái" id="btnChangeStatus4Article" name="btnChangeStatus4Article" onclick="ChangeStatus4Article();" />
        <input type="button" class="btn btn-warning hidden" value="Chuyển chuyên mục" id="btnChangeCategoryArticle" name="btnChangeCategoryArticle" onclick="ChangeCategoryArticle();" />
        <portal:mojoButton ID="btnDelAll" SkinID="ButtonDanger" runat="server" OnClick="btnDelAll_Click" />
    </div>

    <div id="dialogCat" title="Thay đổi chuyên mục tin" style="width: 100%; float: left;">
        <fieldset>
            <legend>Thay đổi chuyên mục tin</legend>

            <div class="search-item" style="width: 100%; float: left;">
                <mp:SiteLabel ID="SiteLabel8" runat="server" ForControl="ddlModuleCat" CssClass="search-label"
                    ConfigKey="ArticleChangeCatLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList CssClass="change-status" Width="40%" ID="ddlModuleCat" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item" style="width: 100%; float: left; margin-top: 10px;">
                <portal:mojoButton ID="btnChangeCat" SkinID="ButtonSuccess" runat="server" OnClick="btnChangeCat_Click" />
                <input type="button" class="btn btn-warning" value="Đóng" onclick="CloseChangeCategoryArticle();" />
            </div>
        </fieldset>
    </div>

    <div id="dialog" title="Thay đổi trạng thái" style="width: 100%; float: left;">
        <fieldset>
            <legend>Thay đổi trạng thái</legend>

            <div class="search-item" style="width: 100%; float: left;">
                <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="ddlAllStatus" CssClass="search-label"
                    ConfigKey="ArticleChangeStatusLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList CssClass="change-status" Width="40%" ID="ddlAllStatus" runat="server"></asp:DropDownList>
                </div>
            </div>
            <asp:Panel ID="pnlCommentByBoss" runat="server" CssClass="settingrow cmt-boss">
                <div class="search-item" style="width: 100%; float: left;">
                    <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="txtCommentByBoss" CssClass="search-label"
                        ConfigKey="ArticleCommentByBosLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                    <asp:TextBox ID="txtCommentByBoss" Width="100%" Height="32" TextMode="MultiLine" runat="server" MaxLength="1500" CssClass="forminput verywidetextbox">
                    </asp:TextBox>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlIsApproved" runat="server" CssClass="settingrow ispublished">
                <div class="settingrow date divThoiGian">
                   <label class="search-label">Thời gian đăng từ ngày</label>
                    <mp:jsCalendarDatePicker ID="dpBeginDate" runat="server" ShowTime="false" CssClass="datetime-input" />
                </div>
                <div class="settingrow date divThoiGian">
                   <label class="search-label">Thời gian đăng đến ngày</label>
                    <mp:jsCalendarDatePicker ID="dpEndDate" runat="server" ShowTime="false" CssClass="datetime-input" />
                </div>
            </asp:Panel>
            <div class="search-item" style="width: 100%; float: left; margin-top: 10px;">
                <portal:mojoButton ID="btnChangeStatus" SkinID="ButtonSuccess" runat="server" OnClick="btnChangeStatus_Click" />
                <input type="button" class="btn btn-warning" value="Đóng" onclick="CloseChangeStatus4Article();" />
            </div>
        </fieldset>
    </div>
</div>
<%-- end toolbar-box --%>
<asp:Panel ID="pnlArticlePager2" runat="server" CssClass="ArticlePager">
    <portal:mojoCutePager ID="pgrArticle2" runat="server" />
</asp:Panel>
<div class="module">
    <div class="module-table-body">
        <asp:Label ID="lblTotalArticle" CssClass="red" runat="server"></asp:Label>
        <asp:Repeater ID="rptArticles" runat="server" SkinID="Article" OnItemCommand="rptArticles_ItemCommand" OnItemDataBound="rptArticles_ItemDataBound">
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover table-condensed">
                    <tr>
                        <th style="width: 5%; text-align: center">
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" /></th>
                        <th style="width: 55%" class="tbl-header">
                            <%#Resources.ArticleResources.HeaderArticleTitle %>
                        </th>
                        <th style="width: 15%" class="tbl-header">
                            <%#Resources.ArticleResources.HeaderArticleCategory %>
                        </th>
                        <th style="width: 5%" class="tbl-header">
                            <%#Resources.ArticleResources.DateModified%>
                        </th>
                        <th style="width: 5%" class="tbl-header">
                            <%#Resources.ArticleResources.HeaderArticleApprove%>
                        </th>
                        <th style="width: 5%" class="tbl-header">
                            <%#Resources.ArticleResources.HeaderArticlePublish%>
                        </th>
                        <th style="width: 5%" class="tbl-header"></th>
                    </tr>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align: center">
                        <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("ItemID") %>' Visible="false"></asp:Literal>
                        <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                        <p>
                            <%# CountNumber(Container.ItemIndex + 1) %>
                        </p>
                    </td>
                    <td>
                        <div style="float: left; width: 25%; margin-right: 2%;" class='<%# "article-logo" + ModuleId + Eval("ItemID") %>' title='<%# Eval("Title") %>'>

                            <asp:Panel ID="showIMG" runat="server" Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>'>
                                <asp:Image ID="image2" runat="server" ImageUrl='<%# mojoPortal.Features.ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                    Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' Width="100%" />
                            </asp:Panel>

                        </div>
                        <div style="float: left; width: 70%;">
                            <div class="article-title">
                                <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                    ToolTip='<%# Eval("Title") %>' Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                    Visible='<%# Config.UseLinkForHeading %>' NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(GetRoot(Convert.ToInt32(Eval("SiteID")), Eval("HostName").ToString()), Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' Target=''>
                                </asp:HyperLink>
                            </div>
                            <p class="author">
                                Đăng bởi:
                                <%#Eval("CreatedByUser") %>
                               | Ngày: <%# FormatArticleDate(Convert.ToDateTime(Eval("CreatedDate"))) %>
                            </p>
                            <p class="red">
                                Site: <%#Eval("SiteName") %>
                            </p>
                            <%# Eval("Summary") %>
                    </td>
                    <td><%# Eval("CategoryName") %></td>
                    <td><%#FormatDate(Convert.ToDateTime(Eval("LastModUtc").ToString())) %></td>
                    <td style="text-align: center">
                        <asp:HyperLink ID="approveLink" data-id='<%# Eval("ItemID") %>' CssClass="pheduyetnoidung" runat="server" Text=""
                            ToolTip="" ImageUrl='<%# mojoPortal.Features.ArticleUtils.ImageApprove(DataBinder.Eval(Container.DataItem,"IsApproved") !=null ? DataBinder.Eval(Container.DataItem,"IsApproved").ToString() : string.Empty) %>' NavigateUrl="javascript:void(0);" Visible="true" />
                    </td>
                    <td style="text-align: center">
                        <asp:HyperLink ID="HyperLink1" runat="server" data-id='<%# Eval("ItemID") %>' CssClass="xuatbannoidung" Text=""
                            ToolTip="" ImageUrl='<%# mojoPortal.Features.ArticleUtils.ImageApprove(DataBinder.Eval(Container.DataItem,"IsPublished") !=null ? DataBinder.Eval(Container.DataItem,"IsPublished").ToString() : string.Empty) %>' NavigateUrl="javascript:void(0);" Visible="true" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                            CommandName="EditItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# EditLinkText %>"
                            CausesValidation="false" />
                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                            CommandName="DeleteItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# DeleteLinkText %>"
                            CausesValidation="false" />
                    </td>
                </tr>
                <input type="hidden" id="ipRole" value='<%#role %>' />
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
<div id="dialogPheDuyetNoiDung" title="Phê duyệt nội dung">
    <div class="search-item" style="width: 100%; float: left;">
        <div class="search-control">
            <input type="radio" class="rdoApproved" name="rdoApproved" value="1" checked="checked" id="rdoApproved_1" /><label for="rdoApproved_1">Phê duyệt</label>
            <input type="radio" class="rdoApproved" name="rdoApproved" value="0" id="rdoApproved_0" /><label for="rdoApproved_0">Không phê duyệt</label>
        </div>
    </div>
    <asp:Panel ID="Panel1" runat="server" CssClass="settingrow cmtboss">
        <div class="search-item" style="width: 100%; float: left;">
            <mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="txtCommentByBossApprove" CssClass="search-label"
                ConfigKey="ArticleCommentByBosLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
            <textarea id="txtCommentByBossApprove" name="txtCommentByBossApprove"></textarea>
        </div>
    </asp:Panel>
    <%--    <asp:Panel ID="Panel2" runat="server" CssClass="settingrow isapprove">
        <div class="settingrow date divThoiGian" id="_StartDate">
            <mp:SiteLabel ID="SiteLabel6" runat="server" ForControl="dpBeginDateApprove" ConfigKey="ArticleEditStartDateLabel"
                ResourceFile="ArticleResources" CssClass="search-label"></mp:SiteLabel>
            <mp:jsCalendarDatePicker ID="dpBeginDateApprove" runat="server" ShowTime="false" CssClass="datetime-input" />
        </div>
        <div class="settingrow date divThoiGian" id="_EndDate">
            <mp:SiteLabel ID="SiteLabel7" runat="server" ForControl="dpEndDateApprove" ConfigKey="ArticleEditEndDateLabel"
                ResourceFile="ArticleResources" CssClass="search-label"></mp:SiteLabel>
            <mp:jsCalendarDatePicker ID="dpEndDateApprove" runat="server" ShowTime="false" CssClass="datetime-input" />
        </div>
    </asp:Panel>--%>
</div>

<div id="dialogXuatBanNoiDung" title="Xuất bản hiển thị nội dung">
    <div class="search-item" style="width: 100%; float: left;">
        <div class="search-control">
            <input type="radio" class="rdoPublished" name="rdoPublished" value="1" checked="checked" id="rdoPublished_1" /><label for="rdoPublished_1">Cho hiển thị</label>
            <input type="radio" class="rdoPublished" name="rdoPublished" value="0" id="rdoPublished_0" /><label for="rdoPublished_0">Không cho hiển thị</label>
        </div>
    </div>
    <asp:Panel ID="Panel3" runat="server" CssClass="settingrow cmtbosspublish">
        <div class="search-item" style="width: 100%; float: left;">
            <mp:SiteLabel ID="SiteLabel3" runat="server" ForControl="txtCommentByBossPublish" CssClass="search-label"
                ConfigKey="ArticleCommentByBosLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
            <textarea id="txtCommentByBossPublish" name="txtCommentByBossPublish"></textarea>
        </div>
    </asp:Panel>
</div>
<script>
    function ReloadPage() {
        window.location.href = window.location.pathname;
    }
    $(document).ready(function () {
        $("#<%=lboxSiteTab_2.ClientID%>").fastselect({ placeholder: "Chọn website" });
        $("#<%=lboxCategoryTab_2.ClientID%>").fastselect({ placeholder: "Chọn danh mục" });
        $("#<%=lboxAuthorTab_3.ClientID%>").fastselect({ placeholder: "Chọn tác giả" });
        $("#<%=ddlApproveStatus.ClientID%>").fastselect();
        $("#<%=ddlIsHome.ClientID%>").fastselect();
        $("#<%=ddlIsHot.ClientID%>").fastselect();
        $("#<%=ddlPublishStatus.ClientID%>").fastselect();
        $('.date')
            .datepicker({
                format: 'dd/mm/yyyy',
                startDate: '01/01/2010',
                endDate: '12/30/2050'
            });
        $body = $("body");
        var itemid = 0;
        $(".cmt-boss").hide();
        $(".cmtboss").hide();
        $(".cmtbosspublish").hide();
        if ($(".change-status").val() == "1") {
            $(".ispublished").show();
        }
        else {
            $(".ispublished").hide();
        }
        $("#dialog").hide();
        $("#dialogCat").hide();
        $("#dialogPheDuyetNoiDung").dialog({
            autoOpen: false,
            height: 250,
            width: 400,
            show: {
                effect: "blind",
                duration: 500
            },
            buttons: {
                "Cập nhật": LuuPheDuyetNoiDung,
                "Đóng": function () {
                    $("#dialogPheDuyetNoiDung").dialog("close");
                }
            }
        });
        $("#dialogXuatBanNoiDung").dialog({
            autoOpen: false,
            height: 250,
            width: 400,
            show: {
                effect: "blind",
                duration: 500
            },
            buttons: {
                "Cập nhật": LuuXuatBanNoiDung,
                "Đóng": function () {
                    $("#dialogXuatBanNoiDung").dialog("close");
                }
            }
        });
        $(".rdoApproved").change(function () {
            if ($(this).val() == "1") {
                $(".isapprove").show();
                $(".cmtboss").hide();
            }
            else {
                $(".isapprove").hide();
                $(".cmtboss").show();
            }
        });
        $("#<%=btnChangeStatus.ClientID%>").click(function () {
            if ($("#<%=ddlAllStatus.ClientID%>").val() == "2" || $("#<%=ddlAllStatus.ClientID%>").val() == "4") {
                if ($("#<%=txtCommentByBoss.ClientID%>").val() == "") {
                    alert("Bạn phải nhập nhận xét");
                    return false;
                }
            }
        });
        $(".rdoPublished").change(function () {
            if ($(this).val() == "1") {
                $(".cmtbosspublish").hide();
            }
            else {
                $(".cmtbosspublish").show();
            }
        });

        $(".change-status").change(function () {
            if ($(this).val() == "2" || $(this).val() == "4") {
                $(".cmt-boss").show();
            }
            else {
                $(".cmt-boss").hide();
            }
            if ($(this).val() == "1") {
                $(".ispublished").show();
            }
            else {
                $(".ispublished").hide();
            }
        });
    });

    function ReloadListBox() {
        $("#<%=lboxCategoryTab_2.ClientID%>").fastselect({ placeholder: "Chọn danh mục" });
        $("#<%=lboxAuthorTab_3.ClientID%>").fastselect({ placeholder: "Chọn tác giả" });
    }

    $(".pheduyetnoidung").click(function () {
        itemid = $(this).attr("data-id");
        if ($(".hdfRole").val() == "1") {
            $("#dialogPheDuyetNoiDung").dialog("open");
        }
        else {
            alert("Bạn không có quyền duyệt bài viết này!");
        }
    });
    $(".xuatbannoidung").click(function () {
        itemid = $(this).attr("data-id");
        $("#dialogXuatBanNoiDung").dialog("open");
    });
    function LuuPheDuyetNoiDung() {
        var role = $("#ipRole").val();
        var isapprove = $("input[name='rdoApproved']:checked").val();
        var startdate = $("#_StartDate input").val();
        var enddate = $("#_EndDate input").val();
        var comment = $("#txtCommentByBossApprove").val();
        if (typeof isapprove == "undefined") {
            alert("Bạn phải chọn trạng thái phê duyệt");
            return;
        }
        if (isapprove != "1") {
            if (comment.trim().length == 0) {
                alert("Bạn phải nhập nhận xét cho Biên tập viên");
                return;
            }
        }
        $body.addClass("loading");
        $.ajax({
            type: "POST",
            url: "/Article/ManageAllPost.aspx/PheDuyetNoiDung",
            data: "{itemid:'" + itemid + "',isapprove:'" + isapprove + "',startdate:'" + startdate + "',enddate:'" + enddate + "',comment:'" + comment + "',role:'" + role + "'}",
            contentType: "application/json; charset=utf-8",
            datatype: "jsondata",
            async: "true",
            success: function (response) {
                $body.removeClass("loading");
                location.reload();
            },
            error: function (response) {
                $body.removeClass("loading");
                alert(response.status + ' ' + response.statusText);
            }

        });
    }

    function LuuXuatBanNoiDung() {
        var role = $("#ipRole").val();
        var ispublish = $("input[name='rdoPublished']:checked").val();
        var comment = $("#txtCommentByBossPublish").val();
        if (typeof ispublish == "undefined") {
            alert("Bạn phải chọn trạng thái xuất bản");
            return;
        }
        if (ispublish == "0") {
            if (comment.trim().length == 0) {
                alert("Bạn phải nhập nhận xét.");
                return;
            }
        }
        $body.addClass("loading");
        $.ajax({
            type: "POST",
            url: "/Article/ManageAllPost.aspx/XuatBanNoiDung",
            data: "{itemid:'" + itemid + "',ispublish:'" + ispublish + "',comment:'" + comment + "',role:'" + role + "'}",
            contentType: "application/json; charset=utf-8",
            datatype: "jsondata",
            async: "true",
            success: function (response) {
                $body.removeClass("loading");
                location.reload();
            },
            error: function (response) {
                $body.removeClass("loading");
                alert(response.status + ' ' + response.statusText);
            }

        });
    }
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
    function ChangeCategoryArticle() {
        //$("#dialog").dialog("open");
        $("#dialogCat").show("slow");
    }
    function CloseChangeCategoryArticle() {
        //$("#dialog").dialog("close");
        $("#dialogCat").hide("slow");
    }

    function ChangeStatus4Article() {
        //$("#dialog").dialog("open");
        $("#dialog").show("slow");
    }
    function CloseChangeStatus4Article() {
        //$("#dialog").dialog("close");
        $("#dialog").hide("slow");
    }
</script>
<style>
    .calendar {
        z-index: 1000;
    }

    .datetime-input input {
        width: 40% !important;
        float: left;
    }

    .datetime-input button {
        float: left;
    }

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
        color: #666;
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

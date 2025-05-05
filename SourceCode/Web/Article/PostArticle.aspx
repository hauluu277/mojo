<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="PostArticle.aspx.cs" Inherits="ArticleFeature.UI.PostArticle" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <asp:Panel ID="pnlArticle" runat="server" DefaultButton="btnUpdate" CssClass="panelwrapper admin editpage blogedit">
        <portal:ModuleTitleControl ID="moduleTitle" runat="server" RenderArtisteer="true"
            UseLowerCaseArtisteerClasses="true" Visible="true" />
        <portal:HeadingControl ID="heading" runat="server" />
        <%--Fast select plugin--%>
        <link href="/ClientScript/fastselect/fontcss.css" rel="stylesheet" />
        <link href="/ClientScript/fastselect/fastselect.min.css" rel="stylesheet" />
        <script src="/ClientScript/fastselect/fastselect.standalone.js"></script>
        <%--End fast select plugin--%>

        <link href="/Data/plugins/Pagination/pagination.css" rel="stylesheet" />
        <link href="/ClientScript/fastselect/fontcss.css" rel="stylesheet" />
        <link href="/ClientScript/fastselect/fastselect.min.css" rel="stylesheet" />
        <script src="/ClientScript/fastselect/fastselect.standalone.js"></script>
        <portal:SkinFolderScript runat="server" ScriptFileName="CommonJS" ScriptFullUrl="/Data/Script/CommonValidation.js" RenderInPlace="true" />
        <%--End fast select plugin--%>

        <link href="/Data/assets/article/css/postArticle.css" rel="stylesheet" />

        <link href="/Data/js/CropImage/css/cropper.css" rel="stylesheet" />
        <link href="/Data/js/CropImage/css/main.css" rel="stylesheet" />

        <link rel="stylesheet" href="/Data/plugins/EasySelect/EasySelectStyle.css">
        <script src="/Data/plugins/EasySelect/EasySelect.js"></script>

        <script src="/Data/plugins/Pagination/pagination.js"></script>

        <script type="text/javascript">
            //End Jcrop Cắt ảnh
            $(function () {

                $("#<%=txtTitle.ClientID%>").change(function () {
                    $("#<%=txtMetaTitle.ClientID%>").val($("#<%=txtTitle.ClientID%>").val());
                });
                $("#<%=txtSummary.ClientID%>").change(function () {
                    $("#<%=txtMetaDescription.ClientID%>").val($("#<%=txtSummary.ClientID%>").val());
                });
                $("#<%=dpAddDate2.ClientID%>").change(function () {
                    $("#<%=calenderMetaDate.ClientID%>").val($("#<%=dpAddDate2.ClientID%>").val());
                });
            })
        </script>
        <style>
            .settingrow textarea {
                width: 100% !important;
            }

            .img_avatar {
                min-height: 272px;
            }

            .parentImgDefault button i {
                margin-right: 10px;
            }

            .parentImgDefault button {
                margin-bottom: 15px;
                margin-right: 5px;
            }

            .action3 i {
                margin-right: 10px;
            }

            .pointer {
                cursor: pointer;
            }

            #lboxCategories {
                margin: 0;
                padding: 0;
                height: 40px;
            }

            .options {
                width: 50%;
            }
        </style>
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
            <div id="divChuyenMuc" style="display: none">
                <asp:Literal runat="server" ID="ddlChuyenMuc" ClientIDMode="Static"></asp:Literal>
            </div>
            <div class="modulecontent editArticle" id="frmArticle">
                <div id="divtabs" class="mojo-tabs">
                    <ul>
                        <li class="selected">
                            <asp:Literal ID="litContentTab" runat="server" /></li>
                        <li>
                            <asp:Literal ID="litMetaTab" runat="server" /></li>
                    </ul>
                    <div id="tabContent">
                        <div class="settingrow">
                            <label class="settinglabel">Chuyên mục(*)</label>
                            <asp:ListBox ID="lboxCategories" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="has-errored require form-control"></asp:ListBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblTitle" runat="server" ForControl="txtTitle" CssClass="settinglabel"
                                ConfigKey="ArticleEditTitleLabel" ResourceFile="ArticleResources"></mp:SiteLabel>

                            <input type="text" style="width: 100%" class="has-errored require form-control" id="txtTitle" name="txtTitle" runat="server" />
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblSummary" runat="server" ForControl="txtSummary" CssClass="settinglabel"
                                ConfigKey="ArticleSummaryLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtSummary" Rows="7" TextMode="MultiLine" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                        </div>
                        <div class="settingrow">
                            <mpe:EditorControl ID="edContent" runat="server">
                            </mpe:EditorControl>
                        </div>
                        <%--Đường dẫn--%>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="txtItemUrl" CssClass="settinglabel"
                                ConfigKey="ArticleEditItemUrlLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <input type="text" runat="server" name="txtItemUrl" id="txtItemUrl" style="width: 100%" class="form-control has-errored require" />
                            <span id="spnUrlWarning" runat="server" style="font-weight: normal;" class="txterror"></span>
                            <asp:HiddenField ID="hdnTitle" runat="server" />
                        </div>
                        <%--end--%>
                        <div class="settingrow">
                            <h2 class="setting-section-title">Tập tin
                                    <small>Hình ảnh cho tin bài, File audio cho tin bài, Tập tin đính kèm cho tin bài</small>
                            </h2>
                        </div>
                        <%--ẢNh đại diện--%>
                        <div class="settingrow">
                            <div class="col-sm-6" style="padding-left: 0">
                                <div class="width100 form-group">
                                    <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="txtAuthor" CssClass="settinglabel"
                                        ConfigKey="ArticleEditAuthorLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                    <asp:TextBox ID="txtAuthor" runat="server" MaxLength="255" Width="290" CssClass="forminput verywidetextbox"></asp:TextBox>
                                </div>
                                <div class="width100">
                                    <label class="settinglabel">Ảnh đại diện</label>
                                    <div style="width: calc(100% - 15em); float: left">
                                        <button id="btnAvatarLibrary" type="button" class="btn btn-info" style="padding: 6px; width: 290px; text-align: left; font-weight: bold; margin-left: 0; margin-top: 12px;">
                                            <i class="fa fa-cloud-upload" aria-hidden="true"></i>&nbsp; Chọn ảnh đại diện
                                        </button>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6" style="padding: 0; border: 1px solid #ddd; position: relative;">

                                <div class="width100" id="div_avatar" style="position: relative">
                                    <div style="float: right; position: absolute; top: 0; right: 0; display: none" id="delete_image">
                                        <a href="javascript:DeleteImg()" title="Xóa ảnh" id="isDelete" style="color: red; float: left; margin: 10px"><i class="fa fa-remove fa-lg">&nbsp; Xóa</i></a>
                                    </div>
                                    <asp:Image ID="imgJcrop" runat="server" ClientIDMode="Static" CssClass="img_avatar" />
                                </div>
                            </div>
                        </div>
                        <%--Hết mục ảnh đại diện--%>

                        <%--Tệp đính kèm--%>
                        <div class="settingrow">
                            <asp:Panel ID="pnlAttachment" runat="server" CssClass="settingrow articleReference width100">
                                <div class="settingrow">
                                    <mp:SiteLabel ID="lblAttachments" runat="server" ForControl="txtCategory" CssClass="settinglabel"
                                        ConfigKey="AttachmentsLabel" ResourceFile="ArticleResources"></mp:SiteLabel>

                                    <span class="btn btn-danger" onclick="ChooseFileReference()">
                                        <i class="fa fa-plus"></i>&nbsp;Chọn tệp đính kèm
                                    </span>
                                    <div id="fileList" style="float: right; width: calc(100% - 14em); margin-top: 20px;">
                                        <div class="panel panel-danger">
                                            <div class="panel-heading" style="padding: 5px 15px">
                                                Danh sách tệp đính kèm
                                            </div>
                                            <div class="panel-body">
                                                <table id="tblFileList" class="table table-striped">
                                                    <tbody>
                                                        <asp:Repeater ID="rptAttachments" runat="server">
                                                            <ItemTemplate>
                                                                <tr data-id="<%#Eval("FileName") %>">
                                                                    <td><a href="<%# Eval("FilePath") %>" download="<%#Eval("FileName") %>"><%#Eval("FileName") %></a></td>
                                                                    <td>
                                                                        <span style="cursor: pointer;" onclick="removeFileAtachment('<%#Eval("FileName") %>',1,<%#Eval("ItemID") %>)" title="Xóa tin đính kèm"><i class="fa fa-times" aria-hidden="true"></i></span>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <%--end--%>
                        <div class="settingrow">
                            <h2 class="setting-section-title">Tin bài liên quan
                                    <small>Tin bài liên quan, Sự kiện đính với tin bài, Tag đính với tin bài, Bình chọn đính với tin bài</small>
                            </h2>
                        </div>
                        <%--Bài viết liên quan--%>
                        <div class="settingrow articleReference" style="float: left">
                            <mp:SiteLabel ID="SiteLabel25" runat="server" ForControl="lboxArticleReference" ConfigKey="ArticleReference"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <span class="btn btn-primary" onclick="ShowArticleReference();">
                                <i class="fa fa-plus"></i>&nbsp;Chọn tin bài liên quan
                            </span>
                            <asp:ListBox ID="lboxArticleReference" Visible="false" Width="80%" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            <div id="refenreceActive">
                                <div class="panel panel-primary">
                                    <div class="panel-heading" style="padding: 5px 15px">
                                        Tin bài liên quan đã chọn
                                    </div>
                                    <div class="panel-body">
                                        <table id="tblRefenreceActive" class="table table-striped">
                                            <tbody>
                                                <asp:Repeater ID="rptArticleReference" runat="server">
                                                    <ItemTemplate>
                                                        <tr data-id='<%#Eval("ItemID") %>'>
                                                            <td><%#Eval("Title") %></td>
                                                            <td><span style='cursor: pointer;' onclick='removeRefenreceActive(<%#Eval("ItemID") %>)' title='Xóa tin bài liên quan'><i class='fa fa-times' aria-hidden='true'></i></span></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--End bài viết liên quan--%>
                        <%--Đính với tag--%>
                        <div class="settingrow tag" style="float: left; width: 100%">
                            <label class="settinglabel" style="width: 100%">Tag tin bài</label>
                            <div style="float: left; margin-bottom: 15px; width: 500px;">
                                <asp:UpdatePanel ID="uptag" runat="server">
                                    <ContentTemplate>
                                        <mp:SiteLabel ID="Sitelabel18" runat="server" ForControl="lboxTag" ConfigKey="ArticleTag"
                                            ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                        <asp:ListBox ID="lboxTag" Width="100%" ClientIDMode="Static" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnReloadTag" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div style="float: left; margin-left: 20px; width: calc(100% - 520px);">
                                <span class="btn btn-primary" aria-label="Settings" onclick="showtag()" title="Thêm mới tag bài viết">
                                    <i class="fa fa-plus" aria-hidden="true"></i>
                                </span>
                                <portal:mojoButton ID="btnReloadTag" runat="server" Text="Tải lại" SkinID="ButtonSuccess" />
                                <span class="btn btn-danger" aria-label="Settings" onclick="window.location.href='<%=SiteRoot%>/Admin/TagArticle/ArticleTag.aspx'" title="Quản lý tag bài viết">Quản lý tag bài viết
                                    <i class="fa fa-cog fa-spin "></i>
                                    <span class="sr-only">Loading...</span>
                                </span>
                            </div>
                        </div>
                        <%--end--%>
                        <%--Đính với bình chọn--%>
                        <%-- <div class="settingrow ishot">
                                <mp:SiteLabel ID="SiteLabel12" runat="server" ForControl="ddlPoll" ConfigKey="PollLabel"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <asp:DropDownList ID="ddlPoll" runat="server" Width="300"></asp:DropDownList>
                            </div>--%>
                        <%--end--%>
                        <div class="settingrow">
                            <h2 class="setting-section-title">Tùy chọn tin bài
                                    <small>Là tin bài tiêu điểm, Tin được hiển thị lên trang chủ, Cho phép hiển thị WCAG</small>
                            </h2>
                        </div>

                        <%--Tin hot--%>
                        <div class="settingrow ishot">
                            <mp:SiteLabel ID="Sitelabel15" runat="server" ForControl="chkIsHot" ConfigKey="IsHotLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <asp:CheckBox ID="chkIsHot" runat="server" CssClass="forminput" Checked="false"></asp:CheckBox>
                        </div>
                        <%-- end--%>

                        <%--Hiển thị trên trang chủ--%>
                        <div class="settingrow ishot">
                            <label for="chkIsHome" class="settinglabel">Đưa bài viết lên trang chủ</label>
                            <asp:CheckBox ID="chkIsHome" runat="server" CssClass="forminput" Checked="false" ClientIDMode="Static"></asp:CheckBox>
                        </div>
                        <%-- end--%>
                        <%--Cho phép hiển thị WCAG --%>
                        <div class="settingrow ishot">
                            <mp:SiteLabel ID="SiteLabel16" runat="server" ForControl="chkIsAllowWCAG" ConfigKey="IsAllowWCAGLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <asp:CheckBox ID="chkIsAllowWCAG" runat="server" CssClass="forminput" Checked="true"></asp:CheckBox>
                        </div>
                        <%-- end --%>
                        <%--Bình luận--%>
                        <div class="settingrow ishot">
                            <mp:SiteLabel ID="lblAllowComment" runat="server" ForControl="chkIsAllowComment" ConfigKey="IsAllowCommentLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <asp:CheckBox ID="chkIsAllowComment" runat="server" CssClass="forminput" Checked="false"></asp:CheckBox>
                        </div>
                        <%-- end--%>
                        <%--Cho phép đính vào RSS--%>
                        <div class="settingrow incluefeed" style="display: none">
                            <mp:SiteLabel ID="Sitelabel1" runat="server" ForControl="chkIncludeInFeed" ConfigKey="BlogEditIncludeInFeedLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <asp:CheckBox ID="chkIncludeInFeed" Checked="false" runat="server" CssClass="forminput"></asp:CheckBox>
                        </div>
                        <%-- end--%>
                        <div class="settingrow">
                            <h2 class="setting-section-title">Ngày tin bài
                                    <small>Ngày đăng tin, Ngày tin bài được hiển thị, Ngày hết hiệu lực hiển thị tin bài</small>
                            </h2>
                        </div>
                        <%--Ngày đăng tin--%>
                        <div class="settingrow date">
                            <mp:SiteLabel ID="SiteLabel9" runat="server" ForControl="dpAddDate" ConfigKey="ArticleEditAddDateLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <mp:DatePickerControl ID="dpAddDate2" runat="server" ShowTime="true" ClockHours="24" />
                        </div>
                        <%--end--%>

                        <style>
                            .position-date-posted input {
                                width: auto !important;
                            }

                            .position-date-posted ul label {
                                cursor: pointer;
                            }
                        </style>
                        <%--Vị trí hiển thị ngày đăng bài--%>
                        <div class="settingrow position-date-posted">
                            <label class="settinglabel">Vị trí hiển thị ngày đăng bài</label>
                            <asp:RadioButtonList ID="ViTriHienThiNgayDang" CssClass="forminput kiemduyet" runat="server">
                                <asp:ListItem Value="left" Text="Bên trái"></asp:ListItem>
                                <asp:ListItem Value="right" Text="Bên phải"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <%--end--%>

                        <%--Thời gian hiển thị tin bài--%>
                        <div class="settingrow date divThoiGian">
                            <mp:SiteLabel ID="lblStartDate" runat="server" ForControl="dpBeginDate" ConfigKey="ArticleEditStartDateLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <mp:DatePickerControl ID="dpBeginDate2" ShowTime="true" runat="server" ClockHours="24" CssClass="has-errored required form-control" />
                            <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dpBeginDate2"
                                Display="None" CssClass="txterror" ValidationGroup="article">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="settingrow date divThoiGian">
                            <mp:SiteLabel ID="lblEndDate" runat="server" ForControl="dpEndDate" ConfigKey="ArticleEditEndDateLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <mp:DatePickerControl ID="dpEndDate2" ShowTime="true" runat="server" ClockHours="24" />
                        </div>
                        <%--end--%>
                        <div class="settingrow">
                            <h2 class="setting-section-title">Duyệt/xuất bản
                                    <small>Kiểm duyệt tin bài, Nhận xét của lãnh đạo khi kiểm duyệt tin bài, Xuất bản tin bài</small>
                            </h2>
                        </div>
                        <%--Kiểm duyệt tin bài--%>
                        <asp:Panel ID="pnlIsApproved" runat="server" CssClass="settingrow ispublished">
                            <mp:SiteLabel ID="Sitelabel13" runat="server" ForControl="rdoIsApproved" ConfigKey="IsApprovedLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <asp:RadioButtonList ID="rdoIsApproved" CssClass="forminput kiemduyet" EnableViewState="false" runat="server">
                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                            </asp:RadioButtonList>

                            <%-- end--%>
                        </asp:Panel>
                        <%--Xuất bản tin bài--%>
                        <div class="divPublished">
                            <asp:Panel ID="pnlIsPublished" runat="server" CssClass="settingrow ispublished">
                                <mp:SiteLabel ID="Sitelabel2" runat="server" ForControl="chkIsPublished" ConfigKey="IsPublishedLabel"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <asp:CheckBox ID="chkIsPublished" runat="server" CssClass="forminput"></asp:CheckBox>
                                <%-- end--%>
                            </asp:Panel>
                        </div>
                        <%--Hiển thị tác giả--%>
                        <div class="divShowAuthor">
                            <asp:Panel ID="Panel1" runat="server" CssClass="settingrow ispublished">
                                <label class="settinglabel">Hiển thị tác giả</label>
                                <asp:CheckBox ID="IsHienThiTacGia" runat="server" CssClass="forminput"></asp:CheckBox>
                            </asp:Panel>
                        </div>
                        <%-- end--%>
                        <%--Nhận xét của cấp trên--%>
                        <div class="divNhanXetCapTren settingrow" style="float: left">
                            <mp:SiteLabel ID="SiteLabel3" runat="server" ForControl="txtCommentByBoss" CssClass="settinglabel"
                                ConfigKey="CommentPostArticleLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtCommentByBoss" TextMode="MultiLine" runat="server" MaxLength="1500" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                        </div>
                        <%-- end--%>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel35" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                            <div class="forminput">
                                <NeatUpload:ProgressBar ID="progressBar" runat="server">
                                </NeatUpload:ProgressBar>
                                <portal:mojoButton ID="btnUpdate" runat="server" SkinID="ButtonSuccess" CausesValidation="false" OnClientClick="return checkSubmitArticle();" />
                                <portal:mojoButton ID="btnSaveAndPreview" SkinID="ButtonPrimary" runat="server" CausesValidation="false" OnClientClick="return checkSubmitArticle();" />&nbsp;
                                <portal:mojoButton ID="btnDelete" runat="server" SkinID="ButtonDanger" Text="Delete this item" CausesValidation="false" />
                                <asp:HyperLink ID="lnkCancel" runat="server" CssClass="cancellink" />&nbsp;
                            </div>
                            <br />
                            <portal:mojoLabel ID="lblError" runat="server" CssClass="txterror" />
                            <asp:HiddenField ID="hdnHxToRestore" runat="server" />
                            <asp:ImageButton ID="btnRestoreFromGreyBox" runat="server" />
                        </div>
                        <asp:Panel ID="pnlArticleLog" runat="server">
                            <div class="settingrow">
                                <!-- Nav tabs -->
                                <div class="tabbable-panel">
                                    <div class="tabbable-line">
                                        <ul class="nav nav-tabs ">
                                            <li class="active">
                                                <a href="#tab_default_1" data-toggle="tab" class="active">Lịch sử tin bài </a>
                                            </li>
                                            <li>
                                                <a href="#tab_default_2" data-toggle="tab">Lịch sử phiên bản tin bài </a>
                                            </li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab_default_1">
                                                <div class="logArticle">
                                                    <table class="table">
                                                        <asp:Repeater ID="rptLogArticle" runat="server">
                                                            <HeaderTemplate>
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Người xử lý</th>
                                                                        <th>Ngày đăng</th>
                                                                        <th>Ngày hiển thị</th>
                                                                        <th>Ngày hết hiệu lực</th>
                                                                        <th>Xuất bản</th>
                                                                        <th>Phê duyệt</th>
                                                                        <th>Ghi chú</th>
                                                                        <th>Ngày cập nhật</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# rptLogArticle.Items.Count+1 %></td>
                                                                    <td><%# Eval("UserName") %></td>
                                                                    <td><%# string.Format("{0:dd-MM-yyyy HH:mm}",Eval("PostDate")) %></td>
                                                                    <td><%# string.Format("{0:dd-MM-yyyy HH:mm}",Eval("StartDate")) %></td>
                                                                    <td><%# string.Format("{0:dd-MM-yyyy HH:mm}",Eval("EndDate")) %></td>
                                                                    <td>
                                                                        <%# StatePublic(Eval("IsPublic")) %>
                                                                    </td>
                                                                    <td><%# StatePublic(Eval("IsApprove")) %></td>
                                                                    <td><%# Eval("Comment") %></td>
                                                                    <td><%# string.Format("{0:dd-MM-yyyy HH:mm}",Eval("CreateDate")) %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody> 
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab_default_2">
                                                <asp:Panel ID="pnlHistory" runat="server" Visible="false">
                                                    <div class="settingrow">
                                                        <mp:SiteLabel ID="SiteLabel10" runat="server" CssClass="settinglabel" ConfigKey="VersionHistory"
                                                            ResourceFile="ArticleResources"></mp:SiteLabel>
                                                    </div>
                                                    <div class="settingrow">
                                                        <asp:UpdatePanel ID="updHx" UpdateMode="Conditional" runat="server">
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="grdHistory" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <mp:mojoGridView ID="grdHistory" runat="server" CssClass="editgrid" AutoGenerateColumns="false"
                                                                    DataKeyNames="Guid">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <%# string.Format("{0:dd/MM/yyyy HH:mm}",Eval("CreatedUtc"))%>
                                                                                <%--<%# DateTimeHelper.GetTimeZoneAdjustedDateTimeString(Eval("CreatedUtc"), timeOffset)%>--%>
                                                                                <br />
                                                                                <%# Eval("UserName") %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <%--<%# DateTimeHelper.GetTimeZoneAdjustedDateTimeString(Eval("HistoryUtc"), timeOffset)%>--%>
                                                                                <%# string.Format("{0:dd/MM/yyyy HH:mm}",Eval("HistoryUtc"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="lnkcompare" runat="server" CssClass="cblink"
                                                                                    NavigateUrl='<%# SiteRoot + "/Article/ArticleCompare.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" + itemId + "&h=" + Eval("Guid") %>'
                                                                                    Text='So sánh với phiên bản hiện tại' ToolTip='So sánh với phiên bản hiện tại'
                                                                                    DialogCloseText='<%# Resources.ArticleResources.DialogCloseLink %>' />
                                                                                <asp:Button ID="btnRestoreToEditor" runat="server" Text='Tải vào đến trình soạn thảo'
                                                                                    CommandName="RestoreToEditor" CommandArgument='<%# Eval("Guid") %>' Visible='<%#RoleAccess %>' />
                                                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger" CommandName="DeleteHistory" CommandArgument='<%# Eval("Guid") %>'
                                                                                    Visible='<%# RoleAccess %>' Text='<%# Resources.ArticleResources.DeleteHistoryButton %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </mp:mojoGridView>
                                                                <div class="modulepager">
                                                                    <portal:mojoCutePager ID="pgrHistory" runat="server" />
                                                                </div>
                                                                <div id="divHistoryDelete" runat="server" class="settingrow">
                                                                    <mp:SiteLabel ID="SiteLabel8" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                                                                    <portal:mojoButton ID="btnDeleteHistory" SkinID="ButtonWarning" runat="server" Text="" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="settingrow">
                                                        <portal:mojoLabel ID="lblErrorMessage" runat="server" CssClass="txterror" />
                                                        &nbsp;
                                                    </div>
                                                </asp:Panel>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div id="tabMeta">
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel21" runat="server" ForControl="txtMetaTitle" CssClass="settinglabel"
                                ConfigKey="MetaTitleLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtMetaTitle" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel22" runat="server" ForControl="txtMetaCreator" CssClass="settinglabel"
                                ConfigKey="MetaCreatorLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtMetaCreator" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                        </div>
                        <div class="settingrow date divThoiGian">
                            <mp:SiteLabel ID="SiteLabel24" runat="server" ForControl="calenderMetaDate" CssClass="settinglabel"
                                ConfigKey="MetaDateLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <mp:jsCalendarDatePicker ID="calenderMetaDate" runat="server" ShowTime="false" CssClass="datetime-input" />
                            <portal:mojoLabel ID="lblMetaTimeError" runat="server" CssClass="txterror" />
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel7" runat="server" ForControl="txtMetaPublisher" CssClass="settinglabel"
                                ConfigKey="MetaPublisherLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtMetaPublisher" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel6" runat="server" ForControl="txtMetaDescription" CssClass="settinglabel"
                                ConfigKey="MetaDescriptionLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtMetaDescription" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel23" runat="server" ForControl="txtMetaCreator" CssClass="settinglabel"
                                ConfigKey="MetaIdentifierLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtMetaIdentifier" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblAdditionalMetaTags" runat="server" CssClass="settinglabel" ConfigKey="MetaAdditionalLabel"
                                ResourceFile="ArticleResources"></mp:SiteLabel>
                            <portal:mojoHelpLink ID="MojoHelpLink25" runat="server" HelpKey="pagesettingsadditionalmetahelp" />
                        </div>
                        <asp:Panel ID="pnlMetaData" runat="server" CssClass="settingrow">
                            <asp:UpdatePanel ID="updMetaLinks" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mp:mojoGridView ID="grdMetaLinks" runat="server" CssClass="editgrid" AutoGenerateColumns="false"
                                        DataKeyNames="Guid" EnableTheming="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditMetaLink" runat="server" CommandName="Edit" Text='<%# Resources.ArticleResources.ContentMetaGridEditButton %>' />
                                                    <asp:ImageButton ID="btnMoveUpMetaLink" runat="server" ImageUrl='<%# Page.ResolveUrl("~/Data/SiteImages/up.gif") %>'
                                                        CommandName="MoveUp" CommandArgument='<%# Eval("Guid") %>' AlternateText='<%# Resources.ArticleResources.ContentMetaGridMoveUpButton %>'
                                                        Visible='<%# (Convert.ToInt32(Eval("SortRank")) > 3) %>' />
                                                    <asp:ImageButton ID="btnMoveDownMetaLink" runat="server" ImageUrl='<%# Page.ResolveUrl("~/Data/SiteImages/dn.gif") %>'
                                                        CommandName="MoveDown" CommandArgument='<%# Eval("Guid") %>' AlternateText='<%# Resources.ArticleResources.ContentMetaGridMoveDownButton %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%# Eval("Rel") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div class="settingrow">
                                                        <mp:SiteLabel ID="lblNameMetaRel" runat="server" ForControl="txtRel" CssClass="settinglabel"
                                                            ConfigKey="ContentMetaRelLabel" ResourceFile="ArticleResources" />
                                                        <asp:TextBox ID="txtRel" CssClass="verywidetextbox forminput" runat="server" Text='<%# Eval("Rel") %>' />
                                                        <asp:RequiredFieldValidator ID="reqMetaName" runat="server" ControlToValidate="txtRel"
                                                            ErrorMessage='<%# Resources.ArticleResources.ContentMetaLinkRelRequired %>' ValidationGroup="metalink" />
                                                    </div>
                                                    <div class="settingrow">
                                                        <mp:SiteLabel ID="lblMetaHref" runat="server" ForControl="txtHref" CssClass="settinglabel"
                                                            ConfigKey="ContentMetaMetaHrefLabel" ResourceFile="ArticleResources" />
                                                        <asp:TextBox ID="txtHref" CssClass="verywidetextbox forminput" runat="server" Text='<%# Eval("Href") %>' />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHref"
                                                            ErrorMessage='<%# Resources.ArticleResources.ContentMetaLinkHrefRequired %>'
                                                            ValidationGroup="metalink" />
                                                    </div>
                                                    <div class="settingrow">
                                                        <mp:SiteLabel ID="lblScheme" runat="server" ForControl="txtScheme" CssClass="settinglabel"
                                                            ConfigKey="ContentMetHrefLangLabel" ResourceFile="ArticleResources" />
                                                        <asp:TextBox ID="txtHrefLang" CssClass="verywidetextbox forminput" runat="server" Text='<%# Eval("HrefLang") %>' />
                                                    </div>
                                                    <div class="settingrow">
                                                        <asp:Button ID="btnUpdateMetaLink" runat="server" Text='<%# Resources.ArticleResources.ContentMetaGridUpdateButton %>'
                                                            CommandName="Update" ValidationGroup="metalink" CausesValidation="true" />
                                                        <asp:Button ID="btnDeleteMetaLink" runat="server" Text='<%# Resources.ArticleResources.ContentMetaGridDeleteButton %>'
                                                            CommandName="Delete" CausesValidation="false" />
                                                        <asp:Button ID="btnCancelMetaLink" runat="server" Text='<%# Resources.ArticleResources.ContentMetaGridCancelButton %>'
                                                            CommandName="Cancel" CausesValidation="false" />
                                                    </div>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%# Eval("Href") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </mp:mojoGridView>
                                    <div class="settingrow">
                                        <table>
                                            <tr>
                                                <td>
                                                    <portal:mojoButton ID="btnAddMetaLink" runat="server" Visible="true" />&nbsp;
                                                </td>
                                                <td>
                                                    <asp:UpdateProgress ID="prgMetaLinks" runat="server" AssociatedUpdatePanelID="updMetaLinks">
                                                        <ProgressTemplate>
                                                            <img src='<%= Page.ResolveUrl("~/Data/SiteImages/indicators/indicator1.gif") %>'
                                                                alt=' ' />
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="settingrow">
                                <asp:UpdatePanel ID="upMeta" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <mp:mojoGridView ID="grdContentMeta" runat="server" CssClass="editgrid" AutoGenerateColumns="false"
                                            DataKeyNames="Guid" EnableTheming="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEditMeta" runat="server" CommandName="Edit" Text='<%# Resources.ArticleResources.ContentMetaGridEditButton %>' />
                                                        <asp:ImageButton ID="btnMoveUpMeta" runat="server" ImageUrl='<%# Page.ResolveUrl("~/Data/SiteImages/up.gif") %>'
                                                            CommandName="MoveUp" CommandArgument='<%# Eval("Guid") %>' AlternateText='<%# Resources.ArticleResources.ContentMetaGridMoveUpButton %>'
                                                            Visible='<%# (Convert.ToInt32(Eval("SortRank")) > 3) %>' />
                                                        <asp:ImageButton ID="btnMoveDownMeta" runat="server" ImageUrl='<%# Page.ResolveUrl("~/Data/SiteImages/dn.gif") %>'
                                                            CommandName="MoveDown" CommandArgument='<%# Eval("Guid") %>' AlternateText='<%# Resources.ArticleResources.ContentMetaGridMoveDownButton %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%# Eval("Name") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <div class="settingrow">
                                                            <mp:SiteLabel ID="lblName" runat="server" ForControl="txtName" CssClass="settinglabel"
                                                                ConfigKey="ContentMetaNameLabel" ResourceFile="ArticleResources" />
                                                            <asp:TextBox ID="txtName" CssClass="verywidetextbox forminput" runat="server" Text='<%# Eval("Name") %>' />
                                                            <asp:RequiredFieldValidator ID="reqMetaName" runat="server" ControlToValidate="txtName"
                                                                ErrorMessage='<%# Resources.ArticleResources.ContentMetaNameRequired %>' ValidationGroup="meta" />
                                                        </div>
                                                        <div class="settingrow">
                                                            <mp:SiteLabel ID="lblMetaContent" runat="server" ForControl="txtMetaContent" CssClass="settinglabel"
                                                                ConfigKey="ContentMetaMetaContentLabel" ResourceFile="ArticleResources" />
                                                            <asp:TextBox ID="txtMetaContent" CssClass="verywidetextbox forminput" runat="server"
                                                                Text='<%# Eval("MetaContent") %>' />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                                                ErrorMessage='<%# Resources.ArticleResources.ContentMetaContentRequired %>' ValidationGroup="meta" />
                                                        </div>
                                                        <div class="settingrow">
                                                            <mp:SiteLabel ID="lblScheme" runat="server" ForControl="txtScheme" CssClass="settinglabel"
                                                                ConfigKey="ContentMetaSchemeLabel" ResourceFile="ArticleResources" />
                                                            <asp:TextBox ID="txtScheme" CssClass="verywidetextbox forminput" runat="server" Text='<%# Eval("Scheme") %>' />
                                                        </div>
                                                        <div class="settingrow">
                                                            <mp:SiteLabel ID="lblLangCode" runat="server" ForControl="txtLangCode" CssClass="settinglabel"
                                                                ConfigKey="ContentMetaLangCodeLabel" ResourceFile="ArticleResources" />
                                                            <asp:TextBox ID="txtLangCode" CssClass="smalltextbox forminput" runat="server" Text='<%# Eval("LangCode") %>' />
                                                        </div>
                                                        <div class="settingrow">
                                                            <mp:SiteLabel ID="lblDir" runat="server" ForControl="ddDirection" CssClass="settinglabel"
                                                                ConfigKey="ContentMetaDirLabel" ResourceFile="ArticleResources" />
                                                            <asp:DropDownList ID="ddDirection" runat="server" CssClass="forminput">
                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="ltr" Value="ltr"></asp:ListItem>
                                                                <asp:ListItem Text="rtl" Value="rtl"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="settingrow">
                                                            <asp:Button ID="btnUpdateMeta" runat="server" Text='<%# Resources.ArticleResources.ContentMetaGridUpdateButton %>'
                                                                CommandName="Update" ValidationGroup="meta" CausesValidation="true" />
                                                            <asp:Button ID="btnDeleteMeta" runat="server" Text='<%# Resources.ArticleResources.ContentMetaGridDeleteButton %>'
                                                                CommandName="Delete" CausesValidation="false" />
                                                            <asp:Button ID="btnCancelMeta" runat="server" Text='<%# Resources.ArticleResources.ContentMetaGridCancelButton %>'
                                                                CommandName="Cancel" CausesValidation="false" />
                                                        </div>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%# Eval("MetaContent") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </mp:mojoGridView>
                                        <div class="settingrow">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <portal:mojoButton ID="btnAddMeta" runat="server" Visible="false" />&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:UpdateProgress ID="prgMeta" runat="server" AssociatedUpdatePanelID="upMeta">
                                                            <ProgressTemplate>
                                                                <img src='<%= Page.ResolveUrl("~/Data/SiteImages/indicators/indicator1.gif") %>'
                                                                    alt=' ' />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="settingrow">
                                    <mp:SiteLabel ID="SiteLabel19" runat="server" CssClass="settinglabel" ConfigKey="spacer"></mp:SiteLabel>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel20" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                            <div class="forminput">
                                <portal:mojoButton ID="btnUpdate3" runat="server" Visible="false" ValidationGroup="article" />&nbsp;
                                <portal:mojoButton ID="btnDelete3" runat="server" Visible="false" CausesValidation="False" />
                                <asp:HyperLink ID="lnkCancel3" runat="server" CssClass="cancellink" />&nbsp;
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnReturnUrl" runat="server" />
            <div class="cleared">
            </div>
        </portal:mojoPanel>
    </asp:Panel>
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-lg" style="display: none; max-width: 100%; min-width: 950px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 0">
                    <h4 class="caption" style="margin: 10px; font-size: 16px; font-weight: 600; line-height: 18px; color: #444; width: 100%; text-rendering: optimizelegibility;">Chọn các tin bài liên quan</h4>
                    <button type="button" class="close" data-dismiss="modal" style="margin: 0; padding: 0; margin-right: 10px">&times;</button>
                </div>
                <div class="modal-body" style="padding: 0; height: 520px; overflow-y: auto;">
                    <div class="searchReference">
                        <div class="search-left">
                            <div class="col-sm-6">
                                <label for="ProductName">Tin bài</label>
                                <input class="entity-picker-searchterm form-control" id="ReferenceTitle" name="ProductName" type="text" value="">
                            </div>
                            <div class="col-sm-6">
                                <label for="CategoryId">Danh mục</label>
                                <asp:DropDownList runat="server" CssClass="fastselect" ClientIDMode="Static" ID="ddlCategoryReference"></asp:DropDownList>
                            </div>
                            <div class="col-sm-6 hide">
                                <label for="ddlTypeSearch">Tìm theo</label>
                                <asp:DropDownList ID="ddlTypeSearch" runat="server" CssClass="fastselect" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                            <div class="col-sm-6" style="margin-top: 10px;">
                                <div class="col-sm-2">
                                    <button type="button" class="btn btn-warning" name="SearchEntities" onclick="ReferenSearch()" data-loading-text="Loading…">
                                        <i class="fa fa-search"></i>&nbsp;Tìm kiếm
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="search-right">
                            <h3>Tin bài đã chọn</h3>
                        </div>
                    </div>
                    <ul class="live-search-list">
                    </ul>
                    <div class="contentReference">
                        <div class="entity-picker-list reference-left">
                            <div class="width100" id="articleReference"></div>
                            <div class="width100" id="pagination"></div>
                        </div>
                        <div class="reference-right" id="reference_Active">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <span style="float: left; width: 100%; text-align: left">Nhấn vào một mục để chọn hoặc bỏ chọn nó và OK để áp dụng lựa chọn.</span>
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="ArticleReferenceAtive" />
                    <button type="button" class="btn btn-warning disbled" style="border-radius: 0" onclick="ApplyProduct()" id="btnApplyRefence" data-dismiss="modal">Hoàn thành</button>
                    <button type="button" class="btn btn-default" style="border-radius: 0" data-dismiss="modal">Hủy bỏ</button>
                </div>
            </div>
        </div>
    </div>
    <div id="popupTag">
        <div class="modal fade" id="modaltag" role="dialog">
            <div class="modal-dialog modal-tag" style="display: none;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 0">
                        <h4 class="caption" style="margin: 10px; font-size: 16px; font-weight: 600; line-height: 18px; color: #444; width: 100%; text-rendering: optimizelegibility;">Thêm mới tag bài viết</h4>
                        <button type="button" class="close" data-dismiss="modal" style="margin: 0; margin-right: 10px">&times;</button>
                    </div>
                    <div class="modal-body" style="padding: 10px; height: 150px; overflow-y: auto;">
                        <table class="tbl100">
                            <tr>
                                <td style="width: 130px">
                                    <span>Tag bài viết</span>
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="txtTag" />
                                    <span class="nullerror" id="tagNull">Bạn chưa nhập tên tag bài viết</span>
                                </td>
                            </tr>
                            <tr style="height: 20px"></tr>
                            <tr>
                                <td>
                                    <span>Mô tả</span>
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="txtdesTag" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-warning disbled" disabled="disabled" style="border-radius: 0" id="btnCreateTag" onclick="CreateTag()" data-dismiss="modal">Thêm mới</button>
                        <button type="button" class="btn btn-default" style="border-radius: 0" data-dismiss="modal">Hủy bỏ</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <asp:HiddenField ID="hdfCategories" runat="server" />
    <asp:HiddenField ID="hdfFileAtachment" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdfCategoryAccess" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdfListChuyenMuc" runat="server" ClientIDMode="Static" />
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    <portal:SessionKeepAliveControl ID="ka1" runat="server" />
    <input type="hidden" id="fileName" />
    <asp:HiddenField ID="ImageName" runat="server" />
    <div class="modal fade" id="modalCropImage" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog" style="width: 80%; min-width: 80%" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalLabel">Chỉnh sửa hình ảnh</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="col-sm-9">
                        <div class="img-container">
                            <img id="image" src="/Data/Images/bdbnd/ImageMD.png">
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="action1">
                            <img style="display: none" class="rounded" id="avatar" src="/Data/Images/bdbnd/ImageMD.png" alt="avatar">
                            <button type="button" class="btn btn-primary" id="tylefr"><i class="fa fa-arrows-h"></i>Free</button>
                            <button type="button" class="btn btn-primary" id="tyle23"><i class="fa fa-arrows-h"></i>2:3</button>
                            <button type="button" class="btn btn-primary" id="tyle11"><i class="fa fa-arrows-h"></i>1:1</button>
                            <button type="button" class="btn btn-primary" id="tyle43"><i class="fa fa-arrows-h"></i>4:3</button>
                            <button type="button" class="btn btn-primary" id="tyle169"><i class="fa fa-arrows-h"></i>16:9</button>
                            <button type="button" class="btn btn-primary" id="crop3"><i class="fa fa-rotate-left"></i>Scale</button>
                            <button type="button" class="btn btn-primary" id="moveup"><i class="fa fa-arrow-up"></i>Move up</button>
                            <button type="button" class="btn btn-primary" id="movedown"><i class="fa fa-arrow-down"></i>Move down</button>
                            <button type="button" class="btn btn-primary" id="moveright"><i class="fa fa-arrow-right"></i>Move right</button>
                            <button type="button" class="btn btn-primary" id="moveleft"><i class="fa fa-arrow-left"></i>Move left</button>
                            <button type="button" class="btn btn-danger" id="reset"><i class="fa fa-refresh"></i>Reset</button>
                        </div>

                    </div>
                </div>
                <div class="modal-footer center">
                    <div class="action3">
                        <button type="button" class="btn btn-success" id="crop"><i class="fa fa-life-saver"></i>Cắt và lưu</button>
                        <button type="button" class="btn btn-primary" id="crop2"><i class="fa fa-save"></i>Không cắt và lưu</button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-close"></i>Hủy</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <textarea id="tempReference" style="display: none">
        <div class="item-wrap" data-category="945" title="{title}" data-id="{id}" >
            <div class="item {active}">
                <div class="referenceTitle">
                    <span>{title}</span>
                </div>
                <div class="referenceDate">
                    <i class="fa fa-clock-o" aria-hidden="true"></i>
                    &nbsp;<span>{startDate}</span>
                </div>
                <div class="referenceCategory">
                    <i class="published fa fa-globe xicon-active-true" aria-hidden="true"></i>
                    &nbsp;{categoryName}
                </div>
            </div>
        </div>
    </textarea>
    <script src="/Data/assets/article/script/articlePostReference.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="/Data/assets/article/script/postArticle.js"></script>--%>
    <script src="/Data/js/CropImage/js/cropper.js"></script>



    <script type="text/javascript">
        if (!String.prototype.formatAll) {
            String.prototype.formatAll = function () {
                var args;
                args = arguments;
                if (args.length === 1 && args[0] !== null && typeof args[0] === 'object') {
                    args = args[0];
                }
                return this.replace(/{([^}]*)}/g, function (match, key) {
                    return (typeof args[key] !== "undefined" ? args[key] : match);
                });
            };
        }

        String.prototype.format = function () {
            var formatted = this;
            for (var arg in arguments) {
                formatted = formatted.replace("{" + arg + "}", arguments[arg]);
            }
            return formatted;
        };


        var countChuyenMuc = -1;
        function addChuyenMuc() {
            countChuyenMuc++;
            var chuyenMucCon = "<select class='form-control'>";
            chuyenMucCon += "<option>--Chọn chuyên mục con--</option>";
            chuyenMucCon += "</select>";

            var chuyenMucCha = $("#divChuyenMuc").html();
            var append = "";
            append += `<tr id="tr_chuyenmuc_${countChuyenMuc}">`;
            append += `<td>${chuyenMucCha}</td>`;
            append += `<td><input type="checkbox"/></td>`;
            append += `<td>${chuyenMucCon}</td>`;
            append += `<td class="text-center"><input type="checkbox"/></td>`;
            //append += `<td class="text-center"><input type="checkbox"/></td>`;
            append += `<td><span onclick="removeChuyenMuc(${countChuyenMuc})" class="red pointer"><i class="fa fa-trash red" aria-hidden="true"></i></span></td>`;
            append += `</tr>`;
            $("#tblChuyenMuc tbody").append(append);
        }


        function checkSubmitArticle() {
            if (FormInvalid("frmArticle")) {
                var result = true;
                if ($("#<%=lboxCategories.ClientID%>").val() == null || $("#<%=lboxCategories.ClientID%>").val() == "") {
                    result = false;
                }

                if (result == false) {
                    NotifyError("Bạn chưa chọn chuyên mục cho tin bài !");
                    return false;
                }
                var notCategoryChild = false;
                var arrCategory = [];
                $("#<%=hdfCategories.ClientID%>").val($("#<%=lboxCategories.ClientID%>").val());
                GenderChuyenMuc();
                return true;
            }
            NotifyError("Vui lòng hoàn thiện đăng tin theo hướng dẫn !");
            return false;
        }
        function removeChuyenMuc(index, isDeleteServer) {
            if (isDeleteServer) {
                if (confirm("Bạn có chắc chắn muốn xóa?")) {
                    $("#tr_chuyenmuc_" + index).remove();
                }
            } else {
                $("#tr_chuyenmuc_" + index).remove();
            }
        }
        var ds_chuyenmuc = [];
        function GenderChuyenMuc() {
            $("#tblChuyenMuc tbody tr").each(function () {
                var categoryParentID = $(this).find("td:nth-child(1) select").val();
                var isHotCatParent = $(this).find("td:nth-child(2) input[type=checkbox]").prop("checked");
                var categoryChildID = $(this).find("td:nth-child(3) select").val();
                var isHotCatChild = $(this).find("td:nth-child(4) input[type=checkbox]").prop("checked");
                var isNotDisplayParent = $(this).find("td:nth-child(5) input[type=checkbox]").prop("checked");
                var objChuyenMuc = {
                    CategoryParentID: (categoryParentID != "" && !isNaN(categoryParentID)) ? categoryParentID : 0,
                    IsHotCatParent: isHotCatParent,
                    CategoryChildID: (categoryChildID != "" && !isNaN(categoryChildID)) ? categoryChildID : 0,
                    IsHotCatChild: isHotCatChild,
                    IsNotDisplayParent: isNotDisplayParent
                };
                if (objChuyenMuc.CategoryParentID > 0) {
                    ds_chuyenmuc.push(objChuyenMuc);
                }
            });
            $("#hdfListChuyenMuc").val(JSON.stringify(ds_chuyenmuc));
        }



        $(document).ready(function () {
            var loadCategories = $("#<%=lboxCategories.ClientID%>").easySelect({
                buttons: true, //
                search: true,
                placeholder: 'Chọn chuyên mục',
                placeholderColor: '#524781',
                selectColor: '#524781',
                itemTitle: 'Chuyên mục tin bài',
                showEachItem: true,
                width: '100%',
                dropdownMaxHeight: '90vh',
            })
            if (loadCategories) {
                //lấy lever của chuyên mục
                var listLever = [];
                $("#<%=lboxCategories.ClientID%> option:selected").each(function (index, element) {
                    var value = $(this).attr("value");
                    var text = $(this).text();
                    $("input[value=" + value + "]").click();
                });
                for (var i = 1; i < 20; i++) {
                    $("#<%=lboxCategories.ClientID%> option[data-lever=" + i + "]").each(function (index, element) {
                        var lever_search = $(this).attr("data-lever");
                        if (lever_search) {
                            var value_search = $(this).val();
                            var infoLever = {
                                value: value_search,
                                lever: lever_search
                            };
                            listLever.push(infoLever);
                        }
                    });
                }
                //chèn css dựa vào lever cho listbox
                for (var i = 0; i < listLever.length; i++) {
                    var value = listLever[i].value;
                    var lever = listLever[i].lever;
                    $("input[value=" + value + "]").parent().parent().css("margin-left", (lever * 15));
                }


            }
            ReloadTag();

            SetupFormError("frmArticle");
            var url_string = window.location.href;
            var url = new URL(url_string);
            var itemId = url.searchParams.get("ItemID");
            if (itemId == "" || itemId <= 0) {
                addChuyenMuc();
            }
            setTimeout(function () {
                var fileName = $("#<%=ImageName.ClientID%>").val();
                if (fileName == null || fileName == "") {
                    $("#delete_image").hide();
                } else {
                    $("#delete_image").show();
                }
            }, 2000);
        });
        window.addEventListener('DOMContentLoaded', function () {
            var avatar = document.getElementById('avatar');
            var image = document.getElementById('image');
            var btnAvatarLibrary = document.getElementById('btnAvatarLibrary');
            //var $alert = $('.alert');
            var $modal = $('#modalCropImage');
            var cropper;
            var file;

            $('[data-toggle="tooltip"]').tooltip();

            btnAvatarLibrary.addEventListener('click', function (e) {

                var done = function (url) {
                    btnAvatarLibrary.value = '';
                    image.src = url;
                    $("#<%=ImageName.ClientID%>").val(url);
                    //$alert.hide();
                    $modal.modal('show');
                };
                var reader;
                var url;

                var finder = new CKFinder();
                finder.inpopup = true;
                finder.defaultlanguage = 'vi';
                finder.language = 'vi';
                finder.popupfeatures = "width=900,height=900,menubar=yes,toolbar=no,modal=yes";
                finder.selectmultiple = false;
                finder.startuppath = "Images:/";
                finder.baseurl = "Images/";
                finder.resourcetype = 'Images';
                finder.selectActionFunction = function (fileurl, data, allfiles) {
                    done(data.fileUrl);
                //$("#imgJcrop").prop("src", data.fileUrl);
                //$("#txtImageUrl").val(decodeURIComponent(data.fileUrl));
              <%--  $("#<%=ImageName.ClientID%>").val(data.fileUrl);--%>
                };
                finder.popup();
            });

            $modal.on('shown.bs.modal', function () {
                cropper = new Cropper(image, {
                    aspectRatio: 16 / 9,
                    viewMode: 0,
                    cropper(event) {
                        console.log(event.detail.x);
                        console.log(event.detail.y);
                        console.log(event.detail.width);
                        console.log(event.detail.height);
                        console.log(event.detail.rotate);
                        console.log(event.detail.scaleX);
                        console.log(event.detail.scaleY);
                    }
                });
            }).on('hidden.bs.modal', function () {
                cropper.destroy();
                cropper = null;
            });

            //Thêm option
            $("#crop3").click(function () {
                cropper.rotate(45).scale(1, -1)
            })
            $("#reset").click(function () {
                cropper.reset();
            })
            $("#moveup").click(function () {
                cropper.move(0, -1);
            })
            $("#movedown").click(function () {
                cropper.move(0, 1);
            })
            $("#moveright").click(function () {
                cropper.move(1, 0);
            })
            $("#moveleft").click(function () {
                cropper.move(-1, 0);
            })
            $("#tyle169").click(function () {
                cropper.setAspectRatio(1.77777778);
            })
            $("#tyle43").click(function () {
                cropper.setAspectRatio(4 / 3);
            })
            $("#tyle23").click(function () {
                cropper.setAspectRatio(2 / 3);
            })
            $("#tyle11").click(function () {
                cropper.setAspectRatio(1 / 1);
            })
            $("#tylefr").click(function () {
                cropper.setAspectRatio(NaN);
            })

            //End thêm option
            $("#ImageName").focus(function () {
                $(".bxname .mes-note-error").css("display", "none");
            })
            document.getElementById('crop').addEventListener('click', function () {
                var initialAvatarURL;
                var canvas;
                if (cropper) {
                    canvas = cropper.getCroppedCanvas();
                    initialAvatarURL = avatar.src;
                    //avatar.src = canvas.toDataURL();
                    //$alert.removeClass('alert-success alert-warning');
                    canvas.toBlob(function (blob) {
                        var formData = new FormData();

                        formData.append('file', blob, 'hinhanh.jpg');
                        $.ajax('/Article/ShowImage.ashx', {
                            method: 'POST',
                            contentType: 'multipart/form-data',
                            data: formData,
                            processData: false,
                            contentType: false,
                            xhr: function () {
                                var xhr = new XMLHttpRequest();
                                return xhr;
                            },

                            success: function (response) {
                                $("#imgJcrop").prop("src", "/<%=ConfigurationManager.AppSettings["ArticleImagesFolder"]%>" + response);
                                $("#<%=ImageName.ClientID%>").val(response);
                                $modal.modal('hide');
                                $("#delete_image").show();

                            },

                            error: function () {
                                avatar.src = initialAvatarURL;
                                //$alert.show().addClass('alert-warning').text('Upload error');
                            },
                        });

                    });
                }
            });
            document.getElementById('crop2').addEventListener('click', function () {
                $("#imgJcrop").prop("src", image.src);
              <%--  $("#<%=ImageName.ClientID%>").val(image.src);--%>
                $modal.modal('hide');
                $("#delete_image").show();
            });
        });

        var arrFileList = [];
        function ChooseFileReference() {
            var finder = new CKFinder();
            finder.inpopup = true;
            finder.defaultlanguage = 'vi';
            finder.language = 'vi';
            finder.popupfeatures = "width=900,height=900,menubar=yes,toolbar=no,modal=yes";
            finder.selectmultiple = true;
            finder.startuppath = "Files:/";
            finder.baseurl = "/File/";
            finder.resourcetype = 'File';
            finder.selectActionFunction = function (fileurl, data, allfiles) {
                $(allfiles).each(function (index, element) {
                    var filePath = this.data.fileUrl;
                    var fileName = filePath.substring(filePath.lastIndexOf("/") + 1, filePath.length);
                    var fileInfo = {
                        filePath: filePath,
                        fileName: fileName
                    };
                    var hasExisted = false;
                    $.each(arrFileList, function (index, element) {
                        if (element.fileName == fileName) {
                            hasExisted = true;
                        }
                    });
                    if (hasExisted) return;
                    arrFileList.push(fileInfo);
                    $("#hdfFileAtachment").val(JSON.stringify(arrFileList));

                    var table = $("#tblFileList");
                    var append = "<tr data-id='" + fileName + "'>";
                    append += `<td>`;
                    append += `<a href=` + filePath + ` download='` + fileName + `'>` + fileName + `</a>`;
                    append += `<td>`;
                    append += `<span style="cursor: pointer;" onclick="removeFileAtachment('` + fileName + `',0,0)" title="Xóa tài liệu đính kèm"><i class="fa fa-times" aria-hidden="true"></i></span>`;
                    append += `</tr>`;
                    $("tbody", table).prepend(append)

                });
            };
            finder.popup();
        }

        function removeFileAtachment(fileName, isDeleteServer, itemId) {
            if (confirm("Xóa file đính kèm đã chọn ?")) {
                if (isDeleteServer) {
                    $.ajax({
                        type: "post",
                        url: "/article/postarticle.aspx/DeleteFile",
                        data: "{ 'itemid': " + itemId + " }",
                        cache: false,
                        async: true,
                        contentType: "application/json; charset=utf-8",
                        //dataType: "json",
                        success: function (res) {
                            if (res.d) {
                                $("table#tblFileList tbody tr[data-id='" + fileName + "']").remove();
                                $.each(arrFileList, function (i) {
                                    if (arrFileList[i].fileName == i) {
                                        arrFileList.splice(i, 0);
                                        $("#hdfFileAtachment").val(JSON.stringify(arrFileList));
                                        return false;
                                    }
                                });
                                NotifySuccess("Xóa tệp đính kèm thành công!");
                            } else {
                                NotifyError("Không thể thực hiện thao tác này!");

                            }
                        }, error: function (err) {
                            console.log("Đã có lỗi xảy ra");
                            console.log(err.responseText);
                        }

                    });

                } else {
                    $("table#tblFileList tbody tr[data-id='" + fileName + "']").remove();
                    $.each(arrFileList, function (i) {
                        if (arrFileList[i].fileName == i) {
                            arrFileList.splice(i, 0);
                            $("#hdfFileAtachment").val(JSON.stringify(arrFileList));
                            return false;
                        }
                    });
                }
            }

        }
        function DeleteImg() {
            if (confirm("Bạn có chắc chắn muốn xóa ảnh này?")) {
                $("#<%=ImageName.ClientID%>").val("");
                $("#delete_image").hide();
                $("#imgJcrop").prop("src", "/Data/Images/haan_noimage.jpg");
            }
        }

        function ReloadTag() {
            $("#<%=lboxTag.ClientID%>").fastselect({ placeholder: "Tag tin bài" });
        }

        $(document).ready(function () {
            <%--$("#<%=ddlCategories.ClientID%>").fastselect({ placeholder: "Danh mục tin bài" });--%>
           <%-- var categorySource = $("#<%= hdfCategoryAccess.ClientID%>").val();
            if (categorySource == "all") {
                //continude
            } else {
                var categoryList = categorySource.split(",");
                $("#<%=ddlCategories.ClientID%> option").each(function (index, element) {
                    if (categoryList.indexOf($(element).val()) == -1) {
                        $(element).attr("disabled", "disabled");
                    }
                });
            }--%>


         <%--          $("#<%=lboxArticleReference.ClientID%>").fastselect({ placeholder: "Bài viết liên quan" });--%>

            $(".divApproved input").click(function () {
                var kiemduyet = $(this).val();
                if (kiemduyet == "0") {
                    $(".divPublished").hide();
                    $(".divNhanXetCapTren textarea").focus();
                }
                else if (kiemduyet == "1") {
                    $(".divPublished").show();
                }
                else {
                    $(".divPublished").hide();
                }
            });
        });
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

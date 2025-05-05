<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPost.aspx.cs" Inherits="ArticleFeature.UI.EditPost" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <asp:Panel ID="pnlArticle" runat="server" DefaultButton="btnUpdate" CssClass="panelwrapper admin editpage blogedit">
        <portal:ModuleTitleControl ID="moduleTitle" runat="server" RenderArtisteer="true"
            UseLowerCaseArtisteerClasses="true" Visible="true" />
        <portal:HeadingControl ID="heading" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
            <%--Fast select plugin--%>
            <link href="/ClientScript/fastselect/fontcss.css" rel="stylesheet" />
            <link href="/ClientScript/fastselect/fastselect.min.css" rel="stylesheet" />
            <script src="/ClientScript/fastselect/fastselect.standalone.js"></script>
            <%--End fast select plugin--%>
            <%--Jcrop Cắt ảnh--%>
            <link href="/Data/js/jcrop/css/jquery.Jcrop.min.css" rel="stylesheet" />
            <link href="/Data/js/jcrop/css/jquery.Jcrop.css" rel="stylesheet" />
            <script src="/Data/js/jcrop/js/jquery.Jcrop.min.js"></script>
            <script src="/Data/js/jcrop/js/jquery.color.js"></script>
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
            
            <div class="modulecontent editArticle">

                <div id="divtabs" class="mojo-tabs">
                    <ul>
                        <li class="selected">
                            <asp:Literal ID="litContentTab" runat="server" /></li>
                        <li>
                            <asp:Literal ID="litMetaTab" runat="server" /></li>
                    </ul>
                    <div id="tabContent">
                        <asp:Panel ID="pnlCategories" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="settingrow articleeditcategories">
                                        <mp:SiteLabel ID="lblCat" runat="server" ForControl="txtCategory" CssClass="settinglabel"
                                            ConfigKey="ArticleEditCategoryLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                        <asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvCategory" ControlToValidate="ddlCategories" InitialValue="0" ErrorMessage="" ValidationGroup="article"></asp:RequiredFieldValidator>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblTitle" runat="server" ForControl="txtTitle" CssClass="settinglabel"
                                ConfigKey="ArticleEditTitleLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="255" Width="100%" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ValidationGroup="article" />
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
                            <asp:TextBox ID="txtItemUrl" runat="server" Enabled="false" Width="79%" MaxLength="255" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                            <span id="spnUrlWarning" runat="server" style="font-weight: normal;" class="txterror"></span>
                            <asp:HiddenField ID="hdnTitle" runat="server" />
                            <asp:RegularExpressionValidator ID="regexUrl" runat="server" ControlToValidate="txtItemUrl"
                                ValidationExpression="((~/){1}\S+)" Display="None" ValidationGroup="article" />
                        </div>
                        <%--end--%>
                        <%--Tác giả--%>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblAuthor" runat="server" ForControl="txtAuthor" CssClass="settinglabel"
                                ConfigKey="ArticleEditAuthorLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <asp:TextBox ID="txtAuthor" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                            </asp:TextBox>
                        </div>
                        <%--end--%>
                        <div class="settingrow">
                            <h2 class="setting-section-title">Tập tin
                                    <small>Hình ảnh cho tin bài, File audio cho tin bài, Tập tin đính kèm cho tin bài</small>
                            </h2>
                        </div>
                        <%--ẢNh đại diện--%>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel14" runat="server" ForControl="nuImageUrl" CssClass="settinglabel"
                                ConfigKey="ArticleEditItemImageUrlLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <NeatUpload:InputFile ID="nuImageUrl" Accept=".jpg,.png,.gif" runat="server" />
                            <br />
                            <asp:Panel ID="pnlImage" runat="server">
                            </asp:Panel>
                            <asp:Label ID="lblImageUrlError" runat="server" Style="margin-left: 14.5em; font-style: italic;" />
                        </div>
                        <div class="settingrow">
                            <asp:UpdatePanel ID="updImgDel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div runat="server" id="divImage" visible="false">
                                        <mp:SiteLabel ID="lblCurrentImage" runat="server" ForControl="imgView" CssClass="settinglabel"
                                            ConfigKey="ArticleCurrentImageLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                        <asp:Image runat="server" ID="imgView" Width="100px" Style="max-height: 60px" />
                                        <asp:ImageButton ID="btnDeleteImg" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="settingrow" style="display: none; width: 100%; float: left" id="jcropedIMG">
                            <img id="imgJcrop" style="margin-left: 200px; width: 100px; float: left" />
                            <div style="float: left; margin-left: 10px">
                                <a href="javascript:EditImg()" title="Chỉnh sửa ảnh" id="isEdit"><i class="fa fa-edit fa-lg"></i>&nbsp; Chỉnh sửa</a>
                                <br />
                                <a href="javascript:DeleteImg()" title="Xóa ảnh" id="isDelete" style="color: red; float: left; margin-top: 10px"><i class="fa fa-remove fa-lg">&nbsp; Xóa</i></a>
                            </div>
                        </div>
                        <%--Hết mục ảnh đại diện--%>
                        <%--File Audio--%>
                        <div class="settingrow" style="display: none">
                            <mp:SiteLabel ID="SiteLabel11" runat="server" ForControl="nuAudioUrl" CssClass="settinglabel"
                                ConfigKey="ArticleEditItemAudioUrlLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                            <NeatUpload:InputFile ID="nuAudioUrl" runat="server" />
                            <br />
                            <asp:Label ID="lblAudioUrlError" runat="server" Style="margin-left: 14.5em; font-style: italic;" />
                            <asp:UpdatePanel ID="updAudioDel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div runat="server" id="divAudio" visible="false">
                                        <mp:SiteLabel ID="lblCurrentAudio" runat="server" ForControl="txtImageUrl" CssClass="settinglabel"
                                            ConfigKey="ArticleCurrentAudioLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                        <span runat="server" id="TTSPanel">
                                            <object type="application/x-shockwave-flash" data="/Data/mp3player.swf" id="ttsaudioplayer" height="24" width="200">
                                                <param name="movie" value="/Data/mp3player.swf" />
                                                <param name="FlashVars" value="playerID=ttsaudioplayer&autostart=no&soundFile=<%=fileAudio%>" />
                                                <param name="quality" value="high" />
                                                <param name="menu" value="false" />
                                                <param name="wmode" value="transparent" />
                                            </object>
                                        </span>
                                        <asp:ImageButton ID="btnDeleteAudio" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%--Hết mục file Audio--%>
                        <%--Tệp đính kèm--%>
                        <div class="settingrow">
                            <asp:Panel ID="pnlAttachment" runat="server">
                                <div class="settingrow">
                                    <mp:SiteLabel ID="lblAttachments" runat="server" ForControl="txtCategory" CssClass="settinglabel"
                                        ConfigKey="AttachmentsLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                    <asp:Repeater ID="rptAttachments" runat="server">
                                        <ItemTemplate>
                                            <asp:Image ID="imgType" runat="server" AlternateText=" " ImageUrl='<%# Page.ResolveUrl("~/Data/SiteImages/Icons/unknown.png") %>' />
                                            <asp:Label ID="Label1" Text='<%# Eval("FileName") %>' runat="server" />
                                            <asp:ImageButton ID="ibtnDelete" runat="server" AlternateText=" " ImageUrl='<%# Page.ResolveUrl("~/Data/SiteImages/delete.gif") %>'
                                                CommandName="DeleteItem" CommandArgument='<%# Eval("ServerFileName") %>' />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="settingrow blogeditattachment">
                                    <mp:SiteLabel ID="SiteLabel17" runat="server" ForControl="txtCategory" CssClass="settinglabel"
                                        ConfigKey="AttachmentLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                    <asp:FileUpload ID="fileUpload" runat="server" AllowMultiple="true" Multiple="Multiple" CssClass="fleft" />
                                    <portal:mojoButton ID="btnUpload" runat="server" Text="Upload" CssClass="bfloat" SkinID="ButtonPrimary" ValidationGroup="article" />
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
                                        <table id="tblRefenreceActive" class="table table-striped"></table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--End bài viết liên quan--%>
                        <%--Đính với sự kiện--%>
                        <%-- <div class="settingrow ishot" style="float: left; display: none">
                                <div style="float: left; margin-bottom: 15px;">
                                    <asp:UpdatePanel ID="upnEvent" runat="server">
                                        <ContentTemplate>
                                            <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="lboxEvent" ConfigKey="EventLabel"
                                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                            <asp:ListBox ID="lboxEvent" Width="100%" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <%--<asp:AsyncPostBackTrigger ControlID="btnReloadEvent" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div style="float: left; margin-left: 20px">
                                    <span class="btn btn-primary" aria-label="Settings" title="Thêm mới sự kiện">
                                        <i class="fa fa-pencil " aria-hidden="true"></i>
                                    </span>
                                    <span class="btn btn-danger" aria-label="Settings" title="Quản lý sự kiện">
                                        <i class="fa fa-cog fa-spin "></i>
                                        <span class="sr-only">Loading...</span>
                                    </span>
                                </div>
                            </div>
                        --%>
                        <%--end--%>
                        <%--Đính với tag--%>
                        <div class="settingrow tag" style="float: left">
                            <div style="float: left; margin-bottom: 15px;">
                                <asp:UpdatePanel ID="uptag" runat="server" UpdateMode="Conditional">
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
                            <div style="float: left; margin-left: 20px">
                                <span class="btn btn-primary" aria-label="Settings" onclick="showtag()" title="Thêm mới tag bài viết">
                                    <i class="fa fa-plus" aria-hidden="true"></i>
                                </span>
                                <portal:mojoButton ID="btnReloadTag" runat="server" Text="Tải lại" SkinID="ButtonSuccess" />
                                <span class="btn btn-danger" aria-label="Settings" onclick="window.href='<%=SiteRoot%>/Admin/TagArticle/TagArticle.aspx'" title="Quản lý tag bài viết">
                                    <i class="fa fa-cog fa-spin "></i>
                                    <span class="sr-only">Loading...</span>
                                </span>
                            </div>
                        </div>
                        <%--end--%>
                        <%--Đính với bình chọn--%>
                        <div class="settingrow ishot">
                            <mp:SiteLabel ID="SiteLabel12" runat="server" ForControl="ddlPoll" ConfigKey="PollLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <asp:DropDownList ID="ddlPoll" runat="server" Width="300"></asp:DropDownList>
                        </div>
                        <%--end--%>
                        <div class="settingrow">
                            <h2 class="setting-section-title">Tùy chọn tin bài
                                    <small>Là tin bài tiêu điểm, Tin được hiển thị lên trang chủ, Cho phép hiển thị WCAG, Cho phép bình luận tin bài, Cho phép đính vào RSS</small>
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
                            <mp:SiteLabel ID="lblIsHome" runat="server" ForControl="chkIsHome" ConfigKey="IsHomeLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <asp:CheckBox ID="chkIsHome" runat="server" CssClass="forminput" Checked="false"></asp:CheckBox>
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
                        <div class="settingrow incluefeed">
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
                            <mp:DatePickerControl ID="dpAddDate2" runat="server" />
                        </div>
                        <%--end--%>
                        <%--Thời gian hiển thị tin bài--%>
                        <div class="settingrow date divThoiGian">
                            <mp:SiteLabel ID="lblStartDate" runat="server" ForControl="dpBeginDate" ConfigKey="ArticleEditStartDateLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <mp:DatePickerControl ID="dpBeginDate2" runat="server" />
                            <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dpBeginDate2"
                                Display="None" CssClass="txterror" ValidationGroup="article">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="settingrow date divThoiGian">
                            <mp:SiteLabel ID="lblEndDate" runat="server" ForControl="dpEndDate" ConfigKey="ArticleEditEndDateLabel"
                                ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                            <mp:DatePickerControl ID="dpEndDate2" runat="server" />
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
                            <%--Nhận xét của cấp trên--%>
                            <div class="divNhanXetCapTren settingrow" style="float: left">
                                <mp:SiteLabel ID="SiteLabel3" runat="server" ForControl="txtCommentByBoss" CssClass="settinglabel"
                                    ConfigKey="ArticleCommentByBosLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <asp:TextBox ID="txtCommentByBoss" TextMode="MultiLine" runat="server" MaxLength="1500" CssClass="forminput verywidetextbox">
                                </asp:TextBox>
                            </div>
                            <%-- end--%>
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
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel35" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                            <div class="forminput">
                                <NeatUpload:ProgressBar ID="progressBar" runat="server">
                                </NeatUpload:ProgressBar>
                                <portal:mojoButton ID="btnUpdate" runat="server" SkinID="ButtonSuccess" ValidationGroup="article" />
                                <portal:mojoButton ID="btnSaveAndPreview" SkinID="ButtonPrimary" runat="server" ValidationGroup="article" />&nbsp;
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
                                                                        <%# StatePublic(Eval("IsPublic").ToString()) %>
                                                                    </td>
                                                                    <td><%# StatePublic(Eval("IsApprove").ToString()) %></td>
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
        <div class="modal-dialog modal-lg" style="display: none; width: 90%; max-width: 100%">
            <div class="modal-content">
                <div class="modal-header" style="padding: 0">
                    <h4 class="caption" style="margin: 10px; font-size: 16px; font-weight: 600; line-height: 18px; color: #444; width: 100%; text-rendering: optimizelegibility;">Chọn các tin bài liên quan</h4>
                    <button type="button" class="close" data-dismiss="modal" style="margin: 0; margin-right: 10px">&times;</button>
                </div>
                <div class="modal-body" style="padding: 0; height: 470px; overflow-y: auto;">
                    <div class="searchReference">
                        <div class="form-horizontal">
                            <div class="control-group search-reference">
                                <label class="control-label" for="ProductName">Tin bài</label>
                                <div class="controls">
                                    <input class="entity-picker-searchterm form-control" id="ReferenceTitle" name="ProductName" type="text" value="">
                                    <button type="button" class="btn btn-warning" name="SearchEntities" data-loading-text="Loading…">
                                        <i class="fa fa-search"></i>&nbsp;Tìm kiếm
                                    </button>
                                    <button type="button" class="btn" onclick="fillterReference()">
                                        <i class="fa fa-filter"></i>&nbsp;Lọc
                                    </button>
                                </div>
                            </div>


                            <div class="entity-picker-filter" style="display: none; margin-top: 20px">
                                <div class="control-group">
                                    <label class="control-label" for="CategoryId">Danh mục</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlCategoryReference"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <ul class="live-search-list">
                    </ul>
                    <div class="contentReference">
                        <div class="cssload-thecube loadingReference" style="display: none;">
                            <div class="cssload-cube cssload-c1"></div>
                            <div class="cssload-cube cssload-c2"></div>
                            <div class="cssload-cube cssload-c4"></div>
                            <div class="cssload-cube cssload-c3"></div>
                        </div>
                        <div class="entity-picker-list" id="articleReference"></div>
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
    <asp:HiddenField ID="hdfReference" ClientIDMode="Static" runat="server" />
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    <portal:SessionKeepAliveControl ID="ka1" runat="server" />
    <input type="hidden" id="x" />
    <input type="hidden" id="y" />
    <input type="hidden" id="x2" />
    <input type="hidden" id="y2" />
    <input type="hidden" id="w" />
    <input type="hidden" id="h" />
    <input type="hidden" id="fileName" />
    <asp:HiddenField ID="ImageName" runat="server" />
    <div id="jcropimg" class="jcrop-modal">
        <div class="jcrop-modal-content">
            <div class="modal-header">
                <h2>Hiệu chỉnh hình ảnh
                        <span class="close">&times;</span>
                </h2>
            </div>
            <div class="modal-body">
                <div class="body-content">
                    <img id="target" alt="[Jcrop Example]" />
                    <div id="preview-pane">
                        <div class="preview-container">
                            <img id="imgPopup" class="jcrop-preview" alt="Preview" />
                        </div>
                    </div>
                </div>
                <div class="body-right">
                    <div class="submit-bottom">
                        <asp:Button ID="btnCrop" CssClass="btn btn-success" runat="server" OnClientClick="return SaveIMG()" UseSubmitBehavior="false" Text="Cắt ảnh" />
                        <a href="javascript:void(0)" class="btn btn-danger btncancel">Hủy</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="EditPostReference.js"></script>
    <script type="text/javascript">

        var reloadJcrop = "";
        reloadJcrop += "<img id='target' alt='[Jcrop Example]' />";
        reloadJcrop += "<div id='preview-pane'>";
        reloadJcrop += "<div class='preview-container'>";
        reloadJcrop += "<img id='imgPopup' class='jcrop-preview' alt='Preview' />";
        reloadJcrop += "</div>";
        reloadJcrop += "</div>";

        var modal = document.getElementById('jcropimg');

        // Get the button that opens the modal
        var btn = document.getElementById("myBtn");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
            $("#<%=nuImageUrl.ClientID%>").val("");
            $(".body-content").html(reloadJcrop);
        }
        var btncancel = document.getElementsByClassName("btncancel")[0];
        btncancel.onclick = function () {
            modal.style.display = "none";
            $("#<%=nuImageUrl.ClientID%>").val("");
            $(".body-content").html(reloadJcrop);
        }
        // When the user clicks anywhere outside of the modal, close it
        //window.onclick = function (event) {
        //    if (event.target == modal) {
        //        modal.style.display = "none";
        //        $(".body-content").html(reloadJcrop);
        //    }
        //}
        function EditImg() {
            $("#target").attr("src", "/<%=ConfigurationManager.AppSettings["ArticleImagesFolder"]%>" + $("#fileName").val() + "");
            $("#imgPopup").attr("src", "/<%=ConfigurationManager.AppSettings["ArticleImagesFolder"]%>" + $("#fileName").val() + "");
            modal.style.display = "block";
            // Create variables (in this scope) to hold the API and image size
            var jcrop_api,
                boundx,
                boundy,
                // Grab some information about the preview pane
                $preview = $('#preview-pane'),
                $pcnt = $('#preview-pane .preview-container'),
                $pimg = $('#preview-pane .preview-container img'),

                xsize = $pcnt.width(),
                ysize = $pcnt.height();
            var x_Old = $("#x").val();
            var y_Old = $("#y").val();
            var x2_Old = $("#x2").val();
            var y2_Old = $("#y2").val();
            $('#target').Jcrop({
                bgFade: true,
                onChange: updatePreview,
                onSelect: updateSelect,
                aspectRatio: xsize / ysize
            }, function () {
                // Use the API to get the real image size
                var bounds = this.getBounds();
                boundx = bounds[0];
                boundy = bounds[1];
                // Store the API in the jcrop_api variable
                jcrop_api = this;
                jcrop_api.animateTo([x_Old, y_Old, x2_Old, y2_Old]);
                jcrop_api.setOptions({ allowResize: false });
                jcrop_api.setOptions({ allowSelect: false });
                // Move the preview into the jcrop container for css positioning
                $preview.appendTo(jcrop_api.ui.holder);
            });
            function updateSelect(c) {

                $('#x').val(parseInt(c.x));
                $('#y').val(parseInt(c.y));
                $('#x2').val(parseInt(c.x2));
                $('#y2').val(parseInt(c.y2));
                $('#w').val(parseInt(c.w));
                $('#h').val(parseInt(c.h));
            };
            function updatePreview(c) {
                if (parseInt(c.w) > 0) {
                    var rx = xsize / c.w;
                    var ry = ysize / c.h;

                    $pimg.css({
                        width: Math.round(rx * boundx) + 'px',
                        height: Math.round(ry * boundy) + 'px',
                        marginLeft: '-' + Math.round(rx * c.x) + 'px',
                        marginTop: '-' + Math.round(ry * c.y) + 'px'
                    });
                }

                $('#x').val(parseInt(c.x));
                $('#y').val(parseInt(c.y));
                $('#x2').val(parseInt(c.x2));
                $('#y2').val(parseInt(c.y2));
                $('#w').val(parseInt(c.w));
                $('#h').val(parseInt(c.h));
            };
        }
        function DeleteImg() {
            $("#jcropedIMG").hide();
            $("#imgJcrop").attr("src", "");
            $("#<%=ImageName.ClientID%>").val("");
            modal.style.display = "none";
            $("#<%=nuImageUrl.ClientID%>").val("");
            $(".body-content").html(reloadJcrop);
        }
        function SaveIMG() {
            $.ajax({
                type: 'POST',
                cache: false,
                async: false,
                url: "/Article/Editpost.aspx/SaveJcrop",
                data: "{ fileName: '" + $("#fileName").val() + "', w: '" + $('#w').val() + "', h: '" + $('#h').val() + "', x: '" + $('#x').val() + "', y: '" + $('#y').val() + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#<%=ImageName.ClientID%>").val(data.d);
                    $("#imgJcrop").attr("src", "/<%=ConfigurationManager.AppSettings["ArticleImagesFolder"]%>" + data.d + "");
                    $("#jcropedIMG").show();
                    $("#jcropimg").hide();
                    $(".body-content").html(reloadJcrop);
                },
                error: function (er) {

                }

            });
        }
        function IsValidPreview(files) {
            var reader = new FileReader();
            var img = new Image();


            reader.onload = function (e) {
                img.src = e.target.result;
                fileSize = Math.round(files.size / 1024);
                img.onload = function () {
                    if (this.width < 500) {
                        return false;
                    }
                };

            };
            reader.readAsDataURL(files);
            return true;
        }
        var sizeValid = true;
        function ReloadTag() {
            $("#<%=lboxTag.ClientID%>").fastselect({ placeholder: "Tag tin bài" });
        }

        $(document).ready(function () {
            $("#<%=ddlCategories.ClientID%>").fastselect({ placeholder: "Danh mục tin bài" });
            $("#<%=lboxTag.ClientID%>").fastselect({ placeholder: "Tag tin bài", clearQueryOnSelect: true });

            $("#<%=lboxArticleReference.ClientID%>").fastselect({ placeholder: "Bài viết liên quan" });
            $("#<%=ddlPoll.ClientID%>").fastselect({ placeholder: "Bình chọn bài viết" });

            $("#<%=nuImageUrl.ClientID%>").on('change', function () {
                var fileExtentsion = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
                var files = $("#<%=nuImageUrl.ClientID%>").get(0).files;
                if (files.length > 0) {
                    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtentsion) == -1) {
                        alert("Ảnh tải lên sai định dạng, bạn vui lòng tải lên đúng định dạng cho phép(định dạng file cho phép .jpeg, .jpg, .gif)");
                        $("#<%=nuImageUrl.ClientID%>").val("");
                        return false;
                    }
                    var _URL = window.URL || window.webkitURL;
                    var image, file;
                    file = files[0];
                    img = new Image();
                    img.onload = function () {
 <%--                       if (this.width < 500 || this.height < 300) {
                    $("#<%=nuImageUrl.ClientID%>").val("");
                            alert("Ảnh tải lên kích thước quá nhỏ, bạn vui lòng chọn ảnh có kích thước phù hợp (540*300)");
                            return false;

                        } else {--%>
                        if (parseFloat(files[0].size / 1024).toFixed(2) > 4024) {
                            alert("Ảnh tải lên đã vượt kích thước cho phép, bạn vui lòng tải lên kích thước cho phép (kích thước ảnh hợp lệ < 2MB)");
                            $("#<%=nuImageUrl.ClientID%>").val("");
                            return false;
                        }
                        var uploadfiles = $("#<%=nuImageUrl.ClientID%>").get(0);
                        var uploadedfiles = uploadfiles.files;
                        var fromdata = new FormData();
                        for (var i = 0; i < uploadedfiles.length; i++) {
                            fromdata.append(uploadedfiles[i].name, uploadedfiles[i]);
                        }
                        //if (this.width == 500 || this.height == 300) {
                        if (this.width > 100) {
                            $.ajax({
                                url: '/Article/ShowImage.ashx',
                                type: 'POST',
                                contentType: 'multipart/form-data',
                                data: fromdata,
                                success: function (status) {
                                    $("#fileName").val(status);
                                    $("#<%=ImageName.ClientID%>").val(status);
                                        $("#imgJcrop").attr("src", "/<%=ConfigurationManager.AppSettings["ArticleImagesFolder"]%>" + status + "");
                                        $("#jcropedIMG").show();
                                        $("#isEdit").hide();
                                    },
                                    processData: false,
                                    contentType: false,
                                    error: function () {
                                        alert("Whoops something went wrong!");
                                    }
                                });
                            } else {
                                $.ajax({
                                    url: '/Article/ShowImage.ashx',
                                    type: 'POST',
                                    contentType: 'multipart/form-data',
                                    data: fromdata,
                                    success: function (status) {
                                        $("#fileName").val(status);
                                        $("#<%=ImageName.ClientID%>").val(status);
                                        //$("#jcropimg").addClass("jcrop-modal");
                                        var modal = document.getElementById('jcropimg');
                                        $("#target").attr("src", "/<%=ConfigurationManager.AppSettings["ArticleImagesFolder"]%>" + status + "");
                                        $("#imgPopup").attr("src", "/<%=ConfigurationManager.AppSettings["ArticleImagesFolder"]%>" + status + "");
                                        //$(".jcrop-holder img").attr("src", "/<%=ConfigurationManager.AppSettings["ArticleImagesFolder"]%>" + status + "");
                                        modal.style.display = "block";
                                        // Create variables (in this scope) to hold the API and image size
                                        var jcrop_api,
                                            boundx,
                                            boundy,
                                            // Grab some information about the preview pane
                                            $preview = $('#preview-pane'),
                                            $pcnt = $('#preview-pane .preview-container'),
                                            $pimg = $('#preview-pane .preview-container img'),

                                            xsize = $pcnt.width(),
                                            ysize = $pcnt.height();

                                        $('#target').Jcrop({
                                            bgFade: true,
                                            onChange: updatePreview,
                                            onSelect: updateSelect,
                                            //aspectRatio: xsize / ysize
                                        }, function () {
                                            // Use the API to get the real image size
                                            var bounds = this.getBounds();
                                            boundx = bounds[0];
                                            boundy = bounds[1];
                                            // Store the API in the jcrop_api variable
                                            jcrop_api = this;
                                            //jcrop_api.animateTo([37, 74, 577, 375]);
                                            //jcrop_api.setSelect(0, 0, parseInt(<%=ConfigurationManager.AppSettings["ArticleImageThumbnailWidth540"]%>), parseInt(<%=ConfigurationManager.AppSettings["ArticleImageThumbnailHeight302"]%>));
                                            jcrop_api.animateTo([37, 74, 577, 375]);
                                            jcrop_api.setOptions({ allowResize: true });
                                            jcrop_api.setOptions({ allowSelect: false });
                                            // Move the preview into the jcrop container for css positioning
                                            $preview.appendTo(jcrop_api.ui.holder);
                                        });
                                    function updateSelect(c) {

                                        $('#x').val(parseInt(c.x));
                                        $('#y').val(parseInt(c.y));
                                        $('#x2').val(parseInt(c.x2));
                                        $('#y2').val(parseInt(c.y2));
                                        $('#w').val(parseInt(c.w));
                                        $('#h').val(parseInt(c.h));

                                    };
                                    function updatePreview(c) {
                                        if (parseInt(c.w) > 0) {
                                            var rx = xsize / c.w;
                                            var ry = ysize / c.h;

                                            $pimg.css({
                                                width: Math.round(rx * boundx) + 'px',
                                                height: Math.round(ry * boundy) + 'px',
                                                marginLeft: '-' + Math.round(rx * c.x) + 'px',
                                                marginTop: '-' + Math.round(ry * c.y) + 'px'
                                            });
                                        }
                                        $('#x').val(parseInt(c.x));
                                        $('#y').val(parseInt(c.y));
                                        $('#x2').val(parseInt(c.x2));
                                        $('#y2').val(parseInt(c.y2));
                                        $('#w').val(parseInt(c.w));
                                        $('#h').val(parseInt(c.h));
                                    };
                                    console.log(status);
                                    if (status != 'error') {
                                        var my_path = "MediaUploader/" + status;
                                        $("#myUploadedImg").attr("src", my_path);
                                    }
                                },
                                processData: false,
                                contentType: false,
                                error: function () {
                                    alert("Whoops something went wrong!");
                                }
                            });
                        }
                        //}
                    };
                    img.onerror = function () {
                        alert("not a valid file: " + file.type);
                    };
                    img.src = _URL.createObjectURL(file);

                }
            });
            var kiemduyet = $(".box-inline input[type='radio']:checked").val();
            if (kiemduyet == "1") {
                $(".divPublished").show();
            }
            else {
                $(".divPublished").hide();
            }

            $(".box-inline input").click(function () {
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
    <style>
        .panel-heading {
            padding: 3px 15px;
        }

        #refenreceActive {
            padding-left: 200px;
            margin-top: 10px;
            width: 100%;
            float: left;
            display: none;
        }

        .divPublished {
            display: none;
        }

        .cssload-thecube {
            width: 75px;
            height: 75px;
            margin: 0 auto;
            margin-top: 155px;
            position: relative;
            transform: rotateZ(45deg);
            -o-transform: rotateZ(45deg);
            -ms-transform: rotateZ(45deg);
            -webkit-transform: rotateZ(45deg);
            -moz-transform: rotateZ(45deg);
        }

            .cssload-thecube .cssload-cube {
                position: relative;
                transform: rotateZ(45deg);
                -o-transform: rotateZ(45deg);
                -ms-transform: rotateZ(45deg);
                -webkit-transform: rotateZ(45deg);
                -moz-transform: rotateZ(45deg);
            }

            .cssload-thecube .cssload-cube {
                float: left;
                width: 50%;
                height: 50%;
                position: relative;
                transform: scale(1.1);
                -o-transform: scale(1.1);
                -ms-transform: scale(1.1);
                -webkit-transform: scale(1.1);
                -moz-transform: scale(1.1);
            }

                .cssload-thecube .cssload-cube:before {
                    content: "";
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;
                    background-color: rgb(43,160,199);
                    animation: cssload-fold-thecube 2.76s infinite linear both;
                    -o-animation: cssload-fold-thecube 2.76s infinite linear both;
                    -ms-animation: cssload-fold-thecube 2.76s infinite linear both;
                    -webkit-animation: cssload-fold-thecube 2.76s infinite linear both;
                    -moz-animation: cssload-fold-thecube 2.76s infinite linear both;
                    transform-origin: 100% 100%;
                    -o-transform-origin: 100% 100%;
                    -ms-transform-origin: 100% 100%;
                    -webkit-transform-origin: 100% 100%;
                    -moz-transform-origin: 100% 100%;
                }

            .cssload-thecube .cssload-c2 {
                transform: scale(1.1) rotateZ(90deg);
                -o-transform: scale(1.1) rotateZ(90deg);
                -ms-transform: scale(1.1) rotateZ(90deg);
                -webkit-transform: scale(1.1) rotateZ(90deg);
                -moz-transform: scale(1.1) rotateZ(90deg);
            }

            .cssload-thecube .cssload-c3 {
                transform: scale(1.1) rotateZ(180deg);
                -o-transform: scale(1.1) rotateZ(180deg);
                -ms-transform: scale(1.1) rotateZ(180deg);
                -webkit-transform: scale(1.1) rotateZ(180deg);
                -moz-transform: scale(1.1) rotateZ(180deg);
            }

            .cssload-thecube .cssload-c4 {
                transform: scale(1.1) rotateZ(270deg);
                -o-transform: scale(1.1) rotateZ(270deg);
                -ms-transform: scale(1.1) rotateZ(270deg);
                -webkit-transform: scale(1.1) rotateZ(270deg);
                -moz-transform: scale(1.1) rotateZ(270deg);
            }

            .cssload-thecube .cssload-c2:before {
                animation-delay: 0.35s;
                -o-animation-delay: 0.35s;
                -ms-animation-delay: 0.35s;
                -webkit-animation-delay: 0.35s;
                -moz-animation-delay: 0.35s;
            }

            .cssload-thecube .cssload-c3:before {
                animation-delay: 0.69s;
                -o-animation-delay: 0.69s;
                -ms-animation-delay: 0.69s;
                -webkit-animation-delay: 0.69s;
                -moz-animation-delay: 0.69s;
            }

            .cssload-thecube .cssload-c4:before {
                animation-delay: 1.04s;
                -o-animation-delay: 1.04s;
                -ms-animation-delay: 1.04s;
                -webkit-animation-delay: 1.04s;
                -moz-animation-delay: 1.04s;
            }



        @keyframes cssload-fold-thecube {
            0%, 10% {
                transform: perspective(140px) rotateX(-180deg);
                opacity: 0;
            }

            25%, 75% {
                transform: perspective(140px) rotateX(0deg);
                opacity: 1;
            }

            90%, 100% {
                transform: perspective(140px) rotateY(180deg);
                opacity: 0;
            }
        }

        @-o-keyframes cssload-fold-thecube {
            0%, 10% {
                -o-transform: perspective(140px) rotateX(-180deg);
                opacity: 0;
            }

            25%, 75% {
                -o-transform: perspective(140px) rotateX(0deg);
                opacity: 1;
            }

            90%, 100% {
                -o-transform: perspective(140px) rotateY(180deg);
                opacity: 0;
            }
        }

        @-ms-keyframes cssload-fold-thecube {
            0%, 10% {
                -ms-transform: perspective(140px) rotateX(-180deg);
                opacity: 0;
            }

            25%, 75% {
                -ms-transform: perspective(140px) rotateX(0deg);
                opacity: 1;
            }

            90%, 100% {
                -ms-transform: perspective(140px) rotateY(180deg);
                opacity: 0;
            }
        }

        @-webkit-keyframes cssload-fold-thecube {
            0%, 10% {
                -webkit-transform: perspective(140px) rotateX(-180deg);
                opacity: 0;
            }

            25%, 75% {
                -webkit-transform: perspective(140px) rotateX(0deg);
                opacity: 1;
            }

            90%, 100% {
                -webkit-transform: perspective(140px) rotateY(180deg);
                opacity: 0;
            }
        }

        @-moz-keyframes cssload-fold-thecube {
            0%, 10% {
                -moz-transform: perspective(140px) rotateX(-180deg);
                opacity: 0;
            }

            25%, 75% {
                -moz-transform: perspective(140px) rotateX(0deg);
                opacity: 1;
            }

            90%, 100% {
                -moz-transform: perspective(140px) rotateY(180deg);
                opacity: 0;
            }
        }
    </style>
    <style>
        .settingrow {
            width: 100%;
            /*float: left;*/
        }

        .date input {
            float: left;
            width: 200px !important;
        }

        .divPublished {
            display: none;
        }

        /* Add Animation */
        @-webkit-keyframes animatetop {
            from {
                top: -300px;
                opacity: 0;
            }

            to {
                top: 0;
                opacity: 1;
            }
        }

        @keyframes animatetop {
            from {
                top: -300px;
                opacity: 0;
            }

            to {
                top: 0;
                opacity: 1;
            }
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPost.aspx.cs" Inherits="VideoIntroduceFeatures.UI.EditPost" %>

<%@ Import Namespace="mojoPortal.Web" %>
<%@ Import Namespace="mojoPortal.Features" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper sharedfiles">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <%--<script src="/Data/FlowPlayer/nhungyoutube/jquery.video-extend.js"></script>--%>
                    <style>
                        .panel, .templatewrapper {
                            margin-bottom: 20px;
                            border-radius: 4px;
                            -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
                            box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
                            border: 1px solid #ddd;
                        }

                        .panel-heading, .templatetitle {
                            padding: 15px 10px;
                            border-bottom: 1px solid transparent;
                            border-top-right-radius: 3px;
                            border-top-left-radius: 3px;
                        }
                    </style>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Thông tin video
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 form-group">
                                <label class="col-sm-3">Tiêu đề (<span style="color: red">*</span>)</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtTieuDe" MaxLength="350" Width="600" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rflTieuDe" runat="server" ControlToValidate="txtTieuDe" ErrorMessage="Bạn chưa nhập tiêu đề" ValidationGroup="videoVilid"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%--<div class="col-sm-12 form-group">
                                <label class="col-sm-3">Video hoặc Youtube (<span style="color: red">*</span>)</label>

                                <div class="col-sm-9">
                                    <asp:DropDownList ID="drlTypePlayer" AutoPostBack="true" Height="32" runat="server"></asp:DropDownList>
                                </div>
                            </div>--%>
                            <%--   <asp:UpdatePanel ID="updateplTypePlayer" UpdateMode="Always" runat="server">--%>
                            <contenttemplate>
                                <%--  <asp:Panel ID="pnlVideoPlayer" runat="server" CssClass="col-sm-12 form-group" Visible="false">
                                        <div class="col-sm-3">
                                            <label>Video (<span style="color: red">*</span>)</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:FileUpload ID="fileUpload" runat="server" />
                                            <br />
                                            <asp:Label ID="lblRequiredVideo" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                                            <asp:Panel ID="pnlShowVideo" CssClass="showProductIMG" runat="server" Visible="false">
                                                <asp:HiddenField ID="hdfVideoValue" runat="server" />
                                                <asp:ImageButton ID="imgDeleteVideo" CssClass="imgbuttomdelte" runat="server" />
                                                <asp:Literal ID="literVideoPlayer" runat="server"></asp:Literal>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>--%>

                                <asp:Panel ID="pnlYoutubePlayer" runat="server" Visible="true" CssClass="col-sm-12 form-group">
                                    <div class="col-sm-3">
                                        <label>Video (<span style="color: red">*</span>)</label>
                                    </div>
                                    <asp:HiddenField ID="linkVideo" runat="server" />
                                    <div class="col-sm-9">
                                         <asp:Literal ID="literVideoPlayer" runat="server"></asp:Literal>
                                        <%--<video width="400" controls class="video-in-form" src="">
                                          Chọn video
                                        </video>--%>

                                        <span class="choose-video" title="Chọn video"><i class="fa fa-upload" aria-hidden="true"></i></span>

                                        <%--<asp:TextBox ID="txtYoutube" MaxLength="650" Width="600" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvYoutube" runat="server" ErrorMessage="Bạn chưa nhập đường dẫn youtube" ValidationGroup="videoVilid" ControlToValidate="txtYoutube"></asp:RequiredFieldValidator>
                                            <asp:Panel ID="pnlShowYoutube" CssClass="showProductIMG" runat="server" Visible="false">
                                                <asp:ImageButton ID="imgDeleteYoutube" CssClass="imgbuttomdelte" runat="server" />
                                                <asp:Literal ID="literYoutubePlayer" runat="server"></asp:Literal>
                                            </asp:Panel>--%>
                                    </div>
                                </asp:Panel>
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-3">Hình nền video</label>

                                    <div class="col-sm-9">
                                        <asp:FileUpload ID="floadBackground" runat="server" />
                                        <br />
                                        <asp:Panel ID="pnlShowIMG" runat="server" Visible="false">

                                            <asp:HiddenField ID="hdfImageValue" runat="server" />
                                            <div style="width: 100%; float: left;">
                                                <asp:ImageButton ID="imgDeleteIMG" CssClass="imgbuttomdelte" runat="server" />
                                            </div>
                                            <asp:Image ID="imgBackgroundImage" Width="315" Height="200" runat="server" />
                                        </asp:Panel>

                                    </div>
                                </div>
                            </contenttemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="drlTypePlayer" EventName="SelectedIndexChanged" />
                            </triggers>

                            <%--   </asp:UpdatePanel>--%>
                            <div class="col-sm-12 form-group">
                                <label class="col-sm-3">Cho phép hiển thị?</label>

                                <div class="col-sm-9">
                                    <asp:CheckBox ID="chkPublic" runat="server" CssClass="forminput" />
                                </div>
                            </div>
                            <div class="col-sm-12 form-group">
                                <label class="col-sm-3">Nổi bật</label>

                                <div class="col-sm-9">
                                    <asp:CheckBox ID="chkIsHot" runat="server" CssClass="forminput" />
                                </div>
                            </div>
                            <div class="col-sm-12 form-group" style="display: none">
                                <span>Nội dung</span>
                                <mpe:EditorControl ID="editContent" runat="server"></mpe:EditorControl>
                            </div>
                            <asp:Label ID="lblError" runat="server" />
                            <div class="settingrow col-sm-12" style="float: left; clear: both; margin-top: 15px;">
                                <asp:HiddenField ID="hdfVideo" ClientIDMode="Static" runat="server" />
                                 <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Lưu lại" OnClientClick="return SubmitAAA();" CausesValidation="false" />&nbsp;

<%--                                <portal:mojoButton ID="btnSubmit" ValidationGroup="videoVilid" OnClientClick="return SubmitAAA()" SkinID="ButtonSuccess" runat="server" />--%>
                                <portal:mojoButton ID="btnDel" SkinID="ButtonDanger" runat="server" />
                                <portal:mojoButton ID="btnCancel" SkinID="ButtonWarning" runat="server" />
                                <div>
                                    <asp:HyperLink ID="hplDetail" runat="server" Visible="false"></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>

                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
    <portal:SessionKeepAliveControl ID="ka1" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            var initVideoEdit = $('#linkVideo').value()

            $('.video-in-form').attr('src', initVideoEdit)

        })  
        $(document).on('click', '.choose-video', function () {
            var video = $(this)
            var finder = new CKFinder();
            finder.inPopup = true;
            finder.defaultLanguage = 'vi';
            finder.language = 'vi';
            finder.popupFeatures = "width=900,height=900,menubar=yes,toolbar=no,modal=yes";
            finder.selectMultiple = true;
            finder.startupPath = "Video:/";
            finder.BaseUrl = "/Video/";
            finder.resourceType = 'Video';
            finder.selectActionFunction = function (fileUrl, data, allFiles) {
                console.log(video.prev().parent().find('video'));
                video.prev().parent().find('video').attr("src", fileUrl);
            };
            finder.popup();
        })



        function GetVideo() {
            debugger
            var video = $('.video-in-form').attr('src');

            console.log('hung hung');
            if (video !== null && video !== '') {
                var objVideo = {
                    Description: 'Đây là video',
                    CreatedByUser: author,
                    FilePath: video,
                    AlbumOrder: 1
                }
                $('#hdfVideo').val(JSON.stringify(objVideo))
            }

            return false;

        }

        function SubmitAAA() {
            GetVideo()
            return true;
        }

    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />



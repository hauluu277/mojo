<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="CreateMultipleImage.aspx.cs" Inherits="MediaAlbumFeature.UI.CreateMultipleImage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:ModuleTitleControl runat="server" ID="TitleControl" />
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

                    <script src="/Data/plugins/DropzoneJs_scripts/dropzone.js"></script>
                    <link href="/Data/plugins/DropzoneJs_scripts/dropzone.css" rel="stylesheet" />
                    <style type="text/css">
                        .art-content-layout-row .cmszone:first-child {
                            padding-right: 0 !important;
                        }

                        fieldset {
                            min-height: 400px;
                            height: auto;
                        }

                        label {
                            font-size: 14px;
                            vertical-align: middle;
                        }

                        .mess-succ {
                            width: 60%;
                            margin-bottom: 20px;
                            height: 40px;
                            margin: 0 auto;
                            line-height: 40px;
                            background: #5cb85c;
                            color: white;
                            font-size: 19px;
                            text-align: center;
                        }

                        .show-succ {
                            display: none;
                        }
                    </style>
                    <div class="panel panel-default">
                        <div class="panel-heading">Thêm nhiều hình ảnh</div>
                        <div class="panel-body">
                            <table style="width: 100%">
            <%--                    <tr>
                                    <td style="width: 150px;">
                                        <label>Chọn thư viện ảnh: <span style="color: red">*</span></label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drlCategory" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>--%>
           <%--                     <tr style="height: 20px;">
                                    <td></td>
                                    <td>
                                        <span id="CategoryNull" style="color: red; font-size: 14px; display: none">Bạn chưa chọn thư viện ảnh</span>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="2">
                                        <div id="dZUpload" class="dropzone">
                                            <div class="dz-default dz-message">
                                                Drag and drop an image file here or click the button to select a file (.jpg, .jpeg, .png, .gif) 
       
                                            </div>
                                            <div id="msgBoard" style="display: none">Tải ảnh lên thành công !</div>

                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 20px"></tr>
                                <tr>
                                    <td colspan="2" class="show-succ">
                                        <div class="mess-succ">
                                            Tải ảnh lên thành công !
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:HiddenField ID="hdfModuleId" runat="server" />
                                        <asp:HiddenField ID="hdfSiteID" runat="server" />
                                        <asp:HiddenField ID="hdfCategoryID" runat="server" />
                                        <portal:mojoButton ID="btnUpload" ClientIDMode="Static" CssClass="btn btn-success" runat="server" Text="Tải lên" />
                                        <portal:mojoButton ID="btnCancel" ClientIDMode="Static" CssClass="btn btn-danger" runat="server" Text="Quay lại" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <script type="text/javascript">
                        Dropzone.autoDiscover = false;
                        $(document).ready(function () {
                            var myDropzone = new Dropzone("#dZUpload", {
                                autoProcessQueue: false,
                                url: "/Media/MutilpleUploadImage.ashx?categoryIMG=" + $("#<%=hdfCategoryID.ClientID%>").val() + "&mid=" + $("#<%=hdfModuleId.ClientID%>").val() + "&siteId=" + $("#<%=hdfSiteID.ClientID%>").val(),
                                maxFiles: 60,
                                uploadMultiple: true,
                                parallelUploads: 60,
                                //filesizeBase:1024,
                                maxFilesize: 200000,
                                renameFilename: function (file) {
                                    return file.renameFilename = "IMG." + file.split('.').pop();
                                },
                                autoProcessQueue: false,
                                dictDefaultMessage: "Drop files or click here to upload a new DICOM series ...",
                                init: function () {
                                    myDropzone = this;
                                    //Restore initial message when queue has been completed
                                    this.on("drop", function (event) {
                                        this.options.autoProcessQueue = false;
                                    });
                                    this.on("addedfile", function (file) {
                                        this.options.autoProcessQueue = false;
                                    }),
                                        this.on("queuecomplete", function () {
                                            this.options.autoProcessQueue = false;
                                        });

                                    this.on("processing", function () {
                                        this.options.autoProcessQueue = true;
                                    });

                                },
                                successmultiple: function (data, response) {
                                    $('#msgBoard').append(response.message).addClass("alert alert-success");
                                    $(".show-succ").delay(100).fadeIn();
                                    $(".show-succ").delay(2000).fadeOut();
                                    this.removeAllFiles();
                                    //$('#msgBoard').delay(10000).fadeOut();
                                    //$('#imgsubbutt').off('submit').submit();
                                },
                                //dataType: "json",
                                //data: { firstName: 'stack', lastName: 'overflow' },
                                acceptedFiles: 'image/*',
                                addRemoveLinks: true,
                                success: function (file, response) {
                                    var imgName = response;
                                    file.previewElement.classList.add("dz-success");
                                    console.log("Successfully uploaded :" + imgName);
                                },
                                error: function (file, response) {
                                    file.previewElement.classList.add("dz-error");
                                }
                            });
                            $("#btnUpload").on("click", function (e) {
                                // Make sure that the form isn't actually being sent.
               <%--                 var category = $("#<%=drlCategory.ClientID%>").val();
                                if (category == "" || category == null) {
                                    $("#CategoryNull").show();
                                    return false;
                                } else {--%>
                                e.preventDefault();
                                e.stopPropagation();

                                if (myDropzone.getQueuedFiles().length > 0) {
                                    myDropzone.options.url = "/Media/MutilpleUploadImage.ashx?categoryIMG=" + $("#<%=hdfCategoryID.ClientID%>").val() + "&mid=" + $("#<%=hdfModuleId.ClientID%>").val() + "&siteId=" + $("#<%=hdfSiteID.ClientID%>").val();
                                    myDropzone.options.autoProcessQueue = true;
                                    myDropzone.processQueue();
                                    ('#msgBoard').append(response.message).addClass("alert alert-success");
                                    $('#msgBoard').delay(2000).fadeOut();
                                    $('#btnUpload').off('btnUpload').submit();
                                } else {
                                    alert("Bạn chưa chọn ảnh");
                                }
                                //}
                            });

         <%--                   $("#<%=drlCategory.ClientID%>").change(function () {
                                var data = $(this).val();
                                if (data == "" || data == null) {
                                    $("#CategoryNull").show();
                                } else {
                                    $("#CategoryNull").hide();
                                }
                            })--%>
                        });
    </script>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

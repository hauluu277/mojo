<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPost.aspx.cs" Inherits="AudioFeature.UI.EditPost" %>



<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <script src="/Data/plugins/select2/select2.min.js"></script>
                    <link href="/Data/plugins/select2/select2.min.css" rel="stylesheet" />
                    <style>
                        .add-img img {
                            height: 72px;
                            object-fit: cover;
                            width: 75%;
                        }

                        .add-img {
                            width: 100%;
                            float: left;
                            margin-left: 0;
                            background-color: #f7f2f2;
                            border: 0 !important;
                            height: 72px;
                            padding-bottom: 0;
                            margin-bottom: 0 !important;
                        }



                            .add-img span {
                                width: 25%;
                            }

                        .btn-add-img {
                            height: 60px;
                            width: 55px;
                            margin-left: 10px;
                            margin-top: 0;
                            padding-top: 5%;
                        }

                        #table_bn tr td {
                            height: 100px;
                            border: 1px solid #ddd;
                            text-align: center;
                            padding: 0 10px;
                        }

                        #table_bn_ld tr td {
                            height: auto;
                            border: 1px solid #ddd;
                            text-align: center;
                            padding: 10px;
                        }

                        #btnaAdd {
                            width: 25px;
                            background-color: #4a77d8;
                        }

                        #btnaAdd_ld i {
                            color: white;
                        }

                        #btnaAdd_ld {
                            width: 25px;
                            background-color: #4a77d8;
                            border: 0;
                            height: 25px;
                        }

                        .add-img input {
                            height: 27px;
                            margin-top: 3px;
                            float: right;
                            margin-right: 5px;
                            width: 30px;
                            text-align: center;
                        }

                        .dang_ky_tuyen_sinh {
                            padding: 20px;
                        }

                        .haan_chucnang_nhiemvu {
                            width: 50% !important;
                            float: left;
                            padding: 10px;
                        }

                        #table_bn tr td input {
                            border: 1px solid #ddd;
                            width: 100%;
                            padding: 5px;
                        }

                        #table_bn_ld tr td input {
                            border: 1px solid #ddd;
                            width: 100%;
                            padding: 5px;
                        }

                        #table_bn tr th {
                            border: 1px solid #ddd;
                            text-align: center;
                            height: 43px;
                            padding: 5px;
                        }

                        #table_bn_ld tr th {
                            border: 1px solid #ddd;
                            text-align: center;
                            height: 43px;
                            padding: 5px;
                        }

                        .fstMultipleMode .fstControls {
                            width: 23em;
                        }

                        .tieude-dm h2 {
                            margin-top: 0;
                        }

                        .settingrow h3 {
                            float: left;
                            width: 100%;
                            margin-bottom: 5px;
                        }

                        .settingrow > div {
                            width: 100%;
                            float: left;
                        }

                        .btndelete button {
                            width: 25px;
                            background: #f5eded;
                            color: red;
                            font-size: 20px;
                            border: 0 !important;
                        }

                        .tb-han-padding > p {
                            margin-top: 0;
                            margin-bottom: 5px;
                            font-size: 15px;
                        }

                        #btnaAdd_mutiple {
                            width: 28px;
                            background-color: #ffc107;
                            border: 0;
                            height: 25px;
                            cursor: pointer;
                        }

                        #table_bn_ld {
                            width: 100%;
                        }

                    </style>
                    <div id="divLoaiButDanh" style="display: none">
                        <asp:DropDownList ID="ddlLoaiButDanh" ClientIDMode="Static" runat="server"></asp:DropDownList>
                    </div>
                    <div id="divButDanh" style="display: none">
                        <asp:DropDownList ClientIDMode="Static" ID="ddlButDanh" runat="server"></asp:DropDownList>
                    </div>
                    <div class="width100" id="frmGallery">
                        <fieldset class="fieldset">
                            <legend class="legend" id="legendGroupMedia" runat="server"></legend>
                            <div class="formlist">
                                <div class="settingrow">
                                    <label class="settinglabel">Tiêu đề<span class="red">*</span></label>
                                    <asp:TextBox ID="txtNameGroup" Width="50%" runat="server" SkinID="Required"></asp:TextBox>
                                </div>
                                <div class="settingrow">
                                    <label class="settinglabel">Chuyên mục<span class="red">*</span></label>
                                    <asp:DropDownList ID="ddlCategory" Width="50%" SkinID="Required" runat="server"></asp:DropDownList>
                                </div>
                                <div class="settingrow">
                                    <label class="settinglabel">Sapo</label>
                                    <asp:TextBox ID="txtSapo" runat="server" Width="50%" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                </div>
                                <div class="settingrow">
                                    <label class="settinglabel">Hiển thị trang chủ</label>
                                    <asp:CheckBox ID="IsHome" runat="server" TextMode="MultiLine"></asp:CheckBox>
                                </div>
                                <div class="settingrow">
                                    <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="txtOrder" CssClass="settinglabel label-fix" ConfigKey="OrderLabel" ResourceFile="MediaResources" />
                                    <asp:TextBox ID="txtOrder" Width="50%" runat="server" SkinID="RequireNumber"></asp:TextBox>
                                </div>
                                <div class="settingrow up_avatar">
                                    <label class="settinglabel">Ảnh đại diện<span class="red">*</span></label>
                                    <asp:Panel ID="pnUpflash" Width="50%" runat="server">
                                        <span style="float: left">
                                            <asp:FileUpload runat="server" ID="uploadFile" />
                                        </span>
                                        <asp:HiddenField ID="hfFilePath" runat="server" />
                                        <asp:RequiredFieldValidator ID="rvfFileUpload" runat="server" CssClass="rqDoc" ControlToValidate="uploadFile" ValidationGroup="mediaGroup" ErrorMessage="Bạn chưa chọn ảnh đại diện"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="uploadFile" ValidationGroup="mediaGroup"
                                            ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])$)"></asp:RegularExpressionValidator>
                                    </asp:Panel>
                                    <p style="width: 100%; float: left; margin-left: 200px;">
                                        <asp:Label ID="lblFileUrlError" Font-Bold="true" ForeColor="Red" runat="server" />
                                    </p>
                                </div>
                                <div class="settingrow">
                                    <asp:Panel ID="pnIMG" runat="server">
                                        <mp:SiteLabel ID="SiteLabel3" runat="server" ForControl="ImageID" CssClass="settinglabel label-fix" ConfigKey="ImageLabel" ResourceFile="MediaResources" />
                                        <img id="ImageID" runat="server" width="100" height="100" />
                                    </asp:Panel>
                                </div>
                                <h3 style="font-size: 20px; color: #333; text-transform: uppercase; text-align: center; margin-bottom: 0;">Danh sách Audio</h3>
                                <div class="settingrow">
                                    <%--Danh sách hình ảnh--%>
                                    <table id="table_bn_ld" style:"width: 100%;">
                                        <thead>
                                            <tr>
                                                <th style="width: 50px;">STT</th>
                                                <th style="width: 180px;">Audio</th>
                                                <th style="width: 220px">Tác giả</th>
                                                <th>Sapo Audio</th>
                                                <th style="width: 75px">
                                                    <button id="btnaAdd_ld" title="Thêm một audio" type="button" class="btn_giaoban"><i class="fa fa-plus"></i></button>
                                                    &nbsp;
                                                    <button id="btnaAdd_mutiple" onclick="CreateMutiple()" title="Thêm nhiều audio" type="button" class="btn_giaoban"><i class="fa fa-plus-circle" aria-hidden="true"></i></button>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Panel ID="pnlFirst" runat="server">
                                                <tr>
                                                    <td style="width: 50px;">1
                                                    </td>

                                                    <td style="width: 140px;">
                                                        <p class="add-img">
                                                                       <audio controls autoplay src="">
                                                                    Your browser does not support the audio element.
                                                                </audio>
                                                            <span class="text-primary pointer choose-img" title="Chọn audio"><i class="fa fa-upload" aria-hidden="true"></i></span>
                                                            <span class="text-danger pointer remove-img"><i class="fa fa-trash" aria-hidden="true"></i></span>
                                                        </p>
                                                    </td>
                                                    <td>
                                                        <input type="text" name="txtTacGia" placeholder='Tên tác giả' />
                                                    </td>
                                                    <td>
                                                        <input name="txtTitle" type="Text" placeholder='Sapo' />
                                                    </td>
                                                    <td style="width: 70px" class="btndelete">
                                                        <button onclick="deleteRow(this)" id="btnDelete2" type="button"><i class="fa fa-remove"></i></button>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Repeater ID="rptMediaImage" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 50px;">
                                                            <%# Container.ItemIndex + 2 %>
                                                        </td>
                                                        <td style="width: 140px;">
                                                            <p class="add-img">
                                                                <audio controls autoplay src="">
                                                                    Your browser does not support the audio element.
                                                                </audio>


                                                                <span class="text-primary pointer choose-img" title="Chọn ảnh"><i class="fa fa-upload" aria-hidden="true"></i></span>
                                                                <span class="text-danger pointer remove-img"><i class="fa fa-trash" aria-hidden="true"></i></span>
                                                            </p>
                                                        </td>
                                                        <td style="width: 50px;">
                                                            <asp:TextBox ID="txtTacGia" Text='<%#Eval("CreatedByUser") %>' ClientIDMode="Static" type="Text" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitle" Text='<%#Eval("Description") %>' ClientIDMode="Static" runat="server"></asp:TextBox>
                                                        </td>

                                                        <td class="btndelete" style="width: 70px">
                                                            <button onclick="deleteRow(this)" id="btnDelete2" type="button"><i class="fa fa-remove"></i></button>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <%--end Danh sách hình ảnh--%>
                                </div>

                                <div class="settingrow">
                                    <label class="settinglabel">Tác giả<span class="red">*</span></label>
                                    <asp:TextBox ID="txtCreatedByUser" Width="50%" runat="server" SkinID="Required"></asp:TextBox>
                                </div>
                                <div class="settingrow">
                                    <label class="settinglabel">Ngày đăng<span class="red">*</span></label>
                                    <mp:DatePickerControl ID="dpCreatedDate" Width="47%" CssClass="has-errored required" runat="server" />
                                </div>
                                <div class="settingrow">
                                    <label class="settinglabel">Xuất bản</label>
                                    <asp:CheckBox ID="ckbPublish" runat="server" />
                                </div>
                                <div class="settingrow">
                                    <asp:HiddenField ID="hdfHinhAnh" ClientIDMode="Static" runat="server" />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Lưu lại" OnClientClick="return checkSubmitGallery();" CausesValidation="false" />&nbsp;
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Xóa" Visible="false" />
                                    <asp:Button ID="btnCancel" CssClass="btn btn-info" runat="server" Text="Quay lại" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
    <style type="text/css">
        .date input {
            float: left;
            width: 200px !important;
        }

        #tblButDanh input[type=text], #tblButDanh input[type=number] {
            width: 100%;
        }

        .form-control {
            float: left;
        }
    </style>
    <script type="text/javascript" src="/Data/Script/jquery.tablednd.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            LoadDragTable();
        });
        function LoadDragTable() {
            $("#table_bn_ld").tableDnD({
                onDragClass: "myDragClass",
                onDrop: function (table, row) {
                    var rows = table.tBodies[0].rows;
                    ReloadTable();
                    //for (var i=0; i<rows.length; i++) {
                    //    $(rows[i]).find(".orderItem").val(i+1);
                    //}
                }
            });
        }


        function ReloadTable() {
            var count = 0;
            $("#table_bn_ld tbody tr").each(function () {
                count++;
                $(this).find("th:nth-child(1)").html(count);
            })
        };

    </script>
    <script type="text/jscript">
        function CreateMutiple() {
            var finder = new CKFinder();
            finder.inpopup = true;
            finder.defaultlanguage = 'vi';
            finder.language = 'vi';
            finder.popupfeatures = "width=900,height=900,menubar=yes,toolbar=no,modal=yes";
            finder.selectmultiple = true;
            finder.startupPath = "Audio:/";
            finder.BaseUrl = "/Audio/";
            finder.resourceType = 'Audio';
            finder.selectActionFunction = function (fileurl, data, allfiles) {
                $(allfiles).each(function (index, element) {
                    console.log(this);
                    var dem = $('#table_bn_ld tbody tr').length;
                    var table = $("#table_bn_ld");
                    var append = "<tr>";
                    append += `<td style="width:50px;">` + (dem + 1) + `</td>`;
                    append += `<td style="width: 140px;">`;
                    append += `<p class="add-img" >
                       <input type="hidden" />
                        <audio controls src="`+ this.data.fileUrl + `">
                        </audio>
                       <span class="text-primary pointer choose-img" title="Audio"><i class="fa fa-upload" aria-hidden="true"></i></span>
                       <span class="text-danger pointer remove-img"><i class="fa fa-trash" aria-hidden="true"></i></span>
                       </p>`;
                    append += `</td>`;
                    append += `<td><input name="txtTacGia" type="text" placeholder='Tên tác giả'/></td>`;
                    append += `<td><input name="txtTitle" type="text" placeholder='Sapo'/></td>`;
                    append += ` <td style="width:70px" class="btndelete">
                         <button onclick="deleteRow(this)" id="btnDelete2_ld" type="button"><i class="fa fa-remove"></i></button>
                         </td>`;

                    append += `</tr>`;
                    $("tbody", table).prepend(append)

                });
                LoadDragTable();
            };
            finder.popup();
        }
        function LoadSelect2() {
            $("#tblButDanh tbody select").select2();
        }
        $(document).ready(function () {
            SetupFormError("frmGallery");
            LoadSelect2();
        });
        function checkSubmitGallery() {
            console.log('123');
            if (FormInvalid("frmGallery")) {
                GetHinhAnh();
                //alert($("#hdfHinhAnh").val());
                return true;
            }
            NotifyError("Vui lòng hoàn thiện form theo hướng dẫn !");
            return false;
        }



    </script>

    <script type="text/javascript">
        function LoadError() {
            NotifyError("Vui lòng hoàn thiện form theo hướng dẫn !");
        }
        function addNewRow(ID) {
            var dem = $('#table_bn_ld tbody tr').length;
            var table = $("#" + ID);
            var append = "<tr>";
            append += `<td style="width:50px;">` + (dem + 1) + `</td>`;
            append += `<td style="width: 140px;">`;
            append += `<p class="add-img" >
                       <input type="hidden" />
                              <audio controls src="">
                        </audio>
                       <span class="text-primary pointer choose-img" title="Chọn audio"><i class="fa fa-upload" aria-hidden="true"></i></span>
                       <span class="text-danger pointer remove-img"><i class="fa fa-trash" aria-hidden="true"></i></span>
                       </p>`;
            append += `</td>`;
            append += `<td><input name="txtTacGia" type="text" placeholder='Tên tác giả'/></td>`;
            append += `<td><input name="txtTitle" type="text" placeholder='Sapo'/></td>`;
            append += ` <td style="width:70px" class="btndelete">
                         <button onclick="deleteRow(this)" id="btnDelete2_ld" type="button"><i class="fa fa-remove"></i></button>
                         </td>`;

            append += `</tr>`;
            $("tbody", table).prepend(append);
            LoadDragTable();
        }

        $('#btnaAdd_ld').click(function () {
            addNewRow("table_bn_ld")
        });

        function deleteRow(r) {
            var i = r.parentNode.parentNode.rowIndex;
            document.getElementById("table_bn_ld").deleteRow(i);
        }
        $(document).on("click", ".remove-img", function () {
            var p_Parent = $(this).parent();
            var img = p_Parent.find("img").attr("src");
            if (img == "/Data/Images/imgbvna/imageno.jpg") {
                p_Parent.remove();
                return;
            }
            if (confirm("Bạn chắc chắn muốn xóa ảnh này?")) {
                p_Parent.remove();
            }
        });
        $(document).on("click", ".choose-img", function () {
            var img = $(this);
            var finder = new CKFinder();
            finder.inPopup = true;
            finder.defaultLanguage = 'vi';
            finder.language = 'vi';
            finder.popupFeatures = "width=900,height=900,menubar=yes,toolbar=no,modal=yes";
            finder.selectMultiple = true;
            finder.startupPath = "Audio:/";
            finder.BaseUrl = "/Audio/";
            finder.resourceType = 'Audio';
            finder.selectActionFunction = function (fileUrl, data, allFiles) {
                console.log(img);
                console.log(img.prev());
                img.prev().attr("src", fileUrl);
            };
            finder.popup();
        });


        function GetHinhAnh() {
            var allCanBold = [];
            var count = 0;
            $("#table_bn_ld tbody tr").each(function (index, element) {
                count++;
                var title = toTrim($(this).find("td:nth-child(4) input[type=text]").val());
                var images = $(this).find("td:nth-child(2) audio");
                var author = $(this).find("td:nth-child(3) input[type=text]").val();
                var listImg;
                listImg = images.map((index, img, images) => {
                    var src = $(img).attr("src");
                    if (src != "/Data/Images/imgbvna/imageno.jpg") {
                        return src;
                    }
                }).get().join(';');
                if (listImg != null && listImg != "") {
                    var benhNhan_ld = {
                        Description: title,
                        CreatedByUser: author,
                        FilePath: listImg,
                        AlbumOrder: count

                    };
                    allCanBold.push(benhNhan_ld);
                }
            });

            console.log(allCanBold);


            $("#hdfHinhAnh").val(JSON.stringify(allCanBold));
            //alert(allCanBold);



            return false;
        }

    </script>
    <%--end Dánh ách lãnh đạo--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

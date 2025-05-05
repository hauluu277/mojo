<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="PostUnit.aspx.cs" Inherits="UnitFeatures.UI.PostUnit" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <asp:Panel ID="pnlEvent" runat="server" DefaultButton="btnUpdate" CssClass="panelwrapper admin editpage blogedit">
        <portal:ModuleTitleControl ID="moduleTitle" runat="server" RenderArtisteer="true"
            UseLowerCaseArtisteerClasses="true" Visible="false" />
        <portal:BreadCrumbControl ID="BreadCrumbControl" runat="server" />
        <portal:HeadingControl ID="heading" runat="server" />
        <link href="/Data/plugins/select2/select2.min.css" rel="stylesheet" />
        <style>
            .add-img img {
                height: 60px;
                object-fit: contain;
                width: 100%;
            }

            .add-img {
                width: 78%;
                float: left;
                margin-left: 10px;
                background-color: #f7f2f2;
                border: 1px solid !ddd;
                border-radius: 5px;
                height: 60px;
                margin-bottom: 10px;
                padding-bottom: 0;
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

            .tbl_phongban tr td {
                height: 100px;
                border: 1px solid #ddd;
                text-align: center;
                padding: 0 10px;
            }

            #btnaAdd {
                width: 25px;
                background-color: #4a77d8;
            }

            #btnaAdd_ld {
                width: 25px;
                background-color: #4a77d8;
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

            .tbl_phongban tr td input {
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

            .tbl_phongban tr th {
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
            }

            .tb-han-padding > p {
                margin-top: 0;
                margin-bottom: 5px;
                font-size: 15px;
            }

            #ctl00_mainContent_txtMaPhong {
                width: 100% !important;
            }
        </style>

        <div class="dang_ky_tuyen_sinh ui-widget ui-widget-content">
            <div class="tieude-dm">
                <h2>
                    <asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
            </div>

            <div class="settingrow">
                <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="txtTitle" CssClass="settinglabel"
                    ConfigKey="ArticleEditTitleLabel" ResourceFile="TrainingResources"></mp:SiteLabel>
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="255" Width="100%" CssClass="forminput verywidetextbox">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ValidationGroup="save_functionalunit" />
            </div>
            <div class="settingrow">
                <asp:TextBox ID="txtItemUrl" runat="server" Width="100%" MaxLength="255" CssClass="forminput verywidetextbox"></asp:TextBox>
                <span id="spnUrlWarning" runat="server" style="font-weight: normal;" class="txterror"></span>
                <asp:HiddenField ID="hdnTitle" runat="server" />
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="txtItemUrl"
                    CssClass="txterror"
                    ID="reqTitle"
                    ErrorMessage="Đường dẫn UrlItem không được để trống"
                    ValidationGroup="save_functionalunit"></asp:RequiredFieldValidator>
                <br />
                <asp:RegularExpressionValidator runat="server"
                    ControlToValidate="txtItemUrl"
                    ID="regexUrl"
                    ErrorMessage="Đường dẫn Url phải bắt đầu bằng ~/ và không được chứa khoảng trắng."
                    ValidationExpression="((~/){1}\S+)"
                    ValidationGroup="save_functionalunit" />

            </div>
            <div class="settingrow">
                <h3>
                    <label class="settinglabel">I. Thông tin chung</label>
                </h3>
                <mpe:EditorControl ID="edGeneral" runat="server"></mpe:EditorControl>
            </div>

            <div class="settingrow haan_thongtin_chung">
                <h3>
                    <label class="settinglabel">II. Chức năng - Nhiệm vụ</label></h3>
                <div class="haan_chucnang_nhiemvu cnnv1">
                    <p>1. Chức năng</p>
                    <mpe:EditorControl ID="edFunctionP" runat="server"></mpe:EditorControl>
                </div>
                <div class="haan_chucnang_nhiemvu cnnv2">
                    <p>2. Nhiệm vụ</p>
                    <mpe:EditorControl ID="edMission" runat="server"></mpe:EditorControl>
                </div>
            </div>

            <div class="settingrow">
                <h3>
                    <label class="settinglabel">III. Đội ngũ cán bộ</label></h3>
                <div class="tb-han-padding">
                    <div class="settingrow">
                        <label>Mã khoa/phòng (Mã khoa/phòng trên phần mềm quản lý nhân sự để sử dụng API lấy dữ liệu các cán bộ thuộc khoa/phòng)</label>
                        <asp:TextBox ID="txtMaPhong" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <%--BẢNG DANH SÁCH CÁN BỘ--%>



            <div class="settingrow">
                <h3>
                    <label class="settinglabel">IV. Thành tích nổi bật</label></h3>
                <mpe:EditorControl ID="edAchievement" runat="server"></mpe:EditorControl>
            </div>

            <div class="settingrow">
                <h3>
                    <label class="settinglabel">V. Lịch công tác</label></h3>
                <mpe:EditorControl ID="edLichCongTac" runat="server"></mpe:EditorControl>
            </div>
            <div class="settingrow">
                <h3>
                    <label class="settinglabel">VI. Thủ tục hành chính</label></h3>
                <mpe:EditorControl ID="edProcedureP" runat="server"></mpe:EditorControl>
            </div>

            <div class="settingrow">
                <h3>
                    <label class="settinglabel">VII. Liên hệ</label></h3>
                <mpe:EditorControl ID="edContact" runat="server"></mpe:EditorControl>
            </div>

            <div class="settingrow">
                <label class="settinglabel">Người đăng</label>
                <asp:TextBox ID="txtCreator" runat="server"></asp:TextBox>
            </div>

            <div class="settingrow">
                <label class="settinglabel">Ngày đăng</label>
                <mp:DatePickerControl ID="dpCreateDate" runat="server" />
            </div>
            <div class="settingrow">
                <label class="settinglabel">Thứ tự hiển thị?</label>
                <asp:TextBox ID="txtOrderBy" runat="server" TextMode="Number"></asp:TextBox>

            </div>
            <asp:Panel ID="pnlAllowUserModify" runat="server" CssClass="settingrow">
                <label class="settinglabel">Người được chỉnh sửa</label>
                <asp:DropDownList ID="ddlUser" runat="server" ClientIDMode="Static"></asp:DropDownList>
            </asp:Panel>
            <%--Xuất bản--%>
            <div class="settingrow">
                <label class="settinglabel">Hiển thị: </label>
                <asp:CheckBox ID="chkIsPubshed" runat="server" />
            </div>
            <%--Cho phép người dùng đăng câu hỏi--%>
            <div class="settingrow">
                <label class="settinglabel">Cho phép gửi câu hỏi?</label>
                <asp:CheckBox ID="chkIsShowQuestion" runat="server" />
            </div>
            <div class="settingrow">
                <mp:SiteLabel ID="SiteLabel35" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                <div class="forminput">
                    <asp:HiddenField ID="hdfCanBo" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hdfCanBold" ClientIDMode="Static" runat="server" />
                    <NeatUpload:ProgressBar ID="progressBar" runat="server">
                    </NeatUpload:ProgressBar>
                    <portal:mojoButton ID="btnUpdate" runat="server" Text="Lưu" />
                    <portal:mojoButton ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Xóa đối tượng này?" CausesValidation="false" />
                    <asp:HyperLink ID="lnkCancel" runat="server" CssClass="cancellink" />&nbsp;
                </div>
                <br />
                <portal:mojoLabel ID="lblError" runat="server" CssClass="txterror" />
            </div>
            <div class="settingrow">
                <portal:mojoLabel ID="lblErrorMessage" runat="server" CssClass="txterror" />
                &nbsp;
            </div>
        </div>
        <asp:HiddenField ID="hdnReturnUrl" runat="server" />
    </asp:Panel>
    <script src="/Data/plugins/select2/select2.min.js"></script>

    <%--Dánh sách lãnh đạo--%>
    <script type="text/javascript">
        $("#ddlUser").select2();

        function addNewRow(ID) {
            var table = $("#" + ID);
            var append = "<tr>";
            append += `<td><input type="text"/></td>`;
            append += `<td><input type="text"/></td>`;
            append += `<td><input type="text"/></td>`;
            append += `<td><input type="text"/></td>`;
            append += `<td><textarea name="name" rows="3"></textarea>`;
            append += `<td><input type="number" value="1" /></td>`;
            append += `<td>`;
            append += `<p class="add-img" >
                       <input type="hidden" />
                       <img src="/Data/Images/no-image.png" width="50">
                       <span class="text-primary pointer choose-img" title="Chọn ảnh"><i class="fa fa-upload" aria-hidden="true"></i></span>
                       <span class="text-danger pointer remove-img"><i class="fa fa-trash" aria-hidden="true"></i></span>
                       </p>`;
            append += `</td>`;

            append += ` <td class="btndelete">
                         <button onclick="deleteRowLanhDao(this)" type="button"><i class="fa fa-remove"></i></button>
                         </td>`;

            append += `</tr>`;
            $("tbody", table).prepend(append)
        }

        $('#btnaAdd_ld').click(function () {
            addNewRow("tbl_LanhDao")
        });

        function deleteRowLanhDao(row) {
            var tr = row.parentNode.parentNode.rowIndex;
            document.getElementById("tbl_LanhDao").deleteRow(tr);
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
            finder.startupPath = "Images:/";
            finder.BaseUrl = "/Images/";
            finder.resourceType = 'Images';
            finder.selectActionFunction = function (fileUrl, data, allFiles) {
                img.prev().attr("src", fileUrl);
            };
            finder.popup();
        });

    </script>
    <%--end Dánh ách lãnh đạo--%>




    <%--Dánh sách cán bộ--%>
    <script type="text/javascript">
        function deleteRowCanBo(row) {
            var tr = row.parentNode.parentNode.rowIndex;
            document.getElementById("tbl_CanBo").deleteRow(tr);
        }
        function deleteRowLanhDao(row) {
            var tr = row.parentNode.parentNode.rowIndex;
            document.getElementById("tbl_LanhDao").deleteRow(tr);
        }

        function addNewRowCanBo(ID) {
            var table = $("#" + ID);
            var append = "<tr>";
            append += `<td><input type="text"/></td>`;
            append += `<td><input type="text"/></td>`;
            append += `<td><input type="text"/></td>`;
            append += `<td><input type="text"/></td>`;
            append += `<td><textarea name="name" rows="3"></textarea>`;
            append += `<td><input type="number" value="1" /></td>`;
            append += `<td>`;
            append += `<p class="add-img" >
                       <input type="hidden" />
                       <img src="/Data/Images/no-image.png" width="50">
                       <span class="text-primary pointer choose-img" title="Chọn ảnh"><i class="fa fa-upload" aria-hidden="true"></i></span>
                       <span class="text-danger pointer remove-img"><i class="fa fa-trash" aria-hidden="true"></i></span>
                       </p>`;
            append += `</td>`;

            append += ` <td class="btndelete">
                         <button onclick="deleteRowCanBo(this)" type="button"><i class="fa fa-remove"></i></button>
                         </td>`;

            append += `</tr>`;
            $("tbody", table).prepend(append)
        }

        $('#btnaAdd').click(function () {
            addNewRowCanBo("tbl_CanBo")
        });


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
            finder.startupPath = "Images:/";
            finder.BaseUrl = "/Images/";
            finder.resourceType = 'Images';
            finder.selectActionFunction = function (fileUrl, data, allFiles) {
                img.prev().attr("src", fileUrl);
            };
            finder.popup();
        });
        function GetDanhsachCanbo() {
            var allCanBo = [];
            $("#tbl_CanBo tbody tr").each(function (index, element) {
                var name = toTrim($(this).find("td:nth-child(1) input[type=text]").val());
                var position = toTrim($(this).find("td:nth-child(2) input[type=text]").val());
                var email = toTrim($(this).find("td:nth-child(3) input[type=text]").val());
                var phone = toTrim($(this).find("td:nth-child(4) input[type=text]").val());
                var missionOfficer = toTrim($(this).find("td:nth-child(5) textarea").val());
                var images = $(this).find("td:nth-child(7) img");
                var orderBy = toTrim($(this).find("td:nth-child(6) input[type=number]").val());

                var listImg;
                listImg = images.map((index, img, images) => {
                    var src = $(img).attr("src");
                    if (src != "/Data/Images/imgbvna/imageno.jpg") {
                        return src;
                    }
                }).get().join(';');
                if (name != null && name != "") {
                    var benhNhan = {
                        Name: name,
                        Position: position,
                        Email: email,
                        Phone: phone,
                        MissionOfficer: missionOfficer,
                        UrlImage: listImg,
                        OrderByOfficer: orderBy
                    };
                    allCanBo.push(benhNhan);
                }
            });
            $("#hdfCanBo").val(JSON.stringify(allCanBo));


            var allCanBold = [];
            $("#tbl_LanhDao tbody tr").each(function (index, element) {
                var name = toTrim($(this).find("td:nth-child(1) input[type=text]").val());
                var position = toTrim($(this).find("td:nth-child(2) input[type=text]").val());
                var email = toTrim($(this).find("td:nth-child(3) input[type=text]").val());
                var phone = toTrim($(this).find("td:nth-child(4) input[type=text]").val());
                var missionOfficer = toTrim($(this).find("td:nth-child(5) textarea").val());
                var images = $(this).find("td:nth-child(7) img");
                var orderBy = toTrim($(this).find("td:nth-child(6) input[type=number]").val());

                var listImg;
                listImg = images.map((index, img, images) => {
                    var src = $(img).attr("src");
                    if (src != "/Data/Images/imgbvna/imageno.jpg") {
                        return src;
                    }
                }).get().join(';');
                if (name != null && name != "") {
                    var benhNhan_ld = {
                        Name: name,
                        Position: position,
                        Email: email,
                        Phone: phone,
                        MissionOfficer: missionOfficer,
                        UrlImage: listImg,
                        OrderByOfficer: orderBy
                    };
                    allCanBold.push(benhNhan_ld);
                }
            });
            $("#hdfCanBold").val(JSON.stringify(allCanBold));
        }
    </script>
    <%--end Dánh ách cán bộ--%>
    <script type="text/javascript" src="/ClientScript/fastselect/fastselect.standalone.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />


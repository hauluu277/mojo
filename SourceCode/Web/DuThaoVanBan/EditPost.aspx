<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPost.aspx.cs" Inherits="DuThaoVanBanFeature.UI.EditPostPage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <fieldset>
                        <legend class="legend-title">Tạo dự thảo</legend>
                        <ol class="formlist">
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblLinhVucID" runat="server" ForControl="ddlLinhVuc" CssClass="settinglabel" ConfigKey="LinhVucIDLabel" ResourceFile="DuThaoVanBanResources" />
                                <asp:DropDownList ID="ddlLinhVuc" runat="server" CssClass="edit-drl-fix ddlSettingLabel1">
                                    <asp:ListItem Value="0" Text="Chọn lĩnh vực" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLinhVuc" runat="server" CssClass="rqDoc" ControlToValidate="ddlLinhVuc"
                                    ValidationGroup="DuThaoVanBan" ErrorMessage="Bạn chưa chọn lĩnh vực" InitialValue="0" />
                            </li>
                            <li class="settingrow">
                                <label class="settinglabel">Cơ quan ban hành</label>
                                <asp:DropDownList ID="ddlCoQuanBanHanh" runat="server" CssClass="edit-drl-fix ddlSettingLabel1">
                                    <asp:ListItem Value="0" Text="Chọn cơ quan ban hành" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rqDoc" ControlToValidate="ddlCoQuanBanHanh"
                                    ValidationGroup="DuThaoVanBan" ErrorMessage="Bạn chưa chọn cơ quan ban hành" InitialValue="0" />
                            </li>
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblLoaiVanBanID" runat="server" ForControl="ddlLoaiVB" CssClass="settinglabel" ConfigKey="LoaiVanBanIDLabel" ResourceFile="DuThaoVanBanResources" />
                                <asp:DropDownList ID="ddlLoaiVB" runat="server" CssClass="edit-drl-fix ddlSettingLabel1">
                                    <asp:ListItem Value="0" Text="Chọn loại văn bản" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLoaiVanBan" runat="server" CssClass="rqDoc" ControlToValidate="ddlLoaiVB"
                                    ValidationGroup="DuThaoVanBan" ErrorMessage="Bạn chưa chọn loại văn bản" InitialValue="0" />
                            </li>
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblTitle" runat="server" ForControl="txtTitle" CssClass="settinglabel" ConfigKey="TitleLabel" ResourceFile="DuThaoVanBanResources" />
                                <asp:TextBox ID="txtTitle" CssClass="verywidetextbox forminput" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rqDoc" ControlToValidate="txtTitle"
                                    ValidationGroup="DuThaoVanBan" ErrorMessage="Bạn chưa nhập tiêu đề văn bản" />
                            </li>
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblSummary" runat="server" ForControl="edSummary" CssClass="settinglabel" ConfigKey="SummaryLabel" ResourceFile="DuThaoVanBanResources" />
                                <div class="settingrow">
                                    <mpe:EditorControl ID="edSummary" runat="server"></mpe:EditorControl>
                                </div>
                            </li>
                            <li class="settingrow">
                                <table id="frmFile" width="100%">
                                    <tr>
                                        <th>Tên tài liệu
                                        </th>
                                        <th></th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="fileName1" class="form-control" name="fileName1" type="text" />
                                        </td>
                                        <td>
                                            <input id="fileInput1" size="25" runat="server" name="fileInput1" type="file" onchange="check_extension(this.value,'btnSave');" />
                                            &nbsp;<input id="btnAddFile" type="button" value="Thêm" onclick="AddFile();" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnFile" runat="server">
                                    <asp:Repeater ID="rptFile" runat="server" OnItemCommand="rptFile_Command">
                                        <ItemTemplate>
                                            <div style="padding: 5px 0px;">
                                                <asp:HyperLink ID="lnkAttach" runat="server" Text='<%#Eval("Name") %>' NavigateUrl='<%# "/"+ConfigurationManager.AppSettings["DraftDocumentFileFolder"]+Eval("FilePath") %>' Target="_blank"></asp:HyperLink></asp:HyperLink>&nbsp; &nbsp;<asp:ImageButton ID="btnDeleteImg" ImageUrl='<%#deleteImg %>' ToolTip='<%#deleteTooltip %>' runat="server" CommandName="Delete" CommandArgument='<%#Eval("ItemID") %>' runat="server" CausesValidation="false" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>

                                <%--<mp:SiteLabel id="lblFile" runat="server"  CssClass="settinglabel" ConfigKey="FileLabel" ResourceFile="DuThaoVanBanResources" />
                                <table id="frmFile" width="100%" >
                                <tr>
                                    <th>
                                        Tên tài liệu
                                    </th>
                                    <th>                                      
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <input id="fileName1" name="fileName1" type="text" />
                                    </td>
                                    <td>
                                        <input id="fileInput1" size="25" runat="server" name="fileInput1" type="file" onchange="check_extension(this.value,'btnSave');" />
                                        &nbsp;<input id="btnAddFile" type="button" value="Thêm" onclick="AddFile();" />
                                    </td>
                                </tr>
                            </table>--%>
                            </li>
                            <li class="settingrow">
                                <asp:Label ID="lblError" Text="Bạn chưa chọn file" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                            </li>
                            <li class="settingrow date">
                                <mp:SiteLabel ID="lblDateNotice" runat="server" ForControl="dpDateStart" CssClass="settinglabel" ConfigKey="DateStartLabel2" ResourceFile="DuThaoVanBanResources" />
                                <mp:jsCalendarDatePicker ID="dpDateStart" runat="server" ShowTime="false" CssClass="datetime-input" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rqDoc" ControlToValidate="dpDateStart"
                                    ValidationGroup="DuThaoVanBan" ErrorMessage="(*)" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="dpDateStart" ValidationGroup="DuThaoVanBan" ErrorMessage="Ngày bắt đầu sai định dạng"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                            </li>
                            <li class="settingrow date">
                                <mp:SiteLabel ID="lblDateExpires" runat="server" ForControl="dpDateExpires" CssClass="settinglabel" ConfigKey="DateExpiresLabel" ResourceFile="DuThaoVanBanResources" />
                                <mp:jsCalendarDatePicker ID="dpDateExpires" runat="server" ShowTime="false" CssClass="datetime-input" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="dpDateExpires" ValidationGroup="DuThaoVanBan" ErrorMessage="Ngày kết thúc sai định dạng"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rqDoc" ControlToValidate="dpDateExpires"
                                    ValidationGroup="DuThaoVanBan" ErrorMessage="Bạn chưa chọn ngày kết thúc" />
                            </li>

                            <li class="settingrow">
                                <mp:SiteLabel ID="lblCreatedByUser" runat="server" ForControl="txtCreatedByUser" CssClass="settinglabel" ConfigKey="CreatedByUserLabel" ResourceFile="DuThaoVanBanResources" />
                                <asp:TextBox ID="txtCreatedByUser" CssClass="verywidetextbox forminput" runat="server" />
                            </li>
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblItemUrl" runat="server" ForControl="txtItemUrl" CssClass="settinglabel" ConfigKey="ItemUrlLabel" ResourceFile="DuThaoVanBanResources" />
                                <asp:TextBox ID="txtItemUrl" CssClass="verywidetextbox forminput" runat="server" />
                                <span id="spnUrlWarning" runat="server" style="font-weight: normal;" class="txterror"></span>
                                <asp:HiddenField ID="hdnTitle" runat="server" />
                                <asp:RegularExpressionValidator ID="regexUrl" runat="server" ControlToValidate="txtItemUrl"
                                    ValidationExpression="((~/){1}\S+)" Display="None" ValidationGroup="DuThaoVanBan" />
                            </li>
                            <li class="settingrow ishot">
                                <mp:SiteLabel ID="lblIsPublic" runat="server" ForControl="chkIsPublic" ConfigKey="IsPublicLabel"
                                    ResourceFile="DuThaoVanBanResources" CssClass="settinglabel"></mp:SiteLabel>
                                <asp:CheckBox ID="chkIsPublic" runat="server" CssClass="forminput" Checked="false"></asp:CheckBox>
                            </li>
                            <li class="settingrow">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="DuThaoVanBan" />
                                <asp:Button ID="btnDel" runat="server" OnClientClick="if(!ConfirmDelete()){ return false;}" />
                                <asp:Button ID="btnCancel" runat="server" />
                            </li>
                        </ol>
                    </fieldset>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
    <script type="text/javascript">
        var fileIndex = 1;
        function AddFile() {
            fileIndex++;
            var tag = "<td><input class='form-control' id='fileName" + fileIndex.toString() + "' name='fileName" + fileIndex.toString() + "' type=\"Text\" /></td>" +
                "<td><input id='fileInput" + fileIndex.toString() + "' name='fileInput" + fileIndex.toString() + "' type=\"file\" size='25' /></td>";
            var row = "<tr>" + tag + "</tr>";
            $("#frmFile").append(row);
        };
        function ConfirmDelete() {
            var check = confirm('Bạn có chắc chắn muốn xóa dự thảo này?')
            if (check == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style>
        .date input {
            float: left;
            width: 200px !important;
        }

        .form-control, input[type=text].forminput, select.forminput {
            display: inherit;
        }

        #btnAddFile {
            width: 10%;
            height: 44px;
            padding: 10px;
        }

        .datepickerbutton {
            width: 5%;
            height: 35px;
            float: left
        }

        #ctl00_mainContent_pnFile a {
            background: #00ad07;
            padding: 7px;
            color: white;
            border-radius: 5px;
        }

        #ctl00_mainContent_pnFile div {
            margin-top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server">
</asp:Content>


<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPost.aspx.cs" Inherits="DocumentFeature.UI.EditPostPage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <portal:HeadingControl ID="heading" runat="server" />
                    <style type="text/css">
                        .formlist li {
                            width: 100%;
                            float: left;
                        }

                        .box-inline li {
                            width: auto;
                        }

                        .box-inline label {
                            font-size: 14px;
                        }
                    </style>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Thông tin văn bản pháp quy
                        </div>
                        <div class="panel-body">
                            <ol class="formlist">
                                <li class="settingrow hide">
                                    <label class="settinglabel">Nhóm văn bản (*)</label>
                                    <asp:DropDownList ID="ddlNhomVanBan" runat="server" CssClass="ddlSettingLabel1"></asp:DropDownList>
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblLinhVuc" runat="server" ForControl="ddlLinhVuc" CssClass="settinglabel" ConfigKey="LinhVucLabel" ResourceFile="DocumentResources" />
                                    <asp:DropDownList ID="ddlLinhVuc" runat="server" CssClass="ddlSettingLabel1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvLinhVuc" runat="server" CssClass="rqDoc" ControlToValidate="ddlLinhVuc"
                                        ValidationGroup="document" InitialValue="0" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblLoaiVB" runat="server" ForControl="txtLoaiVB" CssClass="settinglabel" ConfigKey="LoaiVBLabel" ResourceFile="DocumentResources" />
                                    <asp:DropDownList ID="ddlLoaiVB" runat="server" CssClass="ddlSettingLabel1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvLoaiVanBan" runat="server" CssClass="rqDoc" ControlToValidate="ddlLoaiVB"
                                        ValidationGroup="document" InitialValue="0" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblCoQuanID" runat="server" ForControl="ddlCoQuanID" CssClass="settinglabel" ConfigKey="CoQuanIDLabel" ResourceFile="DocumentResources" />
                                    <asp:DropDownList ID="ddlCoQuanID" runat="server" CssClass="ddlSettingLabel1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCoQuan" runat="server" CssClass="rqDoc" ControlToValidate="ddlCoQuanID"
                                        ValidationGroup="document" InitialValue="0" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblSign" runat="server" ForControl="txtSign" CssClass="settinglabel" ConfigKey="SignLabel" ResourceFile="DocumentResources" />
                                    <asp:TextBox ID="txtSign" CssClass="verywidetextbox forminput" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvSign" runat="server" ControlToValidate="txtSign"
                                        ErrorMessage="(*)" ValidationGroup="document" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblSummary" runat="server" ForControl="txtSummary" CssClass="settinglabel" ConfigKey="SummaryLabel" ResourceFile="DocumentResources" />
                                    <div style="width: 100%; float: left">
                                        <asp:TextBox TextMode="MultiLine" runat="server" ID="txtSummary"></asp:TextBox>
                                    </div>
                                    <portal:mojoLabel ID="lblSummaryErrorMessage" runat="server" CssClass="txterror" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblContent" runat="server" ForControl="edContent" CssClass="settinglabel" ConfigKey="ContentLabel" ResourceFile="DocumentResources" />
                                    <div style="width: 100%; float: left">
                                        <mpe:EditorControl ID="edContent" runat="server">
                                        </mpe:EditorControl>
                                    </div>
                                </li>
                                <li class="settingrow date">
                                    <mp:SiteLabel ID="lblDatePromulgate" runat="server" ForControl="dpDatePromulgate" CssClass="settinglabel" ConfigKey="DatePromulgateLabel" ResourceFile="DocumentResources" />
                                    <portal:jDatePicker ID="dpDatePromulgate2" runat="server"></portal:jDatePicker>
                                    <asp:RequiredFieldValidator ID="rfvDatePromulgate" runat="server" ControlToValidate="dpDatePromulgate2"
                                        ErrorMessage="(*)" ValidationGroup="document" />
                                </li>
                                <li class="settingrow date">
                                    <mp:SiteLabel ID="lblDateEffect" runat="server" ForControl="dpDateEffect" CssClass="settinglabel" ConfigKey="DateEffectLabel" ResourceFile="DocumentResources" />
                                    <portal:jDatePicker ID="dpDateEffect2" runat="server"></portal:jDatePicker>
                                    <asp:RequiredFieldValidator ID="rfvDateEffect" runat="server" ControlToValidate="dpDateEffect2"
                                        ErrorMessage="(*)" ValidationGroup="document" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblSigner" runat="server" ForControl="txtSigner" CssClass="settinglabel" ConfigKey="SignerLabel" ResourceFile="DocumentResources" />
                                    <asp:TextBox ID="txtSigner" CssClass="verywidetextbox forminput" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvSigner" runat="server" ControlToValidate="txtSigner"
                                        ErrorMessage="(*)" ValidationGroup="document" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblFilePath" runat="server" ForControl="uploadFile" CssClass="settinglabel" ConfigKey="FilePathLabel" ResourceFile="DocumentResources" />
                                    <asp:Panel ID="pnUpflash" runat="server">
                                        <asp:FileUpload runat="server" ID="uploadFile" />
                                    </asp:Panel>
                                    <br />
                                    <asp:Label ID="lblFileUrlError" runat="server" />
                                </li>
                                <li class="settingrow">
                                    <asp:Panel ID="pnFile1" runat="server">
                                        <mp:SiteLabel ID="lbl1" runat="server" ConfigKey="CurrentFileLabel" ResourceFile="SwirlingQuestionResource" />
                                        <div style="padding: 5px 0px;">
                                            <asp:HyperLink ID="lnkAttach" runat="server" Target="_blank"></asp:HyperLink><asp:ImageButton ID="btnDeleteImg1" runat="server" OnClick="btnDeleteImg1_Click" />
                                        </div>
                                    </asp:Panel>
                                    <portal:mojoLabel ID="lbFailUpload" runat="server" CssClass="txterror" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblItemUrl" runat="server" ForControl="txtItemUrl" CssClass="settinglabel" ConfigKey="ItemUrlLabel" ResourceFile="DocumentResources" />
                                    <asp:TextBox ID="txtItemUrl" CssClass="verywidetextbox forminput" runat="server" />
                                    <span id="spnUrlWarning" runat="server" style="font-weight: normal;" class="txterror"></span>
                                    <asp:HiddenField ID="hdnTitle" runat="server" />
                                    <asp:RegularExpressionValidator ID="regexUrl" runat="server" ControlToValidate="txtItemUrl"
                                        ValidationExpression="((~/){1}\S+)" Display="None" ValidationGroup="document" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblCreatedByUser" runat="server" ForControl="txtCreatedByUser" CssClass="settinglabel" ConfigKey="CreatedByUserLabel" ResourceFile="DocumentResources" />
                                    <asp:TextBox ID="txtCreatedByUser" CssClass="verywidetextbox forminput" runat="server" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblIsApproved" runat="server" ForControl="chkIsApproved" CssClass="settinglabel" ConfigKey="IsApprovedLabel" ResourceFile="DocumentResources" />
                                    <asp:CheckBox ID="chkIsApproved" Checked="true" runat="server" CssClass="forminput" />
                                </li>
                                <li class="settingrow">
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" ValidationGroup="document" />
                                    <asp:Button ID="btnDel" runat="server" CssClass="btn btn-danger" CausesValidation="false" />
                                    <asp:Button ID="btnCancel" CssClass="btn btn-warning" runat="server" />
                                </li>
                            </ol>
                        </div>
                    </div>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
    <style>
        .date input {
            float: left;
        }

        .rqDoc {
            margin-left: 100px !important;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

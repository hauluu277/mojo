<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPostRefer.aspx.cs" Inherits="DocumentFeature.UI.EditPostReferPage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <fieldset>
                        <legend>Thông tin văn bản</legend>

                        <ol class="formlist">
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblLinhVuc" runat="server" ForControl="ddlLinhVuc" CssClass="settinglabel" ConfigKey="LinhVucLabel" ResourceFile="DocumentResources" />
                                <asp:DropDownList ID="ddlLinhVuc" runat="server">
                                    <asp:ListItem Value="0" Text="Chọn lĩnh vực" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblLoaiVB" runat="server" ForControl="txtLoaiVB" CssClass="settinglabel" ConfigKey="LoaiVBLabel" ResourceFile="DocumentResources" />
                                <asp:DropDownList ID="ddlLoaiVB" runat="server">
                                    <asp:ListItem Value="0" Text="Chọn loại văn bản" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblCoQuanID" runat="server" ForControl="ddlCoQuanID" CssClass="settinglabel" ConfigKey="CoQuanIDLabel" ResourceFile="DocumentResources" />
                                <asp:DropDownList ID="ddlCoQuanID" runat="server">
                                    <asp:ListItem Value="0" Text="Chọn cơ quan ban hành" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblSign" runat="server" ForControl="txtSign" CssClass="settinglabel" ConfigKey="SignLabel" ResourceFile="DocumentResources" />
                                <asp:TextBox ID="txtSign" CssClass="verywidetextbox forminput" runat="server" />
                            </li>
                            <li class="settingrow">
                                
                                <mpe:EditorControl ID="edSummary" runat="server">
                                </mpe:EditorControl>
                            </li>
                            <li class="settingrow date">
                                <mp:SiteLabel ID="lblDatePromulgate" runat="server" ForControl="dpDatePromulgate" CssClass="settinglabel" ConfigKey="DatePromulgateLabel" ResourceFile="DocumentResources" />
                                <mp:jsCalendarDatePicker ID="dpDatePromulgate" runat="server" ShowTime="false" CssClass="datetime-input" />
                            </li>
                            <li class="settingrow date">
                                <mp:SiteLabel ID="lblDateEffect" runat="server" ForControl="dpDateEffect" CssClass="settinglabel" ConfigKey="DateEffectLabel" ResourceFile="DocumentResources" />
                                <mp:jsCalendarDatePicker ID="dpDateEffect" runat="server" ShowTime="false" CssClass="datetime-input" />
                            </li>
                            <li class="settingrow">
                                <mp:SiteLabel ID="lblSigner" runat="server" ForControl="txtSigner" CssClass="settinglabel" ConfigKey="SignerLabel" ResourceFile="DocumentResources" />
                                <asp:TextBox ID="txtSigner" CssClass="verywidetextbox forminput" runat="server" />
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
                                <asp:CheckBox ID="chkIsApproved" runat="server" CssClass="forminput" />
                            </li>
                            <li class="settingrow">
                                <asp:Button ID="btnSubmit" runat="server" />
                                <asp:Button ID="btnDel" runat="server" />
                            </li>
                        </ol>
                    </fieldset>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

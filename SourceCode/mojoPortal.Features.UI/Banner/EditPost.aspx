<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPost.aspx.cs" Inherits="BannerFeature.UI.EditPost" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <portal:ModuleTitleControl runat="server" id="TitleControl" />
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <ol class="formlist">
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblName" runat="server" ForControl="txtName" Width="350" CssClass="settinglabel" ConfigKey="NameLabel" ResourceFile="BannerResources" />
                                    <asp:TextBox ID="txtName" CssClass="verywidetextbox forminput" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                        ValidationGroup="banner" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblDescription" runat="server" ForControl="txtDescription" CssClass="settinglabel" ConfigKey="DescriptionLabel" ResourceFile="BannerResources" />
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="forminput verywidetextbox text-area">
                                    </asp:TextBox>
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblLink" runat="server" ForControl="txtLink" CssClass="settinglabel" ConfigKey="LinkLabel" ResourceFile="BannerResources" />
                                    <asp:TextBox ID="txtLink" CssClass="verywidetextbox forminput" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvLink" runat="server" ControlToValidate="txtLink"
                                        ValidationGroup="banner" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblType" runat="server" ForControl="ddlfiletype" CssClass="settinglabel" ConfigKey="TypeLabel" ResourceFile="BannerResources" />
                                    <asp:DropDownList ID="ddlfiletype" runat="server" OnSelectedIndexChanged="ddlfiletype_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqfileType" runat="server" ControlToValidate="ddlfiletype" Text="(Required)" InitialValue="0"></asp:RequiredFieldValidator>
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblFilePath" runat="server" ForControl="nuFilePath" CssClass="settinglabel" ConfigKey="FilePathLabel" ResourceFile="BannerResources" />
                                    <NeatUpload:InputFile ID="nuFilePath" runat="server" />
                                    <asp:Panel ID="pnUpflash" runat="server">
                                        <asp:FileUpload runat="server" ID="uploadFlash" />&nbsp;&nbsp;
                                            <asp:Button ID="btlTaiLen" runat="server" Text="Tải lên" OnClick="btlTaiLen_Click" /><asp:TextBox ID="txtfile" Visible="false" runat="server"></asp:TextBox>
                                    </asp:Panel>

                                    <div class="settingrow">
                                        <asp:UpdatePanel ID="updImgDel" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div runat="server" id="divImage" visible="false">
                                                    <mp:SiteLabel ID="lblCurrentImage" runat="server" ForControl="txtImageUrl" CssClass="settinglabel"
                                                        ConfigKey="BannerCurrentImageLabel" ResourceFile="BannerResources"></mp:SiteLabel>
                                                    <asp:Image runat="server" ID="imgView" Width="100px" Style="max-height: 60px" />
                                                    <%#BuildFlashObject%>
                                                    <asp:ImageButton ID="btnDeleteImg" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:Label ID="lblImageUrlError" CssClass="txterror" runat="server" />
                                </li>
                                <%--                            <li class="settingrow">
                                <mp:SiteLabel ID="lblIsHorizontal" runat="server" ForControl="chkIsHorizontal" CssClass="settinglabel" ConfigKey="IsHorizontalLabel" ResourceFile="BannerResources" />
                                <asp:CheckBox ID="chkIsHorizontal" runat="server" CssClass="forminput" />
                            </li>--%>
                                <li class="settingrow date">
                                    <mp:SiteLabel ID="lblStartDate" runat="server" ForControl="dpBeginDate1" CssClass="settinglabel" ConfigKey="StartDateLabel" ResourceFile="BannerResources" />
                                    <portal:jDatePicker ID="dpBeginDate1" runat="server"></portal:jDatePicker>
                                    <br />
                                    <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ErrorMessage="(Required)" ControlToValidate="dpBeginDate1"
                                        CssClass="txterror" ValidationGroup="banner">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator
                                        ID="dateValidator" runat="server"
                                        Type="Date"
                                        Operator="DataTypeCheck"
                                        ControlToValidate="dpBeginDate1" ValidationGroup="banner">
                                    </asp:CompareValidator>
                                </li>
                                <li class="settingrow date">
                                    <mp:SiteLabel ID="lblEndDate" runat="server" ForControl="dpEndDate2" CssClass="settinglabel" ConfigKey="EndDateLabel" ResourceFile="BannerResources" />
                                    <portal:jDatePicker ID="dpEndDate2" runat="server"></portal:jDatePicker>
                                    <mp:jsCalendarDatePicker ID="JsCalendarDatePicker1" runat="server" ShowTime="false" CssClass="datetime-input" />
                                    <asp:CompareValidator
                                        ID="dateEndValidator" runat="server"
                                        Type="Date"
                                        Operator="DataTypeCheck"
                                        ControlToValidate="dpEndDate2" ValidationGroup="banner">
                                    </asp:CompareValidator>
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="cbkNoClick" CssClass="settinglabel" ConfigKey="NoClickLabel" ResourceFile="BannerResources" />
                                    <asp:CheckBox ID="cbkNoClick" runat="server" CssClass="forminput" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblIsFollow" runat="server" ForControl="chkIsFollow" CssClass="settinglabel" ConfigKey="IsFollowLabel" ResourceFile="BannerResources" />
                                    <asp:CheckBox ID="chkIsFollow" runat="server" CssClass="forminput" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblIsTarget" runat="server" ForControl="chkIsTarget" CssClass="settinglabel" ConfigKey="IsTargetLabel" ResourceFile="BannerResources" />
                                    <asp:CheckBox ID="chkIsTarget" runat="server" CssClass="forminput" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblIsPublic" runat="server" ForControl="chkIsPublic" CssClass="settinglabel" ConfigKey="IsPublicLabel" ResourceFile="BannerResources" />
                                    <asp:CheckBox ID="chkIsPublic" runat="server" CssClass="forminput" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lbPriority" runat="server" ForControl="txtPriority" CssClass="settinglabel" ConfigKey="PriorityLabel" ResourceFile="BannerResources" />
                                    <asp:TextBox ID="txtPriority" CssClass="verywidetextbox forminput disable" runat="server" />
                                    <asp:CompareValidator ID="cvPriority" runat="server" ControlToValidate="txtPriority" Type="Integer"
                                        Operator="DataTypeCheck" ValidationGroup="banner" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lbCreatedByUser" runat="server" ForControl="txtCreatedByUser" CssClass="settinglabel" ConfigKey="CreatedByUserLabel" ResourceFile="BannerResources" />
                                    <asp:TextBox ID="txtCreatedByUser" CssClass="verywidetextbox forminput" runat="server" />
                                </li>
                                <li class="settingrow">
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" ValidationGroup="banner" />
                                    <asp:Button ID="btnDel" runat="server" CssClass="btn btn-danger" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" />
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
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditMediaAlbum.aspx.cs" Inherits="MediaAlbumFeature.UI.EditMediaAlbum" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:HeadingControl ID="heading" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <style type="text/css">
                        .date input {
                            float: left;
                            width: 200px !important;
                        }

                        .album-float-left {
                            float: left;
                        }

                        .label-fix {
                            font-size: 9pt;
                        }

                        .textbox-fix {
                            height: 350px;
                            width: 65%;
                            border-radius: 5px;
                        }

                        .code-fix {
                            width: 65%;
                            height: 50px;
                        }

                        .formlist li {
                            width: 100%;
                            float: left;
                        }
                    </style>
                    <%--                    <script type="text/javascript">
                        $(function () {
                            var ddl = $("#ddlTypeData").val();
                            if (ddl == "1") {
                                $("#divVideo").hide();
                                $("#divCode").hide();
                                $("#rqfCode").hide();
                                $("#rexp").hide();
                                $("#rvfFileUpload").hide();
                            }
                            $("#ddlTypeData").change(function () {
                                if ($(this).val() == "2") {
                                    $("#divVideo").show();
                                    $("#divCode").hide();
                                    $("#rqfCode").hide();
                                    $("#rexp").show();
                                    $("#rvfFileUpload").show();
                                } else if ($(this).val() == "3") {
                                    $("#divVideo").hide();
                                    $("#divCode").show();
                                    $("#rqfCode").show();
                                    $("#rexp").hide();
                                    $("#rvfFileUpload").hide();
                                } else {
                                    $("#divVideo").hide();
                                    $("#divCode").hide();
                                    $("#rqfCode").hide();
                                    $("#rexp").hide();
                                    $("#rvfFileUpload").hide();
                                }

                            });
                        });
                    </script>--%>
                    <div class="width100">
                        <fieldset class="fieldset">
                            <legend class="legend" id="legendMediaAlbum" runat="server"></legend>
                            <ol class="formlist">
         <%--                       <li class="settingrow">
                                    <span class="settinglabel label-fix">
                                        <mp:SiteLabel ID="lblGroupMedia" runat="server" ConfigKey="GroupMultiMediaLabel" ResourceFile="MediaResources" />
                                        <span style="color: red;">*</span>
                                    </span>
                                    <span style="float: left">
                                        <asp:DropDownList ID="drlGroupMedia" runat="server"></asp:DropDownList>
                                    </span>
                                    <asp:RequiredFieldValidator ID="rfvGroupMedia" runat="server" CssClass="label-fix" ControlToValidate="drlGroupMedia" ValidationGroup="albumGroup" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                                </li>--%>
                                <li class="settingrow">
                                    <span class="settinglabel label-fix">Tiêu đề</span>
                                    <asp:TextBox ID="txtTitle" runat="server" Width="65%"></asp:TextBox>
                                </li>
                                <li class="settingrow" style="display: none">
                                    <span class="settinglabel label-fix">
                                        <mp:SiteLabel ID="lblFileName" runat="server" ConfigKey="FileName" ResourceFile="MediaResources" />
                                        <span style="color: red">*</span>
                                    </span>
                                    <asp:TextBox ID="txtFileName" runat="server" Width="65%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFileName" CssClass="label-fix" runat="server" ControlToValidate="txtFileName" ValidationGroup="albumGroup" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                                </li>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                        <li class="settingrow" style="display: none">
                                            <span class="settinglabel label-fix">
                                                <mp:SiteLabel ID="lblTypeData" runat="server" ConfigKey="TypeDataLabel" ResourceFile="MediaResources" />
                                                <%--<span style="color: red">*</span>--%>
                                            </span>
                                            <span style="float: left">
                                                <asp:DropDownList ID="ddlTypeData" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="label-fix" runat="server" ControlToValidate="ddlTypeData" ValidationGroup="albumGroup" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="settingrow">
                                            <span class="settinglabel label-fix">
                                                <mp:SiteLabel ID="lblImageVideo" runat="server" ConfigKey="ImageVideoLabel" ResourceFile="MediaResources" />
                                                <span style="color: red">*</span>
                                            </span>
                                            <span style="float: left">
                                                <NeatUpload:InputFile runat="server" ID="neatUpLoadImage" />
                                                <asp:HiddenField ID="hfImageFilePath" runat="server" />
                                            </span>
                                            <asp:RequiredFieldValidator ID="RqfvFileImage" runat="server" ControlToValidate="neatUpLoadImage" ValidationGroup="albumGroup" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RgExFileImage" runat="server" CssClass="label-fix" ControlToValidate="neatUpLoadImage" ValidationGroup="albumGroup"
                                                ErrorMessage="Only .gif, .jpg, .png"
                                                ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([pP][nN][gG])$)"></asp:RegularExpressionValidator>
                                        </li>
                                        <li class="settingrow" id="pnVideo" runat="server">
                                            <span class="settinglabel label-fix">
                                                <mp:SiteLabel ID="lblFilePath" runat="server" ForControl="uploadFile" ConfigKey="ChooseFileLabel" ResourceFile="MediaResources" />
                                                <span style="color: red">*</span>
                                            </span>
                                            <asp:Panel ID="pnUpflash" runat="server">
                                                <span style="float: left">
                                                    <asp:FileUpload runat="server" ID="uploadFile" />
                                                    <asp:HiddenField ID="hfFileName" runat="server" />
                                                    <asp:HiddenField ID="hfFilePath" runat="server" />
                                                </span>
                                                <asp:RequiredFieldValidator ID="RqfVideo" runat="server" ClientIDMode="Static" CssClass="label-fix" ControlToValidate="uploadFile" ValidationGroup="albumGroup" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RgExVideo" runat="server" CssClass="label-fix" ClientIDMode="Static" ControlToValidate="uploadFile" ValidationGroup="albumGroup"
                                                    ErrorMessage="Only .avi, .mkv, .mp4 "
                                                    ValidationExpression="(.*\.([aA][vV][iI])|.*\.([mM][kK][vV])|.*\.([mM][pP][4])$)"></asp:RegularExpressionValidator>
                                                <asp:Label ID="lblFileUrlError" ForeColor="Red" runat="server" />
                                            </asp:Panel>

                                        </li>
                                        <li class="settingrow" id="pnCode" runat="server">
                                            <span class="settinglabel label-fix">
                                                <mp:SiteLabel ID="SiteLabel2" runat="server" ConfigKey="EmbedCodeLabel" ResourceFile="MediaResources" />
                                                <span style="color: red">*</span>
                                            </span>
                                            <asp:TextBox ID="txtCode" TextMode="MultiLine" CssClass="code-fix" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RqfCode" runat="server" ControlToValidate="txtCode" ValidationGroup="albumGroup" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                                            <p style="clear: both; padding-left: 18%">
                                                <asp:Label ID="lblEmbedCode" runat="server" ForeColor="Red"></asp:Label>
                                            </p>
                                        </li>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <li class="settingrow">
                                    <asp:Panel ID="pnIMG" runat="server">
                                        <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="ImageID" CssClass="settinglabel" ConfigKey="ImageLabel" ResourceFile="MediaResources" />
                                        <img id="ImageID" runat="server" style="margin-left: 15px" width="100" height="100" class="album-float-left" />
                                    </asp:Panel>
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblFeatured" runat="server" Font-Bold="true" ForControl="ccboxFeatured" CssClass="settinglabel" ConfigKey="MediaAlbumFeaturedLable" ResourceFile="MediaResources" />
                                    <asp:CheckBox ID="ccboxFeatured" runat="server" />
                                </li>
                                <li class="settingrow">
                                    <mp:SiteLabel ID="lblPublish" Font-Bold="true" runat="server" ForControl="ccboxPublish" CssClass="settinglabel" ConfigKey="lblPublishLabel" ResourceFile="MediaResources" />
                                    <asp:CheckBox ID="ccboxPublish" runat="server" Checked="true" />
                                </li>
                               <%-- <li class="settingrow">
                                    <span class="settinglabel label-fix">
                                        <mp:SiteLabel ID="lblDescription" runat="server" ConfigKey="MediaAlbumDescriptionLable" ResourceFile="MediaResources" />
                                   
                                    </span>
                                    <asp:TextBox TextMode="MultiLine" ID="txtDescription" CssClass="textbox-fix" runat="server"></asp:TextBox>
                                </li>--%>

                                <li class="settingrow">
                                    <span class="settinglabel" style="margin-top: 10px; margin-left: 15px"></span>
                                    <portal:mojoButton ID="btnSubmit" SkinID="ButtonSuccess" runat="server" ValidationGroup="albumGroup" />
                                    <portal:mojoButton ID="btnDel" SkinID="ButtonDanger" runat="server" />
                                    <portal:mojoButton ID="btnCancel" SkinID="ButtonWarning" runat="server" />
                                </li>
                            </ol>
                        </fieldset>
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

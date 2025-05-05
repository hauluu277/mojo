<%@ Page Language="C#" AutoEventWireup="false" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditWebbuilder.aspx.cs"
    Inherits="mojoPortal.Web.AdminUI.EditWebbuilder" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" CssClass="unselectedcrumb" /><portal:AdminCrumbSeparator ID="litLinkSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkSiteList" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx"
            CssClass="unselectedcrumb" /><portal:AdminCrumbSeparator ID="litLinkSeparator2" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkSiteSettings" runat="server" CssClass="selectedcrumb" />
    </portal:AdminCrumbContainer>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper admin sitesettings">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server" SkinID="admin">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <script src="/Data/plugins/select2/select2.min.js"></script>
                    <link href="/Data/plugins/select2/select2.min.css" rel="stylesheet" />
                    <style>
                        .m-left200 {
                            margin-left: 200px;
                        }

                        .form-control, input[type=text].forminput, select.forminput {
                            display: inline-block;
                            width: calc(100% - 210px);
                            vertical-align: middle;
                        }
                    </style>

                    <asp:Panel ID="pnlSiteSettings" runat="server" DefaultButton="btnSave">
                        <div class="settingrow">
                            <label class="settinglabel">Tên cuộc điều tra</label>
                            <asp:TextBox ID="txtSiteName" TabIndex="10" runat="server" CssClass="forminput widetextbox" />
                            <asp:HyperLink ID="lnkNewSite" runat="server" CssClass="newsitelink" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSiteName" ErrorMessage="Bạn chưa nhập tên cuộc điều tra" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                        <div class="settingrow">
                            <label class="settinglabel" style="float: left; width: 200px">Nhóm cuộc điều tra</label>
                                <asp:DropDownList ID="ddlNhomCuocDieuTra" runat="server" CssClass="select2"></asp:DropDownList>
                        </div>
                        <div id="tabSettings">
                            <asp:Panel ID="pnlNamCuocDieuTra" runat="server" CssClass="settingrow">
                                <label class="settinglabel">Năm cuộc điều tra</label>
                                <asp:DropDownList ID="ddlNamCuocDieuTra" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfYear" runat="server" ControlToValidate="ddlNamCuocDieuTra" ErrorMessage="Bạn chọn năm cuộc điều tra" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <%--<portal:mojoHelpLink ID="MojoHelpLink69" runat="server" HelpKey="meta-profile-help" />--%>
                            </asp:Panel>


                            <asp:Panel ID="Panel1" runat="server" CssClass="settingrow sitemap">
                                <label class="settinglabel">Lĩnh vực điều tra</label>
                                <asp:DropDownList ID="ddlLinhVucDieuTra" runat="server" CssClass="select2"></asp:DropDownList>
                                <portal:mojoLabel ID="lblLinhVucError" runat="server" CssClass="txterror info" />
                            </asp:Panel>
                            <div class="settingrow sitemap">
                                <label class="settinglabel" style="float: left; width: 200px">Đối tượng, đơn vị điều tra</label>
                                <div style="float: left; width: calc(100% - 210px);">
                                    <asp:ListBox ID="lboxDoiTuongDieuTra" SelectionMode="Multiple" runat="server" CssClass="form-control select2"></asp:ListBox>
                                    <portal:mojoLabel ID="lblDoiTuongDieuTraError" runat="server" CssClass="txterror info" />
                                </div>
                            </div>
                            <div class="settingrow sitemap">
                                <label class="settinglabel" style="float: left; width: 200px">Tần suất điều tra</label>
                                <div style="float: left; width: calc(100% - 210px);">
                                    <asp:ListBox ID="lboxTanSuatDieuTra" SelectionMode="Multiple" runat="server" CssClass="form-control select2"></asp:ListBox>
                                    <portal:mojoLabel ID="lblErrorTanSuatDieuTra" runat="server" CssClass="txterror info" />
                                </div>
                            </div>
                            <div class="settingrow sitemap">
                                <label class="settinglabel" style="float: left; width: 200px">Phạm vi tổng hợp số liệu</label>
                                <div style="float: left; width: calc(100% - 210px);">
                                    <asp:ListBox ID="lboxPhamViTongHop" SelectionMode="Multiple" runat="server" CssClass="form-control select2"></asp:ListBox>
                                    <portal:mojoLabel ID="lblPhamViTongHop" runat="server" CssClass="txterror info" />
                                </div>
                            </div>
                            <div class="settingrow sitemap">
                                <label class="settinglabel" style="float: left; width: 200px">Nội dung điều tra</label>
                                <div style="float: left; width: calc(100% - 210px);">
                                    <mpe:EditorControl ID="txtNoiDung" runat="server">
                                    </mpe:EditorControl>
                                </div>
                            </div>
                                              <div class="settingrow sitemap">
                                    <label class="settinglabel" style="float: left; width: 200px">Trạng thái điều tra:</label>
                                    <div style="float: left; width: calc(100% - 210px);">
                                        <asp:DropDownList ID="ddlTrangThaiDieuTra" SkinID="Required" runat="server"  CssClass="select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="settingrow sitemap">
                                    <label class="settinglabel" style="float: left; width: 200px">File dự thảo:</label>
                                    <div style="float: left; width: calc(100% - 210px);">
                                        <asp:TextBox ID="txtFileDuThao" Width="80%" runat="server"></asp:TextBox>
                                        &nbsp;
                                         <button type="button" class="btn btn-default" onclick="GetFileDuThao()"><i class="fa fa-folder-open" aria-hidden="true"></i></button>
                                    </div>
                                </div>
                            <div class="settingrow sitemap">
                                <label class="settinglabel" style="float: left; width: 200px">Ảnh đại diện:</label>
                                <div style="float: left; width: calc(100% - 210px);">
                                    <asp:TextBox ID="txtUrlImage" Width="80%" SkinID="Required" runat="server"></asp:TextBox>
                                    &nbsp;
                                         <button type="button" class="btn btn-default" onclick="GetUrlImage()"><i class="fa fa-folder-open" aria-hidden="true"></i></button>
                                </div>

                            </div>
                        </div>


                        <div class="settingrow">
                            <asp:ValidationSummary ID="vSummary" runat="server" ValidationGroup="sitesettings" />
                            <asp:HiddenField ID="hdnCurrentSkin" runat="server" />
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel35" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                            <portal:mojoButton ID="btnSave" Text="Cập nhật" runat="server" />&nbsp;&nbsp;
			                <portal:mojoButton ID="btnDelete" runat="server" BackColor="Red" Text="Xóa" Visible="false" />&nbsp;&nbsp;
                            <asp:HyperLink ID="hplBack" runat="server"></asp:HyperLink>&nbsp;&nbsp;
                        </div>
                    </asp:Panel>
                    <script src="/ClientScript/ckfinder/ckfinder.js"></script>
                    <script>
                        $(document).ready(function () {
                            $(".select2").select2();
                        })
                        function GetUrlImage() {
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
                                $("#<%=txtUrlImage.ClientID%>").val(fileUrl);
                                $("#viewVideo").empty();
                                $("#viewVideo").append("<img src='" + fileUrl + "' width='200'/>");
                                $("#<%=txtUrlImage.ClientID%>").change();
                            };
                            finder.popup();
                        }

                        function GetFileDuThao()
                        {
                            var finder = new CKFinder();
                            finder.inPopup = true;
                            finder.defaultLanguage = 'vi';
                            finder.language = 'vi';
                            finder.popupFeatures = "width=900,height=900,menubar=yes,toolbar=no,modal=yes";
                            finder.selectMultiple = true;
                            finder.startupPath = "Files:/";
                            finder.BaseUrl = "/Files/";
                            finder.resourceType = 'Files';
                            finder.selectActionFunction = function (fileUrl, data, allFiles) {
                                $("#<%=txtFileDuThao.ClientID%>").val(fileUrl);
                            };
                            finder.popup();
                        }

                    </script>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server"></asp:Content>


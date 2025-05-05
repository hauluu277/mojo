<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="UserMenu.aspx.cs" Inherits="mojoPortal.Web.AdminUI.UserMenu" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper adminmenu">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <ul class="simplelist">
                        <li id="liSiteSettings" runat="server">
                            <asp:HyperLink ID="lnkSiteSettings" runat="server" CssClass="lnkSiteSettings" />
                        </li>
                        <li id="liSiteList" runat="server">
                            <asp:HyperLink ID="lnkSiteList" runat="server" CssClass="lnkSiteList" />
                        </li>
                        <li id="liSettingWebBuilder" runat="server">
                            <asp:HyperLink ID="lnkSettingWebBuilder" runat="server" CssClass="lnkWebBuilder" />
                        </li>
                        <li id="ArticleMenu" runat="server">
                            <asp:HyperLink ID="lnkArticleMenu" runat="server" CssClass="linkArticleMenu"></asp:HyperLink>
                        </li>
                        <li id="liSecurityAdvisor" runat="server" visible="false" class="liSecurityAdvisor">
                            <asp:HyperLink ID="lnkSecurityAdvisor" runat="server" CssClass="lnkSecurityAdvisor" />
                            <span class="secwarning">
                                <asp:Image ID="imgMachineKeyDanger" runat="server" Visible="false" CssClass="securitywarning" />
                                <mp:SiteLabel ID="lblNeedsAttantion" runat="server" CssClass="txterror needsattention" ConfigKey="NeedsAttention" UseLabelTag="false" Visible="false"></mp:SiteLabel>
                            </span>
                        </li>
                        <li id="liRoleAdmin" runat="server">
                            <asp:HyperLink ID="lnkRoleAdmin" runat="server" CssClass="lnkRoleAdmin" />
                        </li>
                        <li id="liPermissions" runat="server">
                            <asp:HyperLink ID="lnkPermissionAdmin" runat="server" CssClass="lnkPermissionAdmin" />
                        </li>
                        <li id="liMemberList" runat="server">
                            <asp:HyperLink ID="lnkMemberList" runat="server" CssClass="lnkMemberList" />
                        </li>
                        <li id="liAddUser" runat="server">
                            <asp:HyperLink ID="lnkAddUser" runat="server" CssClass="lnkAddUser" />
                        </li>
                        <li id="liPageTree" runat="server">
                            <asp:HyperLink ID="lnkPageTree" runat="server" CssClass="lnkPageTree" />
                        </li>
                        <li id="liContentManager" runat="server">
                            <asp:HyperLink ID="lnkContentManager" runat="server" CssClass="lnkContentManager" />
                        </li>
                        <li id="liCategoryManager" runat="server">
                            <asp:HyperLink ID="lnkCategoryManager" runat="server" CssClass="lnkCategory" />
                        </li>
                        <li id="liPageModuleManager" runat="server">
                            <asp:HyperLink ID="lnkPageModuleManager" runat="server" CssClass="lnkPageModuleManager" />
                        </li>
                        <li id="liContentWorkFlow" runat="server">
                            <asp:HyperLink ID="lnkContentWorkFlow" runat="server" CssClass="lnkContentWorkFlow" />
                        </li>
                        <li id="liContentTemplates" runat="server">
                            <asp:HyperLink ID="lnkContentTemplates" runat="server" CssClass="lnkContentTemplates" />
                        </li>
                        <li id="liStyleTemplates" runat="server">
                            <asp:HyperLink ID="lnkStyleTemplates" runat="server" CssClass="lnkStyleTemplates" />
                        </li>
                        <portal:FileManagerLink runat="server" RenderAsListItem="true" CssClass="lnkFileManager" ListItemID="liFileManager" OpenInModal="false" QueryString="?view=fullpage" />
                        <%--<li id="liFileManager" runat="server">
				<asp:HyperLink ID="lnkFileManager" runat="server" CssClass="lnkFileManager" />
			</li>--%>
                        <li id="liNewsletter" runat="server">
                            <asp:HyperLink ID="lnkNewsletter" runat="server" CssClass="lnkNewsletter" />
                        </li>
                        <li id="liRegistrationAgreement" runat="server">
                            <asp:HyperLink ID="lnkRegistrationAgreement" runat="server" CssClass="lnkRegistrationAgreement" />
                        </li>
                        <li id="liLoginInfo" runat="server">
                            <asp:HyperLink ID="lnkLoginInfo" runat="server" CssClass="lnkLoginInfo" />
                        </li>
                        <li id="liCoreData" runat="server">
                            <asp:HyperLink ID="lnkCoreData" runat="server" CssClass="lnkCoreData" />
                        </li>
                        <li id="liAdvancedTools" runat="server">
                            <asp:HyperLink ID="lnkAdvancedTools" runat="server" CssClass="lnkAdvancedTools" />
                        </li>
                        <li id="liLogViewer" runat="server">
                            <asp:HyperLink ID="lnkLogViewer" runat="server" CssClass="lnkLogViewer" />
                        </li>
                        <li id="liServerInfo" runat="server">
                            <asp:HyperLink ID="lnkServerInfo" runat="server" CssClass="lnkServerInfo" />
                        </li>
                        <li id="liCommerceReports" runat="server">
                            <asp:HyperLink ID="lnkCommerceReports" runat="server" CssClass="lnkCommerceReports" />
                        </li>
                        <li id="liDictionary" runat="server">
                            <asp:HyperLink ID="lnkDictionaryManager" runat="server" CssClass="lnkCategoryManager" />
                        </li>
                        <%-- Biểu mẫu thông tin --%>
                        <li id="liBieuMauThongTin" runat="server">
                            <asp:HyperLink ID="lnkBieuMauThongTin" runat="server" CssClass="lnkCategoryManager" />
                        </li>
                        <%-- quản lý kê khai thông tin --%>
                        <li id="liKeKhaiThongTin" runat="server">
                            <asp:HyperLink ID="hplKeKhaiThongTin" runat="server" CssClass="lnkCategoryManager" />
                        </li>
                        <%-- quản lý giao diện --%>
                        <li id="liQuanLyGiaoDien" runat="server">
                            <asp:HyperLink ID="hplQuanLyGiaoDien" runat="server" CssClass="lnkCategoryManager" />
                        </li>
                        <%-- quản lý tin cổng thành viên --%>
                        <li id="liQuanLyTinCongThanhVien" runat="server">
                            <asp:HyperLink ID="hplQuanLyTinCongThanhVien" runat="server" CssClass="lnkCategoryManager" />
                        </li>


                        <%-- thư viện ảnh --%>
                        <li id="liGallery" runat="server">
                            <asp:HyperLink ID="hplGallery" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- thư viện video --%>
                        <li id="liVideo" runat="server">
                            <asp:HyperLink ID="hplVideo" runat="server" CssClass="lnkCategoryManager" />
                        </li>


                        <%-- thư viện audio --%>
                        <li id="liAudio" runat="server">
                            <asp:HyperLink ID="hplAudio" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- quản lý hỏi đáp --%>
                        <li id="liHoiDap" runat="server">
                            <asp:HyperLink ID="hplHoiDap" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- công khai ngân sách --%>
                        <li id="liCongKhaiNganSach" runat="server">
                            <asp:HyperLink ID="hplCongKhaiNganSach" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- thủ tục hành chính --%>
                        <li id="liThuTucHanhChinh" runat="server">
                            <asp:HyperLink ID="hplThuTucHanhChinh" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- lịch công tác --%>
                        <li id="liLichCongTac" runat="server">
                            <asp:HyperLink ID="hplLichCongTac" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- quản lý văn bản --%>
                        <li id="liQuanLyVanBan" runat="server">
                            <asp:HyperLink ID="hplQuanLyVanBan" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- quản lý cổng thành viên --%>
                        <li id="liCongThanhVien" runat="server">
                            <asp:HyperLink ID="hplCongThanhVien" runat="server" CssClass="lnkCategoryManager" />
                        </li>
                        <%-- dự thảo văn bản --%>
                        <li id="liDuThaoVanBan" runat="server">
                            <asp:HyperLink ID="hplDuThaoVanBan" runat="server" CssClass="lnkCategoryManager" />
                        </li>


                        <%-- Quản lý nhật ký --%>
                        <li id="liNhatky" runat="server">
                            <asp:HyperLink ID="hplNhatKy" runat="server" CssClass="lnkCategoryManager" />
                        </li>

                        <%-- Hoạt động người dùng --%>
                        <li id="liHoatDongNguoiDung" runat="server">
                            <asp:HyperLink ID="hplHoatDongNguoiDung" runat="server" CssClass="lnkCategoryManager" />
                        </li>
                        <asp:Literal ID="litSupplementalLinks" runat="server" />
                    </ul>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

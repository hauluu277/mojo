<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="AdminMenu.aspx.cs" Inherits="mojoPortal.Web.AdminUI.AdminMenuPage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper adminmenu">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <ul class="simplelist">
                        <%-- quản trị hệ thống --%>
                        <li id="LiQuanTriHeThong" runat="server">
                            <asp:HyperLink ID="hplQuanTriHeThong" runat="server" CssClass="lnkSystem"></asp:HyperLink>
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


                        <li id="liCategoryManager" runat="server">
                            <asp:HyperLink ID="lnkCategoryManager" runat="server" CssClass="lnkCategory" />
                        </li>
                        <li id="liPageModuleManager" runat="server">
                            <asp:HyperLink ID="lnkPageModuleManager" runat="server" CssClass="lnkPageModuleManager" />
                        </li>
                        <li id="liContentWorkFlow" runat="server">
                            <asp:HyperLink ID="lnkContentWorkFlow" runat="server" CssClass="lnkContentWorkFlow" />
                        </li>
                        <%--thêm--%>

                        <li id="liContentTemplates" runat="server">
                            <asp:HyperLink ID="lnkContentTemplates" runat="server" CssClass="lnkContentTemplates" />
                        </li>
                        <li id="liStyleTemplates" runat="server">
                            <asp:HyperLink ID="lnkStyleTemplates" runat="server" CssClass="lnkStyleTemplates" />
                        </li>
                        <li id="liFileManager" runat="server">
				          <asp:HyperLink ID="lnkFileManager" runat="server" CssClass="lnkFileManager adminlink filemanlink  cblink cboxElement" />
			              </li>
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
                        <li id="liCommerceReports" runat="server">
                            <asp:HyperLink ID="lnkCommerceReports" runat="server" CssClass="lnkCommerceReports" />
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

                        <%-- thư viện ảnh --%>
                        <li id="liGallery" runat="server">
                            <asp:HyperLink ID="hplGallery" runat="server" CssClass="lnkCategoryManager2">[hplGallery]</asp:HyperLink>
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
                            <asp:HyperLink ID="hplDuThaoVanBan" runat="server" CssClass="lnkCategoryManager1">[hplDuThaoVanBan]</asp:HyperLink>
                        </li>


                        <%-- Quản lý nhật ký --%>
                        <li id="liNhatky" runat="server">
                            <asp:HyperLink ID="hplNhatKy" runat="server" CssClass="lnkCategoryManager" />
                        </li>

        
                        <%-- Quản lý liên hệ --%>
                        <li id="liLienHe" runat="server">
                            <asp:HyperLink ID="hplLienHe" runat="server" CssClass="lnkLienHeManager" />
                        </li>
                     
                           <%-- Quản lý chuyên mục liên kết --%>
                        <li id="liChuyenMucLienKet" runat="server">
                            <asp:HyperLink ID="hplChuyenMucLienKet" runat="server" CssClass="lnkLienHeManager" />
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
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server">
</asp:Content>


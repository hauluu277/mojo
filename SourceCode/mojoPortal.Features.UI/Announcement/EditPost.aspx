<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPost.aspx.cs" Inherits="AnnouncementFeature.UI.EditPost" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <asp:Panel ID="pnlEvent" runat="server" DefaultButton="btnUpdate" CssClass="panelwrapper admin editpage blogedit">
        <portal:ModuleTitleControl ID="moduleTitle" runat="server" RenderArtisteer="true"
            UseLowerCaseArtisteerClasses="true" Visible="false" />
        <portal:HeadingControl ID="heading" runat="server" />
        
        <div class="dang_ky_tuyen_sinh ui-widget ui-widget-content">
            <div class="tieude-dm">
                <h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
            </div>
            <div "btnAdd_SV">
                <asp:Button ID="btnAdd_SV" runat="server" />
            </div>
           <%-- <div class="settingrow">
                <label class="settinglabel">Họ và Tên đệm </label>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            </div>--%>

            <div class="settingrow">
                <label class="settinglabel">Ngày thông báo </label>
                 <mp:DatePickerControl ID="DatePickerControlAnno" ClockHours="24" runat="server" />
            </div>

            <div class="settingrow">
                <label class="settinglabel">Nội dung thông báo</label>
                <mpe:EditorControl ID="edContentAnno" runat="server"></mpe:EditorControl>
            </div>


            <div class="settingrow">
                <mp:SiteLabel ID="SiteLabel35" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                <div class="forminput">
                    <asp:HiddenField ID="hdfBenhNhan" ClientIDMode="Static" runat="server" />
                    <NeatUpload:ProgressBar ID="progressBar" runat="server">
                    </NeatUpload:ProgressBar>
                    <portal:mojoButton ID="btnUpdate" runat="server" Text="Đăng kí"/>
                    <asp:HyperLink ID="lnkRecentList" runat="server" />
                </div>
                <br />
                <portal:mojoLabel ID="lblError" runat="server" CssClass="txterror" />
            </div>
            <div class="settingrow">
                <portal:mojoLabel ID="lblErrorMessage" runat="server" CssClass="txterror" />
                &nbsp;
            </div>

        </div>


        </asp:Panel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />




<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="EditPostCategory.aspx.cs" Inherits="mojoPortal.Web.AdminUI.EditPostCategory" MasterPageFile="~/App_MasterPages/layout.Master" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper sharedfiles">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <ol class="formlist">
                        <li class="settingrow">
                            <span class="settinglabel">
                                <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="txtName" ConfigKey="CategoryNameLabel" ResourceFile="Resource" />
                                <span style="color: red">*</span>
                            </span>
                            <asp:TextBox ID="txtName"  runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfFolder" runat="server" ControlToValidate="txtName" ErrorMessage="(*)" ValidationGroup="validateCategory"></asp:RequiredFieldValidator>
                        </li>
                        <li class="settingrow">
                            <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="txtCategoryParent" CssClass="settinglabel" ConfigKey="CategoryParentLabel" ResourceFile="Resource" />
                            <asp:DropDownList ID="drlCategory" placeholder="Chọn danh mục" ClientIDMode="Static" runat="server"></asp:DropDownList>
                        </li>
                        <li class="settingrow">
                            <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="txtOrder" CssClass="settinglabel" ConfigKey="CategoryOrder" ResourceFile="Resource" />
                            <asp:TextBox ID="txtOrder" runat="server"></asp:TextBox>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOrder" ErrorMessage="Bạn phải nhập số" ValidationGroup="validateCategory" ValidationExpression="\d+"></asp:RegularExpressionValidator>

                        </li>
                        <li class="settingrow">
                            <mp:SiteLabel ID="SiteLabel3" runat="server" CssClass="settinglabel" ForControl="txtUrl" ConfigKey="CategoryUrl" ResourceFile="Resource" />
                            <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                        </li>
                        <li class="settingrow">
                            <label class="settinglabel" for="chkIsPhongBan">Hiển thị phòng ban?</label>
                            <asp:CheckBox ID="chkIsPhongBan" runat="server" ClientIDMode="Static" />
                        </li>
         
                        <li class="settingrow">
                            <portal:mojoButton ID="btnSave" runat="server" ValidationGroup="validateCategory" />
                            &nbsp;
                            <portal:mojoButton ID="btnRemove" Visible="false" runat="server" />
                            &nbsp;<asp:LinkButton ID="lnkCancel" runat="server"></asp:LinkButton>
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </li>
                    </ol>
                    <link href="/Data/plugins/select2/select2.min.css" rel="stylesheet" />
                    <script src="/Data/plugins/select2/select2.min.js"></script>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $("#drlCategory").select2({
                                placeholder: "Chọn danh mục",
                                allowClear: true
                            });
                        });
			</script>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
    <portal:SessionKeepAliveControl ID="ka1" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

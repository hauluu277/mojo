<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
    CodeBehind="CoQuanBanHanh.aspx.cs" Inherits="mojoPortal.Web.AdminUI.CoQuanBanHanh" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" />
    </portal:AdminCrumbContainer>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper admin admincountry ">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <fieldset>
                        <legend>Tìm kiếm
                        </legend>
                        <div class="settingrow">
                            Từ khóa
                        </div>
                        <div class="settingrow">
                            <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                        </div>
                        <%--<div class="settingrow">
                            Danh mục cha
                        </div>
                        <div class="settingrow">
                            <asp:DropDownList ID="ddlSearchCategory" runat="server"></asp:DropDownList>
                        </div>--%>
                        <div class="settingrow">
                            <portal:mojoButton ID="btnSearchCoreCategory" runat="server" />
                        </div>
                    </fieldset>
                    <div class="settingrow">
                        <portal:mojoButton ID="btnAddNewTop" runat="server" />
                    </div>
                    <mp:mojoGridView ID="grdContentField" runat="server" AllowPaging="false" AllowSorting="true"
                        AutoGenerateColumns="false" CssClass="" DataKeyNames="ItemID" OnRowDataBound="grdContentField_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" CssClass="buttonlink" Text='<%# Resources.Resource.CoreCategoryGridUpdateButton%>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnGridUpdate" runat="server" Text='<%# Resources.Resource.CoreCategoryGridEditButton  %>'
                                        CommandName="Update" />
                                    <asp:Button ID="btnGridDelete" runat="server" Text='<%# Resources.Resource.CoreCategoryGridDeleteButton %>'
                                        CommandName="Delete" />
                                    <asp:Button ID="btnGridCancel" runat="server" Text='<%# Resources.Resource.CoreCategoryGridCancelButton %>'
                                        CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("Name") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" Columns="20" Text='<%# Eval("Name") %>' runat="server"
                                        MaxLength="255" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("NameEN") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNameEN" Columns="20" Text='<%# Eval("NameEN") %>' runat="server"
                                        MaxLength="255" />
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("ParentName")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCategory" runat="server" />
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("Priority")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPriority" Columns="5" Text='<%# Eval("Priority") %>' runat="server"
                                        MaxLength="2" />
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("Description")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" Columns="20" Text='<%# Eval("Description") %>' runat="server"
                                        MaxLength="255" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </mp:mojoGridView>
                    <div class="settingrow">
                        <portal:mojoButton ID="btnAddNew" runat="server" />
                    </div>
                    <portal:mojoCutePager ID="pgrContentCategory" runat="server" />
                    <portal:EmptyPanel ID="divCleared1" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
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


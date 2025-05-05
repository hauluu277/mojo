<%@ Page Language="c#" CodeBehind="CategoryTemplate.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.AdminUI.CategoryTemplate" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="litLinkSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkWebBuilder" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" />
    </portal:AdminCrumbContainer>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper adminmenu">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <style type="text/css">
                        .modal-title {
                            width: 100%;
                        }

                        .modal-header .close {
                            margin: 0;
                        }

                        .row {
                            padding: 0;
                        }
                    </style>
                    <div style="width: 100%; float: left; padding: 10px 0; text-align: right">
                        <button class="btn btn-primary" onclick="javascript:CreateCategory(); return false;">Thêm mới</button>

                    </div>
                    <asp:Repeater ID="rptCategory" runat="server" SkinID="Article">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>STT</th>
                                        <th>Danh mục</th>
                                        <th>Danh mục cha</th>
                                        <th>Thứ tự</th>
                                        <th>Url</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#rptCategory.Items.Count +1%></td>
                                <td><%#Eval("Name") %></td>
                                <td><%#Eval("ParentName")%></td>
                                <td><%#Eval("Priority")%></td>
                                <td><%#Eval("Description") %></td>
                                <td>
                                    <span id="category_<%#Eval("ItemID") %>" data-name="<%#Eval("Name") %>" data-parent="<%#Eval("ParentID") %>" data-orderby="<%#Eval("Priority") %>" data-url="<%#Eval("Description") %>"></span>
                                    <span title="Sửa danh mục" style="cursor: pointer" onclick="javascript:EditCategory(<%#Eval("ItemID") %>)">
                                        <i class="fa fa-pencil-square-o text-primary" aria-hidden="true"></i>
                                    </span>
                                    <span title="Xóa danh mục" style="cursor: pointer" onclick="javascript:DeleteCategory(<%#Eval("ItemID") %>)">
                                        <i class="fa fa-times text-danger" aria-hidden="true"></i>
                                    </span>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                </table>
                        </FooterTemplate>
                    </asp:Repeater>


                    <div class="modal fade" id="myModal" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Thêm mới danh mục template</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body" style="margin: 0 5px">
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Danh mục</label>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtCategoryName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Danh mục cha</label>
                                        <div class="col-8">
                                            <asp:DropDownList ID="ddlCategory" CssClass="form-control" AppendDataBoundItems="true" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Thứ tự</label>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtOrder" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Url</label>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtUrl" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:HiddenField ID="hdfCategoryID" runat="server" />
                                    <asp:Button ID="btnSaveCategory" runat="server" CssClass="btn btn-success" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                    <script type="text/javascript">
                        function DeleteCategory(id) {
                            if (confirm("Dữ liệu xóa, sẽ không khôi phục được?")) {
                                $.ajax({
                                    type: "POST",
                                    url: "/Admin/WebBuilder/CategoryTemplate.aspx/DeleteCategory",
                                    data: '{ id: "' + id + '" }',
                                    cache: false,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {
                                        location.reload();
                                    }
                                });
                            }
                        }
                        function EditCategory(id) {
                            $("#<%=txtCategoryName.ClientID%>").val($("#category_" + id).attr("data-name"));
                            $("#<%=txtOrder.ClientID%>").val($("#category_" + id).attr("data-orderby"));
                            var parent = $("#category_" + id).attr("data-parent");
                            if (parent != null) {
                                $("#<%=ddlCategory.ClientID%>").val(parent);
                            }
                            $("#<%=txtUrl.ClientID%>").val($("#category_" + id).attr("data-url"));
                            $(".modal-title").text("Cập nhật danh mục website");
                            $("#<%=hdfCategoryID.ClientID%>").val(id);
                            $("#<%=btnSaveCategory.ClientID%>").val("Cập nhật");

                            $("#myModal").modal({ backdrop: "static", keyboard: true });
                        }
                        function CreateCategory() {
                            $(".modal-title").text("Thêm mới danh mục website");
                            $("#<%=txtCategoryName.ClientID%>").text("");
                            $("#<%=txtOrder.ClientID%>").text("");
                            $("#<%=txtUrl.ClientID%>").val("");
                            $("#<%=btnSaveCategory.ClientID%>").val("Thêm mới");
                            $("#<%=hdfCategoryID.ClientID%>").val("");
                            $("#myModal").modal({ backdrop: "static", keyboard: true });
                        }
                    </script>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

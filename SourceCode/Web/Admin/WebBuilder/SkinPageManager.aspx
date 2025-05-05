<%@ Page Language="c#" CodeBehind="SkinPageManager.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.AdminUI.SkinPageManager" %>


<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="litLinkSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkWebBuilder" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator2" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkSkinManager" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" />
    </portal:AdminCrumbContainer>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper adminmenu">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <style type="text/css">
                        .modal-header .close {
                            margin: 0;
                        }

                        .row {
                            padding: 0;
                            margin: 10px;
                        }
                    </style>
                    <div style="width: 100%; float: left; padding: 10px 0; text-align: right">
                        <button class="btn btn-primary" onclick="javascript:CreatePage(); return false;">Thêm mới page</button>
                    </div>
                    <asp:Repeater ID="rptSkinPage" runat="server" SkinID="Article">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>STT</th>
                                        <th>Trang</th>
                                        <th>Trang cha</th>
                                        <th>Danh mục</th>
                                        <th>Thứ tự</th>
                                        <th>Thiết lập tính năng</th>
                                        <th>View page</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#rptSkinPage.Items.Count +1%></td>
                                <td><%#Eval("Title") %></td>
                                <td><%#Eval("ParentName")%></td>
                                <td><%#Eval("CategoryName") %></td>
                                <td><%#Eval("OrderBy")%></td>
                                <td><a href='<%#GetUrlSkinPageFeature(Convert.ToInt32(Eval("SkinID")), Convert.ToInt32(Eval("ItemID"))) %>' title="Thiết lập tính năng trang"><i class="fa fa-cogs" aria-hidden="true"></i></a></td>
                                <td><a href='<%#GetUrlPageBuilder(Convert.ToInt32(Eval("ItemID"))) %>' title="View page"><i class="fa fa-cogs" aria-hidden="true"></i></a></td>
                                <td>
                                    <span id="pagefc_<%#Eval("ItemID") %>" data-category="<%#Eval("CategoryID") %>" data-title="<%#Eval("Title") %>" data-parentid="<%#Eval("ParentID") %>" data-orderby="<%#Eval("OrderBy") %>"></span>
                                    <span title="Sửa trang" style="cursor: pointer" onclick="javascript:EditPage(<%#Eval("ItemID") %>)">
                                        <i class="fa fa-pencil-square-o text-primary" aria-hidden="true"></i>
                                    </span>
                                    <span title="Xóa trang" style="cursor: pointer" onclick="javascript:DeletePage(<%#Eval("ItemID") %>)">
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
                                    <h4 class="modal-title">Thêm mới page</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group row">
                                        <label class="col-4col-form-label">Tên trang</label>
                                        <asp:TextBox SkinID="fullWidth" ID="txtTitlePage" CssClass="col-8 form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4col-form-label">Trang cha</label>
                                        <div class="col-8">
                                            <asp:DropDownList ID="ddlPageParent" SkinID="fullWidth" CssClass="form-control" AppendDataBoundItems="true" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4col-form-label">Danh mục tin bài</label>
                                        <div class="col-8">
                                            <asp:DropDownList ID="ddlCategory" SkinID="fullWidth" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4col-form-label">Thứ tự</label>
                                        <div class="col-8">
                                            <asp:TextBox SkinID="fullWidth" ID="txtOrder" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:HiddenField ID="hdfPageID" runat="server" />
                                    <asp:Button ID="btnCreatePage" runat="server" CssClass="btn btn-success" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div id="create_page" style="display: none">
                    </div>
                    <script type="text/javascript">
                        function DeletePage(id) {
                            if (confirm("Dữ liệu xóa, sẽ không khôi phục được?")) {
                                $.ajax({
                                    type: "POST",
                                    url: "/Admin/WebBuilder/SkinPageManager.aspx/DeletePage",
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
                        function EditPage(id) {
                            $("#<%=txtTitlePage.ClientID%>").val($("#pagefc_" + id).attr("data-title"));
                            $("#<%=txtOrder.ClientID%>").val($("#pagefc_" + id).attr("data-orderby"));
                            $("#<%=ddlCategory.ClientID%>").val($("#pagefc_" + id).attr("data-category"));
                            var parentID = $("#pagefc_" + id).attr("data-parentid");
                            if (parentID != null) {
                                $("#<%=ddlPageParent.ClientID%>").val(parentID);
                            } else {
                                $("#<%=ddlPageParent.ClientID%>").val("");
                            }
                            $(".modal-title").text("Cập nhật trang");
                            $("#<%=hdfPageID.ClientID%>").val(id);
                            $("#<%=btnCreatePage.ClientID%>").val("Cập nhật");

                            $("#myModal").modal({ backdrop: "static" });
                        }
                        function CreatePage() {
                            $(".modal-title").text("Thêm mới trang");
                            $("#<%=txtTitlePage.ClientID%>").text("");
                            $("#<%=txtOrder.ClientID%>").text("");
                            $("#<%=ddlPageParent.ClientID%>").val("");
                            $("#<%=ddlCategory.ClientID%>").val("");
                            $("#<%=btnCreatePage.ClientID%>").val("Thêm mới");
                            $("#<%=hdfPageID.ClientID%>").val("");
                            $("#myModal").modal({ backdrop: "static" });
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

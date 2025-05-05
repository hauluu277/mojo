<%@ Page Language="c#" CodeBehind="SkinManager.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.AdminUI.SkinManager" %>

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
                        <button class="btn btn-primary" onclick="javascript:CreateSkin(); return false;">Thêm mới template website</button>
                    </div>
                    <asp:Repeater ID="rptSkin" runat="server" SkinID="Article">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>STT</th>
                                        <th>Template website</th>
                                        <th>Loại hình template</th>
                                        <th>Danh mục tin bài</th>
                                        <th>Thứ tự</th>
                                        <th>Cấu hình danh mục</th>
                                        <th>Cấu hình trang</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#rptSkin.Items.Count +1%></td>
                                <td><%#Eval("Title") %></td>
                                <td><%#Eval("SkinTypeName")%></td>
                                <td><%#Eval("CategoryName") %></td>
                                <td><%#Eval("OrderBy")%></td>
                                <td><a href='<%#GetUrlSkinCategory(Convert.ToInt32(Eval("ItemID"))) %>' title="Thiết lập danh mục"><i class="fa fa-cogs" aria-hidden="true"></i></a></td>
                                <td><a href='<%#GetUrlSkinPage(Convert.ToInt32(Eval("ItemID"))) %>' title="Thiết lập trang mặc định"><i class="fa fa-cogs" aria-hidden="true"></i></a></td>
                                <td>
                                    <span id="skin_<%#Eval("ItemID") %>" data-category="<%#Eval("CategoryArticle") %>" data-title="<%#Eval("Title") %>" data-skintype="<%#Eval("SkinType") %>" data-orderby="<%#Eval("OrderBy") %>"></span>
                                    <span title="Sửa skin" style="cursor: pointer" onclick="javascript:EditSkin(<%#Eval("ItemID") %>)">
                                        <i class="fa fa-pencil-square-o text-primary" aria-hidden="true"></i>
                                    </span>
                                    <span title="Xóa Skin" style="cursor: pointer" onclick="javascript:DeleteSkin(<%#Eval("ItemID") %>)">
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
                                    <h4 class="modal-title">Thêm mới template website</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body" style="margin: 0 5px">
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Tên template</label>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtSkinTitle" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Loại hình template</label>
                                        <div class="col-8">
                                            <asp:DropDownList ID="ddlSkinTypeCreate" CssClass="form-control" AppendDataBoundItems="true" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Danh mục tin bài</label>
                                        <div class="col-8">
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Thứ tự</label>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtOrder" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:HiddenField ID="hdfSkinID" runat="server" />
                                    <asp:Button ID="btnSaveSkin" runat="server" CssClass="btn btn-success" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                    <script type="text/javascript">
                        function DeleteSkin(id) {
                            if (confirm("Dữ liệu xóa, sẽ không khôi phục được?")) {
                                $.ajax({
                                    type: "POST",
                                    url: "/Admin/WebBuilder/SkinManager.aspx/DeleteSkin",
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
                        function EditSkin(id) {
                            $("#<%=txtSkinTitle.ClientID%>").val($("#skin_" + id).attr("data-title"));
                            $("#<%=txtOrder.ClientID%>").val($("#skin_" + id).attr("data-orderby"));
                            $("#<%=ddlCategory.ClientID%>").val($("#skin_" + id).attr("data-category"));
                            var skinType = $("#skin_" + id).attr("data-skintype");
                            if (skinType != null) {
                                $("#<%=ddlSkinTypeCreate.ClientID%>").val(skinType);
                            }
                            $(".modal-title").text("Cập nhật template website");
                            $("#<%=hdfSkinID.ClientID%>").val(id);
                            $("#<%=btnSaveSkin.ClientID%>").val("Cập nhật");

                            $("#myModal").modal({ backdrop: "static", keyboard: true });
                        }
                        function CreateSkin() {
                            $(".modal-title").text("Thêm mới template website");
                            $("#<%=txtSkinTitle.ClientID%>").text("");
                            $("#<%=txtOrder.ClientID%>").text("");
                            $("#<%=ddlCategory.ClientID%>").val("");
                            $("#<%=btnSaveSkin.ClientID%>").val("Thêm mới");
                            $("#<%=hdfSkinID.ClientID%>").val("");
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

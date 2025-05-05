<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="ManagePost.aspx.cs" Inherits="HomeCategoryFeature.UI.ManagePost" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
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
                                        <th>Tiêu đề</th>
                                        <th>Url</th>
                                        <th width="50%">Mô tả</th>
                                        <th>Hình ảnh</th>
                                        <th>Thứ tự</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#rptCategory.Items.Count +1%></td>
                                <td><%#Eval("Title") %></td>
                                <td><%#Eval("ItemUrl")%></td>
                                <td><%#Eval("Description")%></td>
                                <td>
                                    <img src="<%#Eval("ItemIcon") %>" alt="<%#Eval("Title") %>" width="100" />
                                </td>
                                <td><%#Eval("OrderBy") %></td>
                                <td>
                                    <input type="hidden"
                                        id="category_<%#Eval("ItemID") %>"
                                        data-title="<%#Eval("Title") %>"
                                        data-url="<%#Eval("ItemUrl") %>"
                                        data-description="<%#Eval("Description") %>"
                                        data-icon="<%#Eval("ItemIcon") %>"
                                        data-order="<%#Eval("OrderBy") %>"></input>
                                    <span title="Sửa" style="cursor: pointer" onclick="javascript:EditCategory(<%#Eval("ItemID") %>)">
                                        <i class="fa fa-pencil-square-o text-primary" aria-hidden="true"></i>
                                    </span>
                                    <span title="Xóa" style="cursor: pointer" onclick="javascript:DeleteCategory(<%#Eval("ItemID") %>)">
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
                    <asp:Panel ID="pnlCategoryPager" runat="server" CssClass="ArticlePager">
                        <portal:mojoCutePager ID="pgrCategory" runat="server" />
                    </asp:Panel>

                    <div class="modal fade" id="myModal" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Thêm mới</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body" style="margin: 0 5px">
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Tiêu đề</label>
                                        <div class="col-8">
                                            <asp:TextBox skinID="fullWidth" ID="txtTitle" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Url</label>
                                        <div class="col-8">
                                            <asp:TextBox skinID="fullWidth" ID="txtUrl" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Mô tả</label>
                                        <div class="col-8">
                                            <asp:TextBox skinID="fullWidth" ID="txtDescription" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Hình ảnh</label>
                                        <div class="col-8">
                                            <div class="input-group margin-bottom-sm">
                                                <asp:TextBox skinID="w-500" ID="txtItemIcon" Width="500" runat="server"></asp:TextBox>
                                                <span class="input-group-addon" style="cursor: pointer" onclick="CallCkfinder();"><i class="fa fa-cloud-upload"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-4 col-form-label">Thứ tự</label>
                                        <div class="col-8">
                                            <asp:TextBox skinID="fullWidth" ID="txtOrderBy" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:HiddenField ID="hdfCategoryID" runat="server" />
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" />
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <script type="text/javascript">
                        function CallCkfinder() {
                            var finder = new CKFinder();
                            finder.selectMultiple = false;
                            finder.resourceType = 'Images';
                            finder.connectorInfo = '?Type=Images';
                            finder.defaultLanguage = 'it';
                            finder.defaultDisplayFilesize_Images = true;
                            finder.selectActionFunction = function (url) {
                                $("#<%=txtItemIcon.ClientID%>").val(url);
                            };
                            finder.popup();
                        }
                        function DeleteCategory(id) {
                            if (confirm("Dữ liệu xóa, sẽ không khôi phục được?")) {
                                $.ajax({
                                    type: "POST",
                                    url: "/ModuleBuilder/HomeCategory/ManagePost.aspx/Delete",
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
                            $("#<%=txtTitle.ClientID%>").val($("#category_" + id).attr("data-title"));
                            $("#<%=txtUrl.ClientID%>").val($("#category_" + id).attr("data-url"));
                          $("#<%=txtItemIcon.ClientID%>").val($("#category_" + id).attr("data-icon"));
                          $("#<%=txtOrderBy.ClientID%>").val($("#category_" + id).attr("data-order"));
                          $("#<%=txtDescription.ClientID%>").val($("#category_" + id).attr("data-description"));
                          $(".modal-title").text("Cập nhật");
                          $("#<%=hdfCategoryID.ClientID%>").val(id);
                          $("#<%=btnSave.ClientID%>").val("Cập nhật");

                          $("#myModal").modal({ backdrop: "static", keyboard: true });
                        }
                        function CreateCategory() {
                            $("#<%=txtUrl.ClientID%>").val("");
                            $("#<%=txtUrl.ClientID%>").val("");
                            $("#<%=txtItemIcon.ClientID%>").val("");
                            $("#<%=txtOrderBy.ClientID%>").val("");
                            $("#<%=txtDescription.ClientID%>").val("");
                            $(".modal-title").text("Thêm mới");
                            $("#<%=btnSave.ClientID%>").val("Thêm mới");

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

<%@ Page Language="c#" CodeBehind="WebBuilderManager.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.AdminUI.WebBuilderManager" %>

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
                        <a href="/Admin/WebBuilder/WebBuilderInfor.aspx?siteID=-1" title="Thêm mới template website" class="btn btn-primary">Thêm mới template subPortal</a>
                    </div>
                    <asp:Repeater ID="rptSkin" runat="server" SkinID="Article">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>STT</th>
                                        <th>Template website</th>
                                        <th>Loại hình template</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#rptSkin.Items.Count +1%></td>
                                <td><a href='<%#GetUrlTemplate(Eval("SiteID").ToString()) %>'><%#Eval("SiteName") %></a></td>
                                <td><%#mojoPortal.Web.Components.SiteContants.GetSkinType(Convert.ToInt32(Eval("TemplateType")))%></td>
                                <td>
                                    <span title="Sửa template" style="cursor: pointer" onclick="EditTemplate(<%#Eval("SiteID") %>)">
                                        <i class="fa fa-pencil-square-o text-primary" aria-hidden="true"></i>
                                    </span>
                                    <span title="Xóa Skin" style="cursor: pointer" onclick="javascript:DeleteTemplate(<%#Eval("SiteID") %>)">
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
                    <script type="text/javascript">
                        function EditTemplate(siteId) {
                            window.location.href = '/Admin/SiteSettings.aspx?id=' + siteId + '';
                        }
                        function DeleteTemplate(id) {
                            if (confirm("Dữ liệu xóa, sẽ không khôi phục được?")) {
                                $.ajax({
                                    type: "POST",
                                    url: "/Admin/WebBuilder/WebBuilderManager.aspx/DeleteTemplate",
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

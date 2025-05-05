<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="CategoryArticleManage.aspx.cs" Inherits="mojoPortal.Web.AdminUI.CategoryArticleManage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:AdminCrumbContainer ID="divAdminLinks" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" /><portal:AdminCrumbSeparator ID="litLinkSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkPageTree" runat="server" /><portal:AdminCrumbSeparator ID="altPmSeparator" runat="server" Text="&nbsp;&gt;" EnableViewState="false" Visible="false" />
        <asp:HyperLink ID="lnkAltPageManager" runat="server" Visible="false" />
    </portal:AdminCrumbContainer>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper admin pagetree">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <style>
                        .jqtree-element {
                            position: relative;
                        }

                        ul.jqtree-tree li.jqtree_common {
                            position: relative;
                        }

                        .pagetree ul.treecommands, .popover {
                            z-index: 9;
                        }

                        .pagetree .treecontainer li > div.jqtree-element > span.jqtree-title {
                            font-size: 14px;
                        }

                        ul.jqtree-tree .jqtree-title {
                            margin-left: 0 !important;
                        }

                        .pagetree ul.treecommands .phanquenContent:before {
                            content: '\f044';
                            display: inline-block;
                            font-size: inherit;
                            -webkit-font-smoothing: antialiased;
                            -moz-osx-font-smoothing: grayscale;
                            font: normal normal normal 14px/1 FontAwesome;
                            text-rendering: auto;
                        }
                    </style>

                    <div class="settingrow">
                        <a class="create" href="javascript:FormCategory(0,0)">Thêm mới</a>&nbsp;<portal:mojoLabel ID="litWarning" runat="server" CssClass="txterror warning" />
                    </div>

                    <div class="settingrow">
                        <div id="menuModal" class="modal fade" role="dialog" data-backdrop="static"></div>
                        <div id="tree1" class="treecontainer"></div>
                    </div>
                    <link rel="stylesheet" href="/Data/plugins/jqTree/jqtree.css" />
                    <link rel="stylesheet" href="/Data/style/jqtree.css" />
                    <link href="/Data/plugins/menuContext/jquery.contextMenu.css" rel="stylesheet" />

                    <script src="/ClientScript/jqmojo/tree.jquery.js"></script>
                    <script type="text/javascript">
                        function FormCategory(parentId, id) {
                            var param = {
                                siteId:<%=SiteId%>,
                                parentId: parentId,
                                id: id,
                                addArticle: 1
                            };
                            CallAjaxLoading("get", "/CategoryArea/Category/FormCategory", param, true, function (rs) {
                                $("#menuModal").html(rs);
                                $("#menuModal").modal("show");
                            });
                        }
                        function FormCategory2(parentId, id) {
                            var param = {
                                siteId:<%=SiteId%>,
                                parentId: parentId,
                                id: id
                            }

                            CallAjaxLoading("get", "/CategoryArea/category/FormCategoryPhanNguoiDungChucNang", param, true, function (rs) {
                                $("#menuModal").html(rs);
                                $("#menuModal").modal("show");
                            })

                        }


                       




                        function LoadTree() {
                            var param = {
                                siteId:<%=SiteId%>
                            };
                            CallAjaxLoading("post", "/CategoryArea/Category/GetListArticle", JSON.stringify(param), true, function (rs) {
                                $('#tree1').tree({
                                    data: rs,
                                    autoOpen: 0,
                                    dragAndDrop: true,
                                });
                                $("#tree1 > ul > li:nth-child(1)").removeClass("jqtree_common");
                                $("#tree1 > ul > li:nth-child(1) > div").removeClass("jqtree-element jqtree_common");
                                $("#tree1 > ul > li:nth-child(1) > div > span").removeClass("jqtree-title jqtree_common");
                            });
                        }
                        $(document).ready(function () {
                            LoadTree();

                            $('#tree1').on(
                                'tree.select',
                                function (event) {
                                    if (event.node) {
                                        // node was selected
                                        var node = event.node;
                                        //The clicked node is 'event.node'
                                        var node = event.node;

                                        $("#ulCommands").remove();
                                        var html = "";
                                        html += `<ul id="ulCommands" class="treecommands" style="display: block; left: 248.453px;">`;
                                        html += `<li id="liInfo" class="pageinfo">${node.name}</li>`;
                                        html += `<li id="liEdit" class="newchild">`;
                                        html += `<a id="lnkEdit" class="" href="javascript:FormCategory(${node.id},0)">`;
                                        html += `Thêm mới danh mục con`;
                                        html += `</a>`;
                                        html += `</li>`;
                                        html += `<li id="liEdit" class="editcontent">`;
                                        html += `<a id="lnkEdit" class="" href="javascript:FormCategory(${node.ParentId},${node.id})">`;
                                        html += `Chỉnh sửa danh mục`;
                                        html += `</a>`;
                                        html += `</li>`;
                                        //html += `<li id="liView" class="viewpage">`;
                                        //html += `<a id="lnkView" class="" href="${node.LinkMenu}">`;
                                        //html += `View Page`;
                                        //html += `</a>`;
                                        //html += `</li >`;
                                        //html += `<li id="liSort" class="sortpages" style="">`;
                                        //html += `<a id="lnkSort" class="" href="#">`;
                                        //html += `Sort Child Pages Alphabetically`;
                                        //html += `</a >`;
                                        //html += `</li >`;
                                        html += `<li id="liDeletePage" class="deletepage">`;
                                        html += `<a id="lnkDeletePage" class="" href="javascript:DeleteCategory(${node.id})">`;
                                        html += `Xóa danh mục`;
                                        html += `</a>`;
                                        html += `</li>`;
                                        html += `<li id="liPhanQuyen" class="phanquenContent">`;
                                        html += `<a id="lnkPhanQuyenPage" class="" href="javascript:FormCategory2(${node.id},${node.id})">`;
                                        html += `Phân quyền danh mục`;
                                        html += `</a>`
                                        html += `</li>`






                                        html += `</ul>`;
                                        $(node.element).append(html);
                                    }
                                    else {
                                        // event.node is null
                                        // a node was deselected
                                        // e.previous_node contains the deselected node
                                    }
                                }
                            );
                        });

                        function DeleteCategory(idMenu) {
                            if (confirm("Dữ liệu xóa sẽ không khôi phục được?")) {
                                var param = { id: idMenu };
                                CallAjaxLoading("post", "/CategoryArea/Category/DeleteCategory", JSON.stringify(param), true, function (rs) {
                                    if (rs.Status) {
                                        NotifySuccess("Xóa danh mục thành công");
                                        var node1 = $('#tree1').tree('getNodeById', idMenu);
                                        $('#tree1').tree('removeNode', node1);
                                        //location.reload();
                                    } else {
                                        NotifyError("Không thể thực hiện thao tác này");
                                    }
                                });
                            }
                        }

                    </script>
                    <div class="settingrow inforow">
                        <portal:mojoHelpLink ID="MojoHelpLink4" runat="server" HelpKey="sts-page-menager-help" />
                        <asp:Literal ID="litDemoInfo" runat="server" Visible="false" EnableViewState="false" />
                    </div>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />


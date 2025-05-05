<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="MenuManager.aspx.cs" Inherits="mojoPortal.Web.AdminUI.MenuManager" %>

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
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <link rel="stylesheet" href="/Data/plugins/jqTree/jqtree.css" />
                    <link rel="stylesheet" href="/Data/style/jqtree.css" />
                    <link href="/Data/plugins/menuContext/jquery.contextMenu.css" rel="stylesheet" />
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

                        .panel-danger > .panel-heading, .panel-success > .panel-heading {
                            font-size: 18px;
                        }
                    </style>
                    <div id="menuModal" class="modal fade" role="dialog" data-backdrop="static"></div>
                    <asp:Panel ID="pnlTiengVietMenu" runat="server" CssClass="panel panel-danger">
                        <div class="panel-body">
                            <asp:Panel ID="pnlMenuLeft" CssClass="width1000" runat="server">
                                <portal:HeadingControl ID="headingMenuMain" runat="server" />
                                <div class="settingrow">
                                    <a class="create" href="javascript:CreateMenu(0,0,<%=mojoPortal.Business.WebHelpers.MenuConstant.MenuMain %>,'tree1')"><i class="fa fa-plus-circle" aria-hidden="true"></i>&nbsp;Thêm mới</a>&nbsp;<portal:mojoLabel ID="litWarning" runat="server" CssClass="txterror warning" />
                                </div>
                                <div class="settingrow">
                                    <div id="tree1" class="treecontainer"></div>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlTopMenu" runat="server" CssClass="panel panel-danger">
                               <asp:Panel ID="Panel1" CssClass="width1000" runat="server">
                                <portal:HeadingControl ID="headingMenutop" runat="server" />
                                <div class="settingrow">
                                    <a class="create" href="javascript:CreateMenuTop(0,0)"><i class="fa fa-plus-circle" aria-hidden="true"></i>&nbsp;Thêm mới</a>&nbsp;<portal:mojoLabel ID="MojoLabel1" runat="server" CssClass="txterror warning" />
                                </div>
                                <div class="settingrow">
                                    <div id="treeTop" class="treecontainer"></div>
                                </div>
                            </asp:Panel>
                    </asp:Panel>
                    <script src="/ClientScript/jqmojo/tree.jquery.js"></script>
                    <script type="text/javascript">

                        function LoadTree() {
                            var param = {
                                siteId:<%=SiteId%>,
                                typeMenu:<%=mojoPortal.Business.WebHelpers.MenuConstant.MenuMain %>};
                            CallAjaxLoading("post", "/MenuContextArea/MenuContext/GetTree", JSON.stringify(param), true, function (rs) {
                                $('#tree1').tree({
                                    data: rs,
                                    autoOpen: false,
                                    dragAndDrop: true
                                });
                            });
                        }
                        function LoadTreeTop() {
                            var param = {
                                siteId:<%=SiteId%>,
                                typeMenu:<%=mojoPortal.Business.WebHelpers.MenuConstant.MenuTop %>};
                            CallAjaxLoading("post", "/MenuContextArea/MenuContext/GetTree", JSON.stringify(param), true, function (rs) {
                                $('#treeTop').tree({
                                    data: rs,
                                    autoOpen: false,
                                    dragAndDrop: true
                                });
                            });
                        }

                        $(document).ready(function () {
                            LoadTree();
                            LoadTreeTop();
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
                                        html += `<a id="lnkEdit" class="" href="javascript:CreateMenu(${node.id},0)">`;
                                        html += `Thêm mới menu con`;
                                        html += `</a>`;
                                        html += `</li>`;
                                        html += `<li id="liEdit" class="editcontent">`;
                                        html += `<a id="lnkEdit" class="" href="javascript:CreateMenu(0,${node.id})">`;
                                        html += `Chỉnh sửa menu`;
                                        html += `</a>`;
                                        html += `<li id="liView" class="viewpage">`;
                                        html += `<a id="lnkView" class="" href="${node.LinkMenu}">`;
                                        html += `View Page`;
                                        html += `</a>`;
                                        html += `</li >`;
                                        //html += `<li id="liSort" class="sortpages" style="">`;
                                        //html += `<a id="lnkSort" class="" href="#">`;
                                        //html += `Sort Child Pages Alphabetically`;
                                        //html += `</a >`;
                                        //html += `</li >`;
                                        html += `<li id="liDeletePage" class="deletepage">`;
                                        html += `<a id="lnkDeletePage" class="" href="javascript:DeleteMenu(${node.id},'tree1')">`;
                                        html += `Xóa menu`;
                                        html += `</a>`;
                                        html += `</li>`;
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

                            $('#treeTop').on(
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
                                        html += `<a id="lnkEdit" class="" href="javascript:CreateMenuTop(${node.id},0)">`;
                                        html += `Thêm mới menu con`;
                                        html += `</a>`;
                                        html += `</li>`;
                                        html += `<li id="liEdit" class="editcontent">`;
                                        html += `<a id="lnkEdit" class="" href="javascript:CreateMenuTop(0,${node.id})">`;
                                        html += `Chỉnh sửa menu`;
                                        html += `</a>`;
                                        html += `<li id="liView" class="viewpage">`;
                                        html += `<a id="lnkView" class="" href="${node.LinkMenu}">`;
                                        html += `View Page`;
                                        html += `</a>`;
                                        html += `</li >`;
                                        //html += `<li id="liSort" class="sortpages" style="">`;
                                        //html += `<a id="lnkSort" class="" href="#">`;
                                        //html += `Sort Child Pages Alphabetically`;
                                        //html += `</a >`;
                                        //html += `</li >`;
                                        html += `<li id="liDeletePage" class="deletepage">`;
                                        html += `<a id="lnkDeletePage" class="" href="javascript:DeleteMenu(${node.id},'treeTop')">`;
                                        html += `Xóa menu`;
                                        html += `</a>`;
                                        html += `</li>`;
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


                            setTimeout(function () {
                                $("#tree1 > ul > li:nth-child(1)").removeClass("jqtree_common");
                                $("#tree1 > ul > li:nth-child(1) > div").removeClass("jqtree-element jqtree_common");
                                $("#tree1 > ul > li:nth-child(1) > div > span").removeClass("jqtree-title jqtree_common");


                                $("#treeTop > ul > li:nth-child(1)").removeClass("jqtree_common");
                                $("#treeTop > ul > li:nth-child(1) > div").removeClass("jqtree-element jqtree_common");
                                $("#treeTop > ul > li:nth-child(1) > div > span").removeClass("jqtree-title jqtree_common");


                                $("#treeMenuEnglish > ul > li:nth-child(1)").removeClass("jqtree_common");
                                $("#treeMenuEnglish > ul > li:nth-child(1) > div").removeClass("jqtree-element jqtree_common");
                                $("#treeMenuEnglish > ul > li:nth-child(1) > div > span").removeClass("jqtree-title jqtree_common");


                                $("#treeMenuTopEnglish > ul > li:nth-child(1)").removeClass("jqtree_common");
                                $("#treeMenuTopEnglish > ul > li:nth-child(1) > div").removeClass("jqtree-element jqtree_common");
                                $("#treeMenuTopEnglish > ul > li:nth-child(1) > div > span").removeClass("jqtree-title jqtree_common");
                            }, 2000);
                        });


                        function DeleteMenu(idMenu, treeId) {
                            if (confirm("Dữ liệu xóa sẽ không khôi phục được?")) {
                                var param = { id: idMenu };
                                CallAjaxLoading("post", "/MenuContextArea/MenuContext/DeleteMenu", JSON.stringify(param), true, function (rs) {
                                    if (rs.Status) {
                                        NotifySuccess("Xóa menu thành công");
                                        //location.reload();
                                        var node1 = $('#' + treeId).tree('getNodeById', idMenu);
                                        $('#' + treeId).tree('removeNode', node1);
                                    } else {
                                        NotifyError("Không thể thực hiện thao tác này");
                                    }
                                });
                            }
                        }

                          function CreateMenuTop(parentId, id) {
                            $("#menuModal").html("");
                            var param = {
                                siteId:<%=SiteId%>,
                                typeMenu:<%=mojoPortal.Business.WebHelpers.MenuConstant.MenuTop %>,
                                parentId: parentId,
                                id: id,
                                treeId: 'treeTop'
                            };
                            console.log(param);
                            CallAjaxLoading("get", "/MenuContextArea/MenuContext/FormMenu", param, true, function (rs) {
                                $("#menuModal").html(rs);
                                $("#menuModal").modal("show");
                            });
                        }


                        function CreateMenu(parentId, id) {
                            $("#menuModal").html("");
                            var param = {
                                siteId:<%=SiteId%>,
                                typeMenu:<%=mojoPortal.Business.WebHelpers.MenuConstant.MenuMain %>,
                                parentId: parentId,
                                id: id,
                                treeId: 'tree1'
                            };
                            console.log(param);
                            CallAjaxLoading("get", "/MenuContextArea/MenuContext/FormMenu", param, true, function (rs) {
                                $("#menuModal").html(rs);
                                $("#menuModal").modal("show");
                            });
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


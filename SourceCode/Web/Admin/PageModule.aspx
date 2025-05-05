<%@ Page Language="c#" CodeBehind="PageModule.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.AdminUI.PageModule" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper admin pagelayout">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <style type="text/css">
                        ul.ul-page {
                            width: 100%;
                            float: left;
                            margin-left: 20px;
                        }

                            ul.ul-page li:first-child {
                                font-weight: bold;
                                color: red;
                                font-size: 14px;
                            }

                            ul.ul-page li span {
                                cursor: pointer;
                            }

                            ul.ul-page li input[type="checkbox"] {
                                margin-right: 12px;
                            }
                    </style>
                    <script type="text/javascript">
                        function NotifyPageSuccess() {
                            NotifySuccess("Cắm module vào các page thành công");
                        }
                        function GetPage() {
                            var pageArr = [];
                            $("ul.ul-page li input[type=checkbox]:checked").each(function (index, element) {
                                var pageId = $(element).attr("data-id");
                                if (pageId) {
                                    pageArr.push(pageId);
                                }
                            });
                            var stringArr = pageArr.join(",");
                            $("#<%=hdfPages.ClientID%>").val(stringArr);
                            console.log(stringArr);
                            return true;
                        }
                        $(document).on("click", "ul.ul-page li span", function () {
                            var id = $(this).prev().attr("id");
                            if (id == "all_page") {
                                var checked = $("#all_page").prop("checked");
                                if (checked == true) {
                                    $("ul.ul-page input[type=checkbox]").prop("checked", false);
                                } else {
                                    $("ul.ul-page input[type=checkbox]").prop("checked", true);
                                }
                            } else {
                                if ($(this).prev().prop("checked") == true) {
                                    $(this).prev().prop("checked", false);
                                } else {
                                    $(this).prev().prop("checked", true);
                                    var itemId = $(this).prev().attr("data-id");
                                    $("ul.ul-page input[data-parentid=" + itemId + "]").prop("checked", true);
                                }
                            }

                        });


                        $(document).on("change", "ul.ul-page input[type=checkbox]", function () {
                            var itemId = $(this).attr("data-id");
                            var id = $(this).attr("id");
                            if (id == "all_page") {
                                if ($(this).prop("checked") == true) {
                                    $("ul.ul-page input[type=checkbox]").prop("checked", true);
                                } else {
                                    $("ul.ul-page input[type=checkbox]").prop("checked", false);

                                }
                            } else {
                                $("ul.ul-page input[data-parentid=" + itemId + "]").prop("checked", true);
                            }
                        });
                    </script>
                    <asp:Panel ID="pnlContent" runat="server" DefaultButton="btnCreateNewContent">
                        <portal:PageLayoutDisplaySettings ID="displaySettings" runat="server" />
                        <div id="divAdminLinks" runat="server">
                            <asp:HyperLink ID="lnkEditSettings" EnableViewState="false" runat="server" /><asp:Literal
                                ID="litLinkSpacer1" runat="server" EnableViewState="false" />
                            <asp:HyperLink ID="lnkViewPage" runat="server" EnableViewState="false"></asp:HyperLink><asp:Literal
                                ID="litLinkSpacer2" runat="server" EnableViewState="false" />
                            <asp:HyperLink ID="lnkPageTree" runat="server" />
                        </div>
                        <asp:UpdatePanel ID="upLayout" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class="settings">
                                    <div class="addcontent">
                                        <strong>
                                            <mp:SiteLabel ID="lblAddModule" runat="server" ConfigKey="PageLayoutAddModuleLabel"
                                                UseLabelTag="false"></mp:SiteLabel>
                                        </strong>
                                        <div class="settingrow">
                                            <mp:SiteLabel ID="lblModuleType" runat="server" ForControl="moduleType" CssClass="settinglabel"
                                                ConfigKey="PageLayoutModuleTypeLabel"></mp:SiteLabel>
                                            <asp:DropDownList ID="moduleType" runat="server" EnableTheming="false" CssClass="forminput"
                                                DataValueField="ModuleDefID" DataTextField="FeatureName">
                                            </asp:DropDownList>
                                            <portal:mojoHelpLink ID="MojoHelpLink1" runat="server" HelpKey="pagelayoutmoduletypehelp" />
                                        </div>
                                        <div class="settingrow">
                                            <mp:SiteLabel ID="lblModuleName" runat="server" ForControl="moduleTitle" CssClass="settinglabel"
                                                ConfigKey="PageLayoutModuleNameLabel"></mp:SiteLabel>
                                            <asp:TextBox ID="moduleTitle" runat="server" CssClass="widetextbox forminput" Text=""
                                                EnableViewState="false"></asp:TextBox>
                                            <portal:mojoHelpLink ID="MojoHelpLink2" runat="server" HelpKey="pagelayoutmodulenamehelp" />
                                            <asp:RequiredFieldValidator ID="reqModuleTitle" runat="server" ControlToValidate="moduleTitle" ValidationGroup="pagelayout" />
                                            <asp:CompareValidator ID="cvModuleTitle" runat="server" Operator="NotEqual" ControlToValidate="moduleTitle" ValidationGroup="pagelayout" />
                                        </div>
                                        <div class="settingrow">
                                            <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="ddPaneNames" CssClass="settinglabel"
                                                ConfigKey="PageLayoutLocationLabel"></mp:SiteLabel>
                                            <asp:DropDownList ID="ddPaneNames" runat="server" EnableTheming="false" CssClass="forminput"
                                                DataTextField="key" DataValueField="value">
                                            </asp:DropDownList>
                                            <portal:mojoHelpLink ID="MojoHelpLink3" runat="server" HelpKey="pagelayoutmodulelocationhelp" />
                                            <asp:HyperLink ID="lnkGlobalContent" runat="server" Visible="false" />
                                            <asp:HiddenField ID="hdnModuleID" runat="server" />
                                            <asp:ImageButton ID="btnAddExisting" runat="server" />
                                        </div>
                                        <div class="settingrow">
                                            <mp:SiteLabel ID="lblOrganizeModules" runat="server" CssClass="settinglabel" ConfigKey="EmptyLabel"
                                                UseLabelTag="false"></mp:SiteLabel>
                                            <portal:mojoButton ID="btnCreateNewContent" CausesValidation="false" OnClientClick="return GetPage();" runat="server" CssClass="forminput" ValidationGroup="pagelayout" />
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdfPages" runat="server" />
                                    <div class="panelPage">
                                        <asp:Literal ID="literLoadPage" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared">
            </portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

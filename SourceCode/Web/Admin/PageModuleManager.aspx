<%@ Page Language="c#" CodeBehind="PageModuleManager.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.AdminUI.PageModuleManager" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper admin pagelayout">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <asp:Panel ID="pnlContent" runat="server" Visible="False" DefaultButton="btnCreateNewContent">
                        <portal:PageLayoutDisplaySettings ID="displaySettings" runat="server" />
                        <asp:UpdatePanel ID="upLayout" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class="settings">
                                    <div class="adddefaultcontent">
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
                                        </div>
                                        <div class="settingrow">
                                            <mp:SiteLabel ID="lblOrganizeModules" runat="server" CssClass="settinglabel" ConfigKey="EmptyLabel"
                                                UseLabelTag="false"></mp:SiteLabel>
                                            <portal:mojoButton ID="btnCreateNewContent" runat="server" CssClass="forminput" ValidationGroup="pagelayout" />
                                        </div>
                                    </div>
                                    <div class="panelayout">
                                        <asp:Panel class="altlayoutnotice" ID="divAltLayoutNotice" runat="server" SkinID="notice">
                                            <asp:Literal ID="litAltLayoutNotice" runat="server" />
                                        </asp:Panel>
                                        <div class="pane layoutalt1" id="divAltPanel1" runat="server">
                                            <h2>
                                                <mp:SiteLabel ID="lblTopPane" runat="server" ConfigKey="PageLayoutAltPanel1Label"
                                                    UseLabelTag="false" />
                                            </h2>
                                            <div class="panelistbox">
                                                <asp:ListBox ID="topPane" runat="server" DataValueField="ItemID" DataTextField="ModuleTitle"
                                                    Rows="7" />
                                                <div class="layoutbuttons">
                                                    <asp:ImageButton ID="btnTopUp" runat="server" ImageUrl="~/Data/SiteImages/up.gif"
                                                        CommandName="up" CommandArgument="TopPane" SkinID="pageLayoutMoveUp" CssClass="btnup" />
                                                    <asp:ImageButton ID="btnTopDown" runat="server" ImageUrl="~/Data/SiteImages/dn.gif"
                                                        CommandName="down" CommandArgument="TopPane" SkinID="pageLayoutMoveDown"
                                                        CssClass="btndown" />
                                                    <asp:ImageButton ID="btnTopCenter" runat="server" ImageUrl="~/Data/SiteImages/dn2.gif"
                                                        CssClass="btndownpanel" SkinID="pageLayoutItemMoveDown" />
                                                    <asp:ImageButton ID="btnTopDelete" runat="server" CommandName="delete" CommandArgument="TopPane"
                                                        SkinID="pageLayoutDeleteItem" CssClass="btnremove" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="regularpanes">
                                            <div class="pane layoutleft">
                                                <h2>
                                                    <mp:SiteLabel ID="lblLeftPane" runat="server" ConfigKey="PageLayoutLeftPaneLabel"
                                                        UseLabelTag="false" />
                                                </h2>
                                                <div class="panelistbox">
                                                    <asp:ListBox ID="leftPane" runat="server" DataValueField="ItemID" DataTextField="ModuleTitle"
                                                        Rows="10" />
                                                    <div class="layoutbuttons">
                                                        <asp:ImageButton ID="LeftUpBtn" runat="server" ImageUrl="~/Data/SiteImages/up.gif"
                                                            CommandName="up" CommandArgument="LeftPane" SkinID="pageLayoutMoveUp" CssClass="btnup" />
                                                        <asp:ImageButton ID="LeftDownBtn" runat="server" ImageUrl="~/Data/SiteImages/dn.gif"
                                                            CommandName="down" CommandArgument="LeftPane" SkinID="pageLayoutMoveDown" CssClass="btndown" />
                                                        <asp:ImageButton ID="LeftRightBtn" runat="server" ImageUrl="~/Data/SiteImages/rt2.gif"
                                                            CommandName="right" SkinID="pageLayoutMoveItemRight" CssClass="btnright" />
                                                        <asp:ImageButton ID="LeftDeleteBtn" runat="server" CommandName="delete" CommandArgument="LeftPane"
                                                            SkinID="pageLayoutDeleteItem" CssClass="btnremove" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="pane layoutcenter">
                                                <h2>
                                                    <mp:SiteLabel ID="lblContentPane" runat="server" ConfigKey="PageLayoutContentPaneLabel"
                                                        UseLabelTag="false" />
                                                </h2>
                                                <div class="panelistbox">
                                                    <asp:ListBox ID="contentPane" runat="server" DataValueField="ItemID" DataTextField="ModuleTitle"
                                                        Rows="10" />
                                                    <div class="layoutbuttons">
                                                        <asp:ImageButton ID="ContentUpBtn" runat="server" ImageUrl="~/Data/SiteImages/up.gif"
                                                            CommandName="up" CommandArgument="ContentPane" SkinID="pageLayoutMoveUp" CssClass="btnup" />
                                                        <asp:ImageButton ID="ContentDownBtn" runat="server" ImageUrl="~/Data/SiteImages/dn.gif"
                                                            CommandName="down" CommandArgument="ContentPane" SkinID="pageLayoutMoveDown"
                                                            CssClass="btndown" />
                                                        <asp:ImageButton ID="ContentLeftBtn" runat="server" ImageUrl="~/Data/SiteImages/lt2.gif"
                                                            SkinID="pageLayoutMoveItemLeft" CssClass="btnleft" />
                                                        <asp:ImageButton ID="ContentRightBtn" runat="server" ImageUrl="~/Data/SiteImages/rt2.gif"
                                                            SkinID="pageLayoutMoveItemRight" CssClass="btnright" />
                                                        <asp:ImageButton ID="ContentUpToNextButton" runat="server" ImageUrl="~/Data/SiteImages/up2.gif"
                                                            CommandName="uptoalt1" CommandArgument="ContentPane" SkinID="pageLayoutMoveItemUp"
                                                            CssClass="btnuppanel" />
                                                        <asp:ImageButton ID="ContentDownToNextButton" runat="server" ImageUrl="~/Data/SiteImages/dn2.gif"
                                                            CommandName="downtoalt2" CommandArgument="ContentPane" SkinID="pageLayoutItemMoveDown"
                                                            CssClass="btndownpanel" />
                                                        <asp:ImageButton ID="ContentDeleteBtn" runat="server" CommandName="delete" CommandArgument="ContentPane"
                                                            SkinID="pageLayoutDeleteItem" CssClass="btnremove" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="pane layoutright">
                                                <h2>
                                                    <mp:SiteLabel ID="lblRightPane" runat="server" ConfigKey="PageLayoutRightPaneLabel"
                                                        UseLabelTag="false" />
                                                </h2>
                                                <div class="panelistbox">
                                                    <asp:ListBox ID="rightPane" runat="server" DataValueField="ItemID" DataTextField="ModuleTitle"
                                                        Rows="10" />
                                                    <div class="layoutbuttons">
                                                        <asp:ImageButton ID="RightUpBtn" runat="server" ImageUrl="~/Data/SiteImages/up.gif"
                                                            CommandName="up" CommandArgument="RightPane" SkinID="pageLayoutMoveUp" CssClass="btnup" />
                                                        <asp:ImageButton ID="RightDownBtn" runat="server" ImageUrl="~/Data/SiteImages/dn.gif"
                                                            CommandName="down" CommandArgument="RightPane" SkinID="pageLayoutMoveDown" CssClass="btndown" />
                                                        <asp:ImageButton ID="RightLeftBtn" runat="server" ImageUrl="~/Data/SiteImages/lt2.gif"
                                                            SkinID="pageLayoutMoveItemLeft" CssClass="btnleft" />
                                                        <asp:ImageButton ID="RightDeleteBtn" runat="server" CommandName="delete" CommandArgument="RightPane"
                                                            SkinID="pageLayoutDeleteItem" CssClass="btnremove" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="pane layoutalt2" id="divAltPanel2" runat="server">
                                            <h2>
                                                <mp:SiteLabel ID="lblBottomPane" runat="server" ConfigKey="PageLayoutAltPanel2Label"
                                                    UseLabelTag="false" />
                                            </h2>
                                            <div class="panelistbox">
                                                <asp:ListBox ID="bottomPane" runat="server" DataValueField="ItemID" DataTextField="ModuleTitle"
                                                    Rows="7" />
                                                <div class="layoutbuttons">
                                                    <%-- <asp:ImageButton ID="btnMoveAlt2ToAlt1" runat="server" ImageUrl="~/Data/SiteImages/up.gif" />--%>
                                                    <asp:ImageButton ID="btnBottomUp" runat="server" ImageUrl="~/Data/SiteImages/up.gif"
                                                        CommandName="up" CommandArgument="BottomPane" SkinID="pageLayoutMoveUp" CssClass="btnup" />
                                                    <asp:ImageButton ID="btnBottomDown" runat="server" ImageUrl="~/Data/SiteImages/dn.gif"
                                                        CommandName="down" CommandArgument="BottomPane" SkinID="pageLayoutMoveDown"
                                                        CssClass="btndown" />
                                                    <asp:ImageButton ID="btnBottomCenter" runat="server" ImageUrl="~/Data/SiteImages/up2.gif"
                                                        SkinID="pageLayoutMoveItemUp" CssClass="btnuppanel" />
                                                    <asp:ImageButton ID="btnBottomDelete" runat="server" CommandName="delete" CommandArgument="BottomPane"
                                                        SkinID="pageLayoutDeleteItem" CssClass="btnremove" />
                                                </div>
                                            </div>
                                        </div>
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
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server">
</asp:Content>


<%@ Page Language="c#" CodeBehind="Login.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" Inherits="mojoPortal.Web.UI.Pages.LoginPage" %>

<%@ Register TagPrefix="mp" TagName="Login" Src="~/Controls/LoginControl.ascx" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <asp:Panel ID="pnlLogin" runat="server" CssClass="panelwrapper login">
        <div class="modulecontent">
            <style>
                .rememberrow{
                    display: none;
                }
            </style>
            <fieldset>
                <legend>
                    <mp:SiteLabel ID="SiteLabel1" runat="server" ConfigKey="SignInLabel" UseLabelTag="false"></mp:SiteLabel>
                </legend>
                <asp:Panel ID="pnlTopContent" runat="server" Visible="false" CssClass="logincontent logincontenttop" EnableViewState="false">
                    <asp:Literal ID="litTopContent" runat="server" EnableViewState="false" />
                </asp:Panel>
                <asp:Panel ID="pnlStandardLogin" runat="server" CssClass="floatpanel">
                    <mp:Login ID="login1" runat="server" />
                </asp:Panel>
                <div class="floatpanel">
                    <asp:Panel ID="pnlWindowsLive" runat="server" Visible="false">
                        <div style="padding: 0px 0px 0px 6px;">
                            <portal:WindowsLiveLoginControl ID="livelogin" runat="server" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="divLiteralOr" runat="server" Visible="false" CssClass="clearpanel">
                        <asp:Literal ID="litOr" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlOpenID" runat="server" Visible="false"></asp:Panel>
                </div>
                <asp:Panel ID="pnlBottomContent" runat="server" Visible="false" CssClass="clearpanel logincontent logincontentbottom" EnableViewState="false">
                    <asp:Literal ID="litBottomContent" runat="server" EnableViewState="false" />
                </asp:Panel>
            </fieldset>
        </div>
    </asp:Panel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LoginControl.ascx.cs"
    Inherits="mojoPortal.Web.UI.LoginControl" %>
<style>
    #cboxDieuHanh {
        margin-left: 80px;
    }

    .lnkpasswordrecovery {
        display: none
    }
</style>
<portal:SiteLogin ID="LoginCtrl" runat="server" CssClass="logincontrol">
    <LayoutTemplate>
        <asp:Panel ID="pnlLContainer" runat="server" DefaultButton="Login">
            <div class="settingrow idrow">
                <strong>
                    <mp:SiteLabel ID="lblEmail" runat="server" ForControl="UserName" ConfigKey="SignInEmailLabel"></mp:SiteLabel>
                    <mp:SiteLabel ID="lblUserID" runat="server" ForControl="UserName" ConfigKey="ManageUsersLoginNameLabel"></mp:SiteLabel>
                </strong>
                <br />
                <asp:TextBox ID="UserName" runat="server" CssClass="normaltextbox signinbox" MaxLength="100" />
            </div>
            <div class="settingrow passwordrow">
                <strong>
                    <mp:SiteLabel ID="lblPassword" runat="server" ForControl="Password" ConfigKey="SignInPasswordLabel"></mp:SiteLabel>
                </strong>
                <br />
                <asp:TextBox ID="Password" runat="server" CssClass="normaltextbox passwordbox" TextMode="password" />
            </div>
            <div class="settingrow rememberrow">
                <asp:CheckBox ID="RememberMe" runat="server" Visible="false" />
                <asp:CheckBox ID="cboxPortal" runat="server" ClientIDMode="Static" />
                <asp:CheckBox ID="cboxDieuHanh" runat="server" ClientIDMode="Static" />
            </div>
            <asp:Panel class="settingrow" ID="divCaptcha" runat="server">
                <mp:CaptchaControl ID="captcha" runat="server" />
            </asp:Panel>
            <div class="settingrow buttonrow">
                <portal:mojoButton ID="Login" CommandName="Login" runat="server" Text="Login" />
                <asp:HyperLink ID="hplLoginSSO2" runat="server" CssClass="btn btn-dange"></asp:HyperLink>
                <br />
                <portal:mojoLabel ID="FailureText" runat="server" CssClass="txterror" EnableViewState="false" />
            </div>

            <script type="text/javascript">
                $(document).ready(function () {
                    $("#cboxPortal").change(function () {
                        $("#cboxDieuHanh").attr("checked", false);
                    });
                    $("#cboxDieuHanh").change(function () {
                        $("#cboxPortal").attr("checked", false);
                    });
                });
            </script>
        </asp:Panel>
        <asp:Panel ID="pnlLoginSSO" runat="server">
            <asp:HyperLink ID="hplLoginSSo" runat="server" CssClass="btn btn-danger"></asp:HyperLink>
        </asp:Panel>
                    <div class="settingrow registerrow btn_link-dangky">
                <asp:HyperLink ID="lnkPasswordRecovery" runat="server" CssClass="lnkpasswordrecovery" />
                <portal:RegisterLink ID="rgister" runat="server" RenderAsListItem="true" />
                <asp:HyperLink ID="lnkRegisterExtraLink" runat="server" CssClass="lnkregister" />
            </div>
    </LayoutTemplate>
</portal:SiteLogin>


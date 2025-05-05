<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CommentEditor.ascx.cs" Inherits="mojoPortal.Web.UI.CommentEditor" %>
<portal:CommentSystemDisplaySettings ID="displaySettings" runat="server" />
<asp:Panel ID="pnlNewComment" runat="server" CssClass="blogcommentservice commenteditpanel">

    <fieldset>
        <%-- <div id="divTitle" runat="server" class="settingrow">
        <mp:SiteLabel ID="lblCommentTitle" runat="server" ForControl="txtCommentTitle" CssClass="settinglabel"
            ConfigKey="CommentTitle" ResourceFile="Resource" EnableViewState="false" />
        <asp:TextBox ID="txtCommentTitle" runat="server" MaxLength="100" EnableViewState="true" CssClass="forminput widetextbox" />
    </div>--%>
        <h2 id="module1461" class="moduletitle comment-heading comment-title">
            <span class="art-postheadericon">Bình luận</span>
        </h2>
        <asp:UpdatePanel ID="pnlUpdate" runat="server">
            <ContentTemplate>
                <div id="divUserName" runat="server" class="settingrow">
                    <asp:TextBox ID="txtName" placeholder="Họ và tên" runat="server" MaxLength="100" EnableViewState="true" CssClass="forminput widetextbox" />
                    <asp:RequiredFieldValidator ID="reqName" runat="server" ValidationGroup="comments" ControlToValidate="txtName" Display="Dynamic" EnableClientScript="true" SetFocusOnError="true" />
                </div>
                <div id="divUserEmail" runat="server" class="settingrow">
                    <asp:TextBox ID="txtEmail" placeholder="Địa chỉ email" runat="server" MaxLength="100" EnableViewState="true" CssClass="forminput widetextbox" />
                    <portal:EmailValidator ID="regexEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="comments" Display="Dynamic" EnableClientScript="true" SetFocusOnError="true" />
                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" ValidationGroup="comments" ControlToValidate="txtEmail" Display="Dynamic" EnableClientScript="true" SetFocusOnError="true" />
                </div>

                <%--<div id="divCommentUrl" runat="server" class="settingrow">
        <mp:SiteLabel ID="lblCommentURL" runat="server" ForControl="txtURL" CssClass="settinglabel"
            ConfigKey="CommentUrl" ResourceFile="Resource" EnableViewState="false" />
        <asp:TextBox ID="txtURL" runat="server" MaxLength="200" EnableViewState="true" CssClass="forminput widetextbox" />
        <asp:RegularExpressionValidator ID="regexUrl" runat="server" ControlToValidate="txtURL" SetFocusOnError="true"
            ValidationGroup="comments" ValidationExpression="(((http(s?))\://){1}\S+)" />
    </div>--%>
                <asp:Panel ID="pnlRemeberMe" runat="server" CssClass="settingrow">
                    <mp:SiteLabel ID="lblRememberMe" runat="server" ForControl="chkRememberMe" CssClass="settinglabel"
                        ConfigKey="SignInSendRememberMeLabel" ResourceFile="Resource" EnableViewState="false" />
                    <asp:CheckBox ID="chkRememberMe" runat="server" EnableViewState="false" CssClass="forminput" />
                </asp:Panel>
                <div class="settingrow">
                    <%--<mpe:EditorControl ID="edComment" runat="server"></mpe:EditorControl>--%>
                    <%--<asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" placeholder="Ý kiến của bạn"></asp:TextBox>--%>
                    <mpe:EditorControl ID="edComment" runat="server"></mpe:EditorControl>

                </div>
                <asp:Panel ID="pnlAntiSpam" runat="server" Visible="false">
                    <mp:CaptchaControl ID="captcha" runat="server" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPostComment" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="settingrow" style="margin-top: 15px;">
            <portal:mojoButton ID="btnPostComment" runat="server" SkinID="ButtonPrimary" Text="Gửi" ValidationGroup="comments" />
        </div>
    </fieldset>
</asp:Panel>

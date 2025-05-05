<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/DialogMaster.Master" CodeBehind="MessageListDialog.aspx.cs" Inherits="mojoPortal.Web.ContactUI.MessageListDialog" %>

<%@ Register Namespace="mojoPortal.Web.ContactUI" Assembly="mojoPortal.Features.UI" TagPrefix="contact" %>

<asp:Content ContentPlaceHolderID="phHead" ID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="phMain" ID="MainContent" runat="server">
    <contact:ContactFormDisplaySettings runat="server" ID="displaySettings" />

    <script type="text/javascript">
        function GetMessage(messageGuid, context) {
			<%= sCallBackFunctionInvocation %>
        }

        function GetMeGetViewShowSendMailssage(messageGuid, context) {
			<%= sCallBackFunctionInvocation %>
        }

        function ShowMessage(message, context) {
            document.getElementById('<%= pnlMessage.ClientID %>').innerHTML = message;
        }



        function OnError(message, context) {
            //alert('An unhandled exception has occurred:\n' + message);
        }
	</script>

    <portal:BasePanel ID="pnlContainer" runat="server" CssClass="container-fluid" RenderId="false">
        <asp:Panel ID="pnlSearch" runat="server" CssClass="search-header" DefaultButton="btnSearch">
            <h3 class="h3-search">Tìm kiếm liên hệ</h3>
            <div class="col-sm-12" style="border: 1px solid #eee; padding: 20px;">
                <div class="col-sm-4 hide">
                    <label class="control-label">Họ tên</label>
                    <asp:TextBox ID="txtHoTen" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <label class="control-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                        <asp:HyperLink ID="lnkRefresh" runat="server" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch_message" Text="Tìm kiếm" />
                </div>
            </div>
            <br />
        </asp:Panel>
        <portal:BasePanel runat="server" ID="rowPnl" RenderId="false">
            <portal:BasePanel ID="pnlLeft" runat="server" CssClass="col-sm-4" RenderId="false">
                <mp:mojoGridView runat="server"
                    ID="grdContactFormMessage"
                    AllowPaging="false"
                    AllowSorting="false"
                    CssClass=""
                    TableCssClass="jqtable"
                    AutoGenerateColumns="false"
                    DataKeyNames="RowGuid">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Eval("Url") %>
                                <br />
                                <a href='mailto:<%# Eval("Email") %>'>
                                    <%# Eval("Email") %>
								</a>
                                <br />
                                <%# Eval("Subject") %>
                                <br />
                                <%# FormatDate(Convert.ToDateTime(Eval("CreatedUtc")))%>
                                <br />
                                <asp:Button runat="server"
                                    ID="btnView"
                                    Text='<%# Resources.ContactFormResources.ContactFormViewButton %>'
                                    CommandArgument='<%# Eval("RowGuid") %>'
                                    CommandName="view"
                                    OnClientClick='<%# GetViewOnClick(Eval("RowGuid").ToString()) %>' />
                                <asp:Button runat="server"
                                    ID="btnSendMail"
                                    Text='Gửi mail'
                                    CommandArgument='<%# Eval("RowGuid") %>'
                                    CommandName="mail" />
                                <asp:Button runat="server"
                                    ID="btnDelete"
                                    Text='<%# Resources.ContactFormResources.ContactFormDeleteButton %>'
                                    CommandArgument='<%# Eval("RowGuid") %>'
                                    CommandName="remove"
                                    OnClientClick='<%# GetDeleteOnClick(Eval("RowGuid").ToString()) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </mp:mojoGridView>

                <portal:mojoCutePager ID="pgrContactFormMessage" runat="server" />

 
            </portal:BasePanel>

            <portal:BasePanel ID="pnlCenter" runat="server" CssClass="col-sm-8" RenderId="false">
                <asp:Literal ID="litMessage" runat="server" />
                <portal:BasePanel ID="pnlMessage" runat="server" CssClass="contactmessage"></portal:BasePanel>
                <asp:UpdatePanel ID="pnlPanel" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSendMail" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="pnlSendMail" runat="server" Visible="false">
                            <h3 class="h3-search"></h3>
                            <div class="col-sm-12" style="border: 1px solid #eee">
                                <div class="form-group ">
                                    <label class="col-sm-4">Gửi đến email: </label>
                                    <asp:TextBox ID="txtToMail" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqToMail" ControlToValidate="txtToMail" runat="server" Text="Vui lòng nhập địa chỉ email"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rgMail" runat="server" ValidationGroup="sendMail" ControlToValidate="txtToMail" Text="Địa chỉ email không hợp lệ" ValidationExpression="^\S+@\S+$"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group ">
                                    <label class="col-sm-4">Tiêu đề: </label>
                                    <asp:TextBox ID="txtTieuDe" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="sendMail" ControlToValidate="txtTieuDe" runat="server" Text="Vui lòng nhập tiêu đề"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Nội dung: </label>
                                    <mpe:EditorControl ID="edMessage" runat="server"></mpe:EditorControl>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnSendMail" runat="server" Text="Gửi mail" ValidationGroup="sendMail" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlSendMailSuccess" runat="server" Visible="false">
                            <p>
                                <span class="alert alert-success">Gửi mail đến người liên hệ thành công</span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="pnlSendMailError" runat="server" Visible="false">
                            <p>
                                <span class="alert alert-danger">Gửi mail đến người liên hệ thất bại</span>
                            </p>
                        </asp:Panel>
                    </ContentTemplate>

                </asp:UpdatePanel>

            </portal:BasePanel>
        </portal:BasePanel>
    </portal:BasePanel>
</asp:Content>

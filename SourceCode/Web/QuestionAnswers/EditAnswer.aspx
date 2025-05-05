<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditAnswer.aspx.cs" Inherits="QuestionAnswerFeatures.UI.EditAnswer" %>

<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:ModuleTitleControl EditText="Add" EditUrl="~/menu/editpost.aspx" Visible="false" ToolTip="Trả lời hỏi đáp" runat="server" ID="TitleControl" />
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">


                    <div class="questionDetail">
                        <h3>
                            <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                        </h3>
                        <div class="questionDetail-info">
                            <asp:Label ID="lblSenderLabel" runat="server"></asp:Label>
                            <asp:Label ID="lblSender" Font-Bold="true" runat="server"></asp:Label>
                            &nbsp; &#124 &nbsp;
        <asp:Label ID="lblTimeSendLabel" runat="server"></asp:Label>
                            <asp:Label ID="lblTimeSend" runat="server"></asp:Label>
                        </div>
                        <div class="questionDetail-view">
                            <asp:Label ID="lblTrong" runat="server" Text="Trong: "></asp:Label>
                            <asp:HyperLink ID="hplCategory" runat="server"></asp:HyperLink>
                            <asp:HyperLink ID="hplChildCategory" runat="server"></asp:HyperLink>
                            &nbsp; &#124 &nbsp;
        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            &nbsp; &#124 &nbsp;
        <asp:Label ID="lblView" runat="server"></asp:Label>
                        </div>
                        <div class="questionDetail-content">
                            <div class="questionDetail-contentTitle">
                                <asp:Label ID="lblDetailContent" Font-Bold="true" runat="server"></asp:Label>
                            </div>
                            <asp:Literal ID="literContent" runat="server"></asp:Literal>
                        </div>
                        <div class="questionAnswerDetail">
                            <div class="questionAnswerDetail-info">
                                <div class="questionAnswerDetail-info-left">
                                    <asp:Label ID="lblAnswerSender" runat="server" Text="Tên người gửi(*)"></asp:Label>
                                    <asp:TextBox ID="txtAnswerSender" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                                <div class="questionAnswerDetail-info-right">
                                    <asp:Label ID="lblAnswerEmail" runat="server" Text="Email(*)"></asp:Label>
                                    <asp:TextBox ID="txtAnswerEmail" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                                <div class="questionAnswerDetail-info-content">
                                    <mpe:EditorControl ID="editAnswer" runat="server"></mpe:EditorControl>
                                </div>
                                <div style="width: 100%; float: left; clear: both; margin-top: 20px">
                                    <portal:mojoButton ID="btnSendAnswer" ClientIDMode="Static" runat="server" Text="Gửi" />
                                    <portal:mojoButton ID="btnApprove" ClientIDMode="Static" runat="server" Text="Phê duyệt" />
                                    <portal:mojoButton ID="btnDelete" ClientIDMode="Static" runat="server" Text="Xóa" />
                                    <portal:mojoButton ID="btnComeBack" runat="server" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <script type="text/javascript">
    $(document).ready(function () {
        $("#btnSendAnswer").click(function () {
            if ($("#txtAnswerSender").val() == null || $("#txtAnswerSender").val() == "") {
                alert("Bạn cần nhập tên liên hệ");
                $("#txtAnswerSender").focus();
                return false;
            }
            if ($("#txtAnswerEmail").val() == null || $("#txtAnswerEmail").val() == "") {
                alert("Bạn cần nhập email liên hệ");
                $("#txtAnswerEmail").focus();
                return false;
            }
            if (!validateEmail($("#txtAnswerEmail").val())) {
                alert("Email liên hệ không đúng!");
                return false;
            }
        });
    });
    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }
    function displayReplyForm() {
        if ($(".questionAnswerDetail").hasClass("answerActive")) {
            $(".questionAnswerDetail").removeClass("answerActive");
            $(".questionAnswerDetail").fadeOut();
        } else {
            $(".questionAnswerDetail").addClass("answerActive");
            $(".questionAnswerDetail").fadeIn();
        }
    }
                    </script>


                </portal:mojoPanel>
                <div class="cleared">
                </div>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</asp:Content>

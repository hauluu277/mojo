<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="QuestionDetailRecentList.ascx.cs" Inherits="QuestionAnswerFeatures.UI.QuestionDetailRecentList" %>


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
    <div class="questionDetailButton">
        <h2>
            <a id="reply" href="javascript:void(0)" onclick="displayReplyForm()" ref="nofollow">
                <img src="../../Data/skins/framework/images/traloi.png" alt="Trả lời câu hỏi"></a>
        </h2>
        <h2>
            <asp:HyperLink ID="hplPostQA" runat="server">
                <img src="../../Data/skins/framework/images/datcauhoi.png" alt="Đặt câu hỏi"></a>
            </asp:HyperLink>
        </h2>
    </div>
    <div class="questionAnswerDetail" style="display: none">
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

            <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <div style="float: left; clear: both">
                        <asp:Label ID="lblAnswerNull" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <div class="questionAnswerDetail-captcha">
                        <mp:CaptchaControl ID="captcha" runat="server"></mp:CaptchaControl>
                        <br />
                        <asp:Label ID="lblCaptchaError" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="width: 100%; float: left; clear: both">
                <portal:mojoButton ID="btnSendAnswer" ClientIDMode="Static" runat="server" Text="Gửi" />
            </div>

        </div>
    </div>
</div>
<div class="ListAnswer">
    <asp:Panel ID="pnlListAnswer" runat="server" Visible="false">
        <h2>
            <label id="lblTotalAnswer" runat="server"></label>
        </h2>
        <asp:Repeater ID="rptAnswer" runat="server">
            <ItemTemplate>
                <div class="Answer">
                    <div class="AnswerAvata">
                        <img src="../..//Data/SiteImages/user-48.png" width="48" height="48" />
                    </div>
                    <div class="AnswerItem">
                        <div class="AnswerInfo">
                            <span><%#Eval("Name") %></span>
                        </div>
                        <div class="AnswerContent">
                            <%#Eval("AnswerName") %>
                        </div>
                        <div class="AnswerTime">
                            <span>
                                <%#string.Format("{0:dd/MM/yyyy HH:mm}",Eval("CreateDate")) %>
                            </span>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlQuestion" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrQuestion" runat="server" />
        </asp:Panel>
    </asp:Panel>
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
        if ($(".questionAnswerDetail").hasClass("answerActive")) {
            $(".questionAnswerDetail").addClass("answerActive");
            $(".questionAnswerDetail").css("display", "block");
        }
    });
    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }
    function displayReplyForm() {
        $(".questionAnswerDetail").slideToggle();
    }
</script>

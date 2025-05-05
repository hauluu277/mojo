<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostQuestion.ascx.cs" Inherits="QuestionAnswerFeatures.UI.PostQuestion" %>
<%@ Import Namespace="mojoPortal.Features" %>

<div class="postquestion">
    <div class="createQA">
        <h1>
            <mp:SiteLabel ID="SiteLabel1" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="LabelHeaderTitle" />
        </h1>
        <%--Tiêu đề thông tin cá nhân--%>
        <div class="QAtitle">
            <mp:SiteLabel ID="SiteLabel2" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="LabelHeaderPersional" />
        </div>
        <%--Hết tiêu đề thông tin cá nhân--%>

        <%--Tên cá nhân--%>
        <div class="QAlleft">
            <mp:SiteLabel ID="lblCreatedName" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="CreatedName" ForControl="txtCreatedName" />
        </div>

        <div class="QLright">
            <asp:TextBox ID="txtCreatedName" runat="server" MaxLength="350" />
            <div class="QAerror">
                <asp:RequiredFieldValidator ID="rfvCreatedName" runat="server" ControlToValidate="txtCreatedName" ValidationGroup="pageSwirlingQuestion" />
            </div>
        </div>
        <%--Hết tên cá nhân--%>

        <%--Email cá nhân--%>
        <div class="QAlleft">
            <mp:SiteLabel ID="lblCreatedEmail" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="CreatedEmail" ForControl="txtCreatedEmail" />
        </div>
        <div class="QLright">
            <asp:TextBox ID="txtCreatedEmail" runat="server" MaxLength="350" />
            <div class="QAerror">
                <asp:RequiredFieldValidator ID="rfvCreatedEmail" runat="server" ControlToValidate="txtCreatedEmail" ValidationGroup="pageSwirlingQuestion" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtCreatedEmail" ValidationExpression="^([a-zA-Z0-9_\-\.]+)*@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ValidationGroup="pageSwirlingQuestion" Font-Bold="False" Font-Italic="False" Font-Size="Small"></asp:RegularExpressionValidator>

            </div>
        </div>
        <%--Hết email cá nhân--%>

        <%--Số điện thoại--%>
        <div class="QAlleft">
            <mp:SiteLabel ID="lblCreatedPhone" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="CreatedPhone" ForControl="txtCreatedPhone" />
        </div>
        <div class="QLright">
            <asp:TextBox ID="txtCreatedPhone" runat="server" MaxLength="50" />
            <div class="QAerror">
                <asp:RegularExpressionValidator ID="rgePhone" runat="server" ControlToValidate="txtCreatedPhone" ValidationExpression="^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$" ValidationGroup="pageSwirlingQuestion" Font-Bold="False" Font-Italic="False" Font-Size="Small"></asp:RegularExpressionValidator>
            </div>
        </div>
        <%--Hết số điện thoại--%>

        <%--Nội dung câu hỏi--%>
        <div class="QAtitle">
            <mp:SiteLabel ID="SiteLabel3" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="LabelHeaderContentQA" />
        </div>
        <%--Hết nội dung câu hỏi--%>

        <%--Tiêu đề câu hỏi--%>
        <div class="QAlleft">
            <mp:SiteLabel ID="lblTitle" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="TitleHeaderLabel" ForControl="txtTitle" />
        </div>
        <div class="QLright">
            <asp:TextBox ID="txtTitle" runat="server" MaxLength="350" />
            <div class="QAerror">
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                    ValidationGroup="pageSwirlingQuestion" />
            </div>
        </div>
        <%--Hết tiêu đề câu hỏi--%>
        <asp:UpdatePanel ID="pnlCategory" runat="server"  Class="fullscreen">
            <ContentTemplate>
                <%--Lĩnh vực hỏi đáp--%>
                <div class="QAlleft">
                    <mp:SiteLabel ID="lblCat" runat="server" ForControl="ddlCategories" ConfigKey="SwirlingQuestionCategoryLabel" ResourceFile="SwirlingQuestionResource"></mp:SiteLabel>
                </div>
                <div class="QLright">

                    <asp:DropDownList ID="ddlCategories" AutoPostBack="true" runat="server"></asp:DropDownList>
                    <div class="QAerror">
                        <asp:RequiredFieldValidator ID="rfvCategories" runat="server" ControlToValidate="ddlCategories"
                            ValidationGroup="pageSwirlingQuestion" InitialValue="0" />
                    </div>
                </div>
                <%--Hết lĩnh vực hỏi đáp--%>

                <%--Loại lĩnh vực--%>
                <asp:Panel ID="pnlTypeCategory" runat="server" Visible="false">
                    <div class="QAlleft">
                        <mp:SiteLabel ID="SiteLabel6" runat="server" ForControl="ddlTypeCategory" ConfigKey="TypeCategoryLabel" ResourceFile="SwirlingQuestionResource"></mp:SiteLabel>
                    </div>
                    <div class="QLright">

                        <asp:DropDownList ID="ddlTypeCategory" runat="server"></asp:DropDownList>
                        <div class="QAerror">
                            <asp:RequiredFieldValidator ID="rfvTypeCategory" runat="server" ControlToValidate="ddlCategories"
                                ValidationGroup="pageSwirlingQuestion" InitialValue="0" />
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--Hết loại lĩnh vực--%>
        <%--Nội dung câu hỏi--%>
        <div class="QAlleft">
            <mp:SiteLabel ID="lblQuestion" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="ContentLabel" ForControl="edQuestion" />
        </div>
        <div class="QLright">
            <asp:TextBox ID="txtContent" Height="60" TextMode="MultiLine" runat="server"></asp:TextBox>
            <div class="QAerror">
                <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtContent" ValidationGroup="pageSwirlingQuestion" />
            </div>
        </div>
        <%--Hết nội dung câu hỏi--%>

        <%--Captcha mã an toàn--%>
        <div class="QAlleft">
            <mp:SiteLabel ID="SiteLabel4" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="SecurityCodeLabel" ForControl="edQuestion" />
        </div>
        <div class="QLright">
            <div id="divCaptcha" runat="server">
                <mp:CaptchaControl ID="captcha" runat="server" />
            </div>
        </div>
        <%--Hết captcha mã an toàn--%>
        <div class="pnlmessage">
            <asp:UpdatePanel runat="server" Class="fullscreen" ChildrenAsTriggers="false" UpdateMode="Conditional"
                ID="updatePanelMessage">
                <ContentTemplate>
                    <div class="QAlleft">
                        &nbsp;
                    </div>
                    <div class="QLright">
                        <portal:mojoLabel ID="lblError" runat="server" CssClass="txterror" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
     </div>
    <div class="buttonCenter">
     
            <portal:mojoButton ID="btnSend" runat="server" ValidationGroup="pageSwirlingQuestion" CausesValidation="false" />
</div>

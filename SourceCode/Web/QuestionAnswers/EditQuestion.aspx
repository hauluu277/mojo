<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditQuestion.aspx.cs" Inherits="QuestionAnswerFeatures.UI.EditQuestion" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

                    <div class="postquestion">
                        <div class="createQA">
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
                                    <asp:RequiredFieldValidator ID="rfvCreatedName" runat="server" ControlToValidate="txtCreatedName" ValidationGroup="validateQuestion" />
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
                                    <asp:RequiredFieldValidator ID="rfvCreatedEmail" runat="server" ControlToValidate="txtCreatedEmail" ValidationGroup="validateQuestion" />
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtCreatedEmail" ValidationExpression="^([a-zA-Z0-9_\-\.]+)*@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ValidationGroup="validateQuestion" Font-Bold="False" Font-Italic="False" Font-Size="Small"></asp:RegularExpressionValidator>

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
                                    <asp:RegularExpressionValidator ID="rgePhone" runat="server" ControlToValidate="txtCreatedPhone" ValidationExpression="^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$" ValidationGroup="validateQuestion" Font-Bold="False" Font-Italic="False" Font-Size="Small"></asp:RegularExpressionValidator>
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
                                        ValidationGroup="validateQuestion" />
                                </div>
                            </div>
                            <%--Hết tiêu đề câu hỏi--%>
                            <asp:UpdatePanel ID="pnlCategory" runat="server">
                                <ContentTemplate>
                                    <%--Lĩnh vực hỏi đáp--%>
                                    <div class="QAlleft">
                                        <mp:SiteLabel ID="lblCat" runat="server" ForControl="ddlCategories" ConfigKey="SwirlingQuestionCategoryLabel" ResourceFile="SwirlingQuestionResource"></mp:SiteLabel>
                                    </div>
                                    <div class="QLright">

                                        <asp:DropDownList ID="ddlCategories" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        <div class="QAerror">
                                            <asp:RequiredFieldValidator ID="rfvCategories" runat="server" ControlToValidate="ddlCategories"
                                                ValidationGroup="validateQuestion" />
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
                                                    ValidationGroup="validateQuestion" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--Hết loại lĩnh vực--%>
                            <%-- phân công phòng ban trả lời câu hỏi --%>
                            <div class="form-group">
                                <div class="QAlleft">
                                    <label>Phân công phòng/ban</label>
                                </div>
                                <div class="QLright">
                                    <asp:DropDownList CssClass="form-control" ID="ddlDepartment" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <%--Nội dung câu hỏi--%>
                            <div class="QAlleft">
                                <mp:SiteLabel ID="lblQuestion" runat="server" ResourceFile="SwirlingQuestionResource" ConfigKey="ContentLabel" ForControl="edQuestion" />
                            </div>
                            <div class="QLright">
                                <asp:TextBox ID="txtContent" Height="60" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <div class="QAerror">
                                    <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtContent" ValidationGroup="validateQuestion" />
                                </div>
                            </div>
                            <%--Hết nội dung câu hỏi--%>
                        </div>
                        <div class="settingrow text-center">
                            <portal:mojoButton ID="btnSend" runat="server" CausesValidation="false" ValidationGroup="validateQuestion" />
                            <portal:mojoButton ID="btnApprove" runat="server" CausesValidation="false" />
                            <portal:mojoButton ID="btnDelete" runat="server" CausesValidation="false" />
                            <portal:mojoButton ID="btnComeBack" runat="server" CausesValidation="false" />
                        </div>
                        <div class="pnlmessage">
                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional"
                                ID="updatePanelMessage">
                                <ContentTemplate>
                                    <div class="QAlleft">
                                    </div>
                                    <div class="QLright">
                                        <portal:mojoLabel ID="lblError" runat="server" CssClass="txterror" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

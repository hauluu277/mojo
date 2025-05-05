<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="EditPost.aspx.cs" Inherits="QuestionAnswerFeatures.UI.EditPost" %>

<asp:content contentplaceholderid="leftContent" id="MPLeftPane" runat="server" />
<asp:content contentplaceholderid="mainContent" id="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
         <style type="text/css">
             .height20 {
                 height: 20px;
             }

             .QAlleft {
                 float: left;
                 width: 25%;
                 text-align: left;
             }

             .QLright {
                 float: left;
                 width: 75%;
             }

             .QAerror {
                 width: 100%;
                 float: left;
                 color: red;
                 clear: both;
                 height: 30px;
             }

             .createQA {
                 width: 98%;
                 float: left;
             }

             .QLright-left {
                 width: 50%;
                 float: left;
             }

             .QLright-right {
                 width: 50%;
                 float: left;
             }

             .QAtitle {
                 width: 100%;
                 float: left;
                 line-height: 25px;
                 background: #cfe5f7;
                 color: #000;
                 border-radius: 4px 4px 0 0;
                 margin: 5px 0 10px;
                 padding: 0 10px;
                 font-weight: bold;
             }

             .createQA h1 {
                 color: #055699;
                 text-transform: uppercase;
                 text-align: center;
                 font-size: 14px;
                 margin: 5px 0 15px;
             }

             .createQA input[type=text], select, textarea {
                 border: 1px solid #ccc;
                 border-radius: 2px;
                 padding: 2px 10px;
                 width: 95%;
             }

             .captcha span {
                 display: none !important;
             }
         </style>
            <div class="modulecontent">
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
                                        ValidationGroup="pageSwirlingQuestion" />
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
                                            ValidationGroup="pageSwirlingQuestion" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--Hết loại lĩnh vực--%>

                    <asp:UpdatePanel ID="pnlArea" runat="server">
                        <ContentTemplate>
                            <%--Khu vực mà bạn quan tâm - Tỉnh/TP và Quận/Huyện--%>
                            <div class="QAlleft">
                                <mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="ddlCity" ConfigKey="AreaLabel" ResourceFile="SwirlingQuestionResource"></mp:SiteLabel>
                            </div>
                            <div class="QLright">
                                <div class="QLright-left">
                                    <asp:DropDownList ID="ddlCity" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    <div class="QAerror">
                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                            ValidationGroup="pageSwirlingQuestion" />
                                    </div>
                                </div>
                                <div class="QLright-right">
                                    <asp:DropDownList ID="ddlDistrict" runat="server"></asp:DropDownList>
                                    <div class="QAerror">
                                    </div>
                                </div>
                            </div>
                            <%--Hết khu vực mà bạn quan tâm - Tỉnh/TP và Quận/Huyện--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>

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
                </div>
            </div>
            <div class="settingrow">
                <portal:mojoButton ID="btnSend" runat="server" ValidationGroup="pageSwirlingQuestion" CausesValidation="false" />
                <portal:mojoButton ID="btnApprove" runat="server" CausesValidation="false" />
                    <portal:mojoButton ID="btnDelete" runat="server" CausesValidation="false" />
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

                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
</asp:content>
<asp:content contentplaceholderid="rightContent" id="MPRightPane" runat="server" />
<asp:content contentplaceholderid="pageEditContent" id="MPPageEdit" runat="server" />

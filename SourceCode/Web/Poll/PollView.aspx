<%@ Page ValidateRequest="false" Language="c#"
    CodeBehind="PollView.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="PollFeature.UI.PollView" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper poll">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <asp:Panel ID="pnlPoll" runat="server">
                        <div class="pollwrap">
                            <div class="settingrow">
                                <mp:SiteLabel ID="lblQuestion" runat="server" ForControl="txtQuestion" CssClass="settinglabel"
                                    ConfigKey="PollEditQuestionLabel" ResourceFile="PollResources"></mp:SiteLabel>
                                <asp:TextBox ID="txtQuestion" runat="server" Enabled="false" Width="640" TextMode="MultiLine" CssClass="forminput verywidetextbox" MaxLength="255"></asp:TextBox>
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="lblAnonymousVoting" runat="server" ForControl="chkAnonymousVoting"
                                    CssClass="settinglabel" ConfigKey="PollEditAnonymousVotingLabel" ResourceFile="PollResources"></mp:SiteLabel>
                                <asp:CheckBox ID="chkAnonymousVoting" runat="server" Enabled="false"></asp:CheckBox>
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="lblAllowViewingResultsBeforeVoting" runat="server" ForControl="chkAllowViewingResultsBeforeVoting"
                                    CssClass="settinglabel" ConfigKey="PollEditAllowViewingResultsBeforeVotingLabel"
                                    ResourceFile="PollResources"></mp:SiteLabel>
                                <asp:CheckBox ID="chkAllowViewingResultsBeforeVoting" runat="server" Enabled="false"></asp:CheckBox>
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="lblShowOrderNumbers" runat="server" ForControl="chkShowOrderNumbers"
                                    CssClass="settinglabel" ConfigKey="PollEditShowOrderNumbersLabel" ResourceFile="PollResources"></mp:SiteLabel>
                                <asp:CheckBox ID="chkShowOrderNumbers" runat="server" Enabled="false"></asp:CheckBox>
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="lblShowResultsWhenDeactivated" runat="server" ForControl="chkShowResultsWhenDeactivated"
                                    CssClass="settinglabel" ConfigKey="PollEditShowResultsWhenDeactivatedLabel" ResourceFile="PollResources"></mp:SiteLabel>
                                <asp:CheckBox ID="chkShowResultsWhenDeactivated" runat="server" Enabled="false"></asp:CheckBox>
                            </div>

                            <div class="settingrow">
                                <mp:SiteLabel ID="lblPollAddOptions" runat="server" ForControl="tblOptions" CssClass="settinglabel"
                                    ConfigKey="PollEditOptionsLabel" ResourceFile="PollResources"></mp:SiteLabel>
                                <table id="tblOptions" cellpadding="0" cellspacing="0" border="0">
                                    <tr valign="top">
                                        <td>
                                            <asp:ListBox ID="lbOptions" SkinID="PageTree" DataTextField="Answer" DataValueField="OptionGuid"
                                                Enabled="false"
                                                Rows="10" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />

                            <div class="settingrow">
                                <mp:SiteLabel ID="lblActiveFromTo" runat="server" CssClass="settinglabel" ConfigKey="PollEditActiveFromToLabel"
                                    ResourceFile="PollResources"></mp:SiteLabel>
                                <asp:Label ID="lblTimeAcctive" runat="server"></asp:Label>
                                <asp:Label ID="lblToTime" runat="server"></asp:Label>
                                <asp:Label ID="lblActiveTo" runat="server"></asp:Label>
                                &nbsp;&nbsp
                                <asp:Label ID="lblTimeError" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                            </div>


                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="CboxPublish"
                                CssClass="settinglabel" ConfigKey="PollPublishTitle" ResourceFile="PollResources"></mp:SiteLabel>
                            <asp:CheckBox ID="CboxPublish" Checked="true" runat="server" Enabled="false"></asp:CheckBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="CboxApprove"
                                CssClass="settinglabel" ConfigKey="PollApproveTitle" ResourceFile="PollResources"></mp:SiteLabel>
                            <asp:CheckBox ID="CboxApprove" Checked="true" runat="server" Enabled="false"></asp:CheckBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel3" runat="server" ForControl="CboxApprove"
                                CssClass="settinglabel" ConfigKey="CommentsOfLeadersLabel" ResourceFile="PollResources"></mp:SiteLabel>
                            <asp:TextBox ID="textComment" TextMode="MultiLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlComent" runat="server" Visible="false">
                        <div style="width: 100%; height: 35px;"></div>
                        <%-- <div style="width: 50%; height: 20px; border-bottom: 1px solid rgba(0, 0, 0, 0.55);"></div>--%>
                        <% if (CheckCount())
                           { %>
                        <span style="width: 100%; height: 15px; border-bottom: 1px solid rgba(0, 0, 0, 0.55); font-weight: bold; text-align: left">
                            <asp:Label ID="lblListComment" runat="server"></asp:Label>
                        </span>
                        <%} %>
                        <table>
                            <asp:Repeater ID="rptOpinion" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <mp:SiteLabel ID="lblSiteComment" runat="server" ConfigKey="PollCommentBy" ResourceFile="PollResources" />
                                        </td>
                                        <td>
                                            <%#Eval("NameUser") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <mp:SiteLabel ID="lblContent" runat="server" ConfigKey="PollContent" ResourceFile="PollResources" />
                                        </td>
                                        <td>
                                            <%#Eval("Opinion") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="width: 100%; height: 10px; border-bottom: 1px solid rgba(0, 0, 0, 0.55)"></div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div style="height: 25px;"></div>
                        <span style="border-bottom: 1px solid rgba(0, 0, 0, 0.55)" />
                        <table>
                            <tr>
                                <td>
                                    <%#Resources.PollResources.CommentsOfLeadersLabel %>
                                    <asp:Label ID="lblComment" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtComment" runat="server" Width="640" Height="110" Visible="false" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <portal:mojoButton ID="btnSaveComment" Visible="false" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
    <portal:SessionKeepAliveControl ID="ka1" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

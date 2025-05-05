<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DialogQuestionModule.ascx.cs" Inherits="QuestionAnswerFeature.UI.DialogQuestionModule" %>
<%@ Import Namespace="QuestionAnswersFeatures.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Document">
        <portal:ModuleTitleControl runat="server" ID="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <div class="dialogquestion">
                    <div class="HomeArticleHot-title">
                        <span>
                            <asp:HyperLink ID="hplLink" runat="server"></asp:HyperLink>
                        </span>
                    </div>
                    <div class="dialogContent">
                        <div class="dialog-listquestion">
                            <ul>
                                <asp:Repeater ID="rptQuestion" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a href='<%#QuestionAnswerUtils.FormatDetailQuestionUrl(SiteRoot,int.Parse(Eval("PageID").ToString()), Eval("ItemUrl").ToString(),int.Parse(Eval("ItemID").ToString()),false,string.Empty) %>' title='<%#Eval("Question") %>'><%#Eval("Question") %></a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

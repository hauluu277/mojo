<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="QuestionAnswerUser.ascx.cs" Inherits="QuestionAnswersFeature.UI.QuestionAnswerUser" %>
<%@ Import Namespace="QuestionAnswersFeatures.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
  

    table {
        border-collapse: collapse;
    }
</style>

<div id="QAuser">
    <div class="QAuser-title">
        Quản lý câu hỏi, trả lời
    </div>

    <div class="QAuser_header">
        <ul>
            <li>
                <asp:Button ID="btnQuestion" Text="Câu hỏi" runat="server" />
            </li>
            <li>
                <asp:Button ID="btnAnswer" Text="Câu trả lời" runat="server" />
            </li>
        </ul>
    </div>
    <br>
    <asp:Panel ID="pnlShowQuestion" runat="server" CssClass="QAquestion">
        <asp:Repeater ID="rptQuestion" runat="server" SkinID="Blog">
            <HeaderTemplate>
                <table class="myTable" style="width: 100%; float: left; margin-top: 10px;">
                    <thead>
                        <tr>
                            <th style="width: 25%">Câu hỏi
                            </th>
                            <th style="width: 35%">Nội dung
                            </th>
                            <th style="width: 10%">Ngày tạo
                            </th>
                            <th style="width: 10%">Lượt xem
                            </th>
                            <th style="width: 10%">Kiểm duyệt</th>
                            <th style="width: 5%"></th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:HyperLink ID="hplDetailQuestion" runat="server" ToolTip='<%#Eval("Question") %>' NavigateUrl='<%#QuestionAnswerUtils.FormatDetailQuestionUrl(SiteRoot,Eval("ItemUrl").ToString(),int.Parse(Eval("ItemID").ToString()),false,string.Empty) %>'><%#Eval("Question")%> (<%#Eval("TotalAnswerApproved")%>)</asp:HyperLink>
                    </td>
                    <td>
                        <%#Eval("ContentQuestion") %>
                    </td>
                    <td>
                        <%# string.Format("{0:dd/MM/yyyy}",Eval("CreateDate")) %>
                    </td>
                    <td style="text-align: center">
                        <%#Eval("Views") %>
                    </td>
                    <td style="text-align: center">
                        <img src='<%# mojoPortal.Features.QuestionAnswerUtils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsApprove").ToString())) %>' alt="<%# StateLink %>" />
                    </td>
                    <td>

                        <asp:ImageButton ID="ibtnEditQA" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                            CommandName="EditItemQA" CommandArgument='<%# Eval("ItemID") %>'
                            CausesValidation="false" />

                        <asp:ImageButton ID="ibtnDeleteQA" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                            CommandName="DeleteItemQA" CommandArgument='<%# Eval("ItemID") %>'
                            CausesValidation="false" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlQuestion" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrQuestion" runat="server" />
        </asp:Panel>
        <br />
        <asp:Label ID="lblQuestionNull" runat="server" Visible="false" ForeColor="Red" Text="Không tìm thấy câu hỏi nào"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlShowAnswer" runat="server" class="QAanswer">
        <asp:Repeater ID="rptAnswer" runat="server" SkinID="Blog">
            <HeaderTemplate>
                <table class="myTable" style="width: 100%; float: left; margin-top: 10px;">
                    <tbody>
                        <tr class="tit-tbl bg_tit">
                            <th style="width: 35%">Nội dung
                            </th>
                            <th style="width: 25%">Câu hỏi
                            </th>
                            <th style="width: 15%">Ngày tạo
                            </th>
                            <th style="width: 10%">Kiểm duyệt</th>
                            <th style="width: 5%"></th>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("AnswerName") %>
                    </td>
                    <td>
                        <asp:HyperLink ID="hplDetailQuestion" runat="server" ToolTip='<%#Eval("QuestionName") %>' NavigateUrl='<%#QuestionAnswerUtils.FormatDetailQuestionUrl(SiteRoot,Eval("QuestionUrl").ToString(),int.Parse(Eval("QuestionID").ToString()),false,string.Empty) %>'><%#Eval("QuestionName")%> (<%#Eval("TotalAnswerApproved")%>)</asp:HyperLink>
                    </td>
                    <td>
                        <%# string.Format("{0:dd/MM/yyyy}",Eval("CreateDate")) %>
                    </td>
                    <td style="text-align: center">
                        <img src='<%# mojoPortal.Features.QuestionAnswerUtils.ImageApprove(bool.Parse(DataBinder.Eval(Container.DataItem,"IsApprove").ToString())) %>' alt="<%# StateLink %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtnEditAnswer" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                            CommandName="EditItemAnswer" CommandArgument='<%# Eval("ItemID") %>'
                            CausesValidation="false" />

                        <asp:ImageButton ID="ibtnDeleteAnswer" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                            CommandName="DeleteItemAnswer" CommandArgument='<%# Eval("ItemID") %>'
                            CausesValidation="false" />

                    </td>
                </tr>

                </tbody>
        </table>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Panel ID="pnlAnswer" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrAnswer" runat="server" />
        </asp:Panel>
        <br />
        <asp:Label ID="lblAnswer" runat="server" Visible="false" ForeColor="Red" Text="Không tìm thấy câu trả lời nào"></asp:Label>
    </asp:Panel>
    <div class="clear">
    </div>
</div>

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CommentsWidget.ascx.cs" Inherits="mojoPortal.Web.UI.CommentsWidget" %>
<portal:CommentSystemDisplaySettings ID="displaySettings" runat="server" />
<portal:CommentsOuterPanel ID="pnlOuterPanel" runat="server" RenderId="false">
    <portal:CommentEditor ID="commentEditor" runat="server" />
    <portal:HeadingControl ID="commentListHeading" runat="server" SkinID="Comments" CssClass="comment-heading" HeadingTag="h3" RenderId="false" />
    <portal:CommentsInnerPanel ID="pnlInnerPanel" runat="server" RenderId="false">
        <asp:UpdatePanel ID="pnlReloadComment" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Repeater ID="rptComments" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <portal:CommentItemWrapper ID="pnlItem" runat="server" RenderId="false" CssClass='<%# ItemWrapperCssClass + " modstatus-" + Eval("ModerationStatus").ToString() %>' Visible='<%# UserCanModerate || (Convert.ToInt32(Eval("ModerationStatus")) == Comment.ModerationApproved)   %>'>
                            <portal:CommentItemLeftPanel ID="pnlLeft" runat="server" RenderId="false" CssClass='<%# LeftPanelCssClass %>'>
                                <div id='post<%# Eval("Guid") %>'>
                                    <NeatHtml:UntrustedContent ID="UntrustedContent1" runat="server" TrustedImageUrlPattern='<%# AllowedImageUrlRegexPatern %>' ClientScriptUrl="~/ClientScript/NeatHtml.js">
                                        <portal:CommentItemBodyPanel ID="pnlBody" runat="server" CssClass='<%# ItemBodyCssClass %>' RenderId="false"><%# Eval("UserComment").ToString()%></portal:CommentItemBodyPanel>
                                    </NeatHtml:UntrustedContent>
                                </div>
                            </portal:CommentItemLeftPanel>
                            <portal:CommentItemRightPanel ID="pnlRight" runat="server" CssClass='<%# RightPanelCssClass %>' RenderId="false">
                                <%--<portal:CommentItemInnerPanel ID="itemheaderpanel" runat="server" CssClass='<%# ItemHeaderCssClass %>' RenderId="false">
                            <portal:CommentDateWrapper ID="dw1" runat="server" RenderId="false" CssClass='<%# DateWrapperCssClass %>'><%# FormatCommentDate(Convert.ToDateTime(Eval("CreatedUtc"))) %></portal:CommentDateWrapper>
                        </portal:CommentItemInnerPanel>--%>
                                <%-- <portal:CommentItemInnerPanel ID="usernamepanel" runat="server" CssClass='<%# UsernameWrapperCssClass %>' RenderId="false">
                            <%# GetProfileManageIcon(Convert.ToInt32(Eval("UserId"))) %>
                            <%# GetProfileLinkOrLabel(Convert.ToInt32(Eval("UserId")), Eval("PostAuthor").ToString(), Eval("PostAuthorWebSiteUrl").ToString())%>
                        </portal:CommentItemInnerPanel>--%>
                                <portal:CommentItemInnerPanel ID="Commentinfo" runat="server" CssClass='<%# CommentinfoCssClass %>' RenderId="false">
                                    <span class='<%# UsernameWrapperCssClass %>'><%# Eval("PostAuthor").ToString() %></span>
                                    <span class='<%# DateWrapperCssClass %>'>- <%# FormatCommentDate(Convert.ToDateTime(Eval("CreatedUtc"))) %></span>
                                </portal:CommentItemInnerPanel>

                                <portal:CommentItemInnerPanel ID="revenuepanel" runat="server" CssClass='<%# RevenueWrapperCssClass %>' RenderId="false" Visible='<%# showUserRevenue %>'>
                                    <mp:SiteLabel ID="SiteLabel1" runat="server" ConfigKey="UserSalesLabel" ResourceFile="ForumResources" UseLabelTag="false" />
                                    <%# string.Format(currencyCulture, "{0:c}", Convert.ToDecimal(Eval("UserRevenue"))) %>
                                </portal:CommentItemInnerPanel>
                                <span class="comment-manage-edit">
                                    <%-- <asp:HyperLink CssClass="commentEdit ceditlink ModuleSettingsLink" Text="<%$ Resources:Resource, EditLink %>"
                                        ID="editLink"
                                        NavigateUrl='<%# EditBaseUrl + "&c=" + Eval("Guid")  %>'
                                        Visible='<%# UserCanEdit(new Guid(Eval("UserGuid").ToString()), Eval("UserEmail").ToString(), Convert.ToInt32(Eval("ModerationStatus")), Convert.ToDateTime(Eval("CreatedUtc"))) %>' runat="server" />--%>
                                    <asp:Button ID="btnEditComment" CssClass="btn btn-xs btn-primary" runat="server" Text="Chỉnh sửa" CommandName="EditComment" CommandArgument='<%#Eval("Guid") %>' Visible='<%# UserCanEdit(new Guid(Eval("UserGuid").ToString()), Eval("UserEmail").ToString(), Convert.ToInt32(Eval("ModerationStatus")), Convert.ToDateTime(Eval("CreatedUtc"))) %>' />
                                </span>
                                <span class="comment-manage-approve">
                                    <portal:mojoButton ID="btnApprove" runat="server" Text='Xuất bản' CommandName="ApproveComment" CommandArgument='<%# Eval("Guid")%>'
                                        Visible='<%# CanApprove(Eval("Guid"))%>' SkinID="ButtonSuccessSmall" />
                                </span>
                                <span class="comment-manage-approve">
                                    <portal:mojoButton ID="btnUnApprove" runat="server" Text='Hủy xuất bản' CommandName="UnApproveComment" CommandArgument='<%# Eval("Guid")%>'
                                        Visible='<%# CanUnApprove(Eval("Guid"))%>' SkinID="DeleteButtonSmall" />
                                </span>
                                <span class="comment-manage-delete">
                                    <portal:mojoButton ID="btnDelete" runat="server" Text='<%$ Resources:Resource, DeleteButton %>' CommandName="DeleteComment" CommandArgument='<%# Eval("Guid")%>'
                                        Visible='<%# UserCanModerate %>' SkinID="DeleteButtonWarning" />
                                </span>

                            </portal:CommentItemRightPanel>
                        </portal:CommentItemWrapper>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="modal fade" id="commentModal" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Chỉnh sửa bình luận</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-2">Họ tên: </label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblHoTen" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-2">Email: </label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-2">Ngày đăng: </label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblCreateDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 form-group">
                                    <label class="col-sm-2">Bình luận</label>
                                    <div class="col-sm-10">
                                        <%--<asp:TextBox ID="txtComment" runat="server" Rows="5" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>--%>
                                        <mpe:EditorControl ID="edComment" runat="server"></mpe:EditorControl>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:HiddenField ID="hdfCommentGuid" runat="server" />
                                <asp:Button ID="btnSavecomment" runat="server" CssClass="btn btn-success" Text="Cập nhật" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                            </div>
                        </div>
                    </div>
                </div>
                <script>
                    function UpdateCommentReponse(result) {
                        $("#commentModal").removeClass("in");
                        $(".modal-backdrop").remove();
                        $("body").removeClass("modal-open");
                        $("#commentModal").modal("hide");
                        if (result) {
                            NotifySuccess("Cập nhật bình luận thành công!");
                        } else {
                            NotifyError("Bình luận không tồn tại!");
                        }
                    }
                    function showCommentModel() {
                        $("#commentModal").modal("show");
                    }
                </script>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSavecomment" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </portal:CommentsInnerPanel>
    <asp:Panel ID="pnlCommentsClosed" runat="server" EnableViewState="false" CssClass="commentsclosed">
        <asp:Literal ID="litCommentsClosed" runat="server" EnableViewState="false" />
    </asp:Panel>
    <asp:Panel ID="pnlCommentsRequireAuthentication" runat="server" Visible="false" EnableViewState="false" CssClass="commentsclosed">
        <portal:SignInOrRegisterPrompt ID="srPrompt" runat="server" />
    </asp:Panel>
</portal:CommentsOuterPanel>
<div id="divCommentService" runat="server" enableviewstate="false" class="blogcommentservice commentservice">
    <portal:IntenseDebateDiscussion ID="intenseDebate" runat="server" EnableViewState="false" />
    <portal:DisqusWidget ID="disqus" runat="server" RenderPoweredBy="false" EnableViewState="false" />
    <portal:FacebookCommentWidget ID="fbComments" runat="server" Visible="false" EnableViewState="false" SkinID="Blog" />
</div>


<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DuThaoView.ascx.cs" Inherits="DuThaoVanBanFeature.UI.DuThaoView" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Import Namespace="DuThaoVanBanFeature.UI" %>

<div class="module">
    <div class="info-list">
        <i class="fa fa-calendar-check-o">&nbsp;</i> Toàn văn
    </div>
    <table class="table">
        <tbody>
            <tr>
                <th style="width: 150px">Tiêu đề</th>
                <td>
                    <asp:Literal ID="litTitle" runat="server" EnableViewState="false"></asp:Literal><asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>" ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl() %>' CssClass="ModuleEditLink" />
                </td>
            </tr>
            <tr>
                <th>Trích yếu</th>
                <td>
                    <asp:Literal ID="litSummary" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>Nội dung</th>
                <td></td>
            </tr>
            <tr>
                <th>Cơ quan ban hành</th>
                <td>
                    <asp:Label ID="lblCoQuanBanHanh" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Loại văn bản</th>
                <td>
                    <asp:Label ID="lblLoaiVanBan" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Lĩnh vực</th>
                <td>
                    <asp:Label ID="lblLinhVuc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Thời gian</th>
                <td>
                    <asp:Label ID="lblTime" runat="server"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>

    <div class="info-list m-top35">
        <i class="fa fa-download">&nbsp;</i> Tài liệu
    </div>
    <div class="list-group laws-download-file">
        <div class="list-group-item">
            <a href="javascript:void(0)" title="Tải tập tin demo.pdf">Tải tập tin : </a>
            <strong>
                <asp:Repeater ID="rptFile" runat="server">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkAttach" CssClass="lnk_others" runat="server" Text='<%#Eval("Name") %>' NavigateUrl='<%# "/"+ConfigurationManager.AppSettings["DraftDocumentFileFolder"]+Eval("FilePath") %>' Target="_blank"></asp:HyperLink>
                        &nbsp;
                    </ItemTemplate>
                </asp:Repeater>
            </strong>
        </div>
    </div>
    <div class="cleared"></div>
    <asp:Panel runat="server" ID="pncomment">
        <div class="info-list m-top35">
            <i class="fa fa-list-alt">&nbsp;</i> Danh sách ý kiến đóng góp
        </div>
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:Repeater ID="rptComment" runat="server">
                    <ItemTemplate>
                        <div class="with100">
                            <div class="blogdate create">
                                <span class="lbl-tit">
                                    <i class="fa fa-user-o"></i>
                                    <asp:Label ID="Literal1" CssClass="" runat="server" Text='<%#CreatedBy %>' EnableViewState="false" Visible="true" />
                                    <asp:Label ID="litAuthor" runat="server" Text='<%#Eval("Name") %>' EnableViewState="false" Visible="true" />
                                </span>
                                <span class="lbl-tit">&nbsp;
                                    <i class="fa fa-clock-o"></i>
                                    <%--<%#CreatByDate %>--%>
                                    <%# FormatBlogDate(Convert.ToDateTime(Eval("DateCreated"))) %>
                                </span>
                            </div>
                            <div class="contentComment">
                                <span class="lbl-content">
                                    <i class="fa fa-tags"></i>&nbsp;Nội dung
                                </span>
                                <span class="panel-comment"><%#Eval("Comment") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlCommentPager" runat="server" CssClass="ArticlePager">
                    <portal:mojoCutePager ID="pgrComment" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
    <div class="cleared"></div>
    <asp:Panel ID="pncmtdraft" runat="server">
        <div class="info-list m-top35">
            <i class="fa fa-envelope">&nbsp;</i>
            <asp:Label ID="lblHeaderForm" runat="server"></asp:Label>
        </div>
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:Panel ID="pnlGopY" runat="server">
                    <div class="cmtdraft">
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblName" runat="server" ForControl="txtName" CssClass="settinglabel" ConfigKey="NameLabel" ResourceFile="DuThaoVanBanResources" />
                            <asp:TextBox ID="txtName" CssClass="settingtextbox widetextbox1" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ValidationGroup="DongGopDuThaoVanBan" InitialValue="" />
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblEmail" runat="server" ForControl="txtEmail" CssClass="settinglabel" ConfigKey="EmailLabel" ResourceFile="DuThaoVanBanResources" />
                            <asp:TextBox ID="txtEmail" CssClass="settingtextbox widetextbox1" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="DongGopDuThaoVanBan" InitialValue="" />
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^([a-zA-Z0-9_\-\.]+)*@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ValidationGroup="DongGopDuThaoVanBan"></asp:RegularExpressionValidator>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblAddress" runat="server" ForControl="txtAddress" CssClass="settinglabel" ConfigKey="AddressLabel" ResourceFile="DuThaoVanBanResources" />
                            <asp:TextBox ID="txtAddress" CssClass="settingtextbox widetextbox1" runat="server" />
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblMobile" runat="server" ForControl="txtMobile" CssClass="settinglabel" ConfigKey="MobileLabel" ResourceFile="DuThaoVanBanResources" />
                            <asp:TextBox ID="txtMobile" CssClass="settingtextbox widetextbox1" runat="server" />
                        </div>
                        <div class="settingrow setingcolum">
                            <mp:SiteLabel ID="lblComment" runat="server" ForControl="mpComments" CssClass="settinglabel" ConfigKey="CommentLabel" ResourceFile="DuThaoVanBanResources" />
                            <div class="settingrow" style="width: 100%; float: left">
                                <mpe:EditorControl ID="mpComments" runat="server"></mpe:EditorControl>
                                <br />
                                <portal:mojoLabel ID="lblCommentErrorMessage" runat="server" CssClass="txterror" />
                            </div>
                        </div>
                        <div class="settingrow" id="divCaptcha" runat="server">
                            <mp:CaptchaControl ID="captcha" runat="server" />
                        </div>
                        <%--<div class="settingrow">
            <mp:SiteLabel id="lblItemUrl" runat="server" ForControl="txtItemUrl" CssClass="settinglabel" ConfigKey="ItemUrlLabel" ResourceFile="DuThaoVanBanResources" />
            <asp:TextBox ID="txtItemUrl" CssClass="verywidetextbox forminput"  runat="server" />
            <span id="spnUrlWarning" runat="server" style="font-weight: normal;" class="txterror"></span>
            <asp:HiddenField ID="hdnTitle" runat="server" />
            <asp:RegularExpressionValidator ID="regexUrl" runat="server" ControlToValidate="txtItemUrl"
                                    ValidationExpression="((~/){1}\S+)" Display="None" ValidationGroup="DuThaoVanBan" />	
        </div>--%>
                        <div class="buttonCenter">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" CausesValidation="false" ValidationGroup="DongGopDuThaoVanBan" />
                        </div>
                    </div>
                    <portal:mojoLabel ID="lblMessageError" runat="server" CssClass="txterror" />
                </asp:Panel>

                <asp:Panel ID="pnlSuccess" runat="server" CssClass="width100 ">
                    <p class="alert alert-success">Cảm ơn bạn đã đóng góp ý kiến!</p>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <div class="cleared"></div>
    <div class="panel panel-green m-top35">
        <div class="panel-heading"><span>Danh sách dự thảo khác</span></div>
        <div class="panel-body">
            <div class="draftOrther">
                <ul class="ulDuThaoOrther">
                    <asp:Repeater ID="rptOrther" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="lnkDetail" runat="server" CssClass="tit-DuThaoOrther" EnableViewState="false" Visible="true" NavigateUrl='<%#DuThaoVanBanUltils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'><%#Eval("Title") %> (<%#string.Format("{0:d/M/yyyy}", Eval("LastModUtc")) %>)</asp:HyperLink></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</div>



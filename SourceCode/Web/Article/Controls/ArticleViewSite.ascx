<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleViewSite.ascx.cs"
    Inherits="ArticleFeature.UI.ArticleViewSite" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<script src="/Data/js/ASPSnippets_Pager.min.js"></script>
<link href="../../Data/js/bootstrap-datepicker-1.6.4-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<script src="../../Data/js/bootstrap-datepicker-1.6.4-dist/js/bootstrap-datepicker.min.js"></script>
<script src="../../Data/js/bootstrap-datepicker-1.6.4-dist/locales/bootstrap-datepicker.vi.min.js"></script>
<script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-5d3959e22f352b9d"></script>
<style type="text/css">
    .left-menu {
        width: 255px;
        float: left;
        border: 1px solid white;
        margin-right: 20px;
        margin-bottom: 30px;
    }

        .left-menu h3 {
            background: #0078d7;
            color: white;
            margin: 0;
            padding: 8px;
            font-size: 16px;
            text-align: center;
            line-height: 25px;
            width: 100%;
            float: left;
            border-bottom: 1px solid white;
        }

            .left-menu h3 a, .left-menu h3 a:hover {
                color: white !important;
            }

        .left-menu ul {
            list-style: none;
        }

            .left-menu ul.article_list {
                list-style: none;
                width: 100%;
                float: left;
            }

                .left-menu ul.article_list li a {
                    color: black;
                }

                .left-menu ul.article_list li {
                    width: 100%;
                    float: left;
                    border-bottom: 1px solid #ddd;
                    font-size: 14px;
                    padding: 10px;
                }

    .content-article {
        width: calc(100% - 275px);
        float: left;
    }

    @media all and (max-width: 480px) and (min-width: 320px) {
        .left-menu {
            width: 100%;
        }

        .content-article {
            width: 100%;
        }
    }

    @media all and (max-width: 768px) and (min-width: 480px) {
        .left-menu {
            display: none;
        }

        .content-article {
            width: 100%;
        }
    }
</style>

<asp:Panel ID="pnlArticle" runat="server" CssClass="panelwrapper articleview">
    <%--<portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true"
        UseLowerCaseArtisteerClasses="true" />--%>
    <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent"
        RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
        <div class="modulecontent">
            <asp:Panel ID="divarticle" runat="server" CssClass="articlecenter-rightnav" SkinID="plain">
                <div class="header-article ">
                    <asp:Panel ID="pnlLeftMenu" runat="server">
                        <ul>
                            <asp:Repeater ID="rptCategory" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <h3>
                                            <a href='/articletraining/viewlist.aspx?catid=<%#Eval("ItemID") %>' title='<%#Eval("Name") %>'><%#Eval("Name") %></a>
                                        </h3>
                                        <ul class="article_list">
                                            <asp:Repeater runat="server" ID="rptArticle" DataSource='<%#LoadArticle(Eval("ItemID")) %>'>
                                                <ItemTemplate>
                                                    <li>
                                                        <a href="<%# string.Format("{0}/Article/ViewDetail.aspx?&itemid={1}",SiteRoot,Eval("ItemID")) %>" title="<%#Eval("Title") %>"><%#Eval("Title") %></a>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlContentArticle">
                        <h2 class="articletitle">
                            <asp:Literal ID="litTitle" runat="server" EnableViewState="false" />&nbsp;
                        <asp:HyperLink ID="lnkEdit" runat="server" EnableViewState="false" CssClass="ModuleEditLink hide"></asp:HyperLink>
                        </h2>
                        <div class="clearfix"></div>
                        <asp:Panel ID="pnlArticleReference" runat="server">
                            <ul class="ulActiveReference">
                                <asp:Repeater ID="rptActiveReference" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot,Eval("ItemUrl").ToString(),int.Parse(Eval("ItemID").ToString()),PageId,ModuleId) %>' title='<%#Eval("Title") %>'><%#Eval("Title") %></a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </asp:Panel>
                        <div class="width100pad">
                            <div class="contentdes-left contentdes-author" style="display: none">
                                <div class="author-post">
                                    <asp:Image ID="imgAuthor" runat="server" />
                                    <span>
                                        <asp:Label ID="lblAuthor" runat="server"></asp:Label></span>
                                </div>

                            </div>
                            <div class="contentdes-right social-right">
                                <span id="pnlWCAG" runat="server" class="wvag">&nbsp; &nbsp;<mp:SiteLabel ID="lblXemVoiCoChu" runat="server" ConfigKey="LabelXemVoiCoChu"  ResourceFile="ArticleResources" />
                                    <i class="fa fa-font" onclick="fontMinus()" title="Xem với cỡ chữ nhỏ hơn" aria-hidden="true" style="font-size: 18px; color: #337ab7; cursor: pointer;"></i>
                                    <i class="fa fa-font fa-2x" aria-hidden="true" title="Xem với cỡ chữ to hơn" onclick="fontPlus()" style="color: #337ab7; cursor: pointer;"></i>
                                </span>
                                <div class="addthis_inline_share_toolbox"></div>

                                <ul class="list-inline text-right">
                                    <li><a class="dimgray" rel="nofollow" title="In ra" href="javascript: void(0)" onclick="return PrintSubject(document.title)"><em class="fa fa-print fa-lg">&nbsp;</em></a></li>
                                </ul>
                                <p>
                                    <span class="post-date-article contentdes">
                                        <img src="/Data/Images/special/post-clock.png" />
                                        <asp:Literal ID="litStartDate" runat="server" EnableViewState="false" />
                                        <span>| <asp:Literal ID="Literal1" runat="server" EnableViewState="false" />
                                        <asp:Label ID="lblHitCountLabel" runat="server" CssClass="texthit" />
                                        </span>
                                    </span>
                                </p>
                            </div>
                        </div>

                        <%--end--%>
                        <div class="clearfix"></div>
                        <asp:Panel ID="pnlImageWrapper" runat="server" class="image-wrapper">
                            <center style="margin-bottom: 10px;">
                        <asp:Image ID="image1" runat="server" />
                            </center>
                        </asp:Panel>

                        <asp:Panel ID="pnlDetails" runat="server">
                            <div class="articletext" id="article_text">
                                <asp:Literal ID="litDescription" runat="server" EnableViewState="false" />
                            </div>
                        </asp:Panel>
                        <%--                       <div class="author-bar" style="display: none">
                            <div class="author-detail">
                            </div>
                        </div>--%>
                        <asp:Panel ID="pnlReadPdf" runat="server">
                            <asp:Repeater ID="rptReadPDF" runat="server">
                                <ItemTemplate>
                                    <p>
                                        <asp:Literal ID="literc" runat="server" Text='<%#ReadPDF(Eval("FilePath"),650)%>'></asp:Literal>
                                    </p>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                        <div class="article-file">
                            <asp:Panel ID="pnlAttachment" runat="server" CssClass="blogattachment">
                                <mp:SiteLabel ID="lblAttachments" runat="server" ForControl="txtCategory" CssClass="label"
                                    ConfigKey="AttachmentsLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <asp:Repeater ID="rptAttachments" runat="server" EnableViewState="false">
                                    <ItemTemplate>
                                        <div class="file-item">
                                            <asp:Image ID="imgType" runat="server" AlternateText=" " ImageUrl='<%# Page.ResolveUrl("~/Data/SiteImages/Icons/unknown.png") %>' />
                                            <a href="<%#Eval("FilePath") %>" download="<%#Eval("FilePath") %>"><%#Eval("FileName") %></a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>
                        <div class="article-tool">
                            <div class="articlehit">
                                <portal:FacebookLikeButton ID="fblike" runat="server" />
                                <portal:TweetThisLink ID="tweetThis1" runat="server" />
                                <asp:Literal ID="ltrCommentCountLabel" runat="server" />
                                <a href="#" class="gotop">
                                    <asp:Image ID="imageGoTop" runat="server" />
                                    <asp:Literal ID="ltrGoTop" runat="server"></asp:Literal>
                                </a>
                            </div>
                        </div>
                        <%--Hiển thị tag nếu chọn config = true--%>
                        <div class="clearfix"></div>
                        <asp:Repeater ID="rptTags" runat="server">
                            <HeaderTemplate>
                                <div class="tags-detail">
                                    <label class="blogtags tagslabel">
                                        <mp:SiteLabel ID="lblcatBottom" runat="server" ConfigKey='TagLabel' ResourceFile="ArticleResources" UseLabelTag="false" ShowWarningOnMissingKey="false" />
                                    </label>
                                    <span class="blogtags">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="Hyperlink5" runat="server" EnableViewState="false"
                                    Text='<%# Eval("Name").ToString() %>'
                                    NavigateUrl='<%# this.SiteRoot + "/article/viewtag.aspx?tag=" + DataBinder.Eval(Container.DataItem,"TagID") + "&pageid=" + PageId.ToString() + "&mid=" + ModuleId.ToString()%>'>
                                </asp:HyperLink>
                            </ItemTemplate>
                            <FooterTemplate></span> </div></FooterTemplate>
                        </asp:Repeater>

                        <%--end--%>
                    </asp:Panel>



                    <div style="clear: both"></div>
                    <portal:CommentsWidget ID="InternalCommentSystem" runat="server" Visible="false" />
                    <asp:Panel ID="pnlFeedback" runat="server" CssClass="bcommentpanel">
                        <portal:HeadingControl ID="commentListHeading" runat="server" SkinID="articlecomments" HeadingTag="h3" />
                        <div class="articlecomments">
                            <asp:Repeater ID="dlComments" runat="server" EnableViewState="true" OnItemCommand="dlComments_ItemCommand">
                                <ItemTemplate>

                                    <<%# CommentItemHeaderElement %> class="blogtitle">
                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="<%# Resources.ArticleResources.DeleteImageAltText %>"
                                        ToolTip="<%# Resources.ArticleResources.DeleteImageAltText %>" ImageUrl='<%# DeleteLinkImage %>'
                                        CommandName="DeleteComment" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ArticleCommentID")%>'
                                        Visible="<%# IsEditable%>" />
                                    <asp:Literal ID="litTitle" runat="server" EnableViewState="false" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem,"Title").ToString()) %>' />
                                </<%# CommentItemHeaderElement %>>
                                    <div>
                                        <asp:Label ID="Label2" Visible="True" runat="server" EnableViewState="false" CssClass="blogdate"
                                            Text='<%# FormatCommentDate(Convert.ToDateTime(Eval("DateCreated"))) %>' />
                                        <asp:Label ID="Label3" runat="server" EnableViewState="false" Visible='<%# (bool) (DataBinder.Eval(Container.DataItem, "URL").ToString().Length == 0) %>'
                                            CssClass="blogcommentposter">
					        <%#  Server.HtmlEncode(DataBinder.Eval(Container.DataItem,"Name").ToString()) %>
                                        </asp:Label>
                                        <NeatHtml:UntrustedContent ID="UntrustedContent2" runat="server" EnableViewState="false"
                                            TrustedImageUrlPattern='<%# RegexRelativeImageUrlPatern %>' ClientScriptUrl="~/ClientScript/NeatHtml.js">
                                            <asp:HyperLink ID="Hyperlink2" runat="server" EnableViewState="false" Visible='<%# (bool) (DataBinder.Eval(Container.DataItem, "URL").ToString().Length != 0) %>'
                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem,"Name").ToString()) %>'
                                                NavigateUrl='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem,"URL").ToString())%>'
                                                CssClass="blogcommentposter">
                                            </asp:HyperLink>
                                        </NeatHtml:UntrustedContent>
                                    </div>
                                    <div class="blogcommenttext">
                                        <NeatHtml:UntrustedContent ID="UntrustedContent1" runat="server" EnableViewState="false"
                                            TrustedImageUrlPattern='<%# RegexRelativeImageUrlPatern %>' ClientScriptUrl="~/ClientScript/NeatHtml.js">
                                            <asp:Literal ID="litComment" runat="server" EnableViewState="false" Text='<%# DataBinder.Eval(Container.DataItem, "Comment").ToString() %>' />
                                        </NeatHtml:UntrustedContent>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <fieldset id="fldEnterComments" runat="server" visible="false">
                            <legend>
                                <mp:SiteLabel ID="lblFeedback" runat="server" ConfigKey="NewComment" ResourceFile="ArticleResources"
                                    EnableViewState="false"></mp:SiteLabel>
                            </legend>
                            <asp:Panel ID="pnlNewComment" runat="server">
                                <div class="settingrow">
                                    <mp:SiteLabel ID="lblCommentTitle" runat="server" ForControl="txtCommentTitle" CssClass="settinglabel"
                                        ConfigKey="BlogCommentTitleLabel" ResourceFile="ArticleResources" EnableViewState="false"></mp:SiteLabel>
                                    <asp:TextBox ID="txtCommentTitle" runat="server" MaxLength="100" EnableViewState="false"
                                        CssClass="forminput widetextbox">
                                    </asp:TextBox>
                                </div>
                                <div class="settingrow">
                                    <mp:SiteLabel ID="lblCommentUserName" runat="server" ForControl="txtName" CssClass="settinglabel"
                                        ConfigKey="BlogCommentUserNameLabel" ResourceFile="ArticleResources" EnableViewState="false"></mp:SiteLabel>
                                    <asp:TextBox ID="txtName" runat="server" MaxLength="100" EnableViewState="false"
                                        CssClass="forminput widetextbox">
                                    </asp:TextBox>
                                </div>
                                <div id="divCommentUrl" runat="server" class="settingrow">
                                    <mp:SiteLabel ID="lblCommentURL" runat="server" ForControl="txtURL" CssClass="settinglabel"
                                        ConfigKey="BlogCommentUrlLabel" ResourceFile="ArticleResources" EnableViewState="false"></mp:SiteLabel>
                                    <asp:TextBox ID="txtURL" runat="server" MaxLength="200" EnableViewState="true" CssClass="forminput widetextbox">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regexUrl" runat="server" ControlToValidate="txtURL"
                                        Display="Dynamic" ValidationGroup="articlecomments" ValidationExpression="(((http(s?))\://){1}\S+)">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="settingrow">
                                    <mp:SiteLabel ID="lblRememberMe" runat="server" ForControl="chkRememberMe" CssClass="settinglabel"
                                        ConfigKey="BlogCommentRemeberMeLabel" ResourceFile="ArticleResources" EnableViewState="false"></mp:SiteLabel>
                                    <asp:CheckBox ID="chkRememberMe" runat="server" EnableViewState="false" CssClass="forminput"></asp:CheckBox>
                                </div>
                                <div class="settingrow">
                                    <mp:SiteLabel ID="SiteLabel1" runat="server" CssClass="settinglabel" ConfigKey="BlogCommentCommentLabel"
                                        ResourceFile="ArticleResources" EnableViewState="false"></mp:SiteLabel>
                                </div>
                                <div class="settingrow">
                                    <mpe:EditorControl ID="edComment" runat="server">
                                    </mpe:EditorControl>
                                </div>
                                <asp:Panel ID="pnlAntiSpam" runat="server" Visible="true">
                                    <mp:CaptchaControl ID="captcha" runat="server" />
                                </asp:Panel>
                                <div class="modulebuttonrow">
                                    <portal:mojoButton ID="btnPostComment" runat="server" Text="Submit" ValidationGroup="articlecomments" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlCommentsClosed" runat="server" EnableViewState="false">
                                <asp:Literal ID="litCommentsClosed" runat="server" EnableViewState="false" />
                            </asp:Panel>
                            <asp:Panel ID="pnlCommentsRequireAuthentication" runat="server" Visible="false" EnableViewState="false">
                                <asp:Literal ID="litCommentsRequireAuthentication" runat="server" EnableViewState="false" />
                            </asp:Panel>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="pnlFeedbackDone" runat="server">
                        <p>Cảm ơn bạn đã đăng nhận xét, ban biên tập sẽ kiểm duyệt nội dung nhận xét của bạn.</p>
                    </asp:Panel>
                    <div class="articlecommentservice">
                        <portal:IntenseDebateDiscussion ID="intenseDebate" runat="server" />
                        <portal:DisqusWidget ID="disqus" runat="server" RenderPoweredBy="false" />
                    </div>
                    <%--<asp:Panel ID="pnlFeedback" runat="server" CssClass="feedback">
                            <div class="fb-comments" data-href='<%#ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' data-numposts="5" data-width="700" data-colorscheme="light"></div>
                        </asp:Panel>--%>

                    <asp:Panel ID="pnEventArticle" Visible="false" runat="server">
                        <h2 id="module1461" class="box-other-title"><span class="art-postheadericon">
                            <label>Bài viết cùng sự kiện</label>
                        </span></h2>
                        <div runat="server" visible="true" class="nbccTabContent" style="display: block; margin-top: 0px">
                            <div class="articleTitle" style="margin-top: 0px">
                                <%--<ul class="ul-ortherArticle">--%>
                                <ul class="ulOrtherArticle" style="border: 0px">
                                    <asp:Repeater ID="rptEventArticle" runat="server">
                                        <ItemTemplate>
                                            <li style="padding-top: 0">
                                                <%--                                            <div class='img-other-article' title='<%# Eval("Title") %>'>
                                                <asp:Image ID="image1" Width="100%" Height="100%" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                            </div>--%>
                                                <asp:HyperLink class="linktip" ID="lnkOrther" runat="server" Text='<%#Eval("Title") %>' NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot,Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'></asp:HyperLink>
                                                <span class="bdate-other-article">(
                                                <%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %>)</span>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnShowImageOtherArticle" CssClass="pnOtherArticle" runat="server" Visible="false">
                        <div class="divorther">
                            <div class="divorthertitle">
                             <mp:SiteLabel ID="lblb" runat="server" ResourceFile="ArticleResources" ConfigKey="LabelTinBaiLienQuan" />
                            </div>
                        </div>
                        <ul class="ul-ortherArticleReference">
                            <asp:Repeater ID="rptShowImageOrtherArticle" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:HyperLink ID="lnkOrther" runat="server" Text='<%#Eval("Title") %>' NavigateUrl='<%# GetUrlArticle(Convert.ToInt32(Eval("ItemID"))) %>'></asp:HyperLink>
                                        <span class="bdate-other-article">(
                                                <%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %>)</span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </asp:Panel>

                    <asp:Panel ID="pnlNewOther" runat="server" class="panel panel-default contentNews__Other">
                        <div class="loader">
                            <div class="loadercontent">loading....</div>
                        </div>
                        <div class="panel-body height602" style="border: 0">
                            <div class="divorther">
                                <div class="divorthertitle">
                                    <mp:SiteLabel ID="lblOtherArticle" runat="server" ConfigKey="OrtherArticle" ResourceFile="ArticleResources"></mp:SiteLabel>
                                </div>

                                <button id="search-advanced" data-toggle="collapse" data-target="#search-article" type="button" class="btn btn-default" style="color: #383838; float: right">
                                    <mp:SiteLabel ID="lblll" runat="server" ResourceFile="ArticleResources" ConfigKey="LabelTimKiem" />&nbsp;<i class="fa fa-search"></i>
                                </button>
                            </div>
                            <div class="search-orther">
                                <div class=" collapse" id="search-article">
                                    <div class="form-group col-sm-6" style="padding-top: 10px;">
                                        <label for="filter"><i class="fa fa-star" aria-hidden="true"></i>&nbsp;<mp:SiteLabel ID="SiteLabel5" runat="server" ResourceFile="ArticleResources" ConfigKey="LabelDanhMuc" /></label>
                                        <asp:DropDownList ID="ddlCategorySearch" Style="width: 100%" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-sm-6" style="padding-top: 10px;">
                                        <label for="contain"><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;<mp:SiteLabel ID="SiteLabel4" runat="server" ResourceFile="ArticleResources" ConfigKey="LabelNgayDangTin" /></label>
                                        <div class='input-group date'>
                                            <input type='text' class="form-control" id="datetimepicker" style="width: calc(100% - 50px)" />
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-12">
                                        <label for="contain" style="width: 100%"><i class="fa fa-key" aria-hidden="true"></i>&nbsp;<mp:SiteLabel ID="SiteLabel6" runat="server" ResourceFile="ArticleResources" ConfigKey="LabelKeyword" /></label>
                                        <input class="form-control" style="width: 100%" type="text" id="keyword" />
                                    </div>
                                    <div class="form-group col-sm-12" style="text-align: center">
                                        <button type="button" class="btn btn-danger" data-toggle="collapse" data-target="#search-article"><span>X</span> <mp:SiteLabel ID="SiteLabel7" runat="server" ResourceFile="ArticleResources" ConfigKey="LabelClose" /></button>
                                        <button type="button" class="btn btn-primary" onclick="SearchArticle(1)"><i class="fa fa-search" aria-hidden="true"></i><mp:SiteLabel ID="SiteLabel8" runat="server" ResourceFile="ArticleResources" ConfigKey="LabelTimKiem" /></button>
                                    </div>
                                </div>

                            </div>

                            <asp:Panel ID="pnOrtherArticle" runat="server" Visible="false">
                                <div class="divArticleOrther">
                                    <ul class="ulArticleOrther" id="ShowArticleOrther">
                                    </ul>
                                </div>
                            </asp:Panel>

                            <div class="Pager tinLienquan"></div>
                        </div>
                        <script type="text/javascript">
                            $(document).on("click", ".page", function () {
                                SearchArticle(parseInt($(this).attr('page')));
                            });
                            $(document).ready(function () {
                                SearchArticle(1);
                                $('ul.ulOrtherArticle li a').mouseover(function () {
                                    $(this).next().show();
                                }).mouseout(function () {
                                    $(this).next().hide();
                                });
                            });
                            $('#datetimepicker').datepicker({
                                clearBtn: true,
                                language: "vi",
                                keyboardNavigation: false,
                                forceParse: false,
                                autoclose: true,
                                todayHighlight: true,
                                toggleActive: true
                            });
                            $("#search-advanced").click(function () {
                                $(".searchblock").fadeToggle();
                            });
                            $(".close").click(function () {
                                $(".searchblock").fadeOut();
                            });
                            //$(document).click(function (event) {
                            //    target = document.getElementById("art-main");
                            //    alert(target);
                            //    alert(event.target);
                            //    if (event.target == target) {
                            //        $(".searchblock").fadeOut();
                            //    }
                            //});
                            function SearchArticle(pageIndex) {
                                $(".loader").fadeIn();
                                var category = $("#<%=ddlCategorySearch.ClientID%>").val();
        var keyword = $("#keyword").val();
        var startdate = $("#datetimepicker").val();
        var parameter = {
            pageIndex: pageIndex,
            category: category,
            startdate: startdate,
            keyword: keyword
        };
        $.ajax({
            type: "POST",
            cache: false,
            //async: false,
            url: "<%=ResolveUrl("~/Article/ViewPost.aspx/SearchOrtherArticle") %>",
            data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            error: function (error) {
                //alert(error.message);
            }
        });
                                $(".searchblock").fadeOut();
                                $(".loader").fadeOut();
                            }
                            function OnSuccess(response) {
                                var articleOrther = response.d.ListArticle;
                                if (articleOrther != null && articleOrther.length > 0) {
                                    var str = "";
                                    $.each(articleOrther, function (index, article) {
                                        var url = "";
                                        if (article.ItemUrl != null && article.ItemUrl.length > 0) {
                                            url = article.SiteRoot + article.ItemUrl.replace("~", "");
                                        }
                                        else {
                                            url = article.SiteRoot + "/Article/ViewPost.aspx?pageid=" + article.PageID + "&itemid=" + article.ItemID + "&mid=" + article.ModuleID;
                                        }
                                        str += "<li>";
                                        str += "<div class='width10'><i class='fa fa-caret-right' style='color:#a5a5a5'>&nbsp;</i>&nbsp;</div>";
                                        str += "<div class='fwidth'><a href='" + url + "' class='linktip' title='" + article.Title + "'>" + article.Title + "</a>";
                                        str += "&nbsp;<span class='dateArticleOrther'>(" + article.StartDate + ")</span>";
                                        str += '</div>';
                                        str += ' <div class="art-tooltip" style="display: none;">';
                                        str += '<a title="' + article.Title + '" href="' + url + '">' + article.Title + '</a>';
                                        str += '<span>' + article.StartDate + '</span>';
                                        str += '< p >' + article.Summary + '</p>';
                                        str += '<div class="tipnarrow"></div>';
                                        str += '</div>';
                                        str += "</li>";
                                    });
                                    $("#ShowArticleOrther").html(str);
                                    $(".Pager").ASPSnippets_Pager({
                                        ActiveCssClass: "current",
                                        PagerCssClass: "pager",
                                        PageIndex: response.d.PageIndex,
                                        PageSize: response.d.PageSize,
                                        RecordCount: response.d.CountItem,
                                        Page: "Trang",
                                    });
                                } else {
                                    $("#ShowArticleOrther").html("<li><span class='notfound'>Không tìm thấy dữ liệu...</span></li>");
                                    $(".Pager").html("");
                                }
                            };
                        </script>

                    </asp:Panel>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlExcerpt" runat="server" Visible="false">
                <div class="articletext">
                    <asp:Literal ID="litExcerpt" runat="server" EnableViewState="false" />
                </div>
                <mp:SiteLabel ID="SiteLabel2" runat="server" CssClass="settinglabel" ConfigKey="MustSignInToViewFullPost"
                    ResourceFile="ArticleResources" EnableViewState="false"></mp:SiteLabel>
            </asp:Panel>
        </div>
    </portal:mojoPanel>
</asp:Panel>
<script>
    var list_childNodes = document.getElementById("article_text").childNodes.length;
    function fontPlus() {
        var divTemp = document.getElementById('article_text');
        if (divTemp.style.fontSize == '') divTemp.style.fontSize = '12px';
        var s = divTemp.style.fontSize;
        if ((s.indexOf('px') > -1) && (s.indexOf('px') == (s.length - 2))) s = s.substring(0, s.indexOf('px'));
        divTemp.style.fontSize = (parseFloat(s) + 1) + 'px';

        divTemp = document.getElementById('article_Summary');
        if (divTemp.style.fontSize == '') divTemp.style.fontSize = '14px';
        s = divTemp.style.fontSize;
        if ((s.indexOf('px') > -1) && (s.indexOf('px') == (s.length - 2))) s = s.substring(0, s.indexOf('px'));
        divTemp.style.fontSize = (parseFloat(s) + 1) + 'px';

        if (list_childNodes > 0) {
            var c = document.getElementById('article_text').childNodes;
            for (var i = 1; i <= list_childNodes; i++) {
                //if (i == 1 || i % 2 > 0) {
                if (c[i] != undefined && c[i].style != undefined) {
                    var s1 = c[i].style.fontSize;
                    if ((s1.indexOf('px') > -1) && (s1.indexOf('px') == (s1.length - 2)))
                        s1 = s1.substring(0, s1.indexOf('px'));
                    c[i].style.fontSize = (parseFloat(s1) + 1) + 'px';
                    SetFontSizeUp(c[i]);
                }
                //}
            }
        }
    }

    function SetFontSizeUp(obj) {
        var list_childNodes = obj.childNodes.length;
        if (list_childNodes > 0) {
            var c = obj.childNodes;
            for (var i = 0; i < list_childNodes; i++) {
                if (c[i] != undefined && c[i].style != undefined) {
                    var s1 = c[i].style.fontSize;
                    //if (i == 1 || i % 2 > 0) {
                    if ((s1.indexOf('px') > -1) && (s1.indexOf('px') == (s1.length - 2)))
                        s1 = s1.substring(0, s1.indexOf('px'));
                    c[i].style.fontSize = (parseFloat(s1) + 1) + 'px';
                    //}
                    if (c[i].childNodes.length > 0) {
                        SetFontSizeUp(c[i])
                    }
                }
            }
        }
    }

    function fontMinus() {
        var divTemp = document.getElementById('article_text');
        if (divTemp.style.fontSize == '') divTemp.style.fontSize = '12px';
        var s = divTemp.style.fontSize;
        if ((s.indexOf('px') > -1) && (s.indexOf('px') == (s.length - 2))) s = s.substring(0, s.indexOf('px'));
        divTemp.style.fontSize = (parseFloat(s) - 1) + 'px';
        divTemp = document.getElementById('article_Summary');
        if (divTemp.style.fontSize == '') divTemp.style.fontSize = '14px';
        s = divTemp.style.fontSize;
        if ((s.indexOf('px') > -1) && (s.indexOf('px') == (s.length - 2))) s = s.substring(0, s.indexOf('px'));
        divTemp.style.fontSize = (parseFloat(s) - 1) + 'px';

        if (list_childNodes > 0) {
            var c = document.getElementById('article_text').childNodes;
            for (var i = 1; i <= list_childNodes; i++) {
                //if (i == 1 || i % 2 > 0) {
                if (c[i] != undefined && c[i].style != undefined) {
                    var s1 = c[i].style.fontSize;
                    if ((s1.indexOf('px') > -1) && (s1.indexOf('px') == (s1.length - 2)))
                        s1 = s1.substring(0, s1.indexOf('px'));
                    c[i].style.fontSize = (parseFloat(s1) - 1) + 'px';
                    SetFontSizeDown(c[i]);
                }
                //}
            }
        }
    }

    function SetFontSizeDown(obj) {
        var list_childNodes = obj.childNodes.length;
        if (list_childNodes > 0) {
            var c = obj.childNodes;
            for (var i = 0; i < list_childNodes; i++) {
                if (c[i] != undefined && c[i].style != undefined) {
                    var s1 = c[i].style.fontSize;
                    //if (i == 1 || i % 2 > 0) {
                    if ((s1.indexOf('px') > -1) && (s1.indexOf('px') == (s1.length - 2)))
                        s1 = s1.substring(0, s1.indexOf('px'));
                    c[i].style.fontSize = (parseFloat(s1) - 1) + 'px';
                    //}
                    if (c[i].childNodes.length > 0) {
                        SetFontSizeDown(c[i])
                    }
                }
            }
        }
    }

    /*In nội dung bài viết*/
    function PrintSubject(titlePage) {
        $(".gotop").hide();
        $(".feedback").hide();
        $(".fblikebutton").hide();
        $("h2.moduletitle").hide();
        $(".pnOtherArticle").hide();
        $(".otherpanel").hide();
        $(".print").hide();
        var htmlContent = $('.articlecenter-rightnav div').html();
        var obj;
        obj = document.getElementById('tableforprint');
        myWindow = window.open('_blank', 'Preview', 'resizable = 1 ,scrollbars =1 , menubar = 1 ,status=1,left = 0 ,top = 0 ');
        myWindow.document.write("<html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>");
        myWindow.document.write("<head>");
        myWindow.document.write("<title>" + titlePage + "</title><style type='text/css'>.Title{ font-family: 'Times New Roman'; font-size: 14pt; font-weight: bold; margin-top: 0 }</style>");
        myWindow.document.write("<link rel='stylesheet' type='text/css' href='/Data/Sites/1/skins/art42-blue/style.css' />");
        //myWindow.document.write("<link rel='stylesheet' type='text/css' href='/CMS/css/ISE_SP.css' media='print' />");
        myWindow.document.write("</head>");
        myWindow.document.write("<body style='width:700px;margin: 0px auto 0;background-color: #FFF;' >");

        myWindow.document.write("<div align='right' style='width:700px;margin-left:auto;margin-right:auto'><a herf='' onclick='javascript:print();return false;'><img style='border-width:0px;cursor: pointer;' src='/Data/Sites/1/skins/art42-blue/images/printer32x32.png' alt='In trang'/></a></div>");
        myWindow.document.write("<div style='float:left;width:100%;font-size:25px;'><center>" + $(".siteheading").html() + "</center></div>");
        //siteheading
        myWindow.document.write("<div style='float:left;width:100%'>" + document.URL + "</div>");
        myWindow.document.write("<div align='left' style='width:100%;text-align:left;margin-left:auto;margin-right:auto'>" + htmlContent + "</div><br/>");
        myWindow.document.write("<div style='float:left;width:100%'>" + document.URL + "</div>");
        myWindow.document.write("");
        myWindow.document.write("</body>");
        myWindow.document.write("</html>");
        myWindow.document.close();
        $(".gotop").show();
        $(".feedback").show();
        $(".fblikebutton").show();
        $("h2.moduletitle").show();
        $(".pnOtherArticle").show();
        $(".otherpanel").show();
        $(".print").show();
        return false;
    }

</script>

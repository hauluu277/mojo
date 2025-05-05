<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="QuestionAnswerRecenlist.ascx.cs" Inherits="QuestionAnswerFeatures.UI.QuestionAnswerRecenlist" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="recentlistQuestion">
    <h1>
        <asp:Label ID="lblTitle" runat="server"></asp:Label>
    </h1>
    <div class="btn-dangtin">
        <button type="button" id="btnDangTin" runat="server">
            <i class="fa fa-plus" aria-hidden="true"></i>&nbsp; Đăng câu hỏi
        </button>
    </div>
    <div class="clear"></div>
    <asp:Panel ID="pnlSearchControl" runat="server" DefaultButton="btnSearch">
        <div class="search-container">
            <div class="search-category">
                <asp:DropDownList ID="ddlLinhVuc" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>
            <div class="search-keyword">
                <div class="qa-header-search">
                    <div id="qa-search">
                        <asp:TextBox ID="txtKeyword" runat="server" placeholder="Tìm kiếm hỏi đáp..."></asp:TextBox>
                        <asp:Button ID="btnSearch" CausesValidation="false" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#explandctl").click(function () {
                $(this).hide();
                $("#collapse").show();
                $("#searchcontrol").show();
            });

            $("#collapse").click(function () {
                $(this).hide();
                $("#explandctl").show();
                $("#searchcontrol").hide();
            });

            if ($("#hdfHasParam").val() != null && $("#hdfHasParam").val() != "") {
                $("#explandctl").hide();
                $("#collapse").show();
                $("#searchcontrol").show();
            }
        });
    </script>
    <div class="col-md-6 col-sm-12 no-padding text-left info-list">
        <i class="fa fa-calendar-check-o" aria-hidden="true"></i>
        <asp:Label ID="lblHoiDapResult" runat="server"></asp:Label>
    </div>
    <style>
        .qa-questions-list {
            width: 100%;
            float: left;
        }

        .HienThiDangList .qa-question-item {
            width: 100%;
            float: left;
        }

        .HienThiDangList .qa-question-info {
            position: relative;
        }

        .HienThiDangList .qa-question-info {
            margin-bottom: 0;
            padding: 0 0 0 90px;
            position: relative;
        }

        .HienThiDangList .qa-question-item .qa-question-info {
            padding: 20px 180px 20px 80px;
            background-color: #eef4f9;
            margin: 15px 0;
        }

        .HienThiDangList .qa-question-title {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            -webkit-transition: all .4s linear 0s;
            transition: all .4s linear 0s;
            max-width: 100%;
            padding: 10px 0px;
        }

            .HienThiDangList .qa-question-title a {
                color: #666;
                font-weight: 600;
                font-size: 16px;
            }

        .qa-question-item .qa-question-meta {
            font-size: small;
            margin-bottom: 20px;
        }

        .HienThiDangRutGon .qa-question-meta, .HienThiDangRutGon .qa-question-stats, .HienThiDangRutGon .qa-question-content {
            padding: 10px 15px;
        }

        .qa-questions-list .qa-question-item .qa-question-meta {
            color: #999;
            margin-bottom: 0;
        }

        .HienThiDangList .qa-question-item .qa-status {
            background: rgba(0,0,0,0) none repeat scroll 0 0;
            border-radius: 36px;
            box-shadow: 0 0 0 1px #e67e22 inset;
            height: 36px;
            left: 15px;
            margin-top: -18px;
            padding: 0;
            position: absolute;
            text-indent: -9999px;
            top: 50%;
            width: 36px;
        }

        .HienThiDangList .qa-question-item .qa-status-answered {
            box-shadow: 0 0 0 1px #1ba1e2 inset;
        }

        .HienThiDangList .qa-question-item .qa-status:after {
            display: block;
            height: 36px;
            position: absolute;
            text-align: center;
            text-indent: 0;
            text-rendering: auto;
            top: 0;
            line-height: 36px;
            width: 36px;
        }

        .HienThiDangList .qa-question-item .qa-status-answered:after {
            color: #1ba1e2;
            font-weight: 900;
            content: "?";
            font-size: 14px;
        }

        .qa-question-meta > span a {
            color: #2196f3;
            font-size: 14px;
            text-transform: capitalize;
        }

        .qa-question-meta > span a {
            color: #2196f3;
            font-size: 14px;
            text-transform: capitalize;
        }

        .HienThiDangList.qa-questions-list .qa-question-item .qa-question-stats {
            margin-top: -24px;
            position: absolute;
            right: 10px;
            top: 50%;
        }

        .HienThiDangList .qa-views-count {
            background-color: #2196f3;
            color: #fff;
            font-size: 13px;
            padding-top: 5px;
        }

        .HienThiDangRutGon .qa-views-count {
            margin-right: 10px;
        }

        .HienThiDangList .qa-answers-count {
            background-color: #f26c4f;
            color: #fff;
            font-size: 13px;
            padding-top: 5px;
        }

        .HienThiDangList.qa-questions-list .qa-question-item .qa-question-stats span {
            border: 1px solid #ddd;
            display: block;
            float: left;
            height: 50px;
            margin-left: 10px;
            min-width: 65px;
            text-align: center;
        }

        .HienThiDangList.qa-questions-list .qa-question-item .qa-question-stats strong {
            display: block;
            font-size: 14px;
            font-weight: 400;
            line-height: 26px;
        }

        .HienThiDangList .qa-question-content {
            display: none;
        }

        .HienThiDangRutGon .qa-question-item {
            border-top: 1px solid #e9ebec;
            border-radius: .25rem;
            color: #212529;
            background-color: #fff;
            border: 1px solid #e9ebec;
        }

            .HienThiDangRutGon .qa-question-item:not(:first-child) {
                margin-top: 10px;
            }

        .HienThiDangRutGon .qa-question-meta, .HienThiDangRutGon .qa-question-stats, .HienThiDangRutGon .qa-question-content, .HienThiDangRutGon .qa-status {
            display: none;
        }

        .HienThiDangRutGon .qa-question-info .qa-question-title a {
            position: relative;
            border-radius: .25rem;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-align: center;
            -ms-flex-align: center;
            align-items: center;
            width: 100%;
            padding: 1rem 1.25rem;
            font-size: 18px;
            color: #212529;
            text-align: left;
            background-color: #fff;
            border: 0;
            overflow-anchor: none;
            line-height: 25px;
        }

            .HienThiDangRutGon .qa-question-info .qa-question-title a:not(.collapsed) {
                background-color: rgba(64, 81, 137, 0.05);
            }

            .HienThiDangRutGon .qa-question-info .qa-question-title a::after {
                -ms-flex-negative: 0;
                flex-shrink: 0;
                width: 1rem;
                height: 1rem;
                margin-left: auto;
                content: "";
                background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='var%28--vz-body-color%29'%3e%3cpath fill-rule='evenodd' d='M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z'/%3e%3c/svg%3e");
                background-repeat: no-repeat;
                background-size: 1rem;
            }

        .HienThiDangRutGon .show {
            display: block !important;
        }

        .HienThiDangRutGon .qa-question-content__title span {
            font-size: 16px;
        }

        .HienThiDangRutGon .qa-question-content__body {
            margin-top: 10px;
        }
    </style>
    <div class="<%= GiaoDienHienThiQA %> qa-questions-list">
        <asp:Repeater ID="rptQuestion" runat="server">
            <ItemTemplate>
                <div class="qa-question-item">
                    <div class="qa-question-info">
                        <div class="qa-question-title">
                            <asp:HyperLink ID="hplQuestion" CssClass="collapsed" runat="server" ToolTip='<%# Eval("Question") %>' NavigateUrl='<%# QuestionAnswerUtils.FormatDetailQuestionUrl(SiteRoot,PageID, Eval("ItemUrl").ToString(),int.Parse(Eval("ItemID").ToString()),true,"") %>' Text='<%#Eval("Question").ToString() %>'></asp:HyperLink>
                        </div>
                        <div class="qa-question-meta">
                            <span class="qa-status qa-status-answered" title="Open">Open</span>
                            <span>
                                <asp:Label ID="lblSenderLabel" runat="server"></asp:Label>
                                <asp:Label ID="lblSender" ForeColor="#333" Font-Bold="true" runat="server"><%#Eval("Name") %></asp:Label>
                            </span>
                            <span class="qa-question-category">•
                                <asp:HyperLink ID="hplCategory" NavigateUrl='<%# QuestionAnswerUtils.FormatQuestionListUrl(SiteRoot,"",PageID,int.Parse(Eval("LinhVucID").ToString()),int.Parse(Eval("LoaiLinhVucID").ToString()),OrderBy,true,"") %>' ToolTip='<%#Eval("CategoryName") %>' Text='<%#Eval("CategoryName") %>' runat="server"></asp:HyperLink>
                            </span>
                            <span class="qa-question-date">•
                                <asp:Label ID="lblTimeSendLabel" runat="server"></asp:Label>
                                <asp:Label ID="lblTimeSend" runat="server"><%# string.Format("{0:HH:mm dd/MM/yyyy}",DataBinder.Eval(Container.DataItem,"CreateDate")) %></asp:Label>
                            </span>
                        </div>
                        <div class="qa-question-stats">
                            <span class="qa-views-count">
                                <asp:Literal ID="lblAnswerLabel" runat="server"></asp:Literal>
                                <strong>
                                    <asp:Literal ID="lblAnswer" Text='<%#Eval("TotalAnswerApproved").ToString() %>' runat="server"></asp:Literal>
                                </strong>
                            </span>
                            <span class="qa-answers-count">
                                <asp:Literal ID="lblViewLabel" runat="server"></asp:Literal>
                                <strong>
                                    <asp:Literal ID="lblView" Text='<%# Eval("Views").ToString() %>' runat="server"></asp:Literal>
                                </strong>
                            </span>
                        </div>
                    </div>
                    <%--<div class="recentlistBody-left-category">
                        
                        <asp:Panel ID="pnlChileCategory" Visible='<%# DisplayCategory(Eval("LoaiLinhVucID").ToString()) %>' runat="server">
                            &nbsp; &gt;
                            <asp:HyperLink ID="hplChileCategory" ToolTip='<%#Eval("CategoryChildName") %>' NavigateUrl='<%# QuestionAnswerUtils.FormatQuestionListUrl(SiteRoot,"",PageID,int.Parse(Eval("LinhVucID").ToString()),int.Parse(Eval("LoaiLinhVucID").ToString()),OrderBy,true,"") %>' Text='<%#Eval("CategoryChildName") %>' runat="server"></asp:HyperLink>
                        </asp:Panel>
                    </div>--%>
                    <div class="qa-question-content">
                        <div class="qa-question-content__title">
                            <asp:Label ID="lblContent" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="qa-question-content__body">
                            <a href="<%# QuestionAnswerUtils.FormatDetailQuestionUrl(SiteRoot,PageID, Eval("ItemUrl").ToString(),int.Parse(Eval("ItemID").ToString()),true,"") %>">
                                <%#Eval("ContentQuestion") %>
                            </a>
                        </div>
                    </div>

                    <div class="recentlistFooter recentlistFooter hide">
                        <asp:HyperLink ID="hplAnswer" runat="server" NavigateUrl='<%# QuestionAnswerUtils.FormatDetailQuestionUrl(SiteRoot,PageID,Eval("ItemUrl").ToString(),int.Parse(Eval("ItemID").ToString()),true,"") %>' CssClass="floatleft"></asp:HyperLink>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlDictionaryPager" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrDictionary" runat="server" />
        </asp:Panel>
    </div>
</div>
<script>
    $(document).ready(function () {
        $(".HienThiDangRutGon .qa-question-item .qa-question-title").on("click", function (e) {
            e.preventDefault();
            $(this).closest(".qa-question-item").find(".qa-question-title a").toggleClass("collapsed");
            $(this).closest(".qa-question-item").find(".qa-question-meta").toggleClass("show");
            $(this).closest(".qa-question-item").find(".qa-question-stats").toggleClass("show");
            $(this).closest(".qa-question-item").find(".qa-question-content").toggleClass("show");
        })
    })
</script>

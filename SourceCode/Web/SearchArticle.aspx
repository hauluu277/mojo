<%@ Page Language="c#" ValidateRequest="false" MaintainScrollPositionOnPostback="true"
    EnableViewStateMac="false" CodeBehind="SearchArticle.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.UI.Pages.SearchArticle" %>

<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
        <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="/" Text="Trang chủ" /><portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
        <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" Text="Tìm kiếm tin tức" />
    </portal:AdminCrumbContainer>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper searchresults">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <portal:SearchResultsDisplaySettings ID="displaySettings" runat="server" />
                    <link href="../Data/js/bootstrap-datepicker-1.6.4-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
                    <script src="../Data/js/bootstrap-datepicker-1.6.4-dist/js/bootstrap-datepicker.min.js"></script>
                    <style type="text/css">
                        .fstElement {
                            height: 32px;
                            width: 100%;
                        }

                        .fstResultItem {
                            text-align: left;
                        }

                        .fstToggleBtn {
                            width: 100%;
                            min-width: 100%;
                            text-align: left;
                        }

                        .form-control, input[type=text].forminput, select.forminput {
                            border-radius: 0;
                        }

                        label {
                            font-weight: normal;
                        }

                        #dialogCat select, #dialog select {
                            height: 32px;
                        }

                        .search-item {
                            text-align: center;
                        }

                        .mld_width220 {
                            width: 92% !important;
                            float: left;
                        }

                        .width200 input[type=text] {
                            width: 100% !important;
                        }

                        .search-label {
                            width: 220px;
                            text-align: left;
                            float: left;
                            margin-bottom: 5px;
                        }

                        .mld_haan .input-group-addon {
                            max-width: 30px;
                            padding: 6px 3px !important;
                        }

                        .modal-body {
                            position: relative;
                            padding: 15px 15px;
                            height: calc(100vh - 190px);
                        }

                        .fa-angle-double-right:before {
                            font-size: 18px;
                        }

                        .active_search {
                            display: block !important;
                        }

                        #ctl00_mainContent_pnlSearchResult span {
                            background-color: #FF9;
                            color: #555;
                        }

                        .highlight {
                            background-color: yellow;
                        }

                        .search-label-fix {
                            width: 90px;
                            float: left;
                            margin-top: 7px;
                            text-align: left;
                        }

                        .mld_width220-fix {
                            width: auto;
                            float: left;
                        }

                        .box-inline {
                            margin: 0;
                        }

                        input[type=checkbox], input[type=radio] {
                            margin-left: -30px;
                            margin-top: 10px;
                        }

                        .box-inline li label {
                            display: inline;
                            margin: 38px;
                            margin-top: 8px;
                            float: left;
                            margin-left: -3px;
                        }

                        .box-inline li {
                            margin: 0;
                            padding-left: 10px;
                        }

                        #ctl00_mainContent_rbtListSearch_0, #ctl00_mainContent_rbtListSearch_4 {
                            margin-left: -17px;
                        }

                        .fieldset {
                            min-height: 200px;
                        }

                        .form-group, .gmap, .logolist, .modulepager, .pagelayout-buttons, .settingrow, .ui-widget, .uploadcontainer, [id*=_divRole], [id*=_pnlLookup], [id*=pnlNewFolder] {
                            margin-bottom: 15px;
                            width: 96%;
                            float: left;
                            margin-left: 2%;
                        }

                        .article-thongbao img {
                            float: left;
                            padding-right: 10px;
                            object-fit: contain;
                            max-width: 350px;
                        }
                    </style>
                    <script src="/Data/plugins/mark.js/mark.min.js"></script>
                    <script>
                        function ReloadPage() {
                            window.location.href = window.location.pathname;;
                        }
                        function ShowActiveSearch() {
                            if ($("#search_advance").hasClass("active_search")) {
                                $("#search_advance").removeClass("active_search");
                            } else {
                                $("#search_advance").addClass("active_search");
                            }
                        }

                        $(document).ready(function () {
                            $('.date')
                                .datepicker({
                                    format: 'dd/mm/yyyy',
                                    startDate: '01/01/2010',
                                    endDate: '12/30/2050'
                                });


                        });


                    </script>
                    <asp:Panel ID="pnlInternalSearch" runat="server" DefaultButton="btnSearch">
                        <fieldset class="fieldset mld_haan">
                            <legend class="legend">Tìm kiếm tin bài</legend>
                            <div class="form-group">
                                <div class="col-sm-12 col-md-6">
                                    <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label"
                                        ConfigKey="ArticleEditKeywordLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                    <div class="search-control mld_width220">
                                        <asp:TextBox ID="txtKeyword" Width="100%" Height="34" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <label class="search-label">Ngày phát hành</label>
                                    <input type="text" class="form-control date width200" style="width: calc(92% - 50px) !important; float: left" name="date" id="txtStartDate" runat="server" />
                                    <span class="input-group-addon add-on addcontrol" style="width: 50px; float: left; height: 34px;">
                                        <i class="fa fa-calendar" aria-hidden="true"></i>
                                    </span>
                                </div>


                            </div>
                             <div class="form-group">
                                <div class="col-sm-12 col-md-6">
                                    <div class="search-control mld_width220">
                                        <label class="search-label">Chuyên mục</label>
                                        <asp:DropDownList ID="ddlCategory" Width="100%" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <label class="search-label">Thời gian tìm kiếm</label>
                                    <div class="search-control mld_width220" style="float: left;">
                                        <asp:DropDownList Width="100%" ID="ddlTypeSearch" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <label class="search-label-fix">Tìm theo</label>
                                    <div class="search-control mld_width220-fix" style="float: left;">
                                        <asp:RadioButtonList ID="rbtListSearch" runat="server" CssClass="radio-inline"></asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <p style="text-align: right; width: 96%; float: left;">
                                <portal:mojoButton ID="btnSearch" runat="server" Text="Tìm kiếm" SkinID="ButtonSuccess" />
                                <button class="btn btn-warning" type="button" style="display: none" onclick="ShowActiveSearch()"><i class="fa fa-search-minus" aria-hidden="true"></i>&nbsp;Nâng cao</button>
                                <button type="button" class="btn btn-primary" onclick="ReloadPage()"><i class="fa fa-refresh" aria-hidden="true"></i>&nbsp;Tải lại</button>
                            </p>
                        </fieldset>
                    </asp:Panel>
                    <div id="search_div">
                        <asp:Panel ID="pnlSearchResult" runat="server">
                            <asp:Panel ID="pnlScrollable" runat="server">
                                <asp:Literal ID="literSearchResult" runat="server"></asp:Literal>
                                <asp:Repeater ID="rptArticle" runat="server">
                                    <ItemTemplate>
                                        <div class="article-thongbao">
                                            <div runat="server" tooltip='<%# Eval("Title") %>'  visible='<%#ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>'>
                                                <a href="<%# FormatUrlArtcile(Eval("ItemUrl").ToString()) %>">
                                                    <asp:Image ID="image3" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                                </a>
                                            </div>
                                            <div class="info-article">
                                                <%-- <h6>
                                                <asp:HyperLink ID="hplCategory" runat="server" Visible='<%#Config.ShowCategorySetting %>' NavigateUrl='<%#SiteRoot+Eval("CategoryUrl") %>' Text='<%#Eval("CategoryName") %>'></asp:HyperLink>
                                            </h6>--%>
                                                <h3 class="article-title">
                                                    <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                                        ToolTip='<%# Eval("Title") %>' Text='<%# Eval("Title").ToString() %>'
                                                        NavigateUrl='<%#FormatUrlArtcile(Eval("ItemUrl").ToString()) %>'>
                                                    </asp:HyperLink>
                                                </h3>
                                                <div class="author">
                                                    <strong><%# Eval("CreatedByUser") %></strong>
                                                </div>
                                                <asp:Panel ID="pnlPost" runat="server" CssClass="post">
                                                    <div class="article-date">
                                                        <span class="article-author"><b></b></span>
                                                        <div class="detail-muted">
                                                            <ul class="list-unstyled list-inline">
                                                                <li>
                                                                    <img src="/Data/Images/special/post-clock.png" />
                                                                    <%# string.Format("{0:dd/MM/yyyy}", Eval("StartDate")) %>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                    <div class="summary-article">
                                                        <%# Eval("Summary").ToString() %>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Panel ID="pnlArticlePager" runat="server" CssClass="blogpager" Visible="false">
                                    <portal:mojoCutePager ID="pgr" runat="server" />
                                </asp:Panel>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared">
            </portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server">
</asp:Content>


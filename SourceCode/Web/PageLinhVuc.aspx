<%@ Page Language="c#" ValidateRequest="false" MaintainScrollPositionOnPostback="true"
    EnableViewStateMac="false" CodeBehind="PageLinhVuc.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master"
    AutoEventWireup="false" Inherits="mojoPortal.Web.UI.PageLinhVuc" %>

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
                    <style type="text/css">
                        .left-menu {
                            width: 100%;
                            float: left;
                            border: 1px solid white;
                        }

                            .left-menu h3 {
                                background: #6c757d;
                                color: white;
                                margin: 0;
                                padding: 8px;
                                font-size: 16px;
                                text-align: center;
                                line-height: 25px;
                                width: 100%;
                                float: left;
                                border: 1px solid white;
                            }

                                .left-menu h3 a, .left-menu h3 a:hover {
                                    color: white !important;
                                }

                            .left-menu ul li {
                                width: 25%;
                                float: left;
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



                        @media all and (max-width: 480px) and (min-width: 320px) {
                            .bd-text-lv img {
                                height: auto;
                            }

                            .content-article {
                                width: 100%;
                            }

                            .left-menu ul li {
                                width: 100%;
                            }
                        }

                        @media all and (max-width: 768px) and (min-width: 481px) {
                            .bd-text-lv img {
                                height: 145px !important;
                            }

                            .left-menu ul li {
                                width: 50% !important;
                            }

                            .danhsach-form .col-sm-4 {
                                width: 50% !important;
                                max-width: 50% !important;
                                flex: 0 0 50%;
                            }

                            .content-article {
                                width: 100%;
                            }

                            .left-menu h3 {
                                font-size: 14px;
                            }
                        }

                        @media all and (max-width: 991px) and (min-width: 769px) {
                            .bd-text-lv img {
                                height: 200px !important;
                            }

                            .left-menu ul li {
                                width: 50%;
                            }

                            .danhsach-form .col-sm-4 {
                                width: 50% !important;
                                max-width: 50% !important;
                                flex: 0 0 50% !important;
                            }
                        }

                        @media all and (max-width: 1199px) and (min-width: 992px) {
                            .left-menu h3 {
                                font-size: 15px;
                            }
                        }

                        .article-thongbao:first-child {
                            padding-top: 0 !important;
                        }

                        .tieude-lv {
                            text-align: center;
                            font-size: 16px;
                            line-height: 24px;
                            color: #333;
                            font-family: 'Roboto';
                            margin: 15px 0px;
                            overflow: hidden;
                            text-overflow: ellipsis;
                            -webkit-line-clamp: 2;
                            display: -webkit-box;
                            -webkit-box-orient: vertical;
                            height: 45px;
                        }

                        .bd-text-lv {
                            border: 1px solid #e9e9e9;
                            padding: 12px;
                            box-shadow: rgb(25 25 25 / 4%) 0 0 1px 0, rgb(0 0 0 / 10%) 0 3px 4px 0;
                            background-color: #f5f5f566;
                            border-radius: 2px;
                        }

                        .folder-link-lv i {
                            color: #9e9e9e;
                        }

                            .folder-link-lv i span {
                                color: #43a7ef;
                            }

                        .folder-link-lv span {
                            font-size: 12px;
                            color: #9e9e9e;
                            font-family: sans-serif;
                            margin-left: 2px;
                        }

                        .noidung-lv {
                            font-size: 14px;
                            font-family: sans-serif;
                            color: #333;
                            line-height: 22px;
                            margin-top: 10px;
                            padding: 5px 0px;
                            overflow: hidden;
                            text-overflow: ellipsis;
                            -webkit-line-clamp: 3;
                            display: -webkit-box;
                            -webkit-box-orient: vertical;
                            height: 70px;
                        }

                        .bt-search {
                            text-align: center;
                            margin: 15px 0px 5px;
                        }

                        .pd-from-search {
                            width: 100%;
                            float: left;
                        }

                        .from-search-bd {
                            width: 100%;
                            float: left;
                            border: 1px solid #cccccc;
                            padding: 15px;
                            border-radius: 5px;
                            background-color: #f5f5f566;
                        }

                        .text-label {
                            width: 25%;
                            float: left;
                            padding: 5px;
                            font-size: 14px;
                            font-family: 'Roboto Medium';
                        }

                        .ip-text {
                            width: 75%;
                            float: left;
                            padding: 0px 10px;
                        }

                        .input-text {
                            width: 100%;
                            padding: 5px;
                        }

                        .from-search-bd .col-sm-12 {
                            float: left;
                        }

                        .from-search-bd .col-sm-6 {
                            margin: 10px 0px;
                            float: left;
                        }

                        .danhsach-form {
                            width: 100%;
                            float: left;
                            margin: 15px 0px;
                            padding-left: -15px !important;
                            padding-right: -15px !important;
                        }

                            .danhsach-form .col-sm-4 {
                                margin: 15px 0px;
                            }

                        .bd-text-lv img {
                            height: 230px;
                        }

                        .div_link {
                            position: absolute;
                            background-color: rgba(0,0,0,.7);
                            top: 0;
                            left: 0;
                            height: 230px;
                            width: 100%;
                            opacity: 1;
                            -moz-transition: all .2s ease;
                            -webkit-transition: all .2s ease;
                            transition: all .2s ease;
                            display: none;
                            text-align: center;
                        }

                        .bd-text-lv > .div_content:hover .div_link {
                            display: block;
                        }

                        .div_link span {
                            display: inline-block;
                            padding: 10px 20px;
                            color: #fff;
                            border-radius: 30px;
                            width: 80%;
                            font-size: 15px;
                            text-align: left;
                            font-weight: bold;
                            max-width: 240px;
                        }

                            .div_link span a {
                                color: white;
                            }

                            .div_link span:nth-child(1) {
                                background-color: #f7af05;
                                margin-top: 50px;
                                margin-bottom: 20px;
                            }

                            .div_link span:nth-child(2) {
                                background-color: #00b14f;
                            }

                        .active > h3 {
                            background: #00b14f !important;
                            font-weight: bold !important;
                        }

                        .danhsach-form .bd-text-lv:hover .div_link {
                            display: block;
                        }
                    </style>


                    <div>
                        <asp:Panel ID="pnlLeftMenu" runat="server">
                            <ul>
                                <asp:Repeater ID="rptLeftCategory" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <%--<img src="<%#Eval("PathIMG") %>" />--%>
                                            <h3>
                                                <a href='<%# string.Format("{0}{1}",SiteRoot, Eval("Description")) %>' title='<%#Eval("Name") %>'>
                                                    <%#Eval("Name") %></a>
                                            </h3>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </asp:Panel>
                    </div>

                    <asp:Panel runat="server" ID="pnlContentArticle">
                        <div class="danhsach-form">
                            <div class="row">
                                <asp:Repeater ID="rptLinhVuc" runat="server">
                                    <ItemTemplate>
                                        <div class="col-sm-4">
                                            <div class="bd-text-lv">
                                                <a title="<%# Eval("SiteName") %>" href="<%# string.Format("{0}/{1}/home",SiteRoot,Eval("UrlSiteMap")) %>">
                                                    <img src="<%#Eval("PathIMG") %>" width="100%" />
                                                    <div class="tieude-lv"><%# Eval("SiteName") %> </div>
                                                </a>
                                                <asp:Panel ID="pnl" runat="server" Visible="<%# Request.IsAuthenticated %>">
                                                    <div class="div_link">
                                                        <span class="create-cv-button">
                                                            <a href='<%# string.Format("/GlobalModule/Report/Detail.aspx?reportid={0}", Eval("SiteID"))%>'>
                                                                <i class="fa fa-pie-chart" aria-hidden="true"></i>&nbsp; Xem báo cáo thống kê
                                                            </a>
                                                        </span>
                                                        <span class="create-cv-button">
                                                            <a href='<%# string.Format("/{0}/home",Eval("UrlSiteMap"))%>'>
                                                                <i class="fa fa-link" aria-hidden="true"></i>&nbsp; Truy cập website
                                                            </a>
                                                        </span>
                                                    </div>
                                                </asp:Panel>
                                                <div class="folder-link-lv">
                                                    <i class="fa fa-folder-o"><span><%# Eval("LinhVucName") %></span></i>
                                                    <i class="fa fa-clock-o"><span><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></span></i>
                                                </div>

                                                <div class="noidung-lv">
                                                    <%#Eval("NoiDungDieuTra") %>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Panel ID="pnlLinhVucPager" runat="server" CssClass="blogpager">
                                    <portal:mojoCutePager ID="pgr" runat="server" />
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>




                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared">
            </portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

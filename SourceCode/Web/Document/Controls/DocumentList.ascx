<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DocumentList.ascx.cs" Inherits="DocumentFeature.UI.DocumentList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:HeadingControl ID="heading" runat="server" />
<style>
    .CountLinhVuc ul {
        list-style: none;
    }

        .CountLinhVuc ul li {
            width: 33%;
            float: left;
        }

    .forums table > tbody > tr > td, .forums table > tbody > tr > th, .forums table > tfoot > tr > td, .forums table > tfoot > tr > th, .forums table > thead > tr > td, .forums table > thead > tr > th, .forumview table > tbody > tr > td, .forumview table > tbody > tr > th, .forumview table > tfoot > tr > td, .forumview table > tfoot > tr > th, .forumview table > thead > tr > td, .forumview table > thead > tr > th, .table-condensed > tbody > tr > td, .table-condensed > tbody > tr > th, .table-condensed > tfoot > tr > td, .table-condensed > tfoot > tr > th, .table-condensed > thead > tr > td, .table-condensed > thead > tr > th {
        padding: 12px;
    }

    #ctl00_divCenter {
        padding-left: 5px;
    }

    @media screen and (max-width: 991px) and (min-width: 320px) {
        .only__document {
            display: flex;
            flex-direction: column-reverse;
        }
    }

    h3.moduletitle {
        font-size: 22px;
        text-transform: uppercase;
        color: #0072c4;
        text-shadow: none;
        font-weight: bold;
        font-family: Roboto;
    }

    #tbl_document a:hover {
        color: red !important;
        text-decoration: underline;
    }

    .center {
        text-align: center;
    }

    .vanban-css select, .vanban-css .search-control input {
        width: 100% !important;
    }

    .panel-collapse {
        border: 0;
    }

    .a-search:hover, .a-search:active {
        color: white !important
    }
</style>
<div id="document__page">
    <div class="" style="margin: 0">
        <%--<div class="panel-heading">
            <div>Tìm kiếm văn bản</div>
        </div>--%>
        <%--    <div class="CountLinhVuc">
        <ul>
            <asp:Repeater ID="rptCountLinhVuc" runat="server">
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="lnkLinhVuc" CssClass="doc_link_search" runat="server" NavigateUrl='<%#Eval("Url") %>'><%#Eval("Name") %><%#Eval("Count") %></asp:HyperLink>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>--%>

        <div class="vanban-css">
      
            <div class="well search-from" style="float: left; width: 100%">
                <div id="collapseDiv" aria-expanded="false" class="collapse" role="tabpanel" style="height: auto;">
                    <div class="search-item col-sm-6">
                        <label class="search-label2">Lĩnh vực</label>
                        <div class="search-control">
                            <asp:DropDownList ID="ddlLinhVuc" runat="server" CssClass="dlldata"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="search-item col-sm-6">
                        <mp:SiteLabel ID="lblLoai" runat="server" ForControl="ddlLoaiVB" CssClass="search-label2"
                            ConfigKey="LoaiVBHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                        <div class="search-control">
                            <asp:DropDownList ID="ddlLoaiVB" runat="server" CssClass="dlldata"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="search-item col-sm-6">
                        <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="ddlCoQuan" CssClass="search-label2"
                            ConfigKey="CoQuanIDHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                        <div class="search-control">
                            <asp:DropDownList ID="ddlCoQuan" runat="server" CssClass="dlldata"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="search-item col-sm-6">
                        <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="ddlNam" CssClass="search-label2"
                            ConfigKey="NamLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                        <div class="search-control">
                            <asp:DropDownList CssClass="dlldata" ID="ddlNam" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="search-item col-sm-6 fix_vanban-label">
                    <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label2"
                        ConfigKey="KeywordLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                    <div class="search-control">
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="txtdatasearch"></asp:TextBox>
                    </div>
                </div>
                <div class="search-item col-sm-6" style="margin-top: 30px;">
                    <portal:mojoButton ID="btnSearch" SkinID="SearchButton" runat="server" />
                    &nbsp;&nbsp;
                <portal:mojoButton ID="btnReset" SkinID="ResetButton" runat="server" Visible="false" />
                <a href="#collapseDiv" class="btn btn-primary a-search" style="margin-left: 10px" aria-controls="collapsePanel" data-toggle="collapse" role="button">
                <i class="fa fa-search"></i><span>&nbsp;Tìm kiếm nâng cao</span>
            </a>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function ShowSearch() {
            if ($(".hide-item").hasClass("hide")) {
                $(".hide-item").removeClass("hide");
            } else {
                $(".hide-item").addClass("hide");
            }
        }
    </script>
    <div class="module">
        <div class="module-table-body">
            <div class="width100 font-bold text-right fix_title-vanban">
                <asp:Literal ID="lblTotalVanBan" runat="server"></asp:Literal>
            </div>
            <div class="table__vanban">
                <asp:Repeater ID="rptArticles" runat="server" SkinID="Article">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered table-hover table-condensed" id="tbl_document" style="width: 100%">
                            <tr style="background: #439bf1; color: white">
                                <%--<th>STT</th>--%>
                                <th class="tbl-header center">Số ký hiệu
                                </th>
                                <th class="tbl-header center">Cơ quan ban hành
                                </th>
                                <th class="tbl-header center">Loại văn bản
                                </th>
                                <th class="tbl-header center">Lĩnh vực</th>
                                <th class="center">Trích yếu</th>
                                <th class="center">Ngày ban hành</th>
                                <th></th>
                            </tr>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%--                    <td>
                        <%#rptArticles.Items.Count +1%>
                    </td>--%>
                            <td>
                                <%#Eval("Sign") %>
                            </td>
                            <td><%#Eval("CoQuanName") %></td>
                            <td><%#Eval("LoaiVBName") %></td>
                            <td><%#Eval("LinhVucName") %></td>
                            <%--<%#DocumentUltils.FormatBlogTitleUrl(siteSettings.SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>--%>
                            <td>
                                <%--<asp:HyperLink ID="lnkDetail" runat="server" CssClass="doc_detail" EnableViewState="false" Visible="true" NavigateUrl='/document/viewpost.aspx?itemid=<%#Eval("ItemID") %>'><%#formatContent(Eval("Summary").ToString()) %></asp:HyperLink>--%>
                                <a href="<%#string.Format("{0}/document/detail.aspx?pageid={1}&item={2}&mid={3}",siteSettings.SiteRoot,PageId,Eval("ItemID"),ModuleId) %>" title="<%#Eval("Summary") %>"><%#formatContent(Eval("Summary").ToString()) %></a>
                            </td>
                            <td><%#  string.Format("{0:dd/MM/yyyy}",Eval("DatePromulgate"))%></td>
                            <td>
                                <a href='<%#DownloadFile(Eval("FilePath").ToString()) %>' style='<%# Eval("FilePath") == null ? "display:none": string.Empty %>' download title="Tải xuống">
                                    <i class="fa fa-download" aria-hidden="true"></i>
                                </a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <asp:Panel ID="pnlArticlePager" runat="server" CssClass="ArticlePager">
                <portal:mojoCutePager ID="pgrArticle" runat="server" />
            </asp:Panel>
        </div>
    </div>
</div>

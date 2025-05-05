<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RecentList.ascx.cs" Inherits="LichCongTacFeature.UI.RecentList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .slide-view {
        padding-top: 10px;
        text-align: right;
    }

        .slide-view a {
            color: #dc0204;
            font-weight: bold;
            font-family: Arial;
        }

    .tablesorter tr td:first-child {
        width: 7%;
    }

    .tablesorter tr td p {
        margin: 12px 0;
        line-height: 20px;
        text-align: center !important;
    }

    .module {
        background-color: white;
    }

    table.tablesorter {
        width: 100%;
    }

    .tablesorter td table {
        width: 100% !important;
        border-color: rgba(128, 128, 128, 0.19);
    }

    .tablesorter td {
        border: 0;
        border-color: rgba(128, 128, 128, 0.19);
    }
</style>
<div class="slide-view hide">
    <asp:HyperLink ID="hplSlide" runat="server" ToolTip="Xem dưới dạng trình chiếu" Text="Xem dưới dạng trình chiếu"></asp:HyperLink>
</div>
<div class="module">
    <div class="module-table-body">
        <div class="bxSearch">
            <div class="bedate">
                <asp:Label ID="lblbedate" runat="server"></asp:Label>
            </div>
            <div class="week">
                <asp:HyperLink ID="lnkPre" runat="server" ToolTip="Quay lại tuần trước" Text="<"></asp:HyperLink>
                <asp:Label ID="lblweek" runat="server"></asp:Label>
                <asp:HyperLink ID="lnkNext" runat="server" ToolTip="Tuần tiếp theo" Text=">"></asp:HyperLink>
            </div>
        </div>
        <div class="day" id="divDay">
            <a href="javascript:void(0)" id='all' class="lctactive" style="padding: 0px 20px; font-size: 15px; font-weight: bold;">Cả tuần</a>
            <asp:Repeater ID="rptDay" runat="server">
                <ItemTemplate>
                    <a href="javascript:void(0)" id='<%#Eval("Key") %>' style="padding: 0px 20px; font-size: 15px; font-weight: bold;"><%#Eval("Value") %></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="ndLichCongTac" id="contentall" style="display: block;">
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <%--                    <div class="bxSearchAll" style="text-align: center;">
                        <div style="padding-top: 7px; color: #FFF;"><%#Eval("Value") %></div>
                    </div>--%>
                    <div class="cleared"></div>
                    <div class="ndLichCongTacAll" style="display: block;">
                        <asp:Repeater ID="rptAll" runat="server" DataSource='<%#BindDocument(Convert.ToInt32(Eval("Key"))) %>'>
                            <HeaderTemplate>
                                <%--                                <table class="tablesorter">
                                    <tr>
                                        <th colspan="2" class="bxSearchAll">
                                            <div style="text-align: center;">
                                                <div style="padding-top: 7px; color: #FFF;"><%#Eval("Thu") %>, <%#Eval("StartDate") %></div>
                                            </div>
                                        </th>
                                    </tr>--%>
                                <%--                                    <tr>
                                        <th style="width: 25%" class="tbl-header">
                                            <%#Resources.LichCongTacResources.DateLabel %>
                                        </th>
                                        <th style="width: 75%" class="tbl-header">
                                            <%#Resources.LichCongTacResources.SummaryLabel %>
                                        </th>
                                    </tr>--%>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="tablesorter">
                                    <tr runat="server" visible='<%#string.IsNullOrEmpty(Eval("BuoiSang").ToString())? false:true%>'>
                                        <th colspan="2" class="bxSearchAll" style="background-color: white">
                                            <div style="text-align: left;">
                                                <div style="color: red; line-height: 20px; font-size: 16px; font-family: Arial; font-weight: bold;">
                                                    <%#Eval("Thu") %>,        Ngày <%#Convert.ToDateTime(Eval("StartDate")).ToString("dd/MM/yyyy") %>
                                                </div>
                                            </div>
                                        </th>
                                    </tr>
                                    <tr runat="server" visible='<%#string.IsNullOrEmpty(Eval("BuoiSang").ToString())? false:true%>'>
                                        <td><%#string.IsNullOrEmpty(Eval("BuoiSang").ToString())?"<span style='color:#f0ad4e'>Nội dung đang được cập nhật...</span>":Eval("BuoiSang") %></td>
                                    </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>          
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="cleared"></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="ndLichCongTac" id="<%# "content"+ Eval("Key").ToString()%>">
                    <asp:Repeater ID="rptArticles" DataSource='<%#BindDocument(Convert.ToInt32(Eval("Key"))) %>' runat="server" SkinID="Article">
                        <HeaderTemplate>
                            <%--                            <table class="tablesorter" id="myTable">
                                <tr>
                                    <th colspan="2" class="bxSearchAll">
                                        <div style="text-align: center;">
                                            <div style="padding-top: 7px; color: #FFF;"><%#Eval("Thu") %>, <%#Eval("StartDate") %></div>
                                        </div>
                                    </th>
                                </tr>--%>
                            <%--                                <tr>
                                    <th style="width: 25%" class="tbl-header">
                                        <%#Resources.LichCongTacResources.DateLabel %>
                                    </th>
                                    <th style="width: 75%" class="tbl-header">
                                        <%#Resources.LichCongTacResources.SummaryLabel %>
                                    </th>
                                </tr>--%>
                            <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table class="tablesorter" id="myTable">
                                <tr>
                                    <th colspan="2" class="bxSearchAll" style="background-color: white">
                                        <div style="text-align: left;">
                                            <div style="color: red; line-height: 20px; font-size: 16px; font-family: Arial; font-weight: bold;">
                                                <%#Eval("Thu") %>, Ngày <%#Convert.ToDateTime(Eval("StartDate")).ToString("dd/MM/yyyy") %>
                                            </div>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td><%#string.IsNullOrEmpty(Eval("BuoiSang").ToString())?"<span style='color:#f0ad4e'>Nội dung đang được cập nhật...</span>":Eval("BuoiSang") %></td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                </table>
            
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlArticlePager" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrArticle" runat="server" />
        </asp:Panel>
    </div>
</div>
<script>
    $(function () {
        $("#divDay a").click(function () {
            $("#divDay a").removeClass("lctactive");
            $(this).addClass("lctactive");
            $(".ndLichCongTac").hide();
            $("#content" + $(this).attr("id")).show();
        })
    });

</script>
<style>
    .search-box {
        width: 100%;
        float: left;
    }

    .searchSubmit {
        float: left;
        margin-top: 15px;
    }

    .search-item {
        float: left;
        width: 45%;
        margin-right: 5%;
        margin-bottom: 5px;
    }

    .search-label {
        float: left;
        min-width: 150px;
        display: block;
    }

    .article-title a {
        font-size: 13px;
        color: #0a8acb;
        font-weight: bold;
        text-decoration: none;
    }

        .article-title a:hover {
            text-decoration: underline;
        }

    .author {
        font-size: 11px;
        color: #C0C0C0;
        font-style: itali
        font-w iht: b;
    }


    tabl, caption y tr, th, td {
        er: 0 none;
        s u one;
        -align: baseli e;
        .bot om-sp;

    {
        in-bottom: 20px;
    }


    d float margi -b widt div. odule d e-body floa pding: widt : 100 v.module t ble {
        px solid #d9d9d9;
        0px;
        width: 100%;
        div .module table th;

    {
        lor: # eeeee;
        border-b ttom 9;
        */ r #d9d9d9;
        */ p dding: 5 : left;
        iv .module table t :hover;

    {
        backgro nd-olor: #e7f6fa;
        l d-color: #f ffff;
        b rder: 1px s d9;
        p: .align-center;

    {
        gn: center;
    }

    d {
        text d e;
    }


    bor er-co l border-spacing: 0
    }


    t b text- lign: left; width: 100%; table.tablesorter head r .tbl- background image: url /articl -icon/bg ound-position: ri ht ce te; background-repeat no-r ursor: p
    }


    sorter tbody td {
        : #fff;
        color # d padding: 4px i;
    }

    able.tablesor e {
        background-color: #f1f5fa;
    }


    a tbl-thead tr .hea erSortUp a l("/Data/SiteImag s/article-icon/asc.gif");
    }

    e -thead tr .header ortDown {
        ba kground-image: ur ("/Data/S te mages/article i;;
    }

    o -thead tr .head r table.table orte tbl-tead tr .ederSortU ackgr : #dddddd; div toolb ack ne rep at s 0 fb; op: 10 x; m ttom f oat: pa di wi th: 96% .tool-btn {
        float: r float: right;
    }

    .clr {
        clear: both;
        height: 0;
        overflow: hidden;
    }
</style>

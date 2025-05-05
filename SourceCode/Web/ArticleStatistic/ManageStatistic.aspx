<%@ Page Language="c#" CodeBehind="ManageStatistic.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" Inherits="ArticleFeature.UI.ManageStatistic" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <portal:AdminCrumbContainer ID="pnlAdminCrumbs" runat="server" CssClass="breadcrumbs">
                <asp:HyperLink ID="lnkAdminMenu" runat="server" NavigateUrl="~/Admin/AdminMenu.aspx" />
                <portal:AdminCrumbSeparator ID="litLinkSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
                <asp:HyperLink ID="lnkAdminArtile" runat="server" CssClass="breadcrumbs" NavigateUrl="~/Admin/Article/ArticleMenu.aspx" />
                <portal:AdminCrumbSeparator ID="AdminCrumbSeparator1" runat="server" Text="&nbsp;&gt;" EnableViewState="false" />
                <asp:HyperLink ID="lnkCurrentPage" runat="server" CssClass="selectedcrumb" />
            </portal:AdminCrumbContainer>
            <portal:HeadingControl ID="heading" runat="server" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
                <script src="/Data/js/bootstrap-multiselect/bootstrap-multiselect.js"></script>
                <link rel="stylesheet" href="/Data/js/bootstrap-multiselect/bootstrap-multiselect.css" />
                <link href="../Data/js/bootstrap-datepicker-1.6.4-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
                <script src="../Data/js/bootstrap-datepicker-1.6.4-dist/js/bootstrap-datepicker.min.js"></script>
                <script src="/Data/js/chartjs/Chart.bundle.min.js"></script>
                <script src="/Data/js/chartjs/utils.js"></script>
                <style>
                    .input-group {
                        width: 97%;
                    }

                        .input-group input[type=text] {
                            min-height: 34px;
                        }

                    .settingrow select {
                        border-radius: 0;
                    }

                    .btn {
                        background-image: none;
                    }

                    .pdt15 {
                        padding-top: 25px;
                    }

                    .search {
                        margin-bottom: 30px;
                    }

                    #ctl00_mainContent_ddlYear.form-control {
                        vertical-align: middle;
                        margin-right: 60px;
                        float: left;
                    }

                    #ctl00_mainContent_ddlMonth.form-control {
                        width: 150px;
                        float: left;
                    }

                    span.btn.btn-primary3 {
                        border-radius: 0;
                        color: #fff;
                        background-color: #337ab7;
                        border-color: #2e6da4;
                        margin-left: -13%;
                    }

                    span.btn.btn-primary2 {
                        border-radius: 0;
                        color: #fff;
                        background-color: #337ab7;
                        border-color: #2e6da4;
                        margin-left: -93%;
                        margin-top: 14%;
                    }

                    .row25 col-sm-6 pd0 dropdownThongke {
                        width: 41%;
                    }

                    #ctl00_mainContent_pnlContainer .tab-content .row20 {
                        width: 50%;
                    }

                    /* display: inline-flex;*/



                    #ctl00_mainContent_ddlMonth.form-control1 {
                        margin-left: -12%;
                        margin-top: 0%;
                    }



                    .search1 {
                        display: flex;
                        align-items: center;
                        flex-wrap: wrap;
                        margin-top: 30px;
                    }

                        .search1 label {
                            margin-top: 10px;
                        }

                    span.btn.btn-primary1 {
                        border-radius: 0;
                        margin-left: -20%;
                        border-radius: 0;
                        color: #fff;
                        background-color: #337ab7;
                        margin-top: -4%;
                    }


                    #startDate3.form-control date width2000 {
                        width: 399px;
                        vertical-align: middle;
                        margin-left: -14%;
                        vertical-align: middle;
                        margin-left: 0%;
                    }
                </style>
                <div class="statisticForm">
                    <div class="settingrow">
                        <!-- Nav tabs -->
                        <div class="tabbable-panel">
                            <div class="tabbable-line">
                                <ul class="nav nav-tabs ">
                                    <li class="active">
                                        <a href="#tab_default_1" data-toggle="tab">Thống kê theo tháng, năm </a>
                                    </li>
                                  <%--  <li>
                                        <a href="#tab_default_2" data-toggle="tab">Thống kê theo chuyên mục tin bài</a>
                                    </li>
                                    <li>
                                        <a href="#tab_default_3" data-toggle="tab">Thống kê theo tác giả</a>
                                    </li>
                                    <li>
                                        <a href="#tab_default_4" data-toggle="tab">Thống kê theo site</a>
                                    </li>--%>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active fade in" id="tab_default_1">
                                        <div class="search1">
                                            <div class="col-md-4">
                                                <label class="col-sm-4">Năm</label>
                                                <asp:DropDownList ID="ddlYear" Width="150" CssClass="form-control1" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label class="col-sm-4">Tháng</label>
                                                <asp:DropDownList ID="ddlMonth" Width="150" mutiple CssClass="form-control1" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="row25 pd0 dropdownThongke">
                                                <label>Danh sách website</label>
                                                <asp:ListBox ID="lboxSiteTab_1" cssClaass="form-control" runat="server"></asp:ListBox>
                                            </div>
                                            <div class="row20 pdt15">
                                                <label>&nbsp;</label>
                                                <span class="btn btn-primary1" onclick="search_statistic_tab1(); return false;" style="border-radius: 0">Thống kê</span>
                                            </div>
                                        </div>
                                        <div id="container" style="width: 100%;">
                                            <canvas id="canvas"></canvas>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_default_2">
                                        <div class="search">
                                            <div class="row25 col-sm-6 pd0">
                                                <label>Từ ngày</label>
                                                <input type="text" class="form-control date width2000" name="date" id="startDate2" />
                                                <%-- <span class="input-group-addon add-on addcontrol">
                                                             <i class="fa fa-calendar" aria-hidden="true"></i>
                                                     </span>--%>
                                            </div>
                                            <div class="row25 col-sm-6 pd0">
                                                <label>Đến ngày</label>
                                                <input type="text" class="form-control date width2000" name="date" id="endDate2" />
                                                <%-- <span class="input-group-addon add-on addcontrol">
                                                         <i class="fa fa-calendar" aria-hidden="true"></i>
                                                     </span>--%>
                                            </div>

                                            <div class="row25 col-sm-6 pd0 dropdownThongke">
                                                <label>Danh sách website</label>
                                                <asp:ListBox ID="lboxSiteTab_2" cssClaass="form-control" AutoPostBack="true" runat="server"></asp:ListBox>
                                            </div>
                                            <div class="row40 col-sm-6 pd0 dropdownThongke">
                                                <label>Danh mục</label>
                                                <asp:UpdatePanel ID="pnlUpdateCategoryTab_2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:ListBox ID="lboxCategoryTab_2" cssClaass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lboxSiteTab_2" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>


                                            <div class="row10">
                                                <label>&nbsp;</label>
                                                <span class="btn btn-primary2" onclick="search_statisticTab2(); return false;" style="border-radius: 0">Thống kê</span>
                                            </div>
                                        </div>
                                        <div id="containerTab2" style="width: 100%;">
                                            <canvas id="canvasTab2"></canvas>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_default_3">
                                        <div class="search">
                                            <div class="row25 col-sm-6 pd0">
                                                <label>Từ ngày</label>
                                                <input type="text" class="form-control date width2000" name="date" id="startDate3" />
                                                <%-- <span class="input-group-addon add-on addcontrol">
                                                             <i class="fa fa-calendar" aria-hidden="true"></i>
                                                     </span>--%>
                                            </div>
                                            <div class="row25 col-sm-6 pd0">
                                                <label>Đến ngày</label>
                                                <input type="text" class="form-control date width2000" name="date" id="endDate3" />
                                                <%-- <span class="input-group-addon add-on addcontrol">
                                                         <i class="fa fa-calendar" aria-hidden="true"></i>
                                                     </span>--%>
                                            </div>
                                            <div class="row25 col-sm-6 pd0 dropdownThongke">
                                                <label>Danh sách website</label>
                                                <asp:ListBox ID="lboxSiteTab_3" cssClaass="form-control" runat="server" AutoPostBack="true"></asp:ListBox>
                                            </div>
                                            <div class="row25 col-sm-6 pd0 dropdownThongke">
                                                <label>Tác giả</label>
                                                <asp:UpdatePanel ID="pnlUpdateCategoryTab_3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:ListBox ID="lboxAuthorTab_3" SelectionMode="Multiple" cssClaass="form-control" runat="server"></asp:ListBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lboxSiteTab_3" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="row10 wf100">
                                                <label>&nbsp;</label>
                                                <span class="btn btn-primary3" onclick="search_statistic_tab3(); return false;" style="border-radius: 0">Thống kê</span>
                                            </div>
                                        </div>
                                        <div id="containerTab3" style="width: 100%;">
                                            <canvas id="canvasTab3"></canvas>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tab_default_4">
                                        <div class="search">
                                            <div class="row25 col-sm-6 pd0">
                                                <label>Từ ngày</label>
                                                <input type="text" class="form-control date width2000" name="date" id="startDate4" />
                                                <%-- <span class="input-group-addon add-on addcontrol">
                                                             <i class="fa fa-calendar" aria-hidden="true"></i>
                                                     </span>--%>
                                            </div>
                                            <div class="row25 col-sm-6 pd0">
                                                <label>Đến ngày</label>
                                                <input type="text" class="form-control date width2000" name="date" id="endDate4" />
                                                <%-- <span class="input-group-addon add-on addcontrol">
                                                         <i class="fa fa-calendar" aria-hidden="true"></i>
                                                     </span>--%>
                                            </div>
                                            <div class="row25 col-sm-6 pd0 dropdownThongke hidden">
                                                <label>Danh mục</label>
                                                <asp:ListBox ID="lboxCategoryTab_4" mutiple cssClaass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                            </div>
                                            <div class="row25 col-sm-6 pd0 dropdownThongke hidden">
                                                <label>Tác giả</label>
                                                <asp:ListBox ID="lboxAuthorTab_4" cssClaass="form-control" runat="server"></asp:ListBox>
                                            </div>
                                            <div class="row25 col-sm-6 pd0 dropdownThongke">
                                                <label>Danh sách website</label>
                                                <asp:ListBox ID="lboxSiteTab_4" cssClaass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                            </div>
                                            <div class=" row20 pdt15">
                                                <label>&nbsp;</label>
                                                <span class="btn btn-primary" onclick="search_statistic_tab4(); return false;" style="border-radius: 0">Thống kê</span>
                                            </div>
                                        </div>
                                        <div id="containerTab4" style="width: 100%;">
                                            <canvas id="canvasTab4"></canvas>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    var result = "abc";

                    $(document).ready(function () {
                        $('.date')
                            .datepicker({
                                format: 'dd/mm/yyyy',
                                startDate: '01/01/2010',
                                endDate: '12/30/2050'
                            });
                        //.on('changeDate', function (e) {
                        //    // Revalidate the date field
                        //    $('#dateRangeForm').formValidation('revalidateField', 'date');
                        //});
                        ReloadListBox();
                        //var d = new Date();
                        //var month = d.getMonth() + 1;
                        //var obj_param = { year: d.getFullYear(), month: month, categories: "", userGuid: "", siteid: "" };
                        //get_statistic(obj_param);
                        search_statistic_tab1();
                    });
                    function search_statisticTab2() {
                        var category = "";
                        var siteId = "";
                        var userGuids = "";
                        var siteId = $("#<%=lboxSiteTab_2.ClientID%>").val();

                        $.each($("#<%=lboxCategoryTab_2.ClientID%>").val(), function (key, value) {
                            category += value + ",";
                        });
                        var obj_param = {};
                        obj_param.startDate = $("#startDate2").val();
                        obj_param.endDate = $("#endDate2").val();
                        obj_param.siteid = siteId;
                        obj_param.userGuid = userGuids;
                        obj_param.categories = category;
                        $.ajax({
                            type: "POST",
                            cache: false,
                            data: JSON.stringify(obj_param),
                            url: "/ArticleStatistic/ManageStatistic.aspx/ArticaleTotalCountForCatalogs",
                            contentType: "application/json; charset= utf-8",
                            dataType: "json",
                            async: false,
                            success: successTab2_
                        });
                    }
                    function search_statistic_tab3() {
                        var userGuids = $("#<%=lboxAuthorTab_3.ClientID%>").val().toString();
                      <%--  $.each($("#<%=lboxAuthorTab_3.ClientID%>").val(), function (key, value) {
                            userGuids += value + ",";
                        });--%>
                        var siteId = $("#<%=lboxSiteTab_3.ClientID%>").val();
                        var obj_param = {};
                        obj_param.startDate = $("#startDate3").val();
                        obj_param.endDate = $("#endDate3").val();
                        obj_param.userGuid = userGuids;
                        obj_param.siteid = siteId;

                        $.ajax({
                            type: "POST",
                            cache: false,
                            data: JSON.stringify(obj_param),
                            url: "/ArticleStatistic/ManageStatistic.aspx/ArticaleTotalCountForAuthor",
                            contentType: "application/json; charset= utf-8",
                            dataType: "json",
                            async: false,
                            success: successTab3_
                        });
                    }

                    function successTab3_(reponse) {
                        var container = document.getElementById("containerTab3");
                        container.innerHTML = "";
                        container.innerHTML = "<canvas id='canvasTab3'></canvas>";
                        var iData = reponse.d;
                        var labels = iData[0];
                        var dataset = iData[1];
                        var title = iData[2];
                        var color = Chart.helpers.color;
                        var barChartData = {
                            labels: labels,
                            datasets: [{
                                label: "Số tin bài",
                                backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                                borderColor: window.chartColors.red,
                                borderWidth: 1,
                                data: dataset
                            }]

                        };

                        var ctx = document.getElementById("canvasTab3").getContext("2d");
                        window.myBar = new Chart(ctx, {
                            type: 'bar',
                            data: barChartData,
                            options: {
                                responsive: true,
                                scales: {
                                    xAxes: [{
                                        stacked: false,
                                        beginAtZero: true,
                                        scaleLabel: {
                                            labelString: title
                                        },
                                        ticks: {
                                            stepSize: 1,
                                            min: 0,
                                            autoSkip: false
                                        }
                                    }]
                                },
                                legend: {
                                    position: 'top',
                                },
                                title: {
                                    display: true,
                                    text: title
                                }
                            }
                        });
                    }

                    //-----------Tab 4 thống kê tất cả--------------
                    function search_statistic_tab4() {
                        var siteId = $("#<%=lboxSiteTab_4.ClientID%>").val().toString();
                        var obj_param = {};
                        obj_param.startDate = $("#startDate4").val();
                        obj_param.endDate = $("#endDate4").val();
                        obj_param.siteid = siteId;
                        $.ajax({
                            type: "POST",
                            cache: false,
                            data: JSON.stringify(obj_param),
                            url: "/ArticleStatistic/ManageStatistic.aspx/ArticaleTotalCountForSite",
                            contentType: "application/json; charset= utf-8",
                            dataType: "json",
                            async: false,
                            success: successTab4_
                        });
                    }

                    function successTab4_(reponse) {
                        var container = document.getElementById("containerTab4");
                        container.innerHTML = "";
                        container.innerHTML = "<canvas id='canvasTab4'></canvas>";
                        var iData = reponse.d;
                        var labels = iData[0];
                        var dataset = iData[1];
                        var title = iData[2];
                        var color = Chart.helpers.color;
                        var barChartData = {
                            labels: labels,
                            datasets: [{
                                label: "Số tin bài",
                                backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                                borderColor: window.chartColors.red,
                                borderWidth: 1,
                                data: dataset
                            }]

                        };

                        var ctx = document.getElementById("canvasTab4").getContext("2d");
                        window.myBar = new Chart(ctx, {
                            type: 'bar',
                            data: barChartData,
                            options: {
                                responsive: true,
                                scales: {
                                    xAxes: [{
                                        stacked: false,
                                        beginAtZero: true,
                                        scaleLabel: {
                                            labelString: title
                                        },
                                        ticks: {
                                            stepSize: 1,
                                            min: 0,
                                            autoSkip: false
                                        }
                                    }]
                                },
                                legend: {
                                    position: 'top',
                                },
                                title: {
                                    display: true,
                                    text: title
                                }
                            }
                        });
                    }

                    function search_statistic_tab1() {
                        console.log("StaticArticleTab1");
                        var siteId = $("#<%=lboxSiteTab_1.ClientID%>").val().toString();

                        var obj_param = {};
                        obj_param.year = $("#<%=ddlYear.ClientID%>").val();
                        obj_param.month = $("#<%=ddlMonth.ClientID%>").val();
                        obj_param.siteid = siteId;
                        obj_param.categories = "";

                        $.ajax({
                            type: "POST",
                            cache: false,
                            data: JSON.stringify(obj_param),
                            url: "/ArticleStatistic/ManageStatistic.aspx/StaticArticleTab1",
                            contentType: "application/json; charset= utf-8",
                            dataType: "json",
                            async: false,
                            success: success_
                        });
                    }

                    function successTab2_(reponse) {
                        var container = document.getElementById("containerTab2");
                        container.innerHTML = "";
                        container.innerHTML = "<canvas id='canvasTab2'></canvas>";
                        var iData = reponse.d;
                        var labels = iData[0];
                        var dataset = iData[1];
                        var title = iData[2];
                        var color = Chart.helpers.color;
                        var barChartData = {
                            labels: labels,
                            datasets: [{
                                label: "Số tin bài",
                                backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                                borderColor: window.chartColors.red,
                                borderWidth: 1,
                                data: dataset
                            }]

                        };

                        var ctx = document.getElementById("canvasTab2").getContext("2d");
                        window.myBar = new Chart(ctx, {
                            type: 'bar',
                            data: barChartData,
                            options: {
                                responsive: true,
                                scales: {
                                    xAxes: [{
                                        stacked: false,
                                        beginAtZero: true,
                                        scaleLabel: {
                                            labelString: title
                                        },
                                        ticks: {
                                            stepSize: 1,
                                            min: 0,
                                            autoSkip: false
                                        }
                                    }]
                                },
                                legend: {
                                    position: 'top',
                                },
                                title: {
                                    display: true,
                                    text: title
                                }
                            }
                        });
                    }
                    function success_(reponse) {
                        var container = document.getElementById("container");
                        container.innerHTML = "";
                        container.innerHTML = "<canvas id='canvas'></canvas>";
                        var iData = reponse.d;
                        var labels = iData[0];
                        var dataset = iData[1];
                        var title = iData[2];
                        var color = Chart.helpers.color;
                        var barChartData = {
                            labels: labels,
                            datasets: [{
                                label: "Số tin bài",
                                backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                                borderColor: window.chartColors.red,
                                borderWidth: 1,
                                data: dataset
                            }]

                        };

                        var ctx = document.getElementById("canvas").getContext("2d");
                        window.myBar = new Chart(ctx, {
                            type: 'bar',
                            data: barChartData,
                            options: {
                                responsive: true,
                                scales: {
                                    xAxes: [{
                                        stacked: false,
                                        beginAtZero: true,
                                        scaleLabel: {
                                            labelString: title
                                        },
                                        ticks: {
                                            stepSize: 1,
                                            min: 0,
                                            autoSkip: false
                                        }
                                    }]
                                },
                                legend: {
                                    position: 'top',
                                },
                                title: {
                                    display: true,
                                    text: title
                                }
                            }
                        });
                    }
                    function ReloadListBox() {
                        $('#<%=lboxCategoryTab_2.ClientID%>').multiselect({
                            nonSelectedText: 'Chọn danh mục',
                            enableFiltering: true,
                            includeSelectAllOption: true,
                            selectAllName: true,
                            enableCaseInsensitiveFiltering: true,
                            buttonWidth: '400px'
                        });

                        $('#<%=lboxSiteTab_3.ClientID%>').multiselect({
                            nonSelectedText: 'Chọn website',
                            enableFiltering: true,
                            includeSelectAllOption: true,
                            selectAllName: true,
                            enableCaseInsensitiveFiltering: true,
                            buttonWidth: '400px'
                        });
                        $('#<%=lboxSiteTab_2.ClientID%>').multiselect({
                            nonSelectedText: 'Chọn website',
                            enableFiltering: true,
                            includeSelectAllOption: true,
                            selectAllName: true,
                            enableCaseInsensitiveFiltering: true,
                            buttonWidth: '400px'
                        });
                        $('#<%=lboxSiteTab_1.ClientID%>').multiselect({
                            nonSelectedText: 'Chọn website',
                            enableFiltering: true,
                            includeSelectAllOption: true,
                            selectAllName: true,
                            enableCaseInsensitiveFiltering: true,
                            buttonWidth: '400px'
                        });

                        $('#<%=lboxAuthorTab_3.ClientID%>').multiselect({
                            nonSelectedText: 'Chọn tác giả',
                            enableFiltering: true,
                            includeSelectAllOption: true,
                            selectAllName: true,
                            enableCaseInsensitiveFiltering: true,
                            buttonWidth: '400px'
                        });
                        $('#<%=lboxCategoryTab_4.ClientID%>').multiselect({
                            nonSelectedText: 'Chọn danh mục',
                            enableFiltering: true,
                            includeSelectAllOption: true,
                            selectAllName: true,
                            enableCaseInsensitiveFiltering: true,
                            buttonWidth: '400px'
                        });
                        $('#<%=lboxAuthorTab_4.ClientID%>').multiselect({
                            nonSelectedText: 'Chọn tác giả',
                            enableFiltering: true,
                            includeSelectAllOption: true,
                            selectAllName: true,
                            enableCaseInsensitiveFiltering: true,
                            buttonWidth: '400px'
                        });
                        $('#<%=lboxSiteTab_4.ClientID%>').multiselect({
                            nonSelectedText: 'Chọn website',
                            enableFiltering: true,
                            includeSelectAllOption: true,
                            selectAllName: true,
                            enableCaseInsensitiveFiltering: true,
                            buttonWidth: '400px'
                        });
                    }
                </script>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>

</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server">
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RecentSlideList.ascx.cs" Inherits="LichCongTacFeature.UI.RecentSlideList" %>

<%@ Import Namespace="mojoPortal.Features" %>
<link href="../../Data/Sites/1/skins/art42-blue/example.css" rel="stylesheet" />
<%--<link href="../../Data/Sites/1/skins/art42-blue/font-awesome.min.css" rel="stylesheet" />--%>
<style>
    #slides {
        display: none;
    }

        #slides .slidesjs-navigation {
            margin-top: 5px;
        }
         .slidesjs-previous {
        margin-left: 48%;
    }
    a.slidesjs-next,
    a.slidesjs-previous,
    a.slidesjs-play,
    a.slidesjs-stop {
        background-image: url(../Data/Sites/1/skins/art42-blue/images/btns-next-prev.png);
        background-repeat: no-repeat;
        display: block;
        width: 12px;
        height: 18px;
        overflow: hidden;
        text-indent: -9999px;
        float: left;
        margin-right: 5px;
    }

    a.slidesjs-next {
        margin-right: 10px;
        background-position: -12px 0;
    }

    a:hover.slidesjs-next {
        background-position: -12px -18px;
    }

    a.slidesjs-previous {
        background-position: 0 0;
    }

    a:hover.slidesjs-previous {
        background-position: 0 -18px;
    }

    a.slidesjs-play {
        width: 15px;
        background-position: -25px 0;
    }

    a:hover.slidesjs-play {
        background-position: -25px -18px;
    }

    a.slidesjs-stop {
        width: 18px;
        background-position: -41px 0;
    }

    a:hover.slidesjs-stop {
        background-position: -41px -18px;
    }

    .slidesjs-pagination {
        margin: 7px 0 0;
        float: right;
        list-style: none;
    }

        .slidesjs-pagination li {
            float: left;
            margin: 0 1px;
        }

            .slidesjs-pagination li a {
                display: block;
                width: 13px;
                height: 0;
                padding-top: 13px;
                background-image: url(../Data/Sites/1/skins/art42-blue/images/pagination.png);
                background-position: 0 0;
                float: left;
                overflow: hidden;
            }

                .slidesjs-pagination li a.active,
                .slidesjs-pagination li a:hover.active {
                    background-position: 0 -13px;
                }

                .slidesjs-pagination li a:hover {
                    background-position: 0 -26px;
                }

    #slides a:link,
    #slides a:visited {
        color: #333;
    }

    #slides a:hover,
    #slides a:active {
        color: #9e2020;
    }

    .navbar {
        overflow: hidden;
    }
</style>
<!-- End SlidesJS Optional-->

<!-- SlidesJS Required: These styles are required if you'd like a responsive slideshow -->
<style>
    #slides {
        display: none;
    }

    .container {
        margin: 0 auto;
    }

    /* For tablets & smart phones */
    @media (max-width: 767px) {
        body {
            padding-left: 20px;
            padding-right: 20px;
        }

        .container {
            width: auto;
        }
    }

    /* For smartphones */
    @media (max-width: 480px) {
        .container {
            width: auto;
        }
    }

    /* For smaller displays like laptops */
    @media (min-width: 768px) and (max-width: 979px) {
        .container {
            width: 724px;
        }
    }

    /* For larger displays */
    @media (min-width: 1200px) {
        .container {
            width: 99.5%;
        }
    }

    /*.slide-wrapper table tr td {
        border: 1px solid #d9d9d9;
    }*/

    .slide-wrapper table tr td:first-child {
        /*background-color: rgba(245,245,241,1);*/
    }

    .slide-wrapper table tr td div {
        width: 99%;
        border-bottom: 1px solid #e2e2e2;
        /*padding-top: 7px;*/
        margin: 0px 0 9px;
    }

        .slide-wrapper table tr td div:last-child {
            border: none;
        }

    .slide-wrapper table tr th {
        /*background-color: rgba(245,245,241,1);*/
        color: red;
    }

    /*.slide-wrapper table tr th {
        border: 1px solid #d9d9d9;
    }*/

    .container {
        font-family: Arial;
        margin: 0 auto;
        margin-left: 5px;
    }

    .slide-wrapper table tr {
        min-height: 100px;
        height: auto;
    }

    .slides-header {
        text-align: center;
        font-family: Arial;
        font-weight: bold;
        margin-top: 10px;
        margin-bottom: 10px;
        color: rgb(23,96,147);
        font-size: 18px;
    }

    .buttomBack {
        display: inline-block;
        padding: 6px 12px;
        margin-bottom: 0;
        font-size: 14px;
        font-weight: 400;
        line-height: 1.42857143;
        text-align: center;
        white-space: nowrap;
        vertical-align: middle;
        -ms-touch-action: manipulation;
        touch-action: manipulation;
        cursor: pointer;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
        background-image: none;
        border: 1px solid transparent;
        border-radius: 4px;
        color: #fff;
        background-color: #d9534f;
        border-color: #d43f3a;
        -webkit-appearance: button;
        float: right;
        margin-right: 18px;
    }

    .slide-wrapper table tr td div {
        width: 100% !important;
    }

    .slide-wrapper table {
        width: 100% !important;
        border: 0;
        border-collapse: collapse;
    }

        .slide-wrapper table tr th {
            border: 0;
        }

        .slide-wrapper table tr td {
            border: 0;
        }

    p {
        text-align: center !important;
    }
</style>

<div class="slides-header">
    LỊCH CÔNG TÁC TUẦN CỦA BỆNH VIỆN MẮT TRUNG ƯƠNG
    <button class="buttomBack" onclick="window.location='<%=SiteRoot+"/lich-lam-viec" %>'">Quay lại</button>
    <br />
    <span style="font-size: 15px">
        <asp:Label ID="lblweek" runat="server"></asp:Label>
        <asp:Label ID="lblbedate" runat="server"></asp:Label>
    </span>
</div>
<div class="container">
    <div id="slides">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <asp:Repeater ID="rptSlide" DataSource='<%#BindDocument(Convert.ToInt32(Eval("Key"))) %>' runat="server" SkinID="Article">
                    <ItemTemplate>
                        <div class="slide-wrapper">
                            <table style="width: 100%; height: 575px; border-collapse: collapse">
                                <thead>
                                    <tr>
                                        <th style="width: 100%; height: 35px; font-weight: bold;" colspan="2"><%#Eval("Thu") %>, Ngày <%#string.Format("{0:dd/MM/yyyy}", Eval("StartDate")) %> </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top;"><%#Eval("BuoiSang") %></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>



<!-- SlidesJS Required: Link to jquery.slides.js -->
<script src="../../Data/Sites/1/skins/art42-blue/jquery.min.js"></script>
<script src="../../Data/Sites/1/skins/art42-blue/jquery.slides.min.js"></script>
<!-- End SlidesJS Required -->

<!-- SlidesJS Required: Initialize SlidesJS with a jQuery doc ready -->
<script>
    $(document).ready(function () {
        demo();
    });

    function demo() {
        $('#slides').slidesjs({
            width: 575,
            height: $('.slide-wrapper').find('table').height(),
            play: {
                active: true,
                auto: true,
                interval: 20000,
                swap: true
            },
            callback: {
                loaded: function () {
                    var tableHeight = $('.slide-wrapper').find('table').height();
                    $(document).find('.slidesjs-container').css('height', tableHeight + 'px');
                    $(document).find('.slidesjs-control').css('height', tableHeight + 'px');
                }
            }
        });
    }
  </script>
<!-- End SlidesJS Required -->

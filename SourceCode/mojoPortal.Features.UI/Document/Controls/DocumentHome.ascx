<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentHome.ascx.cs" Inherits="DocumentFeature.UI.DocumentHome" %>
<div class="documentHome">
    <div class="box-icon-title">
        Văn bản mới
    </div>
    <div class="box-icon-body">
        <div class="titlebox">
            <h2>Văn bản - Chỉ đạo điều hành</h2>
        </div>

        <div class="block-law marquee" data-direction="up" data-duration="10000" data-pauseonhover="true" data-duplicated="true" style="height: 285px; overflow: hidden;">
            <div class="js-marquee-wrapper" style="margin-top: 0px; animation: marqueeAnimation-62829960 15.3509s linear 0s infinite running;">
                <div class="js-marquee" style="margin-right: 0px; float: none; margin-bottom: 20px;">
                    <asp:Repeater ID="rptDocument" runat="server">
                        <ItemTemplate>
                            <div class="m-bottom item">
                                <h3 class="law-code">
                                    <a href="/laws/detail/Cong-van-so-4622-BGDDT-CNTT-ve-viec-huong-dan-thuc-hien-nhiem-vu-CNTT-nam-hoc-2016-2017-1/" title="Công văn số 4622/BGDĐT-CNTT về việc hướng dẫn thực hiện nhiệm vụ CNTT năm học 2016 – 2017">4622/BGDĐT-CNTT</a>
                                </h3>
                                <p class="law-title">Công văn số 4622/BGDĐT-CNTT về việc hướng dẫn thực hiện nhiệm vụ CNTT năm học 2016 – 2017</p>
                                <em class="text-muted law-view">lượt xem: 9 | lượt tải:2</em>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <style>
                    @-webkit-keyframes marqueeAnimation-62829960 {
                        100% {
                            margin-top: -590px;
                        }
                    }
                </style>
            </div>
        </div>
        <div class="clearfix text-right">
            <a href="/van-ban" class="more">Xem tiếp&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i></a>
        </div>
    </div>
</div>

<style type="text/css">
    .box-icon-title {
        margin: 20px 0;
        line-height: 32px;
        font-size: 20px;
        font-weight: 700;
        padding-left: 50px;
        background: url(../images/bg-national-emblem.png) no-repeat left center;
    }

    .box-icon-body .titlebox {
        margin-bottom: 20px;
    }

        .box-icon-body .titlebox h2, .box-icon-body .titlebox h2 a {
            color: #fff;
        }

        .box-icon-body .titlebox h2 {
            font-size: 14px;
            line-height: 22px;
            padding: 6px 22px 4px 22px;
            margin: 0;
            text-transform: uppercase;
            border-radius: 4px;
            font-weight: normal;
        }

    .block-law {
        padding: 0px 10px;
    }

    .m-bottom {
        margin-bottom: 10px !important;
    }

    .text-muted {
        color: #999999;
    }

    .text-right {
        text-align: right;
    }

    .box-icon-body a.more {
        font-size: 12px;
        font-weight: bold;
    }
</style>

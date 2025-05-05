<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Detail.ascx.cs" Inherits="VideoIntroduceFeature.UI.Detail" %>

<%@ Import Namespace="mojoPortal.Features" %>

<%--flowplayer 6.5--%>
<%--div id="player" class="flowplayer" data-swf="../../Data/FlowPlayer/flowplayer.commercial-6.0.5/flowplayer.swf" data-key="$863732616083910" data-ratio="0.4167"></div>--%>
<link href="../../Data/FlowPlayer/flowplayer.commercial-6.0.5/skin/functional.css" rel="stylesheet" />
<script src="../../Data/FlowPlayer/flowplayer.commercial-6.0.5/flowplayer.min.js"></script>


<%-- flowplayer 7.2--%>
<%--<link href="../../Data/FlowPlayer/skin/skin.css" rel="stylesheet" />
                <script src="../../Data/FlowPlayer/flowplayer.min.js"></script>--%>


<style type="text/css">
    .videolistwrraper {
        width: 100%;
        float: left;
        margin-bottom: 20px;
    }

    .videolist {
        width: 100%;
        float: left;
    }

    .videoDetail {
        width: calc(50% - 10px);
        padding-right: 10px;
        float: left;
        margin-top: 10px;
    }

    .videoTitle:hover {
        color: blue;
    }


    .itemVideo img {
        position: relative;
        height: 150px;
    }

    .imgVideo {
        width: 100%;
        float: left;
        height: 150px;
    }

    .showplay {
        top: 36%;
        left: 36%;
        position: absolute;
        cursor: pointer;
        width: 64px;
        height: 64px;
        z-index: 99;
        background-image: url('../../Data/Icon16x16/youtube-64.png');
    }

    .imgVideo img {
        width: 100%;
        height: 150px;
        margin: 0;
    }

    .playicon {
    }

    .videoContent {
        width: 100%;
        height: auto;
        float: left;
    }

    .videoInfo {
        width: 65%;
        float: left;
    }

    .x-time {
        color: #777;
        margin-bottom: 10px;
    }

    .fa {
        display: inline-block;
        font: normal normal normal 14px/1 FontAwesome;
        font-size: inherit;
        text-rendering: auto;
        -webkit-font-smoothing: antialiased;
    }

        .fa:before {
            content: "\f073";
        }

    .othderVideo-title {
        width: 100%;
        float: left;
        font-size: 18px;
        height: 24px;
        color: #ffa312;
        border-bottom: 2px solid #016597;
    }

    .videoInfo-date {
        text-align: right;
        color: #666;
        font-size: 12px;
    }

    .othderVideo {
        width: calc(35% - 20px);
        padding-left: 20px;
        float: left;
    }

    .videoTitle {
        width: 100%;
        float: left;
        font-size: 12px;
        margin-top: 5px;
        text-align: justify;
        height: 27px;
        overflow: hidden;
        color: rgb(0,0,0);
        font-weight: normal;
    }

    .itemVideo {
        width: 100%;
        float: left;
        height: 200px;
    }

    .videoInfo-title {
    }


    .fp-brand {
        display: none !important;
        width: 0px !important;
    }

    .flowplayer {
        background-size: 100%;
        height: 490px;
    }

        .flowplayer.is-loading .fp-waiting,
        .flowplayer.is-seeking .fp-waiting {
            display: block;
        }

        .flowplayer .fp-waiting svg {
            height: 0;
        }

    .fixVideoDetail {
        padding: 0 !important;
    }

    .listvideo {
        width: 100%;
        float: left;
        height: 430px;
        overflow-x: auto;
    }

    .videoInfo-title {
        max-height: 65px;
        overflow: hidden;
        line-height: 22px;
        text-align: justify;
        font-size: 18px;
        font-weight: bold;
    }
</style>
<div class="videolistwrraper">
    <asp:Panel ID="pnlListVideo" runat="server" CssClass="videolist">
        <div class="videoInfo">

            <div class="videoContent">
                <div id="player" class="flowplayer" data-swf="../../Data/FlowPlayer/flowplayer.commercial-6.0.5/flowplayer.swf" data-key="$863732616083910" data-ratio="0.4167"></div>
                <asp:Literal ID="literPlayer" ClientIDMode="Static" runat="server"></asp:Literal>
                <asp:HiddenField ID="hdfVideo" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdfTitle" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdfVideoActive" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdfBackground" runat="server" ClientIDMode="Static" />
            </div>
        </div>
        <div class="othderVideo">
            <div class="videoInfo-title">
                <asp:Label ID="lblTittle" runat="server"></asp:Label>
            </div>
            <div class="videoInfo-view" style="display: none">
                <div class="x-time">
                    <asp:Label ID="lblView" runat="server"></asp:Label>
                </div>
            </div>
            <div class="videoInfo-date">
                <asp:Label ID="lblDate" runat="server"></asp:Label>
            </div>
            <asp:Panel ID="pnlOrtherVideo" runat="server" Visible="false">

                <div class="othderVideo-title">
                    Video khác
                </div>
                <div class="listvideo">
                    <asp:Repeater ID="rptVideo" runat="server">
                        <ItemTemplate>
                            <div class="videoDetail <%# (Container.ItemIndex > 0 && Container.ItemIndex + 1 % 3 == 0)?"fixVideoDetail":string.Empty %>">
                                <div class="itemVideo">
                                    <a href='<%# VideoIntroduceUtils.VideoDetailItemUrl(SiteRoot,PageId, ModuleId, int.Parse(Eval("ItemID").ToString()),string.Empty)%>' title='<%#Eval("Title") %>'>
                                        <div class="imgVideo">
                                            <span class="playicon"></span>
                                            <img src='<%#ConfigurationManager.AppSettings["VideoIntroduceFileFolder"]+Eval("ImageVideo") %>' />
                                        </div>
                                        <div class="videoTitle">
                                            <%#Eval("Title") %>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        if ($("#hdfVideo").val() != null && $("#hdfVideo").val() != "") {
            flowplayer("#player", {
                autoplay: true,
                aspectRatio: "12:5",
                clip: {
                    sources: [
                      { type: "application/x-mpegurl", src: "" + $("#hdfVideo").val() + "" },
                      { type: "video/mp4", src: "" + $("#hdfVideo").val() + "" },
                       { type: "video/avi", src: "" + $("#hdfVideo").val() + "" },
                        { type: "video/mkv", src: "" + $("#hdfVideo").val() + "" },
                          { type: "video/ogm", src: "" + $("#hdfVideo").val() + "" }
                    ],
                    title: "" + $("#hdfTitle").val() + "",
                    logo: {
                        url: 'http://flash.flowplayer.org/media/img/player/acme.png',
                        fullscreenOnly: true,
                        opacity: 0
                    },
                }
            });
        } else {
            $("#player").hide();
        }
        //$(".itemVideo").hover(
        //    function () {
        //        $(this).find("span").addClass("showplay");
        //    }, function () {
        //        $(this).find("span").removeClass("showplay");
        //    }
        //    );

    });
</script>

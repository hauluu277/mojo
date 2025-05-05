<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="VideoHot.ascx.cs" Inherits="VideoIntroduceFeatures.UI.VideoHot" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%--flowplayer 6.5--%>
<%--<div id="player" class="flowplayer" data-swf="../../Data/FlowPlayer/flowplayer.commercial-6.0.5/flowplayer.swf" data-key="$863732616083910" data-ratio="0.4167"></div>--%>
<%--                <link href="../../Data/FlowPlayer//flowplayer.commercial-6.0.5/skin/functional.css" rel="stylesheet" />
                <script src="../../Data/FlowPlayer/flowplayer.commercial-6.0.5/flowplayer.min.js"></script>--%>


<%-- flowplayer 7.2--%>
<%--<link href="../../Data/FlowPlayer/skin/skin.css" rel="stylesheet" />
<script src="../../Data/FlowPlayer/flowplayer.min.js"></script>--%>


<style type="text/css">
    .fp-brand {
        display: none !important;
    }

    #player {
        /*min-height: 250px;*/
        float: left;
    }

    /*.flowplayer {
        /*background-image: url(https://flowplayer.org/media/img/logo-blue.png);*/
        background-size: 100%;
    }*/

        /* waiting element with custom graphic */
        /*.flowplayer .fp-waiting {
                            background: url(../../Data/Icon16x16/loading.gif) no-repeat center center;
                            height: 100%;
                            display: none;
                        }*/

        .flowplayer.is-loading .fp-waiting,
        .flowplayer.is-seeking .fp-waiting {
            display: block;
        }

        .flowplayer .fp-waiting svg {
            height: 0;
        }




    /* The Modal (background) */
    .modal {
        display: none; /* Hidden by default */
        position: fixed; /* Stay in place */
        z-index: 99999; /* Sit on top */
        padding-top: 80px; /* Location of the box */
        left: 0;
        top: 0;
        width: 100%; /* Full width */
        height: 100%; /* Full height */
        overflow: auto; /* Enable scroll if needed */
        background-color: rgb(0,0,0); /* Fallback color */
        background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
    }

    /* Modal Content */
    .modal-content {
        position: relative;
        background-color: #fefefe;
        margin: auto;
        padding: 0;
        border: 1px solid #888;
        width: 60%;
        box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
        -webkit-animation-name: animatetop;
        -webkit-animation-duration: 0.4s;
        animation-name: animatetop;
        animation-duration: 0.4s;
    }

    /* Add Animation */
    @-webkit-keyframes animatetop {
        from {
            top: -300px;
            opacity: 0;
        }

        to {
            top: 0;
            opacity: 1;
        }
    }

    @keyframes animatetop {
        from {
            top: -300px;
            opacity: 0;
        }

        to {
            top: 0;
            opacity: 1;
        }
    }

    .ortherVideo ul li {
        cursor: pointer;
    }
    /* The Close Button */
    .close {
        color: white;
        float: right;
        font-size: 24px;
        font-weight: bold;
    }

        .close:hover,
        .close:focus {
            color: #000;
            text-decoration: none;
            cursor: pointer;
        }

    .modal-header {
        background-color: #2196F3;
        color: white;
        padding: 0;
        padding-left: 20px;
    }


    .modal-body {
        height: 400px;
        padding: 0;
    }

    .modal-footer {
        background-color: #f1f1f1;
        color: white;
    }
</style>


<div class="ViddeoHighlight ViddeoHighlight_Han">
    <div class="HomeArticleHot-title doc_slide__tinnoibat han_title_video">
        <h3>
            <asp:HyperLink ID="hplVideo" runat="server"></asp:HyperLink></h3>
    </div>
    <asp:Literal ID="literPlayer" runat="server"></asp:Literal>
    <p><asp:HyperLink ID="hplVideoHot" runat="server"></asp:HyperLink></p>
    <div id="player" class="flowplayer" data-swf="../../Data/FlowPlayer/flowplayer.commercial-6.0.5/flowplayer.swf" data-key="$863732616083910" data-ratio="0.4167"></div>
    <%--<div id="player" class="flowplayer" data-swf="../../Data/FlowPlayer/flowplayer.commercial-6.0.5/flowplayer.swf" data-key="$863732616083910" data-ratio="0.4167"></div>--%>
    <div style="clear: both"></div>
    <asp:HiddenField ID="hdfVideo" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdfTitle" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdfBackground" runat="server" ClientIDMode="Static" />
    <div style="clear: both"></div>
        <div class="titelVideo">
            <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
        <%--<asp:HyperLink ID="TitleVideo" runat="server" ></asp:HyperLink>--%>
    </div>
    <div class="ortherVideo">
        <ul>
            <asp:Repeater ID="rptVideo" runat="server">
                <ItemTemplate>
                    <li class="showVideo" data-video='<%#Eval("YoutubeUrl") %>' data-title='<%#Eval("Title") %>'>
                        <a href='javascript:void(0)' title='<%#Eval("Title") %>'>
                            <%#Eval("Title") %>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
<div id="myModal" class="modal">

    <!-- Modal content -->
    <div class="modal-content">
        <div class="modal-header">
            <span class="close">&times;</span>
            <h3 id="modal-title">Modal Header</h3>
        </div>
        <div class="modal-body">
            <p>Some text in the Modal Body</p>
            <p>Some other text...</p>
        </div>
        <%--        <div class="modal-footer">
            <h3>Modal Footer</h3>
        </div>--%>
    </div>

</div>
<script>
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the button that opens the modal

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal 

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
        modal_body.innerHTML = "";
        modalTitle.innerHTML = "";
    }
    // When the user clicks anywhere outside of the modal, close it

    var modal_body = document.getElementsByClassName("modal-body")[0];
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            modal_body.innerHTML = "";
            modalTitle.innerHTML = "";
        }
    }
    var modalTitle = document.getElementById('modal-title');
    $(".showVideo").click(function () {
        modal_body.innerHTML = $(this).attr("data-video");
        modalTitle.innerHTML = $(this).attr("data-title");
        modal.style.display = "block";
    });

</script>
<script type="text/javascript">
    $(document).ready(function () {
        if ($("#hdfVideo").val() != null && $("#hdfVideo").val() != "") {
            $(".flowplayer").css("background-image", 'url(' + $("#hdfBackground").val() + ')');
            //flowplayer("#player", {
            //    //splash: true,
            //    aspectRatio: "12:5",
            //    clip: {
            //        //rtmp: "rtmp://r.demo.flowplayer.netdna-cdn.com/play",
            //        //title: "Buffalo Soldiers",
            //        sources: [
            //          { type: "application/x-mpegurl", src: "" + $("#hdfVideo").val() + "" },
            //          { type: "video/mp4", src: "" + $("#hdfVideo").val() + "" },
            //           { type: "video/avi", src: "" + $("#hdfVideo").val() + "" },
            //            { type: "video/mkv", src: "" + $("#hdfVideo").val() + "" },
            //              { type: "video/ogm", src: "" + $("#hdfVideo").val() + "" }
            //        ]
            //    }
            //});

            flowplayer("#player", {
                aspectRatio: "12:5",
                clip: {
                    sources: [
                      { type: "application/x-mpegurl", src: "" + $("#hdfVideo").val() + "" },
                      { type: "video/mp4", src: "" + $("#hdfVideo").val() + "" },
                       { type: "video/avi", src: "" + $("#hdfVideo").val() + "" },
                        { type: "video/mkv", src: "" + $("#hdfVideo").val() + "" },
                          { type: "video/ogm", src: "" + $("#hdfVideo").val() + "" }
                    ]
                    //,
                    //title: "" + $("#hdfTitle").val() + ""
                }
            });
        }
        var id = $(".HomeArticleHot-category li").first().attr("data-category");
        $(".Hot ul[data-orther=" + id + "]").css("display", "");
        $(".HomeArticleHot-category li").css("background", "rgb(191,191,191)");
        $(".HomeArticleHot-category li[data-category=" + id + "]").css("background", "rgb(219, 73, 94)");

    });
</script>

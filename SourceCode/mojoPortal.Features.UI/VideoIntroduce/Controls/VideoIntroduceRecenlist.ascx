<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="VideoIntroduceRecenlist.ascx.cs" Inherits="VideoIntroduceFeatures.UI.VideoIntroduceRecenlist" %>

<%@ Import Namespace="mojoPortal.Features" %>


<style type="text/css">
    .video-all {
        width: 100%;
        /* margin-bottom: 20px; */
        float: left;
        /* height: 262px; */
        background-color: #F4F4F4;
    }

        .video-all ul {
            width: 100%;
            float: left;
        }

            .video-all ul li {
                float: left;
                width: 25%;
                list-style: none;
                padding: 0;
                margin: 0;
                overflow: hidden;
                border-bottom: 5px solid white;
                border-right: 5px solid white;
                cursor: pointer;
            }

    .lastItem {
        border-bottom: 0 !important;
    }

    .video-detail {
        width: 100%;
        height: 194px;
        overflow: hidden;
        position: relative;
    }

    .video-img {
        width: 100%;
        height: 100%;
        -webkit-transition: all 1s ease;
        transition: all 1s ease;
    }

    .video-all ul li:hover .video-img {
        -webkit-transform: scale(1.3);
        -ms-transform: scale(1.3);
        transform: scale(1.3);
    }

    .video-all li .video-descript {
        bottom: 0;
        left: 0;
        right: 0;
        background: transparent;
        opacity: 1;
        display: inline-block;
        position: absolute;
        color: #fff;
        z-index: 11;
        top: auto;
        background: linear-gradient(to bottom,rgba(0,0,0,0) 0%,rgba(0,0,0,0.8) 49%,rgba(0,0,0,0.8) 100%);
        height: 80px;
    }

    .video-des-title {
        color: white;
        font-family: Arial;
        display: block;
        text-align: left;
        line-height: 22px;
        font-size: 18px;
        font-weight: 500;
        padding: 30px 5px 10px 10px;
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
<div class="video-all">
    <ul>
        <asp:Repeater ID="rptVideo" runat="server">
            <ItemTemplate>
                <li class="showVideo" data-video='<%#Eval("YoutubeUrl") %>' data-title='<%#Eval("Title") %>'>
                    <div class="video-detail">
                        <div class="video-img" style="background: url('<%#ConfigurationManager.AppSettings["VideoIntroduceFileFolder"]+Eval("ImageVideo") %>'); background-size: 100% 100%; background-repeat: no-repeat"></div>
                        <div class="video-descript">
                            <span class="video-des-title">
                                <%#Eval("Title") %>
                            </span>
                        </div>
                    </div>
                </li>

            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <asp:Label ID="VideoNull" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Panel ID="pnlVideoPager" runat="server" CssClass="ArticlePager">
        <portal:mojoCutePager ID="pgrVideo" runat="server" />
    </asp:Panel>
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

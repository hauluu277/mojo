<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="VideoListControl.ascx.cs" Inherits="VideoIntroduceFeature.UI.VideoListControl" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .all-list ul li {
        float: left;
        width: calc(25% - 5px);
        list-style: none;
        padding: 0;
        margin: 0;
        border-bottom: 5px solid white;
        border-right: 5px solid white;
        overflow: hidden;
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
        color: #0000;
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
       /* padding-left: 20px;*/
    }


    .modal-body {
        height: 400px;
        padding: 0;
    }

    .modal-footer {
        background-color: #f1f1f1;
        color: white;
    }

    .list-unstyled li img {
        height: auto;
    }
</style>

<style>
    .box-bigImage {
        width: 100%;
        display: flex;
        justify-content: space-between;
        height: 300px;
        overflow: hidden;
    }

    .box-bigImage-Left {
        flex: 2;
    }

        .box-bigImage-Left a {
            display: block;
            width: 100%;
        }

        .box-bigImage-Left img {
            width: 100%;
            height: 100%;
        }

    .box-bigImage-Right {
        flex: 1;
        background-color: #F7F7F7;
    }

        .box-bigImage-Right p {
            padding-left: 30px;
            width: 200px;
            white-space: pre-line;
            word-wrap: break-word;
        }

    .row-bigImage {
        margin-bottom: 20px;
    }

    .box-child {
        display: flex;
        flex-direction: column;
    }

        .box-child .box-image-child {
            height: 200px;
            overflow: hidden;
        }

            .box-child .box-image-child a {
                display: block;
            }

                .box-child .box-image-child a img {
                    width: 100%;
                }

        .box-child .box-content-child h3 {
            margin: 10px 0px 0px 0px;
        }

        .manhdeptrai2 {
            width: 100%;
            height: 100%;
        }

        .mg-bt-5 {
            margin-bottom: 8px;
        }
</style>
<div class="lightSlideImageOther">
<%--    <div class="slideOtherTitle">
        <p>
            <span>Thư viện Video</span>
        </p>
    </div>--%>
    <div class="all-list">

        <div class="row">
            <div class="col-sm-12">
                <div class="row row-bigImage">
                    <div class="col-sm-12">
                        <div class="box-bigImage">
                            <div class="box-bigImage-Left">
                                 <asp:HiddenField ID="IdItem" runat="server" />
                                <div class="manhdeptrai manhdeptrai2">
                                    <asp:Image ID="imgFirst" runat="server" />
                                    <div class="overlay">
                                        <div class="text">
                                            <div class="svg-wrapper">
                                                <svg height="60" width="110" xmlns="http://www.w3.org/2000/svg">
                                                    <rect id="shape" height="60" width="110" />
                                                    <asp:Literal ID="literbtnChiTetVideo" runat="server"></asp:Literal>
                                                </svg>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="box-bigImage-Right">
                                <p>
                                <asp:Label ID="lblname" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
                <div class="row content_video-bot">
                    <asp:Repeater ID="rptVideo" runat="server" SkinID="Blog">
                         <ItemTemplate>
                            <div class="col-sm-4 col-lg-4 ">
                                <div class="box-child">

                                    <div class="box-image-child">
                                        <%--<a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                            <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                        </a>--%>


                                        <div class="manhdeptrai manhdeptrai2">
                                           <%-- <asp:Image ID="imgFirst" runat="server" />--%>
                                            <img src="/Data/File/VideoIntroduce/<%#Eval("ImageVideo") %>" title="<%#Eval("Title") %>" />
                                            <div class="overlay">
                                                <div class="text">
                                                    <div class="svg-wrapper">
                                                        <svg height="60" width="110" xmlns="http://www.w3.org/2000/svg">
                                                            <rect id="shape" height="60" width="110" />
                                                            <div id="text">
                                                                <a href="javascript:ShowVideo(<%#Eval("ItemID") %>)" id="video_<%#Eval("ItemID") %>" class="showVideo" data-video='<%#Eval("ItemUrl") %>' data-title='<%#Eval("Title") %>' data-ngaytao='<%#Eval("CreateDate") %>'><span class="spot"></span><i class="fa fa-play-circle" aria-hidden="true"></i></a>
                                                            </div>
                                                        </svg>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                            
                                    <div class="box-content-child">
                                        <h3><%#Eval("Title") %></h3>
                                        <p><%#string.Format("{0:dd/MM/yyyy}", Eval("CreateDate")) %></p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
        </div>

        

        <%--<asp:Repeater ID="rptVideo" runat="server">
            <ItemTemplate>
                <div class="manhdeptrai">
                    <img src="<%#ConfigurationManager.AppSettings["VideoIntroduceFileFolder"]+Eval("ImageVideo") %>" alt="Avatar" class="image">
                    <div class="overlay">
                        <div class="text">
                            <div class="svg-wrapper">
                                <svg height="40" width="150" xmlns="http://www.w3.org/2000/svg">
                                    <rect id="shape" height="40" width="150" />
                                    <div id="text">
                                        <a href="javascript:ShowVideo(<%#Eval("ItemID") %>)" id="video_<%#Eval("ItemID") %>" class="showVideo" data-video='<%#Eval("YoutubeUrl") %>' data-title='<%#Eval("Title") %>'><span class="spot"></span>Xem</a>
                                    </div>
                                </svg>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>--%>
    </div>
</div>
<asp:Panel ID="pnlDonViPager" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrDanhBa" runat="server" />
        </asp:Panel>
<div id="myModal" class="modal" >

    <!-- Modal content -->
    <div class="modal-content fix_video" >
        <div class="modal-header">
            <span class="close fix_close-video">&times;</span>
            <h3 id="modal-title" class="fix_title-video-top">Modal Header</h3>
        </div>
        <div class="modal-body" style="height:auto">
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
    function ShowVideo(id) {
        var video = $("#video_" + id);
        var title = $(video).attr("data-title")
        var dataUrl = $(video).attr("data-video")
        var ngayTao = $(video).attr("data-ngaytao")
        var xhtml = `
                <div class='container-fluid'>
                <div class='row'>
                    
                    <div class='col-sm-9 fix_video-box'>
                        <video style="width:100%; height:100%"  controls class="" src="${dataUrl}"> </video>
                    </div>

                    <div class='col-sm-3 fix_title-video-box'>
                        <div class='mg-bt-5 fix_title-video'>${title}</div>
                        <div>${ngayTao}</div>
                    </div>
                </div>
                </div>
            `

        modal_body.innerHTML = xhtml;
        modalTitle.innerHTML = $(video).attr("data-title");
        modal.style.display = "block";
    }


</script>

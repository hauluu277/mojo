<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="VideoListControl.ascx.cs" Inherits="VideoIntroduceFeature.UI.VideoListControl" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>


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
</style>


<div class="lightSlideImageOther">
    <div class="slideOtherTitle">
        <p>
            <span>Thư viện Video</span>
        </p>
    </div>
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
                                                <svg height="40" width="150" xmlns="http://www.w3.org/2000/svg">
                                                    <rect id="shape" height="40" width="150" />
                                                    <asp:Literal ID="literbtnChiTetVideo" runat="server"></asp:Literal>
                                                    <%--<div id="text">
                                                        <a href="javascript:ShowVideo(<%#Eval("ItemID") %>)" id="video_<%#Eval("ItemID") %>" class="showVideo" data-video='<%#Eval("YoutubeUrl") %>' data-title='<%#Eval("Title") %>'><span class="spot"></span>Xem</a>
                                                    </div>--%>
                                                </svg>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="box-bigImage-Right">
                                <p>
                                    Tên:
                                <asp:Label ID="lblname" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="row hide">
                    <asp:Repeater ID="rptVideo" runat="server" SkinID="Blog" Visible="false">
                         <ItemTemplate>
                            <div class="col-sm-6">
                                <div class="box-child">

                                    

                                    <div class="box-image-child">
                                        <%--<a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                            <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                        </a>--%>


                                        <div class="manhdeptrai manhdeptrai2">
                                            <img src="/Data/File/VideoIntroduce/<%#Eval("ImageVideo") %>" title="<%#Eval("Title") %>" />
                                            <div class="overlay">
                                                <div class="text">
                                                    <div class="svg-wrapper">
                                                        <svg height="40" width="150" xmlns="http://www.w3.org/2000/svg">
                                                            <rect id="shape" height="40" width="150" />
                                                            <div id="text">
                                                                <a href="javascript:ShowVideo(<%#Eval("ItemID") %>)" id="video_<%#Eval("ItemID") %>" class="showVideo" data-video='<%#Eval("ItemUrl") %>' data-title='<%#Eval("Title") %>'><span class="spot"> <i class="fa fa-youtube-play" aria-hidden="true"></i> </span></a>
                                                            </div>
                                                        </svg>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                            
                                    <div class="box-content-child">
                                        <h3>Tên: <%#Eval("Title") %></h3>
                                        <p>Người đăng: <%#Eval("CreatedDate") %></p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>





        <asp:Panel ID="pnlDonViPager" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrDanhBa" runat="server" />
        </asp:Panel>



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
    function ShowVideo(id) {
        var video = $("#video_" + id);
        modal_body.innerHTML = $(video).attr("data-video");


        


        modalTitle.innerHTML = $(video).attr("data-title");
        modal.style.display = "block";
    }


</script>

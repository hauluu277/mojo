<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MediaGroupList.ascx.cs" Inherits="MediaFeature.UI.MediaGroupList" %>
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

    .box-bigImage-Left a:hover {
        transform: scale(1.5)
    }

    .box-image-child a {
        transition: all 0.7s ease;
    }

        .box-image-child a:hover {
            transform: scale(1.5)
        }

    .box-bigImage-Left {
        flex: 2;
        overflow: hidden;
    }

        .box-bigImage-Left a {
            display: block;
            width: 100%;
            transition: all 0.7s ease;
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



<div class="han_all-list">



    <div class="row">
        <div class="col-sm-12">
            <div class="row row-bigImage">
                <div class="col-sm-12">
                    <div class="box-bigImage">
                        <div class="box-bigImage-Left">
                            <asp:HyperLink ID="hplFirst" runat="server">
                                <asp:Image ID="imgFirst" runat="server" />
                            </asp:HyperLink>
                        </div>


                        <div class="box-bigImage-Right content_thuvienha-top-right">
                            <p>

                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </p>
                            <p>

                                <asp:Literal ID="ltlSapo" runat="server"></asp:Literal>
                            </p>
                            <p>

                                <asp:Label ID="lblNgayTao" runat="server"></asp:Label>
                            </p>
                            <p>

                                <asp:Label ID="lblNguoiTao" runat="server"></asp:Label>
                            </p>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <asp:Repeater ID="rptImage" runat="server">
            <ItemTemplate>
                <div class="col-sm-4">
                    <div class="box-child">

                        <div class="box-image-child">
                            <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                            </a>
                        </div>

                        <div class="box-content-child">
                            <h3><%#Eval("NameGroup") %></h3>
                            <p><%#Eval("Sapo") %></p>
                            <p><%#Eval("CreatedByUser") %></p>
                        </div>

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>







</div>
<asp:Panel ID="pnlDonViPager" runat="server" CssClass="ArticlePager">
    <portal:mojoCutePager ID="pgrDanhBa" runat="server" />
</asp:Panel>
<asp:Label ID="DanhBanull" runat="server" Visible="false"></asp:Label>

<script>
    $(document).ready(function () {
        $(".Haan_Gallery_Search").hide(); // thêm dòng này để nội dung trong thẻ p ẩn lúc đầu
        $("#haan_btn_hide").hide();

        $("#haan_btn_hide").click(function () {
            $(".Haan_Gallery_Search").hide();
            $("#haan_btn_hide").hide();
            $("#haan_btn_show").show();
        });
        $("#haan_btn_show").click(function () {
            $(".Haan_Gallery_Search").show();
            $("#haan_btn_hide").show();
            $("#haan_btn_show").hide();
        });
    });
</script>

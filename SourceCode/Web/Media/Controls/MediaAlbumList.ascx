<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MediaAlbumList.ascx.cs" Inherits="MediaFeature.UI.MediaAlbumList" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<script src="/Data/FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function show(id) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            url: "/Media/ViewListMediaAlbum.aspx/ViewCount",
            data: "{'id':'" + id + "'}",
            success: function (data) {
            }
        });

        var featured = $("#featured_" + id).val();
        var featuredStr = "";
        var totalView = $("#hfView").val() + ": " + $("#view_" + id).val();
        var des = $("#des_" + id).val();
        if (featured == "True") {
            featuredStr = $("#hfFeatured").val();
        }
        var url = $('#item_' + id).attr('data-href');
        var filePath = $('#url_' + id).val();
        $("#hfFile").val(id);

        //$(".cnt223 #img").attr("src", $("#item_" + id + " img").attr("src"));
        //$(".dlClick").attr("data-id", filePath);


        if ($("#item_" + id).attr("data-type") == "2") {
            var urlImg = $('#item_' + id + ' img').attr('src');

            var str = "<a href='" + url + "' class='player'>";
            //str+="<img src="+urlImg+" />";
            str += "</a>";
            $("#item_data").html(str);

            var player = flowplayer("a.player", "/Data/FlowPlayer/flowplayer-3.2.16.swf", {
                plugins: {
                    pseudo: { url: "/Data/FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
                },
                canvas: {
                    // configure background properties
                    background: ' url(' + urlImg + ') no-repeat center center fixed',
                },
                clip: {
                    provider: 'pseudo', autoPlay: false,
                    //autoBuffering: true,

                    //start: 62,
                    //url: $("a.player").attr("a[href]")
                }
            });
            $("#lnkDowload").show();
        }
        else if ($("#item_" + id).attr("data-type") == "3") {
            var embed = $("#embed_" + id).val();
            str = embed;
            $("#item_data").html(embed);
            $("#lnkDowload").hide();
        } else {
            var url = $('#item_' + id + ' img').attr('src');
            var str = "<img src='" + url + "'/>";
            $("#lnkDowload").hide();
            $("#item_data").html(str);
        }
        $("#des").html(des);
        $("#featureed").text(featuredStr);
        $("#totalview").text(totalView);
        $(".popup").show();
    }
    $(function () {
        $('.close').click(function () {
            $("#item_data").html("");
            $('.popup').hide();
            return false;
        });

        $('.x').click(function () {
            $("#item_data").html("");
            $('.popup').hide();
            return false;
        });
    });

    function DowloadFile() {
        var CommentId = $(".dlClick").attr("data-id");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            url: "/Media/ViewListMediaAlbum.aspx/Download",
            data: "{'filePath':'" + CommentId + "'}",
            success: function (data) {


            },
            complete: function () {

            }
        });

    }
    function Show(id) {
        $("#Show_" + id).show();
    }
</script>
<style type="text/css">
    .search-by-dr {
        width: 210px;
        height: 26.5px;
        /* background: #EEE; */
        border-radius: 3px;
    }

    .search-by {
        float: left;
        margin-left: 75px;
    }
</style>
<div class="all">
    <div class='popup'>
        <div class='cnt223'>
            <img src="/Data/File/Media/Delete.png" class="x" title="Close" />
            <br />
            <span id="des" class="des-cription"></span>
            <p>
                <br />
                <span id="item_data"></span>
            </p>
            <div class="view-dowload">
                <div class="left">
                    <span id="featureed" style="color: red; font-weight: bold"></span>
                    <br />
                    <span id="totalview" style="font-weight: bold"></span>
                </div>
                <div class="right">
                    <span id="dowload" style="float: right">
                        <br />
                        <asp:LinkButton ID="lnkDowload" ClientIDMode="Static" Font-Underline="true" runat="server"></asp:LinkButton>
                        <asp:HiddenField ClientIDMode="Static" ID="hfFile" runat="server" />
                    </span>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="breadcrumb">
    <div class="breadcrumb-item">
        <asp:Label ID="lblDictionaryTitle" runat="server"></asp:Label>
    </div>
    <div class="form-search">
        <div class="search-by">
            <asp:DropDownList ID="drlSearch" CssClass="search-by-dr" runat="server"></asp:DropDownList>
            <%--            <select id="drlSearch2" runat="server" class="search-by-dr">
                <option value="0">Tiêu chí tìm kiếm</option>
                <option value="3">Mới nhất</option>
                <option value="2">Nổi bật</option>
                <option value="1">Xem nhiều nhất</option>
            </select>--%>
        </div>
        <div class="search-wrapper cf">
            <input type="text" id="txtSearch2" runat="server" placeholder="Tìm kiếm...">
            <button id="btnSearch" runat="server" onserverclick="btnSearch_Click">Search</button>
        </div>
    </div>
</div>
<div class="search-media">
    <span class="viewAll" style="display: none">
        <asp:HyperLink ID="hplDataGroup" runat="server" NavigateUrl='#' />
        >
        <asp:HyperLink ID="hplMutilData" runat="server" NavigateUrl="#"></asp:HyperLink>
    </span>
    <span class="data-search" style="padding-bottom: 10px; display: none">chuyên mục:
        <asp:DropDownList ID="ddlMultiGroup" runat="server"></asp:DropDownList>
    </span>
    <span class="data-search" style="padding-bottom: 10px; display: none">
        <mp:SiteLabel ID="lblFeatured" runat="server" CssClass="lbl-search" ConfigKey="MediaAlbumFeaturedLable" ResourceFile="MediaResources" />
        <asp:DropDownList ID="ddlFeatured" runat="server"></asp:DropDownList>
    </span>
</div>
<div class="all-list">
    <ul>
        <asp:Repeater ID="dtlData" runat="server">
            <ItemTemplate>
                <li>
                    <div class="main-data">
                        <asp:Literal runat="server" Text='<%# GetHtml(Eval("ItemID").ToString(),Eval("FilePath").ToString(),Eval("TypeData").ToString(),Eval("ImageVideo").ToString(),Eval("Description").ToString()) %>'></asp:Literal>
                        <input type="hidden" id="view_<%#Eval("ItemID") %>" value="<%#Eval("TotalView") %>" />
                        <input type="hidden" id="featured_<%#Eval("ItemID") %>" value='<%#Eval("Featured") %>' />
                        <input type="hidden" id="url_<%#Eval("ItemID") %>" value='<%#Eval("FilePath") %>' />
                        <input type="hidden" id="embed_<%#Eval("ItemID") %>" value='<%#Eval("EmbedCode") %>' />
                        <div class="des-item">
                            <span class="text-left">
                                <span style="font-size: 12px; color: #a2a2a2;" id="createDate_<%#Eval("ItemID") %>">
                                    <%#Eval("CreatedDate") %>
                                </span>
                                <span class="des-fix">
                                    <a href="javascript:void(0)" title='<%#Eval("Description") %>' onclick='show(<%#Eval("ItemID") %>)'>
                                        <asp:Literal ID="lterDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Literal>
                                    </a>
                                </span>
                                <input type="hidden" id="des_<%#Eval("ItemID") %>" value="<%#Eval("Description") %>" />
                            </span>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
<asp:Label ID="DanhBanull" runat="server" Visible="false"></asp:Label>
</div>
<asp:HiddenField runat="server" ID="hfFeatured" ClientIDMode="Static" />
<asp:HiddenField ID="hfView" runat="server" ClientIDMode="Static" />
<asp:Panel ID="pnlDonViPager" runat="server" CssClass="ArticlePager">
    <portal:mojoCutePager ID="pgrDanhBa" runat="server" />
</asp:Panel>

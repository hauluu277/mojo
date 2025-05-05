<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SlideMediaControl.ascx.cs" Inherits="MediaFeature.UI.SlideMediaControl" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="breadcrumb" style="display:none">
    <div class="breadcrumb-item">
        <asp:Label ID="lblDictionaryTitle" runat="server"></asp:Label>
    </div>
    <div class="form-search" style="display: none">
        <div class="search-wrapper cf">
            <input type="text" id="txtSearch2" runat="server" placeholder="Search here...">
            <button id="btnSearch2" runat="server" onserverclick="btnSearch2_Click">Search</button>
        </div>
    </div>
</div>
<div class="all-list">
    <div class="gallery">
        <asp:HyperLink ID="hplTitle" runat="server"></asp:HyperLink>
    </div>
    <div style="margin-top:10px;">
    <img src="./ONA_BDT/Resources/left-arrow2.gif" style="float:left;" class="arrow" onclick="Element_scrollX('tableAnhThumb',-130*5)">
    <div id="tableAnhThumb" style="overflow:hidden;width:625px;float:left;margin:0 5px 0;background:#e6e6e6;padding:1px 0;">
        <table cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
        <asp:Repeater ID="dtlData" runat="server">
            <ItemTemplate>
                <li>
                    <div class="main-data">
                        <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                            <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                        </a>
                        <div class="des-item">
                            <span class="text-left">
    <%--                            <span style="font-size: 12px; color: #a2a2a2;" id="createDate_<%#Eval("ItemID") %>">
                                    <%#Eval("CreatedDate") %>
                                </span>--%>
                                <span class="des-fix">
                                    <%#Eval("NameGroup") %>
                                </span>
                            </span>
                        </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
       
                </tr>
            </tbody>
        </table>
    </div>
    <img src="./ONA_BDT/Resources/right-arrow2.gif" style="float:right;" class="arrow" onclick="Element_scrollX('tableAnhThumb',130*5)">
</div>
</div>
<asp:Panel ID="pnlDonViPager" runat="server" CssClass="ArticlePager">
    <portal:mojoCutePager ID="pgrDanhBa" runat="server" />
</asp:Panel>
<asp:Label ID="DanhBanull" runat="server" Visible="false"></asp:Label>

 <script>
                            $(function(){
                            var objImg=$("#tableAnhThumb").find("img")[0];
                            ShowImage(objImg,'imageContent');
                            $("#imageDesc").css({opacity:0.4});
                            $("#imageContent").load(function(){
                            $(".image_desc").css({left:0,top:$("#imageContent").height()-14-$("#divDesc").height()});    
                            $(".image_desc").show();});
                            });
                            var activeTab;
                            function ShowImage(objImg)
                            {
                            $("#imageDesc").hide();
                            var imageSrc=""+$(objImg).attr("src");
                            imageSrc=imageSrc.substr(0,imageSrc.indexOf("&"));//+"&w=663";
                            $("#tableAnhThumb img").css("border","solid 2px #e6e6e6");
                            $(objImg).css("border","solid 2px #012A78");
                            $("#divDesc").css("height","");
                            $("#divDesc,#divShadow").html($(objImg).attr("title"));
                            var h = $("#divDesc").height();
                            if (h < 40) h = 40;$(".image_desc").height(h).hide();
                            $("#imageContent").attr("src", imageSrc);
                            }
                        </script>



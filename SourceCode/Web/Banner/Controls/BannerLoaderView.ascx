<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="BannerLoaderView.ascx.cs" Inherits="BannerFeature.UI.BannerLoaderView" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="clearfix"></div>
                <div class="view_banner">
                     <%if(!isSlideTop && !isSlideBottom) {%>
                    <asp:Repeater runat="server" ID="rptBanner">
                        <ItemTemplate>
                            <%if (IsHorizontal)
                              { %>
                            <div class="link_edit" style='width:<%#Width(IsHorizontal, isheight, b++, Convert.ToDecimal(Eval("Width"))).ToString().Replace(",", ".")+"%" %>' >
<%--                                <p>
                                    <asp:HyperLink ID="lnkEdit" runat="server" Text="<%# EditLinkText %>" ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' Visible='<%#IsEditable %>'
                                        NavigateUrl='<%#SiteRoot+"/Banner/EditPost.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>'></asp:HyperLink>
                                </p>--%>
                                <asp:HyperLink ID="linkAdvertise" runat="server" NavigateUrl='<%#SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                                    <%if (isheight == true) {%>  <asp:Image ID="imgBanner"  Height='<%#Height() %>' Width="100%" Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' /><%} %>
                                    <%else{ %><asp:Image ID="Image2" Width="100%" Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>'/><%} %>
                            <%#BuildFlashObject(Convert.ToBoolean(Eval("IsImage")),Eval("Path").ToString(), Convert.ToDecimal(Eval("Width")),IsHorizontal,isNotHeight) %>
                                </asp:HyperLink>
                            </div>
                            <%} %>
                            <%else
                              { %><div class="banner_vertical" >
                               <%-- <p>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="<%# EditLinkText %>" ToolTip="<%# EditLinkTooltip %>" ImageUrl='<%# EditLinkImageUrl %>' Visible='<%#IsEditable %>'
                                        NavigateUrl='<%#SiteRoot+"/Banner/EditPost.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>'></asp:HyperLink>
                                </p>--%>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                                    <%if (isheight == true) {%> <asp:Image ID="Image1" Width='100%'  Height='<%#Height() %>' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' /><%} %>
                                    <%else{ %><asp:Image ID="Image3" Width='100%' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' /><%} %>
                            <%#BuildFlashObject(Convert.ToBoolean(Eval("IsImage")),Eval("Path").ToString(), Convert.ToDecimal(Eval("Width")), IsVertical, isNotHeight) %>
                                </asp:HyperLink>
                            </div>
                            <%} %>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%} %>
                    <%else if (isSlideBottom)
                      { %>
                    <div class="flexslider carousel">    
                        <ul class="slides">                       
                        <asp:Repeater ID="rptSlideBottom" runat="server">
                            <ItemTemplate>      
                                <li>       
                                    <asp:HyperLink ID="linkAdvertise" runat="server" NavigateUrl='<%#SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>   
                                        <asp:Image ID="imgBanner" Height='<%#Height() %>' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' />
                                    </asp:HyperLink>
                                    </li>   
                            </ItemTemplate>
                        </asp:Repeater>                     
                       </ul>
                    </div>
                    <%} %>
                     <%else if (isSlideTop)
                      { %>                   
                     <portal:EasySlider ID="ptEasy" runat="server">
                         <ul>
                        <asp:Repeater ID="rptSlideTop" runat="server">
                            <ItemTemplate>
                                <li>
                                                                     <a href='<%#SiteRoot+"/Banner/Click.aspx?pageid="+PageId+"&mid="+ModuleId+"&item="+Eval("ItemID") %>' Target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'>
                                <asp:Image ID="imgBanner" Height='<%#heightSlideTop %>' Width='<%#widthSlideTop %>' Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' />
                                        </a>
                                    </li>
                            </ItemTemplate>
                        </asp:Repeater>   
                            </ul>
                       </portal:EasySlider>     
<%--                         <portal:bxSlider ID="bxSlideTop" runat="server">                 
                        <asp:Repeater ID="rptSlideTop" runat="server">
                            <ItemTemplate>                
                                <a href="#"><asp:Image ID="imgBanner"  Width="100%" Visible='<%#VisbleBannerImage(Convert.ToBoolean(Eval("IsImage")), Eval("StartDate").ToString(), Eval("EndDate") !=null ?Eval("EndDate").ToString() : string.Empty )%>' runat="server" ImageUrl='<%# BannerUtils.FormatImageDialog(ConfigurationManager.AppSettings["BannerImagesFolder"], Eval("Path").ToString()) %>' /></a>
                            </ItemTemplate>
                        </asp:Repeater>                       
                    </portal:bxSlider>--%>
                    <%} %>
                </div>
<script type="text/javascript">
   <%-- $(window).load(function () {
        $('.flexslider').flexslider({
            animation: "slide",
            animationLoop: true,
            itemWidth: 125,
            itemMargin: 25,
            minItems: <%=numberImage%>,
            maxItems: 8
        });
    });--%>
    $('.easyslider a').click(function (e) {
        var i = $(this).index();
        //alert(i);
        slider.goToSlide(i);
        slider.stopAuto();
        restart = setTimeout(function () {
            slider.startAuto();
        }, 500);

        return false;
    });

</script>
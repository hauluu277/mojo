<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HotListRight.ascx.cs" Inherits="ArticleFeature.UI.HotListRight" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<script src="../../Data/Sites/1/skins/framework/jssor.slider-21.1.5.min.js"></script>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper SlideList">
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

                <style>
                    /* jssor slider bullet navigator skin 03 css */
                    /*
        .jssorb03 div           (normal)
        .jssorb03 div:hover     (normal mouseover)
        .jssorb03 .av           (active)
        .jssorb03 .av:hover     (active mouseover)
        .jssorb03 .dn           (mousedown)
        */
                    .jssorb03 {
                        position: absolute;
                    }

                        .jssorb03 div, .jssorb03 div:hover, .jssorb03 .av {
                            position: absolute;
                            /* size of bullet elment */
                            width: 21px;
                            height: 21px;
                            text-align: center;
                            line-height: 21px;
                            color: white;
                            font-size: 12px;
                            background: url('/../Data/Sites/1/skins/framework/images/tiep.png') no-repeat;
                            overflow: hidden;
                            cursor: pointer;
                        }

                        .jssorb03 div {
                            background-position: -5px -4px;
                        }

                            .jssorb03 div:hover, .jssorb03 .av:hover {
                                background-position: -35px -4px;
                            }

                        .jssorb03 .av {
                            background-position: -65px -4px;
                        }

                        .jssorb03 .dn, .jssorb03 .dn:hover {
                            background-position: -95px -4px;
                        }

                    /* jssor slider arrow navigator skin 03 css */
                    /*
        .jssora03l                  (normal)
        .jssora03r                  (normal)
        .jssora03l:hover            (normal mouseover)
        .jssora03r:hover            (normal mouseover)
        .jssora03l.jssora03ldn      (mousedown)
        .jssora03r.jssora03rdn      (mousedown)
        */
                    .jssora03l, .jssora03r {
                        display: block;
                        position: absolute;
                        /* size of arrow element */
                        width: 55px;
                        height: 55px;
                        cursor: pointer;
                        background: url('/../Data/Sites/1/skins/framework/images/quaylai.png') no-repeat;
                        overflow: hidden;
                    }

                    .jssora03l {
                        background-position: -3px -33px;
                    }

                    .jssora03r {
                        background-position: -63px -33px;
                    }

                    .jssora03l:hover {
                        background-position: -123px -33px;
                    }

                    .jssora03r:hover {
                        background-position: -183px -33px;
                    }

                    .jssora03l.jssora03ldn {
                        background-position: -243px -33px;
                    }

                    .jssora03r.jssora03rdn {
                        background-position: -303px -33px;
                    }
                </style>

                <script type="text/javascript">
                    jssor_1_slider_init = function () {

                        var jssor_1_options = {
                            $AutoPlay: false,
                            $AutoPlaySteps: 6,
                            $SlideDuration: 160,
                            $SlideWidth: 150,
                            $SlideSpacing: 3,
                            $Cols: 6,
                            $ArrowNavigatorOptions: {
                                $Class: $JssorArrowNavigator$,
                                $Steps: 6
                            },
                            $BulletNavigatorOptions: {
                                $Class: $JssorBulletNavigator$,
                                $SpacingX: 1,
                                $SpacingY: 1
                            }
                        };

                        var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

                        //responsive code begin
                        //you can remove responsive code if you don't want the slider scales while window resizing
                        // function ScaleSlider() {
                        //var refSize = jssor_1_slider.$Elmt.parentNode.clientWidth;
                        //if (refSize) {
                        //    refSize = Math.min(refSize, 0);
                        //    jssor_1_slider.$ScaleWidth(refSize);
                        //}
                        //else {
                        //    window.setTimeout(ScaleSlider, 30);
                        //}
                        //}
                        //ScaleSlider();
                        //$Jssor$.$AddEvent(window, "load", ScaleSlider);
                        //$Jssor$.$AddEvent(window, "resize", ScaleSlider);
                        //$Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
                        //responsive code end
                    };
                </script>
                <%if (displayTitle)
                  { %>
                <div class="header_slide" style="display:none;">
                    <h2>
                        <asp:Label ID="lblTit" runat="server"></asp:Label></h2>
                </div>
                <%} %>
                <% if (showHotNew)
                    { %>
                <div class="col-sm-12 pd0 tinTopRight">
                    <div class="imgeFirstNews">
                    <asp:Image ID="imgRightFirst" runat="server" class="col-sm-12 pd0"/>
                    </div>
                    <div class="titleFirstNews">
                    <asp:HyperLink ID="hplRightFirst" runat="server" class="col-sm-12 pd0"></asp:HyperLink>

                    </div>
                </div>

                    <%} %>
                <%if (showHotNew)
                  {%>
                <div class="wrraper" id="jssor_1" >
                  
                    <div class="hide_opcity">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <%if (visibleImg)
                                  { %>
                                <div class="newsTopRight col-sm-12 pd0">
                                        <div class="content-newsright col-sm-12 pd0">

                                            <div class="img-newsright col-sm-4 pd0">
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                                <asp:Image ID="imghotright" runat="server" Visible='<%#visibleImg %>' Width="100%" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                    </asp:HyperLink>
                                            </div>
                                            <div class="tit-newsright col-sm-8 pdr0" style=" color: #333333; line-height: 1.5; font-weight: bold; text-align: justify; font-size: 14px">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                                <%#formatContent(Eval("Title").ToString()) %>
                                    </asp:HyperLink>
                                            </div>
                                        </div>

                                </div>
                                <%}
                                  else
                                  { %>
                                <div class="li_docSlideNoImg">
                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                        <%#formatContent(Eval("Title").ToString()) %>
                                    </asp:HyperLink>
                                </div>
                                <%} %>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--                        <div data-u="navigator" class="jssorb03" style="bottom: 10px; right: 10px;">
                            <!-- bullet navigator item prototype -->
                            <div data-u="prototype" style="width: 21px; height: 21px;">
                                <div data-u="numbertemplate"></div>
                            </div>
                        </div>
                        <span data-u="arrowright" class="jssora03r" style="top: 0px; right: 8px; width: 55px; height: 55px;" data-autocenter="2"></span>
                        <span data-u="arrowleft" class="jssora03l" style="top: 0px; left: 8px; width: 55px; height: 55px;" data-autocenter="2"></span>

                        --%>
                        <!-- Arrow Navigator -->
                        <%--                        <span style="top: 0%; width: 14.2%; cursor: pointer; left: 0px; position: absolute; height: 90px; opacity: 0.3; background: black;"></span>
                        <span style="width: 14.2%; background: black; position: absolute; top: 0%; left: 75.9%; height: 90px; opacity: 0.3;"></span>--%>
                    </div>
                </div>
                <script type="text/javascript">jssor_1_slider_init();</script>
                <%}
                  else
                  { %>
                <style type="text/css">
                    .doc_slide ul {
                        padding: 5px 10px 10px 0px;
                    }

                    .doc_slide a {
                        color: #333 !important;
                    }

                    .doc_slide a {
                        color: #333 !important;
                        font-size: 12px !important;
                        font-weight: bold;
                        text-align: justify;
                    }

                    .content-newsright {
                        padding: 5px 0px;
                    }

                    .img-newsright {
                        width: 80px;
                        float: left;
                        margin: 0px;
                        padding: 2px 10px 2px 0px;
                    }

                </style>
                <div class="doc_slide doc_slide__tinnoibat">
                    <h3>Tin tức nổi bật</h3>

                    <ul>
                        <asp:Repeater ID="rptArticle" runat="server">
                            <ItemTemplate>
                                <%if (visibleImg)
                                  { %>

                                <li class="li_docSlide2">
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                        <div class="content-newsright">
                                            <div class="img-newsright li_docSlide2__item">
                                                <asp:Image ID="imghotright" runat="server" Height="53" Width="80" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                            </div>
                                            <div class="tit-newsright li_docSlide2__item"><%#formatContent(Eval("Title").ToString()) %></div>
                                        </div>
                                        <div class="cleared"></div>
                                    </asp:HyperLink>

                                </li>
                                <%}
                                  else
                                  { %>
                                <li class="li_docSlideNoImg">
                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                        <%#formatContent(Eval("Title").ToString()) %>
                                    </asp:HyperLink>
                                </li>
                                <%} %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <%} %>

            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
<script>
         var motaTeaser = $(".tit-newsright").text().length;
        if (motaTeaser > 80) {
            var newmota = $(".tit-newsright").text().substring(0, 80) + " ...";
            $(".tit-newsright").text(newmota);
        }
</script>
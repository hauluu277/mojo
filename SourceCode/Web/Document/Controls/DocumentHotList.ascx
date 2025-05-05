<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DocumentHotList.ascx.cs" Inherits="DocumentFeature.UI.DocumentHotList" %>

<%@ Import Namespace="DocumentFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper SlideList">
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <style type="text/css">
                    /*.doc_slide {
                        background: #F1F1F1;
                        padding-bottom: 0px;
                        margin-bottom: 10px;
                    }

                    .header_slide {
                        background: #016597; /*url('images/bg_banner.png') repeat-x;*/
                    /* height: 30px;
                    }*/



                    .doc_slide a {
                        color: #174EA5 !important;
                        font-size: 12px !important;
                        font-weight: bold;
                        text-align: justify;
                    }

                        .doc_slide a:hover,
                        .doc_slide a:visited {
                            color: #174EA5;
                            text-decoration: none;
                        }

                    .content-news-event {
                        width: 100%;
                        margin: 0px;
                        padding: 5px 0px;
                    }

                    .img-news-event {
                        width: 100%;
                        margin: 0px;
                        padding: 2px 10px 2px 0px;
                    }

                        .img-news-event img {
                            margin: 0px !important;
                        }

                    .tit-news-event {
                        /*width: calc(95% - 80px);*/
                        width: auto;
                        float: none;
                        margin: 0px;
                        padding-left: 0px;
                        text-align: justify;
                    }

                    .event-date {
                        font-style: italic;
                    }

                    .event-content {
                        min-height: 50px;
                        text-overflow: clip;
                        overflow: hidden;
                        padding-top: 10px;
                    }

                    .scroll-event {
                        max-height: 400px;
                        height: auto;
                        height: 340px;
                        /*overflow-y: hidden;*/
                    }

                        .scroll-event ul {
                            list-style-image: url('/Data/Sites/1/skins/art42-blue/images/sqpurple.gif');
                            list-style-position: inside;
                            text-align: justify !important;
                        }

                            .scroll-event ul li:first-child {
                                list-style: none;
                                padding: 15px 13px 0px !important;
                                margin: 0px !important;
                                height: 123px !important;
                            }

                            .scroll-event ul li {
                                float: left;
                                padding-right: 5px !important;
                                margin-bottom: 9px;
                            }

                                .scroll-event ul li a {
                                    font-size: 13px !important;
                                    color: #333333 !important;
                                    font-weight: bold;
                                    text-align: justify !important;
                                    line-height: 20px;
                                }

                                    .scroll-event ul li a:hover {
                                        color: #b80002 !important;
                                    }

                    .tit-news-event {
                        font-family: Arial;
                        font-size: 13px;
                        font-weight: bold;
                    }
                </style>
                <div class="doc_slide">
                    <div class="header_slide">
                        <h2>
                            <asp:Label ID="lblTit" runat="server"></asp:Label></h2>
                    </div>
                    <div class="scroll-event">
                        <ul>
                            <li style="height: 63px; overflow: hidden; width: 92%;">
                                <img src="../../Data/skins/art42-blue/images/vanbanmoi.png" style="width: 187px; height: 123px; margin-left: -2px; margin-top: -5px;" />
                            </li>
                            <asp:Repeater ID="rptArticle" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:HyperLink ID="lnkDetail" runat="server" NavigateUrl='<%# FormatBlogTitleUrl(siteSettings.SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                    <%--                        <asp:Panel ID="pnShowIMG" runat="server" Visible="<%#(Container.ItemIndex + 1) ==1 %>">
                                                <asp:Image ID="imghotright" Visible='<%#ShowImage(Eval("ImageUrl").ToString())%>' runat="server" Height="105" Width="100%" ImageUrl='<%# EventUtils.FormatImageDialog(ConfigurationManager.AppSettings["EventImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                            </asp:Panel>--%>

                                            <%#formatContent(Eval("Summary").ToString()) %>
                                            <%--<span class="event-date">(<%#FormatDate(Eval("StartDate").ToString()) %>)</span>--%>
                                        </asp:HyperLink>
                                    </li>

                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

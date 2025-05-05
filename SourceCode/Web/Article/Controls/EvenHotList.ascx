<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EvenHotList.ascx.cs" Inherits="ArticleFeature.UI.EvenHotList" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper SlideList">
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <div class="doc_slide">
                    <div class="header_slide">
                        <h2>
                            <asp:Label ID="lblTit" runat="server"></asp:Label></h2>
                    </div>
                    <ul>
                        <asp:Repeater ID="rptArticle" runat="server">
                            <ItemTemplate>
<%--                                <%if (visibleImg)
                                  { %>  Visible='<%#visibleImg %>' --%>
                                <li class="li_docSlide">
                                    <asp:HyperLink ID="lnkDetail" runat="server" NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                        <div class="content-newsright">
                                            <div class="img-newsright">
                                                <asp:Image ID="imghotright" runat="server" Height="53" Width="80" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' /></div>
                                            <div class="4tit-newsright"><%#formatContent(Eval("Title").ToString()) %></div>
                                        </div>
                                        <div class="cleared"></div>
                                    </asp:HyperLink>

                                </li>
<%--                                <%} %>
                                <%else
                                  { %>
                                <li class="li_docSlideNoImg">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                        <%#formatContent(Eval("Title").ToString()) %>
                                    </asp:HyperLink>
                                </li>
                                <%} %>--%>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

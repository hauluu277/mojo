<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleHotSchoolControl.ascx.cs" Inherits="ArticleFeature.UI.ArticleHotSchoolControl" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<asp:Panel ID="pnlType1" runat="server">
    <section class="awe-section-8" id="demos">
        <section class="constructo-latest-article-section section-padding">
            <div class="container">
                <div class="large-12 columns xghan-tintucNoibat">
                    <asp:Repeater ID="rptArticle" runat="server">
                        <ItemTemplate>
                            <div class="col-sm-4 xghan-item">
                                <div class="constructo-single-news">
                                    <div class="xghan-news-img">
                                        <picture>
							<source src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" media="(max-width: 480px)">										
							<img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" alt="<%#Eval("Title") %>">
						</picture>
                                    </div>
                                   
                                    <div class="single-news">
                                        <h3><a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" tabindex="0"><%#Eval("Title") %></a></h3>

                                        
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel runat="server" ID="pnlViewMore" CssClass="tintucNoibat__xemthem">
                        <a href="/tin-tuc">Xem thêm</a>
                    </asp:Panel>
                </div>
            </div>
        </section>
    </section>
</asp:Panel>
<asp:Panel ID="pnlType2" runat="server">
    <div class="hot-left col-lg-6 padding0">
        <asp:Image ID="imgType2" runat="server" />
        <h3>
            <asp:HyperLink ID="hplTitleType2" runat="server"></asp:HyperLink>
        </h3>
        <div class="hot-date">
            <asp:Label ID="lblStartDate" Class="" runat="server"></asp:Label>  |
            <asp:HyperLink ID="hplHotCategory" runat="server"></asp:HyperLink>
        </div>
        <div class="hot-sumarry">
            <asp:Literal ID="literSumaryType2" runat="server"></asp:Literal>
        </div>
    </div>
    <div id="bgr-center" class="col-lg-1">
        <img src="../../Data/Sites/84/skins/framework/images/layer.png" />
    </div>
    <div class="hot-right col-lg-5 padding0">
        <asp:Repeater ID="rptArticleType2" runat="server">
            <ItemTemplate>
                <div class="item-hot">
                    <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" />
                    <p><a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" tabindex="0"><%#Eval("Title") %></a></p>
                    <div class="date-category">
                        <span><%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %></span>  |
                        <a href="<%#Eval("CategoryUrl") %>"><%#Eval("CategoryName") %></a>
                    </div>
                    <div class="type2-sumarry">
                        <%#Eval("Summary") %>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel runat="server" ID="pnlViewMoreType2" CssClass="tintucNoibat__xemthem">
            <asp:HyperLink ID="hplType2More" runat="server"></asp:HyperLink>

        </asp:Panel>
    </div>
</asp:Panel>




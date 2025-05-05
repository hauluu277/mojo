<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HotList.ascx.cs" Inherits="ArticleFeature.UI.HotListModule" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%--<link href="../../Data/skins/art42-blue/pgwslider.css" rel="stylesheet" />--%>
<script src="../../Data/skins/framework/pgwslider.js"></script>
<mp:CornerRounderTop ID="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper HotList">
    <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
        <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
            <asp:Panel ID="pnlShowArticle" runat="server">

                <% if (ShowImgConfig)
                    { %>
                <div class="col-sm-12 pd0 tinTopRight">
                    <div class="imgeFirstNews">
                    <asp:Image ID="imgFirst" runat="server" class="col-sm-12 pd0"/>
                    </div>
                    <div class="titleFirstNews">
                    <asp:HyperLink ID="hplFirst" runat="server" class="col-sm-12 pd0"></asp:HyperLink>

                    </div>
                </div>

                <div class="pgwSlider pgwSliderShowIMG">
                    <%}
                        else
                        { %>
                    <div class="pgwSlider pgwSliderNoIMG">
                        <%} %>
                        <%--DataSource='<%# BindArticleHotOrther(Convert.ToInt32(Eval("ItemID"))) %>'--%>
                        <asp:Repeater ID="rptHotOrther" runat="server">
                            <ItemTemplate>
                                <% if (ShowImgConfig)
                                    { %>
                                <div class="col-sm-6 pd0 newsTopLeft">
                                    <div class="col-sm-4 pd0">
                                        <asp:Image ID="imgHot" CssClass="ShowImgConfig" alt='<%#Eval("Title") %>' data-description='<%#Eval("Summary") %>' Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                    </div>
                                   <div class="col-sm-8 pdr0">
                                        <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' title='<%#Eval("Title") %>' onclick="ShowArticle('<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>')">
                                        <input type="hidden" alt='<%#Eval("Title") %>' data-description='<%#Eval("Summary") %>' runat="server" value='<%# ArticleUtils.FormatImageDialogNew(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' data-large-src='<%# ArticleUtils.FormatImageDialogNew(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                        <span><%#Eval("Title") %></span>
                                    </a>
                                   </div>
                                </div>
                                <%}
                                    else
                                    { %>
                                <div class="col-sm-6">
                                    <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' title='<%#Eval("Title") %>' onclick="ShowArticle('<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>')">
                                        <input type="hidden" alt='<%#Eval("Title") %>' data-description='<%#Eval("Summary") %>' runat="server" value='<%# ArticleUtils.FormatImageDialogNew(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' data-large-src='<%# ArticleUtils.FormatImageDialogNew(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                        <span><%#Eval("Title") %></span>
                                    </a>
                                </div>
                                <%} %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <script type="text/javascript">
                        function ShowArticle(url) {
                            window.location = url;
                        }
                        $(document).ready(function () {
                            var pgwSlider = $('.pgwSlider').pgwSlider();
                            //pgwSlider.selectionMode("mouseOver");
                            pgwSlider.startSlide();
                        });
                        //setInterval(function () {
                        //    var imgs = $$('.fadein img'),
                        //     visible = imgs.findAll(function (img) { return img.visible(); });
                        //    if (visible.length > 1) visible.last().fade({ duration: .3 });
                        //    else imgs.last().appear({
                        //        duration: .3,
                        //        afterFinish: function () { imgs.slice(0, imgs.length - 1).invoke('show'); }
                        //    });
                        //}, 3000);
                    </script>
            </asp:Panel>
            <%-- <div>
                    <ul class="pgwSlider">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <asp:Repeater ID="rptHotOrther" DataSource='<%# BindArticleHotOrther(Convert.ToInt32(Eval("ItemID"))) %>' runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a href='javascript:void(0)' title='<%#Eval("Title") %>' onclick="ShowArticle('<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>')">
                                                <asp:Image ID="imgHot" alt="Paris, France" data-description="Eiffel Tower and Champ de Mars" Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                                <input type="hidden" alt='<%#Eval("Title") %>' data-description='<%#Eval("Summary") %>' runat="server" value='<%# ArticleUtils.FormatImageDialogNew(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' data-large-src='<%# ArticleUtils.FormatImageDialogNew(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                                <span><%#Eval("Title") %></span>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>--%>
        </portal:InnerBodyPanel>
    </portal:OuterBodyPanel>
    <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom ID="cbottom1" runat="server" />

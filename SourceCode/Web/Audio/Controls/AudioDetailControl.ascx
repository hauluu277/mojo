<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AudioDetailControl.ascx.cs" Inherits="AudioFeature.UI.AudioDetailControl" %>
<%@ Import Namespace="AudioFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<asp:Panel ID="pnlGallery" runat="server">


    <div class="warrperSilderIMG">
        <div class="DetailImageSlide">
            <p>
                <asp:Label ID="lblIMGofLibrary" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblGroupName" runat="server" CssClass="slideTitle"></asp:Label>
                <asp:HyperLink ID="hplEdit" runat="server" ToolTip="Edit"></asp:HyperLink>
            </p>
            <span>
                <asp:Label ID="lblSapoLibrary" runat="server"></asp:Label>
            </span>
        </div>

        <%--Nav ảnh--%>
        <div class="lightSlideImageLeft">
            <div class="lightSlideImageContent">
            </div>
            <div class="lightSlideImage">
                <div class="item">
                    <div class="clearfix" style="max-height: 400px;">
                        <ul id="content-slider" class="content-slider">
                            <asp:Repeater ID="rptSlider" runat="server">
                                <ItemTemplate>
                                    <li data-thumb='<%# Eval("FilePath") %>' data-src='<%# Eval("FilePath") %>' title='<%#Eval("Description") %>' style="cursor: pointer">
                                        <img data-id="<%#Container.ItemIndex + 1%>" src='<%# Eval("FilePath") %>' alt='<%#Eval("Description") %>' />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>

        </div>

        <div class="demo-gallery">
            <ul id="lightgallery" class="row">
                <asp:Repeater ID="dtlData" runat="server">
                    <ItemTemplate>
                        <li class="itemLbImage" data-responsive="<%# Eval("FilePath") %> 375, <%# Eval("FilePath") %> 480, <%# Eval("FilePath") %> 800" data-src="<%# Eval("FilePath") %>" data-sub-html="<h4 class='h3Slide'><%#Eval("Description") %></h4>">
                            <%--<img class="img-responsive" src="<%# Eval("FilePath") %>">--%>
                            <audio controls autoplay src="<%# Eval("FilePath") %>">
                                                                    Your browser does not support the audio element.
                                                                </audio>
                            <h4 class='h3Slide'><%#Eval("Description") %></h4>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="author_gallery">
                <asp:Label ID="lblAuthor" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblDateCreate" runat="server" CssClass="slideDate"></asp:Label>
            </div>
        </div>

        <!-- #endregion Jssor Slider End -->
        <%--Thư viện khác--%>
        <div class="lightSlideImageOther">
            <div class="slideOtherTitle">
                <p>
                    <asp:Label ID="lblOrtherLibrary" runat="server"></asp:Label>
                </p>
            </div>

            <div class="all-list">
                <ul>
                    <asp:Repeater ID="rptGroupMedia" runat="server">
                        <ItemTemplate>
                            <li>
                                <div class="gallery-list">
                                    <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                        
                                        <div class="gallery-img">
                                    <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                </div>
                                <div class="des-item">
                                    <%--  <%#Eval("CreatedDate") %>--%>
                                    <span class="des-fix">
                                        <%#Eval("NameGroup") %>
                                    </span>
                                    </span>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
    <asp:Label ID="GalleryNull" runat="server" Visible="false"></asp:Label>
    <asp:HiddenField runat="server" ID="hfFeatured" ClientIDMode="Static" />
    <asp:HiddenField ID="hfView" runat="server" ClientIDMode="Static" />
</asp:Panel>


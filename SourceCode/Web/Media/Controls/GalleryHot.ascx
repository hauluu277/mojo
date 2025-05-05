<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="GalleryHot.ascx.cs" Inherits="MediaFeature.UI.GalleryHot" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<link href="/Data/js/Carousel/assets/css/owl.carousel.min.css" rel="stylesheet" />
<link href="/Data/js/Carousel/assets/css/owl.theme.default.min.css" rel="stylesheet" />
<script src="/Data/js/Carousel/assets/js/owl.carousel.js"></script>

<%--Tab 1 Hiển thị các thư viện ảnh ở trang chủ--%>
<asp:Panel ID="pnlTab1" runat="server" ViewStateMode="Disabled" EnableViewState="false">
    <div class="han_iamge">
        <div class="all_image_han haan_all_image">
            <div class="category-list">
                <ul class="img_video_title_han">
                    <li>
                        <h3><a href="/anh">ẢNH</a></h3>
                    </li>
                </ul>
            </div>
            <div class="image_han_content">
                <div class="left-video han-left-image-video">

                    <div class="owl-carousel owl-theme">

                        <asp:Repeater ID="rptGalleryHot" runat="server" ViewStateMode="Disabled" EnableViewState="false">
                            <ItemTemplate>
                                <div class="item">
                                    <div class="image_han">
                                        <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                                            <img src="/Data/File/Media/<%#Eval("FilePath") %>" />
                                        </a>
                                    </div>
                                    <div class="nd_image">
                                        <h3>
                                           <p><a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>"><%#Eval("NameGroup") %></a></p>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>
                <div class="right-video">
                    <asp:Repeater ID="rptGallery" runat="server" ViewStateMode="Disabled" EnableViewState="false">
                        <ItemTemplate>
                            <div class="image_han_content_item">
                                <div class="image_han">
                                    <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                                        <img src="/Data/File/Media/<%#Eval("FilePath") %>" />
                                    </a>
                                    <div class="abtn_camera_han_img">
                                        <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                                            <i class="fa fa-picture-o" aria-hidden="true"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="nd_image">
                                    <h3>
                                        <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>"><%#Eval("NameGroup") %></a>
                                    </h3>
                                    <div>
                                        <p>
                                        <p class="fa fa-clock-o"><span><%#FormatDateGallery(Eval("CreatedDate")) %></span></i></p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>

<%--Tab 2 cho phép thiết lập chọn và hiển thị thư viện ảnh ở trang Nghị viện thế giới--%>
<asp:Panel ID="pnlTab2" runat="server" ViewStateMode="Disabled" EnableViewState="false">
    <div class="pdr0 phongsu">
        <div class="phongsu_box wf100">
            <h2><a href="#">PHÓNG SỰ ẢNH</a></h2>
            <asp:HyperLink ID="hplTab2Image" runat="server" ViewStateMode="Disabled" EnableViewState="false">
                <asp:Image ID="imgTab2" runat="server" ViewStateMode="Disabled" EnableViewState="false" />
            </asp:HyperLink>
            <h3>
                <asp:HyperLink ID="hplTab2Title" runat="server" ViewStateMode="Disabled" EnableViewState="false"></asp:HyperLink></h3>
        </div>
    </div>
</asp:Panel>

<%--Tab 3 hiện thị phóng sự ảnh ở trang Quốc hội--%>
<asp:Panel ID="pnlTab3" runat="server" ViewStateMode="Disabled" EnableViewState="false">
    <div class="lapphap_phongsu_right wf100">
        <h2>PHÓNG SỰ ẢNH</h2>
        <div class="lapphap_ps_hinght">
            <asp:HyperLink ID="hplTab3Image" runat="server" ViewStateMode="Disabled" EnableViewState="false">
                <asp:Image ID="imgTab3" runat="server" ViewStateMode="Disabled" EnableViewState="false" />
            </asp:HyperLink>
            <h3>
                <asp:HyperLink ID="hplTab3Title" runat="server" ViewStateMode="Disabled" EnableViewState="false"></asp:HyperLink></h3>
        </div>
        <div class="lapphap_phongsu_right_img">
            <asp:Repeater ID="rptTab3Gallery" runat="server" ViewStateMode="Disabled" EnableViewState="false">
                <ItemTemplate>
                    <div class="col-sm-6 lapphap_psu_imgItem">
                        <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                            <img src="../Data/Sites/1/skins/framework/img/bdbnd_img/camera.png" />
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Panel>
<%--tab 2 hiển thị danh sách ảnh --%>
<asp:Panel ID="pnlTab4" runat="server" ViewStateMode="Disabled" EnableViewState="false">
    <div class="list_media_haan" id="haan_chuyenmuc_anh_video">
        <div class="slideOtherTitle">
            <h3>
                <a href="/anh">Chuyên mục ảnh</a>
            </h3>
        </div>
        <div class="all-list-media-haan">
            <ul>
                <asp:Repeater ID="rptGroupMedia" runat="server" ViewStateMode="Disabled" EnableViewState="false">
                    <ItemTemplate>
                        <li>
                            <div class="gallery-list-media-haan">
                                <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                    <div class="gallery-img-haan">
                                        <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                        <%--<i title="<%#Eval("NameGroup") %>" class="fa fa-camera"></i>--%>
                                    </div>
                                    <div class="des-item-media-haan">
                                        <%--  <%#Eval("CreatedDate") %>--%>
                                        <span class="des-fix-media-haan">
                                            <%#Eval("NameGroup") %>
                                        </span>
                                        </span>
                                    </div>
                                </a>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Panel>
<%--Tab 5 dnah sách ảnh từng chuyên mục --%>
<asp:Panel ID="pnlTab5" runat="server" ViewStateMode="Disabled" EnableViewState="false">
    <div class="haan_8video_module">
        <div class="haan-title-category-gallery-image">
            <asp:HyperLink ID="hplTitleCategoryImage" runat="server" ViewStateMode="Disabled" EnableViewState="false"></asp:HyperLink>
        </div>
        <div class="haan-content-gallery-image haan_content_video">
            <asp:Repeater ID="rptGalleryImageTab5" runat="server" ViewStateMode="Disabled" EnableViewState="false">
                <ItemTemplate>
                    <div class="haan-gallery-image-item haan_video_tab9_item">
                        <div class="image_han img_video_tab9">
                            <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                                <img src="/Data/File/Media/<%#Eval("FilePath") %>" />
                            </a>
                        </div>
                        <div class="title_video_haan_ngocj">
                            <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>"><%#Eval("NameGroup") %></a>
                            <asp:HyperLink ID="HyperLink1" runat="server" Visible='<%#AllowEdit %>' CssClass="ModuleEditLink" Text="Chỉnh sửa" NavigateUrl=<%#string.Format("{0}/Media/Editpost.aspx?item={1}",SiteRoot,Eval("ItemID")) %> ToolTip="Edit"></asp:HyperLink>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Panel>

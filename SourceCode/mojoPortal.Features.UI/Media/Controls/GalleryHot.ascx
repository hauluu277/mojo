<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="GalleryHot.ascx.cs" Inherits="MediaFeature.UI.GalleryHot" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<%--Tab 1 Hiển thị các thư viện ảnh ở trang chủ--%>
<asp:Panel ID="pnlTab1" runat="server">
    <div class="han_iamge">
        <div class="all_image_han">
            <div class="category-list">
                <ul class="img_video_title_han">
                    <li>
                        <h3><a href="/gallery">ẢNH</a></h3>
                    </li>
                </ul>
            </div>
            <div class="image_han_content">
                <div class="left-video han-left-image-video">
                    <asp:Repeater ID="rptGalleryHot" runat="server">
                        <ItemTemplate>
                            <div class="image_han">
                                <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                                    <img src="/Data/File/Media/<%#Eval("FilePath") %>" />
                                </a>
                            </div>
                            <div class="nd_image">
                                <h3>
                                    <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>"><%#Eval("NameGroup") %></a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="right-video">
                    <asp:Repeater ID="rptGallery" runat="server">
                        <ItemTemplate>
                            <div class="image_han_content_item">
                                <div class="image_han">
                                    <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                                        <img class="lazy" data-src="/Data/File/Media/<%#Eval("FilePath") %>" />
                                    </a>
                                    <div class="btn_camera_han_img">
                                        <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                                            <img class="lazy" data-src="../Data/Sites/1/skins/framework/img/bdbnd_img/camera.png" />
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
<asp:Panel ID="pnlTab2" runat="server">
    <div>PHÓNG SỰ ẢNH</div>
    <div>
        <asp:HyperLink ID="hplTab2Image" runat="server">
            <asp:Image ID="imgTab2" runat="server" />
        </asp:HyperLink>
        <asp:HyperLink ID="hplTab2Title" runat="server"></asp:HyperLink>
    </div>
</asp:Panel>

<%--Tab 3 hiện thị phóng sự ảnh ở trang Quốc hội--%>
<asp:Panel ID="pnlTab3" runat="server">
    <div>PHÓNG SỰ ẢNH</div>
    <div>
        <div>
            <div>
                <asp:HyperLink ID="hplTab3Image" runat="server">
                    <asp:Image ID="imgTab3" runat="server" />
                </asp:HyperLink>
                <asp:HyperLink ID="hplTab3Title" runat="server"></asp:HyperLink>
            </div>
        </div>
        <ul>
            <asp:Repeater ID="rptTab3Gallery" runat="server">
                <ItemTemplate>
                    <li>
                        <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                            <img src="../Data/Sites/1/skins/framework/img/bdbnd_img/camera.png" />
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</asp:Panel>
<%--Tab 4 dnah sách ảnh--%>
<asp:Panel ID="pnlTab4" runat="server">
    <div class="list_media_haan">
        <div class="slideOtherTitle">
            <h3>
                <a href="/anh">Chuyên mục ảnh</a>
            </h3>
        </div>
        <div class="all-list-media">
            <ul>
                <asp:Repeater ID="rptGroupMedia" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="gallery-list-media">
                                <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                    <div class="gallery-img">
                                        <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                    </div>
                                    <div class="des-item-media">
                                        <%--  <%#Eval("CreatedDate") %>--%>
                                        <span class="des-fix-media">
                                            <%#Eval("NameGroup") %>
                                        </span>
                                        </span>
                                    </div>
                                </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Panel>
<%--Tab 5 dnah sách ảnh từng chuyên mục --%>
<asp:Panel ID="pnlTab5" runat="server">
    <div class="haan-title-category-gallery-image">
        <asp:HyperLink ID="hplTitleCategoryImage" runat="server"></asp:HyperLink>
    </div>
    <div class="haan-content-gallery-image">
        <asp:Repeater ID="rptGalleryImageTab5" runat="server">
            <ItemTemplate>
                <div class="haan-gallery-image-item">
                    <div class="image_han">
                        <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>">
                            <img class="lazy" data-src="/Data/File/Media/<%#Eval("FilePath") %>" />
                        </a>
                    </div>
                    <div class="nd_image">
                        <p class="fa fa-clock-o"><span><%#FormatDateGallery(Eval("CreatedDate")) %></span></i></p>
                        <h3>
                            <a href="<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>" title="<%#Eval("NameGroup") %>"><%#Eval("NameGroup") %></a>
                        </h3>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>

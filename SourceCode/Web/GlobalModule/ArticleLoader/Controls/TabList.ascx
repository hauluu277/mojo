<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TabList.ascx.cs" Inherits="ArticleFeature.UI.TabLoader" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>



<%--Hiển thị tab kiểu tin mới, tin đọc nhiều --%>
<asp:Panel runat="server" ID="pnlTinMoiTinDocNhieu" CssClass="nbcContent">
    <div class="clearfix"></div>
    <div class=" bg-sukien">
        <asp:HyperLink runat="server" ID="hplTinMoi" Text="Tin mới" CssClass="sukien btn_tinmoi"></asp:HyperLink>
        <i class="fa fa-circle dots_tintuc hide" aria-hidden="true"></i>
        <asp:HyperLink runat="server" ID="HyperTinDocNhieu" Text="Tin đọc nhiều" CssClass="sukien btn_tindocnhieu hide"></asp:HyperLink>
    </div>
    <div class="bg-sukien-bt">
        <ul class=" nd-list-sukien nd-list-tinmoi">
            <asp:Repeater ID="rptTinMoi" runat="server">
                <ItemTemplate>
                    <li class="nd-list-sukien-item">
                        <div class="nd-sukien nd-list-tinmoi toggle-block fix_f">
                            <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><span><%# Eval("Title") %></span> </a>

                            <asp:Image runat="server" CssClass="nd-list-sukien-img" ID="mg" Visible='<%# ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' AlternateText='<%#Eval("Title") %>' />
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <ul class=" nd-list-sukien nd-list-tindocnhieu">
            <asp:Repeater ID="rptTinDocNhieu" runat="server">
                <ItemTemplate>
                    <li class="nd-list-sukien-item">
                        <div class="nd-sukien fix_f">
                            <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><span><%# Eval("Title") %></span> </a>
                            <asp:Image runat="server" CssClass="nd-list-sukien-img" ID="Image1" Visible='<%# ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' AlternateText='<%#Eval("Title") %>' />
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <script>
        document.querySelector(".btn_tindocnhieu").onclick = function (e) {
            document.querySelector(".nd-list-tindocnhieu").style.display = "block";
            document.querySelector(".nd-list-tinmoi").style.display = "none";
            e.target.style.color = "#fff";
            document.querySelector(".btn_tinmoi").style.color = "#91ecb1 ";

            console.log("bật tin đọc nhiều");
        }
        document.querySelector(".btn_tinmoi").onclick = function (e) {
            document.querySelector(".nd-list-tinmoi").style.display = "block";
            document.querySelector(".nd-list-tindocnhieu").style.display = "none";
            e.target.style.color = "#fff";
            document.querySelector(".btn_tindocnhieu").style.color = "#91ecb1";

            console.log("bật tin tin mới");
        }
    </script>
</asp:Panel>
<%--Kết thúc hiển thị tab kiểu tin mới, tin đọc nhiều--%>
<div class="clear"></div>

<%--Hiển thị tab kiểu  Kiểu thông báo--%>
<asp:Panel runat="server" ID="pnlThongBao" CssClass="nbcContent">
    <div class="col-sm-12 nopd nd-dv fix_thongbao-bot">
        <img src="/data/images/thong-bao.png" />
        <asp:Repeater ID="rptThongBao" runat="server">
            <ItemTemplate>
                <div class="col-sm-12 pl-0">
                    <a class="tieude-tb" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                        <i class="fa fa-angle-double-right"></i>
                        <%# Eval("Title") %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>
<%--Kết thúc hiển thị tab kiểu thông báo--%>

<%--Hiển thị tab kiểu văn bản mới--%>
<asp:Panel runat="server" ID="pnlVanBanMoi" CssClass="nbcContent fix_pr-30">
    <div class="nopd">
        <div class="document">
            <div class="document-title title_vanbanmoi">
                <a href="<%= string.Format("{0}van-ban",SiteRoot) %>">Văn bản mới</a>
                <a href='<%= string.Format("{0}van-ban",SiteRoot) %>'>
                    <span class="rounded-circle">
                        <img class="img_icon-right" src="/Data/Sites/1/skins/framework/images/icon_right.png" />
                    </span>
                </a>
            </div>
        </div>
        <div class="d-flex float-left flex-column">
            <asp:Repeater ID="rptDocument" runat="server">
                <ItemTemplate>
                    <div class="content_10-right-box document-content d-flex flex-column">
                        <div class="content_10-title-box document-content__top d-flex align-items-center">
                            <img src="<%#GetIconDocument(Eval("FilePath").ToString()) %>" />
                            <div class="document-content__title">
                                <p class="document-content__top-title"><%#Eval("Sign") %></p>
                                <a href="<%#DocumentUltils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId,true,SiteRoot+"document/detail.aspx?item="+Eval("ItemID")) %>">
                                    <p class="content_10-title-box-title"><%#Eval("LoaiVBName") %>&nbsp;<%#Eval("Summary") %></p>
                                </a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Panel>
<%--Kết thúc hiển thị tab kiểu văn bản mới--%>

<%--Hiển kiểu CÔNG NGHỆ THÔNG TIN VÀ CHUYỂN ĐỔI SỐ --%>
<asp:Panel runat="server" ID="pnlCongNgheThongTin" CssClass="nbcContent congnghethongtin_chuyendoiso">
    <div class="nopd">
        <div class="cds">
            <div class="cds-title">
                <p>
                    <asp:HyperLink runat="server" ID="hplCongNgheThongTin" Text="Chưa có thiết lập" CssClass=""></asp:HyperLink>
                </p>
                <a id="hplMoreCongNgheThongTin" title="Xem thêm" runat="server">
                    <span class="rounded-circle">
                        <img class="img_icon-right" src="/Data/Sites/1/skins/framework/images/icon_right.png" />
                    </span>
                </a>
            </div>
        </div>
        <div class="d-flex float-left row">
            <div class="d-flex flex-column cds-content-left col-lg-7 col-sm-12 col-12">
                <asp:Image ID="imgArticleCongNgheThongTin" runat="server" CssClass="cds-left-img" />
                <asp:HyperLink ID="hplArticleCongNgheThongTin" runat="server"></asp:HyperLink>
            </div>

            <div class="col-lg-5 col-sm-12 pl-0 cds-content-right fix_mobile-box">
                <asp:Repeater ID="rptCongNgheThongTin" runat="server">
                    <ItemTemplate>
                        <div class="col-sm-12 d-flex p-0 mb-20 fix_mobile">
                            <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">

                                <asp:Image runat="server" CssClass="img-hoatdong-dp" ID="Image2" Visible='<%# ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' AlternateText='<%#Eval("Title") %>' />


                            </a>
                            <div class="cds-content__right">
                                <div class="CNTT_CDS-right-title">
                                    <a class="" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%# Eval("Title") %></a>
                                </div>
                                <div class="cds-content__right-time"><%#string.Format("{0:dd/MM/yyyy}", Eval("StartDate")) %></div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Panel>
<%--Kết thục kiểu CÔNG NGHỆ THÔNG TIN VÀ CHUYỂN ĐỔI SỐ--%>

<%--Hiển kiểu thông tin tuyển sinh --%>
<asp:Panel runat="server" ID="pnlThongTinTuyenSinh" CssClass="nbcContent">
    <div class=" nopd">

        <div class="dichvuthongke document thongtin_tuyensinh-title-box ">
            <div class="row class-no-flex dichvuthongke-text document-title thongtin_tuyensinh-title">
                <div class="col-lg-6">
                    <asp:HyperLink CssClass="school-title_main" ID="hplCategoryThongTinTuyenSinh" runat="server"></asp:HyperLink>
                </div>
                <ul class="thongtin_tuyensinh-ul col-lg-6">
                    <asp:Repeater ID="rptCategoryTuyenSinh" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href="<%#Eval("Description") %>" title="<%#Eval("Name") %>"><%#Eval("Name") %></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="col-sm-12 nopd nd-dv row fix_m-thongtints ">
            <div class="d-flex flex-column cds-content-left col-sm-12 col-lg-6 thongtin_tuyensinh-left">
                <asp:Image ID="imgArticleTuyenSinh" runat="server" CssClass="" />
                <asp:HyperLink ID="hplArticleTuyenSinh" runat="server"></asp:HyperLink>
                <span class="title_bot-ttts">
                    <asp:Literal ID="liArticleTuyenSinh" runat="server"></asp:Literal>
                </span>
            </div>
            <div class="col-lg-6 col-sm-12 thongtin_ts-right">
                <asp:Repeater ID="rptThongTinTuyenSinh" runat="server">
                    <ItemTemplate>
                        <div class="col-sm-12 col-lg-6">
                            <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                <asp:Image runat="server" CssClass="img-hoatdong-dp" ID="Image2" Visible='<%# ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' AlternateText='<%#Eval("Title") %>' />
                                <a class="tieude-dv thongtin_ts-title-right" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%# Eval("Title") %></a>
                                <div class="linktip"><%#Eval("Summary") %></div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </div>
    </div>
</asp:Panel>
<%--Kết thục kiểu thông tin tuyển sinh--%>

<%--Hiển kiểu thư viện ảnh - video --%>
<asp:Panel runat="server" ID="pnlGalleryVideo" CssClass="nbcContent">
    <div class=" nopd">
        <div class="dichvuthongke document">
            <div class="dichvuthongke-text  anhvideo-title">
                <div>
                    <span>
                        <a href="/thu-vien-hinh-anh">Thư viện ảnh</a>
                        - <a href="/video">Video</a>
                    </span>
                </div>
                <span class="rounded-circle hide">
                    <img class="img_icon-right" src="/Data/Sites/1/skins/framework/images/icon_right.png" />
                </span>
            </div>
        </div>
        <div class="">
            <div class="row class-no-flex">
                <div class="col-sm-12 nopd nd-dv">
                    <div class="d-flex flex-column cds-content-left col-sm-12 col-lg-6 thuvien_anh-left">
                        <asp:Image ID="imgThuVienAnh" runat="server" CssClass="cds-left-img" />
                        <asp:HyperLink ID="hplThuVienAnh" runat="server" CssClass="thuvien_title-left"></asp:HyperLink>
                    </div>
                    <div class="col-sm-12 col-lg-6 thuvien_anh-right">
                        <asp:Repeater ID="rptThuVienAnh" runat="server">
                            <ItemTemplate>
                                <div class="col-sm-12 col-lg-6">
                                    <a href="<%# string.Format("{0}{1}",SiteRoot,Eval("ItemUrl").ToString().Replace("~/","")) %>">
                                        <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                        <div>
                                            <a class="tieude-dv" title="<%#Eval("NameGroup") %>" href="<%# string.Format("{0}{1}",SiteRoot,Eval("ItemUrl").ToString().Replace("~/","")) %>"><%# Eval("NameGroup") %></a>
                                        </div>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<%--Kết thúc kiểu thư viện ảnh và video--%>

<%--Hiển kiểu Tin tức - sự kiện--%>
<asp:Panel runat="server" ID="pnlTab5" CssClass="nbcContent">
    <div id="tab5">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            CallAjaxLoading("get", "/DieuTraArea/DieuTra/ListCuocDieuTra", null, true, function (rs) {
                $("#tab5").html(rs);
            });
        });
    </script>
</asp:Panel>
<%--Kết thục kiểu tab5 --- Hiển thị danh sách lĩnh vực điều tra các cuộc điều tra--%>

<div class="clear"></div>



<%-- Các phòng GD&ĐT và đơn vị trực thuộc sở --%>
<asp:Panel ID="pnlCacPhongDonVi" runat="server">
    <div class="width100 department fix_mb-20">
        <h3>
            <asp:HyperLink runat="server" ID="hplCategoryCacPhong" Text="Chưa có thiết lập" CssClass="nbchTitle"></asp:HyperLink>
        </h3>
        <ul class="donvi_tructhuoc-ul">
            <asp:Repeater ID="rptArticleCacPhong" runat="server">
                <ItemTemplate>
                    <li class="donvi_tructhuoc">
                        <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                            <i class="fa fa-angle-double-right" aria-hidden="true"></i>
                            <%#Eval("Title") %>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</asp:Panel>
<%-- End Các phòng GD&ĐT và đơn vị trực thuộc sở --%>

<%-- Danh sách trường --%>
<asp:Panel ID="pnlDanhSach" runat="server">
    <div class="nopd">
        <div class="school">
            <div class="school-title">
                <span class="school-title_main">Danh sách trường</span>
                <ul class="d-flex">
                    <asp:Repeater ID="rptDanhSachTruong" runat="server">
                        <ItemTemplate>
                            <li class="school-title_main-item">
                                <a title="<%#Eval("Name") %>" href="<%# string.Format("{0}{1}", SiteRoot, Eval("Description")) %>">
                                    <%#Eval("Name") %>
                                </a>

                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="school-list-content">
            <ul class="col-lg-12 col-sm-12">
                <asp:Repeater ID="rptCacTruong" runat="server">
                    <ItemTemplate>
                        <li>
                            <span><%#Container.ItemIndex + 1 %></span>
                            <div class="school-content__info">
                                <p>
                                    <a class="school__info-name" href="<%# string.Format("{0}{1}",SiteRoot,Eval("Description")) %>" title="<%#Eval("Name") %>"><%#Eval("Name") %></a>
                                </p>
                                <p>
                                    <a href="<%# string.Format("{0}{1}",SiteRoot,Eval("Description")) %>" title="<%#Eval("Name") %>">
                                        <%#Eval("Sumary") %>
                                    </a>
                                </p>
                                <p>
                                    <a href="<%# string.Format("{0}{1}",SiteRoot,Eval("Description")) %>" title="<%#Eval("Name") %>"><%#Eval("Description") %></a>
                                </p>

                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="read-more">
            <asp:HyperLink ID="hplMoreDanhSachTruong" runat="server" NavigateUrl="/">Xem thêm</asp:HyperLink>
            <span class="rounded-circle">
                <img class="img_icon-right-chil" src="/Data/Sites/1/skins/framework/images/icon_right.png" />
            </span>
        </div>
    </div>
</asp:Panel>
<%-- End Danh sách trường --%>

<%-- thành tích bảng vàng--%>
<asp:Panel ID="pnlThanhTichBangVang" runat="server">
    <div class="width100 bangvang_content">
        <div class="bangvang_title">
            <h3>BẢNG VÀNG</h3>
            <asp:HyperLink ID="hplThanhTichNamHoc" runat="server"></asp:HyperLink>
        </div>
        <div class="width100 bangvang_content-bot">
            <ul class="bangvang_banner-list">
                <asp:Repeater ID="rptThanhTichNamHoc" runat="server">
                    <ItemTemplate>
                        <li class="bangvang_banner-list-item">
                            <img src="<%#Eval("PathFile") %>" />
                            <div>
                                <p><%#Eval("Name") %></p>
                                <p><%#Eval("SubName") %></p>
                                <p><%#Eval("Sumary") %></p>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>


    </div>
</asp:Panel>
<%-- End thành tích bảng vàng --%>
<%-- liên kết website --%>
<asp:Panel ID="pnlLienKetWebsite" runat="server">
    <div class="width100 lienket">
        <asp:DropDownList ID="ddlLienKetWebsite" onchange="window.open(this.value, '_blank')" runat="server" CssClass="danhsach_lienket"></asp:DropDownList>
    </div>
</asp:Panel>
<%-- end liên kết website --%>



<%--Hiển kiểu tin tức sự kiện --%>
<asp:Panel runat="server" ID="pnlTinTucSuKien" CssClass="nbcContent">
    <div class="nopd">
        <div class="dichvuthongke document fix_title-tintuc">
            <div class="dichvuthongke-text document-title">
                <asp:HyperLink runat="server" ID="hplTinTucSuKien" Text="Chưa có thiết lập" CssClass="nbchTitle"></asp:HyperLink>
                <a id="hplMoreTinTucSuKien" title="Xem thêm" runat="server">
                    <span class="rounded-circle">
                        <img class="img_icon-right" src="/Data/Sites/1/skins/framework/images/icon_right.png" />
                    </span>
                </a>
            </div>
        </div>
        <div class="">
            <div class="row class-no-flex">
                <div class="col-sm-12 nopd nd-dv">
                    <asp:Repeater ID="rptTinTucSuKien" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-sm-12 tintuc_sukien-item">
                                <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                    <asp:Image runat="server" CssClass="img-hoatdong-dp" ID="Image2" Visible='<%# ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' AlternateText='<%#Eval("Title") %>' />

                                    <a class="tieude-dv" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%# Eval("Title") %></a>
                                    <div class="cds-content__right-time"><%#string.Format("{0:dd/MM/yyyy}", Eval("StartDate")) %></div>
                                    <div class="linktip">
                                        <%#Eval("Summary") %>
                                    </div>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<%--Kết thục kiểu tin tức sự kiện--%>


<%--Hiển kiểu gương sáng --%>
<asp:Panel runat="server" ID="pnlGuongSang" CssClass="nbcContent">
    <div class=" nopd">
        <div class="dichvuthongke document">
            <div class="dichvuthongke-text document-title">
                <asp:HyperLink runat="server" ID="hplGuongSang" Text="Chưa có thiết lập" CssClass="nbchTitle"></asp:HyperLink>
                <a id="hplMoreGuongSang" title="Xem thêm" runat="server">
                    <span class="rounded-circle">
                        <img class="img_icon-right" src="Data/Sites/1/skins/framework/images/icon_right.png" />
                    </span>
                </a>
            </div>
        </div>
        <div class="">
            <div class="row class-no-flex fix_guongsang-box">
                <div class="col-sm-12 nopd nd-dv fix_m-guongsang-box ">
                    <div class="d-flex flex-column cds-content-left col-sm-12 col-lg-6 thongtin_tuyensinh-left">
                        <asp:Image ID="imgArticleGuongSang" runat="server" />
                        <asp:HyperLink ID="hplArticleGuongSang" runat="server"></asp:HyperLink>
                        <span class="title_bot-ttts">
                            <asp:Literal ID="liArticleGuongSang" runat="server"></asp:Literal>
                        </span>
                    </div>
                    <div class="col-lg-6 col-sm-12 thongtin_ts-right">
                        <asp:Repeater ID="rptArticleGuongSang" runat="server">
                            <ItemTemplate>
                                <div class="col-sm-12 col-lg-6">
                                    <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">

                                        <asp:Image runat="server" CssClass="img-hoatdong-dp" ID="Image2" Visible='<%# ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' AlternateText='<%#Eval("Title") %>' />


                                        <a class="tieude-dv thongtin_ts-title-right" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%# Eval("Title") %></a>
                                        <%--<div class="linktip"><%#Eval("Summary") %></div>--%>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<%--Kết thúc kiểu gương sáng --%>


<%-- Hiển thị thông báo mới, văn bản mới, đọc nhiều nhất  --%>
<asp:Panel ID="pnlThongBaoVanBan" runat="server">
    <div class="thong-bao-moi">
        <div class="card">
            <div class="card-header spacer header-snip">
                <asp:HyperLink ID="hplThongBaoMoiNhat" runat="server" CssClass="text-uppercase bt-titct"></asp:HyperLink>

                <asp:HyperLink ID="hplThongBaoMoiMore" runat="server" CssClass="more-ctsn">
                    Xem thêm&nbsp;<i class="fa fa-angle-double-right" aria-hidden="true"></i>
                </asp:HyperLink>
            </div>
            <div class="card-body">
                <div class="row-cols-1 heightvanban">
                    <asp:Repeater ID="rptThongBaoMoi" runat="server">
                        <ItemTemplate>
                            <div class="col d-grid itemdocnhieu">
                                <a title="<%#Eval("Title") %>" class="text-justify txt-snipt hov-underline" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                    <%#Eval("Title") %>
                                </a>
                                <span class="timesnip">
                                    <asp:Image ID="img" runat="server" Visible='<%# bool.Parse(Eval("IsNew").ToString()) %>' ImageUrl="/data/images/iconnew.png" />
                                    <%#string.Format("{0:dd/MM/yyyy}",Eval("StartDate")) %></span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

    <div class="van-ban-moi">
        <div class="card">
            <div class="card-header spacer header-snip">
                <asp:HyperLink ID="hplVanBanMoiNhat" runat="server" CssClass="text-uppercase bt-titct"></asp:HyperLink>
                <asp:HyperLink ID="hplVanBanMoiMore" runat="server" CssClass="more-ctsn">
                    Xem thêm&nbsp;<i class="fa fa-angle-double-right" aria-hidden="true"></i>
                </asp:HyperLink>

            </div>
            <div class="card-body">
                <div class="row-cols-1 heightvanban">
                    <asp:Repeater ID="rptVanBanMoi" runat="server">
                        <ItemTemplate>
                            <div class="col d-grid itemdocnhieu">
                                <a title="<%#Eval("Summary") %>" class="text-justify txt-snipt hov-underline" href="<%#DocumentUltils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId,true,SiteRoot+"document/detail.aspx?item="+Eval("ItemID")+"&pageid=3986") %>">
                                    <%#Eval("Summary") %>
                                </a>
                                <span class="timesnip">
                                    <asp:Image ID="img" runat="server" Visible='<%# bool.Parse(Eval("IsNew").ToString()) %>' ImageUrl="/data/images/iconnew.png" />
                                    <%#string.Format("{0:dd/MM/yyyy}",Eval("ApprovedDate")) %></span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <div class="doc-nhieu-nhat">
        <div class="card">
            <div class="card-header spacer header-snip">
                <span class="text-uppercase bt-titct">Đọc nhiều nhất</span>
                <%--           <a id="ctl00_mainContent_ctl01_ctl00_hplVanBanMoiMore" class="more-ctsn" href="/van-ban">
                    Xem thêm&nbsp;<i class="fa fa-angle-double-right" aria-hidden="true"></i>
                </a>--%>
            </div>
            <div class="card-body body-docnhieu">
                <div class="heightvanban">
                    <asp:Repeater ID="rptDocNhieuNhat" runat="server">
                        <ItemTemplate>
                            <div class="itemdocnhieu">
                                <span class="a_left"><%#  (Container.ItemIndex + 1).ToString("00") %></span>
                                <span class="a_right">
                                    <a title="<%#Eval("Title") %>" class="text-justify txt-snipt hov-underline" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <%#Eval("Title") %>
                                    </a>
                                    <span class="timesnip"><%#string.Format("{0:dd/MM/yyyy}",Eval("StartDate")) %></span>
                                </span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<%-- kết thúc Hiển thị thông báo mới, văn bản mới, đọc nhiều nhất --%>
<%--Hiển thị các chuyên mục --%>
<asp:Panel ID="pnlHienThiCacChuyenMuc" runat="server">
    <section class="d-xl-flex justify-content-xl-center bg-cl width100">
        <div class="row row-cols-1 width1200 width100 margin0 white-back ct-v padding0">
            <div class="col padding0 gradient-1">
                <h1 class="text-uppercase b-title">Chuyên mục</h1>
            </div>
            <div class="col hol-itct padding0">
                <div class="row row-cols-3 hol-item">
                    <asp:Repeater ID="rptChuyenMucLienKet" runat="server">
                        <ItemTemplate>
                            <div class="col itclhol">
                                <div class="it-ct"><i class="fa fa-circle"></i><a class="txt-ct" title="<%#Eval("Title") %>" href="<%#Eval("UrlLink") %>"><%#Eval("Title") %></a></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>
</asp:Panel>
<%-- Kết thúc hiển thị các chuyên mục --%>

<asp:Panel ID="pnlChuyenMucCon" runat="server" CssClass="item-box-cate box-last">
    <div class="box-category box-cate-featured box-cate-featured-vertical">
        <!-- Tiêu đề chuyên mục -->
        <hgroup class="width_common title-box-category thoisu">
            <h2 class="parent-cate">
                <asp:HyperLink ID="hplChuyenMucCon" runat="server" CssClass="inner-title" />
            </h2>
            <asp:Repeater ID="rptChuyenMucCon1" runat="server">
                <ItemTemplate>
                    <span class="sub-cate">
                        <a href='<%# Eval("Description") %>' title='<%# Eval("Name") %>'><%# Eval("Name") %></a>
                    </span>
                </ItemTemplate>
            </asp:Repeater>
        </hgroup>

        <!-- Nội dung bài viết -->
        <div class="width_common content-box-category">
            <!-- Bài viết nổi bật -->
            <article class="item-news full-thumb">
                <div class="thumb-art">
                    <asp:Image ID="ImgChuyenMucCon" runat="server" CssClass="lazy" AlternateText="" />
                </div>
                <div class="width_common box-info-news">
                    <h3 class="title-news">
                        <asp:HyperLink ID="hplChuyenMucCon2" runat="server" />
                    </h3>
                    <p class="description">
                        <asp:Literal ID="liArticleChuyenMucCon" runat="server"></asp:Literal>
                    </p>
                </div>
            </article>

            <!-- Danh sách bài viết phụ -->
            <div class="sub-news-cate list-dotted">
                <asp:Repeater ID="rptChuyenMucCon2" runat="server">
                    <ItemTemplate>
                        <article class="item-news">
                            <h3 class="title-news">
                                <a class="tieude-dv" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" title="<%#Eval("Title") %>">
                                    <span class="dotted"></span><%# Eval("Title") %>
                                </a>
                            </h3>
                        </article>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Panel>


<!-- Dạng hiển thị chuyển trang -->

<asp:Panel ID="pnlTinSuKien" runat="server" CssClass="item-box-cate box-last">
    <div class="event-widget">
        <div class="event-header">
            <h3>
                <asp:HyperLink ID="hplChuyenMucTin" runat="server" CssClass="inner-title" />
            </h3>
            <div class="nav-buttons"> 
                <button class="nav-left" type="button">
                    <img src="/Data/Sites/1/skins/bentredefault/images/prev.png" alt="Prev" width="16" height="16" />
                </button>
                <button class="nav-right" type="button">
                    <img src="/Data/Sites/1/skins/bentredefault/images/next.png" alt="Next" width="16" height="16" />
                </button>
            </div>
        </div>
        <div class="event-list">

            <asp:Repeater ID="rptTinSuKien" runat="server">
                <ItemTemplate>
                    <div class="event-item" data-index='<%# Container.ItemIndex %>'>
                        <asp:Image runat="server" CssClass="img-hoatdong-dp" ID="Image2" Visible='<%# ArticleUtils.ShowImage(Eval("ImageUrl").ToString()) %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' AlternateText='<%#Eval("Title") %>' />
                        <div class="event-info">
                            <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" title="<%#Eval("Title") %>">
                                <%# Eval("Title") %>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Panel>

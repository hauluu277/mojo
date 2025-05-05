<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="NewRight.ascx.cs" Inherits="ArticleFeature.UI.NewRight" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<%--<script src="../../Data/Sites/1/skins/framework/jssor.slider-21.1.5.min.js"></script>--%>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper SlideList">
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <style>
                    .new-right-module .btn-tin{
                        color: #fff !important;
                    }
                    .new-right-module .btn-tin.active{
                        color: #91ecb1 !important;
                        border-bottom: 3px solid #91ecb1;
                    }
                    .new-right-content .nd-list-sukien {
                        display: none;
                    }
                    .new-right-content .nd-list-sukien.active {
                        display: block !important;
                    }
                </style>
                <%--Hiển thị tab kiểu tin mới, tin đọc nhiều --%>
                <asp:Panel runat="server" ID="pnlTinMoiTinDocNhieu" CssClass="nbcContent">
                    <div class="clearfix"></div>
                    <div class="new-right-module bg-sukien">
                        <asp:HyperLink runat="server" ID="hplTinMoi" Text="Tin mới" data-target=".nd-list-tinmoi" CssClass="sukien btn-tin btn_tinmoi active"></asp:HyperLink>
                        <i class="fa fa-circle dots_tintuc" aria-hidden="true"></i>
                        <asp:HyperLink runat="server" ID="HyperTinDocNhieu" Text="Tin đọc nhiều" data-target=".nd-list-tindocnhieu" CssClass="sukien btn-tin btn_tindocnhieu"></asp:HyperLink>
                    </div>
                    <div class="bg-sukien-bt new-right-content">
                        <ul class="nd-list-sukien nd-list-tinmoi js--animation__TinMoi">
                            <asp:Repeater ID="rptTinMoi" runat="server">
                                <ItemTemplate>
                                    <li class="nd-list-sukien-item">
                                        <div class="nd-sukien nd-list-tinmoi toggle-block fix_f">
                                            <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"> <span><%# Eval("Title") %></span> </a>
                                            <img class="nd-list-sukien-img" src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" alt="<%#Eval("Title") %>">
                                        </div>
                                        <asp:Panel ID="show" runat="server" Visible='<%#isShowSapo %>'>
                                            <%#Eval("Summary") %>
                                        </asp:Panel>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <ul class=" nd-list-sukien nd-list-tindocnhieu">
                            <asp:Repeater ID="rptTinDocNhieu" runat="server">
                                <ItemTemplate>
                                    <li class="nd-list-sukien-item">
                                        <div class="nd-sukien fix_f">
                                            <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"> <span><%# Eval("Title") %></span> </a>
                                            <img class="nd-list-sukien-img" src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" alt="<%#Eval("Title") %>">
                                        </div>
                                        <asp:Panel ID="show" runat="server" Visible='<%#isShowSapo %>'>
                                            <%#Eval("Summary") %>
                                        </asp:Panel>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <script>
                        $(document).ready(function () {
                            var anim = "<%=HieuUngTin%>";
                            var delay = "<%=ThoiGianChuyenDong%>";
                            startAnim(".js--animation__TinMoi", anim, delay);
                            function startAnim(el, anim, delay) {
                                setTimeout(function () {
                                    $(el).addClass("active");
                                    $(el).addClass(anim + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                                        $(this).removeClass(anim + ' animated');
                                    });
                                }, delay * 1000);
                            }

                            $(".new-right-module .btn-tin").on("click", function (e) {
                                e.preventDefault();
                                $(".new-right-module .btn-tin").removeClass("active");
                                $(this).addClass("active");
                                $(".new-right-content .nd-list-sukien").removeClass("active");
                                var target = $(this).attr("data-target");
                                $(target).addClass("active");
                            })
                        });
                    </script>
                </asp:Panel>
                <%--Kết thúc hiển thị tab kiểu tin mới, tin đọc nhiều--%>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>


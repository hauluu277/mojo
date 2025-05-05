<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RecentList.ascx.cs" Inherits="DuThaoVanBanFeature.UI.RecentList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Import Namespace="DuThaoVanBanFeature.UI" %>
<script type="text/javascript">
    function SearchMore() {
        $(".advanced-search").toggleClass("show-div");
    }

</script>
    <div class="module">
        <div class="module-table-body">
            <h3 class="draft-header">Tìm kiếm văn bản dự thảo văn bản</h3>
            <div class="draft-search">
                <div class="quick-search">
                    <input type="text" id="txtKeyword" runat="server" class="form-control" />
                    <button type="submit" id="btnSearch" runat="server"><i class="fa fa-search"></i>&nbsp;Tìm kiếm</button>
                    <button type="button" class="search-more" onclick="SearchMore()"><i class="fa fa-ellipsis-v"></i></button>
                </div>
                <div class="advanced-search">
                    <div class="col-sm-12 col-md-6 no-padding">
                        <div class="col-sm-12 col-md-6 no-padding">
                            <label class="">Cơ quan ban hành</label>
                            <asp:DropDownList ID="ddlCoQuanBanHanh" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-sm-12 col-md-6 no-padding">
                            <label class="">Loại văn bản</label>
                            <asp:DropDownList ID="ddlLoaiVanBan" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-6 no-padding control-full">
                        <label class="">Lĩnh vực</label>
                        <asp:DropDownList ID="ddlLinhVuc" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="draft">
                <div class="info-list">
                    <i class="fa fa-calendar-check-o" aria-hidden="true"></i>&nbsp; VĂN BẢN ĐANG CÒN HẠN LẤY Ý KIẾN
                </div>
                <table class="table table-striped table-bordered table-hover table-condensed">
                    <asp:Repeater ID="rptDuThao" runat="server" SkinID="Article">
                        <HeaderTemplate>
                            <tr>
                                <th>Tiêu đề văn bản</th>
                                <th>Loại văn bản</th>
                                <th>Lĩnh vực</th>
                                <th>Ngày kết thúc</th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width: 45%">
                                    <div class="lstDuThao">
                                        <asp:HyperLink ID="lnkDetail" runat="server" CssClass="tit-DuThao" EnableViewState="false" Visible="true" NavigateUrl='<%#DuThaoVanBanUltils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'><%#Eval("Title") %></asp:HyperLink>
                                        <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>" Visible='<%#IsEditable %>' ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>' CssClass="ModuleEditLink" />
                                    </div>
                                </td>
                                <td>
                                    <%-- Loại văn bản --%>
                                    <%#Eval("LoaiVBName") %>
                                </td>
                                <td>
                                    <%-- Lĩnh vực --%>
                                    <%#Eval("LinhVucName") %>
                                </td>
                                <td>
                                    <%-- Ngày kết thúc --%>
                                    <%#Convert.ToDateTime(Eval("EndDate")).ToString("dd/MM/yyyy") %>
                                </td>
                                <td class="text-center">
                                    <a class="hyperlink" href='<%#DuThaoVanBanUltils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' title="Xem chi tiết">Chi tiết</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="pnlDuThaoPager" runat="server" CssClass="ArticlePager">
                    <portal:mojoCutePager ID="pgrArticle" runat="server" />
                </asp:Panel>
            </div>
            <div class="draft m-top30">
                <div class="info-list">
                    <i class="fa fa-calendar-times-o" aria-hidden="true"></i>&nbsp; VĂN BẢN ĐÃ HẾT HẠN LẤY Ý KIẾN
                </div>
                <table class="table table-striped table-bordered table-hover table-condensed">
                    <asp:Repeater ID="rptDraftExpires" runat="server" SkinID="Article">
                        <HeaderTemplate>
                            <tr>
                                <th>Tiêu đề văn bản</th>
                                <th>Loại văn bản</th>
                                <th>Lĩnh vực</th>
                                <th>Ngày kết thúc</th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width: 45%">
                                    <div class="lstDuThao">
                                        <asp:HyperLink ID="lnkDetail" runat="server" CssClass="tit-DuThao" EnableViewState="false" Visible="true" NavigateUrl='<%#DuThaoVanBanUltils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'><%#Eval("Title") %></asp:HyperLink>
                                        <asp:HyperLink ID="editLink" runat="server" EnableViewState="false" Text="<%# EditLinkText %>" Visible='<%#IsEditable %>' ImageUrl='<%# EditLinkImageUrl %>' NavigateUrl='<%# BuildEditUrl(Convert.ToInt32(Eval("ItemID"))) %>' CssClass="ModuleEditLink" />
                                    </div>
                                </td>
                                <td>
                                    <%-- Loại văn bản --%>
                                    <%#Eval("LoaiVBName") %>
                                </td>
                                <td>
                                    <%-- Lĩnh vực --%>
                                    <%#Eval("LinhVucName") %>
                                </td>
                                <td>
                                    <%-- Ngày kết thúc --%>
                                    <%#Convert.ToDateTime(Eval("EndDate")).ToString("dd/MM/yyyy") %>
                                </td>
                                <td class="text-center">
                                    <a class="hyperlink" href='<%#DuThaoVanBanUltils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' title="Xem chi tiết">Chi tiết</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="pnDuThaoEx" runat="server" CssClass="ArticlePager">
                    <portal:mojoCutePager ID="pgrDuThaoEx" runat="server" />
                </asp:Panel>
            </div>

        </div>
    </div>


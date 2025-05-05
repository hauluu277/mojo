<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EventView.ascx.cs" Inherits="EventFeature.UI.EventView" %>
<%@ Import Namespace="EventFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="center-pane clearfix">

    <div class="blogwrapper">
        <%--    <h2 id="module146" class="box-other-title"><span class="art-postheadericon">
        <mp:SiteLabel ID="lblOtherArticle" runat="server" ConfigKey="EventInformationTilte" ResourceFile="EventResources"></mp:SiteLabel>
    </span></h2>--%>
        <asp:Panel ID="divblog" runat="server" CssClass="articlecenter-rightnav format-event" SkinID="plain">
            <asp:Panel ID="pnlScrollable" runat="server">
                <div class="list-article">
                    <%--  <div class='img-article2'>
                    <asp:Image ID="image1" Width="100%" Height="100%" runat="server" />
                </div>--%>
                    <div>
                        <h3 class="article-title">
                            <asp:HyperLink SkinID="BlogTitle" ID="HyperLink1" runat="server" EnableViewState="false"></asp:HyperLink>
                            <asp:Literal ID="Literal1" runat="server" />
                            <asp:HyperLink ID="HyperLink2" runat="server" EnableViewState="false" CssClass="ModuleEditLink" />
                        </h3>
                        <div>
                            <span class="article-author"><b>
                                <asp:Label ID="lblCreatedByUser" runat="server"></asp:Label>
                            </b></span>
                            <div class="LichSuKien">
                                <div class="LichSuKien__Time">
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblTime" runat="server"></asp:Label></th>
                                                <th>Địa điểm</th>
                                            </tr>
                                        </thead>
                                        <tr>
                                            <td>
                                                <i class="fa fa-clock-o"></i>
                                                <span class="bdate">Từ
                                                <asp:Label ID="lblStartDate" runat="server"></asp:Label></span>
                                                <span class="bdate">đến
                                                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                            <td>
                                                <i class="fa fa-map-marker"></i>
                                                <span class="dia-diem">
                                                    <asp:Label runat="server" ID="lblDiaDiem"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <span class="mo-ta">
                                <asp:Literal ID="literDescription" runat="server"></asp:Literal>
                            </span>
                        </div>
                        <asp:Label ID="lblSummary" Visible="false" runat="server"></asp:Label>
                        <asp:Label ID="lblMoreLink" Visible="false" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</div>
<div class="right-pane pdr0 Event__Right">
    <div class="blockevent_other doc_slide__tinnoibat">
        <div class="divorther">
            <div class="divorthertitle">
                <label>Sự kiện khác</label>
            </div>
        </div>
        <%--        <h3 id="module145" class="box-other-title"><span class="art-postheadericon">Sự kiện khác
        </span></h3>--%>
        <div class="list-article">
            <ul class="ulArticleOrther">
                <asp:Repeater ID="rptEventRecent" runat="server" SkinID="Blog" EnableViewState="False">
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                ToolTip='<%# Eval("Title") %>' Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                NavigateUrl='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                            </asp:HyperLink>

                            <span class="dateArticleOrther">(<%# FormatArticleDate(Convert.ToDateTime(Eval("StartTime"))) %>
                                -
                                <%# FormatArticleDate(Convert.ToDateTime(Eval("EndTime"))) %>
                                    )
                            </span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>

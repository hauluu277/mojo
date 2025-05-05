<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PostList.ascx.cs" Inherits="EventFeature.UI.PostList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="search-box">
    <fieldset class="fieldset">
        <legend class="legend" runat="server" id="legendSearchProperty"></legend>
        <div class="col-sm-12 form-group">
            <div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
                <mp:SiteLabel ID="lblStatus" runat="server" ForControl="ddlPublishStatus"
                    ConfigKey="EventStatusPublishLabel" ResourceFile="EventResources"></mp:SiteLabel>
                <asp:DropDownList Width="100%" ID="ddlPublishStatus" runat="server"></asp:DropDownList>
            </div>
            <div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
                <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword"
                    ConfigKey="ArticleEditKeywordLabel" ResourceFile="EventResources"></mp:SiteLabel>
                <asp:TextBox ID="txtKeyword" Width="100%" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
                <br />
                <portal:mojoButton ID="btnSearch" runat="server" />

            </div>
        </div>
    </fieldset>
</div>

<%-- begin toolbar-box --%>
<div id="toolbar-box">
    <div class="tool-btn">
        <portal:mojoButton ID="btnaddnew" SkinID="ButtonPrimary" runat="server" OnClick="btnaddnew_Click" />
        <portal:mojoButton ID="btnDelAll" SkinID="ButtonDanger" runat="server" OnClick="btnDelAll_Click" />
    </div>
</div>
<%-- end toolbar-box --%>

<div class="module">
    <div class="module-table-body">
        <asp:Repeater ID="rptEvents" runat="server" SkinID="Article" OnItemCommand="rptEvents_ItemCommand" OnItemDataBound="rptEvents_ItemDataBound">
            <HeaderTemplate>
                <table class="table table-bordered table-striped table-hover" id="myTable">
                    <tr>
                        <th style="width: 5%">
                            <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" /></th>
                        <th style="width: 50%" class="tbl-header">
                            <%#Resources.EventResources.HeaderArticleTitle %>
                        </th>
                        <th style="width: 15%" class="tbl-header">Thời gian hiển thị</th>
                        <th style="width: 15%" class="tbl-header">Thời gian sự kiện</th>
                        <th style="width: 15%" class="tbl-header">
                            <%#Resources.EventResources.PlaceTitle%>
                        </th>
                        <th class="tbl-header">
                            <%#Resources.EventResources.PublishedTilte%>
                        </th>
                        <th style="width: 5%" class="tbl-header"></th>
                    </tr>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("ItemID") %>' Visible="false"></asp:Literal>
                        <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                    </td>
                    <td>
                        <div style="float: left; width: 25%; margin-right: 2%;" class='<%# "article-logo" + ModuleId + Eval("ItemID") %>' title='<%# Eval("Title") %>'>

                            <asp:Image ID="image2" runat="server" ImageUrl='<%# mojoPortal.Features.EventUtils.FormatImageDialog(ConfigurationManager.AppSettings["EventImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                Visible='<%# Config.ShowImage %>' CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' Width="100%" />

                        </div>
                        <div style="float: left; width: 70%;">
                            <div class="article-title">
                                <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                    ToolTip='<%# Eval("Title") %>' Text='<%# EventUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                    Visible='<%# Config.UseLinkForHeading %>' NavigateUrl='<%# EventUtils.FormatBlogTitleUrl(siteSettings.SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>' Target=''>
                                </asp:HyperLink>
                            </div>
                            <p class="author">
                                Đăng bởi:
                                <%#Eval("CreatedByUser") %>
                               | Ngày: <%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %>
                            </p>
                            <%# Eval("Summary") %>
                    </td>
                    <td><%#string.Format("{0} - {1}",string.Format("{0:dd/MM/yyyy HH:mm}",Eval("StartDate")),string.Format("{0:dd/MM/yyyy HH:mm}",Eval("EndDate"))) %></td>
                    <td><%#string.Format("{0} - {1},  {2}",string.Format("{0:HH:mm}",Eval("StartTime")),string.Format("{0:HH:mm}",Eval("EndTime")),string.Format("{0:dd/MM/yyyy}",Eval("StartTime"))) %></td>
                    <td><%#Eval("Location") %></td>
                    <td class="text-center">
                        <asp:HyperLink ID="approveLink" runat="server" EnableViewState="false" Text=""
                            ToolTip="" ImageUrl='<%# mojoPortal.Features.EventUtils.ImageApprove(DataBinder.Eval(Container.DataItem,"IsPublished").ToString()) %>' Visible="true" />
                    </td>
                    <td class="text-center">
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                            CommandName="EditItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# EditLinkText %>"
                            CausesValidation="false" />

                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                            CommandName="DeleteItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="<%# DeleteLinkText %>"
                            CausesValidation="false" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlEventPager" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrEvent" runat="server" />
        </asp:Panel>
    </div>
</div>


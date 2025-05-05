<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DocumentList.ascx.cs" Inherits="DocumentFeature.UI.DocumentList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:HeadingControl ID="heading" runat="server" />
<style>
    .CountLinhVuc ul {
        list-style: none;
    }

        .CountLinhVuc ul li {
            width: 33%;
            float: left;
        }
</style>
<div class="panel panel-border-title">
    <div class="panel-heading">
        <div>Tìm kiếm văn bản</div>
    </div>
    <%--    <div class="CountLinhVuc">
        <ul>
            <asp:Repeater ID="rptCountLinhVuc" runat="server">
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="lnkLinhVuc" CssClass="doc_link_search" runat="server" NavigateUrl='<%#Eval("Url") %>'><%#Eval("Name") %><%#Eval("Count") %></asp:HyperLink>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>--%>
    <div class="panel-body">
        <div class="search-item">
            <label class="search-label2">Lĩnh vực</label>
            <div class="search-control">
                <asp:DropDownList ID="ddlLinhVuc" runat="server" CssClass="dlldata" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
        <div class="search-item">
            <mp:SiteLabel ID="lblLoai" runat="server" ForControl="ddlLoaiVB" CssClass="search-label2"
                ConfigKey="LoaiVBHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
            <div class="search-control">
                <asp:DropDownList ID="ddlLoaiVB" runat="server" CssClass="dlldata"></asp:DropDownList>
            </div>
        </div>
        <div class="search-item">
            <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="ddlCoQuan" CssClass="search-label2"
                ConfigKey="CoQuanIDHeaderLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
            <div class="search-control">
                <asp:DropDownList ID="ddlCoQuan" runat="server" CssClass="dlldata"></asp:DropDownList>
            </div>
        </div>
        <div class="search-item">
            <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="ddlNam" CssClass="search-label2"
                ConfigKey="NamLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
            <div class="search-control">
                <asp:DropDownList CssClass="dlldata" ID="ddlNam" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="search-item" style="width: 100%; margin: 0">
            <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label2"
                ConfigKey="KeywordLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
            <div class="search-control">
                <asp:TextBox ID="txtKeyword" runat="server" CssClass="txtdatasearch"></asp:TextBox>
            </div>
        </div>
        <div class="searchSubmit2 width100 text-center" style="margin-top: 20px">
            <portal:mojoButton ID="btnSearch" SkinID="SearchButton" runat="server" />
            &nbsp;&nbsp;
                <portal:mojoButton ID="btnReset" SkinID="ResetButton" runat="server" />
        </div>
    </div>
</div>
<div class="module">
    <div class="module-table-body">
        <div class="width100 font-bold text-right">
            <asp:Literal ID="lblTotalVanBan" runat="server"></asp:Literal>
        </div>
        <asp:Repeater ID="rptArticles" runat="server" SkinID="Article">
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                    <tr>
                        <%--<th>STT</th>--%>
                        <th class="tbl-header">Số ký hiệu
                        </th>
                        <th class="tbl-header">Cơ quan banh hành
                        </th>
                        <th class="tbl-header">Loại văn bản
                        </th>
                        <th class="tbl-header">Lĩnh vực</th>
                        <th>Trích yếu</th>
                        <th>Ngày ban hành</th>
                        <td></td>
                    </tr>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <%--                    <td>
                        <%#rptArticles.Items.Count +1%>
                    </td>--%>
                    <td>
                        <%#Eval("Sign") %>
                    </td>
                    <td><%#Eval("CoQuanName") %></td>
                    <td><%#Eval("LoaiVBName") %></td>
                    <td><%#Eval("LinhVucName") %></td>
                    <td>
                        <asp:HyperLink ID="lnkDetail" runat="server" CssClass="doc_detail" EnableViewState="false" Visible="true" NavigateUrl='<%#DocumentUltils.FormatBlogTitleUrl(siteSettings.SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'><%#formatContent(Eval("Summary").ToString()) %></asp:HyperLink></td>
                    <td><%#(Convert.ToDateTime(Eval("DatePromulgate"))).ToString("dd/MM/yyyy", CultureInfo.CurrentUICulture)%></td>
                     <td>
                        <a href='<%#DownloadFile(Eval("FilePath").ToString()) %>' style='<%# Eval("FilePath") == null? "display:none": string.Empty %>' download title="Tải xuống">
                            <i class="fa fa-download" aria-hidden="true"></i>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlArticlePager" runat="server" CssClass="ArticlePager">
            <portal:mojoCutePager ID="pgrArticle" runat="server" />
        </asp:Panel>
    </div>
</div>

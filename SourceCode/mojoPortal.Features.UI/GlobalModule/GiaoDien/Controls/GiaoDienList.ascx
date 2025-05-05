<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="GiaoDienList.ascx.cs" Inherits="GiaoDienFeature.UI.GiaoDienList" %>
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
<link href="/Data/plugins/tree/tree.css" rel="stylesheet" />
<script src="/Data/plugins/tree/tree.js"></script>
<div class="col-sm-4">
      <div id="tree"></div>
</div>
<%--<div class="col-sm-8">
    <div class="panel panel-border-title">
        <div class="panel-heading">
            <div>Tìm kiếm thủ tục hành chính</div>
        </div>
        
        <div class="panel-body">
            <div class="search-item">
                <label class="search-label2">Từ khóa</label>
                <div class="search-control">
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="search-item">
                <label class="search-label2">Lĩnh vực:</label>
                <div class="search-control">
                    <asp:DropDownList ID="ddlLinhVuc" runat="server" CssClass="dlldata" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <label class="search-label2">Mức độ DVC:</label>
                <div class="search-control">
                    <asp:DropDownList ID="ddlMucDoDVC" runat="server" CssClass="dlldata" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <label class="search-label2">Cấp thủ tục:</label>
                <div class="search-control">
                    <asp:DropDownList ID="ddlCapThuTuc" runat="server" CssClass="dlldata" AutoPostBack="true"></asp:DropDownList>
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
                            <th>STT</th>
                            <th class="tbl-header">Mã TTHC
                            </th>
                            <th class="tbl-header">Mức độ DVC
                            </th>
                            <th class="tbl-header">Tên thủ tục hành chính
                            </th>
                            <th class="tbl-header">Lĩnh vực</th>
                        </tr>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#rptArticles.Items.Count + 1%>
                        </td>
                        <td>
                            <%#Eval("MaThuTuc") %>
                        </td>
                        <td>
                            <button type="button" disabled="disabled" class="gt-dvc-lv2">
                                <%#Eval("MucDoName") %>
                            </button>
                        </td>
                        <td>
                            <a title="<%#Eval("TenThuTuc") %>" href="<%#string.Format("{0}/GlobalModule/GiaoDien/ViewPost.aspx?itemid={1}",SiteRoot,Eval("ItemID")) %>"><%#Eval("TenThuTuc") %></a>
                        </td>
                        <td><%#Eval("LinhVucName") %></td>
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
</div>--%>

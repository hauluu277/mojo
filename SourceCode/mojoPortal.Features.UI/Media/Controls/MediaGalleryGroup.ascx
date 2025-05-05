<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MediaGalleryGroup.ascx.cs" Inherits="MediaFeature.UI.MediaGalleryGroup" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="breadcrumb" style="display:none">
    <div class="breadcrumb-item">
        <asp:Label ID="lblDictionaryTitle" runat="server"></asp:Label>
    </div>
    <div class="form-search" style="display: none">
        <div class="search-wrapper cf">
            <input type="text" id="txtSearch2" runat="server" placeholder="Search here...">
            <button id="btnSearch2" runat="server" onserverclick="btnSearch2_Click">Search</button>
        </div>
    </div>
</div>
<div class="all-list">
    <div class="gallery">
        <div>
            <img src="../Data/Sites/117/skins/framework/images/logo-gallery-tt.jpg" />
            <h3>Gallery</h3>
            <asp:Label ID="lblSlogan" runat="server"></asp:Label>
        </div>
    </div>
        <asp:Literal ID="literVideo" runat="server"></asp:Literal>
   <ul>
        <asp:Repeater ID="dtlData" runat="server">
            <ItemTemplate>
                <li>
                    <div class="main-data">
                        <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                            <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                        </a>
                        <div class="des-item">
                            <span class="text-left">
    <%--                            <span style="font-size: 12px; color: #a2a2a2;" id="createDate_<%#Eval("ItemID") %>">
                                    <%#Eval("CreatedDate") %>
                                </span>--%>
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
<asp:Panel ID="pnlDonViPager" runat="server" CssClass="ArticlePager">
    <portal:mojoCutePager ID="pgrDanhBa" runat="server" />
</asp:Panel>
<asp:Label ID="DanhBanull" runat="server" Visible="false"></asp:Label>



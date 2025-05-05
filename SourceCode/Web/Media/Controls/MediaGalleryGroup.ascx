<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MediaGalleryGroup.ascx.cs" Inherits="MediaFeature.UI.MediaGalleryGroup" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<div class="breadcrumb" style="display:none">
    <div class="breadcrumb-item">
        <asp:Label ID="lblDictionaryTitle" runat="server" ViewStateMode="Disabled" EnableViewState="false"></asp:Label>
    </div>
    <div class="form-search" style="display: none">
        <div class="search-wrapper cf">
            <input type="text" id="txtSearch2" runat="server" ViewStateMode="Disabled" EnableViewState="false" placeholder="Search here...">
            <button id="btnSearch2" runat="server" ViewStateMode="Disabled" EnableViewState="false" onserverclick="btnSearch2_Click">Search</button>
        </div>
    </div>
</div>
<div class="galery__all__list">
    <div class="gallery__head__slogan">
        <div>
            <img src="../Data/Sites/117/skins/framework/images/logo-gallery-tt.jpg" />
            <h3>Gallery</h3>
            <asp:Label ID="lblSlogan" runat="server" ViewStateMode="Disabled" EnableViewState="false"></asp:Label>
        </div>
    </div>
    <h3>THƯ VIỆN ẢNH</h3>
     <ul>
        <asp:Repeater ID="dtlData" runat="server" ViewStateMode="Disabled" EnableViewState="false">
            <ItemTemplate>
                <li>
                    <div class="gallery__main__data">
                        <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                            <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                        </a>
                        <div class="gallery__des-item">
                            <span class="text-left">
    <%--                            <span style="font-size: 12px; color: #a2a2a2;" id="createDate_<%#Eval("ItemID") %>">
                                    <%#Eval("CreatedDate") %>
                                </span>--%>
                                <span class="gallery__des-fix">
                                    <%#Eval("NameGroup") %>
                                </span>
                            </span>
                        </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <h3>VIDEO</h3>
    <div class="gallery__video">
        <asp:Literal ID="literVideo" runat="server" ViewStateMode="Disabled" EnableViewState="false"></asp:Literal>
    </div>
  
</div>



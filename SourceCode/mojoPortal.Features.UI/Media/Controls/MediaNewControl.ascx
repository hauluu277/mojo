<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MediaNewControl.ascx.cs" Inherits="MediaFeature.UI.MediaNewControl" %>
<%@ Import Namespace="MediaFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Import Namespace="mojoPortal.Business.WebHelpers" %>
<%--tab1 hiển thị Ảnh mới--%>
<asp:Panel ID="pnlTab1" runat="server">
    <div class="video contentCol_Right_common"> 
        <h2><img src="../Data/Images/bdbnd/gihan_camera.png" />
            <asp:HyperLink ID="hpllCategory" runat="server"></asp:HyperLink>
        </h2>
        <div class="video_ct">
            <asp:Image ID="imgNew" runat="server" />
            <h3><asp:HyperLink ID="hplMedia" runat="server"></asp:HyperLink></h3>
        </div>
    </div>
</asp:Panel> 


<%--tab 2 hiển thị danh sách ảnh --%>
<asp:Panel ID="pnlTab2" runat="server">
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

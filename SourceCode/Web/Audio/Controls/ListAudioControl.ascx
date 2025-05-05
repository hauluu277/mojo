<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ListAudioControl.ascx.cs" Inherits="AudioFeature.UI.ListAudioControl" %>
<%@ Import Namespace="AudioFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<div class="row">
    <div class="col-sm-12">
        <div class="row">
            <asp:Repeater ID="rptAudio" runat="server" SkinID="Blog">
                <ItemTemplate>
                    <div class="col-sm-6">
                        <div class="thumbnail">
                            <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                            </a>
                            <div class="caption">
                                <h3><%#Eval("NameGroup") %></h3>
                                <p><%#Eval("CreatedByUser") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="pnlAudioPager" runat="server" CssClass="ArticlePager">
                <portal:mojoCutePager ID="pgrAudio" runat="server" />
            </asp:Panel>
        </div>
    </div>
</div>



<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AudioListControl.ascx.cs" Inherits="AudioFeature.UI.AudioListControl" %>
<%@ Import Namespace="AudioFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<div class="row">
    <div class="col-sm-12">

        <div class="row row-bigImage">
            <div class="col-sm-12">
                <div class="box-bigImage">
                    <div class="box-bigImage-Left">
                        <asp:HyperLink ID="hplFirst" runat="server">
                            <asp:Image ID="imgFirst" runat="server" />
                        </asp:HyperLink>
                    </div>

                    <div class="box-bigImage-Right">
                        <p>
                            <asp:Label ID="lblname" runat="server"></asp:Label>
                        </p>
                        <p>
                            <asp:Literal ID="liFirst" runat="server"></asp:Literal>
                        </p>
                        <p>
                            <asp:Label ID="lblNguoiDang" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <asp:Repeater ID="rptAudio" runat="server" SkinID="Blog">

                <ItemTemplate>
                    <div class="col-sm-6 mg-bt-5">
                        <div class="box-child">
                            <div class="box-image-child">
                                <a href='<%# MediaUtils.FormatAudioTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                    <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                </a>
                            </div>

                            <div class="box-content-child">
                                <h3><%#Eval("NameGroup") %></h3>
                                <p><%#Eval("Sapo") %></p>
                                <p><%#Eval("CreatedByUser") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <asp:Panel ID="pnlAudioPager" runat="server" CssClass="ArticlePager">
        <portal:mojoCutePager ID="pgrAudio" runat="server" />
    </asp:Panel>
</div>

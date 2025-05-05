<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="FileHomeModule.ascx.cs" Inherits="FileHomeFeature.UI.FileHomeModule" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper blogmodule">
                <portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
                <div class="cleared bg-video-tc">
                    <div class="container pdvideo-tc">
                        <div class="class-video-hd">
                            <div class="text-videohd">Video hướng dẫn nghiệp vụ</div>
                            <div class="video-tc">
                                <asp:Literal ID="literVideo" runat="server"></asp:Literal>
                                <span style="display:flex">
                                    <img src="../../Data/Sites/137/skins/framework/images/Untitled-8.png"  class="logoimg-video-hd"/>
                                    <asp:HyperLink ID="hplVideo" runat="server"></asp:HyperLink></span>
                            </div>
                        </div>
                        <div class="class-tailieu-hd">
                            <asp:Label ID="lblCategory" CssClass="tieude-tailieu" runat="server"></asp:Label>

                            <ul>
                                <asp:Repeater ID="rptItem" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a href="<%#string.Format("{0}{1}",SiteRoot, Eval("Description")) %>" title="<%#Eval("Name") %>">
                                                <img src="<%#Eval("PathIMG") %>" />
                                                <%#Eval("Name") %>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

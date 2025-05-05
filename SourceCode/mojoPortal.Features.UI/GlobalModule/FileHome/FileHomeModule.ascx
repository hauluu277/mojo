<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="FileHomeModule.ascx.cs" Inherits="FileHomeFeature.UI.FileHomeModule" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper blogmodule">
                <portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
                <div class="cleared">
                    <div>
                        <h3>Video hướng dẫn nghiệp vụ</h3>
                        <div>
                            <asp:Literal ID="literVideo" runat="server"></asp:Literal>
                            <asp:HyperLink ID="hplVideo" runat="server"></asp:HyperLink>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblCategory" runat="server"></asp:Label>
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
            </asp:Panel>
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

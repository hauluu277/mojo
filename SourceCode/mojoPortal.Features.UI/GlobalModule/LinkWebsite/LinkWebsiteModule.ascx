<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LinkWebsiteModule.ascx.cs" Inherits="LinkWebsiteFeature.UI.LinkWebsiteModule" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper blogmodule">
                <portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
                <div class="cleared">
                    <%-- class="class-select"--%>
                    <div>
                        <div class="class-search">
                            <div class="o-tren">
                                <div class="lien-ket-w">
                                    <asp:Label ID="lblLienKetWebsite" runat="server"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlLink" runat="server"></asp:DropDownList>
                            </div>
                            <div class="o-duoi">
                                <asp:Repeater ID="rptDanhMuc" runat="server">
                                    <ItemTemplate>
                                        <div class="duoi-lienket">
                                            <a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>">
                                                <div class="img-logo-lk">
                                                    <img src="<%#Eval("PathIMG") %>" alt="<%#Eval("Name") %>" />
                                                </div>
                                                <div class="class-text-lk"><a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>"><%#Eval("Name") %></a></div>
                                            </a>
                                        </div>
                                        <div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

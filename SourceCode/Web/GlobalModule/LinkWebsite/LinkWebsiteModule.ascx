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
                            <div class="o-tren link-lienket">
                                <div class="lien-ket-w">
                                    <asp:Label ID="lblLienKetWebsite" runat="server"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlLink" runat="server"></asp:DropDownList>
                            </div>
                            <div class="o-duoi">
                                <asp:Repeater ID="rptDanhMuc" runat="server">
                                    <ItemTemplate>
                                        <div class="duoi-lienket">
                                            <a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>" target="<%#Convert.ToBoolean( Eval("TargetBlank")) == true ?"_blank":"" %>">
                                                <div class="img-logo-lk">
                                                    <img src="<%#Eval("PathIMG") %>" alt="<%#Eval("Name") %>" />
                                                </div>
                                                <div class="class-text-lk">
                                                    <a href="<%#string.Format("{0}{1}",SiteRoot,Eval("Description")) %>">
                                                        <%# mojoPortal.Business.WebHelpers.Ultilities.LoadTitle(Eval("Name"),Eval("SubName")) %>
                                                    </a>
                                                </div>
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
                <script type="text/javascript"> 
                    $(".link-lienket select").change(function () {
                        var url = $(this).val();
                        console.log(url);
                       window.open(url, '_blank');
                    })
                </script>
            </asp:Panel>
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

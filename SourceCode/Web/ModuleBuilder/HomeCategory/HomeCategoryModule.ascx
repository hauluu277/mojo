<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HomeCategoryModule.ascx.cs" Inherits="HomeCategoryFeature.UI.HomeCategoryModule" %>
<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <div class="modulecontent">
                <section class="awe-section-3">
                    <section class="constructo-features-area section-padding">
                        <div class="container">
                            <div class="row">
                                <asp:Repeater ID="rptCategory" runat="server">
                                    <ItemTemplate>
                                        <section class="col-sm-6 col-md-3 text-center wow slideInLeft" data-wow-duration="1s">
                                            <div class="single-features">
                                                <div class="features-icon">
                                                    <img src="<%#Eval("ItemIcon") %>" class="ficon3" alt="<%#Eval("Title") %>">
                                                </div>
                                                <h4><a href="<%#SiteRoot+"/"+ Eval("ItemUrl") %>" title="<%#Eval("Title") %>"><%# Eval("Title") %></a></h4>
                                                <p><%#Eval("Description").ToString() %></p>
                                            </div>
                                        </section>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </section>
                </section>

            </div>
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

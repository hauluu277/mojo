<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CategoryMenuModule.ascx.cs" Inherits="ArticleFeature.UI.CategoryMenuModule" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server">
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper blogmodule">
                <portal:ModuleTitleControl ID="Title1" runat="server" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
                    <div class="modulecontent">
                        <div class="container bck-menu col-md-3 content-menu-3ck">
                            <div class="panel-group " id="accordion_1">
                                <asp:Repeater ID="rptMenuCategory" runat="server">
                                    <ItemTemplate>
                                        <div class="panel panel-default">
                                            <div class="panel-heading <%#Container.ItemIndex == 0?"more":string.Empty  %>">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion_1" href="#category_<%#Eval("ItemID") %>"><%#Eval("Name") %></a>
                                                </h4>
                                            </div>
                                            <div id="category_<%#Eval("ItemID") %>" class="panel-collapse collapse <%#Container.ItemIndex == 0?"in":string.Empty  %>">
                                                <div class="panel-body menu-category-article">
                                                    <ul>
                                                        <asp:Repeater ID="rptCategoryChild" runat="server" DataSource='<%#LoadCategoryChild(Convert.ToInt32(Eval("ItemID")))%>'>
                                                            <ItemTemplate>
                                                                <li>
                                                                    <a href='<%# SiteRoot+Eval("Description")  %>' title='<%#Eval("Name") %>'><%#Eval("Name") %></a>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </portal:mojoPanel>
                <div class="cleared">
                </div>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</portal:OuterWrapperPanel>

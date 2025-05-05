<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="HomeRightModule.ascx.cs" Inherits="HomeCategoryFeature.UI.HomeRightModule" %>
<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <style type="text/css">
                .hn_p_panel .panel-heading {
                    background: #d5d5d5;
                    border-radius: 0px;
                    text-align: center;
                    text-transform: uppercase;
                    height: 60px;
                    font-size: 20px;
                    padding-top: 20px;
                    font-family: 'Roboto';
                    font-weight: bold;
                }

                .panel-default > .panel-heading {
                    color: #333;
                    background-color: #f5f5f5;
                    border-color: #ddd;
                }

                .list-group-item a {
                    text-decoration: none;
                }

                .panel-heading {
                    padding: 10px 15px;
                    border-bottom: 1px solid transparent;
                    border-top-left-radius: 3px;
                    border-top-right-radius: 3px;
                }

                .hn_p_panel .list-group-item {
                    font-family: 'Roboto Slab', serif;
                    font-weight: bold;
                    font-size: 18px;
                    color: #005aab;
                }

                .list-group-item:first-child {
                    border-top-left-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .list-group-item {
                    position: relative;
                    display: block;
                    padding: 10px 15px;
                    margin-bottom: -1px;
                    background-color: #fff;
                    border: 1px solid #ddd;
                }
            </style>
            <div class="modulecontent">
                <div class="panel panel-default hn_p_panel">
                    <!-- Default panel contents -->
                    <div class="panel-heading" style="background-color: #d5d5d5">LĨNH VỰC</div>
                    <!-- List group -->
                    <ul class="list-group">
                        <asp:Repeater ID="rptCategory" runat="server">
                            <ItemTemplate>
                                <li class="list-group-item" style="background-color: #eeeeee">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <a style="float: left" href="<%#SiteRoot +Eval("ItemUrl") %>">
                                                <img alt="" style="width: 20px; height: 22px; margin-right: 16px" src="<%#Eval("ItemIcon") %>" class="group list-group-image">
                                            </a>
                                        </div>
                                        <div class="col-md-10">
                                            <a href="<%#SiteRoot +Eval("ItemUrl") %>" style="color: #005aab"><%#Eval("Title") %></a>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoModule.ascx.cs" Inherits="VideoFeatures.UI.VideoModule" %>

<portal:ModulePanel ID="pnlContainer" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper MyFeature">
        <portal:ModuleTitleControl ID="TitleControl" runat="server" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="articleContent" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true">
            <div class="modulecontent">
                <asp:Panel ID="pnlVideo" runat="server">
                    <div class="col-md-8 col-sm-12">
                        <asp:Literal ID="literVideo" runat="server"></asp:Literal>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <div class="video-title">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </div>
                        <div class="video-author">
                            <asp:Label ID="lblAuthor" runat="server"></asp:Label>
                        </div>
                        <div class="video-createddate">
                            <asp:Label ID="lblCreateddate" runat="server"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </portal:mojoPanel>
        <div class="cleared">
        </div>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:ModulePanel>

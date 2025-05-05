<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SiteStatisticsModule.ascx.cs" Inherits="mojoPortal.Web.StatisticsUI.SiteStatisticsModule" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper stats">
        <portal:ModuleTitleControl ID="Title1" runat="server" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <div class="thongke-all">
                    <div class="container">
                        <div class="thongke-header">
                                <asp:Label runat="server" ID="lbltitle"></asp:Label>
                        </div>
                        <asp:Panel ID="pnlUsersOnline" runat="server" CssClass="site-statistics online_detail floatpanel">
                            <portal:OnlineStatistics ID="ol1" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlMembership" runat="server" CssClass="site-statistics sum_online floatpanel">
                            <portal:MembershipStatistics ID="st1" runat="server" />
                        </asp:Panel>

                        <asp:Panel ID="pnlOnlineMemberList" runat="server" CssClass="clearpanel onlinemembers">
                            <portal:OnlineMemberList ID="olm1" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlUserChart" runat="server" CssClass="clearpanel membergraph">
                            <zgw:ZedGraphWeb ID="zgMembershipGrowth" runat="server" RenderMode="ImageTag"
                                Width="780" Height="400">
                            </zgw:ZedGraphWeb>
                        </asp:Panel>
                    </div>
                </div>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AnnouncementRecenlist.ascx.cs" Inherits="AnnouncementFeatures.UI.AnnouncementRecenlist" %>

<%@ Import Namespace="AnnouncementFeatures.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper SlideList">
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <%--<asp:Button ID="btn_add_addmission" runat="server" CssClass="btn btn-primary" Text="Thêm mới" />--%>
                <ul>
                 <asp:Repeater ID="rptAdmission" runat="server">
                            <ItemTemplate>
                                 <li>
                                    <%# FormatAnnouncementDate(Convert.ToDateTime(Eval("DateAnno"))) %> : <%# Eval("ContentAnno") %>
       
                                </li>
                                 </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                  <asp:Panel ID="pnlAdmissionPager" runat="server" CssClass="blogpager">
                <portal:mojoCutePager ID="pgrAdmission" runat="server" />
            </asp:Panel>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
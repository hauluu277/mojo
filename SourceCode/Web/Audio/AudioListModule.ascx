<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AudioListModule.ascx.cs" Inherits="AudioFeature.UI.AudioListModule" %>

<%@ Import Namespace="mojoPortal.Features" %>
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:HeadingControl ID="heading" runat="server" />
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="row">
                                <asp:Repeater ID="rptAudio" runat="server" SkinID="Blog">
                                    <itemtemplate>
                                        <div class="col-sm-6">
                                            <div class="thumbnail">
                                                <a href='<%# MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, Eval("ItemUrl").ToString(),Convert.ToInt32(Eval("ItemID").ToString()),PageId, ModuleId) %>'>
                                                    <img src="/Data/File/Media/<%#Eval("FilePath") %>" title="<%#Eval("NameGroup") %>" />
                                                </a>
                                                <div class="caption">
                                                    <h3><%#Eval("NameGroup") %></h3>
                                                    <p><%#Eval("CreatedByUser") %></p>
                                                </div>
                                            </div>
                                        </div>
                                    </itemtemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>

    <%--end Dánh ách lãnh đạo--%>



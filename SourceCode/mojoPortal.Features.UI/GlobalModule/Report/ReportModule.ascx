<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ReportModule.ascx.cs" Inherits="ReportFeature.UI.ReportModule" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ArticleHot">
        <portal:ModuleTitleControl EditUrl="~/ArticleHot/ArticleHotEdit.aspx" runat="server" ID="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <div id="report_div" runat="server">
                    <span class="text-danger">Bạn chưa thiết lập chọn lĩnh vực báo cáo</span>
                </div>
                
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

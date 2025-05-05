<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PhuongAnDieuTraModule.ascx.cs" Inherits="InvestigateFeature.UI.PhuongAnDieuTraModule" %>


<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ArticleHot">
        <portal:ModuleTitleControl EditUrl="~/ArticleHot/ArticleHotEdit.aspx" runat="server" id="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <div id="content_unit">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            CallAjaxLoading("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/Index", null, true, function (rs) {
                $("#content_unit").html(rs);
            });
        });
    </script>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LichCongTacModule.ascx.cs" Inherits="LichCongTacFeature.UI.LichCongTacModule" %>
<%@ Register Src="~/LichCongTac/Controls/RecentList.ascx" TagPrefix="portal" TagName="RecentList" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Document">
<portal:ModuleTitleControl runat="server" id="TitleControl" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
    <div id="content_unit">
    </div>
    <script>

        $(document).ready(function () {
            CallAjaxLoading("get", "/QLLichLamViecArea/QLLichLamViec/IndexLichCongTac", null, true, function (rs) {
                $("#content_unit").html(rs);
                
            });
        });
    </script>
</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
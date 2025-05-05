<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="BieuMauThongTinModule.ascx.cs" Inherits="BieuMauThongTinFeature.UI.BieuMauThongTinModule" %>


<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ArticleHot">

        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

                <div id="content_unit">
                </div>
                <script>

                    $(document).ready(function () {
                        CallAjaxLoading("get", "/BieuMauThongTinArea/BieuMauThongTin/IndexGuest", null, true, function (rs) {
                            $("#content_unit").html(rs);
                        });
                    });
                </script>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="ViewPost.aspx.cs" Inherits="ThuTucHanhChinhFeature.UI.ViewPost" %>

<%@ Register Src="~/GlobalModule/ThuTucHanhChinh/Controls/ThuTucHanhChinhList.ascx" TagPrefix="portal" TagName="ThuTucHanhChinhList" %>




<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="OuterWrapperPanel1" runat="server">
        <mp:CornerRounderTop ID="CornerRounderTop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="InnerWrapperPanel1" runat="server" CssClass="panelwrapper ">
            <portal:ModuleTitleControl runat="server" ID="ModuleTitleControl1" />
            <portal:OuterBodyPanel ID="OuterBodyPanel1" runat="server">
                <portal:InnerBodyPanel ID="InnerBodyPanel1" runat="server" CssClass="modulecontent">

                    <portal:ThuTucHanhChinhList runat="server" ID="ThuTucHanhChinhListControl" />

                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="EmptyPanel1" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="CornerRounderBottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />


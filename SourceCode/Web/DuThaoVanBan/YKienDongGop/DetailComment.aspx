<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="DetailComment.aspx.cs" Inherits="DuThaoVanBanFeature.UI.DetailComment" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Import Namespace="DuThaoVanBanFeature.UI" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" EnableViewState="false"  />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
<div class="settingrow">
<mp:SiteLabel id="lblName" runat="server" ForControl="litName" CssClass="settinglabel" ConfigKey="NameHeaderLabel" ResourceFile="DuThaoVanBanResources" />
    <asp:Literal ID="litName" runat="server" EnableViewState="false"></asp:Literal>
</div>
<div class="settingrow">
<mp:SiteLabel id="SiteLabel1" runat="server" ForControl="litEmail" CssClass="settinglabel" ConfigKey="EmailHeaderLabel" ResourceFile="DuThaoVanBanResources" />
    <asp:Literal ID="litEmail" runat="server" EnableViewState="false"></asp:Literal>
</div>
<div class="settingrow">
<mp:SiteLabel id="SiteLabel2" runat="server" ForControl="litAddress" CssClass="settinglabel" ConfigKey="AddressLabel" ResourceFile="DuThaoVanBanResources" />
    <asp:Literal ID="litAddress" runat="server" EnableViewState="false"></asp:Literal>
</div>
<div class="settingrow">
<mp:SiteLabel id="SiteLabel3" runat="server" ForControl="litPhone" CssClass="settinglabel" ConfigKey="MobileLabel" ResourceFile="DuThaoVanBanResources" />
    <asp:Literal ID="litPhone" runat="server" EnableViewState="false"></asp:Literal>
</div>
<div class="settingrow">
<mp:SiteLabel id="SiteLabel4" runat="server" ForControl="litContent" CssClass="settinglabel" ConfigKey="CommentHeaderLabel" ResourceFile="DuThaoVanBanResources" />
    <asp:Literal ID="litContent" runat="server" EnableViewState="false"></asp:Literal>
</div>
    <div class="settingrow">
<mp:SiteLabel id="SiteLabel5" runat="server" ForControl="litApprove" CssClass="settinglabel" ConfigKey="HeaderDocumentApprove" ResourceFile="DuThaoVanBanResources" />
    <asp:Literal ID="litApprove" runat="server" EnableViewState="false"></asp:Literal>
</div>
    <div class="settingrow">
<mp:SiteLabel id="SiteLabel6" runat="server" ForControl="litPublic" CssClass="settinglabel" ConfigKey="HeaderDocumentPublic" ResourceFile="DuThaoVanBanResources" />
    <asp:Literal ID="litPublic" runat="server" EnableViewState="false"></asp:Literal>
</div>
</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel> 
<mp:CornerRounderBottom id="cbottom1" runat="server" EnableViewState="false" />	
</portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />



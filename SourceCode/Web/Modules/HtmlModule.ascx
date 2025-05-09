<%@ Control Language="c#" Inherits="mojoPortal.Web.ContentUI.HtmlModule" CodeBehind="HtmlModule.ascx.cs" AutoEventWireup="false" %>
<%@ Register Namespace="mojoPortal.Web.ContentUI" Assembly="mojoPortal.Web" TagPrefix="html" %>
<html:HtmlDisplaySettings ID="displaySettings" runat="server" />
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper htmlmodule">
        <portal:ModuleTitleControl ID="Title1" runat="server" EditUrl="/Modules/HtmlEdit.aspx" EnableViewState="false" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <portal:mojoRating runat="server" ID="Rating" Enabled="false" />
                <portal:SlidePanel ID="divContent" runat="server" EnableViewState="false" EnableSlideShow="false" class="slidecontainer"></portal:SlidePanel>
                <asp:HiddenField ID="hdnIsDirty" runat="server" />
                <fieldset class="info">
                    <asp:Panel ID="pnlAuthorInfo" runat="server" EnableViewState="false" CssClass="authorinfo">
                        <portal:Avatar ID="userAvatar" runat="server" />
                        <span id="spnAuthorBio" runat="server" visible="false" enableviewstate="false" class="authorbio"></span>
                    </asp:Panel>
                    <asp:Panel ID="pnlCreatedBy" runat="server" CssClass="authorinfo createdby" Visible="false">
                        <asp:Literal ID="litCreatedBy" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlModifiedBy" runat="server" CssClass="authorinfo modifiedby" Visible="false">
                        <asp:Literal ID="litModifiedBy" runat="server" />
                    </asp:Panel>
                    <portal:mojoRating runat="server" ID="RatingBottom" Enabled="false" />
                    <portal:FacebookLikeButton ID="fbLike" runat="server" Visible="false" />
                </fieldset>
            </portal:InnerBodyPanel>
            <portal:EmptyPanel ID="divFooter" runat="server" CssClass="modulefooter" SkinID="modulefooter"></portal:EmptyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
</portal:OuterWrapperPanel>

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="BannerUse.ascx.cs" Inherits="BannerFeature.UI.BannerUse" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper BannerUse">
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
<div class="banner_use">
    <asp:Repeater runat="server" ID="rptbanner">
        <ItemTemplate>
            <a href='<%#Eval("Link") %>' target='<%#Target(Convert.ToBoolean(Eval("IsTarget"))) %>'><asp:Image ID="imgBanner" Visible='<%#Eval("IsImage")%>' runat="server" ImageUrl='<%#"~/Data/Images/Banner/" + Eval("Path")%>' />
                            <%#BuildFlashObject(Convert.ToBoolean(Eval("IsImage")),Eval("Path").ToString()) %></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SlideList.ascx.cs" Inherits="DocumentFeature.UI.SlideListModule" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper SlideList">
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
    <div class="doc_slide">
        <asp:Repeater ID="rptDocSlide" runat="server">
            <ItemTemplate>
               <asp:HyperLink ID="lnkDetail" runat="server" NavigateUrl='<%#formartUrl(Convert.ToInt32(Eval("PageID")), Convert.ToInt32(Eval("ModuleID")), Convert.ToInt32(Eval("ItemID"))) %>'><%#formatContent(Eval("Summary").ToString()) %></asp:HyperLink>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
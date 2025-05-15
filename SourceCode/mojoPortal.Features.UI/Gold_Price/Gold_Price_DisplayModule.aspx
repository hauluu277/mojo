<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gold_Price_DisplayModule.aspx.cs" Inherits="mojoPortal.Features.UI.Gold_Price.Gold_Price_DisplayModule" %>
<%@ Register Src="~/Gold_Price/Controls/Gold_Price_DisplayModuleControls.ascx" TagPrefix="portal" TagName="GoldPriceDisplayControl" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper goldprice-module">
        <portal:HeadingControl ID="heading" runat="server" />
        
        <portal:ModuleTitleControl 
            ID="TitleControl"
            runat="server"
            EditText="Chỉnh sửa giá vàng" 
            EditUrl="~/Gold_Price/Gold_Price_DisplayEdit.aspx"
            EnableViewState="false" />
        
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent goldprice-content">
                <%-- Status message display --%>
                <asp:Panel ID="pnlMessage" runat="server" CssClass="alert alert-info" Visible="false">
                    <asp:Literal ID="litMessage" runat="server" />
                </asp:Panel>
                
                <%-- Main content container --%>
                <div class="goldprice-container">
                    <portal:GoldPriceDisplayControl ID="GoldPriceDisplay" runat="server" />
                </div>
                
                <%-- Add new price button for authorized users --%>
                <asp:Panel ID="pnlAddButton" runat="server" CssClass="module-actions" Visible="false">
                    <asp:HyperLink ID="lnkAddNew" runat="server" 
                        CssClass="btn btn-primary"
                        NavigateUrl="~/Gold_Price/Gold_Price_DisplayEdit.aspx">
                        <i class="fa fa-plus"></i> Thêm giá vàng mới
                    </asp:HyperLink>
                </asp:Panel>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>

        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared" />
    </portal:InnerWrapperPanel>
    
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
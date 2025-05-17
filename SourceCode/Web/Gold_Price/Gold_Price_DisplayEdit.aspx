<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Gold_Price_DisplayEdit.aspx.cs" Inherits="Gold_PriceFeatures.UI.Gold_Price_DisplayEdit" %>

<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:ModulePanel ID="pnlContainer" runat="server" CssClass="admin">
        <portal:ModuleTitleControl EditText="Add" EditUrl="~/menu/editpost.aspx" runat="server" ID="TitleControl" />
        <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
            <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
            <asp:Panel ID="pnlWrapper" runat="server" CssClass="art-Post-inner panelwrapper managepost">
                <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
                    <div>
                        <h2>Cập nhật giá vàng</h2>

                        <asp:HiddenField ID="hfItemID" runat="server" />

                        <div class="form-group">
                            <label>Tên loại vàng:</label>
                            <asp:TextBox ID="txtTenLoaiVang" runat="server" CssClass="form-control" />
                        </div> 

                        <div class="row">
                            <div class="col-md-6">
                                <h4>Hôm nay</h4>
                                <div class="form-group">
                                    <label>Giá mua:</label>
                                    <asp:TextBox ID="txtGiaMuaHomNay" runat="server" CssClass="form-control price" />
                                </div>
                                <div class="form-group">
                                    <label>Giá bán:</label>
                                    <asp:TextBox ID="txtGiaBanHomNay" runat="server" CssClass="form-control price" />
                                </div>
                            </div>
                            
                            <div class="col-md-6">
                                <h4>Hôm trước</h4>
                                <div class="form-group">
                                    <label>Giá mua:</label>
                                    <asp:TextBox ID="txtGiaMuaHomTruoc" runat="server" CssClass="form-control price" />
                                </div>
                                <div class="form-group">
                                    <label>Giá bán:</label>
                                    <asp:TextBox ID="txtGiaBanHomTruoc" runat="server" CssClass="form-control price" />
                                </div>
                            </div>
                        </div> 
                        </div>

                        <div class="form-actions">
                            <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                            <asp:HyperLink ID="lnkCancel" runat="server" NavigateUrl="~/Gold_Price/Gold_PriceDisplayManager.aspx" Text="Hủy" CssClass="btn btn-default" />
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger ml-2" />
                        </div>
                    </div>
                </portal:mojoPanel>
            </asp:Panel>
            <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
        </portal:mojoPanel>
    </portal:ModulePanel>
</asp:Content>
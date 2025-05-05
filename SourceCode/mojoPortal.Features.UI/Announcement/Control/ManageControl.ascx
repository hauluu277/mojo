<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ManageControl.ascx.cs" Inherits="AnnouncementFeatures.UI.ManageControl" %>
<%@ Import Namespace="mojoPortal.Business.WebHelpers" %>


<asp:Button ID="btnAddPost" runat="server" CssClass="btn btn-primary" Text="Thêm mới" />
 <div class="han_title_announcement">
                    <h2>Announcement</h2>
                </div>
                <ul class="Announcement_content">
    <asp:Repeater ID="rptAnnouncement" runat="server">
        <ItemTemplate>
            <li>
               <h3> <%# FormatAnnouncementDate(Convert.ToDateTime(Eval("DateAnno"))) %></h3> <span>:</span> <%# Eval("ContentAnno") %>
       
                <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                            CommandName="EditItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="Chỉnh sửa"
                            CausesValidation="false" />
                <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                            CommandName="DeleteItem" CommandArgument='<%# Eval("ItemID") %>' ToolTip="Xóa"
                            CausesValidation="false" />
                </li>
             </ItemTemplate>
    </asp:Repeater>
</ul>
  <asp:Panel ID="pnlAdmissionPager" runat="server" CssClass="blogpager">
                <portal:mojoCutePager ID="pgrAdmission" runat="server" />
            </asp:Panel>
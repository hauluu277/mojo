<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ListPost.ascx.cs" Inherits="LinkFeature.UI.ListPost" %>
<%@ Import Namespace="LinkFeature.UI" %>
<div class="boxlink">
    <div class="header_slide">
       <h2><asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
    </div>
    <ul class="fstbox-link">
   <asp:Repeater ID="rptLink" runat="server">
       <ItemTemplate>
           <li><a class="lnk-link" href='<%#Eval("Url") %>'> <%#Eval("Name") %></a></li>
           <%--<asp:Panel ID="pnChild" runat="server" Visible='<%#pnChildVisibe(Convert.ToInt32(Eval("ItemID").ToString())) %>' >
           <li>               
               <ul>
           <asp:Repeater ID="rptChild" runat="server" DataSource='<%#GetListChild(Convert.ToInt32(Eval("ItemID").ToString())) %>'>
               <ItemTemplate>
                   <li><a class="lnk-link" href='<%#Eval("Url") %>'><%#Eval("Name") %></a></li>
               </ItemTemplate>
           </asp:Repeater>
                   </ul>
               </li>
            </asp:Panel>--%>
       </ItemTemplate>
   </asp:Repeater>
    </ul>
</div>
<script>
    $(".ddl_link").onchange = function () {
        if (this.selectedIndex !== 0) {
            window.location.href = this.value;
        }
    };
</script>
<style>
    .boxlink{
        background:#F4F4F4;
        width:100%;
        float:left;
        margin-bottom:15px;
    }
</style>
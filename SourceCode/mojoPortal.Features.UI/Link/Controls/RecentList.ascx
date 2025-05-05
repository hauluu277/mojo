<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RecentList.ascx.cs" Inherits="LinkFeature.UI.RecentList" %>
<%@ Import Namespace="LinkFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    select {
        padding: 0;
        border: 0;
        border-radius: 0;
    }
</style>
<%
    if (DisplayList == LinkConstant.DisplayOneLink)//Hiển thị link bình thường
    {
%>
<div class="search-box">
    <asp:DropDownList CssClass="ddl_link" ID="ddlLink" runat="server"></asp:DropDownList>
</div>
<% 
    }
    else if (DisplayList == LinkConstant.DisplayThreeLink)//Hiển thị 3 link
    {
%>
<div class="link-wrraper">
    <div class="listCategory">
        <asp:Repeater ID="rptCategoryLink" runat="server">
            <ItemTemplate>
                <div class="Category" data-category="category_<%# Eval("ItemID") %>" data-categoryid="<%# Eval("ItemID") %>">
                    <a href="javascript:void(0)" title="<%# Eval("Name") %>">
                        <%# Eval("Name") %>
                    </a>
                    <div class="Category-bottom" data-category-bottom="bottom_<%# Eval("ItemID") %>"></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="listLink">
        <asp:Repeater ID="rptCategory" runat="server">
            <ItemTemplate>
                <ul class="links" data-link="link_<%# Eval("ItemID") %>" style="display: none">
                    <asp:Repeater ID="rptLink" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href="<%#Eval("Url") %>" title="<%# Eval("Name") %>">
                                    <%# Eval("Name") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<%}
    else if (DisplayList == LinkConstant.DisplayFourLink) // Hiển thị 4 link
    {%>
<div class="NewLink-wrraper">
    <div class="listNewLink">
        <div class="link-label">Liên kết</div>
        <asp:Repeater ID="rptNewLink" runat="server">
            <ItemTemplate>
                <div>
                    <asp:DropDownList ID="drl" CssClass="select-link" runat="server"></asp:DropDownList>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<%}%>

<script>
    $(document).ready(function () {
        $(".Category:first").addClass("active");
        $(".listLink ul:first").css("display", "block");

        //$('.Category:nth-child(2)').css('border-right', '1px solid #333333');
        $(".Category:first a").css("color", "white");
        $(".Category-bottom:first").css("background-color", "rgb(184,0,2)");



        $(".ddl_link").change(function () {
            if (this.selectedIndex !== 0) {
                window.location.href = this.value;
            }
        });


        $(".select-link").change(function () {
            if (this.selectedIndex !== 0) {
                window.location.href = this.value;
            }
        });

        $(".Category").click(function () {
            var id = $(this).attr("data-categoryID");
            $(".Category").removeClass("active");
            $(".Category a").css("color", "#333333");
            $(".Category-bottom").css("background-color", "rgb(227,160,11)");
            $(".listLink ul").css("display", "none");
            $(this).addClass("active");
            $(this).find("a").css("color", "white");
            $(".Category-bottom[data-category-bottom=bottom_" + id + "]").css("background-color", "rgb(184,0,2)");
            $(".listLink ul[data-link=link_" + id + "]").show();
        });
    });

</script>


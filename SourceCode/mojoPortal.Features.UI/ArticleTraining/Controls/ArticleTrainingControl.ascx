<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleTrainingControl.ascx.cs" Inherits="ArticleFeature.UI.ArticleTrainingControl" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<div class="container bck-menu col-md-3 content-menu-3ck">
    <div class="panel-group " id="accordion_1">
        <asp:Label ID="lblTitle" runat="server"></asp:Label>
        <table class="table table-bordered">
            <tbody>
                <asp:Repeater ID="rptCategory" runat="server">
                    <ItemTemplate>
                        <tr>
                            <th><%#Eval("Name") %></th>
                            <td>
                                <asp:Repeater ID="rptArticle" runat="server" DataSource='<%#LoadArticle(Eval("ItemID"))%>'>
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                                title='<%#Eval("Title") %>'><%#Eval("Title") %></a>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate></ul></FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

    </div>
</div>
<script type="text/javascript">
    function scrollToID(id, speed) {
        var offSet = 70;
        var obj = $(id).offset();
        var targetOffset = $(id).offset().top - offSet;
        $('html,body').animate({ scrollTop: targetOffset }, speed);
    }

    $("#accordion_1 .panel-title a").click(function () {
        var heading = $(this).parent().parent();
        if (heading.hasClass("more")) {
            $("#accordion_1 .panel-heading").removeClass("more");
        } else {
            $("#accordion_1 .panel-heading").removeClass("more");
            heading.addClass("more");
        }


    });
</script>

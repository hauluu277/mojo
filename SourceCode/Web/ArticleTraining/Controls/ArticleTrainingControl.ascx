<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ArticleTrainingControl.ascx.cs" Inherits="ArticleFeature.UI.ArticleTrainingControl" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .format-th {
        color: #c62d2f;
        font-size: 20px;
        text-transform: uppercase;
        padding: 25px !important;
    }

    .format-td {
        width: 250px;
        font-weight: bold;
    }

    #accordion_1 table tbody th, #accordion_1 table tbody td {
        padding: 15px;
    }

    .col-md-12 {
        padding: 0;
    }
	
	.panel-group{
		display:block !important;
	}
</style>
<div class="container bck-menu col-md-12 content-menu-3ck">
    <div class="panel-group " id="accordion_1">
        <table class="table table-bordered">
            <tbody>
                <tr>
                    <th colspan="2" class="format-th">
                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                    </th>
                </tr>
                <asp:Repeater ID="rptCategory" runat="server">
                    <ItemTemplate>
                        <asp:Literal ID="liter" runat="server" Text='<%#GenRow(Eval("Name"), Eval("ItemID"))%>'></asp:Literal>
                        <asp:Repeater ID="rptArticle" runat="server" DataSource='<%#LoadArticle(Eval("ItemID"))%>'>
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%--    <a href='<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'
                                            title='<%#Eval("Title") %>'><%#Eval("Title") %></a>--%>
                                        <asp:HyperLink SkinID="BlogTitle" ID="lnkTitle" runat="server" EnableViewState="false"
                                            ToolTip='<%# Eval("Title") %>' Text='<%# ArticleUtils.FormatBlogTitle(Eval("Title").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'
                                            NavigateUrl='<%# string.Format("{0}/Article/ViewDetail.aspx?itemid={1}&cat={2}",SiteRoot,Eval("ItemID"),Eval("CategoryID")) %>'>
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

    </div>
</div>


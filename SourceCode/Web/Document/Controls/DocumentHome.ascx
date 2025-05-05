<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentHome.ascx.cs" Inherits="DocumentFeature.UI.DocumentHome" %>
<script src="../../Data/js/jquery.marquee.js"></script>
<div class="documentHome">
    <div class="box-icon-body">
        <div class="titlebox">
            <h2>Văn bản - Chỉ đạo điều hành</h2>
        </div>
        <div class="block-law marquee">
            <div class="js-marquee-wrapper">
                <div class="js-marquee" style="margin-right: 0px; float: none; margin-bottom: 20px;">
                    <asp:Repeater ID="rptDocument" runat="server">
                        <ItemTemplate>
                            <div class="m-bottom item law-item">
                                <h3 class="law-code">
                                    <a href="<%#mojoPortal.Features.DocumentUltils.FormatBlogTitleUrl(siteSettings.SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" title="<%#Eval("Summary") %>"><b><%#Eval("Sign") %></b><span><%#Eval("Summary") %></span></a>
                                </h3>
                                <em class="text-muted law-view">Ngày ban hành: <%#(Convert.ToDateTime(Eval("DatePromulgate"))).ToString("dd/MM/yyyy", CultureInfo.CurrentUICulture)%></em>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
        <div class="clearfix text-right">
            <a href="/van-ban" class="more">Xem tiếp&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i></a>
        </div>
    </div>
</div>
<script type="text/javascript">
    //$('.marquee').marquee({
    //    //speed in milliseconds of the marquee
    //    duration: 10000,
    //    //gap in pixels between the tickers
    //    gap: 50,
    //    //time in milliseconds before the marquee will start animating
    //    delayBeforeStart: 0,
    //    //'left' or 'right'
    //    direction: 'up',
    //    //true or false - should the marquee be duplicated to show an effect of continues flow
    //    duplicated: false,
    //    pauseOnHover: true
    //});
</script>


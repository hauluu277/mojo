<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EventHotListCenter.ascx.cs" Inherits="EventFeature.UI.EventHotListCenter" %>

<%@ Import Namespace="EventFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper SlideList">
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
               <div class="training_box_eventhot doc_slide">
                    <div class="container">
                    <div class="header_slide">
                        <h2 class="page-header">
                            <a href="/su-kien" title="Sự kiện">Sự kiện</a>
                        </h2>
                    </div>
                    <div class="scroll-event">
                        <div id="event_hot" class="col-sm-8 pd0">
                            <asp:Repeater ID="rptEventHot" runat="server">
                                <ItemTemplate>
                                   <div class="col-sm-6 event_hot__item event_hot__common">
                                        <div class="event_hot__item__clone">
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                        <asp:Image ID="imghotright" Visible='<%#ShowImage(Eval("ImageUrl").ToString())%>' runat="server" Width="100%" ImageUrl='<%# EventUtils.FormatImageDialog(ConfigurationManager.AppSettings["EventImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                    </asp:HyperLink>
                                   <div class="event_hot__item__clone__content">
                                        <asp:HyperLink ID="lnkDetail" runat="server" NavigateUrl='<%# FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                            <%#formatContent(Eval("Title").ToString()) %>
                                    </asp:HyperLink>

                                            <ul>
                                                <li>
                                            <span class="event-date"><%#FormatDateTime(Eval("StartDate"),Eval("EndDate")) %></span></li>
                                                <li><span><%#Eval("Location") %></span></li>
                                            </ul>
                                   </div>
                                        </div>
                                   </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        
                        <div class="col-sm-4 event_hot__last event_hot__common">
                            <ul>
                            <asp:Repeater ID="rptEvent" runat="server">
                                <ItemTemplate>
                                    <li>
                                       <%-- <asp:HyperLink ID="lnkDetail" runat="server" NavigateUrl='<%# FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>--%>
                                               <div class="event_hot__last__local">
                                                   <div class="event_hot__last___local__StartDate">
                                                       <span class="event-date"><%#FormatDate(Eval("StartDate").ToString()) %></span>
                                                   </div>
                                                   <div class="event_hot__last___local__Location">
                                            <span><%#Eval("Location") %></span>
                                                   </div>
                                               </div>
                                        <%--</asp:HyperLink>--%>
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>'>
                                           <%#Eval("Title") %>
                                        </asp:HyperLink>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        </div>
                    </div>
                    </div>
                </div>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
<script>
     var mota = $(".event_hot__item__clone__content a").text().length;
         if (mota > 70) {
            var newmota = $(".event_hot__item__clone__content a").text().substring(0, 65) + " ...";
            $(".event_hot__item__clone__content a").text(newmota);
        }
</script>
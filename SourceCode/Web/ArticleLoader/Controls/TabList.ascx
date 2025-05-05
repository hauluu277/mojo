<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TabList.ascx.cs" Inherits="ArticleFeature.UI.TabLoader" %>
<%@ Import Namespace="ArticleFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<style type="text/css">
    .xlink {
        font-size: 13px;
        /*font-family:Semibold;*/
    }
</style>
<div style="margin-bottom: 0px;">
       <%--Hiển thị kiểu tab 10--%>
    <asp:Panel ID="pnlUseTab10" runat="server">
        <div class="container">
            <div class="why__choose__us">
                <div class="why__choose-head">
                    <h3><asp:HyperLink ID="hplcategorytab10" runat="server"></asp:HyperLink></h3>
                </div>
                   
                <div class="sinhvien__totnghiep__content">
                    <div class="sv_totnghiep_item_left col-sm-5">
                <asp:Repeater ID="rptActicleTab10" runat="server">
                    <ItemTemplate>
                        <div class="box-1 sv_tn_content-left wow fadeInLeft " style="visibility: visible; animation-delay: 0.2s;">
                            <div class="sv_tn_content-left-1">
                                <h3>
                                     <a title="<%#Eval("Title") %>" class="ahover" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                            <%#Eval("Title") %>
                                     </a>
                                </h3>
                                 <p>
                                    <%#Eval("Description") %>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                        </div>
                     <div class="sv_totnghiep_item_center col-sm-2">
                     <div class="box-2 sv_tn_content-center wow bounceInDown " style="visibility: visible; animation-delay: 0.3s;"><img alt="" class="cangiua" src="../Data/Sites/120/skins/framework/images/why_tuyen_sinh.png" /></div>
                </div>
                     <div class="sv_totnghiep_item_right col-sm-5">
                         <asp:Repeater ID="rptActicleTab10_2" runat="server">
                    <ItemTemplate>
                        <div class="sv_tn_content-right-1 wow fadeInRight" style="visibility: visible; animation-delay: 0.2s;">
                            <div class="sv_tn_content-left-1">
                                <h3>
                                     <a title="<%#Eval("Title") %>" class="ahover" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                            <%#Eval("Title") %>
                                     </a>
                                </h3>
                                 <p>
                                    <%#Eval("Description") %>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                         </div>
                  </div>
                </div>
            </div>
    </asp:Panel>
    <%--end tab10--%>
   
    <%--Hiển thị kiểu tab 9--%>
        <style type="text/css">.ahover:hover{
            color:#d12b22
            }
        </style>
     <asp:Panel ID="pnlUseTab9" runat="server">
       <div class="why__ts">
         <div class="container">
            <div class="why__choose__us__full">
                <div class="container">
                    <div class="why__choose__us">
                        <div class="why__choose-head">
                            <h3> <asp:HyperLink ID="hplcategorytab9" runat="server"></asp:HyperLink></h3>
                        </div>
                        <div class="why__choose-content">
                            <asp:Repeater ID="rptActicleTab9" runat="server">
                                <ItemTemplate>
                                    <div class="han-dt-content wow fadeInLeft" data-wow-delay="0.2s" style="visibility: visible; animation-delay: 0.2s;">
                                        <div class="han-dt--img"> <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" title="<%#Eval("Title") %>" />
                                            <div class="han-dt--hover"><a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" tabindex="0"><i class="fa fa-check-square-o"><span>a</span></i></a></div>
                                            </div>

                                       <p>
                                        <a title="<%#Eval("Title") %>" class="ahover" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                            <%#Eval("Title") %>
                                        </a>
                                    </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                     </div>
                  </div>
               </div>
           </div>
        </div>
    </asp:Panel>
    <%--end tab9--%>

   <%--Hiển thị kiểu tab 8--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet " />
    <asp:Panel ID="pnlUseTab8" runat="server">
        <div class="taisao_chonchung_toi_haan">
            <div id="intro-toan">
            <div class="container">
            <div class="intro-left">
                <h3>
                <asp:HyperLink ID="hplcategorytab8" runat="server"></asp:HyperLink>
                    </h3>
                </div>
            <div class="haan_chon_chungtoi_content intro-right">
                <asp:Repeater ID="rptActicleTab8" runat="server">
                    <ItemTemplate>
                        <div class="intro-item wow fadeInLeft" data-wow-delay="0.2s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInLeft;">
                            <div class="col-sm-6 pdl0 why__choose__img">
                                <div class="item-img">
                                    <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" title="<%#Eval("Title") %>" />
                                     </a>
                                     </div>
                          
                           
                                <p>
                                    <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <%#Eval("Title") %>
                                    </a>
                                </p>
                           
                        </div>
                            </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
                </div>
            </div>
    </asp:Panel>

    <%--end tab 8--%>

    <%--Hiển thị kiểu tab 7--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet " />
    <asp:Panel ID="pnlUseTab7" runat="server">
        <div class="sinhvien_totnghiep_haan">
             <h2 class="page-header">
            <asp:HyperLink ID="hplcategorytab7"  runat="server"></asp:HyperLink>
                </h2>
            <div class="container">
            <div class="col-sm-6 pdl0"><img alt="Sinh viên sau khi tốt nghiệp ĐHSPTN" src="/Data/Sites/1/media/admin/images/SV%20Tot%20nghiep(2).png"></div>
           <div class="totnghiep col-sm-6 pdr0">
                <asp:Repeater ID="rptActicleTab7" runat="server">
                    <ItemTemplate>
                        <div class="totnghiep__content wow fadeInLeft" data-wow-delay="0.1s">
                            <div class="pdl0 why__choose__img">
                               <%-- <div class="item-img">
                                    <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" title="<%#Eval("Title") %>" />
                                     </a>
                                    <div class="why-hover-icon"><a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><i class="fa fa-check">a</i></a></div>
                                 </div>--%>
                          
                                <h3>
                                    <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <%#Eval("Title") %>
                                    </a>
                                </h3>
                                <p>
                                    <%#Eval("Summary") %>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        </div>
    </asp:Panel>

    <%--end tab 7--%>


     <%--Hiển thị kiểu tab 6--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet " />
    <asp:Panel ID="pnlUseTab6" runat="server">
        <div class="taisao_chonchung_toi_haan">
             <h2 class="page-header">
                <asp:HyperLink ID="hplcategorytab6" CssClass="page-header" runat="server"></asp:HyperLink>
            </h2>
            <div class="haan_chon_chungtoi_content">
                <asp:Repeater ID="rptActicleTab6" runat="server">
                    <ItemTemplate>
                        <div class="why__choose why__choose--1 wow fadeInRight" data-wow-delay="0.1s">
                            <div class="col-sm-6 pdl0 why__choose__img">
                                <div class="item-img">
                                    <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" title="<%#Eval("Title") %>" />
                                     </a>
                                    <div class="why-hover-icon"><a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><i class="fa fa-check"><span>a</span></i></a></div>
                                 </div>
                            </div>
                            <div class="col-sm-6 pdr0 why__choose__content">
                                <h3>
                                    <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <%#Eval("Title") %>
                                    </a>
                                </h3>
                                <p>
                                    <%#Eval("Summary") %>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </asp:Panel>

    <%--end tab 6--%>

    <%--Hiển thị tin hot tin hoạt động của khoa--%>
    <asp:Panel ID="pnlUseTab5" runat="server">
        <div class="container">
        <div class="tin-hoat-dong">
            <div class="header">
                <h2>
                    <asp:HyperLink ID="hplTieuDe" runat="server" ></asp:HyperLink>
                </h2>
            </div>
            <div class="tin-hoat-dong-content">
                <asp:Repeater ID="rptTab_5" runat="server">
                    <ItemTemplate>
                        <div class="tin-hoat-dong-item col-md-3">
                            <div class="tinhoatdong-image">
                            <img src="<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>" title="<%#Eval("Title") %>" />
                            <div class="tinhoatdong-hover">
                               <a href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>" class="fa fa-info" tabindex="0"><span>a</span></a>
                        </div>
                            </div>
                                <div class="tin-hoat-dong-date">
                                <%#FormartDateTime(Eval("StartDate")) %>
                            </div>
                            <h3>
                                <a title="<%#Eval("Title") %>" class="linktip han_title_article_thd_khoa" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                    <%#Eval("Title") %>
                                </a>
                            </h3>
                            <div class="tin-hot-dong-summary tin-hot-dong-summary_han_lenght">
                                <%#Eval("Summary") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        </div>
    </asp:Panel>

    <%--Hiển thị danh sách kiểu dọc và các danh mục con--%>
    <asp:Panel ID="pnlUseTab4" runat="server">
        <div class="articletab-all">
            <div class="tt-wrap">
                <asp:Panel runat="server" ID="Panel3" CssClass="tt-head">
                    <asp:Panel ID="pnlHideLink" runat="server" CssClass="tab-left">
                        <div class="tt-parentTab">
                            <asp:HyperLink runat="server" ID="lnkCategory4" Text="Chưa có thiết lập" CssClass="categoryTitle"></asp:HyperLink>
                        </div>
                    </asp:Panel>
                    <div class="tab-right">
                        <asp:Panel runat="server" ID="nbchTab2" CssClass="nbchTabs">
                            <ul class="tab-child">
                                <asp:Repeater ID="rptTabs2" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <h2>
                                                <a href="<%#SiteRoot+Eval("Description") %>" id="<%# Eval("ItemID").ToString() + ModuleId.ToString() %>" title="<%# Eval("Name").ToString() %>">
                                                    <asp:Label ID="litHeading" runat="server" EnableViewState="false" Text='<%# ArticleUtils.FormatBlogTitle(Eval("Name").ToString(), Config.MaxNumberOfCharactersInTitleSetting) %>'></asp:Label>
                                                </a>
                                            </h2>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <div class="nbccTabContent">
                    <div class="leftContent">
                        <asp:Panel ID="pnHighlight" runat="server" CssClass="article-left">
                            <div class="articletab-left">
                                <div class="width100">
                                    <asp:Image ID="imgHeightlight" runat="server" Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' />
                                    <div class="hot-title">
                                        <h3>
                                            <asp:HyperLink ID="hplHeightLight" runat="server"></asp:HyperLink>
                                        </h3>
                                        <div class="more-info">
                                            <span id="lblDateHeightLight" runat="server"></span>|
                                            <asp:HyperLink ID="hplCategory" runat="server"></asp:HyperLink>

                                        </div>
                                    </div>
                                    <div class="Sumary" id="lblSumaryHeightLight" runat="server"></div>
                                </div>
                            </div>
                            <%-- todo: bat dau them 3 tin duoi tin noi bat --%>
                            <div class="articletab-right">
                                <div class="articleTitle">
                                    <ul class="ulOrtherArticle">
                                        <asp:Repeater ID="rptArticles" runat="server" SkinID="Blog" EnableViewState="False">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Panel ID="pnlHasImg" runat="server" CssClass="width100 tt-hasimg">
                                                        <asp:Image ID="imgHot" Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>' />
                                                        <div class="articletab-right-right">
                                                            <a title="<%#Eval("Title") %>" class="linktip" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                                                <%#Eval("Title") %></a>
                                                            <div class="articletab-date"><%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %> | <a href='<%#SiteRoot +Eval("CategoryUrl") %>'><%#Eval("CategoryName") %></a></div>
                                                        </div>
                                                    </asp:Panel>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </asp:Panel>
    <%--Hết hiển thị danh sách kiểu dọc và các danh mục con--%>

    <asp:Panel ID="mp_modulecontent" runat="server">
        <div id="nbcwrap<%=ModuleId.ToString() %>" class="nbcwrap shd-title">
            <asp:Panel runat="server" ID="nbchead" CssClass="nbchead">
                <%--<asp:Label ID="lblPageTitle" runat="server" CssClass="nbchTitle"></asp:Label>--%>
                <div class="parentlink">
                    <div class="parentTab">
                        <asp:HyperLink runat="server" ID="lnkCategory" Text="Chưa có thiết lập" CssClass="nbchTitle"></asp:HyperLink>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="nbcContent" CssClass="nbcContent">
                <%--Hiển thị tab kiểu 1--%>
                <div class="leftContent tab1type" runat="server" id="TabType1" visible="false">
                    <div class="articleTab2-left">
                        <div class="tab1Left-content">
                            <a href='<%=lnkHotArticle_HRef %>'>
                                <img src='<%=lnkHotArticle_Src %>' alt='<%=lnkHotArticle_Title %>' />
                            </a>
                            <p class="hot-title">
                                <a href='<%=lnkHotArticle_HRef %>'><%=lnkHotArticle_Title %></a>
                            </p>
                            <p class="Sumary">
                                <%= lnkHotArticle_Summary %>
                            </p>
                            <div class="tipnarrow"></div>
                        </div>
                    </div>
                    <div class="articleTab1-right">
                        <ul>
                            <asp:Repeater ID="rptTab_1" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%# Eval("Title") %></a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <%--Kết thúc hiển thị tab kiểu 1--%>
                <%--Hiển thị tab kiểu 2--%>
                <div class="leftContent lct-edit-level1" runat="server" id="TabType2" visible="false">
                    <asp:Panel ID="pnlFirst" runat="server" Visible='<%#(rptTab_2.Items.Count ==0) %>' CssClass="articleTab2-left">
                        <div class="tab2Left-content lct-edit-left">
                            <a href='<%=lnkHotArticle_HRef %>'>
                                <img src='<%=lnkHotArticle_Src %>' alt='<%=lnkHotArticle_Title %>' />
                            </a>
                            <p class="hot-title ">
                                <a href='<%=lnkHotArticle_HRef %>'><%=lnkHotArticle_Title %></a>
                                <%--<asp:HyperLink ID="lnkHotArticle" runat="server"></asp:HyperLink>--%>
                            </p>
                            <p class="Sumary">
                                <%= lnkHotArticle_Summary %>
                                <%--<asp:Literal ID="literDescript" runat="server"></asp:Literal>--%>
                            </p>
                            <div class="tipnarrow"></div>
                        </div>
                    </asp:Panel>
                    <div class="articleTab2-right lct-edit-right">
                        <ul>
                            <asp:Repeater ID="rptTab_2" runat="server">
                                <ItemTemplate>
                                    <%-- todo: bat dau them 3 tin duoi tin noi bat --%>
                                    <li>
                                        <div class="articleTab2-image lct-level1-img">
                                            <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                                <asp:Image ID="image21" runat="server" AlternateText='<%#Eval("Title") %>' ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                                    CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' />
                                            </a>
                                        </div>
                                        <div class="articleTab2-content lct-level1-content">
                                            <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%# Eval("Title").ToString() %></a>
                                        </div>
                                    </li>
                                    <%-- todo:ket thuc them 3 tin duoi tin noi bat --%>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <%--Kết thúc tab kiểu 2--%>
                <%--Hiển thị tab kiểu 3--%>
                <asp:Repeater ID="rptTab_3"  runat="server">
                    <ItemTemplate>
                        <div>
                            <div class="tab-3  shd-content">

                                 <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <asp:Image ID="image28" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                    CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' />
                                    </a>
                                <div class="tab-3-content shd-tab-3-content">
                                    <a class="linktip han_title_lich" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>">
                                        <%#Eval("Title") %>
                                    </a>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <%--Kết thúc tab kiểu 3--%>
                <%--Hiển thị tab kiểu 4--%>
                <asp:Repeater ID="rptTab_4" runat="server">
                    <ItemTemplate>
                        <div class="nbccTabContent" id="<%# "content"+ Eval("ItemID").ToString() + ModuleId.ToString() %>" style="<%# Container.ItemIndex == 0 ? "display: block;": "" %>">
                            <div class="leftContent">
                                <asp:Panel ID="pnlFirst" runat="server" Visible='<%#(rptTab_4.Items.Count ==0) %>' CssClass="width100">
                                    <div class="tabcontentFirst">
                                        <asp:Image ID="image28" runat="server" ImageUrl='<%# ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], Eval("ImageUrl").ToString()) %>'
                                            CssClass='<%# "rimg" + ModuleId + Eval("ItemID") %>' Visible='<%#ShowImage(Eval("ImageUrl").ToString()) %>' />
                                        <p class="hot-title"><a title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%#Eval("Title") %></a></p>
                                        <%--<span><%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %></span>--%>
                                        <p class="Sumary"><%#Eval("Summary") %></p>
                                        <div class="tipnarrow"></div>
                                    </div>
                                </asp:Panel>
                                <%-- todo: bat dau them 3 tin duoi tin noi bat --%>
                                <div class="articleTitle">
                                    <ul class="ulOrtherArticle">
                                        <asp:Panel ID="pnlSecond" runat="server" Visible='<%#(rptTab_2.Items.Count >00) %>' CssClass="width100">
                                            <li>
                                                <h2 class="xlink">
                                                    <a class="linktip" title="<%#Eval("Title") %>" href="<%# ArticleUtils.FormatBlogTitleUrl(SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), PageId, ModuleId) %>"><%#Eval("Title") %></a><span class="nbcupdatedate">(<%# FormatArticleDate(Convert.ToDateTime(Eval("StartDate"))) %>)</span>
                                                </h2>
                                            </li>
                                        </asp:Panel>
                                    </ul>
                                </div>
                                <%-- todo:ket thuc them 3 tin duoi tin noi bat --%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <%--Kết thúc tab kiểu 4--%>
            </asp:Panel>
            <div class="clear"></div>
        </div>
    </asp:Panel> 

    <script>
        
         var isIE = /*@cc_on!@*/false || !!document.documentMode;
            if (isIE == true) {
                $(document).ready(function() 
                    { $(".han_title_lich").each(function(i){ 
                     var len=$(this).text().trim().length; 
                     if(len>120) 
                     { 
                      $(this).text($(this).text().substr(0,120)+'...'); 
                     } 
                    }); 
                }); 

                 $(document).ready(function() 
                    { $(".tin-hot-dong-summary_han_lenght").each(function(i){ 
                     var len=$(this).text().trim().length; 
                     if(len>130) 
                     { 
                      $(this).text($(this).text().substr(0,130)+'...'); 
                     } 
                    }); 
                });
                

                $(document).ready(function() 
                    { $(".han_title_article_thd_khoa").each(function(i){ 
                     var len=$(this).text().trim().length; 
                     if(len>75) 
                     { 
                      $(this).text($(this).text().substr(0,75)+'...'); 
                     } 
                    }); 
                });
            }
        $(function () {
            <%--$("#nbcwrap<%=ModuleId.ToString()%> .nbccTabContent:first-child").slideDown();--%>
            $("#nbcwrap<%=ModuleId.ToString()%> .nbchTabs ul li a").click(function () {
                $("#nbcwrap<%=ModuleId.ToString()%> .nbchTabs ul li a").removeClass("active");
                $(this).addClass("active");
                $("#nbcwrap<%=ModuleId.ToString()%> .nbccTabContent").slideUp();
                $("#nbcwrap<%=ModuleId.ToString()%> #content" + $(this).attr("id")).slideDown();

            });
            $('ul.ulcontent li a').mouseover(function () {
                $(this).next().next().show();
            }).mouseout(function () {
                $(this).next().next().hide();
            });

            $('ul.ulOrtherArticle li a').mouseover(function () {
                $(this).next().next().show();
            }).mouseout(function () {
                $(this).next().next().hide();
            });

        });

        $(".list_sub_cat").click(function (event) {
            $(this).next().slideDown(); event.stopPropagation();
            $(".sub_cat_item").hide();
        });

        $("body").not(".list_sub_cat ul").click(function () {
            $(".sub_cat_item").hide();
        });

    </script>
</div>


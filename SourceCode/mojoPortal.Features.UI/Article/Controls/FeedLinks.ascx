﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeedLinks.ascx.cs" Inherits="ArticleFeature.UI.FeedLinks" %>
<ul class="blognav">
    <li id="liRSS" runat="server"><a id="lnkRSS" href="~/RSS.aspx" runat="server" class="rsslink"><img alt="RSS" id="imgRSS" src="/images/xml.gif"  runat="server"  /></a></li>
    <li id="liAddMSN" runat="server"><a id="lnkAddMSN" runat="server"><img alt="Add To My MSN" id="imgMSNRSS" src="~/Data/SiteImages/rss_mymsn.gif" runat="server"  /></a></li>
    <li id="liAddToLive" runat="server"><a id="lnkAddToLive" runat="server"><img alt="Add To Windows Live" id="imgAddToLive" src="~/Data/SiteImages/addtolive.gif" runat="server"  /></a></li>
    <li id="liAddYahoo" runat="server"><a id="lnkAddYahoo" runat="server"><img alt="Add To My Yahoo" id="imgYahooRSS" src="~/Data/SiteImages/addtomyyahoo2.gif" runat="server"  /></a></li>
    <li id="liAddGoogle" runat="server"><a id="lnkAddGoogle" runat="server"><img alt="Add To Google" id="imgGoogleRSS" src="~/Data/SiteImages/googleaddrss.gif" runat="server"  /></a></li>
    <li id="liOdiogoPodcast" runat="server"><a id="lnkOdiogoPodcast" runat="server" class="odiogopodcast"><img alt="Podcast" id="imgOdiogoPodcast" src="~/Data/SiteImages/podcast.png" runat="server"  /></a>&nbsp;<asp:HyperLink ID="lnkOdiogoPodcastTextLink" runat="server" /></li>
</ul>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LogList.ascx.cs" Inherits="LogFeature.UI.LogList" %>
<%@ Import Namespace="mojoPortal.Features" %>
<portal:HeadingControl ID="heading" runat="server" />
<style>
    .CountLinhVuc ul {
        list-style: none;
    }

        .CountLinhVuc ul li {
            width: 33%;
            float: left;
        }
</style>
<link href="/Data/plugins/tree/tree.css" rel="stylesheet" />
<script src="/Data/plugins/tree/tree.js"></script>
<div class="col-sm-4">
      <div id="tree"></div>
</div>
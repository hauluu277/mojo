<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="SlideList.aspx.cs" Inherits="mojoPortal.Features.UI.LichCongTac.SlideList" %>

<%@ Register Src="~/LichCongTac/Controls/RecentSlideList.ascx" TagPrefix="portal" TagName="RecentSlideList" %>
<html>
<head>
    <title>LỊCH CÔNG TÁC TUẦN CỦA BỆNH VIỆN MẮT TRUNG ƯƠNG</title>
</head>
<body>

    <portal:RecentSlideList runat="server" ID="RecentSlideList" />
</body>
</html>


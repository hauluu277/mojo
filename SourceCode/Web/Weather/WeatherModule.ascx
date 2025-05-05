<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="WeatherModule.ascx.cs" Inherits="WeatherFeature.UI.WeatherModule" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Document">
        <portal:ModuleTitleControl runat="server" ID="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <%--                <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
                <script type="text/javascript">
                    $(function () {
                        var select = $("#we_zone option:selected").val().trim();
                        $('#htmlWeather').load('Weather/readXML.ashx?id=' + select);
                    });
                    $(function () {
                        $("#we_zone").change(function () {
                            alert("dsadasdada");
                            var value = $("#we_zone option:selected").val().trim();
                            if (value > 0) {
                                //$('#htmlWeather').html('<img alt="loading..." src="../Data/SiteImages/ajax-loader.gif" />');
                                $('#htmlWeather').load('Weather/readXML.ashx?id=' + value);
                            }

                        });
                    });
                    function getWeather(value) {
                        $('#htmlWeather').html('<img alt="loading..." src="../Data/SiteImages/ajax-loader.gif" />');
                        $('#htmlWeather').load('Weather/readXML.ashx?id=' + value);
                    }
                </script>
                <asp:DropDownList ID="we_zone" runat="server" CssClass="weather-city" ClientIDMode="Static"></asp:DropDownList>
           
                <div id="htmlWeather">
                    <img alt="loading..." src="../Data/SiteImages/ajax-loader.gif" />
                </div>--%>
                <div id="weather-all">
                    <div class="weather-header">
                        <%=Resources.WeatherResources.WeatherTitle%>
                    </div>
                    <div class="weather-content">
                        <asp:Literal ID="literWeather" runat="server"></asp:Literal>
                    </div>
                    <div style="height: 15px; clear: both"></div>
                </div>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

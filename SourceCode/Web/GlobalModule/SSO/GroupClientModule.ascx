<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="GroupClientModule.ascx.cs" Inherits="SSOFeature.UI.GroupClientModule" %>

<portal:outerwrapperpanel id="pnlOuterWrap" runat="server">
    <mp:cornerroundertop id="ctop1" runat="server" />
    <portal:innerwrapperpanel id="pnlInnerWrap" runat="server" cssclass="panelwrapper Document">
        <portal:moduletitlecontrol runat="server" id="TitleControl" />
        <portal:outerbodypanel id="pnlOuterBody" runat="server">
            <portal:innerbodypanel id="pnlInnerBody" runat="server" cssclass="modulecontent">
                <div id="content_group_client">
                </div>

            </portal:innerbodypanel>
        </portal:outerbodypanel>
        <portal:emptypanel id="divCleared" runat="server" cssclass="cleared" skinid="cleared"></portal:emptypanel>
    </portal:innerwrapperpanel>
    <mp:cornerrounderbottom id="cbottom1" runat="server" />
</portal:outerwrapperpanel>

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PollModule.ascx.cs" Inherits="PollFeature.UI.PollModule" EnableViewState="true" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper poll">
        <%-- <portal:ModuleTitleControl ID="" runat="server" />--%>
        <style>
            .moduleTileOver {
                background: #1B7BB5;
                padding: 8px 7px;
                margin: 0 auto 4px;
                color: white;
            }

            .poll-all {
                border: 1px solid rgba(128, 128, 128, 0.19);
                margin-bottom: 5px;
            }

            .poll-header {
                text-decoration: none;
                text-align: center;
                color: rgb(23,96,147);
                text-transform: uppercase;
                border-bottom: 4px solid #b80002;
                padding-top: 10px;
                padding-bottom: 10px;
                font-weight: bold;
                font-size: 16px;
            }

            .poll-header-a {
                text-align: center;
                color: #b80002 !important;
                font-size: 16px !important;
                font-family: Arial;
            }


            .poll-bottom {
                padding-bottom: 10px;
            }

            .poll-question {
                text-indent: 1px;
            }

            .poll-show-result {
                text-decoration: none !important;
            }

                .poll-show-result input {
                    background-color: transparent;
                    color: #1B7BB5;
                    cursor: pointer;
                    font-size: 13px;
                    border-style: none;
                    padding-left: 0px;
                    text-align: left;
                    padding-bottom: 5px;
                }

            .poll-padding {
                padding-left: 5px;
            }

                .poll-padding a {
                    color: #b80002 !important;
                }
        </style>
        <portal:ModuleTitleControlCustom ID="moduleTitle" runat="server" CssClass="moduleTileOver" RenderArtisteer="true" UseLowerCaseArtisteerClasses="true" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                <div class="poll-all">
                    <div class="poll-header">
                        <asp:HyperLink ID="hplAll" CssClass="poll-header-a" runat="server"></asp:HyperLink>
                    </div>
                    <div class="poll-padding">
                        <div class="poll-bottom"></div>
                        <div class="poll-question">
                            <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="pnlPollUpdate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rblOptions" runat="server" DataTextField="Answer" DataValueField="OptionGuid"
                                    AutoPostBack="true" EnableViewState="true">
                                </asp:RadioButtonList>
                                <portal:mojoDataList ID="dlResults" runat="server" DataKeyField="OptionGuid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOption" runat="server"
                                            Text='<%# GetOptionResultText(Eval("Order"), Eval("Answer"), Eval("Votes")) %>'></asp:Label>
                                        <br />
                                        <span id="spnResultImage" runat="server"></span>

                                    </ItemTemplate>
                                </portal:mojoDataList>
                                <asp:Repeater ID="rptResults" runat="server">
                                    <HeaderTemplate>
                                        <div class="AspNet-RadioButtonList">
                                            <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="AspNet-RadioButtonList-Item">
                                            <asp:Label ID="lblOption" runat="server"
                                                Text='<%# GetOptionResultText(Eval("Order"), Eval("Answer"), Eval("Votes")) %>'></asp:Label>
                                            <br />
                                            <span id="spnResultImage" runat="server"></span>
                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("OptionGuid")%>' />
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate></ul></div></FooterTemplate>
                                </asp:Repeater>

                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblVotingStatus" runat="server" />
                                <asp:Panel ID="pnShowResult" runat="server" class="poll-show-result">
                                    <div style="display: none">
                                        <img src="../Data/Icon16x16/aitaportal.png" id="img_result" runat="server" style="vertical-align: bottom; padding-left: 6px;" />
                                    </div>
                                    <asp:Button ID="btnShowResults" runat="server" ForeColor="#b80002"></asp:Button>
                                </asp:Panel>
                                <asp:Button ID="btnBackToVote" runat="server" CssClass="buttonlink" Visible="false"></asp:Button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:HyperLink ID="lnkMyPollHistory" CssClass="poll-a" runat="server" />
                        <asp:HyperLink Visible="false" ID="lnkAll" runat="server"></asp:HyperLink>
                    </div>
                </div>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

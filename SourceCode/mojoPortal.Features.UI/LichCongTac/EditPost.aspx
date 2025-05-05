<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPost.aspx.cs" Inherits="LichCongTacFeature.UI.EditPost" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
                    <fieldset>
                        <legend runat="server" id="legendProperty"></legend>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblYear" runat="server" ForControl="ddlYear" CssClass="settinglabel" ConfigKey="YearLabel" ResourceFile="LichCongTacResources" />
                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="ddlSettingLabel1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvNam" runat="server" CssClass="rqDoc" ControlToValidate="ddlYear"
                                ValidationGroup="LichCongTac" InitialValue="0" />
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblWeek" runat="server" ForControl="ddlWeek" CssClass="settinglabel" ConfigKey="WeekLabel" ResourceFile="LichCongTacResources" />
                            <asp:DropDownList ID="ddlWeek" runat="server" AutoPostBack="true" CssClass="ddlSettingLabel1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvWeek" runat="server" CssClass="rqDoc" ControlToValidate="ddlWeek"
                                ValidationGroup="LichCongTac" InitialValue="0" />
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="txtStartWeek" CssClass="settinglabel" ConfigKey="StartWeekLabel" ResourceFile="LichCongTacResources" />
                            <asp:TextBox ID="txtStartWeek" runat="server" CssClass="verywidetextbox forminput startday" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="txtEndWeek" CssClass="settinglabel" ConfigKey="EndWeekLabel" ResourceFile="LichCongTacResources" />
                            <asp:TextBox ID="txtEndWeek" runat="server" CssClass="verywidetextbox forminput endday" Enabled="false"></asp:TextBox>
                        </div>
                        <%--<div class="settingrow">
                            <mp:SiteLabel id="SiteLabel3" runat="server" ForControl="ddlDay" CssClass="settinglabel" ConfigKey="DayLabel" ResourceFile="LichCongTacResources" />
                            <asp:DropDownList ID="ddlDay" runat="server" CssClass="ddlSettingLabel1">
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDay" runat="server" CssClass="rqDoc" ControlToValidate="ddlDay"
                                       ValidationGroup="LichCongTac" InitialValue="0" />
                            </div>--%>
                        <div class="settingrow date">
                            <mp:SiteLabel ID="lblStartDate" runat="server" ForControl="dpDateStart" CssClass="settinglabel" ConfigKey="StartDateLabel" ResourceFile="LichCongTacResources" />
                            <%--<mp:jsCalendarDatePicker ID="dpStartDate" runat="server" ShowTime="false" CssClass="datetime-input calendarPicker" />--%>
                            <mp:DatePickerControl ID="dpDateStart" runat="server" ShowTime="false" CssClass="datetime-input dpDateStart" />
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="dpDateStart"
                                ValidationGroup="LichCongTac" />
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="SiteLabel6" runat="server" ForControl="ddlCategoryAuthor" CssClass="settinglabel" ConfigKey="ArticleCategoryAuthorLabel" ResourceFile="ArticleResources" />
                             <asp:ListBox ID="lboxCategoryAuthor" Width="260" SelectionMode="Multiple" runat="server"></asp:ListBox>
                        </div>
                        <div class="settingrow">
                            <mp:SiteLabel ID="lblSummary" runat="server" ForControl="edBuoiSang" CssClass="settinglabel" ConfigKey="ContentLabel" ResourceFile="LichCongTacResources" />
                            <div class="settingrow">
                                <mpe:EditorControl ID="edBuoiSang" runat="server">
                                </mpe:EditorControl>
                            </div>

                        </div>
                        <div class="settingrow" style="display: none">
                            <mp:SiteLabel ID="SiteLabel3" runat="server" ForControl="edBuoiChieu" CssClass="settinglabel" ConfigKey="BuoiChieuLabel" ResourceFile="LichCongTacResources" />
                            <div class="settingrow">
                                <mpe:EditorControl ID="edBuoiChieu" runat="server">
                                </mpe:EditorControl>
                            </div>

                        </div>
                        <div class="settingrow" style="display: none">
                            <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="edBuoiToi" CssClass="settinglabel" ConfigKey="BuoiToiLabel" ResourceFile="LichCongTacResources" />
                            <div class="settingrow">
                                <mpe:EditorControl ID="edBuoiToi" runat="server">
                                </mpe:EditorControl>
                            </div>

                        </div>
                        <div class="settingrow" style="display: none">
                            <mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="edAddress" CssClass="settinglabel" ConfigKey="AddressLabel" ResourceFile="LichCongTacResources" />
                            <div class="settingrow">
                                <mpe:EditorControl ID="edAddress" runat="server">
                                </mpe:EditorControl>
                            </div>
                        </div>
                        <%--                            <div class="settingrow date">
                           <mp:SiteLabel id="lblEndDate" runat="server" ForControl="dpEndDate" CssClass="settinglabel" ConfigKey="EndDateLabel" ResourceFile="LichCongTacResources" />
                            <mp:jsCalendarDatePicker ID="dpEndDate" runat="server" ShowTime="false" CssClass="datetime-input" />			
                            </div>--%>

                        <div class="settingrow">
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="LichCongTac" />
                            <asp:Button ID="btnDel" runat="server" />
                            <asp:Button ID="btnCancel" runat="server" />
                        </div>
                    </fieldset>
                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
    <script type="text/javascript">
        var max = $(".endday").val();
        var min = $(".startday").val();
        $(".datepicker").datepicker({
            minDate: min,
            maxDate: max
        });
    </script>
    <style>
        .date input {
            float: left;
            width: 200px !important;
        }

        .rqDoc {
            margin-left: 100px !important;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

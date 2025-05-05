<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="EditPostReference.aspx.cs" Inherits="ArticleFeature.UI.EditPostReference" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <asp:Panel ID="pnlArticle" runat="server" DefaultButton="btnUpdate" CssClass="panelwrapper admin editpage blogedit">
        <portal:ModuleTitleControl ID="moduleTitle" runat="server" RenderArtisteer="true"
            UseLowerCaseArtisteerClasses="true" />
        <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
            <div class="modulecontent">

                <fieldset>
                    <legend>
                        <mp:SiteLabel ID="lblArticleEntry" runat="server" ConfigKey="ArticleEditEntryLabel" ResourceFile="ArticleResources"
                            UseLabelTag="false"></mp:SiteLabel>
                    </legend>
                    <div id="divtabs" class="mojo-tabs">
                        <ul>
                            <li class="selected">
                                <asp:Literal ID="litContentTab" runat="server" /></li>
                            <li>
                                <asp:Literal ID="litMetaTab" runat="server" /></li>
                        </ul>
                        <div id="tabContent">
                            <asp:Panel ID="pnlCategories" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="settingrow articleeditcategories">
                                            <mp:SiteLabel ID="lblCat" runat="server" ForControl="txtCategory" CssClass="settinglabel"
                                                ConfigKey="ArticleEditCategoryLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                            <asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList>
                                        </div>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvCategory" ControlToValidate="ddlCategories" InitialValue="0" ErrorMessage="" ValidationGroup="article"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <div class="settingrow">
                                <mp:SiteLabel ID="lblTitle" runat="server" ForControl="txtTitle" CssClass="settinglabel"
                                    ConfigKey="ArticleEditTitleLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ValidationGroup="article" />
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="lblSummary" runat="server" ForControl="txtSummary" CssClass="settinglabel"
                                    ConfigKey="ArticleSummaryLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <asp:TextBox ID="txtSummary" TextMode="MultiLine" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                                </asp:TextBox>
                            </div>
                            <div class="settingrow">
                                <mpe:EditorControl ID="edContent" runat="server">
                                </mpe:EditorControl>
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="lblAuthor" runat="server" ForControl="txtAuthor" CssClass="settinglabel"
                                    ConfigKey="ArticleEditAuthorLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <asp:TextBox ID="txtAuthor" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                                </asp:TextBox>
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="SiteLabel14" runat="server" ForControl="txtImageUrl" CssClass="settinglabel"
                                    ConfigKey="ArticleEditItemImageUrlLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <NeatUpload:InputFile ID="nuImageUrl" runat="server" />
                                <br />
                                <asp:Label ID="lblImageUrlError" runat="server" />
                            </div>
                            <div class="settingrow">
                                <asp:UpdatePanel ID="updImgDel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div runat="server" id="divImage" visible="false">
                                            <mp:SiteLabel ID="lblCurrentImage" runat="server" ForControl="txtImageUrl" CssClass="settinglabel"
                                                ConfigKey="ArticleCurrentImageLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                            <asp:Image runat="server" ID="imgView" Width="100px" Style="max-height: 60px" />
                                            <asp:ImageButton ID="btnDeleteImg" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="txtItemUrl" CssClass="settinglabel"
                                    ConfigKey="ArticleEditItemUrlLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <asp:TextBox ID="txtItemUrl" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                                </asp:TextBox>
                                <span id="spnUrlWarning" runat="server" style="font-weight: normal;" class="txterror"></span>
                                <asp:HiddenField ID="hdnTitle" runat="server" />
                                <asp:RegularExpressionValidator ID="regexUrl" runat="server" ControlToValidate="txtItemUrl"
                                    ValidationExpression="((~/){1}\S+)" Display="None" ValidationGroup="article" />
                            </div>
                            <div class="settingrow tag">
                                <mp:SiteLabel ID="Sitelabel18" runat="server" ForControl="txtTag" ConfigKey="ArticleTag"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <asp:TextBox ID="txtTag" runat="server" CssClass="verywidetextbox"></asp:TextBox>
                            </div>

                            <div class="settingrow ishot">
                                <mp:SiteLabel ID="Sitelabel15" runat="server" ForControl="chkIsHot" ConfigKey="IsHotLabel"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <asp:CheckBox ID="chkIsHot" runat="server" CssClass="forminput" Checked="false"></asp:CheckBox>
                            </div>
                            <div class="settingrow ishot">
                                <mp:SiteLabel ID="lblIsHome" runat="server" ForControl="chkIsHome" ConfigKey="IsHomeLabel"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <asp:CheckBox ID="chkIsHome" runat="server" CssClass="forminput" Checked="false"></asp:CheckBox>
                            </div>
                            <div class="settingrow ishot">
                                <mp:SiteLabel ID="lblAllowComment" runat="server" ForControl="chkIsAllowComment" ConfigKey="IsAllowCommentLabel"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <asp:CheckBox ID="chkIsAllowComment" runat="server" CssClass="forminput" Checked="false"></asp:CheckBox>
                            </div>
                            <asp:Panel ID="pnlIsApproved" runat="server" CssClass="settingrow ispublished">
                                <mp:SiteLabel ID="Sitelabel13" runat="server" ForControl="chkIsApproved" ConfigKey="IsApprovedLabel"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <asp:CheckBox ID="chkIsApproved" runat="server" CssClass="forminput" Checked="false" OnCheckedChanged="chkIsApproved_CheckedChanged"></asp:CheckBox>
                            </asp:Panel>
                            <div class="settingrow date">
                                <mp:SiteLabel ID="lblStartDate" runat="server" ForControl="dpBeginDate" ConfigKey="ArticleEditStartDateLabel"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <mp:jsCalendarDatePicker ID="dpBeginDate" runat="server" ShowTime="false" CssClass="datetime-input" />
                                <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dpBeginDate"
                                    Display="None" CssClass="txterror" ValidationGroup="article">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="settingrow date">
                                <mp:SiteLabel ID="lblEndDate" runat="server" ForControl="dpEndDate" ConfigKey="ArticleEditEndDateLabel"
                                    ResourceFile="ArticleResources" CssClass="settinglabel"></mp:SiteLabel>
                                <mp:jsCalendarDatePicker ID="dpEndDate" runat="server" ShowTime="false" CssClass="datetime-input" />
                            </div>

                            <div class="settingrow">
                                <mp:SiteLabel ID="SiteLabel35" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                                <div class="forminput">
                                    <NeatUpload:ProgressBar ID="progressBar" runat="server">
                                    </NeatUpload:ProgressBar>
                                    <portal:mojoButton ID="btnUpdate" runat="server" ValidationGroup="article" />
                                    <portal:mojoButton ID="btnSaveAndPreview" runat="server" ValidationGroup="article" Visible="false" />&nbsp;
                                <portal:mojoButton ID="btnDelete" runat="server" Text="Delete this item" CausesValidation="false" />
                                    <asp:HyperLink ID="lnkCancel" runat="server" CssClass="cancellink" />&nbsp;
                                </div>
                                <br />
                                <portal:mojoLabel ID="lblError" runat="server" CssClass="txterror" />
                                <asp:HiddenField ID="hdnHxToRestore" runat="server" />
                                <asp:ImageButton ID="btnRestoreFromGreyBox" runat="server" />
                            </div>
                            <asp:Panel ID="pnlHistory" runat="server" Visible="false">
                                <div class="settingrow">
                                    <mp:SiteLabel ID="SiteLabel10" runat="server" CssClass="settinglabel" ConfigKey="VersionHistory"
                                        ResourceFile="ArticleResources"></mp:SiteLabel>
                                </div>
                                <div class="settingrow">
                                    <asp:UpdatePanel ID="updHx" UpdateMode="Conditional" runat="server">
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="grdHistory" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <mp:mojoGridView ID="grdHistory" runat="server" CssClass="editgrid" AutoGenerateColumns="false"
                                                DataKeyNames="Guid" EnableTheming="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%# DateTimeHelper.GetTimeZoneAdjustedDateTimeString(Eval("CreatedUtc"), timeOffset)%>
                                                            <br />
                                                            <%# Eval("UserName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%# DateTimeHelper.GetTimeZoneAdjustedDateTimeString(Eval("HistoryUtc"), timeOffset)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <portal:GreyBoxHyperlink ID="gb1" runat="server" ClientClick="return GB_showFullScreen(this.title, this.href)"
                                                                NavigateUrl='<%# SiteRoot + "/Article/ArticleCompare.aspx?pageid=" + pageId + "&mid=" + moduleId + "&ItemID=" + itemId + "&h=" + Eval("Guid") %>'
                                                                Text='<%# Resources.ArticleResources.CompareHistoryToCurrentLink %>' ToolTip='<%# Resources.ArticleResources.CompareHistoryToCurrentLink %>'
                                                                DialogCloseText='<%# Resources.ArticleResources.DialogCloseLink %>' />
                                                            <asp:Button ID="btnRestoreToEditor" runat="server" Text='<%# Resources.ArticleResources.RestoreToEditorButton %>'
                                                                CommandName="RestoreToEditor" CommandArgument='<%# Eval("Guid") %>' />
                                                            <asp:Button ID="Button1" runat="server" CommandName="DeleteHistory" CommandArgument='<%# Eval("Guid") %>'
                                                                Visible='<%# isAdmin %>' Text='<%# Resources.ArticleResources.DeleteHistoryButton %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </mp:mojoGridView>
                                            <div class="modulepager">
                                                <portal:mojoCutePager ID="pgrHistory" runat="server" />
                                            </div>
                                            <div id="divHistoryDelete" runat="server" class="settingrow">
                                                <mp:SiteLabel ID="SiteLabel8" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                                                <portal:mojoButton ID="btnDeleteHistory" runat="server" Text="" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="settingrow">
                                    <portal:mojoLabel ID="lblErrorMessage" runat="server" CssClass="txterror" />
                                    &nbsp;
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="tabMeta">
                            <div class="settingrow">
                                <mp:SiteLabel ID="SiteLabel6" runat="server" ForControl="txtMetaDescription" CssClass="settinglabel"
                                    ConfigKey="MetaDescriptionLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <asp:TextBox ID="txtMetaDescription" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                                </asp:TextBox>
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="SiteLabel7" runat="server" ForControl="txtMetaKeywords" CssClass="settinglabel"
                                    ConfigKey="MetaKeywordsLabel" ResourceFile="ArticleResources"></mp:SiteLabel>
                                <asp:TextBox ID="txtMetaKeywords" runat="server" MaxLength="255" CssClass="forminput verywidetextbox">
                                </asp:TextBox>
                            </div>
                              <div class="settingrow">
                                <mp:SiteLabel ID="lblAdditionalMetaTags" runat="server" CssClass="settinglabel" ConfigKey="MetaAdditionalLabel"
                                    ResourceFile="ArticleResources"></mp:SiteLabel>
                                <portal:mojoHelpLink ID="MojoHelpLink25" runat="server" HelpKey="pagesettingsadditionalmetahelp" />
                            </div>
                            <div class="settingrow">
                                <mp:SiteLabel ID="SiteLabel20" runat="server" CssClass="settinglabel" ConfigKey="spacer" />
                                <div class="forminput">
                                    <portal:mojoButton ID="btnUpdate3" runat="server" ValidationGroup="article" />&nbsp;
                                <portal:mojoButton ID="btnDelete3" runat="server" CausesValidation="False" />
                                    <asp:HyperLink ID="lnkCancel3" runat="server" CssClass="cancellink" />&nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <asp:HiddenField ID="hdnReturnUrl" runat="server" />
            <div class="cleared">
            </div>
        </portal:mojoPanel>
    </asp:Panel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    <portal:SessionKeepAliveControl ID="ka1" runat="server" />
    <style>
        .date input {
            float: left;
            width: 200px !important;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="Detail.aspx.cs" Inherits="DocumentFeature.UI.Detail" %>

<%@ Import Namespace="DocumentFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
        <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
        <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
            <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
                <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

                    <div class="module">
                        <div class="module-table-body">
                            <asp:Panel ID="pnDetail_Doc" runat="server">
                                <span style="color: #338AD0; font-weight: bold; font-size: 14px; line-height: 35px;">Xem với cỡ chữ 
                                                                    <i class="fa fa-font" onclick="fontMinus()" title="Xem với cỡ chữ nhỏ hơn" aria-hidden="true" style="font-size: 18px; color: #337ab7; cursor: pointer;"></i>
                                    <i class="fa fa-font fa-2x" aria-hidden="true" title="Xem với cỡ chữ to hơn" onclick="fontPlus()" style="color: #337ab7; cursor: pointer;"></i>
                                </span>
                                <div id="content_doc">
                                    <div class="doc_des" id="docdes">
                                        <h3 class="lawh3">
                                            <asp:Label ID="lbtitle" runat="server"></asp:Label>
                                        </h3>
                                    </div>
                                    <table class="table table-striped table-bordered table-hover table-condensed">
                                        <tr>
                                            <td class="doc_tit">
                                                <mp:SiteLabel ID="lblSign" ConfigKey="SignHeaderLabel" ResourceFile="DocumentResources" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbSign" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="doc_tit">
                                                <mp:SiteLabel ID="SiteLabel1" ConfigKey="DateEffectHeaderLabel" ResourceFile="DocumentResources" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbDatePromulgate" runat="server"></asp:Label></td>
                                        </tr>
                                        <%if (DateEffectVisible)
                                            { %>
                                        <tr>
                                            <td class="doc_tit">
                                                <mp:SiteLabel ID="SiteLabel2" ConfigKey="DatePromulgateHeaderLabel" ResourceFile="DocumentResources" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbDateEffect" runat="server"></asp:Label></td>
                                        </tr>
                                        <%} %>
                                        <tr>
                                            <td class="doc_tit">
                                                <mp:SiteLabel ID="SiteLabel3" ConfigKey="SignerHeaderLabel" ResourceFile="DocumentResources" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbSinger" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="doc_tit">
                                                <mp:SiteLabel ID="SiteLabel4" ConfigKey="SummaryDocHeaderLabel" ResourceFile="DocumentResources" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbSummary" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="doc_tit">
                                                <mp:SiteLabel ID="SiteLabel5" ConfigKey="CoQuanIDHeaderLabel" ResourceFile="DocumentResources" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbCoQuan" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="doc_tit">
                                                <mp:SiteLabel ID="SiteLabel6" ConfigKey="LoaiVBHeaderLabel" ResourceFile="DocumentResources" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbLoaivb" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="display: none">
                                            <td class="doc_tit">
                                                <mp:SiteLabel ID="SiteLabel7" ConfigKey="SubjectHeaderLabel" ResourceFile="DocumentResources" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbSubject" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Panel ID="pnlFile" runat="server">
                                    <h3 class="lawh3"><em class="fa fa-download">&nbsp;</em><mp:SiteLabel ID="SiteLabel8" ConfigKey="FilePathLabel" ResourceFile="DocumentResources" runat="server" />
                                    </h3>
                                    <div class="list-group laws-download-file">
                                        <div class="list-group-item">
                                            <a href="javascript:void(0)" title="Tải tập tin demo.pdf">Tải tập tin : </a>
                                            <strong>
                                                <asp:HyperLink ID="lnkAttach" CssClass="lnk_others" runat="server" Target="_blank"></asp:HyperLink>
                                            </strong>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </asp:Panel>
                            <asp:Panel ID="pnOthers" runat="server">
                                <h3 class="lawh3"><em class="fa fa-book">&nbsp;</em>
                                    <mp:SiteLabel ID="SiteLabel9" ConfigKey="DocumentOtherLabel" ResourceFile="DocumentResources" runat="server" />

                                </h3>
                                <%--<asp:Image runat="server" ID="imgOther" ImageUrl='<%#OtherImageUrl %>' />--%>
                                <table class="table table-striped table-bordered table-hover table-condensed">
                                    <tbody>
                                        <asp:Repeater ID="rptOthers" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("Signer") %></td>
                                                    <td>
                                                        <%#string.Format("{0:dd/MM/yyyy}",Eval("DatePromulgate")) %>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkOthers" CssClass="lnk_others" runat="server" NavigateUrl='<%#DocumentUltils.FormatBlogTitleUrl(siteSettings.SiteRoot, Eval("ItemUrl").ToString(), Convert.ToInt32(Eval("ItemID")), pageId, moduleId) %>'><%#formatContent(Eval("Summary").ToString()) %></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <%--                            <asp:Panel ID="pnlArticlePager" runat="server" CssClass="ArticlePager">
                                <portal:mojoCutePager ID="pgrArticle" runat="server" />
                            </asp:Panel>--%>
                        </div>
                    </div>

                </portal:InnerBodyPanel>
            </portal:OuterBodyPanel>
            <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
        </portal:InnerWrapperPanel>
        <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
    </portal:OuterWrapperPanel>
    <script>
        var list_childNodes = document.getElementById("content_doc").childNodes.length;
        function fontPlus() {
            var divTemp = document.getElementById('content_doc');
            if (divTemp.style.fontSize == '') divTemp.style.fontSize = '12px';
            var s = divTemp.style.fontSize;
            if ((s.indexOf('px') > -1) && (s.indexOf('px') == (s.length - 2))) s = s.substring(0, s.indexOf('px'));
            divTemp.style.fontSize = (parseFloat(s) + 1) + 'px';

            divTemp = document.getElementById('docdes');
            if (divTemp.style.fontSize == '') divTemp.style.fontSize = '12px';
            s = divTemp.style.fontSize;
            if ((s.indexOf('px') > -1) && (s.indexOf('px') == (s.length - 2))) s = s.substring(0, s.indexOf('px'));
            divTemp.style.fontSize = (parseFloat(s) + 1) + 'px';

            if (list_childNodes > 0) {
                var c = document.getElementById('content_doc').childNodes;
                for (var i = 1; i <= list_childNodes; i++) {
                    //if (i == 1 || i % 2 > 0) {
                    if (c[i] != undefined && c[i].style != undefined) {
                        var s1 = c[i].style.fontSize;
                        if ((s1.indexOf('px') > -1) && (s1.indexOf('px') == (s1.length - 2)))
                            s1 = s1.substring(0, s1.indexOf('px'));
                        c[i].style.fontSize = (parseFloat(s1) + 1) + 'px';
                        SetFontSizeUp(c[i]);
                    }
                    //}
                }
            }

        }

        function SetFontSizeUp(obj) {
            var list_childNodes = obj.childNodes.length;
            if (list_childNodes > 0) {
                var c = obj.childNodes;
                console.log(c[0]);
                for (var i = 0; i < list_childNodes; i++) {
                    if (c[i] != undefined && c[i].style != undefined) {
                        var s1 = c[i].style.fontSize;
                        //if (i == 1 || i % 2 > 0) {
                        if ((s1.indexOf('px') > -1) && (s1.indexOf('px') == (s1.length - 2)))
                            s1 = s1.substring(0, s1.indexOf('px'));
                        c[i].style.fontSize = (parseFloat(s1) + 1) + 'px';
                        //}
                        if (c[i].childNodes.length > 0) {
                            SetFontSizeUp(c[i])
                        }
                    }
                }
            }
        }

        function fontMinus() {
            var divTemp = document.getElementById('content_doc');
            if (divTemp.style.fontSize == '') divTemp.style.fontSize = '12px';
            var s = divTemp.style.fontSize;
            if ((s.indexOf('px') > -1) && (s.indexOf('px') == (s.length - 2))) s = s.substring(0, s.indexOf('px'));
            divTemp.style.fontSize = (parseFloat(s) - 1) + 'px';
            divTemp = document.getElementById('docdes');
            if (divTemp.style.fontSize == '') divTemp.style.fontSize = '12px';
            s = divTemp.style.fontSize;
            if ((s.indexOf('px') > -1) && (s.indexOf('px') == (s.length - 2))) s = s.substring(0, s.indexOf('px'));
            divTemp.style.fontSize = (parseFloat(s) - 1) + 'px';

            if (list_childNodes > 0) {
                var c = document.getElementById('content_doc').childNodes;
                for (var i = 1; i <= list_childNodes; i++) {
                    //if (i == 1 || i % 2 > 0) {
                    if (c[i] != undefined && c[i].style != undefined) {
                        var s1 = c[i].style.fontSize;
                        if ((s1.indexOf('px') > -1) && (s1.indexOf('px') == (s1.length - 2)))
                            s1 = s1.substring(0, s1.indexOf('px'));
                        c[i].style.fontSize = (parseFloat(s1) - 1) + 'px';
                        SetFontSizeDown(c[i]);
                    }
                    //}
                }
            }
        }

        function SetFontSizeDown(obj) {
            var list_childNodes = obj.childNodes.length;
            if (list_childNodes > 0) {
                var c = obj.childNodes;
                console.log(c[0]);
                for (var i = 0; i < list_childNodes; i++) {
                    if (c[i] != undefined && c[i].style != undefined) {
                        var s1 = c[i].style.fontSize;
                        //if (i == 1 || i % 2 > 0) {
                        if ((s1.indexOf('px') > -1) && (s1.indexOf('px') == (s1.length - 2)))
                            s1 = s1.substring(0, s1.indexOf('px'));
                        c[i].style.fontSize = (parseFloat(s1) - 1) + 'px';
                        //}
                        if (c[i].childNodes.length > 0) {
                            SetFontSizeDown(c[i])
                        }
                    }
                }
            }
        }
    </script>
    <style>
        td.doc_tit {
            width: 15%;
        }

        span.attach {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

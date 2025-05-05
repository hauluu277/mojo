<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="QuestionList.ascx.cs" Inherits="PollFeature.UI.QuestionList" %>
<%@ Import Namespace="PollFeature.UI" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%--<div class="add-remove">
    <div style="padding: 30px">
        <portal:mojoButton ID="btnAddNew" runat="server" />
        <portal:mojoButton ID="btnRemoveAll" BackColor="#ff0909" runat="server" OnClientClick="ConfirmDeleteAll();" />
    </div>
</div>--%>
<fieldset runat="server">
    <legend id="lengendPoll" runat="server"></legend>
    <asp:UpdatePanel ID="RencenListUpdatePanel" runat="server">
        <ContentTemplate>
            <table width="800">
                <tr>
                    <td>
                        <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="drIsPublic" CssClass="lbl-fix" ConfigKey="IsApprovePoll" ResourceFile="PollResources" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drlIsPublic" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
                    </td>
                    <td style="width: 100px"></td>
                    <td>
                        <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="drlIsApprove" CssClass="lbl-fix" ConfigKey="IsPublishPoll" ResourceFile="PollResources" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drlIsApprove" CssClass="recentlist-drl-fix" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mp:SiteLabel ID="SiteLabel3" runat="server" CssClass="lbl-fix" ConfigKey="PollKeywordLable" ResourceFile="PollResources" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch" CssClass="recentlist-textbox-fix" MaxLength="220" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td>
                        <portal:mojoButton ID="btnSearch" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>

    <div style="padding: 20px 20px 20px 0px">
        <portal:mojoButton ID="btnAddNew" runat="server" />
        <portal:mojoButton ID="btnRemoveAll" BackColor="#ff0909" runat="server" />
        <portal:mojoButton ID="btnActived" runat="server" BackColor="#3333ff" />
    </div>
<asp:Panel ID="pnlPostList" runat="server">
    <asp:Repeater ID="rptPoll" runat="server" SkinID="Blog">
        <HeaderTemplate>
            <table id="myTable" style="width: 100%">
                <tr>


                    <th style="width: 5%">
                        <input type="checkbox" onclick="DoCheckAll(this)" id="checkAll" runat="server" />
                    </th>
                    <th class="tbl-header" style="width: 35%">
                        <%#Resources.PollResources.PollQuestionTitle %>
                    </th>
                    <%--                    <th class="tbl-header">
                        <%#Resources.PollResources.PollApproveTitle %>
                    </th>
                    <th class="tbl-header">
                        <%#Resources.PollResources.PollPublishTitle %>
                    </th>--%>
                    <th class="tbl-header" style="text-align: left; width: 25%">
                    <th class="tbl-header" style="text-align: center">
                        <asp:Label ID="lblTotalVodes" runat="server" Text='<%#Resources.PollResources.PollTotalVotes %>'></asp:Label>
                    </th>
                    <th class="tbl-header" style="text-align: center">
                        <asp:Label ID="lblApprove" runat="server" Text='<%#Resources.PollResources.PollsApprove %>'></asp:Label>
                    </th>
                    <th class="tbl-header" style="text-align: center">
                        <asp:Label ID="bllPublish" runat="server" Text='<%#Resources.PollResources.PollsPublish %>'></asp:Label>
                    </th>
                    <%--                    <th class="tbl-header">
                        <%#Resources.PollResources.PollOpinionLeaders %>
                    </th>--%>
                    <th style="width: 5%" class="tbl-header"></th>
                </tr>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>

                <td>
                    <asp:Literal ID="repeaterID" runat="server" Text='<%# Eval("PollGuid") %>' Visible="false"></asp:Literal>
                    <asp:CheckBox ID="chk" runat="server" CssClass="checkItem" onclick="CheckItem(this)" Checked="false" />
                </td>
                <td>
                    <div class="name-title">

                        <asp:HyperLink ID="hplName" SkinID="BlogTitle" EnableViewState="false" runat="server"
                            ToolTip='<%#Eval("Question") %>' Text='<%#Eval("Question") %>' NavigateUrl='<%#SiteRoot+"/Poll/PollView.aspx?PollGuid="+Eval("PollGuid")+"&pageid="+PageId+"&mid="+ModuleId %>'></asp:HyperLink>
                    </div>
                </td>
                <td>
                    <asp:Literal ID="liter" runat="server" Text='<%#GetTime(Convert.ToDateTime(Eval("ActiveFrom").ToString()),Convert.ToDateTime(Eval("ActiveTo").ToString())) %>' />
                </td>
                <%--                <td>
                    <asp:Label ID="textPhone" Text='<%#GetApprove(Convert.ToBoolean(Eval("IsApprove"))) %>' runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="textFax" Text='<%#GetPublish(Convert.ToBoolean(Eval("IsPublish"))) %>' runat="server"></asp:Label>
                </td>--%>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" ImageUrl='../../Data/Icon16x16/view-poll.png' PostBackUrl='<%#SiteRoot+"/Poll/PollChoose.aspx?pageid="+PageId+"&mid="+ModuleId+"&pollGuid="+Eval("PollGuid")  %>'
                        CausesValidation="false" />
                    <%#Eval("TotalVotes") %>

                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ibtnApprove" runat="server" ImageUrl='<%# mojoPortal.Features.PollUtils.ImageApprove(DataBinder.Eval(Container.DataItem,"IsApprove") !=null ? DataBinder.Eval(Container.DataItem,"IsApprove").ToString() : string.Empty) %>'
                        CommandName="ApproveItem" CommandArgument='<%# Eval("PollGuid") %>'
                        CausesValidation="false" />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ibtnPublish" runat="server" ImageUrl='<%# mojoPortal.Features.PollUtils.ImageApprove(DataBinder.Eval(Container.DataItem,"IsPublish") !=null ? DataBinder.Eval(Container.DataItem,"IsPublish").ToString() : string.Empty) %>'
                        CommandName="PublishItem" CommandArgument='<%# Eval("PollGuid") %>'
                        CausesValidation="false" />
                </td>

                <%--                <td>
                    <%# Eval("TotalOpinion") %>
                    <asp:HyperLink ID="hplOpinion" runat="server" Text='<%#Resources.PollResources.PollView %>' NavigateUrl='<%#SiteRoot+"/Poll/PollView.aspx?pageid="+PageId+"&mid="+ModuleId+"&pollGuid="+Eval("PollGuid") %>'></asp:HyperLink>
                </td>--%>
                <td>
                    <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl='<%# EditLinkImageUrl %>'
                        CommandName="EditItem" CommandArgument='<%# Eval("PollGuid") %>'
                        Visible='true'
                        CausesValidation="false" />

                    <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl='<%# DeleteLinkImageUrl %>'
                        CommandName="DeleteItem" CommandArgument='<%# Eval("PollGuid") %>'
                        Visible='true'
                        CausesValidation="false" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
        </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlPollPager" runat="server" CssClass="ArticlePager">
        <portal:mojoCutePager ID="pgrPoll" runat="server" />
    </asp:Panel>
</asp:Panel>
<asp:Label ID="Pollnull" runat="server" Visible="false"></asp:Label>
<style>
    table, caption, tbody, thead, tr, th, td {
        font-size: 100%;
        outline: 0 none;
        vertical-align: baseline;
        margin-top: 20px;
        border-collapse: collapse;
        border-spacing: 0;
        height: 30px;
    }

    #myTable th {
        background: #eeeeee;
        text-align: left;
    }

    #myTable td {
        padding: 10px 0px 10px 0px;
        text-align: left;
    }

    .add-remove {
        background-color: #F4F5F6;
        height: 80px;
        text-align: right;
    }



    .name-title {
        display: block;
        /*-webkit-margin-before: 1.33em;*/
        -webkit-margin-after: 1.33em;
        -webkit-margin-start: 0px;
        -webkit-margin-end: 0px;
        /*font-weight: bold;*/
    }


        .name-title a {
            /*font-weight: bold;*/
            /*font-size: 15px;*/
            color: #027BA8;
            text-decoration: none;
        }

    .lbl-fix {
        width: 70px;
        float: left;
    }

    .lbl2-fix {
        width: 90px;
        margin-right: 10px;
    }

    .add-remove {
        background-color: #F4F5F6;
        height: 80px;
        text-align: right;
    }

    .recentlist-textbox-fix {
        border-radius: 3px;
        width: 242px !important;
        height: 10px;
    }

    .recentlist-drl-fix {
        width: 250px;
        height: 26px;
        border-radius: 3px;
    }
</style>
<script>
    function DoCheck(control, check) {
        for (var i = 0; i < control.length; i++) {
            if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('chk') != -1) {
                control[i].checked = check;
            }
        }
    };
    function DoCheckAll(obj) {
        var control = document.getElementsByTagName("input");
        DoCheck(control, obj.checked);
    };

    function CheckItem(obj) {
        var control = document.getElementsByTagName("input");
        var controlChkAll;
        var allControl = 0;
        var checkedControl = 0;
        for (var i = 0; i < control.length; i++) {
            if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('chk') != -1) {
                allControl++;
                if (control[i].checked == true) {
                    checkedControl++;
                }
            }
            if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('checkAll') != -1) {
                controlChkAll = control[i];
            }
        }
        if (allControl == checkedControl) {
            controlChkAll.checked = true;
        }
        else {
            controlChkAll.checked = false;
        }
    }
    //function ConfirmDeleteAll() {
    //    var isTrue = confirm("Dữ liệu sẽ bị xóa vĩnh viễn. Bạn có thực sự muốn xóa?");
    //    if (isTrue) {
    //        //ChangeAction('hdAction', 'delete');
    //        //document.getElementById('adminForm').submit();
    //        //alert("chức năng đang hoàn thiện, vui lòng thử lại sau");
    //        return false;
    //    }

    //    return false;
    //}
</script>

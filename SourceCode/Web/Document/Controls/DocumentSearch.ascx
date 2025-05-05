<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DocumentSearch.ascx.cs" Inherits="DocumentFeature.UI.DocumentSearch" %>
<%@ Import Namespace="mojoPortal.Features" %>
<%@ Import Namespace="DocumentFeature.UI" %>
<div class="search-box">
    <fieldset>
        <legend runat="server" id="legendSearchProperty"></legend>
        <div class="CountLinhVuc">
            <asp:Repeater ID="rptCountLinhVuc" runat="server">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkLinhVuc" runat="server" NavigateUrl='<%#Eval("Url") %>'><%#Eval("Name") %><%#Eval("Count") %></asp:HyperLink><br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="bottom-spacing">
            <div class="search-item">
                <mp:SiteLabel ID="lblCat" runat="server" ForControl="ddlLinhVuc" CssClass="search-label"
                    ConfigKey="LinhVucLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlLinhVuc" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <%--<div class="search-item">
                <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="ddlLoaiVB" CssClass="search-label"
                    ConfigKey="LoaiVBLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlLoaiVB" runat="server" OnSelectedIndexChanged="ddlLoaiVB_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>--%>
            <div class="search-item">
                <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="ddlCoQuan" CssClass="search-label"
                    ConfigKey="CoQuanIDLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlCoQuan" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="ddlNam" CssClass="search-label"
                    ConfigKey="NamLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:DropDownList Width="100%" ID="ddlNam" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="search-item">
                <mp:SiteLabel ID="lblKeyword" runat="server" ForControl="txtKeyword" CssClass="search-label"
                    ConfigKey="KeywordLabel" ResourceFile="DocumentResources"></mp:SiteLabel>
                <div class="search-control">
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                </div>
            </div>
            
            <div class="searchSubmit">
                <portal:mojoButton ID="btnSearch" runat="server" />
            </div>
        </div>
    </fieldset>
</div>
<style>
    .search-box{
        width:100%;
        float:left;
    }
    .searchSubmit {
        float: right;
    }

    .search-item {
        float: left;
        width: 45%;
        margin-right: 5%;
        margin-bottom: 5px;
    }

    .search-label {
        float: left;
        min-width: 150px;
        display: block;
    }

    .article-title a {
        font-size: 13px;
        color: #0a8acb;
        font-weight: bold;
        text-decoration: none;
    }

        .article-title a:hover {
            text-decoration: underline;
        }

    .author {
        font-size: 11px;
        color: #C0C0C0;
        font-style: italic;
        font-weight: bold;
    }


    table, caption, tbody, thead, tr, th, td {
        border: 0 none;
        font-size: 100%;
        outline: 0 none;
        vertical-align: baseline;
    }

    .bottom-spacing {
        margin-bottom: 20px;
    }


    div.module {
        float: left;
        margin-bottom: 20px;
        width: 100%;
    }


        div.module div.module-table-body {
            float: left;
            padding: 0;
            width: 100%;
        }

        div.module table {
            border-bottom: 1px solid #d9d9d9;
            border-left: 1px solid #d9d9d9;
            margin: 0 0 10px;
            width: 100%;
        }

            div.module table th {
                background-color: #eeeeee;
                /*border-bottom: 1px solid #d9d9d9;*/
                /*border-right: 1px solid #d9d9d9;*/
                color: #444444;
                padding: 5px;
                text-align: left;
            }

            div.module table tr:hover {
                background-color: #e7f6fa;
            }

            div.module table td {
                background-color: #ffffff;
                /*border-right: 1px solid #d9d9d9;*/
                padding: 5px;
            }

    .align-center {
        text-align: center;
    }

    a, a:visited {
        text-decoration: none;
    }

    table {
        border-collapse: collapse;
        border-spacing: 0;
    }

        table.tablesorter {
            text-align: left;
            width: 100%;
        }

            table.tablesorter thead tr .tbl-header {
                background-image: url("/Data/SiteImages/article-icon/bg.gif");
                background-position: right center;
                background-repeat: no-repeat;
                cursor: pointer;
            }

            table.tablesorter tbody td {
                background-color: #fff;
                color: #3d3d3d;
                padding: 4px;
                vertical-align: top;
            }

            table.tablesorter tbody tr.odd td {
                background-color: #f1f5fa;
            }

            table.tablesorter tbl-thead tr .headerSortUp {
                background-image: url("/Data/SiteImages/article-icon/asc.gif");
            }

            table.tablesorter tbl-thead tr .headerSortDown {
                background-image: url("/Data/SiteImages/article-icon/desc.gif");
            }

            table.tablesorter tbl-thead tr .headerSortDown, table.tablesorter tbl-thead tr .headerSortUp {
                background-color: #dddddd;
            }

    div#toolbar-box {
        background: none repeat scroll 0 0 #fbfbfb;
        margin-top: 10px;
        margin-bottom: 10px;
        float: left;
        padding: 2%;
        width: 96%;
    }

    .tool-btn {
        float: right;
    }

    .clr {
        clear: both;
        height: 0;
        overflow: hidden;
    }
</style>
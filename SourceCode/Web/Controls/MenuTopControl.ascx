<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MenuTopControl.ascx.cs" Inherits="mojoPortal.Web.Controls.MenuTopControl" %>
<div class="text-left header__area__container__row__text--left">
    <asp:Literal ID="literMenuLeft" runat="server"></asp:Literal>
</div>
<style>
    ul.menu-item{
        display: none;
        box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
        border-top: 1px solid #F88721 !important;
    }

    ul.topaddres > li > ul.menu-item {
        padding: 0;
        margin-top: 0;
        margin-bottom: 0;
        z-index: 900 !important;
        display: none;
        position: absolute;
        background-color: white;
    }
    ul.topaddres > li > ul {
        width: 280px;
        margin-left: 280px;
        box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 6px -1px, rgba(0, 0, 0, 0.06) 0px 2px 4px -1px;
    }

    ul.topaddres > li > ul:before {
        content: "";
        border-left: 12px solid #00872f;
        position: absolute;
        z-index: 9999999999;
        border-right: 0 solid #d05a5a00;
        border-bottom: 18px solid #d05a5a00;
        border-top: 19px solid #d05a5a00;
        left: 0;
        display: none;
    }

    ul.topaddres > li:hover > ul:before {
        display: block !important;
    }

    ul.topaddres > li:hover > .menu-item {
        display: block !important;
        transition: all 0.2s ease-in-out;
        pointer-events: auto;
    }

    ul.topaddres > li.menu-bf{
        position: relative;
        padding: 10px !important;
    }

    ul.menu-item > li{
        padding: 10px 0px 7px 20px !important;
    }

    ul.menu-item > li:not(:last-child){
        border-bottom: 1px solid #e9dcdc;
    }

    ul.topaddres .menu-bf:hover a:not(.menu-item a){
        color: #fb6a65 !important;
    }
</style>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MenuTopControl.ascx.cs" Inherits="mojoPortal.Web.Controls.MenuTopControl" %>

<div class="menu-trigger-wrapper">
  <button id="menu-trigger">Tất cả chuyên mục</button>
</div>
<div id="fullMegaMenu" runat="server">
  <asp:Literal ID="literMenuLeft" runat="server"></asp:Literal>
</div>

<style>
  /* --- Chuẩn giao diện mega menu VnExpress --- */
  .mega-wrapper {
    display: flex;
    flex-wrap: wrap;
    max-width: 1100px;
    margin: 0 auto;
    gap: 0 36px;
    box-sizing: border-box;
  }

  .mega-col {
    flex: 0 0 180px;
    min-width: 180px;
    padding: 0;
    margin-bottom: 18px;
  }

  .mega-col h3 {
    font-size: 16px;
    font-weight: bold;
    color: #b3043f;
    margin-bottom: 8px;
    border-bottom: 1px solid #ececec;
    padding-bottom: 6px;
    line-height: 1.25;
    letter-spacing: 0;
  }

  .mega-col ul,
  .submenu-vertical {
    list-style: none;
    padding: 0;
    margin: 0;
    max-height: 340px;
    overflow: visible;
  }

  .mega-col li,
  .submenu-vertical li {
    margin-bottom: 2px;
    line-height: 1.7;
  }

  .mega-col li a,
  .submenu-vertical a {
    font-size: 14px;
    color: #222;
    text-decoration: none;
    display: block;
    line-height: 1.7;
    padding: 0;
    transition: color 0.15s;
    background: none;
    border: none;
  }

  .mega-col li a:hover,
  .submenu-vertical a:hover {
    color: #b3043f;
    text-decoration: underline;
    background: none;
  }

  /* Nút bấm mở menu */
  #menu-trigger {
    background: #b3043f;
    color: white;
    padding: 12px 30px;
    cursor: pointer;
    border: none;
    font-size: 16px;
    font-weight: bold;
    border-radius: 4px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    margin: 16px 0;
  }

  #fullMegaMenu {
    display: none;
    position: absolute;
    top: 60px;
    left: 0;
    width: 100%;
    background: #fdfdfd;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
    z-index: 1000;
    border-top: 4px solid #b3043f;
    padding: 40px 60px;
  }

  #fullMegaMenu.open {
    display: block;
    animation: fadeIn 0.25s ease-in-out;
  }

  @keyframes fadeIn {
    from { opacity: 0; transform: translateY(-5px); }
    to { opacity: 1; transform: translateY(0); }
  }

  .see-more-menu {
    color: #888 !important;
    font-size: 13px !important;
    font-weight: normal;
    padding-left: 0;
    text-decoration: none;
    margin-top: 4px;
    display: block;
    transition: color 0.18s;
}

.see-more-menu:hover {
    color: #b3043f !important;
    text-decoration: underline;
}
</style>

<script>
    document.addEventListener("DOMContentLoaded", function () {
        var trigger = document.getElementById("menu-trigger");
        var menu = document.getElementById("fullMegaMenu");
        if (trigger && menu) {
            trigger.addEventListener("click", function () {
                menu.classList.toggle("open");
            });
        }
        // Ấn ngoài menu sẽ đóng lại
        document.addEventListener('click', function (e) {
            if (!menu.contains(e.target) && e.target !== trigger) {
                menu.classList.remove('open');
            }
        });
    });
</script>

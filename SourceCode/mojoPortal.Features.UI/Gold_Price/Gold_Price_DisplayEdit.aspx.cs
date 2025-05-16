using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static mojoPortal.Web.WindowsLiveLogin;

namespace Gold_PriceFeatures.UI
{
    public partial class Gold_Price_DisplayEdit : mojoBasePage
    {

        private readonly SiteUser user = SiteUtils.GetCurrentSiteUser();
        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra nếu có tham số ItemID (trường hợp sửa)
                if (Request.QueryString["ItemID"] != null && int.TryParse(Request.QueryString["ItemID"], out int itemID))
                {
                    LoadGoldPriceData(itemID);
                }
            }
        }

        private void LoadGoldPriceData(int itemID)
        {
            var goldPrice = new md_Gold_Price(itemID); // Load dữ liệu từ DB

            // Hiển thị dữ liệu lên form
            hfItemID.Value = goldPrice.ItemID.ToString();
            txtTenLoaiVang.Text = goldPrice.TenLoaiVang;
            txtGiaMuaHomNay.Text = goldPrice.GiaMuaHomNay.ToString("N0");
            txtGiaBanHomNay.Text = goldPrice.GiaBanHomNay.ToString("N0");
            txtGiaBanHomTruoc.Text = goldPrice.GiaBanHomNay.ToString("N0");
            txtGiaMuaHomTruoc.Text = goldPrice.GiaBanHomNay.ToString("N0");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var goldPrice = new md_Gold_Price(); 
            if (int.TryParse(hfItemID.Value, out int itemId) && itemId > 0)
            {
                goldPrice = new md_Gold_Price(goldPrice.ItemID);
            }
            else
            {
                goldPrice.ItemID = 0;
                goldPrice.CreatedBy = user.UserId;
                goldPrice.CreatedDate = DateTime.Now;
            }

            goldPrice.TenLoaiVang = txtTenLoaiVang.Text; 
            goldPrice.GiaMuaHomNay = double.Parse(txtGiaMuaHomNay.Text.Replace(",", ""), CultureInfo.InvariantCulture);
            goldPrice.GiaBanHomNay = double.Parse(txtGiaBanHomNay.Text.Replace(",", ""), CultureInfo.InvariantCulture);
            goldPrice.GiaMuaHomTruoc = double.Parse(txtGiaMuaHomTruoc.Text.Replace(",", ""), CultureInfo.InvariantCulture);
            goldPrice.GiaBanHomTruoc = double.Parse(txtGiaBanHomTruoc.Text.Replace(",", ""), CultureInfo.InvariantCulture);
            goldPrice.EditedDate = DateTime.Now;
            if (user != null)
            {
                goldPrice.EditedBy = user.UserId;
            }
            if (goldPrice.Save()) // Gọi phương thức Save() đã có sẵn
            {
                // Thông báo thành công và chuyển hướng
                lblMessage.Text = "Lưu dữ liệu thành công!";
                WebUtils.SetupRedirect(this, SiteRoot + "/Gold_Price/Gold_PriceDisplayManager.aspx");
            }
            else
            {
                lblMessage.Text = "Lỗi khi lưu dữ liệu!";
            }
        }
    }
}
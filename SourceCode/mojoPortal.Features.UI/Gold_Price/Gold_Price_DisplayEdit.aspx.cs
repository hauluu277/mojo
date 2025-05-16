using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using static mojoPortal.Web.WindowsLiveLogin;

namespace mojoPortal.Features.UI.Gold_Price
{
    public partial class Gold_Price_DisplayEdit : mojoBasePage
    {

        private readonly SiteUser user = SiteUtils.GetCurrentSiteUser();
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
            var goldPrice = new md_Gold_Price
            {
                ItemID = int.Parse(hfItemID.Value ?? "0"), // 0 = thêm mới
            };
            if (goldPrice.ItemID > 0)
            {
                goldPrice = new md_Gold_Price(goldPrice.ItemID);

                goldPrice.TenLoaiVang = txtTenLoaiVang.Text;
                goldPrice.GiaMuaHomNay = double.Parse(txtGiaMuaHomNay.Text.Replace(",", ""));
                goldPrice.GiaBanHomNay = double.Parse(txtGiaBanHomNay.Text.Replace(",", ""));
                goldPrice.GiaMuaHomTruoc = double.Parse(txtGiaMuaHomNay.Text.Replace(",", ""));
                goldPrice.GiaBanHomTruoc = double.Parse(txtGiaBanHomNay.Text.Replace(",", ""));
                goldPrice.EditedDate = DateTime.Now;
                if (user != null)
                {
                    goldPrice.EditedBy = user.UserId; 
                }
            }
            if (goldPrice.Save()) // Gọi phương thức Save() đã có sẵn
            {
                // Thông báo thành công và chuyển hướng (nếu cần)
                lblMessage.Text = "Lưu dữ liệu thành công!";
                WebUtils.SetupRedirect(this, Request.RawUrl); // Tải lại trang
            }
            else
            {
                lblMessage.Text = "Lỗi khi lưu dữ liệu!";
            }
        }
    }
}
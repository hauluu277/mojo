using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Features.UI.Gold_Price
{
    public partial class Gold_Price_DisplayEdit : mojoBasePage
    {
        #region Properties
        private int _pageId = -1;
        private int _moduleId = -1;
        private int _itemId = -1;
        private SiteSettings _siteSettings;
        private readonly SiteUser _currentUser;
        private md_Gold_Price _goldPrice;

        public int PageId
        {
            get { return _pageId; }
            set { _pageId = value; }
        }

        public int ModuleId
        {
            get { return _moduleId; }
            set { _moduleId = value; }
        }

        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public SiteSettings SiteSettings
        {
            get { return _siteSettings ?? (_siteSettings = CacheHelper.GetCurrentSiteSettings()); }
        }

        public SiteUser CurrentUser
        {
            get { return _currentUser ?? SiteUtils.GetCurrentSiteUser(); }
        }

        public md_Gold_Price GoldPrice
        {
            get
            {
                return _goldPrice ?? (_goldPrice = ItemId > 0 ? new md_Gold_Price(ItemId) : new md_Gold_Price());
            }
        }
        #endregion

        #region Page Lifecycle
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }

            /*if (!CurrentUser.IsInRoles(WebConfigSettings.RoleThatCanEditGoldPrices))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }*/

            SecurityHelper.DisableBrowserCache();
            LoadParameters();

            if (!IsPostBack)
            {
                LoadGoldPriceData();
                PopulateControls();
            }
        }
        #endregion

        #region Event Handlers
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveGoldPrice())
            {
                WebUtils.SetupRedirect(this, $"~/Gold_Price/Gold_Price_DisplayModule.aspx?pageid={PageId}&mid={ModuleId}");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, $"~/Gold_Price/Gold_Price_DisplayModule.aspx?pageid={PageId}&mid={ModuleId}");
        }
        #endregion

        #region Private Methods
        private void LoadParameters()
        {
            ItemId = WebUtils.ParseInt32FromQueryString("itemId", -1);
            PageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            ModuleId = WebUtils.ParseInt32FromQueryString("mid", -1);
        }

        private void LoadGoldPriceData()
        {
            if (ItemId > 0)
            {
                _goldPrice = new md_Gold_Price(ItemId);
                if (_goldPrice == null || _goldPrice.ItemID == -1)
                {
                    WebUtils.SetupRedirect(this, Request.RawUrl);
                    return;
                }
            }
        }

        private void PopulateControls()
        {
            if (GoldPrice != null)
            {
                hfItemID.Value = GoldPrice.ItemID.ToString();
                txtTenLoaiVang.Text = GoldPrice.TenLoaiVang;
                txtNganHang.Text = GoldPrice.NganHang;

                // Format prices with thousand separators
                txtGiaMuaHomNay.Text = GoldPrice.GiaMuaHomNay.ToString("N0");
                txtGiaBanHomNay.Text = GoldPrice.GiaBanHomNay.ToString("N0");
                txtGiaMuaHomTruoc.Text = GoldPrice.GiaMuaHomTruoc.ToString("N0");
                txtGiaBanHomTruoc.Text = GoldPrice.GiaBanHomTruoc.ToString("N0");

                txtThang1.Text = GoldPrice.Thang1.ToString("N0");
                txtThang3.Text = GoldPrice.Thang3.ToString("N0");
                txtThang6.Text = GoldPrice.Thang6.ToString("N0");
                txtThang9.Text = GoldPrice.Thang9.ToString("N0");
                txtThang12.Text = GoldPrice.Thang12.ToString("N0");
            }
        }

        private bool SaveGoldPrice()
        {
            try
            {
                // Set basic properties
                GoldPrice.TenLoaiVang = txtTenLoaiVang.Text.Trim();
                GoldPrice.NganHang = txtNganHang.Text.Trim();

                // Set prices with proper parsing
                GoldPrice.GiaMuaHomNay = ParsePrice(txtGiaMuaHomNay.Text);
                GoldPrice.GiaBanHomNay = ParsePrice(txtGiaBanHomNay.Text);
                GoldPrice.GiaMuaHomTruoc = ParsePrice(txtGiaMuaHomTruoc.Text);
                GoldPrice.GiaBanHomTruoc = ParsePrice(txtGiaBanHomTruoc.Text);

                GoldPrice.Thang1 = ParsePrice(txtThang1.Text);
                GoldPrice.Thang3 = ParsePrice(txtThang3.Text);
                GoldPrice.Thang6 = ParsePrice(txtThang6.Text);
                GoldPrice.Thang9 = ParsePrice(txtThang9.Text);
                GoldPrice.Thang12 = ParsePrice(txtThang12.Text);

                // Set audit fields
                if (GoldPrice.ItemID <= 0)
                {
                    GoldPrice.CreatedDate = DateTime.Now;
                    GoldPrice.CreatedBy = CurrentUser.UserId;
                }
                GoldPrice.EditedDate = DateTime.Now;
                GoldPrice.EditedBy = CurrentUser.UserId;

                // Save to database
                return GoldPrice.Save();
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        private double ParsePrice(string priceText)
        {
            if (string.IsNullOrWhiteSpace(priceText))
                return 0;

            // Remove thousand separators if present
            priceText = priceText.Replace(",", "").Replace(".", "");

            if (double.TryParse(priceText, out double result))
                return result;

            return 0;
        }
        #endregion
    }
}
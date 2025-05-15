using mojoPortal.Business;
using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Features.UI.Gold_Price
{

    public partial class Gold_Price_DisplayModuleControls : mojoBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGiaVang();
            }
        }

        private void LoadGiaVang()
        {
            try
            {
                var listGiaVang = md_Gold_Price.GetAll();
                rptGiaVang.DataSource = listGiaVang;
                rptGiaVang.DataBind();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
            }
        }
    }
}
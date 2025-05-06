using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.UI;
using System.Collections.Generic;
using Resources;
using mojoPortal.Business;
using Utilities;
using mojoPortal.Features;

namespace ArticleFeature.UI
{
    public partial class ArticleSelectorTabSetting : UserControl, ISettingControl
    {
        private string selectedValue = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (HttpContext.Current == null) { return; }
            Load += Page_Load;
            EnsureItems();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            PopulateLabels();
            BindDDLModules();
        }


        private void PopulateLabels()
        {
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (rblTab != null) return;
            rblTab = new RadioButtonList();
            if (Controls.Count != 0) return;
            Controls.Add(rblTab);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            selectedValue = rblTab.SelectedValue;
            if (selectedValue == string.Empty) return;
        }

        private void BindSelection()
        {
            if (!selectedValue.Equals(string.Empty))
            {
                BindDDLModules();
            }
        }

        private void BindDDLModules()
        {
            //Get Articles Module only
            DBUtilities repository = new DBUtilities();


            List<ListItem> listTab = new List<ListItem>();
            listTab.Add(new ListItem { Text = "Hiển thị kiểu tin mới, đọc nhiều", Value = ArticleConstant.TabTinMoiDocNhieu.ToString() });
            listTab.Add(new ListItem { Text = "Hiển thị kiểu tin thông báo", Value = ArticleConstant.TabTinThongBao.ToString() });
            listTab.Add(new ListItem { Text = "Hiển thị kiểu tin CNTT & CĐS", Value = ArticleConstant.TabCongNgheThongTinCDS.ToString() });
            listTab.Add(new ListItem { Text = "Hiển thị kiểu thông tin tuyển sinh", Value = ArticleConstant.TabThongTinTuyenSinh.ToString() });
            listTab.Add(new ListItem { Text = "Hiển thị kiểu Gallery & Video", Value = ArticleConstant.TabGalleryVideo.ToString() });


            listTab.Add(new ListItem { Text = "Hiển thị kiểu văn bản mới", Value = ArticleConstant.TabVanBanMoi.ToString() });
            listTab.Add(new ListItem { Text = "Hiển thị kiểu các phòng và đơn vị trực thuộc", Value = ArticleConstant.TabPhongTrucThuoc.ToString() });
            listTab.Add(new ListItem { Text = "Hiển thị kiểu danh sách trường", Value = ArticleConstant.TabDanhSachTruong.ToString() });
            listTab.Add(new ListItem { Text = "Hiển thị kiểu liên kết website", Value = ArticleConstant.TabLienKetWebsite.ToString() });

            listTab.Add(new ListItem { Text = "Hiển thị kiểu bảng vàng thành tích", Value = ArticleConstant.TabBangVangThanhTich.ToString() });

            listTab.Add(new ListItem { Text = "Hiển thị kiểu tin tức sự kiện", Value = ArticleConstant.TabTinTucSuKien.ToString() });

            listTab.Add(new ListItem { Text = "Hiển thị kiểu gương sáng", Value = ArticleConstant.TabGuongSang.ToString() });

            listTab.Add(new ListItem { Text = "Hiển thị thông báo mới, văn bản mới", Value = ArticleConstant.TabVanBanThongBao.ToString() });
            listTab.Add(new ListItem { Text = "Hiển thị tin tức và chuyên mục con", Value = ArticleConstant.TabTinVaChuyenMucCon.ToString() });

            listTab.Add(new ListItem { Text = "Hiển thị danh sách các chuyên mục", Value = ArticleConstant.TabDanhSachCacChuyenMuc.ToString() });


            rblTab.DataSource = listTab;
            rblTab.DataTextField = "Text";
            rblTab.DataValueField = "Value";
            rblTab.DataBind();

            if (rblTab.Items.Count > 1)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem item = rblTab.Items.FindByValue(selectedValue);
                    if (item != null)
                    {
                        rblTab.Items.FindByValue(selectedValue).Selected = true;
                    }
                }
                else { rblTab.SelectedIndex = 0; }
            }
            //FormatModuleTitle();
        }

        private void FormatModuleTitle()
        {
            foreach (ListItem item in rblTab.Items)
            {
                if (item.Value == string.Empty) continue;
                if (item.Text.Contains("</span>"))
                {
                    item.Text = FeatureUtilities.RemoveTwoColorModuleTitleText(item.Text);
                }
                Module m = new Module(Convert.ToInt32(item.Value));
                item.Text += @" (Site " + m.SiteId + @")";
            }
        }

        #region ISettingControl

        public string GetValue()
        {
            EnsureItems();
            GetSelectedItems();
            return selectedValue;
        }

        public void SetValue(string val)
        {
            EnsureItems();
            selectedValue = val;
            BindSelection();
        }

        #endregion

    }
}
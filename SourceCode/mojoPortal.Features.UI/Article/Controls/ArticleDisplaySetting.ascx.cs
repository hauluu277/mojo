using mojoPortal.Features;
using mojoPortal.Web.UI;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class ArticleDisplaySetting : UserControl, ISettingControl
    {
        private string selectedValue = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (HttpContext.Current == null) { return; }
            Load += Page_Load;
            EnsureItems();
        }
        private void EnsureItems()
        {
            //why is this null here, its declared
            if (rdblSetting != null) return;
            rdblSetting = new RadioButtonList();
            if (Controls.Count != 0) return;
            Controls.Add(rdblSetting);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            BindDDLModules();
        }
        private void BindSelection()
        {
            if (!selectedValue.Equals(string.Empty))
            {
                BindDDLModules();
            }
        }
        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            selectedValue = rdblSetting.SelectedValue;
            if (selectedValue == string.Empty) return;
            //DataTable dt = Module.GetPageModulesTable(int.Parse(selectedValue));
            //if (dt.Rows.Count > 0)
            //{
            //    selectedValue += dt.Rows[0];
            //}
            //else
            //{
            //    selectedValue += "";
            //}
        }
        private void BindDDLModules()
        {
            //Get Articles Module only
            List<ListItem> listItem = new List<ListItem>();
            listItem.Add(new ListItem { Text = "Hiển thị kiểu khác", Value = DisplaySettingConstant.DisplayType_Khac.ToString() });
            listItem.Add(new ListItem { Text = "Không hiển thị ảnh", Value = DisplaySettingConstant.DisplayNoIMG.ToString() });
            listItem.Add(new ListItem { Text = "Hiển thị kiểu Đào tạo", Value = DisplaySettingConstant.DisplayType_DaoTao.ToString() });
            listItem.Add(new ListItem { Text = "Hiển thị kiểu Hợp tác quốc tế", Value = DisplaySettingConstant.DisplayType_HopTacQuocTe.ToString() });
            listItem.Add(new ListItem { Text = "Hiển thị kiểu Khoa học công nghệ", Value = DisplaySettingConstant.DisplayType_KhoaHocCongNghe.ToString() });
            listItem.Add(new ListItem { Text = "Hiển thị kiểu thông báo", Value = DisplaySettingConstant.DisplayType_ThongBao.ToString() });
            listItem.Add(new ListItem { Text = "Hiển thị kiểu Lịch công tác", Value = DisplaySettingConstant.DisplayType_LichCongTac.ToString() });

            rdblSetting.DataSource = listItem;
            rdblSetting.DataTextField = "Text";
            rdblSetting.DataValueField = "Value";
            rdblSetting.DataBind();
            if (!string.IsNullOrEmpty(selectedValue))
            {

                rdblSetting.SelectedValue = selectedValue;
            }
            else
            {
                rdblSetting.SelectedValue = DisplaySettingConstant.DisplayType_Khac.ToString();
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
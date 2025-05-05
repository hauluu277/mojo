using mojoPortal.Business;
using mojoPortal.Web;
using mojoPortal.Web.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;

namespace BannerFeature.UI
{
    public partial class BannerChooseSetting : UserControl, ISettingControl
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
            listItem.Add(new ListItem { Text = "Không slider", Value = BannerConstant.NoSlide.ToString() });
            listItem.Add(new ListItem { Text = "Slide Fullwidth(Animated Touch)", Value = BannerConstant.FullWidth_AnimatedTouch.ToString() });
            listItem.Add(new ListItem { Text = "Slide owl(Khoa phòng)", Value = BannerConstant.OWL_KhoaPhong.ToString() });
            listItem.Add(new ListItem { Text = "Slide owl(Đối tác)", Value = BannerConstant.OWL_DoiTac.ToString() });
            listItem.Add(new ListItem { Text = "Slide Fullwidth (Jssor)", Value = BannerConstant.FullWidth_Jssor.ToString() });
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
                rdblSetting.SelectedValue = "0";
            }
            //rdblSetting.DataSource = repository.GetModuleByModuleGuid(new Guid("ef7f867a-5449-4be1-9dc6-4ad62f77ce29"));
            //rdblSetting.DataTextField = "ModuleTitle";
            //ddlModule.DataValueField = "ModuleID";
            //ddlModule.DataBind();
            //ddlModule.Items.Insert(0, new ListItem(ArticleResources.EmptyModule, string.Empty));
            //if (ddlModule.Items.Count > 1)
            //{
            //    if (!selectedValue.Equals(string.Empty))
            //    {
            //        ListItem item = ddlModule.Items.FindByValue(selectedValue.Remove(selectedValue.IndexOf("-")));
            //        if (item != null)
            //        {
            //            ddlModule.Items.FindByValue(selectedValue.Remove(selectedValue.IndexOf("-"))).Selected = true;
            //        }
            //    }
            //    else { ddlModule.SelectedIndex = 0; }
            //}
            //FormatModuleTitle();
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
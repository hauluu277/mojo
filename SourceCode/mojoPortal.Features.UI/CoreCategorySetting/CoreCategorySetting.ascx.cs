using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.CoreCategoryUI
{
    public partial class CoreCategorySetting : UserControl, ISettingControl
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
            BindCoreCategorys();
            ListItem categoryOption = new ListItem();
            categoryOption.Text = "Chọn danh mục";
            categoryOption.Value = "0";
            ddlCoreCategory.Items.Insert(0, categoryOption);
        }


         


        private void PopulateLabels()
        {
            //ltrCoreCategoryChoose.Text = Resources.CoreCategoryResources.CoreCategoryChooseLabel;
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (ddlCoreCategory != null) return;
            ddlCoreCategory = new DropDownList();
            if (Controls.Count != 0) return;
            Controls.Add(ddlCoreCategory);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            selectedValue = ddlCoreCategory.SelectedValue;
            if (selectedValue == string.Empty) return;
        }

        private void BindSelection()
        {
            if (!selectedValue.Equals(string.Empty))
            {
                BindCoreCategorys();
            }
        }

        private void BindCoreCategorys()
        {
            SiteSettings settings = CacheHelper.GetCurrentSiteSettings();
            List<CoreCategory> roots = CoreCategory.GetRoot(settings.SiteId);
            ddlCoreCategory.DataSource = roots;
            ddlCoreCategory.DataTextField = "Name";
            ddlCoreCategory.DataValueField = "ItemID";
            ddlCoreCategory.DataBind();

            if (ddlCoreCategory.Items.Count > 0)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem item = ddlCoreCategory.Items.FindByValue(selectedValue);
                    if (item != null)
                    {
                        ddlCoreCategory.Items.FindByValue(selectedValue).Selected = true;
                    }
                }
                else { ddlCoreCategory.SelectedIndex = 0; }
            }
            for (int i = 0; i < ddlCoreCategory.Items.Count; i++)
            {
                ddlCoreCategory.Items[i].Text = ddlCoreCategory.Items[i].Text.Replace("<span class='second left'>", string.Empty).Replace(
                    "<span class='second right'>", string.Empty).Replace("<span class='first left'>", string.Empty).
                    Replace("<span class='first right'>", string.Empty).Replace("</span>", string.Empty);
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
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkFeature.UI
{
    public partial class LinkCategorySetting : UserControl, ISettingControl
    {
        private string selectedValue = string.Empty;
        private SiteSettings settings = CacheHelper.GetCurrentSiteSettings();
        private PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
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
            BindArticleCategorys();
        }

        private void PopulateLabels()
        {
            //ltrCoreCategoryChoose.Text = Resources.CoreCategoryResources.CoreCategoryChooseLabel;
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (lbLinkCategory != null) return;
            lbLinkCategory = new ListBox();
            if (Controls.Count != 0) return;
            Controls.Add(lbLinkCategory);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            if (lbLinkCategory.Items.Count > 0)
            {
                foreach (ListItem listitem in lbLinkCategory.Items)
                {
                    if (listitem.Selected)
                        selectedValue += listitem.Value + "-";
                }
            }
            if (selectedValue == string.Empty) return;
        }

        private void BindSelection()
        {
            if (!selectedValue.Equals(string.Empty))
            {
                BindArticleCategorys();
            }
        }

        private void BindArticleCategorys()
        {
            List<ListItem> listCate = new List<ListItem>();
            List<CategoryLink> roots = CategoryLink.GetAll(siteSetting.SiteId);
            foreach (CategoryLink item in roots)
            {
                ListItem list = new ListItem
                {
                    Text = item.Name,
                    Value = item.ItemID.ToString()
                };
                listCate.Add(list);
            }
            lbLinkCategory.DataSource = listCate;
            lbLinkCategory.DataTextField = "Text";
            lbLinkCategory.DataValueField = "Value";
            lbLinkCategory.DataBind();

            if (string.IsNullOrEmpty(selectedValue))
            {
                selectedValue = pageSettings.CategoryConfig;
            }
            if (lbLinkCategory.Items.Count > 0)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem listItem;
                    var list_category_config = selectedValue.Split('-');
                    if (list_category_config != null)
                    {
                        lbLinkCategory.ClearSelection();
                        foreach (var cat in list_category_config)
                        {
                            if (!string.IsNullOrEmpty(cat))
                            {
                                listItem = lbLinkCategory.Items.FindByValue(cat);
                                if (listItem != null)
                                {
                                    listItem.Selected = true;
                                }
                            }
                        }
                    }
                }
                else { lbLinkCategory.SelectedIndex = 0; }
            }
            for (int i = 0; i < lbLinkCategory.Items.Count; i++)
            {
                lbLinkCategory.Items[i].Text = lbLinkCategory.Items[i].Text.Replace("<span class='second left'>", string.Empty).Replace(
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
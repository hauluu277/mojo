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

namespace ModuleBuilder.SettingControls
{
    public partial class CategoryArticleSelectorSetting : UserControl, ISettingControl
    {
        private string selectedValue = string.Empty;
        private SiteSettings settings = CacheHelper.GetCurrentSiteSettings();
        private PageSettings pageSettings = CacheHelper.GetCurrentPage();
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
            BindCategory();
        }

        private void PopulateLabels()
        {
            //ltrCoreCategoryChoose.Text = Resources.CoreCategoryResources.CoreCategoryChooseLabel;
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (lboxCategoryBuilder != null) return;
            lboxCategoryBuilder = new ListBox();
            if (Controls.Count != 0) return;
            Controls.Add(lboxCategoryBuilder);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            if (lboxCategoryBuilder.Items.Count > 0)
            {
                foreach (ListItem listitem in lboxCategoryBuilder.Items)
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
                BindCategory();
            }
        }

        private void BindCategory()
        {
            List<ListItem> listCategory = new List<ListItem>();
            List<CoreSkin> roots = CoreSkin.GetAll();
            foreach (var item in roots)
            {
                listCategory.Add(new ListItem { Value = "-99", Text = item.Title });
                var categoryParent = CoreSkinCategory.GetAll().Where(x => x.ParentID <= 0 && x.SkinID == item.ItemID).ToList();
                foreach (var parent in categoryParent)
                {
                    listCategory.Add(new ListItem { Text = string.Format("|--{0}", parent.CategoryName), Value = parent.ItemID.ToString() });
                    PopulateCategoryChild(parent.ItemID, listCategory);
                }
            }
            lboxCategoryBuilder.DataValueField = "Value";
            lboxCategoryBuilder.DataTextField = "Text";
            lboxCategoryBuilder.DataSource = listCategory;
            lboxCategoryBuilder.DataBind();
            //foreach (ListItem item in lboxCategoryBuilder.Items)
            //{
            //    if (item.Value.Equals("Builder"))
            //    {
            //        item.Enabled = false;
            //        item.Attributes.Add("disabled", "disabled");
            //    }
            //}


            if (string.IsNullOrEmpty(selectedValue))
            {
                selectedValue = pageSettings.CategoryConfig;
            }
            if (lboxCategoryBuilder.Items.Count > 0)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem listItem;
                    var list_category_config = selectedValue.Split('-');
                    if (list_category_config != null)
                    {
                        lboxCategoryBuilder.ClearSelection();
                        foreach (var cat in list_category_config)
                        {
                            if (!string.IsNullOrEmpty(cat))
                            {
                                listItem = lboxCategoryBuilder.Items.FindByValue(cat);
                                if (listItem != null)
                                {
                                    listItem.Selected = true;
                                }
                            }
                        }
                    }
                }
                else { lboxCategoryBuilder.SelectedIndex = 0; }
            }
            for (int i = 0; i < lboxCategoryBuilder.Items.Count; i++)
            {
                lboxCategoryBuilder.Items[i].Text = lboxCategoryBuilder.Items[i].Text.Replace("<span class='second left'>", string.Empty).Replace(
                    "<span class='second right'>", string.Empty).Replace("<span class='first left'>", string.Empty).
                    Replace("<span class='first right'>", string.Empty).Replace("</span>", string.Empty);
            }
        }
        private void PopulateCategoryChild(int parentID, List<ListItem> listCategory)
        {
            var listChildren = CoreSkinCategory.GetAll().Where(x => x.ParentID == parentID && x.IsCategoryTemplate == false).ToList();
            if (listChildren.Any())
            {
                foreach (var item in listChildren)
                {
                    var categoryBefor = listCategory[listCategory.Count - 1];
                    var child = listChildren.Where(x => x.ItemID == int.Parse(categoryBefor.Value)).FirstOrDefault();
                    if (categoryBefor.Value == item.ParentID.ToString())
                    {
                        string prefix = string.Empty;
                        while (categoryBefor.Text.StartsWith("|"))
                        {
                            prefix += categoryBefor.Text.Substring(0, 3);
                            categoryBefor.Text = categoryBefor.Text.Remove(0, 3);
                        }
                        categoryBefor.Text = prefix + categoryBefor.Text;
                        prefix += "|--";
                        listCategory.Add(new ListItem { Text = string.Format("{0}{1}", prefix, item.CategoryName), Value = item.ItemID.ToString() });
                    }
                    else if (child != null && child.ParentID == item.ParentID)
                    {
                        string prefix = string.Empty;
                        while (categoryBefor.Text.StartsWith("|"))
                        {
                            prefix += categoryBefor.Text.Substring(0, 3);
                            categoryBefor.Text = categoryBefor.Text.Remove(0, 3);
                        }
                        categoryBefor.Text = prefix + categoryBefor.Text;
                        listCategory.Add(new ListItem { Text = string.Format("{0}{1}", prefix, item.CategoryName), Value = item.ItemID.ToString() });
                    }
                    else
                    {
                        listCategory.Add(new ListItem { Text = string.Format("|--|--{0}", item.CategoryName), Value = item.ItemID.ToString() });
                    }
                    PopulateCategoryChild(item.ItemID, listCategory);
                }
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
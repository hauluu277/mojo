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

namespace ArticleFeature.UI
{
    public partial class ArticleCategorySiteSetting : UserControl, ISettingControl
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
            BindArticleCategorys();
        }

        private void PopulateLabels()
        {
            //ltrCoreCategoryChoose.Text = Resources.CoreCategoryResources.CoreCategoryChooseLabel;
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (lbArticleCategory != null) return;
            lbArticleCategory = new ListBox();
            if (Controls.Count != 0) return;
            Controls.Add(lbArticleCategory);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            if (lbArticleCategory.Items.Count > 0)
            {
                foreach (ListItem listitem in lbArticleCategory.Items)
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
            var siteId = 1;
            var site = new SiteSettings(siteId);

            List<CoreCategory> roots = CoreCategory.GetRootByCat(siteId, site.ArticleCategory).ToList();
            foreach (CoreCategory item in roots)
            {
                ListItem list = new ListItem
                {
                    Text = item.Name,
                    Value = item.ItemID.ToString()
                };
                listCate.Add(list);
            }
            for (int i = 0; i < listCate.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(listCate[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (listCate[i].Text.StartsWith("|"))
                {
                    prefix += listCate[i].Text.Substring(0, 3);
                    listCate[i].Text = listCate[i].Text.Remove(0, 3);
                }
                listCate[i].Text = prefix + listCate[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString()
                    };
                    listCate.Insert(listCate.IndexOf(listCate[i]) + index, list);
                    index++;
                }
            }
            listCate.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
            lbArticleCategory.DataSource = listCate;
            lbArticleCategory.DataTextField = "Text";
            lbArticleCategory.DataValueField = "Value";
            lbArticleCategory.DataBind();

            if (string.IsNullOrEmpty(selectedValue))
            {
                selectedValue = pageSettings.CategoryConfig;
            }
            if (lbArticleCategory.Items.Count > 0)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem listItem;
                    var list_category_config = selectedValue.Split('-');
                    if (list_category_config != null)
                    {
                        lbArticleCategory.ClearSelection();
                        foreach (var cat in list_category_config)
                        {
                            if (!string.IsNullOrEmpty(cat))
                            {
                                listItem = lbArticleCategory.Items.FindByValue(cat);
                                if (listItem != null)
                                {
                                    listItem.Selected = true;
                                }
                            }
                        }
                    }
                }
                else { lbArticleCategory.SelectedIndex = 0; }
            }
            for (int i = 0; i < lbArticleCategory.Items.Count; i++)
            {
                lbArticleCategory.Items[i].Text = lbArticleCategory.Items[i].Text.Replace("<span class='second left'>", string.Empty).Replace(
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
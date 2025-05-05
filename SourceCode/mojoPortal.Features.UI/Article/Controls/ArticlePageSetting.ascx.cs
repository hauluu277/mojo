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
    public partial class ArticlePageSetting : UserControl, ISettingControl
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
            BindPageList();
        }

        private void PopulateLabels()
        {
            //ltrCoreCategoryChoose.Text = Resources.CoreCategoryResources.CoreCategoryChooseLabel;
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (lbArticlePage != null) return;
            lbArticlePage = new ListBox();
            if (Controls.Count != 0) return;
            Controls.Add(lbArticlePage);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            if (lbArticlePage.Items.Count > 0)
            {
                foreach (ListItem listitem in lbArticlePage.Items)
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
                BindPageList();
            }
        }

        private void BindPageList()
        {
            List<ListItem> listCate = new List<ListItem>();
            List<PageBO> roots = PageBO.GetListParent(settings.SiteId).ToList();
            foreach (PageBO item in roots)
            {
                ListItem list = new ListItem
                {
                    Text = item.PageName,
                    Value = item.PageID.ToString()
                };
                listCate.Add(list);
            }
            for (int i = 0; i < listCate.Count; i++)
            {
                List<PageBO> children = PageBO.GetListChild(int.Parse(listCate[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (listCate[i].Text.StartsWith("|"))
                {
                    prefix += listCate[i].Text.Substring(0, 3);
                    listCate[i].Text = listCate[i].Text.Remove(0, 3);
                }
                listCate[i].Text = prefix + listCate[i].Text;
                int index = 1;
                foreach (PageBO child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.PageName,
                        Value = child.PageID.ToString()
                    };
                    listCate.Insert(listCate.IndexOf(listCate[i]) + index, list);
                    index++;
                }
            }
            listCate.Insert(0, new ListItem("--Chọn page--", "0"));
            lbArticlePage.DataSource = listCate;
            lbArticlePage.DataTextField = "Text";
            lbArticlePage.DataValueField = "Value";
            lbArticlePage.DataBind();

            if (string.IsNullOrEmpty(selectedValue))
            {
                selectedValue = pageSettings.CategoryConfig;
            }
            if (lbArticlePage.Items.Count > 0)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem listItem;
                    var list_page_config = selectedValue.Split('-');
                    if (list_page_config != null)
                    {
                        lbArticlePage.ClearSelection();
                        foreach (var cat in list_page_config)
                        {
                            if (!string.IsNullOrEmpty(cat))
                            {
                                listItem = lbArticlePage.Items.FindByValue(cat);
                                if (listItem != null)
                                {
                                    listItem.Selected = true;
                                }
                            }
                        }
                    }
                }
                else { lbArticlePage.SelectedIndex = 0; }
            }
            for (int i = 0; i < lbArticlePage.Items.Count; i++)
            {
                lbArticlePage.Items[i].Text = lbArticlePage.Items[i].Text.Replace("<span class='second left'>", string.Empty).Replace(
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
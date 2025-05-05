using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class SiteCategorySetting : UserControl, ISettingControl
    {
        private string selectedValue = string.Empty;
        private SiteSettings settings = CacheHelper.GetCurrentSiteSettings();
        private PageSettings pageSettings = CacheHelper.GetCurrentPage();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (HttpContext.Current == null) { return; }
            Load += Page_Load;
            rptSite.ItemDataBound += RptSite_ItemDataBound;
            EnsureItems();
        }

        protected void RptSite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var a = selectedValue;

                var siteID = ((HiddenField)e.Item.FindControl("hdfSiteID")).Value;
                var categoryArticle = ((HiddenField)e.Item.FindControl("hdfCategoryArticle")).Value;
                if (!string.IsNullOrEmpty(categoryArticle) && Convert.ToInt32(categoryArticle) > 0)
                {
                    var lboxCategory = ((ListBox)e.Item.FindControl("lboxCategory"));
                    if (siteID != null && categoryArticle != null)
                    {
                        var _siteID = Convert.ToInt32(siteID);
                        var _categoryArticle = Convert.ToInt32(categoryArticle);
                        lboxCategory.DataValueField = "Value";
                        lboxCategory.DataTextField = "Text";
                        lboxCategory.DataSource = BindingCategory(_siteID, _categoryArticle);
                        lboxCategory.DataBind();
                        lboxCategory.Items.Insert(0, new ListItem { Text = "Không chọn", Value = "-1" });
                        if (!string.IsNullOrEmpty(selectedValue))
                        {
                            var listSelected = selectedValue.Split('-');
                            if (listSelected != null)
                            {
                                foreach (ListItem item in lboxCategory.Items)
                                {
                                    if (listSelected.Contains(item.Value))
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        private List<ListItem> BindingCategory(int siteId, int categoryArticle)
        {
            List<ListItem> listCate = new List<ListItem>();
            List<CoreCategory> roots = CoreCategory.GetRootByCat(siteId, categoryArticle).ToList();
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
            return listCate;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            PopulateLabels();
            BindSiteCategory();
        }

        private void PopulateLabels()
        {
            //ltrCoreCategoryChoose.Text = Resources.CoreCategoryResources.CoreCategoryChooseLabel;
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (rptSite != null) return;
            rptSite = new Repeater();
            if (Controls.Count != 0) return;
            Controls.Add(rptSite);
        }

        private void GetSelectedItems()
        {

            selectedValue = string.Empty;
            if (rptSite.Items.Count > 0)
            {
                for (int i = 0; i < rptSite.Items.Count; i++)
                {
                    var categorySelected = ((ListBox)rptSite.Items[i].FindControl("lboxCategory")).SelectedValue;
                    if (!string.IsNullOrEmpty(categorySelected))
                    {
                        selectedValue += categorySelected + "-";
                    }
                }
            }
            if (selectedValue == string.Empty) return;
        }

        private void BindSelection()
        {
            if (!selectedValue.Equals(string.Empty))
            {
                BindSiteCategory();
            }
        }

        private void BindSiteCategory()
        {
            rptSite.DataSource = SiteSettings.GetListSiteShort();
            rptSite.DataBind();
        }

        protected IDataReader LoadCategoryList(object siteId, object categoryArticle)
        {
            if (siteId != null && categoryArticle != null)
            {
                var _categoryId = Convert.ToInt32(categoryArticle);
                var _siteId = Convert.ToInt32(siteId);
                var listChild = CoreCategory.GetListChildren(_categoryId);
                return CoreCategory.GetCategoryArticleBySite(_siteId, listChild);
            }
            return null;
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
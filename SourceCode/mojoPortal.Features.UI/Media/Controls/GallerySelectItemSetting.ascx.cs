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
using MediaGroupFeature.Business;
using mojoPortal.Business.WebHelpers;

namespace GalleryFeature.UI
{
    public partial class GallerySelectItemSetting : UserControl, ISettingControl
    {
        private string selectedValue = string.Empty;
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
            BindDDLModules();
        }


        private void PopulateLabels()
        {
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (lboxGallery != null) return;
            lboxGallery = new ListBox();
            if (Controls.Count != 0) return;
            Controls.Add(lboxGallery);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            if (lboxGallery.Items.Count > 0)
            {
                foreach (ListItem listitem in lboxGallery.Items)
                {
                    if (listitem.Selected)
                        selectedValue += listitem.Value;
                }
            }
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

            var source = MediaGroup.GetAllBySitePublish(siteSetting.SiteId);
            lboxGallery.DataSource = source;
            lboxGallery.DataTextField = "NameGroup";
            lboxGallery.DataValueField = "ItemID";
            lboxGallery.DataBind();
          
            if (lboxGallery.Items.Count > 0)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem listItem;
                    var list_category_config = selectedValue.Split('-');
                    if (list_category_config != null)
                    {
                        lboxGallery.ClearSelection();
                        foreach (var cat in list_category_config)
                        {
                            if (!string.IsNullOrEmpty(cat))
                            {
                                listItem = lboxGallery.Items.FindByValue(cat);
                                if (listItem != null)
                                {
                                    listItem.Selected = true;
                                }
                            }
                        }
                    }
                }
                else { lboxGallery.SelectedIndex = 0; }
            }


            if (lboxGallery.Items.Count > 1)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem item = lboxGallery.Items.FindByValue(selectedValue);
                    if (item != null)
                    {
                        lboxGallery.Items.FindByValue(selectedValue).Selected = true;
                    }
                }
                else { lboxGallery.SelectedIndex = 0; }
            }
            //FormatModuleTitle();
        }

        private void FormatModuleTitle()
        {
            foreach (ListItem item in lboxGallery.Items)
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
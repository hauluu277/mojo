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
using System.Linq;
using mojoPortal.Business.WebHelpers;

namespace Global.UI
{
    public partial class CatgorySelectorSetting : UserControl, ISettingControl
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
            if (ddlCategory != null) return;
            ddlCategory = new DropDownList();
            if (Controls.Count != 0) return;
            Controls.Add(ddlCategory);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            selectedValue = ddlCategory.SelectedValue;
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
            var siteSettings = CacheHelper.GetCurrentSiteSettings();
            //Get Articles Module only
            DBUtilities repository = new DBUtilities();
            var source = CoreCategory.GetRoot(siteSettings.SiteId);


            ddlCategory.DataSource = source;
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "ItemID";
            ddlCategory.DataBind();

            if (ddlCategory.Items.Count > 1)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem item = ddlCategory.Items.FindByValue(selectedValue);
                    if (item != null)
                    {
                        ddlCategory.Items.FindByValue(selectedValue).Selected = true;
                    }
                }
                else { ddlCategory.SelectedIndex = 0; }
            }
            //FormatModuleTitle();
        }

        private void FormatModuleTitle()
        {
            foreach (ListItem item in ddlCategory.Items)
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
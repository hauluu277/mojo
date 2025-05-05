using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using Utilities;

namespace mojoPortal.Web.UI
{
    public partial class ArticleModuleSelectorSetting : UserControl, ISettingControl
    {
        /// <summary>
        /// TinLT: "before ; is ModuleID, after - is PageID".
        /// </summary>
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
            BindSelection();
        }


        private void PopulateLabels()
        {

        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (ddlListModule != null) return;
            ddlListModule = new CheckBoxList();
            if (Controls.Count == 0)
            {
                Controls.Add(ddlListModule);
            }
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            foreach (ListItem item in ddlListModule.Items)
            {
                if (!item.Selected) continue;
                selectedValue += item.Value;
                DataTable dt = Module.GetPageModulesTable(int.Parse(item.Value));
                if (dt.Rows.Count > 0)
                {
                    selectedValue += "-" + dt.Rows[0]["PageID"] + ";";
                }
                else
                {
                    selectedValue += "-0;";
                }
            }
        }

        private void BindSelection()
        {
            SiteSettings settings = CacheHelper.GetCurrentSiteSettings();
            DBUtilities repository = new DBUtilities();
            ddlListModule.DataSource = repository.GetModuleByModuleGuid(new Guid("9392aca9-495a-447e-9e0c-de1f23d760f3"), settings.SiteId);
            ddlListModule.DataTextField = "ModuleTitle";
            ddlListModule.DataValueField = "ModuleID";
            ddlListModule.DataBind();
            RemoveHtml();
            if (selectedValue.Equals(string.Empty)) return;
            //Get Articles Module only
            if (ddlListModule.Items.Count <= 0) return;
            if (!selectedValue.Equals(string.Empty))
            {
                char[] param = { ';' };
                string[] listModuleId = selectedValue.Remove(selectedValue.Length - 1).Split(param);
                foreach (string value in listModuleId) // Get moduleId, pageId in format [moduleId-pageId]
                {
                    string moduleId = value.Remove(value.IndexOf('-')); //Get moduleId
                    ListItem item = ddlListModule.Items.FindByValue(moduleId);
                    if (item != null)
                    {
                        ddlListModule.Items.FindByValue(moduleId).Selected = true;
                    }
                }
            }
            else { ddlListModule.SelectedIndex = 0; }
        }

        private void RemoveHtml()
        {
            foreach (ListItem item in ddlListModule.Items)
            {
                if (item.Text != @"No Module")
                {
                    item.Text = FeatureUtilities.RemoveTwoColorModuleTitleText(item.Text);
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
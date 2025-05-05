using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.UI;
using Resources;
using mojoPortal.Business;
using Utilities;

namespace ArticleFeature.UI
{
    public partial class ModuleSelectorSetting : UserControl, ISettingControl
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
            ltrModuleChoose.Text = ArticleResources.ModuleChooseLabel;
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (ddlModule != null) return;
            ddlModule = new DropDownList();
            if (Controls.Count != 0) return;
            Controls.Add(ddlModule);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            selectedValue = ddlModule.SelectedValue;
            if (selectedValue == string.Empty) return;
            DataTable dt = Module.GetPageModulesTable(int.Parse(selectedValue));
            if (dt.Rows.Count > 0)
            {
                selectedValue += "-" + dt.Rows[0]["PageID"];
            }
            else
            {
                selectedValue += "--1";
            }
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
            ddlModule.DataSource = repository.GetModuleByModuleGuid(new Guid("8bdb1450-91e5-4cb0-af1a-2427d7e7e611"));
            ddlModule.DataTextField = "ModuleTitle";
            ddlModule.DataValueField = "ModuleID";
            ddlModule.DataBind();
            ddlModule.Items.Insert(0, new ListItem(ArticleResources.EmptyModule, string.Empty));
            if (ddlModule.Items.Count > 1)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem item = ddlModule.Items.FindByValue(selectedValue.Remove(selectedValue.IndexOf("-")));
                    if (item != null)
                    {
                        ddlModule.Items.FindByValue(selectedValue.Remove(selectedValue.IndexOf("-"))).Selected = true;
                    }
                }
                else { ddlModule.SelectedIndex = 0; }
            }
            FormatModuleTitle();
        }

        private void FormatModuleTitle()
        {
            foreach (ListItem item in ddlModule.Items)
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
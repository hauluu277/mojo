using mojoPortal.Business;
using mojoPortal.Web;
using mojoPortal.Web.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkFeature.UI
{
    public partial class LinkSelectorSetting : UserControl, ISettingControl
    {
        private string selectedValue = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (HttpContext.Current == null) { return; }
            Load += Page_Load;
            EnsureItems();
        }
        private void EnsureItems()
        {
            //why is this null here, its declared
            if (rdblSetting != null) return;
            rdblSetting = new RadioButtonList();
            if (Controls.Count != 0) return;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            BindDDLModules();
        }
        private void BindSelection()
        {
            if (!selectedValue.Equals(string.Empty))
            {
                BindDDLModules();
            }
        }
        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            selectedValue = rdblSetting.SelectedValue;
            if (selectedValue == string.Empty) return;
            try
            {
                //DataTable dt = Module.GetPageModulesTable(int.Parse(selectedValue));
                //if (dt.Rows.Count > 0)
                //{
                //    selectedValue += dt.Rows[0];
                //}
                //else
                //{
                    selectedValue += "";
                //}
            }
            catch
            {

                return;
            }

        }
        private void BindDDLModules()
        {
            //Get Articles Module only
            var status = SiteUtils.StringToDictionary(AdminLinkResources.LinkModuleSetting.ToString(), ",");

            rdblSetting.DataSource = status;
            rdblSetting.DataTextField = "Value";
            rdblSetting.DataValueField = "Key";
            rdblSetting.DataBind();
            if (!string.IsNullOrEmpty(selectedValue))
            {

                rdblSetting.SelectedValue = selectedValue;
            }
            else
            {
                rdblSetting.SelectedValue = "1";
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
            try
            {
                if (!string.IsNullOrEmpty(val))
                {
                    int.Parse(val);
                    selectedValue = val;
                }
            }
            catch (Exception)
            {

                selectedValue = string.Empty;
            }
            EnsureItems();
            //selectedValue = val;
            BindSelection();
        }

        #endregion
    }
}
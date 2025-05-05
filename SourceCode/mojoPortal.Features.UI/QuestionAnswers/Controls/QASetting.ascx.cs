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
using QuestionAnswerFeatures.UI;
using mojoPortal.Features.UI.Article.Components;

namespace QAFeature.UI
{
    public partial class QASetting : UserControl, ISettingControl
    {
        private string selectedValue = string.Empty;
        protected QuestionAnswerConfiguration config = new QuestionAnswerConfiguration();

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
            if (rblTab != null) return;
            rblTab = new DropDownList();
            if (Controls.Count != 0) return;
            Controls.Add(rblTab);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            selectedValue = rblTab.SelectedValue;
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
            List<ListItem> listTab = new List<ListItem>();
            listTab.Add(new ListItem { Text = "Hiển thị dưới dạng danh sách", Value = QuestionAnswerConstant.HienThiDangList });
            listTab.Add(new ListItem { Text = "Hiển thị dưới dạng rút gọn", Value = QuestionAnswerConstant.HienThiDangRutGon });

            rblTab.DataSource = listTab;
            rblTab.DataTextField = "Text";
            rblTab.DataValueField = "Value";
            rblTab.DataBind();

            if (rblTab.Items.Count > 1)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem item = rblTab.Items.FindByValue(selectedValue);
                    if (item != null)
                    {
                        rblTab.Items.FindByValue(selectedValue).Selected = true;
                    }
                }
                else { rblTab.SelectedIndex = 0; }
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
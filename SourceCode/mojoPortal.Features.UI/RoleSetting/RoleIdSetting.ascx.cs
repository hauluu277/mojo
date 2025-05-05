using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.RoleSetting.UI
{
    public partial class RoleIdSetting : UserControl, ISettingControl
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
            BindRoleSettings();
            ListItem RoleOption = new ListItem();
            RoleOption.Text = Resources.ArticleResources.RoleSettingChooseItem;
            RoleOption.Value = "0";
            ddlRoleSetting.Items.Insert(0, RoleOption);
        }


        private void PopulateLabels()
        {
            //ltrRoleSettingChoose.Text = Resources.RoleSettingResources.RoleSettingChooseLabel;
        }

        private void EnsureItems()
        {
            //why is this null here, its declared
            if (ddlRoleSetting != null) return;
            ddlRoleSetting = new DropDownList();
            if (Controls.Count != 0) return;
            Controls.Add(ddlRoleSetting);
        }

        private void GetSelectedItems()
        {
            selectedValue = string.Empty;
            selectedValue = ddlRoleSetting.SelectedValue;
            if (selectedValue == string.Empty) return;
        }

        private void BindSelection()
        {
            if (!selectedValue.Equals(string.Empty))
            {
                BindRoleSettings();
            }
        }

        private void BindRoleSettings()
        {
            SiteSettings settings = CacheHelper.GetCurrentSiteSettings();
            Collection<Role> siteRoles = Role.GetbySite(settings.SiteId);
            if (!WebUser.IsAdmin)
            {
                // must be only Role Admin
                // remove admins role and role admins role
                // from list. Role Admins can't edit or manage
                // membership in Admins role or Role Admins role
                foreach (Role r in siteRoles)
                {
                    if (r.Equals("Admins"))
                    {
                        siteRoles.Remove(r);
                        break;
                    }
                }

                foreach (Role r in siteRoles)
                {
                    if (r.Equals("Role Admins"))
                    {
                        siteRoles.Remove(r);
                        break;
                    }
                }

                if (!WebConfigSettings.AllowRoleAdminsToCreateContentManagers)
                {
                    foreach (Role r in siteRoles)
                    {
                        if (r.Equals("Content Administrators"))
                        {
                            siteRoles.Remove(r);
                            break;
                        }
                    }
                }
            }

            ddlRoleSetting.DataSource = siteRoles;
            ddlRoleSetting.DataTextField = "RoleName";
            ddlRoleSetting.DataValueField = "RoleID";
            ddlRoleSetting.DataBind();

            if (ddlRoleSetting.Items.Count > 0)
            {
                if (!selectedValue.Equals(string.Empty))
                {
                    ListItem item = ddlRoleSetting.Items.FindByValue(selectedValue);
                    if (item != null)
                    {
                        ddlRoleSetting.Items.FindByValue(selectedValue).Selected = true;
                    }
                }
                else { ddlRoleSetting.SelectedIndex = 0; }
            }
            for (int i = 0; i < ddlRoleSetting.Items.Count; i++)
            {
                ddlRoleSetting.Items[i].Text = ddlRoleSetting.Items[i].Text.Replace("<span class='second left'>", string.Empty).Replace(
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
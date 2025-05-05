using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
//using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using mojoPortal.Features;

namespace mojoPortal.Web.AdminUI
{
    public partial class FormCategory : mojoBasePage
    {
        private int itemId = -1;
        private SiteUser siteUser = SiteUtils.GetCurrentSiteUser();

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSave.Click += btnSave_Click;
            btnRemove.Click += btnRemove_Click;
            lnkCancel.Click += lnkCancel_Click;
        }



        private void lnkCancel_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Admin/CategoryArticle.aspx";
            WebUtils.SetupRedirect(this, url);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (itemId > 0)
            {
                CoreCategory.Delete(itemId);
                string url = SiteRoot + "/Admin/CategoryArticle.aspx";
                WebUtils.SetupRedirect(this, url);
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!AccessRole())
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }
            LoadSettings();
            PopulateLabel();
            PopulateControl();
        }
        private bool AccessRole()
        {
            if (!Request.IsAuthenticated)
            {
                return false;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageArticle))
            {
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private bool Save()
        {
            Page.Validate("validateCategory");
            if (!Page.IsValid) return false;

            try
            {
                CoreCategory category;
                if (itemId > 0)
                {
                    category = new CoreCategory(itemId);
                    category.ModifiedBy = siteUser.UserGuid;
                    category.ModifiedUtc = DateTime.UtcNow;
                }
                else
                {
                    category = new CoreCategory();

                    category.CreatedBy = siteUser.UserGuid;
                    category.CreatedUtc = DateTime.UtcNow;

                }
                category.Name = txtName.Text;
                category.ParentID = Convert.ToInt32(drlCategory.SelectedValue);
                category.Description = txtUrl.Text;
                category.SiteID = siteSettings.SiteId;

                category.Priority = txtOrder.Text.ToIntOrZero();

                category.Save();
                if (itemId > 0)
                {
                    var listUserBtv = CategoryUserArticle.GetSelected(itemId, ArticleContant.BienTapVien);
                    if (listUserBtv.Any())
                    {
                        foreach (var item in listUserBtv)
                        {
                            CategoryUserArticle.Delete(item.ItemID);
                        }
                    }

                }
                //save user btv
                foreach (ListItem item in cblBienTapVien.Items)
                {
                    if (item.Selected)
                    {
                        CategoryUserArticle categoryUser = new CategoryUserArticle();
                        categoryUser.CategoryID = category.ItemID;
                        categoryUser.UserID = Convert.ToInt32(item.Value);
                        categoryUser.TypeRole = ArticleContant.BienTapVien;
                        categoryUser.Save();
                    }
                }

                //save user kdv
                foreach (ListItem item in cblKiemDuyetVien.Items)
                {
                    if (item.Selected)
                    {
                        CategoryUserArticle categoryUser = new CategoryUserArticle();
                        categoryUser.CategoryID = category.ItemID;
                        categoryUser.UserID = Convert.ToInt32(item.Value);
                        categoryUser.TypeRole = ArticleContant.KiemDuyetVien;
                        categoryUser.Save();
                    }
                }

                if (itemId > 0)
                {
                    string url = SiteRoot + "/Admin/CategoryArticle.aspx";
                    WebUtils.SetupRedirect(this, url);
                }
                else
                {
                    string url = SiteRoot + "/Admin/CategoryArticle.aspx";
                    WebUtils.SetupRedirect(this, url);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return false;
            }
            return true;
        }
        private void LoadSettings()
        {
            itemId = WebUtils.ParseInt32FromQueryString("item", itemId);
        }
        private void PopulateControl()
        {

            if (!IsPostBack)
            {
                BindUserKdv();
                BindUserBtv();
                LoadParentCategory();
                LoadCategory();
            }
        }
        private void PopulateLabel()
        {
            btnSave.Text = Resources.Resource.CategoryButtonSave;
            btnRemove.Text = Resources.Resource.CategoryButtonDelete;
            lnkCancel.Text = Resources.Resource.CategoryCancelButton;
            SiteUtils.AddConfirmButton(btnRemove, Resources.Resource.CategoryComfirmDelete);
            if (itemId > 0)
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, "Cập nhập chuyên mục tin bài");
            }
            else
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, "Thêm mới chuyên mục tin bài");
            }
        }

        private void BindUserKdv()
        {
            cblKiemDuyetVien.DataValueField = "UserID";
            cblKiemDuyetVien.DataTextField = "Name";
            cblKiemDuyetVien.DataSource = SiteUser.GetByRole(siteSettings.SiteId, WebConfigSettings.RoleApprovedArticle);
            cblKiemDuyetVien.DataBind();
            cblKiemDuyetVien.RepeatColumns = 3;
        }


        private void BindUserBtv()
        {
            cblBienTapVien.DataValueField = "UserID";
            cblBienTapVien.DataTextField = "Name";
            cblBienTapVien.DataSource = SiteUser.GetByRole(siteSettings.SiteId, WebConfigSettings.RolePublishedArticle);
            cblBienTapVien.DataBind();
            cblBienTapVien.RepeatColumns = 3;
        }

        private void LoadCategory()
        {
            if (itemId > 0)
            {
                CoreCategory category = new CoreCategory(itemId);
                txtName.Text = category.Name;
                txtOrder.Text = category.Priority.ToString();
                txtUrl.Text = category.Description;
                if (category.ParentID > 0)
                {
                    drlCategory.SelectedValue = category.ParentID.ToString();
                }
                btnRemove.Visible = true;

                var listBtvSelected = CategoryUserArticle.GetSelected(itemId, ArticleContant.BienTapVien);
                if (listBtvSelected.Any())
                {
                    foreach (ListItem item in cblBienTapVien.Items)
                    {
                        if (listBtvSelected.Where(x => x.UserID == Convert.ToInt32(item.Value)).Any())
                        {
                            item.Selected = true;
                        }
                    }
                }

                var listKdvSelected = CategoryUserArticle.GetSelected(itemId, ArticleContant.KiemDuyetVien);
                if (listKdvSelected.Any())
                {
                    foreach (ListItem item in cblKiemDuyetVien.Items)
                    {
                        if (listKdvSelected.Where(x => x.UserID == Convert.ToInt32(item.Value)).Any())
                        {
                            item.Selected = true;
                        }
                    }
                }

                heading.Text = "Cập nhật chuyên mục tin bài";
            }
            else
            {
                heading.Text = "Thêm mới chuyên mục tin bài";
            }
        }
        private void LoadParentCategory()
        {
            drlCategory.DataTextField = "Name";
            drlCategory.DataValueField = "ItemID";
            drlCategory.DataSource = BindCategory();
            drlCategory.DataBind();

            //ListItem searchOption = new ListItem();
            //searchOption.Text = Resources.Resource.ParentCategoryChoose;
            //searchOption.Value = "0";
            //drlCategory.Items.Insert(0, searchOption);
        }
        private List<CoreCategory> BindCategory()
        {
            List<CoreCategory> categories = new List<CoreCategory>();

            //CoreCategory defaultCat = new CoreCategory();
            //defaultCat.ItemID = 0;
            //defaultCat.Name = Resources.Resource.ParentCategoryChoose;
            //categories.Add(defaultCat);

            List<CoreCategory> roots = CoreCategory.GetRoot(siteSettings.SiteId);
            foreach (CoreCategory item in roots)
            {
                categories.Add(item);
            }
            PopulateChildItem(categories, 0);

            return categories;
        }
        private void PopulateChildItem(List<CoreCategory> root, int itemId)
        {
            for (int i = 0; i < root.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(root[i].ItemID);
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root[i].Name.StartsWith("|"))
                {
                    prefix += root[i].Name.Substring(0, 3);
                    root[i].Name = root[i].Name.Remove(0, 3);
                }
                root[i].Name = prefix + root[i].Name;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    if (child.ItemID.Equals(itemId)) continue;

                    child.Name = prefix + @"|--" + child.Name;
                    root.Insert(root.IndexOf(root[i]) + index, child);
                    index++;
                }
            }
        }
    }
}
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.AdminUI
{
    public partial class LoaiVanBan : NonCmsBasePage
    {
        private int pageNumber = 1;
        private int siteId = -1;
        private int pageSize = 15;
        private int totalPages = 0;
        private string sort = "Name";
        private string keyword = string.Empty;
        private int parentid = 0;
        private bool isAdmin = false;
        private bool isContentAdmin = false;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            if ((!isAdmin) && (!isContentAdmin))
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }

            PopulateLabels();
            PopulateControls();
        }

        private void PopulateControls()
        {
            if (Page.IsPostBack) return;

            BindGrid();
            //BindSearchCategory();

            //ListItem searchOption = new ListItem();
            //searchOption.Text = Resources.Resource.ParentCategoryChoose;
            //searchOption.Value = "0";
            //ddlSearchCategory.Items.Insert(0, searchOption);

            //ddlSearchCategory.SelectedValue = parentid.ToString();
            txtKeyword.Text = string.IsNullOrEmpty(keyword) ? "" : keyword.ToString();

        }

        //private void BindSearchCategory()
        //{
        //    ddlSearchCategory.DataTextField = "Name";
        //    ddlSearchCategory.DataValueField = "ItemID";
        //    ddlSearchCategory.DataSource = BindCategory();
        //    ddlSearchCategory.DataBind();
        //}
        private void BindGrid()
        {
            btnAddNew.Visible = true;
            btnAddNewTop.Visible = true;
            List<LoaiVB> cq = LoaiVB.GetPage(siteSetting.SiteId, pageNumber, pageSize, keyword, out totalPages);
            Comparison<LoaiVB> sortComparer;

            switch (sort)
            {
                case "Description":
                    sortComparer
                        = new Comparison<LoaiVB>(LoaiVB.CompareByDescription);
                    break;

                case "Name":
                default:
                    sortComparer
                        = new Comparison<LoaiVB>(LoaiVB.CompareByName);
                    break;
            }

            if (sortComparer != null)
            {
                cq.Sort(sortComparer);
            }



            if (this.totalPages > 1)
            {
                string pageUrl = SiteRoot + "/Admin/VanBanPhapQuy/LoaiVanBan.aspx?pagenumber={0}&amp;sort=" + sort + "&amp;key=" + keyword;

                pgrContentCategory.Visible = true;
                pgrContentCategory.PageURLFormat = pageUrl;
                pgrContentCategory.ShowFirstLast = true;
                pgrContentCategory.CurrentIndex = pageNumber;
                pgrContentCategory.PageSize = pageSize;
                pgrContentCategory.PageCount = totalPages;

            }
            else
            {
                pgrContentCategory.Visible = false;
            }

            grdContentField.DataSource = cq;
            grdContentField.PageIndex = pageNumber;
            grdContentField.PageSize = pageSize;
            grdContentField.DataBind();

        }

        private void grdContentField_Sorting(object sender, GridViewSortEventArgs e)
        {
            String redirectUrl = SiteRoot
                + "/Admin/VanBanPhapQuy/LoaiVanBan.aspx?pagenumber=" + pageNumber.ToString(CultureInfo.InvariantCulture)
                + "&sort=" + e.SortExpression + "&amp;key=" + keyword;

            WebUtils.SetupRedirect(this, redirectUrl);

        }

        private void btnSearchCoreCategory_Click(object sender, EventArgs e)
        {
            keyword = txtKeyword.Text;
            //parentid = int.Parse(ddlSearchCategory.SelectedValue);
            BindGrid();
        }

        private void grdContentField_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView grid = (GridView)sender;
            string selectItem = grid.DataKeys[e.RowIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;

            TextBox txtName = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtName");
            //TextBox txtNameEN = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtNameEN");
            //TextBox txtParentID = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtParentID");
            //DropDownList ddlCategory = (DropDownList)grid.Rows[e.RowIndex].Cells[1].FindControl("ddlCategory");
            //TextBox txtPriority = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtPriority");
            TextBox txtDescription = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtDescription");
            LoaiVB loaiVb;
            if (itemId <= 0)
            {
                loaiVb = new LoaiVB();
            }
            else
            {
                loaiVb = new LoaiVB(itemId);
            }

            loaiVb.Name = txtName.Text;
            //loaiVb.NameEN = txtNameEN.Text;
            loaiVb.SiteID = siteSettings.SiteId;
            //coreCategory.ParentID = int.Parse(ddlCategory.SelectedValue);
            //coreCategory.Priority = int.Parse(txtPriority.Text);
            loaiVb.Description = txtDescription.Text;
            loaiVb.CreatedUtc = DateTime.UtcNow;
            loaiVb.CreatedBy = siteUser.UserGuid;

            loaiVb.Save();

            WebUtils.SetupRedirect(this, Request.RawUrl);

        }

        private void grdContentField_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView grid = (GridView)sender;
            string selectItem = grid.DataKeys[e.RowIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;
            LoaiVB.Delete(itemId);
            WebUtils.SetupRedirect(this, Request.RawUrl);

        }

        private void grdContentField_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            WebUtils.SetupRedirect(this, Request.RawUrl);
        }

        private void grdContentField_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView grid = (GridView)sender;
            grid.EditIndex = e.NewEditIndex;
            BindGrid();
            string selectItem = grid.DataKeys[e.NewEditIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;

            //var ddlCategory = grid.Rows[e.NewEditIndex].Cells[2].FindControl("ddlCategory") as DropDownList;
            ////if (ddlCategory != null)
            ////{
            ////    PopulateCategories(itemId, ddlCategory);
            ////}

            //ddlCategory.SelectedValue = new CoreCategory(itemId).ParentID.ToString();
            Button btnDelete = (Button)grid.Rows[e.NewEditIndex].Cells[0].FindControl("btnGridDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("OnClick", "return confirm('"
                    + Resource.CoreCategoryGridDeleteWarning + "');");
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ItemID", typeof(int));
            dataTable.Columns.Add("Name", typeof(String));
            //dataTable.Columns.Add("NameEN", typeof(String));
            //dataTable.Columns.Add("ParentID", typeof(int));
            //dataTable.Columns.Add("ddlCategory", typeof(string));
            //dataTable.Columns.Add("Priority", typeof(int));
            dataTable.Columns.Add("Description", typeof(String));
            dataTable.Columns.Add("TotalPages", typeof(int));
            DataRow row = dataTable.NewRow();
            int rowIndex = 0;
            //DropDownList ddlCategory = (DropDownList)grdContentField.Rows[rowIndex].Cells[1].FindControl("ddlCategory");
            row["ItemID"] = -1;
            row["Name"] = string.Empty;
            //row["ParentID"] = 0;
            //row["ddlCategory"] = string.Empty;// ddlCategory.SelectedItem.Text;
            //row["Priority"] = 0;
            row["Description"] = string.Empty;
            row["TotalPages"] = 1;
            dataTable.Rows.Add(row);

            btnAddNew.Visible = false;
            btnAddNewTop.Visible = false;
            pgrContentCategory.Visible = false;
            grdContentField.EditIndex = 0;
            grdContentField.DataSource = dataTable;
            grdContentField.DataBind();

        }


        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, Resource.CoreCategoryAdministrationHeading);

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkCurrentPage.Text = Resource.CoreCategoryAdministrationHeading;
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/VanBanPhapQuy/LoaiVanBan.aspx";

            heading.Text = Resource.CoreCategoryAdministrationHeading;

            grdContentField.ToolTip = Resource.CoreCategoryAdministrationHeading;

            this.grdContentField.Columns[1].HeaderText = Resource.CoreCategoryGridNameHeader;
            //this.grdContentField.Columns[2].HeaderText = Resource.CoreCategoryGridNameENHeader;
            this.grdContentField.Columns[2].HeaderText = Resource.CoreCategoryGridDescriptionHeader;
            btnAddNew.Text = Resource.CoreCategoryGridAddNewButton;
            btnAddNewTop.Text = Resource.CoreCategoryGridAddNewButton;
            btnSearchCoreCategory.Text = Resource.CoreCategorySearchCoreCategory;

        }

        private void LoadSettings()
        {
            siteId = siteUser.SiteId;
            isAdmin = WebUser.IsAdmin;
            isContentAdmin = WebUser.IsContentAdmin || SiteUtils.UserIsSiteEditor();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", 1);
            keyword = Page.Request.Params["key"];
            parentid = WebUtils.ParseInt32FromQueryString("parentid", 0);
            if (Page.Request.Params["sort"] != null)
            {
                sort = Page.Request.Params["sort"];
            }

            AddClassToBody("administration");
            AddClassToBody("geoadmin");
        }



        //private List<Field> BindCategory()
        //{
        //    List<Field> fields = new List<Field>();

        //    //CoreCategory defaultCat = new CoreCategory();
        //    //defaultCat.ItemID = 0;
        //    //defaultCat.Name = Resources.Resource.ParentCategoryChoose;
        //    //categories.Add(defaultCat);

        //    List<Field> roots = Field.GetRoot(siteSettings.SiteId);
        //    foreach (CoreCategory item in roots)
        //    {
        //        categories.Add(item);
        //    }
        //    PopulateChildItem(categories, 0);

        //    return categories;
        //}

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

        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            this.grdContentField.RowDataBound += new GridViewRowEventHandler(grdContentField_RowDataBound);
            this.grdContentField.Sorting += new GridViewSortEventHandler(grdContentField_Sorting);
            this.grdContentField.RowEditing += new GridViewEditEventHandler(grdContentField_RowEditing);
            this.grdContentField.RowCancelingEdit += new GridViewCancelEditEventHandler(grdContentField_RowCancelingEdit);
            this.grdContentField.RowUpdating += new GridViewUpdateEventHandler(grdContentField_RowUpdating);
            this.grdContentField.RowDeleting += new GridViewDeleteEventHandler(grdContentField_RowDeleting);

            this.btnAddNew.Click += new EventHandler(btnAddNew_Click);
            this.btnAddNewTop.Click += new EventHandler(btnAddNew_Click);
            this.btnSearchCoreCategory.Click += new EventHandler(btnSearchCoreCategory_Click);

            SuppressMenuSelection();
            SuppressPageMenu();

            ScriptConfig.IncludeJQTable = true;

        }

        #endregion

        protected void grdContentField_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Control ddlCategory = e.Row.FindControl("ddlCategory");

            //    if (ddlCategory != null)
            //    {
            //        DropDownList dd = ddlCategory as DropDownList;
            //        dd.DataTextField = "Name";
            //        dd.DataValueField = "ItemID";
            //        dd.DataSource = BindCategory();
            //        dd.DataBind();

            //        ListItem searchOption = new ListItem();
            //        searchOption.Text = Resources.Resource.ParentCategoryChoose;
            //        searchOption.Value = "0";
            //        dd.Items.Insert(0, searchOption);
            //    }
            //}
        }
    }
}
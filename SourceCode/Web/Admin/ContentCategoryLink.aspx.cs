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
    public partial class ContentCategoryLink : NonCmsBasePage
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
        private int langId = -1;
        private int LangVN = 1;//ngon ngu tieng viet
        private int LangEN = 2;//ngon ngu tieng anh
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LangVN : LangEN;
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
            txtKeyword.Text = string.IsNullOrEmpty(keyword) ? "" : keyword.ToString();

        }

        private void BindGrid()
        {
            btnAddNew.Visible = true;
            btnAddNewTop.Visible = true;
            List<CategoryLink> fields = CategoryLink.GetPage(siteSetting.SiteId, pageNumber, pageSize, keyword, out totalPages);
            Comparison<CategoryLink> sortComparer;

            switch (sort)
            {
                case "Name":
                default:
                    sortComparer
                        = new Comparison<CategoryLink>(CategoryLink.CompareByName);
                    break;
            }

            if (sortComparer != null)
            {
                fields.Sort(sortComparer);
            }



            if (this.totalPages > 1)
            {
                string pageUrl = SiteRoot + "/Admin/ContentCategoryLink.aspx?pagenumber={0}&amp;sort=" + sort + "&amp;key=" + keyword;

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

            grdContentField.DataSource = fields;
            grdContentField.PageIndex = pageNumber;
            grdContentField.PageSize = pageSize;
            grdContentField.DataBind();

        }

        private void grdContentField_Sorting(object sender, GridViewSortEventArgs e)
        {
            String redirectUrl = SiteRoot
                + "/Admin/ContentCategoryLink.aspx?pagenumber=" + pageNumber.ToString(CultureInfo.InvariantCulture)
                + "&sort=" + e.SortExpression + "&amp;key=" + keyword;

            WebUtils.SetupRedirect(this, redirectUrl);

        }

        private void btnSearchCoreCategory_Click(object sender, EventArgs e)
        {
            keyword = txtKeyword.Text;
            BindGrid();
        }

        private void grdContentField_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView grid = (GridView)sender;
            string selectItem = grid.DataKeys[e.RowIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;

            TextBox txtName = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtName");
            TextBox txtOrderBy = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtOrderBy");
            //TextBox txtNameEN = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtNameEN");
            CategoryLink cateLink;
            if (itemId <= 0)
            {
                cateLink = new CategoryLink();
            }
            else
            {
                cateLink = new CategoryLink(itemId);
            }

            cateLink.Name = txtName.Text;
            //cateLink.NameEN = txtNameEN.Text;
            cateLink.SiteID = siteSettings.SiteId;
            cateLink.CreatedUtc = DateTime.UtcNow;
            cateLink.CreatedBy = siteUser.UserGuid;
            if (!string.IsNullOrEmpty(txtOrderBy.Text))
            {
                cateLink.OrderBy = int.Parse(txtOrderBy.Text);
            }
            cateLink.Save();

            WebUtils.SetupRedirect(this, Request.RawUrl);

        }

        private void grdContentField_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView grid = (GridView)sender;
            string selectItem = grid.DataKeys[e.RowIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;
            CategoryLink.Delete(itemId);
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
            keyword = txtKeyword.Text;
            BindGrid();
            string selectItem = grid.DataKeys[e.NewEditIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;
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
            dataTable.Columns.Add("OrderBy", typeof(int));
            //dataTable.Columns.Add("NameEN", typeof(String));
            dataTable.Columns.Add("TotalPages", typeof(int));
            DataRow row = dataTable.NewRow();
            int rowIndex = 0;
            row["ItemID"] = -1;
            row["Name"] = string.Empty;
            //row["NameEN"] = string.Empty;
            row["OrderBy"] = 0;
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
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/ContentCategoryLink.aspx";

            heading.Text = Resource.CoreCategoryAdministrationHeading;

            grdContentField.ToolTip = Resource.CoreCategoryAdministrationHeading;

            this.grdContentField.Columns[1].HeaderText = Resource.CoreCategoryGridNameHeader;
            this.grdContentField.Columns[2].HeaderText = "#";
            //this.grdContentField.Columns[2].HeaderText = Resource.CoreCategoryGridNameENHeader;
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
        }
    }
}
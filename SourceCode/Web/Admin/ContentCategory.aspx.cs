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
    public partial class ContentCategory : NonCmsBasePage
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
        protected string EditLinkImageUrl = string.Empty;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();

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
            BindSearchCategory();

            ListItem searchOption = new ListItem();
            searchOption.Text = Resources.Resource.ParentCategoryChoose;
            searchOption.Value = "0";
            ddlSearchCategory.Items.Insert(0, searchOption);

            ddlSearchCategory.SelectedValue = parentid.ToString();
            txtKeyword.Text = string.IsNullOrEmpty(keyword) ? "" : keyword.ToString();

        }

        private void BindSearchCategory()
        {
            ddlSearchCategory.DataTextField = "Name";
            ddlSearchCategory.DataValueField = "ItemID";
            ddlSearchCategory.DataSource = BindCategory();
            ddlSearchCategory.DataBind();
        }
        public List<Language> BindLanguage()
        {
            List<Language> language = Language.GetAll();
            return language;
        }
        protected string FormatImgUrlLanguage(string code)
        {
            string Imgurl = "~/Data/SiteImages/flags/" + code + ".gif";
            return Imgurl;
        }
        private void BindGrid()
        {
            var lang = CultureInfo.CurrentCulture;
            btnAddNew.Visible = true;
            btnAddNewTop.Visible = true;
            string childList = string.Empty;
            if (parentid > 0)
            {
                childList = CoreCategory.GetListChildren(parentid);
            }
            List<CoreCategory> categories = CoreCategory.GetPage(siteId, pageNumber, pageSize, keyword, childList, out totalPages);
            Comparison<CoreCategory> sortComparer;

            switch (sort)
            {
                case "ParentID":
                    sortComparer
                        = new Comparison<CoreCategory>(CoreCategory.CompareByParentID);
                    break;

                case "Priority":
                    sortComparer
                        = new Comparison<CoreCategory>(CoreCategory.CompareByPriority);
                    break;

                case "Description":
                    sortComparer
                        = new Comparison<CoreCategory>(CoreCategory.CompareByDescription);
                    break;

                case "Name":
                default:
                    sortComparer
                        = new Comparison<CoreCategory>(CoreCategory.CompareByName);
                    break;
            }

            if (sortComparer != null)
            {
                categories.Sort(sortComparer);
            }



            if (this.totalPages > 1)
            {
                string pageUrl = SiteRoot + "/Admin/ContentCategory.aspx?pagenumber={0}&amp;sort=" + sort + "&amp;key=" + keyword + "&amp;parentid=" + parentid;

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

            grdContentCategory.DataSource = categories;
            grdContentCategory.PageIndex = pageNumber;
            grdContentCategory.PageSize = pageSize;
            grdContentCategory.DataBind();

        }

        private void grdContentCategory_Sorting(object sender, GridViewSortEventArgs e)
        {
            String redirectUrl = SiteRoot
                + "/Admin/ContentCategory.aspx?pagenumber=" + pageNumber.ToString(CultureInfo.InvariantCulture)
                + "&sort=" + e.SortExpression + "&amp;key=" + keyword + "&amp;parentid=" + parentid;

            WebUtils.SetupRedirect(this, redirectUrl);

        }

        private void btnSearchCoreCategory_Click(object sender, EventArgs e)
        {
            keyword = txtKeyword.Text;
            parentid = int.Parse(ddlSearchCategory.SelectedValue);
            String redirectUrl = SiteRoot
                + "/Admin/ContentCategory.aspx?pagenumber=" + pageNumber.ToString(CultureInfo.InvariantCulture)
                + "&key=" + keyword + "&parentid=" + parentid;

            WebUtils.SetupRedirect(this, redirectUrl);
            //BindGrid();
        }

        private void grdContentCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView grid = (GridView)sender;
            string selectItem = grid.DataKeys[e.RowIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;

            TextBox txtName = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtName");
            //TextBox txtNameEN = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtNameEN");
            //TextBox txtParentID = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtParentID");
            //DropDownList ddlCategoryIcon = (DropDownList)grid.Rows[e.RowIndex].Cells[1].FindControl("ddlIcon");
            DropDownList ddlCategory = (DropDownList)grid.Rows[e.RowIndex].Cells[1].FindControl("ddlCategory");
            TextBox txtPriority = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtPriority");
            TextBox txtDescription = (TextBox)grid.Rows[e.RowIndex].Cells[1].FindControl("txtDescription");
            Image img = (Image)grid.Rows[e.RowIndex].Cells[1].FindControl("imgIcon");
            //img.ImageUrl = ddlCategoryIcon.SelectedItem.ToString();
            CoreCategory coreCategory;
            if (itemId <= 0)
            {
                coreCategory = new CoreCategory();
            }
            else
            {
                coreCategory = new CoreCategory(itemId);
            }

            coreCategory.Name = txtName.Text;
            //coreCategory.IconID = int.Parse(ddlCategoryIcon.SelectedValue);
            //coreCategory.NameEN = txtNameEN.Text;
            coreCategory.SiteID = siteSettings.SiteId;
            coreCategory.ParentID = int.Parse(ddlCategory.SelectedValue);
            coreCategory.Priority = int.Parse(txtPriority.Text);
            coreCategory.Description = txtDescription.Text;
            coreCategory.ItemCount = 0;
            coreCategory.CreatedUtc = DateTime.UtcNow;
            coreCategory.CreatedBy = siteUser.UserGuid;
            coreCategory.ModifiedUtc = DateTime.UtcNow;
            coreCategory.ModifiedBy = siteUser.UserGuid;

            coreCategory.Save();

            WebUtils.SetupRedirect(this, Request.RawUrl);

        }

        private void grdContentCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView grid = (GridView)sender;
            string selectItem = grid.DataKeys[e.RowIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;
            CoreCategory.Delete(itemId);
            WebUtils.SetupRedirect(this, Request.RawUrl);

        }

        private void grdContentCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            WebUtils.SetupRedirect(this, Request.RawUrl);
        }

        private void grdContentCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView grid = (GridView)sender;
            grid.EditIndex = e.NewEditIndex;
            BindGrid();
            string selectItem = grid.DataKeys[e.NewEditIndex].Value.ToString();
            int itemId = !string.IsNullOrEmpty(selectItem) ? int.Parse(selectItem) : 0;

            var ddlCategory = grid.Rows[e.NewEditIndex].Cells[3].FindControl("ddlCategory") as DropDownList;
            //if (ddlCategory != null)
            //{
            //    PopulateCategories(itemId, ddlCategory);
            //}
            //var ddlIcon = grid.Rows[e.NewEditIndex].Cells[1].FindControl("ddlIcon") as DropDownList;
            //ddlIcon.SelectedValue = new CoreCategory(itemId).IconID.ToString();
            ddlCategory.SelectedValue = new CoreCategory(itemId).ParentID.ToString();
            Button btnDelete = (Button)grid.Rows[e.NewEditIndex].Cells[0].FindControl("btnGridDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("OnClick", "return confirm('"
                    + Resource.CoreCategoryGridDeleteWarning + "');");
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("ddlIcon", typeof(string));
            //dataTable.Columns.Add("ItemID", typeof(int));
            //dataTable.Columns.Add("Name", typeof(String));
            ////dataTable.Columns.Add("NameEN", typeof(String));
            ////dataTable.Columns.Add("ParentID", typeof(int));
            //dataTable.Columns.Add("ddlCategory", typeof(string));
            //dataTable.Columns.Add("Priority", typeof(int));
            //dataTable.Columns.Add("Description", typeof(String));
            //dataTable.Columns.Add("TotalPages", typeof(int));
            //DataRow row = dataTable.NewRow();
            ////int rowIndex = 0;
            ////DropDownList ddlCategory = (DropDownList)grdContentCategory.Rows[rowIndex].Cells[1].FindControl("ddlCategory");
            //row["ddlIcon"] = string.Empty;
            //row["ItemID"] = -1;
            //row["Name"] = string.Empty;
            ////row["NameEN"] = string.Empty;
            ////row["ParentID"] = 0;
            //row["ddlCategory"] = string.Empty;// ddlCategory.SelectedItem.Text;
            //row["Priority"] = 0;
            //row["Description"] = string.Empty;
            //row["TotalPages"] = 1;
            //dataTable.Rows.Add(row);

            //btnAddNew.Visible = false;
            //btnAddNewTop.Visible = false;
            //pgrContentCategory.Visible = false;
            //grdContentCategory.EditIndex = 0;
            //grdContentCategory.DataSource = dataTable;
            //grdContentCategory.DataBind();
            string url = SiteRoot + "/Admin/EditPostCategory.aspx";
            WebUtils.SetupRedirect(this, url);

        }

        //protected void ImgBut_Lang_Click(object sender, CommandEventArgs e)
        //{
        //    DataTable dataTable = new DataTable();
        //    int LangID =Convert.ToInt32(e.CommandArgument);
        //    dataTable.Columns.Add("ItemID", typeof(int));
        //    dataTable.Columns.Add("Name", typeof(String));
        //    //dataTable.Columns.Add("ParentID", typeof(int));
        //    dataTable.Columns.Add("ddlCategory", typeof(string));
        //    dataTable.Columns.Add("Priority", typeof(int));
        //    dataTable.Columns.Add("Description", typeof(String));
        //    dataTable.Columns.Add("LangID", typeof(int));
        //    dataTable.Columns.Add("TotalPages", typeof(int));

        //    DataRow row = dataTable.NewRow();
        //    int rowIndex = 0;
        //    DropDownList ddlCategory = (DropDownList)grdContentCategory.Rows[rowIndex].Cells[1].FindControl("ddlCategory");
        //    row["ItemID"] = -1;
        //    row["Name"] = string.Empty;
        //    //row["ParentID"] = 0;
        //    row["ddlCategory"] = string.Empty;// ddlCategory.SelectedItem.Text;
        //    row["Priority"] = 0;
        //    row["Description"] = string.Empty;
        //    row["LangID"] = LangID;
        //    row["TotalPages"] = 1;           
        //    dataTable.Rows.Add(row);

        //    btnAddNew.Visible = false;
        //    btnAddNewTop.Visible = false;
        //    pgrContentCategory.Visible = false;
        //    grdContentCategory.EditIndex = 0;
        //    grdContentCategory.DataSource = dataTable;
        //    grdContentCategory.DataBind();

        //}
        private void PopulateLabels()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            Title = SiteUtils.FormatPageTitle(siteSettings, Resource.CoreCategoryAdministrationHeading);

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkCurrentPage.Text = Resource.CoreCategoryAdministrationHeading;
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/ContentCategory.aspx";

            heading.Text = Resource.CoreCategoryAdministrationHeading;

            grdContentCategory.ToolTip = Resource.CoreCategoryAdministrationHeading;

            //this.grdContentCategory.Columns[1].HeaderText = Resource.CoreCategoryGridIconHeader;
            //this.grdContentCategory.Columns[1].HeaderText = Resource.CoreCategoryGridNameHeader;
            this.grdContentCategory.Columns[1].HeaderText = Resource.CoreCategoryGridParentHeader;
            this.grdContentCategory.Columns[2].HeaderText = Resource.CoreCategoryGridPriorityHeader;
            this.grdContentCategory.Columns[3].HeaderText = Resource.CoreCategoryGridDescriptionHeader;
            this.grdContentCategory.Columns[4].HeaderText = "";
            btnAddNew.Text = Resource.CoreCategoryGridAddNewButton;
            btnAddNewTop.Text = Resource.CoreCategoryGridAddNewButton;
            btnSearchCoreCategory.Text = Resource.CoreCategorySearchCoreCategory;

        }

        private void LoadSettings()
        {
            siteId = siteSettings.SiteId;
            isAdmin = WebUser.IsAdmin;
            isContentAdmin = WebUser.IsContentAdmin || SiteUtils.UserIsSiteEditor();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", 1);
            keyword = WebUtils.ParseStringFromQueryString("key", string.Empty);
            parentid = WebUtils.ParseInt32FromQueryString("parentid", 0);
            if (Page.Request.Params["sort"] != null)
            {
                sort = Page.Request.Params["sort"];
            }

            AddClassToBody("administration");
            AddClassToBody("geoadmin");
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
        private List<CoreCategoryIcon> BindIcon()
        {
            List<CoreCategoryIcon> categorieIcons = new List<CoreCategoryIcon>();


            List<CoreCategoryIcon> icons = CoreCategoryIcon.GetAll();
            foreach (CoreCategoryIcon item in icons)
            {
                categorieIcons.Add(item);
            }

            return categorieIcons;
        }


        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            this.grdContentCategory.RowDataBound += new GridViewRowEventHandler(grdContentCategory_RowDataBound);
            this.grdContentCategory.Sorting += new GridViewSortEventHandler(grdContentCategory_Sorting);
            this.grdContentCategory.RowEditing += new GridViewEditEventHandler(grdContentCategory_RowEditing);
            this.grdContentCategory.RowCancelingEdit += new GridViewCancelEditEventHandler(grdContentCategory_RowCancelingEdit);
            this.grdContentCategory.RowUpdating += new GridViewUpdateEventHandler(grdContentCategory_RowUpdating);
            this.grdContentCategory.RowDeleting += new GridViewDeleteEventHandler(grdContentCategory_RowDeleting);
            this.grdContentCategory.RowCommand += new GridViewCommandEventHandler(grdContentCategory_RowCommand);

            this.btnAddNew.Click += new EventHandler(btnAddNew_Click);
            this.btnAddNewTop.Click += new EventHandler(btnAddNew_Click);
            this.btnSearchCoreCategory.Click += new EventHandler(btnSearchCoreCategory_Click);

            SuppressMenuSelection();
            SuppressPageMenu();

            ScriptConfig.IncludeJQTable = true;

        }

        private void grdContentCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                int itemid = Convert.ToInt32(e.CommandArgument.ToString());
                string url = SiteRoot + "/Admin/EditPostCategory.aspx?item=" + itemid;
                WebUtils.SetupRedirect(this, url);
            }
            if (e.CommandName == "DeleteItem")
            {
                int itemid = Convert.ToInt32(e.CommandArgument.ToString());
                List<CoreCategory> lstChild = new List<CoreCategory>();
                lstChild = CoreCategory.GetChildrenByParent(itemid);
                if (lstChild.Count == 0)
                {
                    CoreCategory.Delete(itemid);
                }
                keyword = txtKeyword.Text;
                parentid = int.Parse(ddlSearchCategory.SelectedValue);
                String redirectUrl = SiteRoot
                    + "/Admin/ContentCategory.aspx?pagenumber=" + pageNumber.ToString(CultureInfo.InvariantCulture)
                    + "&key=" + keyword + "&parentid=" + parentid;

                WebUtils.SetupRedirect(this, redirectUrl);
            }
        }

        #endregion

        protected void grdContentCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Control ddlCategory = e.Row.FindControl("ddlCategory");
                //Control ddlIcon = e.Row.FindControl("ddlIcon");
                if (ddlCategory != null)
                {
                    DropDownList dd = ddlCategory as DropDownList;
                    dd.DataTextField = "Name";
                    dd.DataValueField = "ItemID";
                    dd.DataSource = BindCategory();
                    dd.DataBind();

                    ListItem searchOption = new ListItem();
                    searchOption.Text = Resources.Resource.ParentCategoryChoose;
                    searchOption.Value = "0";
                    dd.Items.Insert(0, searchOption);
                }
                //if (ddlIcon != null)
                //{
                //    DropDownList dd1 = ddlIcon as DropDownList;
                //    dd1.DataTextField = "IconUrl";
                //    dd1.DataValueField = "IconID";
                //    dd1.DataSource = BindIcon();
                //    dd1.DataBind();

                //    ListItem searchOption = new ListItem();
                //    //searchOption.Text=Resources.Resource.IconCategoryChoose;
                //    searchOption.Value = "0";
                //    dd1.Items.Insert(0, searchOption);
                //    dd1.Attributes["style"] = "background: url(" + SiteRoot + dd1.SelectedItem + ");background-repeat:no-repeat;";
                //    //for (int i = 0; i < dd1.Items.Count; i++)
                //    //{
                //    //    ListItem item = dd1.Items[i];
                //    //    item.Attributes["style"] = "background: url(" + SiteRoot + dd1.Text.Replace("~", string.Empty) + ");background-repeat:no-repeat;";
                //    //}
                //}
                ImageButton btn = e.Row.FindControl("ibtnDelete") as ImageButton;
                if (btn != null)
                {
                    int itemid = int.Parse(DataBinder.Eval(e.Row.DataItem, "ItemID", "{0}"));
                    List<CoreCategory> lstChild = new List<CoreCategory>();
                    lstChild = CoreCategory.GetChildrenByParent(itemid);
                    if (lstChild.Count > 0)
                    {
                        SiteUtils.AddMessageButton(btn, "Danh mục này đang có các danh mục con, bạn phải xóa các danh mục con trước !");
                    }
                    else
                    {
                        SiteUtils.AddConfirmButton(btn, Resource.CategoryComfirmDelete);
                    }
                }
            }
        }
    }
}
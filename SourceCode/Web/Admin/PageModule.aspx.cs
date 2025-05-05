/// Author:                     
/// Created:                    2004-08-22
///	Last Modified:              2013-02-18
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software. 

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using mojoPortal.SearchIndex;
using Resources;
using System.Collections.Generic;
using System.Linq;

namespace mojoPortal.Web.AdminUI
{

    public partial class PageModule : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PageLayout));

        private bool canEdit = false;
        private bool isSiteEditor = false;
        private int pageID = -1;
        private bool pageHasTopContent = false;
        private bool pageHasBottomContent = false;
        protected string EditSettingsImage = WebConfigSettings.EditPropertiesImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        private int globalContentCount = 0;
        private List<PageSettings> listPage = new List<PageSettings>();

        #region OnInit
        protected override void OnPreInit(EventArgs e)
        {
            // this page needs to use the same skin as the page in case there are extra content place holders
            //SetMasterInBasePage = false;
            AllowSkinOverride = true;
            base.OnPreInit(e);

            if (
                    (siteSettings.AllowPageSkins)
                    && (CurrentPage != null)
                    && (CurrentPage.Skin.Length > 0)
                    )
            {

                if (Global.RegisteredVirtualThemes)
                {
                    this.Theme = "pageskin-" + siteSettings.SiteId.ToInvariantString() + CurrentPage.Skin;
                }
                else
                {
                    this.Theme = "pageskin";
                }
            }
        }

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            this.btnCreateNewContent.Click += new EventHandler(this.btnCreateNewContent_Click);
            btnAddExisting.Click += new ImageClickEventHandler(btnAddExisting_Click);
            SuppressPageMenu();
        }

        #endregion
        private void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }

            SecurityHelper.DisableBrowserCache();

            LoadSettings();

            if (WebUser.IsAdminOrContentAdmin || isSiteEditor || WebUser.IsInRoles(CurrentPage.EditRoles))
            {
                canEdit = true;
            }

            if (CurrentPage.EditRoles == "Admins;")
            {
                if (!WebUser.IsAdmin)
                {
                    canEdit = false;
                }
            }


            PopulateLabels();

            if (!Page.IsPostBack)
            {
                PopulateControls();
                LoadPage();
            }
        }

        private void LoadPage()
        {
            LoadAllPage();
            var allPage = new List<PageSettings>();
            var root = PageSettings.GetRoot(SiteId);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='ul-page'>");
            stringBuilder.Append("<li><input type='checkbox' checked id='all_page'/> <span>Chọn tất cả</span></li>");
            stringBuilder.Append("<ul class='ul-page'>");
            foreach (var item in root)
            {
                var page = item;
                stringBuilder.Append($"<li data-id='{item.PageId}' data-parentid='{item.ParentId}'>");
                stringBuilder.Append($"<input type='checkbox' checked id='page_{item.PageId}'   data-id='{item.PageId}' data-parentid='{item.ParentId}'/><span>{item.PageName}</span>");
                GetChild(ref stringBuilder, ref page);
                stringBuilder.Append("</li>");
            }
            stringBuilder.Append("</ul>");
            stringBuilder.Append("</ul>");

            literLoadPage.Text = stringBuilder.ToString();
        }

        private void LoadAllPage()
        {
            var allPage = PageSettings.GetList(SiteId);
            listPage.AddRange(allPage);
        }

        public void GetChild(ref StringBuilder appendPage, ref PageSettings page)
        {
            var parentId = page.PageId;
            var lstChild = listPage.Where(x => x.ParentId == parentId).OrderBy(x => x.PageOrder).ToList();
            if (lstChild.Count > 0)
            {
                appendPage.Append("<ul class='ul-page'>");
                page.ListChildren = new List<PageSettings>();
                page.ListChildren.AddRange(lstChild);
                for (int i = 0; i < lstChild.Count; i++)
                {
                    appendPage.Append($"<li data-id='{lstChild[i].PageId}' data-parentid='{lstChild[i].ParentId}'/><input type='checkbox' checked id='page_{lstChild[i].PageId}' data-id='{lstChild[i].PageId}' data-parentid='{lstChild[i].ParentId}'/><span>{lstChild[i].PageName}</span></li>");
                    var item = page.ListChildren[i];
                    page.ListChildren[i] = item;
                    GetChild(ref appendPage, ref item);
                }
                appendPage.Append("</ul>");
            }
        }





        private void PopulateControls()
        {
            //lblPageName.Text = CurrentPage.PageName;
            heading.Text = string.Format(CultureInfo.InvariantCulture, Resource.ContentForPageFormat, CurrentPage.PageName);
            BindFeatureList();
        }

        private void BindFeatureList()
        {
            using (IDataReader reader = ModuleDefinition.GetUserModules(siteSettings.SiteId))
            {
                ListItem listItem;
                while (reader.Read())
                {
                    string allowedRoles = reader["AuthorizedRoles"].ToString();
                    if (WebUser.IsInRoles(allowedRoles))
                    {
                        listItem = new ListItem(
                            ResourceHelper.GetResourceString(
                            reader["ResourceFile"].ToString(),
                            reader["FeatureName"].ToString()),
                            reader["ModuleDefID"].ToString());
                        moduleType.Items.Add(listItem);
                    }
                }
            }
        }

        private ArrayList GetPaneModules(string pane)
        {
            ArrayList paneModules = new ArrayList();

            foreach (Module module in CurrentPage.Modules)
            {
                if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, pane))
                {
                    paneModules.Add(module);
                }

                if (!pageHasTopContent)
                {
                    if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "topPane"))
                    {
                        paneModules.Add(module);
                    }
                }

                if (!pageHasBottomContent)
                {
                    if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "bottomPane"))
                    {
                        paneModules.Add(module);
                    }
                }
            }
            return paneModules;
        }

        private void BindPaneModules(ListControl listControl, string pane)
        {

            foreach (Module module in CurrentPage.Modules)
            {
                if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, pane))
                {
                    ListItem listItem = new ListItem(module.ModuleTitle.Coalesce(Resource.ContentNoTitle), module.ModuleId.ToInvariantString());
                    listControl.Items.Add(listItem);
                }
            }
        }


        private void OrderModules(ArrayList list)
        {
            int i = 1;
            list.Sort();

            foreach (Module m in list)
            {
                // number the items 1, 3, 5, etc. to provide an empty order
                // number when moving items up and down in the list.
                m.ModuleOrder = i;
                i += 2;
            }
        }

        void btnAddExisting_Click(object sender, ImageClickEventArgs e)
        {
            int moduleId = -1;
            if (int.TryParse(hdnModuleID.Value, out moduleId))
            {
                Module m = new Module(moduleId);
                if (m.SiteId == siteSettings.SiteId)
                {
                    Module.Publish(
                        CurrentPage.PageGuid,
                        m.ModuleGuid,
                        m.ModuleId,
                        CurrentPage.PageId,
                        ddPaneNames.SelectedValue,
                        999,
                        DateTime.UtcNow,
                        DateTime.MinValue);

                    globalContentCount = Module.GetGlobalCount(siteSettings.SiteId, -1, pageID);
                    // lnkContentLookup.Visible = ((globalContentCount > 0) && !WebConfigSettings.DisableGlobalContent);
                    lnkGlobalContent.Visible = ((globalContentCount > 0) && !WebConfigSettings.DisableGlobalContent);

                    CurrentPage.RefreshModules();

                    ArrayList modules = GetPaneModules(m.PaneName);
                    OrderModules(modules);

                    foreach (Module item in modules)
                    {
                        Module.UpdateModuleOrder(pageID, item.ModuleId, item.ModuleOrder, m.PaneName);
                    }

                    CurrentPage.RefreshModules();

                    if (m.IncludeInSearch)
                    {
                        mojoPortal.SearchIndex.IndexHelper.RebuildPageIndexAsync(CurrentPage);
                    }

                    upLayout.Update();
                }


            }

        }

        private void btnCreateNewContent_Click(Object sender, EventArgs e)
        {
            Page.Validate("pagelayout");
            if (!Page.IsValid) { return; }

            int moduleDefID = int.Parse(moduleType.SelectedItem.Value);
            ModuleDefinition moduleDefinition = new ModuleDefinition(moduleDefID);
            var listPage = hdfPages.Value.ToListInt(',');
            if (listPage != null && listPage.Count() > 0)
            {
                foreach (var pageId in listPage)
                {
                    Module m = new Module();
                    m.SiteId = siteSettings.SiteId;
                    m.SiteGuid = siteSettings.SiteGuid;
                    m.ModuleDefId = moduleDefID;
                    m.FeatureGuid = moduleDefinition.FeatureGuid;
                    m.Icon = moduleDefinition.Icon;
                    m.CacheTime = moduleDefinition.DefaultCacheTime;
                    m.PageId = pageId;
                    m.ModuleTitle = moduleTitle.Text;
                    m.PaneName = ddPaneNames.SelectedValue;
                    //m.AuthorizedEditRoles = "Admins";
                    SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
                    if (currentUser != null)
                    {
                        m.CreatedByUserId = currentUser.UserId;
                    }
                    m.ShowTitle = WebConfigSettings.ShowModuleTitlesByDefault;
                    m.HeadElement = WebConfigSettings.ModuleTitleTag;
                    m.Save();
                    CurrentPage.RefreshModules();

                    ArrayList modules = GetPaneModules(m.PaneName);
                    OrderModules(modules);

                    foreach (Module item in modules)
                    {
                        Module.UpdateModuleOrder(pageID, item.ModuleId, item.ModuleOrder, m.PaneName);
                    }
                }
            }

            //WebUtils.SetupRedirect(this, Request.RawUrl);
            //return;

            CurrentPage.RefreshModules();
            ScriptManager.RegisterStartupScript(upLayout, this.GetType(), "NotifyPageSuccess", "NotifyPageSuccess()", true);
            upLayout.Update();
        }



        private void EditBtn_Click(Object sender, ImageClickEventArgs e)
        {
            string pane = ((ImageButton)sender).CommandArgument;
            ListBox _listbox = (ListBox)this.MPContent.FindControl(pane);

            if (_listbox.SelectedIndex != -1)
            {
                int mid = Int32.Parse(_listbox.SelectedItem.Value, CultureInfo.InvariantCulture);

                WebUtils.SetupRedirect(this, SiteRoot + "/Admin/ModuleSettings.aspx?mid=" + mid + "&pageid=" + pageID);
            }
        }


        private void DeleteBtn_Click(Object sender, ImageClickEventArgs e)
        {
            if (sender == null) return;

            string pane = ((ImageButton)sender).CommandArgument;
            ListBox listbox = (ListBox)this.MPContent.FindControl(pane);

            if (listbox.SelectedIndex != -1)
            {

                int mid = Int32.Parse(listbox.SelectedItem.Value);

                Module m = new Module(mid);

                if (WebConfigSettings.LogIpAddressForContentDeletions)
                {
                    string userName = string.Empty;
                    SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
                    if (currentUser != null)
                    {
                        userName = currentUser.Name;
                    }

                    log.Info("user " + userName + " removed module " + m.ModuleTitle + " from page " + CurrentPage.PageName + " from ip address " + SiteUtils.GetIP4Address());

                }

                Module.DeleteModuleInstance(mid, pageID);
                mojoPortal.SearchIndex.IndexHelper.RebuildPageIndexAsync(new PageSettings(siteSettings.SiteId, pageID));

            }

            globalContentCount = Module.GetGlobalCount(siteSettings.SiteId, -1, pageID);
            //lnkContentLookup.Visible = ((globalContentCount > 0) && !WebConfigSettings.DisableGlobalContent);
            lnkGlobalContent.Visible = ((globalContentCount > 0) && !WebConfigSettings.DisableGlobalContent);
            CurrentPage.RefreshModules();
            upLayout.Update();
        }

        protected Collection<DictionaryEntry> PaneList()
        {
            Collection<DictionaryEntry> paneList = new Collection<DictionaryEntry>();
            paneList.Add(new DictionaryEntry(Resource.ContentManagerCenterColumnLabel, "contentpane"));
            paneList.Add(new DictionaryEntry(Resource.ContentManagerLeftColumnLabel, "leftpane"));
            paneList.Add(new DictionaryEntry(Resource.ContentManagerRightColumnLabel, "rightpane"));

            if (pageHasTopContent)
            {
                paneList.Add(new DictionaryEntry(Resource.PageLayoutAltPanel1Label, "toppane"));
            }

            if (pageHasBottomContent)
            {
                paneList.Add(new DictionaryEntry(Resource.PageLayoutAltPanel2Label, "bottompane"));

            }

            return paneList;
        }



        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, Resource.PageLayoutPageTitle);

            btnCreateNewContent.Text = Resource.ContentManagerCreateNewContentButton;
            btnCreateNewContent.ToolTip = Resource.ContentManagerCreateNewContentButton;

            SiteUtils.SetButtonAccessKey
                (btnCreateNewContent, AccessKeys.ContentManagerCreateNewContentButtonAccessKey);

            UIHelper.AddConfirmationDialog(btnCreateNewContent, "Cảnh báo module sẽ được cắm vào toàn bộ các page bạn đã chọn, bạn có chắc chắn?");

            lnkEditSettings.Text = Resource.PageLayoutEditSettingsLink;
            lnkEditSettings.ToolTip = Resource.PageLayoutEditSettingsLink;
            lnkViewPage.Text = Resource.PageViewPageLink;
            lnkViewPage.ToolTip = Resource.PageViewPageLink;


            if (!Page.IsPostBack)
            {
                if (WebConfigSettings.PrePopulateDefaultContentTitle)
                {
                    moduleTitle.Text = Resource.PageLayoutDefaultNewModuleName;
                }
            }

            if (!Page.IsPostBack)
            {
                ddPaneNames.DataSource = PaneList();
                ddPaneNames.DataBind();
            }

            lnkPageTree.Visible = WebUser.IsAdminOrContentAdmin;
            lnkPageTree.Text = Resource.AdminMenuPageTreeLink;
            lnkPageTree.ToolTip = Resource.AdminMenuPageTreeLink;
            lnkPageTree.NavigateUrl = SiteRoot + WebConfigSettings.PageTreeRelativeUrl;
            btnAddExisting.ImageUrl = "~/Data/SiteImages/1x1.gif";
            btnAddExisting.Attributes.Add("tabIndex", "-1");

            lnkGlobalContent.Text = Resource.AddExistingContent;
            lnkGlobalContent.ToolTip = Resource.AddExistingContent;
            lnkGlobalContent.Visible = ((globalContentCount > 0) && !WebConfigSettings.DisableGlobalContent);
            lnkGlobalContent.NavigateUrl = SiteRoot + "/Dialog/GlobalContentDialog.aspx?pageid=" + pageID.ToInvariantString();

            reqModuleTitle.ErrorMessage = Resource.TitleRequiredWarning;
            reqModuleTitle.Enabled = WebConfigSettings.RequireContentTitle;

            cvModuleTitle.ValueToCompare = Resource.PageLayoutDefaultNewModuleName;
            cvModuleTitle.ErrorMessage = Resource.DefaultContentTitleWarning;
            cvModuleTitle.Enabled = WebConfigSettings.RequireChangeDefaultContentTitle;

        }


        private void LoadSettings()
        {
            pageID = WebUtils.ParseInt32FromQueryString("pageid", -1);
            isSiteEditor = SiteUtils.UserIsSiteEditor();
            pageHasTopContent = this.ContainsPlaceHolder("topContent");
            pageHasBottomContent = this.ContainsPlaceHolder("bottomContent");

            if (pageID > -1)
            {
                pnlContent.Visible = true;

                lnkEditSettings.NavigateUrl = SiteRoot + "/Admin/PageSettings.aspx?pageid=" + pageID.ToString();

                if (CurrentPage != null)
                {
                    lnkViewPage.NavigateUrl = SiteUtils.GetCurrentPageUrl();
                    if (CurrentPage.BodyCssClass.Length > 0) { AddClassToBody(CurrentPage.BodyCssClass); }
                }
                else
                {
                    lnkViewPage.Visible = false;
                }

            }

            AddClassToBody("administration");
            AddClassToBody("pagelayout");

            if (ScriptController != null)
            {
                ScriptController.RegisterAsyncPostBackControl(btnCreateNewContent);
            }

            globalContentCount = Module.GetGlobalCount(siteSettings.SiteId, -1, pageID);

            try
            {
                // this keeps the action from changing during ajax postback in folder based sites
                SiteUtils.SetFormAction(Page, Request.RawUrl);
            }
            catch (MissingMethodException)
            {
                //this method was introduced in .NET 3.5 SP1
            }

        }
    }
}

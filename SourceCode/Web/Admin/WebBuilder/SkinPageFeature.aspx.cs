/// Author:                     Joe Audette
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

namespace mojoPortal.Web.AdminUI
{

    public partial class SkinPageFeature : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PageModuleManager));

        private bool canEdit = false;
        private bool isSiteEditor = false;
        private int siteID = -1;
        private int skinPageId = 1;
        private int skins = 1;
        private bool pageHasAltContent1 = false;
        private bool pageHasAltContent2 = false;
        protected string EditSettingsImage = WebConfigSettings.EditPropertiesImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        private int globalContentCount = 0;
        private ArrayList allModules = new ArrayList();

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

            this.LeftUpBtn.Click += new ImageClickEventHandler(LeftUpBtn_Click);
            this.LeftDownBtn.Click += new ImageClickEventHandler(LeftDownBtn_Click);
            this.ContentUpBtn.Click += new ImageClickEventHandler(ContentUpBtn_Click);
            this.ContentDownBtn.Click += new ImageClickEventHandler(ContentDownBtn_Click);
            this.RightUpBtn.Click += new ImageClickEventHandler(RightUpBtn_Click);
            this.RightDownBtn.Click += new ImageClickEventHandler(RightDownBtn_Click);

            this.btnTopUp.Click += new ImageClickEventHandler(btnTopUp_Click);
            this.btnTopDown.Click += new ImageClickEventHandler(btnTopDown_Click);
            this.btnBottomUp.Click += new ImageClickEventHandler(btnBottomUp_Click);
            this.btnBottomDown.Click += new ImageClickEventHandler(btnBottomDown_Click);


            this.LeftDeleteBtn.Click += new ImageClickEventHandler(this.DeleteBtn_Click);
            this.ContentDeleteBtn.Click += new ImageClickEventHandler(this.DeleteBtn_Click);
            this.RightDeleteBtn.Click += new ImageClickEventHandler(this.DeleteBtn_Click);
            this.btnTopDelete.Click += new ImageClickEventHandler(this.DeleteBtn_Click);
            this.btnBottomDelete.Click += new ImageClickEventHandler(this.DeleteBtn_Click);


            this.LeftRightBtn.Click += new ImageClickEventHandler(LeftRightBtn_Click);
            this.ContentLeftBtn.Click += new ImageClickEventHandler(ContentLeftBtn_Click);
            this.ContentRightBtn.Click += new ImageClickEventHandler(ContentRightBtn_Click);
            this.RightLeftBtn.Click += new ImageClickEventHandler(RightLeftBtn_Click);

            this.btnTopCenter.Click += new ImageClickEventHandler(btnTopCenter_Click);
            this.btnBottomCenter.Click += new ImageClickEventHandler(btnBottomCenter_Click);
            //this.btnMoveAlt1ToAlt2.Click += new ImageClickEventHandler(btnMoveAlt1ToAlt2_Click);
            //this.btnMoveAlt2ToAlt1.Click += new ImageClickEventHandler(btnMoveAlt2ToAlt1_Click);


            this.ContentDownToNextButton.Click += new ImageClickEventHandler(ContentDownToNextButton_Click);
            this.ContentUpToNextButton.Click += new ImageClickEventHandler(ContentUpToNextButton_Click);

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
            LoadParameter();
            LoadSettings();
            //siteSettings = CacheHelper.GetCurrentSiteSettings();

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

            //if ((!canEdit) || (siteID != CurrentPage.SiteId))
            if (!canEdit)
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            PopulateLabels();
            //SetupExistingContentScript();

            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void LoadParameter()
        {
            skins = WebUtils.ParseInt32FromQueryString("skins", skins);
            skinPageId = WebUtils.ParseInt32FromQueryString("skinpageid", skinPageId);
        }
        private void PopulateControls()
        {
            //lblPageName.Text = CurrentPage.PageName;
            //heading.Text = string.Format(CultureInfo.InvariantCulture, Resource.ContentForPageFormat, CurrentPage.PageName);
            if (siteID > -1)
            {
                BindFeatureList();
                BindPanes();
            }

        }

        private void BindPanes()
        {
            leftPane.Items.Clear();
            contentPane.Items.Clear();
            rightPane.Items.Clear();
            topPane.Items.Clear();
            bottomPane.Items.Clear();

            BindPaneModules(leftPane, "leftPane");
            BindPaneModules(contentPane, "contentPane");
            BindPaneModules(rightPane, "rightPane");
            BindPaneModules(topPane, "topPane");
            BindPaneModules(bottomPane, "bottomPane");

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

        private ArrayList GetModule4Site()
        {
            ArrayList listModules = new ArrayList();
            using (IDataReader reader = CoreSkinPageDefault.GetSkinPageDefaultForPage(skinPageId))
            {
                while (reader.Read())
                {
                    CoreSkinPageDefault m = new CoreSkinPageDefault();
                    m.ItemID = Convert.ToInt32(reader["ItemID"]);
                    m.ModuleID = Convert.ToInt32(reader["ModuleID"]);
                    m.PaneName = reader["PaneName"].ToString();
                    m.ModuleTitle = reader["DefautlTitle"].ToString();
                    m.ModuleOrder = Convert.ToInt32(reader["ModuleOrder"]);
                    m.SiteID = Convert.ToInt32(reader["SiteID"]);
                    m.SkinID = Convert.ToInt32(reader["SkinID"]);
                    m.SkinPageID = Convert.ToInt32(reader["SkinPageID"]);
                    listModules.Add(m);
                }
            }
            return listModules;
        }
        private ArrayList GetPaneModules(string pane)
        {
            ArrayList paneModules = new ArrayList();
            foreach (CoreSkinPageDefault module in allModules)
            {
                if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, pane))
                {
                    paneModules.Add(module);
                }
            }

            return paneModules;
        }

        private void BindPaneModules(ListControl listControl, string pane)
        {

            foreach (CoreSkinPageDefault module in allModules)
            {
                if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, pane))
                {
                    ListItem listItem = new ListItem(module.ModuleTitle.Coalesce(Resource.ContentNoTitle), module.ItemID.ToInvariantString());
                    listControl.Items.Add(listItem);
                }
            }

        }


        private void OrderModules(ArrayList list)
        {
            int i = 1;
            list.Sort();

            foreach (CoreSkinPageDefault m in list)
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

                    //globalContentCount = Module.GetGlobalCount(siteSettings.SiteId, -1, pageID);

                    lnkGlobalContent.Visible = ((globalContentCount > 0) && !WebConfigSettings.DisableGlobalContent);

                    CurrentPage.RefreshModules();

                    ArrayList modules = GetPaneModules(m.PaneName);
                    OrderModules(modules);

                    //foreach (Module item in modules)
                    //{
                    //    Module.UpdateModuleOrder(pageID, item.ModuleId, item.ModuleOrder, m.PaneName);
                    //}

                    CurrentPage.RefreshModules();

                    if (m.IncludeInSearch)
                    {
                        mojoPortal.SearchIndex.IndexHelper.RebuildPageIndexAsync(CurrentPage);
                    }

                    BindPanes();
                    upLayout.Update();
                }


            }

        }

        private void btnCreateNewContent_Click(Object sender, EventArgs e)
        {
            Page.Validate("pagelayout");
            if (!Page.IsValid) { return; }

            int moduleID = int.Parse(moduleType.SelectedItem.Value);

            CoreSkinPageDefault dmp = new CoreSkinPageDefault();
            dmp.SiteID = siteSettings.SiteId;
            dmp.ModuleID = moduleID;
            dmp.ModuleTitle = moduleTitle.Text;
            dmp.PaneName = ddPaneNames.SelectedValue;
            dmp.SkinPageID = skinPageId;
            //dmp.SkinID = skin;
            dmp.Save();


            //CurrentPage.RefreshModules();
            allModules = CoreSkinPageDefault.RefreshDefaultModules(dmp.SkinPageID);

            #region  Save module for skin page module of web builder
            ModuleDefinition moduleDefinition = new ModuleDefinition(moduleID);
            Module module = new Module();
            module.SkinPageModule = dmp.ItemID;
            module.SiteId = siteSettings.SiteId;
            module.SiteGuid = siteSettings.SiteGuid;
            module.ModuleDefId = moduleDefinition.ModuleDefId;
            module.FeatureGuid = moduleDefinition.FeatureGuid;
            module.Icon = moduleDefinition.Icon;
            module.CacheTime = moduleDefinition.DefaultCacheTime;
            module.ModuleTitle = moduleTitle.Text;
            module.ControlSource = moduleDefinition.ControlSrc;
            //m.AuthorizedEditRoles = "Admins";
            SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
            if (currentUser != null)
            {
                module.CreatedByUserId = currentUser.UserId;
            }
            module.ShowTitle = WebConfigSettings.ShowModuleTitlesByDefault;
            module.HeadElement = WebConfigSettings.ModuleTitleTag;
            module.SaveForSkinPageModule();
            #endregion


            ArrayList modules = GetPaneModules(dmp.PaneName);
            OrderModules(modules);

            foreach (CoreSkinPageDefault item in modules)
            {
                CoreSkinPageDefault.UpdateDefaultModuleOrder(dmp.ItemID, item.ModuleOrder, dmp.PaneName);
            }

            allModules = CoreSkinPageDefault.RefreshDefaultModules(dmp.SkinPageID);
            BindPanes();
            upLayout.Update();
        }

        #region Move Up or Down
        void RightUpBtn_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(rightPane, pane, direction);

        }
        void RightDownBtn_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(rightPane, pane, direction);

        }
        void btnTopUp_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(topPane, pane, direction);
        }

        void btnTopDown_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(topPane, pane, direction);

        }
        void btnBottomUp_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(bottomPane, pane, direction);
        }
        void btnBottomDown_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(bottomPane, pane, direction);

        }
        void ContentUpBtn_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(contentPane, pane, direction);
        }
        void ContentDownBtn_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(contentPane, pane, direction);

        }

        void LeftUpBtn_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(leftPane, pane, direction);

        }
        void LeftDownBtn_Click(object sender, ImageClickEventArgs e)
        {
            string direction = ((ImageButton)sender).CommandName;
            string pane = ((ImageButton)sender).CommandArgument;
            MoveUpDown(leftPane, pane, direction);
        }

        private void MoveUpDown(ListBox listbox, string pane, string direction)
        {
            ArrayList modules = GetPaneModules(pane);
            CoreSkinPageDefault dmp = null;
            if (listbox.SelectedIndex != -1)
            {
                int delta;
                int selection = -1;

                // Determine the delta to apply in the order number for the module
                // within the list.  +3 moves down one item; -3 moves up one item

                if (direction == "down")
                {
                    delta = 3;
                    if (listbox.SelectedIndex < listbox.Items.Count - 1)
                        selection = listbox.SelectedIndex + 1;
                }
                else
                {
                    delta = -3;
                    if (listbox.SelectedIndex > 0)
                        selection = listbox.SelectedIndex - 1;
                }


                dmp = (CoreSkinPageDefault)modules[listbox.SelectedIndex];
                dmp.ModuleOrder += delta;

                OrderModules(modules);

                //foreach (Module item in modules)
                //{
                //    Module.UpdateModuleOrder(pageID, item.ModuleId, item.ModuleOrder, pane);
                //}
                foreach (CoreSkinPageDefault item in modules)
                {
                    CoreSkinPageDefault.UpdateDefaultModuleOrder(item.ItemID, item.ModuleOrder, pane);
                }
            }

            allModules = CoreSkinPageDefault.RefreshDefaultModules(skinPageId);
            //CurrentPage.RefreshModules();
            BindPanes();
            if (dmp != null)
            {
                SelectDefaultModule(dmp, pane);
            }
            upLayout.Update();
        }

        #endregion

        #region Move To Pane


        void LeftRightBtn_Click(object sender, ImageClickEventArgs e)
        {
            string sourcePane = "leftPane";
            string targetPane = "contentPane";
            MoveContent(leftPane, sourcePane, targetPane);

        }


        void ContentLeftBtn_Click(object sender, ImageClickEventArgs e)
        {
            string sourcePane = "contentPane";
            string targetPane = "leftPane";
            MoveContent(contentPane, sourcePane, targetPane);

        }

        void ContentRightBtn_Click(object sender, ImageClickEventArgs e)
        {
            string sourcePane = "contentPane";
            string targetPane = "rightPane";
            MoveContent(contentPane, sourcePane, targetPane);

        }


        void RightLeftBtn_Click(object sender, ImageClickEventArgs e)
        {
            string sourcePane = "rightPane";
            string targetPane = "contentPane";
            MoveContent(rightPane, sourcePane, targetPane);

        }



        void ContentDownToNextButton_Click(object sender, ImageClickEventArgs e)
        {
            string sourcePane = "contentPane";
            string targetPane = "bottomPane";
            MoveContent(contentPane, sourcePane, targetPane);

        }

        void ContentUpToNextButton_Click(object sender, ImageClickEventArgs e)
        {
            string sourcePane = "contentPane";
            string targetPane = "topPane";
            MoveContent(contentPane, sourcePane, targetPane);

        }
        void btnTopCenter_Click(object sender, ImageClickEventArgs e)
        {
            string sourcePane = "topPane";
            string targetPane = "contentPane";
            MoveContent(topPane, sourcePane, targetPane);

        }

        void btnBottomCenter_Click(object sender, ImageClickEventArgs e)
        {
            string sourcePane = "bottomPane";
            string targetPane = "contentPane";
            MoveContent(bottomPane, sourcePane, targetPane);

        }




        private void MoveContent(ListBox listBox, string sourcePane, string targetPane)
        {

            if (listBox.SelectedIndex != -1)
            {
                ArrayList sourceList = GetPaneModules(sourcePane);

                //Module m = (Module)sourceList[listBox.SelectedIndex];
                //Module.UpdateModuleOrder(pageID, m.ModuleId, 998, targetPane);

                CoreSkinPageDefault m = (CoreSkinPageDefault)sourceList[listBox.SelectedIndex];

                CoreSkinPageDefault.UpdateDefaultModuleOrder(m.ItemID, 998, targetPane);

                allModules = CoreSkinPageDefault.RefreshDefaultModules(skinPageId);

                ArrayList modulesSource = GetPaneModules(sourcePane);
                OrderModules(modulesSource);

                foreach (CoreSkinPageDefault item in modulesSource)
                {
                    CoreSkinPageDefault.UpdateDefaultModuleOrder(m.ItemID, item.ModuleOrder, sourcePane);
                }

                ArrayList modulesTarget = GetPaneModules(targetPane);
                OrderModules(modulesTarget);

                foreach (CoreSkinPageDefault item in modulesTarget)
                {
                    CoreSkinPageDefault.UpdateDefaultModuleOrder(m.ItemID, item.ModuleOrder, targetPane);
                }

                BindPanes();
                SelectDefaultModule(m, targetPane);
                upLayout.Update();
            }
        }


        #endregion

        private void SelectModule(Module m, string paneName)
        {
            ListBox listbox = null;
            switch (paneName.ToLower())
            {
                case "leftpane":
                    listbox = leftPane;
                    break;

                case "rightpane":
                    listbox = rightPane;
                    break;

                case "contentpane":
                    listbox = contentPane;
                    break;

                case "toppane":
                    listbox = topPane;
                    break;

                case "bottompane":
                    listbox = bottomPane;
                    break;

            }

            if (listbox != null)
            {
                ListItem item = listbox.Items.FindByValue(m.ModuleId.ToInvariantString());
                if (item != null)
                {
                    listbox.ClearSelection();
                    item.Selected = true;
                }
            }
        }

        private void SelectDefaultModule(CoreSkinPageDefault m, string paneName)
        {
            ListBox listbox = null;
            switch (paneName.ToLower())
            {
                case "leftpane":
                    listbox = leftPane;
                    break;

                case "rightpane":
                    listbox = rightPane;
                    break;

                case "contentpane":
                    listbox = contentPane;
                    break;

                case "toppane":
                    listbox = topPane;
                    break;

                case "bottompane":
                    listbox = bottomPane;
                    break;

            }

            if (listbox != null)
            {
                ListItem item = listbox.Items.FindByValue(m.ItemID.ToInvariantString());
                if (item != null)
                {
                    listbox.ClearSelection();
                    item.Selected = true;
                }
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

                CoreSkinPageDefault m = new CoreSkinPageDefault(mid);

                if (WebConfigSettings.LogIpAddressForContentDeletions)
                {
                    string userName = string.Empty;
                    SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
                    if (currentUser != null)
                    {
                        userName = currentUser.Name;
                    }

                    log.Info("user " + userName + " removed default module " + m.ModuleTitle + " from skin page default " + skinPageId + " from ip address " + SiteUtils.GetIP4Address());

                }

                CoreSkinPageDefault.DeleteDefaultModule(mid);
                //mojoPortal.SearchIndex.IndexHelper.RebuildPageIndexAsync(new PageSettings(siteSettings.SiteId, pageID));

            }

            //globalContentCount = Module.GetGlobalCount(siteSettings.SiteId, -1, pageID);
            lnkGlobalContent.Visible = ((globalContentCount > 0) && !WebConfigSettings.DisableGlobalContent);
            allModules = CoreSkinPageDefault.RefreshDefaultModules(skinPageId);
            BindPanes();
            upLayout.Update();
        }

        protected Collection<DictionaryEntry> PaneList()
        {
            Collection<DictionaryEntry> paneList = new Collection<DictionaryEntry>();
            paneList.Add(new DictionaryEntry(Resource.ContentManagerCenterColumnLabel, "contentpane"));
            paneList.Add(new DictionaryEntry(Resource.ContentManagerLeftColumnLabel, "leftpane"));
            paneList.Add(new DictionaryEntry(Resource.ContentManagerRightColumnLabel, "rightpane"));
            paneList.Add(new DictionaryEntry(Resource.ContentManagerTopColumnLabel, "toppane"));
            paneList.Add(new DictionaryEntry(Resource.ContentManagerBottomColumnLabel, "bottompane"));
            return paneList;
        }



        private void PopulateLabels()
        {
            //Title = SiteUtils.FormatPageTitle(siteSettings, Resource.PageLayoutPageTitle);

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkWebBuilder.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderMenu.aspx";
            lnkWebBuilder.Text = "Xây dựng website";

            lnkSkinManager.NavigateUrl = SiteRoot + "/Admin/WebBuilder/SkinManager.aspx";
            lnkSkinManager.Text = "Quản trị template website";

            lnkSkinPageManager.NavigateUrl = string.Format("{0}/Admin/WebBuilder/SkinPageManager.aspx?skins={1}", SiteRoot, skins);
            lnkSkinPageManager.Text = "Quản trị trang thuộc template website";

            var skinPage = new CoreSkinPage(skinPageId);
            if (skinPage != null)
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, "Thiết lập tính năng trang - " + skinPage.Title);
                lnkCurrentPage.Text = skinPage.Title;
                //lnkCurrentPage.NavigateUrl = string.Format("{0}/Admin/SkinPageManager.aspx?skins={1}&skinpageid={2}", SiteRoot, skins, skinPageId);
                heading.Text = string.Format("Thiết lập tính năng mặc định trang - {0}", skinPage.Title);
            }
            else
            {

                heading.Text = string.Format(CultureInfo.InvariantCulture, "Thiết lập tính năng mặc định", CurrentPage.PageName);

                Title = SiteUtils.FormatPageTitle(siteSettings, "Thiết lập tính năng trang");

                lnkCurrentPage.Text = "Thiết lập tính năng mặc định";
                //lnkCurrentPage.NavigateUrl = string.Format("{0}/Admin/SkinPageManager.aspx?skins={1}&skinpageid={2}", SiteRoot, skins, skinPageId);
            }



            btnCreateNewContent.Text = Resource.ContentManagerCreateNewContentButton;
            btnCreateNewContent.ToolTip = Resource.ContentManagerCreateNewContentButton;

            SiteUtils.SetButtonAccessKey
                (btnCreateNewContent, AccessKeys.ContentManagerCreateNewContentButtonAccessKey);

            LeftUpBtn.AlternateText = Resource.PageLayoutLeftUpAlternateText;
            LeftUpBtn.ToolTip = Resource.PageLayoutLeftUpAlternateText;

            LeftRightBtn.AlternateText = Resource.PageLayoutLeftRightAlternateText;
            LeftRightBtn.ToolTip = Resource.PageLayoutLeftRightAlternateText;

            LeftDownBtn.AlternateText = Resource.PageLayoutLeftDownAlternateText;
            LeftDownBtn.ToolTip = Resource.PageLayoutLeftDownAlternateText;


            LeftDeleteBtn.AlternateText = Resource.PageLayoutLeftDeleteAlternateText;
            LeftDeleteBtn.ToolTip = Resource.PageLayoutLeftDeleteAlternateText;
            LeftDeleteBtn.ImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            UIHelper.AddConfirmationDialog(LeftDeleteBtn, Resource.PageLayoutRemoveContentWarning);

            ContentUpBtn.AlternateText = Resource.PageLayoutContentUpAlternateText;
            ContentUpBtn.ToolTip = Resource.PageLayoutContentUpAlternateText;

            ContentLeftBtn.AlternateText = Resource.PageLayoutContentLeftAlternateText;
            ContentLeftBtn.ToolTip = Resource.PageLayoutContentLeftAlternateText;

            ContentRightBtn.AlternateText = Resource.PageLayoutContentRightAlternateText;
            ContentRightBtn.ToolTip = Resource.PageLayoutContentRightAlternateText;

            ContentDownBtn.AlternateText = Resource.PageLayoutContentDownAlternateText;
            ContentDownBtn.ToolTip = Resource.PageLayoutContentDownAlternateText;

            ContentDeleteBtn.AlternateText = Resource.PageLayoutContentDeleteAlternateText;
            ContentDeleteBtn.ToolTip = Resource.PageLayoutContentDeleteAlternateText;
            ContentDeleteBtn.ImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            UIHelper.AddConfirmationDialog(ContentDeleteBtn, Resource.PageLayoutRemoveContentWarning);

            RightUpBtn.AlternateText = Resource.PageLayoutRightUpAlternateText;
            RightUpBtn.ToolTip = Resource.PageLayoutRightUpAlternateText;

            RightLeftBtn.AlternateText = Resource.PageLayoutRightLeftAlternateText;
            RightLeftBtn.ToolTip = Resource.PageLayoutRightLeftAlternateText;

            RightDownBtn.AlternateText = Resource.PageLayoutRightDownAlternateText;
            RightDownBtn.ToolTip = Resource.PageLayoutRightDownAlternateText;

            RightDeleteBtn.AlternateText = Resource.PageLayoutRightDeleteAlternateText;
            RightDeleteBtn.ToolTip = Resource.PageLayoutRightDeleteAlternateText;
            RightDeleteBtn.ImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            UIHelper.AddConfirmationDialog(RightDeleteBtn, Resource.PageLayoutRemoveContentWarning);

            litAltLayoutNotice.Text = Resource.PageLayoutAltPanelInfo;

            if (!Page.IsPostBack)
            {
                //moduleTitle.Text = "Thiết lập tính năng trang";
                //CoreSkinPage corePage = new CoreSkinPage(skinPageId);
                //if (corePage != null)
                //{
                //    //moduleTitle.Text = string.Format("Thiết lập tính năng trang - {0}", corePage.Title);



                //}

                //if (WebConfigSettings.PrePopulateDefaultContentTitle)
                //{
                //    moduleTitle.Text = Resource.PageLayoutDefaultNewModuleName;
                //}
            }

            if (!Page.IsPostBack)
            {
                ddPaneNames.DataSource = PaneList();
                ddPaneNames.DataBind();
            }

            ContentDownToNextButton.AlternateText = Resource.PageLayoutMoveCenterToAlt2Button;
            ContentDownToNextButton.ToolTip = Resource.PageLayoutMoveCenterToAlt2Button;
            ContentUpToNextButton.AlternateText = Resource.PageLayoutMoveCenterToAlt1Button;
            ContentUpToNextButton.ToolTip = Resource.PageLayoutMoveCenterToAlt1Button;

            btnTopCenter.AlternateText = Resource.PageLayoutMoveAltToCenterButton;
            btnTopCenter.ToolTip = Resource.PageLayoutMoveAltToCenterButton;

            btnBottomUp.AlternateText = Resource.PageLayoutAlt2MoveUpButton;
            btnBottomUp.ToolTip = Resource.PageLayoutAlt2MoveUpButton;

            btnTopUp.AlternateText = Resource.PageLayoutAlt1MoveUpButton;
            btnTopUp.ToolTip = Resource.PageLayoutAlt1MoveUpButton;

            btnTopDown.AlternateText = Resource.PageLayoutAlt1MoveDownButton;
            btnTopDown.ToolTip = Resource.PageLayoutAlt1MoveDownButton;

            //btnMoveAlt1ToAlt2.AlternateText = Resource.PageLayoutMoveAlt1ToAlt2Button;
            //btnMoveAlt1ToAlt2.ToolTip = Resource.PageLayoutMoveAlt1ToAlt2Button;

            btnTopDelete.AlternateText = Resource.PageLayoutAlt1DeleteButton;
            btnTopDelete.ToolTip = Resource.PageLayoutAlt1DeleteButton;
            btnTopDelete.ImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            UIHelper.AddConfirmationDialog(btnTopDelete, Resource.PageLayoutRemoveContentWarning);
            //btnMoveAlt2ToAlt1.AlternateText = Resource.PageLayoutMoveAlt2ToAlt1Button;
            //btnMoveAlt2ToAlt1.ToolTip = Resource.PageLayoutMoveAlt2ToAlt1Button;

            btnBottomCenter.AlternateText = Resource.PageLayoutMoveAltToCenterButton;
            btnBottomCenter.ToolTip = Resource.PageLayoutMoveAltToCenterButton;

            btnBottomUp.AlternateText = Resource.PageLayoutAlt2MoveUpButton;
            btnBottomUp.ToolTip = Resource.PageLayoutAlt2MoveUpButton;

            btnBottomDown.AlternateText = Resource.PageLayoutAlt2MoveDownButton;
            btnBottomDown.ToolTip = Resource.PageLayoutAlt2MoveDownButton;

            btnBottomDelete.AlternateText = Resource.PageLayoutAlt2DeleteButton;
            btnBottomDelete.ToolTip = Resource.PageLayoutAlt2DeleteButton;
            btnBottomDelete.ImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            UIHelper.AddConfirmationDialog(btnBottomDelete, Resource.PageLayoutRemoveContentWarning);

            lnkGlobalContent.Text = Resource.AddExistingContent;
            lnkGlobalContent.ToolTip = Resource.AddExistingContent;
            lnkGlobalContent.Visible = ((globalContentCount > 0) && !WebConfigSettings.DisableGlobalContent);
            //lnkGlobalContent.NavigateUrl = SiteRoot + "/Dialog/GlobalContentDialog.aspx?pageid=" + pageID.ToInvariantString();

            reqModuleTitle.ErrorMessage = Resource.TitleRequiredWarning;
            reqModuleTitle.Enabled = WebConfigSettings.RequireContentTitle;

            cvModuleTitle.ValueToCompare = Resource.PageLayoutDefaultNewModuleName;
            cvModuleTitle.ErrorMessage = Resource.DefaultContentTitleWarning;
            cvModuleTitle.Enabled = WebConfigSettings.RequireChangeDefaultContentTitle;

        }

        private void SetupExistingContentScript()
        {


            StringBuilder script = new StringBuilder();

            script.Append("\n<script type='text/javascript'>");

            script.Append("$('#" + lnkGlobalContent.ClientID + "').colorbox({width:\"80%\", height:\"80%\", iframe:true});");

            script.Append("function AddModule(moduleId) {");

            //script.Append("GB_hide();");
            //script.Append("alert(moduleId);");

            script.Append("var hdnUI = document.getElementById('" + this.hdnModuleID.ClientID + "'); ");
            script.Append("hdnUI.value = moduleId; ");


            //script.Append("var btn = document.getElementById('" + this.btnAddExisting.ClientID + "');  ");
            //script.Append("btn.click(); ");

            script.Append("$.colorbox.close(); ");

            script.Append("}");
            script.Append("</script>");


            ScriptManager.RegisterStartupScript(this, typeof(Page), "SelectContentHandler", script.ToString(), false);

        }

        private void LoadSettings()
        {
            //siteID = WebUtils.ParseInt32FromQueryString("siteid", -1);
            siteID = siteSettings.SiteId;
            isSiteEditor = SiteUtils.UserIsSiteEditor();
            pageHasAltContent1 = this.ContainsPlaceHolder("topContent");
            pageHasAltContent2 = this.ContainsPlaceHolder("bottomContent");

            allModules = GetModule4Site();

            if (siteID > -1)
            {
                pnlContent.Visible = true;

                //lnkEditSettings.NavigateUrl = SiteRoot + "/Admin/PageSettings.aspx?pageid=" + pageID.ToString();

                if (CurrentPage != null)
                {
                    if (CurrentPage.BodyCssClass.Length > 0) { AddClassToBody(CurrentPage.BodyCssClass); }
                }

            }

            AddClassToBody("administration");
            AddClassToBody("pagelayout");

            //globalContentCount = Module.GetGlobalCount(siteSettings.SiteId, -1, pageID);

            try
            {
                // this keeps the action from changing during ajax postback in folder based sites
                SiteUtils.SetFormAction(Page, Request.RawUrl);
            }
            catch (MissingMethodException)
            {
                //this method was introduced in .NET 3.5 SP1
            }

            if (ScriptController != null)
            {
                ScriptController.RegisterAsyncPostBackControl(btnCreateNewContent);

                ScriptController.RegisterAsyncPostBackControl(LeftUpBtn);
                ScriptController.RegisterAsyncPostBackControl(LeftDownBtn);
                ScriptController.RegisterAsyncPostBackControl(ContentUpBtn);
                ScriptController.RegisterAsyncPostBackControl(ContentDownBtn);
                ScriptController.RegisterAsyncPostBackControl(RightUpBtn);
                ScriptController.RegisterAsyncPostBackControl(RightDownBtn);
                ScriptController.RegisterAsyncPostBackControl(btnTopUp);
                ScriptController.RegisterAsyncPostBackControl(btnTopDown);
                ScriptController.RegisterAsyncPostBackControl(btnBottomUp);
                ScriptController.RegisterAsyncPostBackControl(btnBottomDown);



                ScriptController.RegisterAsyncPostBackControl(LeftDeleteBtn);
                ScriptController.RegisterAsyncPostBackControl(ContentDeleteBtn);
                ScriptController.RegisterAsyncPostBackControl(RightDeleteBtn);
                ScriptController.RegisterAsyncPostBackControl(btnTopDelete);
                ScriptController.RegisterAsyncPostBackControl(btnBottomDelete);
                ScriptController.RegisterAsyncPostBackControl(LeftRightBtn);
                ScriptController.RegisterAsyncPostBackControl(ContentLeftBtn);
                ScriptController.RegisterAsyncPostBackControl(ContentRightBtn);
                ScriptController.RegisterAsyncPostBackControl(RightLeftBtn);
                ScriptController.RegisterAsyncPostBackControl(btnTopCenter);
                ScriptController.RegisterAsyncPostBackControl(btnBottomCenter);
                //ScriptController.RegisterAsyncPostBackControl(btnMoveAlt2ToAlt1);
            }


        }

    }
}
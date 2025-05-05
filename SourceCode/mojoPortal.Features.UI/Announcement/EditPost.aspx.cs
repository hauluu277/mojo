using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System.Linq;
using Resources;
using System.Globalization;

namespace AnnouncementFeature.UI
{
   
    public partial class EditPost : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;

        protected String cacheDependencyKey;
        protected string virtualRoot;
        protected Double timeOffset;
        protected Hashtable moduleSettings;
        protected AnnouncementConfiguration config = new AnnouncementConfiguration();
        private const int pageSize = 10;
        private Guid restoreGuid = Guid.Empty;
        private md_Announcement _announcement;
        protected bool isAdmin;
        readonly ContentMetaRespository metaRepository = new ContentMetaRespository();
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();

        public SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();

        #region OnInt;
        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        protected override void OnInit(EventArgs e)
        {
            LoadParams();
            LoadSettings();
            //LoadPanels();
            base.OnInit(e);
            Load += Page_Load;

            btnUpdate.Click += btnUpdate_Click;
            //btnDeleteImg.Click += btnDeleteImg_Click;
            //SiteUtils.SetupEditor(edContent);

        }
        private void LoadParams()
        {
            itemId = WebUtils.ParseInt32FromQueryString("itemid", itemId);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
        }
        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new AnnouncementConfiguration(moduleSettings);
            if (itemId > 0)
            {
                _announcement = new md_Announcement(itemId);
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Request.IsAuthenticated) { SiteUtils.RedirectToLoginPage(this); return; }
            //if (!siteUser.IsInRoles("Admins") && !siteUser.IsInRoles(config.RoleSetting)) { SiteUtils.RedirectToAccessDeniedPage(this); return; }
            if (itemId > 0)
            {
                lblTitle.Text = "Cập nhật thông báo";
                Title = SiteUtils.FormatPageTitle(siteSetting, "Cập nhật thông báo");
            }
            else
            {
                lblTitle.Text = "Thêm mới thông báo";
                Title = SiteUtils.FormatPageTitle(siteSettings, "Thêm mới thông báo");
            }
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RoleSetting))
                {
                    lnkRecentList.Text = "Danh sách các thông báo";
                    lnkRecentList.NavigateUrl = SiteRoot + "/Announcement/ViewList.aspx?pageid=" + pageId + " &mid=" + moduleId;
                }
            }
            else
            {
                lnkRecentList.Text = "Danh sách các thông báo ";
                lnkRecentList.NavigateUrl = SiteRoot + "/Announcement/Mannage.aspx?pageid=" + pageId + " &mid=" + moduleId;
            }
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        protected virtual void PopulateControls()
        {
            DatePickerControlAnno.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            if (_announcement != null)
            {
                //txtFirstName.Text = _admission.FirstName.ToString();
                DatePickerControlAnno.Text = string.Format("{0:dd/MM/yyyy}", _announcement.DateAnno);
                edContentAnno.Text = _announcement.ContentAnno.ToString();


            }
        }
        protected virtual void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate("save_Announcement");
            if (!Page.IsValid) return;
            if (!Save()) return;
            string url = SiteRoot + "/Announcement/ViewList.aspx?pageid=" + pageId + "&mid=" + moduleId;
            SiteUtils.RedirectToUrl(url);
        }
        private bool ParamsAreValid()
        {
            try
            {
                DateTime.Parse(DatePickerControlAnno.Text);
            }
            catch (FormatException)
            {
                lblErrorMessage.Text = "Ngày sinh bạn nhập không đúng định dạng";
                return false;
            }
            catch (ArgumentNullException)
            {
                lblErrorMessage.Text = "Ngày sinh bạn nhập không đúng định dạng";
                return false;
            }
            return true;
        }
        private bool Save()
        {
            if (_announcement == null)
            {
                _announcement = new md_Announcement(itemId);
            }
            Module module = GetModule(moduleId);
            if (moduleId > 0)
            {
                _announcement.ModuleID = moduleId;
            }
            if (siteUser == null)
            {
                return false;
            }
            _announcement.SiteID = siteSetting.SiteId;
            //_admission.FirstName = txtFirstName.Text.ToString();
            DateTime localTime = DateTime.Parse(DatePickerControlAnno.Text, CultureInfo.CurrentCulture);
            _announcement.DateAnno = localTime;
            _announcement.ContentAnno = edContentAnno.Text.ToString();
            _announcement.Save();

            return true;
        }
    }
}
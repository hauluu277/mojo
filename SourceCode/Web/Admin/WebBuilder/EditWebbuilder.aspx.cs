// Author:					Joe Audette
// Created:				    2004-08-28
// Last Modified:			2014-01-10
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.
// 2010-12-18 modifications by Jamie Eubanks to better support ldap fallback
// 2011-03-01 improvements for multi site management accessibility, got rid of the autopostback dropdown now uses the SiteList.aspx page to select sites

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Business.WebHelpers.SiteCreatedEventHandlers;
using mojoPortal.Web.Editor;
using mojoPortal.Web.Framework;
using mojoPortal.Web.Controls.Captcha;
using mojoPortal.Web.UI;
using Resources;
using mojoPortal.Web.Components;
using System.Linq;
using static mojoPortal.Web.Components.SiteContants;
using mojoPortal.Business.WebHelpers.PageEventHandlers;

namespace mojoPortal.Web.AdminUI
{

    public partial class EditWebbuilder : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WebBuilder));
        private string logoPath = string.Empty;
        private bool sslIsAvailable = false;
        private bool IsServerAdmin = false;
        private int currentSiteID = 0;
        private Guid currentSiteGuid = Guid.Empty;
        private int selectedSiteID = -2;
        private bool isAdmin = false;
        private bool isContentAdmin = false;
        private bool allowPasswordFormatChange = false;
        private bool useFolderForSiteDetection = false;
        private SiteSettings selectedSite;
        protected string lastGroupValue = string.Empty;
        private bool enableSiteSettingsSmtpSettings = false;
        private bool maskSMTPPassword = true;
        private string requestedTab = string.Empty;
        protected string DeleteLinkImage = "~/Data/SiteImages/" + WebConfigSettings.DeleteLinkImage;
        private SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
        private int siteid = 0;

        //private bool siteIsCommerceEnabled = false;


        protected void Page_Load(object sender, EventArgs e)
        {

            if (SiteUtils.SslIsAvailable()) SiteUtils.ForceSsl();

            LoadSettings();

            if (!WebUser.IsInRoles(WebConfigSettings.RoleManagerCuocDieuTra))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }



            lnkAdminMenu.Text = "Trang chủ";
            lnkAdminMenu.ToolTip = "Trang chủ";
            lnkAdminMenu.NavigateUrl = SiteRoot + "/";

            UIHelper.AddConfirmationDialog(btnDelete, Resource.SiteSettingsDeleteWarning);

            lnkSiteList.Visible = true;
            lnkSiteList.Text = "Danh sách cuộc điều tra";
            lnkSiteList.NavigateUrl = SiteRoot + "/GlobalModule/Investigate/ManagerPost.aspx";
            litLinkSeparator2.Visible = lnkSiteList.Visible;

            hplBack.Text = "Quay lại";
            hplBack.NavigateUrl = SiteRoot + "/GlobalModule/Investigate/ManagerPost.aspx";

            //lnkNewSite.Visible = lnkSiteList.Visible;
            lnkNewSite.Visible = false;
            lnkNewSite.Text = Resource.SiteSettingsNewSiteLabel;
            lnkNewSite.ToolTip = Resource.CreateNewSite;
            lnkNewSite.NavigateUrl = SiteRoot + "/Admin/SiteSettings.aspx?SiteID=-1";

            lnkSiteSettings.Text = "Cập nhật cuộc điều tra";
            lnkSiteSettings.ToolTip = "Cập nhật cuộc điều tra";
            lnkSiteSettings.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilder.aspx";

            Title = SiteUtils.FormatPageTitle(siteSettings, "Cập nhật cuộc điều tra");

            if (!Page.IsPostBack)
            {
                //ViewState["skin"] = selectedSite.Skin;
                hdnCurrentSkin.Value = selectedSite.Skin;
                BindLinhVucThongKe();
                BindYearDieuTra();
                LoadNhomDieuTra();
                LoadTrangThaiDieuTra();
                LoadSelectbox(lboxDoiTuongDieuTra, WebConfigSettings.DM_DoiTuongDieuTra);
                LoadSelectbox(lboxTanSuatDieuTra, WebConfigSettings.DM_TanXuatDieuTra);
                LoadSelectbox(lboxPhamViTongHop, WebConfigSettings.DM_PhamViDieuTra);
                PopulateControl();

            }

        }
        private void LoadNhomDieuTra()
        {
            ddlNhomCuocDieuTra.DataTextField = "SiteName";
            ddlNhomCuocDieuTra.DataValueField = "SiteID";
            ddlNhomCuocDieuTra.DataSource = SiteSettings.GetListSiteParent();
            ddlNhomCuocDieuTra.DataBind();
            ddlNhomCuocDieuTra.Items.Insert(0, new ListItem { Text = "--Chọn nhóm điều tra--", Value = "" });

        }
        public void LoadTrangThaiDieuTra()
        {
            ddlTrangThaiDieuTra.DataTextField = "Text";
            ddlTrangThaiDieuTra.DataValueField = "Value";
            ddlTrangThaiDieuTra.DataSource = CuocDieuTraConstant.GetListItem();
            ddlTrangThaiDieuTra.DataBind();
            ddlTrangThaiDieuTra.Items.Insert(0, new ListItem { Text = "--Chọn trạng thái điều tra--", Value = "" });
        }
        private void LoadSelectbox(ListBox listBox, string code)
        {
            listBox.DataValueField = "ItemID";
            listBox.DataTextField = "Name";
            var source = CoreCategory.GetChildren(siteSettings.SiteId, code);
            listBox.DataSource = source;
            listBox.DataBind();
        }


        private void BindLinhVucThongKe()
        {
            //Lấy danh sách lĩnh vực điều tra
            var source = CoreCategory.GetChildren(siteSettings.SiteId, WebConfigSettings.DM_LinhVucDieuTra);
            ddlLinhVucDieuTra.DataTextField = "Name";
            ddlLinhVucDieuTra.DataValueField = "ItemID";
            ddlLinhVucDieuTra.DataSource = source;
            ddlLinhVucDieuTra.DataBind();
        }
        private void BindYearDieuTra()
        {
            var listItem = new List<ListItem>();
            var maxYear = DateTime.Now.Year + 5;
            for (int i = 2010; i < maxYear; i++)
            {
                listItem.Add(new ListItem { Text = "Năm " + i, Value = i.ToString() });
            }
            ddlNamCuocDieuTra.DataTextField = "Text";
            ddlNamCuocDieuTra.DataValueField = "Value";
            ddlNamCuocDieuTra.DataSource = listItem;
            ddlNamCuocDieuTra.DataBind();
            ddlNamCuocDieuTra.SelectedValue = DateTime.Now.Year.ToString();

        }

        private void LoadSettings()
        {
            selectedSiteID = WebUtils.ParseInt32FromQueryString("siteid", selectedSiteID);
            selectedSite = new SiteSettings(selectedSiteID);
            if (selectedSite.SiteId > 0)
            {
                btnDelete.Visible = true;
            }
        }




        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);

            this.btnSave.Click += new EventHandler(btnSave_Click);

            this.btnDelete.Click += new EventHandler(btnDelete_Click);

            SuppressMenuSelection();
            SuppressPageMenu();

        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            if (WebConfigSettings.AllowDeletingChildSites)
            {
                if ((selectedSite != null) && (!selectedSite.IsServerAdminSite))
                {
                    try
                    {
                        DeleteSiteContent(selectedSite.SiteId);
                        CommentRepository commentRepository = new CommentRepository();
                        commentRepository.DeleteBySite(selectedSite.SiteGuid);

                    }
                    catch (Exception ex)
                    {
                        log.Error("error deleting site content ", ex);
                    }

                    SiteSettings.Delete(selectedSite.SiteId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/GlobalModule/Investigate/ManagerPost.aspx");
                }
            }
        }

        private void DeleteSiteContent(int siteId)
        {
            if (siteId == -1) { return; }

            foreach (SitePreDeleteHandlerProvider contentDeleter in SitePreDeleteHandlerProviderManager.Providers)
            {

                try
                {
                    contentDeleter.DeleteSiteContent(siteId);
                }
                catch (Exception ex)
                {
                    log.Error("SiteSettings.aspx.cs.DeleteSiteContent ", ex);
                }

            }

            if (WebConfigSettings.DeleteSiteFolderWhenDeletingSites)
            {
                FolderDeleteTask task = new FolderDeleteTask();
                task.SiteGuid = siteSettings.SiteGuid;
                SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
                if (currentUser != null)
                {
                    task.QueuedBy = currentUser.UserGuid;
                }
                task.FolderToDelete = Server.MapPath("~/Data/Sites/" + siteId.ToInvariantString() + "/");
                task.QueueTask();

                WebTaskManager.StartOrResumeTasks();
            }

        }
        protected void btnSave_Click(Object sender, EventArgs e)
        {
            Page.Validate("sitesettings");
            if (!Page.IsValid) { return; }
            selectedSite.SiteName = txtSiteName.Text;
            selectedSite.Nam = ddlNamCuocDieuTra.SelectedValue.ToIntOrZero();
            selectedSite.LinhVucID = ddlLinhVucDieuTra.SelectedValue.ToIntOrZero();
            selectedSite.DoiTuongDieuTra = lboxDoiTuongDieuTra.SelectedValue.Replace(",", " ");
            selectedSite.TanSuatDieuTra = lboxTanSuatDieuTra.SelectedValue.Replace(",", " ");
            selectedSite.PhamViSoLieu = lboxPhamViTongHop.SelectedValue.Replace(",", " ");
            selectedSite.NoiDungDieuTra = txtNoiDung.Text;
            selectedSite.PathIMG = txtUrlImage.Text;
            selectedSite.ParentID = ddlNhomCuocDieuTra.SelectedValue.ToIntOrNULL();
            selectedSite.FileDuThao = txtFileDuThao.Text;
            selectedSite.TrangThaiDieuTra = ddlTrangThaiDieuTra.SelectedValue.ToIntOrNULL();

            selectedSite.Save();

            WebUtils.SetupRedirect(this, "/GlobalModule/Investigate/ManagerPost.aspx");
        }
        private void PopulateControl()
        {
            txtSiteName.Text = selectedSite.SiteName;
            ddlNamCuocDieuTra.SelectedValue = selectedSite.Nam.ToString();
            ddlLinhVucDieuTra.SelectedValue = selectedSite.LinhVucID.ToString();
            txtNoiDung.Text = selectedSite.NoiDungDieuTra;
            txtUrlImage.Text = selectedSite.PathIMG;
            ddlNhomCuocDieuTra.SelectedValue = selectedSite.ParentID.ToString();
            ddlTrangThaiDieuTra.SelectedValue = selectedSite.TrangThaiDieuTra.ToString();
            txtFileDuThao.Text = selectedSite.FileDuThao;

            if (!string.IsNullOrEmpty(selectedSite.DoiTuongDieuTra))
            {
                var listSelected = selectedSite.DoiTuongDieuTra.ToListInt(' ');
                foreach (ListItem item in lboxDoiTuongDieuTra.Items)
                {
                    if (listSelected.Contains(item.Value.ToIntOrZero()))
                    {
                        item.Selected = true;
                    }
                }
            }

            if (!string.IsNullOrEmpty(selectedSite.TanSuatDieuTra))
            {
                var listSelected = selectedSite.TanSuatDieuTra.ToListInt(' ');
                foreach (ListItem item in lboxTanSuatDieuTra.Items)
                {
                    if (listSelected.Contains(item.Value.ToIntOrZero()))
                    {
                        item.Selected = true;
                    }
                }
            }

            if (!string.IsNullOrEmpty(selectedSite.PhamViSoLieu))
            {
                var listSelected = selectedSite.PhamViSoLieu.ToListInt(' ');
                foreach (ListItem item in lboxPhamViTongHop.Items)
                {
                    if (listSelected.Contains(item.Value.ToIntOrZero()))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

        }


    }


}

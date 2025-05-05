// Author:					HiNet
// Created:					2015-3-19
// Last Modified:			2015-3-19
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace DuThaoVanBanFeature.UI
{

    public partial class EditPostPage : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        protected string deleteImg = string.Empty;
        protected string deleteTooltip = string.Empty;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private DuThaoVanBanConfiguration config = new DuThaoVanBanConfiguration();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private DuThaoVanBan dt;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string dateTimeFormat;
        string name;
        private bool isUserApprove = false;
        private bool isAdmin = false;
        // replace this with your own feature guid or make a static property on one of your business objects
        // like MyFeature.FeatureGuid, then you can use that instead of this variable
        private Guid featureGuid = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            //if (!isAdmin)
            //{
            //    return;
            //}

            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageDuThaoVanBan))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            PopulateLabels();
            SetupScripts();
            if (!IsPostBack)
            {
                PopulateControls();
            }

        }

        private void PopulateControls()
        {
            bindLinhVuc();
            bindLoaiVB();
            bindCoQuanBanHanh();
            if (dt != null)
            {
                pnFile.Visible = true;
                ddlLinhVuc.SelectedValue = dt.LinhVucID.ToString();
                ddlLoaiVB.SelectedValue = dt.LoaiVanBanID.ToString();
                ddlCoQuanBanHanh.SelectedValue = dt.CoQuanBanHanhID.ToString();

                edSummary.Text = dt.Summary;
                txtTitle.Text = dt.Title;
                txtCreatedByUser.Text = dt.CreatedByUser;
                if (!string.IsNullOrEmpty(dt.StartDate.ToString()))
                {
                    dpDateStart.Text = string.Format("{0:dd/MM/yyyy}", dt.StartDate);
                }
                if (!string.IsNullOrEmpty(dt.EndDate.ToString()))
                {
                    dpDateExpires.Text = string.Format("{0:dd/MM/yyyy}", dt.EndDate);
                }
                txtItemUrl.Text = dt.ItemUrl.ToString();
                chkIsPublic.Checked = dt.IsPublic;
                PopulateFile();
            }
            else if (dt == null)
            {
                pnFile.Visible = false;
                txtCreatedByUser.Text = siteUser.Name;
                btnDel.Visible = false;
                return;
            }
        }
        private void PopulateFile()
        {
            List<FileDuThao> lstDuThao = new List<FileDuThao>();
            lstDuThao = FileDuThao.GetAllByDuThaoId(itemId);
            rptFile.DataSource = lstDuThao;
            rptFile.DataBind();
        }
        private void bindLoaiVB()
        {
            //get all categories
            int CategoryConfig = siteSetting.CoreLoaiVanBanQuyPham;
            List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_LoaiVanBan);
            ddlLoaiVB.DataValueField = "ItemID";
            ddlLoaiVB.DataTextField = "Name";
            ddlLoaiVB.DataSource = lstLoai;
            ddlLoaiVB.DataBind();
            ddlLoaiVB.Items.Insert(0, new ListItem("--" + DocumentResources.ChooseTypeDocumentLabel + "--", "0"));
        }
        private void bindLinhVuc()
        {
            //get all categories
            int CategoryConfig = siteSetting.CoreLinhVucVanBanQuyPham;
            List<CoreCategory> lstLinhVuc = CoreCategory.GetChildren(1, WebConfigSettings.DM_LinhVucVanBan);
            ddlLinhVuc.DataValueField = "ItemID";
            ddlLinhVuc.DataTextField = "Name";
            ddlLinhVuc.DataSource = lstLinhVuc;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("--" + DocumentResources.ChooseFieldLabel + "--", "0"));
        }

        private void bindCoQuanBanHanh()
        {
            //get all categories
            List<CoreCategory> lstLinhVuc = CoreCategory.GetChildren(1, WebConfigSettings.DM_CoQuanBanHanh);
            ddlCoQuanBanHanh.DataValueField = "ItemID";
            ddlCoQuanBanHanh.DataTextField = "Name";
            ddlCoQuanBanHanh.DataSource = lstLinhVuc;
            ddlCoQuanBanHanh.DataBind();
            ddlCoQuanBanHanh.Items.Insert(0, new ListItem("--Chọn cơ quan ban hành--", "0"));
        }


        private string SuggestUrl()
        {
            string pageName = txtTitle.Text;
            return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }
        private bool Save()
        {
            Page.Validate("DuThaoVanBan");
            if (!Page.IsValid) return false;
            int check = 0;
            foreach (string item in Request.Files)
            {
                HttpPostedFile file = Request.Files[item];
                if (file.ContentLength > 0)
                {
                    check++;
                }
            }
            if (itemId == -1)
            {
                if (check == 0)
                {
                    lblError.Text = "Bạn chưa chọn file";
                    lblError.Visible = true;
                    return false;
                }
            }
            if (dt == null) { dt = new DuThaoVanBan(itemId); }
            dt.SiteID = siteSettings.SiteId;
            dt.PageID = pageId;
            dt.ModuleID = moduleId;
            dt.Summary = edSummary.Text;
            dt.Title = txtTitle.Text;
            dt.CreatedDate = DateTime.UtcNow;
            dt.LastModUtc = DateTime.UtcNow;
            DateTime localTime = DateTime.Parse(dpDateStart.Text, CultureInfo.CurrentCulture);
            dt.StartDate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
            if (!string.IsNullOrEmpty(dpDateExpires.Text))
            {
                DateTime localEndTime = DateTime.Parse(dpDateExpires.Text, CultureInfo.CurrentCulture);
                dt.EndDate = timeZone != null ? localEndTime.ToUtc(timeZone) : localEndTime.AddHours(-timeOffset);
            }
            else
            {
                dt.EndDate = null;
            }
            dt.CreatedByUser = txtCreatedByUser.Text;
            dt.LastModUserGuid = siteUser.UserGuid;
            var fulltext = edSummary.Text + " " + txtTitle.Text + " " + txtCreatedByUser.Text;
            fulltext = fulltext.ConvertToFTS();
            dt.FTS = fulltext;
            dt.IsPublic = chkIsPublic.Checked;
            if (chkIsPublic.Checked)
            {
                dt.PublicByUser = siteUser.UserId;
                dt.PublicDate = DateTime.UtcNow;
            }
            if (txtItemUrl.Text.Length == 0)
            {
                txtItemUrl.Text = SuggestUrl();
            }

            Guid DuThaoGuid = Guid.NewGuid();
            String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", String.Empty));
            FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

            if (
                ((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != DuThaoGuid))
                && (dt.ItemUrl != txtItemUrl.Text)
                )
            {
                //lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                return false;
            }

            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString))
                {
                    //lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                    return false;
                }
            }

            string oldUrl = dt.ItemUrl.Replace("~/", string.Empty);
            string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));
            dt.ItemUrl = "~/" + newUrl;
            dt.LoaiVanBanID = int.Parse(ddlLoaiVB.SelectedValue);
            dt.LinhVucID = int.Parse(ddlLinhVuc.SelectedValue);
            dt.CoQuanBanHanhID = int.Parse(ddlCoQuanBanHanh.SelectedValue);

            dt.Save();
            foreach (string inputTagName in Request.Files)
            {
                var index = inputTagName.Substring(inputTagName.IndexOf("fileInput") + 9);
                HttpPostedFile file = Request.Files[inputTagName];
                if (file.ContentLength > 0)
                {
                    var fileDuThao = new FileDuThao();
                    fileDuThao.DuThaoID = dt.ItemID;
                    if (string.IsNullOrEmpty(Request["fileName" + index]))
                    {
                        fileDuThao.Name = file.FileName;
                    }
                    else
                    {
                        fileDuThao.Name = Request["fileName" + index];
                    }
                    fileDuThao.ModuleID = moduleId;
                    fileDuThao.PageID = pageId;
                    fileDuThao.SiteID = siteId;
                    fileDuThao.CreatedDate = DateTime.UtcNow;
                    fileDuThao.CreatedByUserID = siteUser.UserId;
                    fileDuThao.LastModUtc = DateTime.UtcNow;
                    string fileExtension = Path.GetExtension(file.FileName);
                    Double fileSize = file.ContentLength / 1024;
                    string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    //string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedFileExtensions"))
                    {
                        //lblFileUrlError.Text = "file tải lên không đúng định dạng";
                    }
                    if (!SiteUtils.IsValidFileSize(fileSize, "AllowedFileSize"))
                    {
                        //lblFileUrlError.Text = "file không hợp lệ";
                    }
                    else
                    {
                        try
                        {
                            string path = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["DraftDocumentFileFolder"]);
                            bool folderExists = Directory.Exists(path);
                            if (!folderExists)
                            {
                                Directory.CreateDirectory(path);
                            }
                            name = guid + fileExtension;
                            fileDuThao.FilePath = name;
                            file.SaveAs(Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["DraftDocumentFileFolder"] + name);
                            fileDuThao.Save();
                        }
                        catch
                        {
                            //lblFileUrlError.Text = "file tải lên bị trùng.";
                        }
                    }
                }
            }
            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                {
                    FriendlyUrl newFriendlyUrl = new FriendlyUrl
                    {
                        SiteId = siteSettings.SiteId,
                        SiteGuid = siteSettings.SiteGuid,
                        PageGuid = DuThaoGuid,
                        Url = friendlyUrlString,
                        RealUrl = "~/DuThaoVanBan/viewpost.aspx?pageid="
                                  + pageId.ToInvariantString()
                                  + "&mid=" + dt.ModuleID.ToInvariantString()
                                  + "&item=" + dt.ItemID.ToInvariantString()
                    };
                    newFriendlyUrl.Save();
                }
                if ((oldUrl.Length > 0) && (newUrl.Length > 0) && (!SiteUtils.UrlsMatch(oldUrl, newUrl)))
                {
                    //worry about the risk of a redirect loop if the page is restored to the old url again
                    // don't create it if a redirect for the new url exists
                    if (
                        (!RedirectInfo.Exists(siteSettings.SiteId, oldUrl))
                        && (!RedirectInfo.Exists(siteSettings.SiteId, newUrl))
                        )
                    {
                        RedirectInfo redirect = new RedirectInfo
                        {
                            SiteGuid = siteSettings.SiteGuid,
                            SiteId = siteSettings.SiteId,
                            OldUrl = oldUrl,
                            NewUrl = newUrl
                        };
                        redirect.Save();
                    }
                    // since we have created a redirect we don't need the old friendly url
                    FriendlyUrl oldFriendlyUrl = new FriendlyUrl(siteSettings.SiteId, oldUrl);
                    if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == DuThaoGuid))
                    {
                        FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                    }

                }
            }

            //else
            //{
            //    dt.SiteID = siteId;
            //    dt.PageID = pageId;
            //    dt.ModuleID = moduleId;
            //    dt.Summary = txtSummary.Text;
            //    dt.Title = txtTitle.Text;
            //    dt.LastModUtc = DateTime.UtcNow;
            //    dt.CreatedByUser = txtCreatedByUser.Text;
            //    DateTime localTime = DateTime.Parse(dpDateStart.Text, CultureInfo.CurrentCulture);
            //    dt.StartDate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
            //    if (!string.IsNullOrEmpty(dpDateExpires.Text))
            //    {
            //        DateTime localEndTime = DateTime.Parse(dpDateExpires.Text, CultureInfo.CurrentCulture);
            //        dt.EndDate = timeZone != null ? localEndTime.ToUtc(timeZone) : localEndTime.AddHours(-timeOffset);
            //    }
            //    else
            //    {
            //        dt.EndDate = null;
            //    }
            //    dt.LastModUserGuid = siteUser.UserGuid;
            //    if (txtItemUrl.Text.Length == 0)
            //    {
            //        txtItemUrl.Text = SuggestUrl();
            //    }

            //    Guid DuThaoGuid = Guid.NewGuid();
            //    String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", String.Empty));
            //    FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

            //    if (
            //        ((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != DuThaoGuid))
            //        && (dt.ItemUrl != txtItemUrl.Text)
            //        )
            //    {
            //        //lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
            //        return;
            //    }

            //    if (!friendlyUrl.FoundFriendlyUrl)
            //    {
            //        if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString))
            //        {
            //            //lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
            //            return;
            //        }
            //    }

            //    string oldUrl = dt.ItemUrl.Replace("~/", string.Empty);
            //    string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));
            //    dt.LoaiVanBanID = int.Parse(ddlLoaiVB.SelectedValue);
            //    dt.LinhVucID = int.Parse(ddlLinhVuc.SelectedValue);
            //    dt.Save();
            //    if (!friendlyUrl.FoundFriendlyUrl)
            //    {
            //        if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
            //        {
            //            FriendlyUrl newFriendlyUrl = new FriendlyUrl
            //            {
            //                SiteId = siteSettings.SiteId,
            //                SiteGuid = siteSettings.SiteGuid,
            //                PageGuid = DuThaoGuid,
            //                Url = friendlyUrlString,
            //                RealUrl = "~/Document/Detail.aspx?pageid="
            //                          + pageId.ToInvariantString()
            //                          + "&mid=" + dt.ModuleID.ToInvariantString()
            //                          + "&item=" + dt.ItemID.ToInvariantString()
            //            };
            //            newFriendlyUrl.Save();
            //        }
            //        if ((oldUrl.Length > 0) && (newUrl.Length > 0) && (!SiteUtils.UrlsMatch(oldUrl, newUrl)))
            //        {
            //            //worry about the risk of a redirect loop if the page is restored to the old url again
            //            // don't create it if a redirect for the new url exists
            //            if (
            //                (!RedirectInfo.Exists(siteSettings.SiteId, oldUrl))
            //                && (!RedirectInfo.Exists(siteSettings.SiteId, newUrl))
            //                )
            //            {
            //                RedirectInfo redirect = new RedirectInfo
            //                {
            //                    SiteGuid = siteSettings.SiteGuid,
            //                    SiteId = siteSettings.SiteId,
            //                    OldUrl = oldUrl,
            //                    NewUrl = newUrl
            //                };
            //                redirect.Save();
            //            }
            //            // since we have created a redirect we don't need the old friendly url
            //            FriendlyUrl oldFriendlyUrl = new FriendlyUrl(siteSettings.SiteId, oldUrl);
            //            if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == DuThaoGuid))
            //            {
            //                FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
            //            }

            //        }
            //    }
            //}
            return true;
        }
        private void DeleteFileFromServer(string fileName)
        {
            string file = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["DraftDocumentFileFolder"] + fileName;
            file = file.Replace("/", "\\");

            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        private void PopulateLabels()
        {
            btnSubmit.Text = DocumentResources.btnSubmit;
            btnCancel.Text = DocumentResources.btnCancel;
            btnDel.Text = DocumentResources.btnDel;
            deleteImg = "~/Data/SiteImages/delete.gif";
            deleteTooltip = ArticleResources.ArticleDeleteLinkText;
            btnCancel.PostBackUrl = "/DuThaoVanBan/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            rfvLinhVuc.ErrorMessage = DocumentResources.ChooseFieldLabel;
            rfvLoaiVanBan.ErrorMessage = DocumentResources.ChooseTypeDocumentLabel;
            RequiredFieldValidator2.ErrorMessage = "Bạn nhập tiêu đề";
            RequiredFieldValidator1.ErrorMessage = "Bạn chọn thời gian bắt đầu";
            RegularExpressionValidator2.ErrorMessage = "Ngày bắt đầu sai định dạng";
            RegularExpressionValidator1.ErrorMessage = "Ngày kết thúc sai định dạng";
        }

        private void LoadSettings()
        {
            if ((WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; }
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new DuThaoVanBanConfiguration(getModuleSettings);
            if (WebUser.IsInRoles(config.RoleApprove))
            {
                isUserApprove = true;
            }
            if (itemId > -1)
            {
                dt = new DuThaoVanBan(itemId);
                if (dt.ModuleID != moduleId) { dt = null; }
            }
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            siteId = siteUser.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
        }

        //private void DeleteFileFromServer()
        //{
        //    string thumbnailImageURL = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["DocumentFileFolder"] + dt.FilePath;
        //    thumbnailImageURL = thumbnailImageURL.Replace("/", "\\");
        //    string imageURL = thumbnailImageURL.Substring(0, thumbnailImageURL.LastIndexOf(".")) + "_t" + thumbnailImageURL.Substring(thumbnailImageURL.LastIndexOf("."));
        //    if (File.Exists(imageURL))
        //    {
        //        File.Delete(imageURL);
        //    }
        //    if (File.Exists(thumbnailImageURL))
        //    {
        //        File.Delete(thumbnailImageURL);
        //    }
        //}

        private void SetupScripts()
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("sarissa"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "sarissa", "<script src=\""
                    + ResolveUrl("~/ClientScript/sarissa/sarissa.js") + "\" type=\"text/javascript\"></script>");
            }

            if (!Page.ClientScript.IsClientScriptBlockRegistered("sarissa_ieemu_xpath"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "sarissa_ieemu_xpath", "<script src=\""
                    + ResolveUrl("~/ClientScript/sarissa/sarissa_ieemu_xpath.js") + "\" type=\"text/javascript\"></script>");
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered("friendlyurlsuggest"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "friendlyurlsuggest", "<script src=\""
                    + ResolveUrl("~/ClientScript/friendlyurlsuggest_v2.js") + "\" type=\"text/javascript\"></script>");
            }

            string focusScript = string.Empty;
            if (itemId == -1) { focusScript = "document.getElementById('" + txtTitle.ClientID + "').focus();"; }

            string hookupInputScript = "<script type=\"text/javascript\">"
                + "new UrlHelper( "
                + "document.getElementById('" + txtTitle.ClientID + "'),  "
                + "document.getElementById('" + txtItemUrl.ClientID + "'), "
                + "document.getElementById('" + hdnTitle.ClientID + "'), "
                + "document.getElementById('" + spnUrlWarning.ClientID + "'), "
                + "\"" + SiteRoot + "/UIUtils/BlogUrlSuggestService.ashx" + "\""
                + "); " + focusScript + "</script>";

            if (!Page.ClientScript.IsStartupScriptRegistered(UniqueID + "urlscript"))
            {
                Page.ClientScript.RegisterStartupScript(
                    GetType(),
                    UniqueID + "urlscript", hookupInputScript);
            }


        }
        #region OnInit
        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (Save())
            {
                WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
            }
        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnDel.Click += new EventHandler(btnDel_Click);
            SiteUtils.SetupEditor(edSummary);
        }

        #endregion
        private void btnDel_Click(object sender, System.EventArgs e)
        {
            if (dt != null)
            {
                List<FileDuThao> lstFile = FileDuThao.GetAllByDuThaoId(dt.ItemID);
                if (lstFile != null && lstFile.Count > 0)
                {
                    foreach (var item in lstFile)
                    {
                        DeleteFileFromServer(item.FilePath);
                        FileDuThao.Delete(item.ItemID);
                    }
                }
                DuThaoVanBan.Delete(itemId);
            }
            WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
        }
        protected void rptFile_Command(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("Delete"))
                {
                    int fileId = int.Parse(e.CommandArgument.ToString());
                    FileDuThao file = new FileDuThao(fileId);
                    DeleteFileFromServer(file.FilePath);
                    FileDuThao.Delete(fileId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/DuThaoVanBan/EditPost.aspx?pageid"
                       + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&item=" + itemId.ToInvariantString());
                }
            }
        }
    }
}
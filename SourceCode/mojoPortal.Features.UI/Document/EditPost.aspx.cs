using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
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



namespace DocumentFeature.UI
{

    public partial class EditPostPage : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private DocumentConfiguration config = new DocumentConfiguration();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private Documentation dc;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string dateTimeFormat;
        string fileName;
        string name;
        // replace this with your own feature guid or make a static property on one of your business objects
        // like MyFeature.FeatureGuid, then you can use that instead of this variable
        private Guid featureGuid = Guid.Parse("2442f589-a459-4e3d-a345-9426ebed0423");

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();

            //if (!UserCanEditModule(moduleId, featureGuid))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}

            LoadSettings();
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
            bindCoQuan();
            bindNhomVanBan();
            if (siteUser != null)
            {
                txtCreatedByUser.Text = siteUser.Name;
            }
            if (dc != null)
            {
                ddlLinhVuc.SelectedValue = dc.LinhVuc.ToString();
                ddlLoaiVB.SelectedValue = dc.LoaiVB.ToString();
                ddlCoQuanID.SelectedValue = dc.CoQuanID.ToString();
                ddlNhomVanBan.SelectedValue = dc.Type.ToString();
                txtSummary.Text = dc.Summary.ToString();
                edContent.Text = dc.ContentDoc.ToString();
                txtSign.Text = dc.Sign.ToString();
                if (dc.DatePromulgate.HasValue)
                {
                    dpDatePromulgate2.Text = dc.DatePromulgate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
                }
                if (dc.DateEffect.HasValue)
                {
                    dpDateEffect2.Text = dc.DateEffect.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
                }
                txtSigner.Text = dc.Signer.ToString();
                txtItemUrl.Text = dc.ItemUrl.ToString();
                txtCreatedByUser.Text = dc.CreatedByUser.ToString();
                chkIsApproved.Checked = dc.IsApproved;
            }

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
        private void bindCoQuan()
        {
            //get all categories
            int CategoryConfig = siteSetting.CoreCoQuanBanHanhVanBanQuyPham;
            List<CoreCategory> lstCoQuan = CoreCategory.GetChildren(1, WebConfigSettings.DM_CoQuanBanHanh);
            ddlCoQuanID.DataValueField = "ItemID";
            ddlCoQuanID.DataTextField = "Name";
            ddlCoQuanID.DataSource = lstCoQuan;
            ddlCoQuanID.DataBind();
            ddlCoQuanID.Items.Insert(0, new ListItem("--" + DocumentResources.AgencyLabel + "--", "0"));
        }

        private void bindNhomVanBan()
        {
            CoreCategory coreCategory = CoreCategory.GetByCode(SiteId, WebConfigSettings.DM_NhomVanBan);
            ListItem list = new ListItem
            {
                Text = coreCategory.Name,
                Value = coreCategory.ItemID.ToString()
            };
            ddlNhomVanBan.Items.Add(list);
            PopulateChildNode(ddlNhomVanBan);
            ddlNhomVanBan.SelectedValue = config.NhomVanBanSetting.ToString();
        }

        private void PopulateChildNode(ListControl root)
        {
            for (int i = 0; i < root.Items.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(root.Items[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root.Items[i].Text.StartsWith("|"))
                {
                    prefix += root.Items[i].Text.Substring(0, 3);
                    root.Items[i].Text = root.Items[i].Text.Remove(0, 3);
                }
                root.Items[i].Text = prefix + root.Items[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString()
                    };
                    root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    index++;
                }
            }
        }




        private string SuggestUrl()
        {
            string pageName = txtSign.Text;
            return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }
        private bool Save()
        {
            Page.Validate("document");
            if (!Page.IsValid) return false;
            if (dc == null)
            {
                Documentation document = new Documentation(itemId);

                document.SiteID = siteSettings.SiteId;
                document.PageID = pageId;
                document.ModuleID = moduleId;
                if (string.IsNullOrEmpty(txtSummary.Text))
                {
                    lblSummaryErrorMessage.Text = DocumentResources.SummaryRequiredLabel;
                    return false;
                }

                document.Summary = txtSummary.Text;
                document.Sign = txtSign.Text;
                DateTime localTime = DateTime.Parse(dpDatePromulgate2.Text, CultureInfo.CurrentCulture);
                document.DatePromulgate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                string year = localTime.ToString("yyyy", CultureInfo.CurrentCulture);
                document.YearPromulgate = int.Parse(year);
                if (!string.IsNullOrEmpty(dpDateEffect2.Text))
                {
                    DateTime localEndTime = DateTime.Parse(dpDateEffect2.Text, CultureInfo.CurrentCulture);
                    document.DateEffect = timeZone != null ? localEndTime.ToUtc(timeZone) : localEndTime.AddHours(-timeOffset);
                }
                document.Signer = txtSigner.Text;
                HttpPostedFile files = uploadFile.PostedFile;
                string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                if (uploadFile.HasFile && files.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(uploadFile.FileName);
                    Double fileSize = files.ContentLength / 1024;
                    //string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedFileExtensions"))
                    {
                        lblFileUrlError.Text = "file tải lên không đúng định dạng";
                    }
                    if (!SiteUtils.IsValidFileSize(fileSize, "AllowedFileSize"))
                    {
                        lblFileUrlError.Text = "file không hợp lệ";
                    }
                    else
                    {
                        try
                        {
                            string path = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["DocumentFileFolder"]);
                            bool folderExists = Directory.Exists(path);
                            if (!folderExists)
                            {
                                Directory.CreateDirectory(path);
                            }
                            name = uploadFile.FileName;
                            var fullPath = path + "/" + uploadFile.FileName;
                            if (File.Exists(fullPath))
                            {
                                name = DateTime.Now.ToString("ddMMyyyyhhmmss") + "-" + uploadFile.FileName;
                            }
                            //name = guid + fileExtension;
                            uploadFile.SaveAs(Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["DocumentFileFolder"] + name);
                            document.FilePath = name;
                        }
                        catch
                        {
                            lblFileUrlError.Text = "file tải lên bị trùng.";
                        }
                    }
                }
                if (txtItemUrl.Text.Length == 0)
                {
                    txtItemUrl.Text = SuggestUrl();
                }

                Guid DocumentGuid = Guid.NewGuid();
                String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", String.Empty));
                FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

                if (
                    ((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != DocumentGuid))
                    && (document.ItemUrl != txtItemUrl.Text)
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
                string oldUrl = document.ItemUrl.Replace("~/", string.Empty);
                string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));
                document.ItemUrl = "~/" + newUrl;
                document.CoQuanID = int.Parse(ddlCoQuanID.SelectedValue);
                document.LoaiVB = int.Parse(ddlLoaiVB.SelectedValue);
                document.LinhVuc = int.Parse(ddlLinhVuc.SelectedValue);
                document.ContentDoc = edContent.Text;
                document.CreatedByUser = txtCreatedByUser.Text;
                document.IsApproved = chkIsApproved.Checked;
                document.CreatedByUserGuid = siteUser.UserGuid;
                document.ApprovedDate = DateTime.UtcNow;
                document.ApprovedGuid = siteUser.UserGuid;
                document.Type = ddlNhomVanBan.SelectedValue.ToIntOrZero();
                document.FTS = txtSign.Text.ConvertToFTS() + " " + txtSummary.Text.ConvertToFTS() + " " + edContent.Text.ConvertToFTS() + " " + txtSigner.Text.ConvertToFTS();
                document.Save();
                if (!friendlyUrl.FoundFriendlyUrl)
                {
                    if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                    {
                        FriendlyUrl newFriendlyUrl = new FriendlyUrl
                        {
                            SiteId = siteSettings.SiteId,
                            SiteGuid = siteSettings.SiteGuid,
                            PageGuid = DocumentGuid,
                            Url = friendlyUrlString,
                            RealUrl = "~/Document/Detail.aspx?pageid="
                                      + pageId.ToInvariantString()
                                      + "&mid=" + document.ModuleID.ToInvariantString()
                                      + "&item=" + document.ItemID.ToInvariantString()
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
                        if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == DocumentGuid))
                        {
                            FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                        }

                    }
                }
            }
            else
            {
                List<ChuDeVanBanPhapQuy> lstChuDe = ChuDeVanBanPhapQuy.GetAllByDocId(dc.ItemID);
                if (lstChuDe != null && lstChuDe.Count > 0)
                {
                    foreach (var item in lstChuDe)
                    {
                        if (item != null)
                        {
                            ChuDeVanBanPhapQuy.Delete(item.ItemID);
                        }
                    }
                }
                dc.SiteID = siteId;
                dc.PageID = pageId;
                dc.ModuleID = moduleId;
                if (string.IsNullOrEmpty(txtSummary.Text))
                {
                    lblSummaryErrorMessage.Text = DocumentResources.SummaryRequiredLabel;
                    return false;
                }
                dc.Summary = txtSummary.Text;
                dc.Sign = txtSign.Text;
                DateTime localTime = DateTime.Parse(dpDatePromulgate2.Text, CultureInfo.CurrentCulture);
                dc.DatePromulgate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                string year = localTime.ToString("yyyy", CultureInfo.CurrentCulture);
                dc.YearPromulgate = int.Parse(year);
                if (!string.IsNullOrEmpty(dpDateEffect2.Text))
                {
                    DateTime localEndTime = DateTime.Parse(dpDateEffect2.Text, CultureInfo.CurrentCulture);
                    dc.DateEffect = timeZone != null ? localEndTime.ToUtc(timeZone) : localEndTime.AddHours(-timeOffset);
                }
                dc.Signer = txtSigner.Text;
                dc.Type = ddlNhomVanBan.SelectedValue.ToIntOrZero();
                HttpPostedFile files = uploadFile.PostedFile;
                if (uploadFile.HasFile && files.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(uploadFile.FileName);
                    Double fileSize = files.ContentLength / 1024;
                    string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedFileExtensions"))
                    {
                        lblFileUrlError.Text = "file tải lên không đúng định dạng";
                    }
                    if (!SiteUtils.IsValidFileSize(fileSize, "AllowedFileSize"))
                    {
                        lblFileUrlError.Text = "file không hợp lệ";
                    }
                    else
                    {
                        try
                        {
                            name = guid + fileExtension;
                            uploadFile.SaveAs(Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["DocumentFileFolder"] + name);
                            dc.FilePath = name;
                        }
                        catch
                        {
                            lblFileUrlError.Text = "flash tải lên bị trùng.";
                        }
                    }
                }
                if (txtItemUrl.Text.Length == 0)
                {
                    txtItemUrl.Text = SuggestUrl();
                }

                Guid DocumentGuid = Guid.NewGuid();
                String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", String.Empty));
                FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

                if (
                    ((friendlyUrl.FoundFriendlyUrl) && (friendlyUrl.PageGuid != DocumentGuid))
                    && (dc.ItemUrl != txtItemUrl.Text)
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

                string oldUrl = dc.ItemUrl.Replace("~/", string.Empty);
                string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));
                dc.ItemUrl = "~/" + newUrl;
                dc.CoQuanID = int.Parse(ddlCoQuanID.SelectedValue);
                dc.LoaiVB = int.Parse(ddlLoaiVB.SelectedValue);
                dc.LinhVuc = int.Parse(ddlLinhVuc.SelectedValue);
                dc.ContentDoc = edContent.Text;
                dc.CreatedByUser = txtCreatedByUser.Text;
                dc.IsApproved = chkIsApproved.Checked;
                dc.CreatedByUserGuid = siteUser.UserGuid;
                dc.ApprovedDate = DateTime.UtcNow;
                dc.ApprovedGuid = siteUser.UserGuid;
                dc.FTS = txtSign.Text.ConvertToFTS() + " " + txtSummary.Text.ConvertToFTS() + " " + edContent.Text.ConvertToFTS() + " " + txtSigner.Text.ConvertToFTS();
                dc.Save();
                if (!friendlyUrl.FoundFriendlyUrl)
                {
                    if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                    {
                        FriendlyUrl newFriendlyUrl = new FriendlyUrl
                        {
                            SiteId = siteSettings.SiteId,
                            SiteGuid = siteSettings.SiteGuid,
                            PageGuid = DocumentGuid,
                            Url = friendlyUrlString,
                            RealUrl = "~/Document/Detail.aspx?pageid="
                                      + pageId.ToInvariantString()
                                      + "&mid=" + dc.ModuleID.ToInvariantString()
                                      + "&item=" + dc.ItemID.ToInvariantString()
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
                        if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == DocumentGuid))
                        {
                            FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                        }

                    }
                }
            }
            return true;
        }

        private void PopulateLabels()
        {
            //edSummary.WebEditor.Height = 100;
            //edSummary.WebEditor.Width = 500;
            edContent.WebEditor.Height = 100;
            edContent.WebEditor.Width = 500;
            pnFile1.Visible = false;
            if (itemId > 0)
            {
                heading.Text = "Cập nhật văn bản pháp quy";
                Title = SiteUtils.FormatPageTitle(siteSetting, "Cập nhật văn bản pháp quy");
            }
            else
            {
                heading.Text = "Thêm mới văn bản pháp quy";
                Title = SiteUtils.FormatPageTitle(siteSetting, "Thêm mưới văn bản pháp quy");
            }
            btnSubmit.Text = DocumentResources.btnSubmit;
            btnCancel.Text = DocumentResources.btnCancel;
            btnDel.Text = DocumentResources.btnDel;
            btnDel.Visible = itemId > 0;
            SiteUtils.AddConfirmButton(btnDel, "Dữ liệu này sẽ bị xóa, bạn có chắc chắn muốn tiếp tục?");

            UIHelper.DisableButtonAfterClick(
    btnSubmit,
    ArticleResources.ButtonDisabledPleaseWait,
    Page.ClientScript.GetPostBackEventReference(btnSubmit, string.Empty)
    );
            btnCancel.PostBackUrl = SiteRoot + "/Document/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            rfvLinhVuc.ErrorMessage = DocumentResources.ChooseFieldLabel;
            rfvCoQuan.ErrorMessage = DocumentResources.AgencyLabel;
            rfvLoaiVanBan.ErrorMessage = DocumentResources.ChooseTypeDocumentLabel;
            rfvSign.ErrorMessage = DocumentResources.SignRequiredLabel;
            rfvDateEffect.ErrorMessage = DocumentResources.DateEffectRequiredLabel;
            rfvDatePromulgate.ErrorMessage = DocumentResources.DatePromulgateRequiredLabel;
            rfvSigner.ErrorMessage = DocumentResources.SignerRequiredLabel;
            if (dc != null)
            {
                if (!string.IsNullOrEmpty(dc.FilePath))
                {
                    pnFile1.Visible = true;
                    lnkAttach.Text = dc.FilePath;
                    lnkAttach.NavigateUrl = "/" + ConfigurationManager.AppSettings["DocumentFileFolder"] + dc.FilePath;
                    btnDeleteImg1.ImageUrl = "~/Data/SiteImages/delete.gif";
                }
            }
        }

        private void LoadSettings()
        {
            if (itemId > -1)
            {
                dc = new Documentation(itemId);
                //if (dc.ModuleID != moduleId) { dc = null; }
            }
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new DocumentConfiguration(getModuleSettings);
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            siteId = siteSetting.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
        }
        protected void btnDeleteImg1_Click(object sender, ImageClickEventArgs e)
        {
            DeleteFileFromServer(dc.FilePath);
            Documentation doc = new Documentation(dc.ItemID);
            doc.FilePath = string.Empty;
            doc.Save();
            WebUtils.SetupRedirect(this, SiteRoot + "/Document/EditPost.aspx?pageid"
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&item=" + dc.ItemID);
        }
        private void DeleteFileFromServer(string file)
        {
            string thumbnailImageURL = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["DocumentFileFolder"] + dc.FilePath;
            thumbnailImageURL = thumbnailImageURL.Replace("/", "\\");
            //string imageURL = thumbnailImageURL.Substring(0, thumbnailImageURL.LastIndexOf(".")) + "_t" + thumbnailImageURL.Substring(thumbnailImageURL.LastIndexOf("."));
            //if (File.Exists(imageURL))
            //{
            //    File.Delete(imageURL);
            //}
            if (File.Exists(thumbnailImageURL))
            {
                File.Delete(thumbnailImageURL);
            }
        }

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
            if (itemId == -1) { focusScript = "document.getElementById('" + txtSign.ClientID + "').focus();"; }

            string hookupInputScript = "<script type=\"text/javascript\">"
                + "new UrlHelper( "
                + "document.getElementById('" + txtSign.ClientID + "'),  "
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
            if (!Save()) return;
            WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
        }
        private void btnDel_Click(object sender, System.EventArgs e)
        {
            Documentation.Delete(itemId);
            string redirectUrl = SiteRoot + "/Document/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, redirectUrl);
        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnDel.Click += new EventHandler(btnDel_Click);
            SiteUtils.SetupEditor(edContent);
            //SiteUtils.SetupEditor(edSummary);
        }

        #endregion
    }
}
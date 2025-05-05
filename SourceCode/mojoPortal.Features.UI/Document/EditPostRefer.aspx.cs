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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using Resources;
using System.IO;
using Brettle.Web.NeatUpload;
using mojoPortal.Features;
using mojoPortal.Web.Editor;



namespace DocumentFeature.UI
{

    public partial class EditPostReferPage : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        protected int langId = -1;
        protected int rootItemId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private DocumentConfiguration config = new DocumentConfiguration();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private DocumentReference dc;
        private Documentation documentRoot;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string dateTimeFormat;
        string fileName;
        string name;
        // replace this with your own feature guid or make a static property on one of your business objects
        // like MyFeature.FeatureGuid, then you can use that instead of this variable
        private Guid featureGuid = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();

            // one of these may be usefull
            //if (!UserCanViewPage(moduleId, featureGuid))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}
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
            if (dc != null)
            {
                rootItemId = dc.RootItemID;
                ddlLinhVuc.SelectedValue = dc.LinhVuc.ToString();
                ddlLoaiVB.SelectedValue = dc.LoaiVB.ToString();
                ddlCoQuanID.SelectedValue = dc.CoQuanID.ToString();
                edSummary.Text = dc.Summary.ToString();
                txtSign.Text = dc.Sign.ToString();
                dpDatePromulgate.Text = string.Format("{0:d/M/yyyy}", dc.DatePromulgate);
                dpDateEffect.Text = string.Format("{0:d/M/yyyy}", dc.DateEffect);
                txtSigner.Text = dc.Signer.ToString();
                txtItemUrl.Text = dc.ItemUrl.ToString();
                txtCreatedByUser.Text = dc.CreatedByUser.ToString();
                chkIsApproved.Checked = dc.IsApproved;
            }
            else if (dc == null)
            {
                if (documentRoot != null)
                {
                    ddlLinhVuc.SelectedValue = documentRoot.LinhVuc.ToString();
                    ddlLoaiVB.SelectedValue = documentRoot.LoaiVB.ToString();
                    ddlCoQuanID.SelectedValue = documentRoot.CoQuanID.ToString();
                }
            }
        }
        private void bindLinhVuc()
        {
            List<LinhVuc> linhvuc = LinhVuc.GetAll(siteSetting.SiteId);
            ddlLinhVuc.DataValueField = "ItemID";
            if (langId == LanguageConstant.VN)
            {
                ddlLinhVuc.DataTextField = "Name";
            }
            else
            {
                ddlLinhVuc.DataTextField = "NameEN";
            }
            ddlLinhVuc.DataSource = linhvuc;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("--Chọn lĩnh vực--", "0"));
        }
        private void bindLoaiVB()
        {
            List<LoaiVB> loaivb = LoaiVB.GetAll(siteSetting.SiteId);
            ddlLoaiVB.DataValueField = "ItemID";
            if (langId == LanguageConstant.VN)
            {
                ddlLoaiVB.DataTextField = "Name";
            }
            else
            {
                ddlLoaiVB.DataTextField = "NameEN";
            }
            ddlLoaiVB.DataSource = loaivb;
            ddlLoaiVB.DataBind();
            ddlLoaiVB.Items.Insert(0, new ListItem("--Chọn loại văn bản--", "0"));
        }
        private void bindCoQuan()
        {
            List<CoQuan> cq = CoQuan.GetAll(siteSetting.SiteId);
            ddlCoQuanID.DataValueField = "ItemID";
            if (langId == LanguageConstant.VN)
            {
                ddlCoQuanID.DataTextField = "Name";
            }
            else
            {
                ddlCoQuanID.DataTextField = "NameEN";
            }
            ddlCoQuanID.DataSource = cq;
            ddlCoQuanID.DataBind();
            ddlCoQuanID.Items.Insert(0, new ListItem("--Chọn cơ quan ban hành--", "0"));
        }
        private string SuggestUrl()
        {
            string pageName = txtSign.Text;
            return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }
        private void Save()
        {
            if (dc == null)
            {
                DocumentReference document = new DocumentReference(itemId);

                document.SiteID = siteSettings.SiteId;
                document.PageID = pageId;
                document.ModuleID = moduleId;
                document.RootItemID = rootItemId;
                document.Summary = edSummary.Text;
                document.Sign = txtSign.Text;
                if (documentRoot != null)
                {
                    document.FilePath = documentRoot.FilePath;
                }
                DateTime localTime = DateTime.Parse(dpDatePromulgate.Text, CultureInfo.CurrentCulture);
                document.DatePromulgate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                string year = localTime.ToString("yyyy", CultureInfo.CurrentCulture);
                document.YearPromulgate = int.Parse(year);
                if (!string.IsNullOrEmpty(dpDateEffect.Text))
                {
                    DateTime localEndTime = DateTime.Parse(dpDateEffect.Text, CultureInfo.CurrentCulture);
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
                            name = guid + fileExtension;
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
                    return;
                }

                if (!friendlyUrl.FoundFriendlyUrl)
                {
                    if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString))
                    {
                        //lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                        return;
                    }
                }

                string oldUrl = document.ItemUrl.Replace("~/", string.Empty);
                string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));
                document.ItemUrl = "~/" + newUrl;
                document.CoQuanID = int.Parse(ddlCoQuanID.SelectedValue);
                document.LoaiVB = int.Parse(ddlLoaiVB.SelectedValue);
                document.LinhVuc = int.Parse(ddlLinhVuc.SelectedValue);
                document.CreatedByUser = txtCreatedByUser.Text;
                document.IsApproved = chkIsApproved.Checked;
                document.CreatedByUserGuid = siteUser.UserGuid;
                document.ApprovedDate = DateTime.UtcNow;
                document.ApprovedGuid = siteUser.UserGuid;
                document.LangID = langId;
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
                                      + "&itemRefer=" + document.ItemID.ToInvariantString()
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
                dc.SiteID = siteId;
                dc.PageID = pageId;
                dc.ModuleID = moduleId;
                dc.RootItemID = rootItemId;
                dc.Summary = edSummary.Text;
                dc.Sign = txtSign.Text;
                dc.DatePromulgate = ToDataTime(dpDatePromulgate.Text);
                //DateTime localTime = DateTime.ParseExact(dpDatePromulgate.Text, "M/d/yyyy h:mm", CultureInfo.InvariantCulture);
                //dc.DatePromulgate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                string year = string.Format("{0:yyyy}", ToDataTime(dpDateEffect.Text));
                dc.YearPromulgate = int.Parse(year);
                if (!string.IsNullOrEmpty(dpDateEffect.Text))
                {
                    dc.DateEffect = ToDataTime(dpDateEffect.Text);
                }
                dc.Signer = txtSigner.Text;
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
                    return;
                }

                if (!friendlyUrl.FoundFriendlyUrl)
                {
                    if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString))
                    {
                        //lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                        return;
                    }
                }

                string oldUrl = dc.ItemUrl.Replace("~/", string.Empty);
                string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));
                dc.ItemUrl = "~/" + newUrl;
                dc.CoQuanID = int.Parse(ddlCoQuanID.SelectedValue);
                dc.LoaiVB = int.Parse(ddlLoaiVB.SelectedValue);
                dc.LinhVuc = int.Parse(ddlLinhVuc.SelectedValue);
                dc.CreatedByUser = txtCreatedByUser.Text;
                dc.IsApproved = chkIsApproved.Checked;
                dc.CreatedByUserGuid = siteUser.UserGuid;
                dc.ApprovedDate = DateTime.UtcNow;
                dc.ApprovedGuid = siteUser.UserGuid;
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
                                      + "&itemRefer=" + dc.ItemID.ToInvariantString()
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

        }
        public DateTime? ToDataTime(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('/');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]) && !string.IsNullOrEmpty(date[2]))
                    {
                        var day = int.Parse(date[0]).ToString("00");
                        var month = int.Parse(date[1]).ToString("00");
                        var year = int.Parse(date[2]).ToString("0000");
                        return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        private void PopulateLabels()
        {
            btnSubmit.Text = BannerResources.btnSubmit;
            btnDel.Text = BannerResources.btnDel;
        }

        private void LoadSettings()
        {

            if (itemId > -1)
            {
                dc = new DocumentReference(itemId);
                if (dc.ModuleID != moduleId) { dc = null; }
                if (dc != null) { rootItemId = dc.RootItemID; }
            }
            if (rootItemId > -1)
            {
                documentRoot = new Documentation(rootItemId);
                if (documentRoot.ModuleID != moduleId) { documentRoot = null; }
            }
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            rootItemId = WebUtils.ParseInt32FromQueryString("rootItemId", rootItemId);
            langId = WebUtils.ParseInt32FromQueryString("langId", langId);
            siteId = siteUser.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
        }

        private void DeleteFileFromServer()
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
                + "\"" + siteSettings.SiteRoot + "/UIUtils/BlogUrlSuggestService.ashx" + "\""
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
            Save();
            WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        #endregion
    }
}
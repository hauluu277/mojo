// Author:					HiNet
// Created:					2015-3-12
// Last Modified:			2015-3-12
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Net;
using mojoPortal.SearchIndex;
using mojoPortal.Web;
using mojoPortal.Web.Editor;
using mojoPortal.Web.Framework;
using Resources;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BannerFeature.UI
{

    public partial class EditPost : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;
        protected int hitcount = 0;
        protected int number = 0;
        protected Banner banner;
        protected bool isAdmin;
        protected string BuildFlashObject = string.Empty;
        private int pageNumber = 1;
        private const int pageSize = 10;
        private int totalPages = 1;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private BannerConfiguration config = new BannerConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        public Dictionary<int, string> FileType = new Dictionary<int, string>();
        private string dateTimeFormat;
        string fileName;
        string widthImage;
        string name;
        // replace this with your own feature guid or make a static property on one of your business objects
        // like MyFeature.FeatureGuid, then you can use that instead of this variable
        private Guid featureGuid = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            LoadParams();

            // one of these may be usefull
            //if (!UserCanViewPage(moduleId, featureGuid))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}
            if (!UserCanEditModule(moduleId, featureGuid))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            LoadSettings();
            PopulateFileType();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }

        }
        private void PopulateFileType()
        {
            FileType.Add(Constant.DefaultID, Constant.Default);
            FileType.Add(Constant.ImageID, Constant.Image);
            FileType.Add(Constant.FlashID, Constant.Flash);
        }
        private void binfile()
        {
            ddlfiletype.DataValueField = "Key";
            ddlfiletype.DataTextField = "Value";
            ddlfiletype.DataSource = FileType;
            ddlfiletype.DataBind();
        }
        private void PopulateControls()
        {
            binfile();
            nuFilePath.Visible = false;
            pnUpflash.Visible = false;
            txtCreatedByUser.Text = siteUser.Name;
            if (!Request.IsAuthenticated)
            {
                txtCreatedByUser.Text = siteUser.Name;
            }
            if (banner != null)
            {
                txtName.Text = banner.Name.ToString();
                txtDescription.Text = banner.Description.ToString();
                txtLink.Text = banner.Link.ToString();
                if (banner.IsImage == true)
                {
                    ddlfiletype.SelectedValue = Convert.ToString(Constant.ImageID);
                    nuFilePath.Visible = true;
                    pnUpflash.Visible = false;
                }
                else if (banner.IsImage == false)
                {
                    ddlfiletype.SelectedValue = Convert.ToString(Constant.FlashID);
                    nuFilePath.Visible = false;
                    pnUpflash.Visible = true;
                }
                dpBeginDate1.Text = banner.StartDate.ToString("dd/MM/yyyy");
                if (banner.EndDate != null)
                {
                    dpEndDate2.Text = banner.EndDate.Value.ToString("dd/MM/yyyy");
                }
                if ((!string.IsNullOrEmpty(banner.Path)) && (banner.IsImage == true))
                {
                    divImage.Visible = true;
                    imgView.ImageUrl = "~/" + ConfigurationManager.AppSettings["BannerImagesFolder"] + banner.Path;
                }
                else if ((!string.IsNullOrEmpty(banner.Path)) && (banner.IsImage == false))
                {
                    var buildFlashObject = "<embed width='100' height='70'  align='top' quality='high' wmode='opaque' allowscriptaccess='always' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' src='{0}/Data/Images/Banner/{1}'></embed>";
                    BuildFlashObject = string.Format(buildFlashObject, SiteRoot, banner.Path);
                    imgView.Visible = false;
                }
                else { divImage.Visible = false; }
                chkIsFollow.Checked = banner.IsFollow;
                chkIsTarget.Checked = banner.IsTarget;
                chkIsPublic.Checked = banner.IsPublic;
                cbkNoClick.Checked = banner.NoClick;
                txtCreatedByUser.Text = banner.CreatedByUser;
                txtPriority.Text = banner.Priority.ToString();
            }
            else if (banner == null)
            {
                txtPriority.Text = "1";
                dpBeginDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnDel.Visible = false;
                chkIsPublic.Checked = true;
                return;
            }
        }
        private void PopulateLabels()
        {
            rfvName.ErrorMessage = BannerResources.NameRequiredLabel;
            rfvLink.ErrorMessage = BannerResources.LinkRequiredLabel;
            reqStartDate.ErrorMessage = BannerResources.StartDateRequiredLabel;
            dateValidator.ErrorMessage = BannerResources.DateFailFormatLabel;
            dateEndValidator.ErrorMessage = BannerResources.DateFailFormatLabel;
            cvPriority.ErrorMessage = BannerResources.IntergerRequiredLabel;
            btnCancel.PostBackUrl = SiteRoot + "/Banner/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            btnDeleteImg.ImageUrl = "~/Data/SiteImages/delete.gif";
            btnDeleteImg.ToolTip = ArticleResources.ArticleDeleteLinkText;
            UIHelper.DisableButtonAfterClick(
            btnSubmit,
            ArticleResources.ButtonDisabledPleaseWait,
            Page.ClientScript.GetPostBackEventReference(btnSubmit, string.Empty)
            );
            if (itemId > -1)
            {
                btnSubmit.Text = "Cập nhật";
                lblTitle.Text = "Cập nhật banner";
                Title = SiteUtils.FormatPageTitle(siteSettings, "Cập nhật thông tin banner");
            }
            else
            {
                btnSubmit.Text = "Thêm mới";
                lblTitle.Text = "Thêm mới banner";
                Title = SiteUtils.FormatPageTitle(siteSettings, "Thêm mới banner");

            }
            btnCancel.Text = "Quay lại";
            btnDel.Text = "Xóa banner";
            UIHelper.AddConfirmationDialog(btnDel, "Bạn có chắc chắn muốn xoá ?");
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new BannerConfiguration(getModuleSettings);
            if (itemId > -1)
            {
                banner = new Banner(itemId);
                if (banner.ModuleID != moduleId) { banner = null; }
            }
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            siteId = siteSettings.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
            dateTimeFormat = config.DateTimeFormat.ToString();
        }


        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnDel.Click += new EventHandler(btnDel_Click);
            btnDeleteImg.Click += btnDeleteImg_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            SiteUtils.RedirectToUrl(SiteRoot + "/Banner/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId);
        }

        private bool Save()
        {
            string startDate = DateTime.Now.ToString();
            string endDate = DateTime.Now.ToString();
            //var listPriority = Banner.GetPage(siteSettings.SiteId, moduleId, pageNumber, config.PageSize, true, "", out totalPages).Select(x => x.Priority).ToList();
            //var MaxPriority = listPriority.Distinct().Max();
            if (txtPriority.Text.ToIntOrZero() == 1)
            {
                //update thứ tự lại hiện thị trong db
                var listBanner = Banner.GetByModule(moduleId);
                if (listBanner != null && listBanner.Count > 0)
                {
                    foreach (var item in listBanner)
                    {
                        item.Priority = (item.Priority + 1);
                        item.Save();
                    }
                }
            }
            if (banner == null)
            {
                Banner bn = new Banner();
                bn.PageID = pageId;
                bn.SiteID = siteId;
                bn.ModuleID = moduleId;
                bn.Name = txtName.Text;
                bn.Description = txtDescription.Text;
                if (ddlfiletype.SelectedValue == Convert.ToString(Constant.ImageID))
                {
                    bn.IsImage = true;
                    if (!SaveImageUrl(out fileName, out widthImage))
                    {
                        lblImageUrlError.Text = "Bạn chưa chọn file tải lên";
                        return false;
                    }
                    if (!fileName.Equals(string.Empty))
                    {
                        bn.Path = fileName;
                    }
                    if (!string.IsNullOrEmpty(widthImage))
                    {
                        bn.Width = widthImage;
                    }
                }
                else
                {
                    bn.Path = txtfile.Text;
                    bn.IsImage = false;
                }
                if (!string.IsNullOrEmpty(dpBeginDate1.Text))
                {
                    startDate = dpBeginDate1.Text;
                    if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                    {
                        var formatDate = startDate.ToDateTimeEn();
                        bn.StartDate = startDate.ToDateTime().Value;
                    }
                    else
                    {
                        bn.StartDate = DateTime.Parse(startDate, CultureInfo.CurrentCulture);
                    }
                }

                if (!string.IsNullOrEmpty(dpEndDate2.Text))
                {
                    if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                    {
                        var formatDate = dpEndDate2.Text;
                        bn.EndDate = formatDate.ToDateTime().Value;
                    }
                    else
                    {
                        bn.EndDate = DateTime.Parse(dpEndDate2.Text, CultureInfo.CurrentCulture);
                    }
                }
                else
                {
                    bn.EndDate = null;
                }
                bn.CreatedByUser = txtCreatedByUser.Text;
                bn.Link = txtLink.Text;
                //Banner.Path = txtPath.Text;
                //Banner.IsImage = chkIsImage.Checked;
                bn.HitCount = hitcount;
                bn.Number = number;
                //if (!string.IsNullOrEmpty(txtPriority.Text))
                //{
                //    bn.Priority = int.Parse(txtPriority.Text);
                //}
                //bn.IsHorizontal = chkIsHorizontal.Checked;

                //bn.Priority = MaxPriority + 1;
                if (!string.IsNullOrEmpty(txtPriority.Text))
                {
                    bn.Priority = txtPriority.Text.ToIntOrZero();
                }
                bn.IsFollow = chkIsFollow.Checked;
                bn.IsTarget = chkIsTarget.Checked;
                bn.IsPublic = chkIsPublic.Checked;
                bn.CreatedDate = DateTime.Now;
                bn.NoClick = cbkNoClick.Checked;
                bn.Save();
            }
            else
            {
                banner.PageID = pageId;
                banner.SiteID = siteId;
                banner.ModuleID = moduleId;
                banner.Name = txtName.Text;
                banner.Description = txtDescription.Text;
                if (ddlfiletype.SelectedValue == Convert.ToString(Constant.ImageID))
                {
                    banner.IsImage = true;
                    if (string.IsNullOrEmpty(banner.Path))
                    {
                        if (!SaveImageUrl(out fileName, out widthImage))
                        {
                            return false;
                        }
                        if (!fileName.Equals(string.Empty))
                        {
                            banner.Path = fileName;
                        }
                    }
                }
                else
                {
                    banner.Path = txtfile.Text;
                    banner.IsImage = false;
                }
                if (!string.IsNullOrEmpty(dpBeginDate1.Text))
                {

                    startDate = dpBeginDate1.Text;
                    if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                    {
                        banner.StartDate = startDate.ToDateTime().Value;
                    }
                    else
                    {
                        banner.StartDate = DateTime.Parse(startDate, CultureInfo.CurrentCulture);
                    }

                }
                if (!string.IsNullOrEmpty(dpEndDate2.Text))
                {
              

                    if (SiteUtils.GetDefaultCulture().ToString().ToLower().Equals("en-us"))
                    {
                        var formatDate = dpEndDate2.Text;
                        banner.EndDate = formatDate.ToDateTime().Value;
                    }
                    else
                    {
                        banner.EndDate = DateTime.Parse(dpEndDate2.Text, CultureInfo.CurrentCulture);
                    }
                }
                else
                {
                    banner.EndDate = null;
                }
                if (!string.IsNullOrEmpty(txtPriority.Text))
                {
                    banner.Priority = txtPriority.Text.ToIntOrZero();
                }
                //banner.Priority = MaxPriority + 1;

                banner.Link = txtLink.Text;
                banner.CreatedByUser = txtCreatedByUser.Text;
                //Banner.Path = txtPath.Text;
                //Banner.IsImage = chkIsImage.Checked;
                //banner.IsHorizontal = chkIsHorizontal.Checked;
                banner.IsFollow = chkIsFollow.Checked;
                banner.IsTarget = chkIsTarget.Checked;
                banner.IsPublic = chkIsPublic.Checked;
                banner.NoClick = cbkNoClick.Checked;
                banner.Save();
            }
            return true;
        }
        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (!Save()) return;
            string redirectUrl = SiteRoot + "/banner/managepost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, redirectUrl);
        }
        private void btnDel_Click(object sender, System.EventArgs e)
        {
            if (banner == null) { return; }
            Banner.Delete(banner.ItemID);
            string redirectUrl = SiteRoot + "/banner/managepost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, redirectUrl);
        }
        private void btnDeleteImg_Click(object sender, ImageClickEventArgs e)
        {
            if (banner == null) { banner = new Banner(itemId); }
            DeleteImageFromServer();
            divImage.Visible = false;
            banner.Path = string.Empty;
            banner.Save();
        }
        private bool SaveImageUrl(out string fileName, out string widthImage)
        {
            String pathToApplicationsFolder
                = HttpContext.Current.Server.MapPath(
                "~/" + ConfigurationManager.AppSettings["BannerImagesFolder"]);
            if (!Directory.Exists(pathToApplicationsFolder))
            {
                Directory.CreateDirectory(pathToApplicationsFolder);
            }
            bool flag = false;
            int width = 0;
            int height = 0;
            fileName = string.Empty;
            widthImage = string.Empty;
            try
            {
                //Check valid file upload
                if (nuFilePath.HasFile && nuFilePath.ContentLength > 0)
                {

                    string fileExtension = Path.GetExtension(nuFilePath.FileName);
                    Double fileSize = nuFilePath.ContentLength / 1024;

                    //Kiem tra ten mo rong file upload
                    if (!SiteUtils.IsValidFileExtension(fileExtension, "AllowedImageFileExtensions"))
                    {
                        lblImageUrlError.Text = BannerResources.ImageUrlErrorFileExtension;
                        return false;
                    }
                    //Kiem tra kich thuoc file upload
                    if (!SiteUtils.IsValidFileSize(fileSize, "AllowedImageSize"))
                    {
                        lblImageUrlError.Text = BannerResources.ImageUrlErrorFileSize + ConfigurationManager.AppSettings["AllowedImageSize"] + @" KB";
                        return false;
                    }
                    string path = Server.MapPath("~/" + ConfigurationManager.AppSettings["BannerImagesFolder"]);
                    string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    System.Drawing.Image image = System.Drawing.Image.FromStream(nuFilePath.FileContent);
                    nuFilePath.FileContent.Close();
                    int resizeWidth;
                    int.TryParse(ConfigurationManager.AppSettings["BannerImageMaxWidth"], out resizeWidth);
                    int resizeHeight;
                    int.TryParse(ConfigurationManager.AppSettings["BannerImageMaxHeight"], out resizeHeight);
                    int thumbnailWidth;
                    int.TryParse(ConfigurationManager.AppSettings["BannerImageMaxThumbnailWidth"], out thumbnailWidth);
                    int thumbnailHeight;
                    int.TryParse(ConfigurationManager.AppSettings["BannerImageMaxThumbnailHeight"], out thumbnailHeight);
                    //SiteUtils.ResizeImage(ref width, ref height, resizeWidth, resizeHeight, image.Width, image.Height);
                    fileName = path + guid + "_t" + fileExtension;
                    if (height != 0)
                    {
                        using (Bitmap bitmap = new Bitmap(image, width, height))
                        {
                            widthImage = image.Width.ToString();
                            bitmap.Save(fileName, image.RawFormat);
                        }
                    }
                    else
                    {
                        using (Bitmap bitmap = new Bitmap(image, image.Width, image.Height))
                        {
                            widthImage = image.Width.ToString();
                            bitmap.Save(fileName, image.RawFormat);
                        }
                        //nuImageUrl.MoveTo(fileName, MoveToOptions.Overwrite);
                        //fuImageUrl.PostedFile.SaveAs(fileName);
                    }
                    //SiteUtils.ResizeImage(ref width, ref height, thumbnailWidth, thumbnailHeight, image.Width, image.Height);
                    fileName = path + guid + fileExtension;
                    if (height != 0)
                    {
                        using (Bitmap bitmap = new Bitmap(image, width, height))
                        {
                            bitmap.Save(fileName, image.RawFormat);
                        }
                    }
                    else
                    {
                        using (Bitmap bitmap = new Bitmap(image, image.Width, image.Height))
                        {
                            bitmap.Save(fileName, image.RawFormat);
                        }
                        //nuImageUrl.MoveTo(fileName, MoveToOptions.Overwrite);
                        //fuImageUrl.PostedFile.SaveAs(fileName);
                    }
                    fileName = guid + fileExtension;
                    if (banner != null && !banner.Path.Equals(string.Empty))
                    {
                        DeleteImageFromServer();
                    }
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception e)
            {
                lblImageUrlError.Visible = true;
                lblImageUrlError.Text = e.Message;//"Error when upload image";
            }
            return flag;

        }
        private void DeleteImageFromServer()
        {
            string thumbnailImageURL = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["BannerImagesFolder"] + banner.Path;
            thumbnailImageURL = thumbnailImageURL.Replace("/", "\\");
            string imageURL = thumbnailImageURL.Substring(0, thumbnailImageURL.LastIndexOf(".")) + "_t" + thumbnailImageURL.Substring(thumbnailImageURL.LastIndexOf("."));
            if (File.Exists(imageURL))
            {
                File.Delete(imageURL);
            }
            if (File.Exists(thumbnailImageURL))
            {
                File.Delete(thumbnailImageURL);
            }
        }
        protected void btlTaiLen_Click(object sender, EventArgs e)
        {
            HttpPostedFile files = uploadFlash.PostedFile;
            if (uploadFlash.HasFile == false || files.ContentLength > 20480)
            {
                lblImageUrlError.Text = "file không hợp lệ";
            }
            else
            {
                string fileExtension = Path.GetExtension(uploadFlash.FileName);
                string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                if (fileExtension.ToLower() == ".swf")
                {
                    try
                    {
                        name = guid + fileExtension;
                        uploadFlash.SaveAs(Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["BannerImagesFolder"] + name);
                        txtfile.Text = name;
                    }
                    catch
                    {
                        lblImageUrlError.Text = "flash tải lên bị trùng.";
                    }
                }
            }
        }
        protected void ddlfiletype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlfiletype.SelectedValue == Convert.ToString(Constant.ImageID))
            {
                nuFilePath.Visible = true;
                pnUpflash.Visible = false;
            }
            else
            {
                if (ddlfiletype.SelectedValue == Convert.ToString(Constant.FlashID))
                {
                    pnUpflash.Visible = true;
                    nuFilePath.Visible = false;
                }
                else
                {
                    pnUpflash.Visible = false;
                    nuFilePath.Visible = false;
                }
            }

        }
        #endregion
    }
}
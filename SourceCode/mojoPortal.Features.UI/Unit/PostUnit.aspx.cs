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
using System.Web.Services;
using mojoPortal.Web.Editor;
using mojoPortal.Business.WebHelpers.CommonModel;

namespace UnitFeatures.UI
{
    public partial class PostUnit : mojoBasePage
    {
        protected int moduleId = -1;
        protected int siteId = -1;
        protected int itemId = -1;
        protected int pageId = -1;

        protected String cacheDependencyKey;
        protected string virtualRoot;
        protected Double timeOffset;
        protected Hashtable moduleSettings;
        protected FunctionalUnitConfiguration config = new FunctionalUnitConfiguration();
        private const int pageSize = 10;
        private Guid restoreGuid = Guid.Empty;
        private md_FunctionalUnit _functionalUnit;
        private md_Officer _officer;
        protected bool isAdmin;
        readonly ContentMetaRespository metaRepository = new ContentMetaRespository();
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly static SiteUser _staticSiteUser = SiteUtils.GetCurrentSiteUser();

        public SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        #region OnInit
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
            btnDelete.Click += btnDelete_Click;
            //btnDeleteImg.Click += btnDeleteImg_Click;
            SiteUtils.SetupEditor(edGeneral);
            SiteUtils.SetupEditor(edFunctionP);
            SiteUtils.SetupEditor(edMission);
            SiteUtils.SetupEditor(edAchievement);
            SiteUtils.SetupEditor(edProcedureP);
            SiteUtils.SetupEditor(edContact);

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
            config = new FunctionalUnitConfiguration(moduleSettings);
            if (itemId > 0)
            {
                _functionalUnit = new md_FunctionalUnit(itemId);
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) { SiteUtils.RedirectToLoginPage(this); return; }
            if (itemId <= 0 && siteUser.IsInRoles(WebConfigSettings.RoleManagePhongBan))
            {
                //CONTINUDE
            }
            else
            {
                if (itemId > 0)
                {
                    //CONTINUDE
                }
                else
                {
                    SiteUtils.RedirectToAccessDeniedPage(this); return;
                }
            }
            //if (!siteUser.IsInRoles("Admins") && !siteUser.IsInRoles(config.RoleSetting)) { SiteUtils.RedirectToAccessDeniedPage(this); return; }
            pnlAllowUserModify.Visible = false;
            if (siteUser.IsInRoles(WebConfigSettings.RoleManagePhongBan) || siteUser.IsInRoles("Admins"))
            {
                pnlAllowUserModify.Visible = true;
            }
            if (itemId > 0)
            {
                if (siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleManagePhongBan) || siteUser.UserId == _functionalUnit.AllowUserModify)
                {
                    //CONTINUDE
                }
                else
                {
                    SiteUtils.RedirectToAccessDeniedPage(this); return;
                }

                lblTitle.Text = "Cập nhật đơn vị chức năng";
                Title = SiteUtils.FormatPageTitle(siteSettings, "Cập nhật đơn vị chức năng");
                btnUpdate.Text = "Cập nhật";
            }
            else
            {
                lblTitle.Text = "Thêm mới đơn vị chức năng";
                Title = SiteUtils.FormatPageTitle(siteSettings, "Thêm mới đơn vị chức năng");
                btnUpdate.Text = "Thêm mới";
            }
            lnkCancel.Text = "Quay lại";
            lnkCancel.NavigateUrl = SiteRoot + "/FunctionalUnit/Manage.aspx?pageid=" + pageId + "&mid=" + moduleId;

            SiteUtils.AddConfirmButton(btnDelete, "Dữ liệu xóa sẽ không khôi phục được, xác nhận xóa?");

            PopulateLabels();
            SetupScripts();
            if ((Request.UrlReferrer != null) && (hdnReturnUrl.Value.Length == 0))
            {
                hdnReturnUrl.Value = Request.UrlReferrer.ToString();
                lnkCancel.NavigateUrl = Request.UrlReferrer.ToString();
                //lnkCancel3.NavigateUrl = lnkCancel.NavigateUrl;

            }
            if (!Page.IsPostBack)
            {
                LoadUser();
                PopulateControls();
            }
        }

        protected virtual void PopulateControls()
        {

            btnDelete.Visible = false;

            dpCreateDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            chkIsPubshed.Checked = true;
            if (_functionalUnit != null)
            {
                if (siteUser.IsInRoles(WebConfigSettings.RoleManagePhongBan) || siteUser.IsInRoles("Admins"))
                {
                    ddlUser.SelectedValue = _functionalUnit.AllowUserModify.ToString();
                }
                chkIsShowQuestion.Checked = _functionalUnit.IsShowQuestion.GetValueOrDefault(false);

                edLichCongTac.Text = _functionalUnit.LichCongTac;
                chkIsPubshed.Checked = _functionalUnit.IsPublished.GetValueOrDefault(false);
                txtTitle.Text = _functionalUnit.Title;
                edGeneral.Text = _functionalUnit.General;
                edFunctionP.Text = _functionalUnit.FunctionP;
                edMission.Text = _functionalUnit.Mission;
                edAchievement.Text = _functionalUnit.Achievement;
                edProcedureP.Text = _functionalUnit.ProcedureP;
                edContact.Text = _functionalUnit.Contact;
                dpCreateDate.Text = string.Format("{0:dd/MM/yyyy}", _functionalUnit.CreateDate);
                txtItemUrl.Text = _functionalUnit.UrlItem;
                txtMaPhong.Text = _functionalUnit.MaKhoaPhong;
                //chkIsPubshed.Checked=_functionalUnit.
                if (!string.IsNullOrEmpty(_functionalUnit.CreateByName))
                {
                    txtCreator.Text = _functionalUnit.CreateByName;
                }
                if (_functionalUnit.IsPublished.HasValue)
                {
                    chkIsPubshed.Checked = _functionalUnit.IsPublished.Value;
                }
                hdnTitle.Value = _functionalUnit.Title;
            }
            else
            {
                chkIsPubshed.Checked = true;
                dpCreateDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                btnDelete.Visible = false;
                if (Request.IsAuthenticated && siteUser != null)
                {
                    txtCreator.Text = siteUser.Name;
                }
                if ((txtItemUrl.Text.Length == 0) && (txtTitle.Text.Length > 0))
                {
                    string friendlyUrl = txtTitle.Text.UrlRewriteDefault();
                    txtItemUrl.Text = @"~/" + friendlyUrl;
                }

            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var listOfficer = md_Officer.GetList(itemId);
            md_Officer.Delete(listOfficer);
            string url = SiteRoot + "/FunctionalUnit/Manage.aspx?pageid=" + pageId + "&mid=" + moduleId;
            SiteUtils.RedirectToUrl(url);
        }

        protected virtual void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate("save_functionalunit");
            if (!Page.IsValid) return;
            if (!Save()) return;
            string url = SiteRoot + "/Functionalunit/Manage.aspx?pageid=" + pageId + "&mid=" + moduleId;
            SiteUtils.RedirectToUrl(SiteRoot + _functionalUnit.UrlItem.Replace("~", string.Empty));
        }

        private bool ParamsAreValid()
        {
            try
            {
                DateTime.Parse(dpCreateDate.Text);
            }
            catch (FormatException)
            {
                lblErrorMessage.Text = "Thời gian bạn nhập không đúng định dạng";
                return false;
            }
            catch (ArgumentNullException)
            {
                lblErrorMessage.Text = "Thời gian bạn nhập không đúng định dạng";
                return false;
            }
            return true;
        }
        private string SuggestUrl()
        {
            string pageName = txtTitle.Text;
            return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }

        private bool Save()
        {
            if (_functionalUnit == null)
            {
                _functionalUnit = new md_FunctionalUnit(itemId);
                _functionalUnit.Creator = siteUser.UserId;
                _functionalUnit.CreateDate = DateTime.Now;
            }
            Module module = GetModule(moduleId);
            if (moduleId > 0)
            {
                _functionalUnit.ModuleID = moduleId;
                _functionalUnit.Editor = siteUser.UserId;
                _functionalUnit.EditDate = DateTime.Now;
            }
            if (siteUser == null)
            {
                return false;
            }

            _functionalUnit.IsShowQuestion = chkIsShowQuestion.Checked;
            _functionalUnit.LichCongTac = edLichCongTac.Text;
            _functionalUnit.SiteID = siteSetting.SiteId;
            _functionalUnit.Title = txtTitle.Text;
            _functionalUnit.General = edGeneral.Text;
            _functionalUnit.FunctionP = edFunctionP.Text;
            _functionalUnit.Mission = edMission.Text;
            _functionalUnit.Achievement = edAchievement.Text;
            _functionalUnit.ProcedureP = edProcedureP.Text;
            _functionalUnit.Contact = edContact.Text;
            _functionalUnit.IsPublished = chkIsPubshed.Checked;
            _functionalUnit.OrderByUnit = txtOrderBy.ToIntOrZero();
            _functionalUnit.MaKhoaPhong = txtMaPhong.Text;
            if (siteUser.IsInRoles(WebConfigSettings.RoleManagePhongBan) || siteUser.IsInRoles("Admins"))
            {
                _functionalUnit.AllowUserModify = ddlUser.SelectedValue.ToShortOrZero();
            }
            if (txtItemUrl.Text.Length == 0)
            {
                txtItemUrl.Text = SuggestUrl();
            }
            String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", String.Empty));
            FriendlyUrl friendlyUrl = new FriendlyUrl(siteSettings.SiteId, friendlyUrlString);

            if (
                (friendlyUrl.FoundFriendlyUrl)
                && (_functionalUnit.UrlItem != txtItemUrl.Text)
                )
            {
                lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                return false;
            }

            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString))
                {
                    lblError.Text = ArticleResources.PageUrlInUseBlogErrorMessage;
                    return false;
                }
            }
            string oldUrl = _functionalUnit.UrlItem.Replace("~/", string.Empty);
            string newUrl = SiteUtils.RemoveInvalidUrlChars(txtItemUrl.Text.Replace("~/", string.Empty));
            _functionalUnit.UrlItem = "~/" + newUrl;

            _functionalUnit.Save();

            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                {
                    FriendlyUrl newFriendlyUrl = new FriendlyUrl
                    {
                        SiteId = siteSettings.SiteId,
                        Url = friendlyUrlString,
                        RealUrl = "~/functionalunit/viewpost.aspx?pageid="
                                  + pageId.ToInvariantString()
                                  + "&mid=" + _functionalUnit.ModuleID.ToInvariantString()
                                  + "&ItemID=" + _functionalUnit.ItemID.ToInvariantString()
                    };

                    newFriendlyUrl.Save();
                }

                //if post was renamed url will change, if url changes we need to redirect from the old url to the new with 301
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
                    if (oldFriendlyUrl.FoundFriendlyUrl)
                    {
                        FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                    }

                }
            }
            else
            {
                if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                {
                    string realUrl = string.Empty;
                    realUrl = "~/functionalunit/viewpost.aspx?pageid="
                 + pageId.ToInvariantString()
                 + "&mid=" + _functionalUnit.ModuleID.ToInvariantString()
                 + "&ItemID=" + _functionalUnit.ItemID.ToInvariantString();
                    FriendlyUrl updateFriendlyUrl = new FriendlyUrl(friendlyUrl.UrlId);
                    if (updateFriendlyUrl != null)
                    {
                        updateFriendlyUrl.RealUrl = realUrl;
                        updateFriendlyUrl.Save();
                    }
                }
            }


            if (itemId > 0)
            {
                var dsCanBo = md_Officer.GetList(itemId);
                md_Officer.Delete(dsCanBo);
                var dsCanBold = md_Officer.GetList_ld(itemId);
                md_Officer.Delete(dsCanBold);
            }
            //Lãnh đạo
            var listCanBo_ld = hdfCanBold.Value;
            var dscanbo_ld = new JavaScriptSerializer().Deserialize<List<md_Officer>>(listCanBo_ld);
            if (dscanbo_ld != null)
            {
                foreach (var canbo_ld in dscanbo_ld)
                {
                    if (string.IsNullOrEmpty(canbo_ld.Name)) { continue; }
                    md_Officer pa = new md_Officer();
                    pa.OfficerID = _functionalUnit.ItemID;
                    pa.Name = canbo_ld.Name;
                    pa.Position = canbo_ld.Position;
                    pa.Email = canbo_ld.Email;
                    pa.Phone = canbo_ld.Phone;
                    pa.MissionOfficer = canbo_ld.MissionOfficer;
                    pa.UrlImage = canbo_ld.UrlImage;
                    pa.IsTop = PhongBanConstant.LANHDAO;
                    pa.OrderByOfficer = canbo_ld.OrderByOfficer;
                    pa.Save();
                }
            }

            //end Lãnh đạo

            //nhân viên
            var listCanBo = hdfCanBo.Value;
            var dscanbo = new JavaScriptSerializer().Deserialize<List<md_Officer>>(listCanBo);
            if (dscanbo != null)
            {
                foreach (var canbo in dscanbo)
                {
                    if (string.IsNullOrEmpty(canbo.Name)) { continue; }
                    md_Officer pa = new md_Officer();
                    pa.OfficerID = _functionalUnit.ItemID;
                    pa.Name = canbo.Name;
                    pa.Position = canbo.Position;
                    pa.Email = canbo.Email;
                    pa.Phone = canbo.Phone;
                    pa.MissionOfficer = canbo.MissionOfficer;
                    pa.UrlImage = canbo.UrlImage;
                    pa.IsTop = PhongBanConstant.NHANVIEN;
                    pa.OrderByOfficer = canbo.OrderByOfficer;
                    pa.Save();
                }
            }
            //end nhân viên 




            CurrentPage.UpdateLastModifiedTime();
            //SaveSyncPost();
            CacheHelper.TouchCacheDependencyFile(cacheDependencyKey);
            SiteUtils.QueueIndexing();

            return true;
        }


        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.EditPostPageTitle);
            moduleTitle.EditText = ArticleResources.BlogEditEntryLabel;
            if (itemId > 0)
            {
                heading.Text = "Cập nhật đơn vị chức năng";
            }
            else
            {
                heading.Text = "Thêm mới đơn vị chức năng";
            }
            progressBar.AddTrigger(btnUpdate);



            //btnUpdate.Text = ArticleResources.BlogEditUpdateButton;
            SiteUtils.SetButtonAccessKey(btnUpdate, ArticleResources.BlogEditUpdateButtonAccessKey);

            UIHelper.DisableButtonAfterClick(
                btnUpdate,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnUpdate, string.Empty)
                );

            edGeneral.WebEditor.ToolBar = ToolBar.Newsletter;
            edGeneral.WebEditor.Height = 300;

            edAchievement.WebEditor.ToolBar = ToolBar.Newsletter;
            edAchievement.WebEditor.Height = 300;

            edProcedureP.WebEditor.ToolBar = ToolBar.Newsletter;
            edProcedureP.WebEditor.Height = 300;

            edContact.WebEditor.ToolBar = ToolBar.Newsletter;
            edContact.WebEditor.Height = 300;


            lnkCancel.Text = ArticleResources.BlogEditCancelButton;
            btnDelete.Text = ArticleResources.BlogEditDeleteButton;
            SiteUtils.SetButtonAccessKey(btnDelete, ArticleResources.BlogEditDeleteButtonAccessKey);
            UIHelper.AddConfirmationDialog(btnDelete, ArticleResources.ArticleDeletePostWarning);

            rfvTitle.ErrorMessage = ArticleResources.TitleRequiredWarning;
            regexUrl.ErrorMessage = ArticleResources.FriendlyUrlRegexWarning;

            InfoLink InfoLink_1 = new InfoLink();
            InfoLink_1.NameLink = "Quản lý đơn vị chức năng";
            InfoLink_1.UrlLink = SiteRoot + "/FunctionalUnit/Manage.aspx";

            InfoLink InfoLink_2 = new InfoLink();
            InfoLink_2.ActiveLink = true;
            if (itemId > 0)
            {
                InfoLink_2.NameLink = "Cập nhật đơn vị chức năng";
                InfoLink_2.UrlLink = SiteRoot + "/FunctionalUnit/EditPost.aspx?itemid=" + itemId;
            }
            else
            {
                InfoLink_2.NameLink = "Thêm mới đơn vị chức năng";
                InfoLink_2.UrlLink = SiteRoot + "/FunctionalUnit/EditPost.aspx";
            }

            BreadCrumbControl.InfoLink_1 = InfoLink_1;
            BreadCrumbControl.InfoLink_2 = InfoLink_2;
        }

        public static int SaveCoreCategory(string coreCategoryName = "")
        {
            CoreCategory coreCategory = new CoreCategory();
            coreCategory.CreatedBy = _staticSiteUser.UserGuid;
            coreCategory.CreatedUtc = DateTime.Now;
            coreCategory.Name = coreCategoryName;
            coreCategory.ParentID = System.Configuration.ConfigurationManager.AppSettings["CategoryDoctorId"].ToIntOrZero();
            coreCategory.Save();
            return coreCategory.ItemID;
        }

        private void LoadUser()
        {
            ddlUser.DataValueField = "UserID";
            ddlUser.DataTextField = "Name";
            ddlUser.DataSource = SiteUser.GetUserBySite(siteSetting.SiteId);
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem { Text = "--Chọn người dùng--", Value = "" });
        }

        protected string GenerateIMG(string urlIMG)
        {
            string append = string.Empty;
            if (!string.IsNullOrEmpty(urlIMG))
            {
                var listIMG = urlIMG.Split(';');
                if (listIMG != null && listIMG.Count() > 0)
                {
                    foreach (var item in listIMG)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            append += "<p class='add-img'>";
                            append += "<img src = '" + item + "' width='50'>";
                            append += "<span class='text-primary pointer choose-img' title='Chọn ảnh'><i class='fa fa-upload' aria-hidden='true'></i></span>";
                            append += "&nbsp;&nbsp;";
                            append += "<span class='text-danger pointer remove-img'><i class='fa fa-trash' aria-hidden='true'></i></span>";
                            append += "</p>";
                        }
                    }
                }

            }
            return append;
        }
        private void SetupScripts()
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("friendlyurlsuggest"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "friendlyurlsuggest", "<script src=\""
                    + ResolveUrl($"{WebConfigSettings.FriendlyUrlSuggestScript}?v={siteSettings.SkinVersion}") + "\" type=\"text/javascript\"></script>");
            }

            string focusScript = string.Empty;

            if (itemId == -1)
            {
                focusScript = $"document.getElementById('{txtTitle.ClientID}').focus();";
            }

            string hookupInputScript = $@"<script type=""text/javascript"">
				new UrlHelper(
						document.getElementById('{txtTitle.ClientID}'),
						document.getElementById('{txtItemUrl.ClientID}'),
						document.getElementById('{hdnTitle.ClientID}'),
						document.getElementById('{spnUrlWarning.ClientID}'), 
						""{SiteRoot}/Blog/BlogUrlSuggestService.ashx"",
						""""
					); {focusScript}</script>";

            if (!Page.ClientScript.IsStartupScriptRegistered(UniqueID + "urlscript"))
            {
                Page.ClientScript.RegisterStartupScript(
                    GetType(),
                    UniqueID + "urlscript", hookupInputScript);
            }


        }

        public string GetIsPublished(object isPublished)
        {
            if (isPublished != null)
            {
                bool result = false;
                bool.TryParse(isPublished.ToString(), out result);
                if (result)
                {
                    return "<span class='font-bold text-success'>Lãnh đạo</span>";
                }
            }
            return "<span class='font-bold red'> </span>"; ;
        }


    }
}
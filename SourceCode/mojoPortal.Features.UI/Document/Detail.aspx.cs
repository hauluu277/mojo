// Author:					HiNet
// Created:					2015-3-23
// Last Modified:			2015-3-23
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
using mojoPortal.Features;
using System.Text.RegularExpressions;



namespace DocumentFeature.UI
{

    public partial class Detail : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int itemId = -1;
        private int referItemId = -1;
        protected int siteId = -1;
        protected string AttachImageUrl = string.Empty;
        protected string OtherImageUrl = string.Empty;
        protected string ListOtherImageUrl = string.Empty;
        protected string fileRoot = string.Empty;
        protected bool DateEffectVisible = false;
        protected string OrtherDocumentLabel = DocumentResources.DocumentOtherLabel;
        private int linhVuc;
        protected int langId = 1;
        private Documentation document;
        public SiteSettings siteSettings;
        private DocumentReference documentReference;
        private DocumentConfiguration config = new DocumentConfiguration();
        // replace this with your own feature guid or make a static property on one of your business objects
        // like MyFeature.FeatureGuid, then you can use that instead of this variable
        private Guid featureGuid = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
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
            if (!IsPostBack)
            {
                PopulateControls();
            }

        }

        private void PopulateControls()
        {
            pnlFile.Visible = false;
            if (document != null)
            {
                lbtitle.Text = document.LoaiVBName + " " + document.Sign + " " + document.CoQuanName + ": " + formatContent(document.Summary);
                lbSign.Text = document.Sign;
                if (document.DatePromulgate.HasValue)
                {
                    lbDatePromulgate.Text = document.DatePromulgate.Value.ToString("dd/MM/yyyy");
                }
                if (document.DateEffect.HasValue)
                {
                    lbDateEffect.Text = document.DateEffect.Value.ToString("dd/MM/yyyy");
                }
                lbSinger.Text = document.Signer;
                lbSummary.Text = document.Summary;
                lbCoQuan.Text = document.CoQuanName;
                lbLoaivb.Text = document.LoaiVBName;
                List<ChuDeVanBanPhapQuy> lstChuDe = ChuDeVanBanPhapQuy.GetAllByDocId(document.ItemID);
                if (lstChuDe != null && lstChuDe.Count > 0)
                {
                    int j = lstChuDe.Count;
                    int i = 1;
                    string Tenchude = string.Empty;
                    foreach (var chude in lstChuDe)
                    {
                        CoreCategory catChude = new CoreCategory(chude.ChuDeID);
                        if (catChude != null)
                        {
                            if (i == j)
                            {
                                Tenchude += catChude.Name;
                            }
                            else
                            {
                                Tenchude += catChude.Name + ", ";
                            }
                        }
                        i++;
                    }
                    lbSubject.Text = Tenchude;

                }
                if (!string.IsNullOrEmpty(document.FilePath))
                {
                    pnlFile.Visible = true;
                    lnkAttach.Text = document.FilePath;
                    lnkAttach.NavigateUrl = "/" + ConfigurationManager.AppSettings["DocumentFileFolder"] + document.FilePath;
                    lnkAttach.Visible = AttachVisible(document.FilePath);
                }
            }
            //PopulateDocument();
            PopulateDocumentOthers();
        }


        private void PopulateLabels()
        {
            //imgAttach.ImageUrl = "/Data/SiteImages/attach.gif";
            AttachImageUrl = "/Data/SiteImages/attach.gif";
            OtherImageUrl = "/Data/SiteImages/other.gif";
            ListOtherImageUrl = "/Data/SiteImages/item.gif";
        }

        private void LoadSettings()
        {
            document = new Documentation(itemId);
            documentReference = new DocumentReference(referItemId);
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new DocumentConfiguration(getModuleSettings);
            LoadSideContent(config.ShowLeftPanelSetting, config.ShowRightPanelSetting);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
        }
        //private void PopulateDocument()
        //{
        //    List<Document> reader = Document.GetById(itemId);
        //    rptArticles.DataSource = reader;
        //    rptArticles.DataBind();      
        //}
        private void PopulateDocumentOthers()
        {
            pnOthers.Visible = false;
            OrtherDocumentLabel = DocumentResources.DocumentOtherLabel;
            List<Documentation> reader = Documentation.GetById(itemId);
            foreach (var a in reader)
            {
                linhVuc = a.LinhVuc;
            }
            List<Documentation> others = Documentation.GetOthers(itemId, siteId, moduleId, linhVuc, config.DocumentOtherNumber);
            rptOthers.DataSource = others;
            rptOthers.DataBind();
            if(others.Count > 0)
            {
                pnOthers.Visible = true;
            }
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteId = siteSettings.SiteId;
            itemId = WebUtils.ParseInt32FromQueryString("item", -1);
        }
        protected string FormatUrl(int itemId)
        {
            return SiteRoot + "/Document/Detail.aspx?pageid=" + pageId + "&mid=" + moduleId + "&item=" + itemId;
        }
        protected bool AttachVisible(string filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool VisibleDateEffect(string DateEffect)
        {
            if (!string.IsNullOrEmpty(DateEffect))
            {
                DateEffectVisible = true;
                return DateEffectVisible;
            }
            else
            {
                return DateEffectVisible;
            }

        }
        protected string formatContent(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}
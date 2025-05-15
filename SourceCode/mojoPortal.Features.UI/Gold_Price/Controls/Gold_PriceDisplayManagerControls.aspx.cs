using mojoPortal.Business.WebHelpers;
using mojoPortal.Business;
using mojoPortal.Web;
using QuestionAnswerFeatures.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using QuestionAnswerFeatures.Business;
using System.Collections;

namespace Gold_PriceFeatures.UI
{

    public partial class Gold_PriceDisplayManagerControls : mojoBasePage
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private mojoBasePage basePage;
        private Module module;
        protected Gold_PriceConfiguration config = new Gold_PriceConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int groupMediaId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSetting; 
        private int orderBy = 1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        protected string DetailAnswerIMG = string.Empty;
        protected string AnswerLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        protected string StateLink = SwirlingQuestionResource.StateStatusTitle;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public Gold_PriceConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
         
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;  

        }

         
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateLabels()
        { 
            legendQuestionAnswer.InnerText = SwirlingQuestionResource.QuestionAnswerSearchTitle;
        }
        private void BindOrderBy()
        {
            var orderByStatus = SiteUtils.StringToDictionary(SwirlingQuestionResource.OrderByStatus, ","); 
        }
        private void PopulateControls()
        {
            BindQuestion();  
            BindOrderBy(); 
        }

        private void BindQuestion()
        {  
            List<md_Gold_Price> reader = md_Gold_Price.GetAll();
            rptQuestion.DataSource = reader;
            rptQuestion.DataBind();
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new Gold_PriceConfiguration(getModuleSettings); 

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        } 
    }
}
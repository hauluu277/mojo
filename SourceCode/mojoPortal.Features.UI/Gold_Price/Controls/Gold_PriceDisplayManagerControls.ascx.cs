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
using mojoPortal.Features;
using mojoPortal.Service.CommonModel.Category;
using SurveyFeature.Business;
using Brettle.Web.NeatUpload;

namespace Gold_PriceFeatures.UI
{

    public partial class Gold_PriceDisplayManagerControls : SiteModuleControl
    {/*
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private mojoBasePage basePage;
        private int pageId = -1;
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
         
        #endregion*/
        protected Gold_PriceConfiguration config = new Gold_PriceConfiguration();

        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();

        public Gold_PriceConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //LoadSettings();
            Load += Page_Load;

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateLabels()
        {
            //legendQuestionAnswer.InnerText = SwirlingQuestionResource.QuestionAnswerSearchTitle;
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
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                md_Gold_Price.Delete(itemId); // Xoá item
                BindQuestion(); // Reload lại danh sách nếu cần
            }
        }
         
    }
}
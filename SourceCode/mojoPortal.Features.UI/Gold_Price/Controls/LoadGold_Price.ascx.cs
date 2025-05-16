using Gold_PriceFeatures.UI;
using mojoPortal.Business;
using mojoPortal.Web;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gold_PriceFeatures.UI
{
    public partial class LoadGold_Price : SiteModuleControl
    {
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
            LoadSettings();
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
            List<md_Gold_Price> reader = md_Gold_Price.GetAll().Take(4).ToList();
            rptQuestion.DataSource = reader;
            rptQuestion.DataBind();
        } 

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            Config = new Gold_PriceConfiguration(getModuleSettings);
            pnlPostList.CssClass = config.ModuleGold_PriceCssCustome;

            pnlContainer.ModuleId = ModuleId;
            Title1.Visible = false;
            if (siteUser.IsInRoles("Admins") && ModuleId > 0 && PageId > 0)
            {
                string html = $@"
                <a id='module{ModuleId}' class='moduleanchor'></a>
                <span class='modulelinks'>
                    <a title='Chỉnh sửa thiết lập cho thực thể nội dung này' class='ModuleEditLink' 
                       href='/Admin/ModuleSettings.aspx?mid={ModuleId}&amp;pageid={PageId}'>
                        Thiết lập
                    </a>
                </span>";
                litModuleLinks.Text = html;
            }
        }
    }
}
using ArticleFeature.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class ReloadArticleSearchTool : mojoBasePage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReloadSearchArticle();
            }
        }
        public void ReloadSearchArticle()
        {
            var listArticle = Article.GetAll();
            foreach (var item in listArticle)
            {
                item.TitleFTS = item.Title.ConvertToFTS();
                item.AuthorFTS = item.CreatedByUser.ConvertToFTS();
                item.SapoFTS = item.Summary.ConvertToFTS();
                item.FTS = string.Format("{0} {1} {2}", item.TitleFTS, item.AuthorFTS, item.SapoFTS);
                item.Save();
            }
        }
    }
}
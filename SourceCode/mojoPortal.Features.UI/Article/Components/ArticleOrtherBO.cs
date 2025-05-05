using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public class ArticleOrtherBO
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string SiteRoot { get; set; }
        public string ItemUrl { get; set; }
        public int ModuleID { get; set; }
        public int PageID { get; set; }
        public string StartDate { get; set; }
        public string ArticleImagesFolder { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
    }
    public class ArticlePagerBO
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<ArticleOrtherBO> ListArticle { get; set; }
        public int CountItem { get; set; }
    }
}

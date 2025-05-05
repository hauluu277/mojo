using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkFeature.UI
{
    public partial class RecentList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected LinkConfiguration config = new LinkConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private string categoryId = string.Empty;
        private string siteRoot = string.Empty;
        private SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        protected int langId = 1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private int displayList = 1;

        public int DisplayList
        {
            get { return displayList; }
            set { displayList = value; }
        }
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

        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public LinkConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public bool IsEditable { get; set; }
        #endregion

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            rptCategory.ItemDataBound += rptCategory_ItemDataBound;
            rptCategory.DataBinding += rptCategory_DataBinding;
            rptNewLink.ItemDataBound += rptNewLink_ItemDataBound;
        }
        protected void rptNewLink_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList childRepeater = (DropDownList)args.Item.FindControl("drl");
                object item = DataBinder.Eval(args.Item.DataItem, "ItemID");

                List<CoreLink> reader = CoreLink.GetByCatIdOrderBy((int)item, config.OrderBySetting);
                childRepeater.DataSource = reader;
                childRepeater.DataBind();

                childRepeater.DataValueField = "Url";
                childRepeater.DataTextField = "Name";
                childRepeater.DataSource = reader;
                childRepeater.DataBind();
                CategoryLink Category = new CategoryLink((int)item);
                if (Category != null)
                {
                    childRepeater.Items.Insert(0, new ListItem(Category.Name, "/"));
                }
            }




        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
        }
        private void BindListLink()
        {
            var lstCategory = new List<int>();
            if (!string.IsNullOrEmpty(categoryId))
            {
                var sub = categoryId.Split('-');
                foreach (var item in sub)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        lstCategory.Add(int.Parse(item));
                    }
                }
            }
            //
            List<CategoryLink> roots = CategoryLink.GetAll(siteSettings.SiteId).Where(x => lstCategory.Contains(x.ItemID)).ToList();
            rptCategory.DataSource = roots;
            rptCategory.DataBind();
            rptCategoryLink.DataSource = roots;
            rptCategoryLink.DataBind();
        }
        protected void rptCategory_DataBinding(object sender, System.EventArgs e)
        {
            //Repeater rep = (Repeater)(sender);

            //int someIdFromParentDataSource = (int)(Eval("ItemID"));

            //// Assuming you have a function call `GetSomeData` that will return
            //// the data you want to bind to your child repeater.
            //List<CoreLink> reader = CoreLink.GetAll().Where(x => x.CategoryID == someIdFromParentDataSource).ToList();
            //rep.DataSource = reader;
            //rep.DataBind();
        }
        protected void rptCategory_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {

            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {

                Repeater childRepeater = (Repeater)args.Item.FindControl("rptLink");
                object item = DataBinder.Eval(args.Item.DataItem, "ItemID");

                List<CoreLink> reader = CoreLink.GetByCatIdOrderBy((int)item, config.OrderBySetting);
                childRepeater.DataSource = reader;
                childRepeater.DataBind();
            }
        }
        private void PopulateControls()
        {
            if (displayList == LinkConstant.DisplayOneLink)//Hiển thị link bình thường
            {
                BindLink();

            }
            else if (displayList == LinkConstant.DisplayThreeLink)// Hiển thị 3 link 
            {
                BindListLink();

            }
            else if (displayList == LinkConstant.DisplayFourLink)//Hiển thị 4 link
            {
                BindNewLink();
            }
        }
        private void BindNewLink()
        {
            var lstCategory = new List<int>();
            if (!string.IsNullOrEmpty(categoryId))
            {
                var sub = categoryId.Split('-');
                foreach (var item in sub)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        lstCategory.Add(int.Parse(item));
                    }
                }
            }
            //
            List<CategoryLink> roots = CategoryLink.GetAll(siteSettings.SiteId).Where(x => lstCategory.Contains(x.ItemID)).ToList();
            rptNewLink.DataSource = roots;
            rptNewLink.DataBind();
        }


        private void BindLink()
        {
            var lstCategory = new List<int>();
            if (!string.IsNullOrEmpty(categoryId))
            {
                var sub = categoryId.Split('-');
                foreach (var item in sub)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        lstCategory.Add(int.Parse(item));
                    }
                }
            }
            string categoryName = string.Empty;
            //int i = 1;
            var cateID = 0;
            if (lstCategory != null && lstCategory.Count > 0)
            {
                cateID = lstCategory.FirstOrDefault();
            }
            List<CoreLink> reader = CoreLink.GetByCatIdOrderBy(cateID, config.OrderBySetting);
            var LinkFrist = reader.FirstOrDefault();
            if (LinkFrist != null)
            {
                categoryName = LinkFrist.CategoryName;
            }
            ddlLink.DataValueField = "Url";
            ddlLink.DataTextField = "Name";
            ddlLink.DataSource = reader;
            ddlLink.DataBind();
            ddlLink.Items.Insert(0, new ListItem("--" + categoryName + "--", "/"));
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new LinkConfiguration(getModuleSettings);
            categoryId = config.LinkCategoryConfig;
            displayList = config.DisplayList;
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
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
        protected string FormatArticleDate(DateTime datePromulgate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return datePromulgate.ToString(config.DateTimeFormat);
        }

    }
}
using ArticleFeature.Business;
using ArticleFeature.UI;
using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleLoaderFeature.UI
{
    public partial class ArticleSchoolModule : SiteModuleControl
    {
        private int loadedModuleId = -1;
        private int loadedPageId = -1;
        private int categoryId = -1;
        string[] listModuleId;
        private int countOfDrafts;
        protected ArticleConfiguration config = new ArticleConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();
            PopulateLabels();
            PopulateControls();
            pnlOuterWrap.CssClass = config.ModuleDisplayCssCustome;

        }

        private void PopulateControls()
        {
            if (config.UseTabArticle || config.UseTabArticle2 || config.UseTabArticle3)
            {
                TabLoader tabLoader = (TabLoader)LoadControl("~/ArticleLoader/Controls/TabListSchool.ascx");
                tabLoader.ListModuleId = listModuleId;
                tabLoader.ModuleId = loadedModuleId > 0 ? loadedModuleId : ModuleId;
                tabLoader.PageId = loadedPageId > 0 ? loadedPageId : PageId;
                tabLoader.IsEditable = IsEditable;
                tabLoader.Config = config;
                tabLoader.SiteRoot = SiteRoot;
                tabLoader.ImageSiteRoot = ImageSiteRoot;
                tabLoader.PureModuleId = ModuleId;
                placeHolder.Controls.Add(tabLoader);
            }
            //else if (config.UseSlideArticle)
            //{
            //    SlideTabLoader slideTabLoader = (SlideTabLoader)LoadControl("~/ArticleLoader/Controls/SlideTabList.ascx");
            //    slideTabLoader.ListModuleId = listModuleId;
            //    slideTabLoader.ModuleId = loadedModuleId;
            //    slideTabLoader.PageId = loadedPageId;
            //    slideTabLoader.IsEditable = IsEditable;
            //    slideTabLoader.Config = config;
            //    slideTabLoader.SiteRoot = SiteRoot;
            //    slideTabLoader.ImageSiteRoot = ImageSiteRoot;
            //    slideTabLoader.CategoryId = categoryId;
            //    placeHolder.Controls.Add(slideTabLoader);
            //}
            else
            {
                PostListLoader postListLoader = (PostListLoader)LoadControl("~/ArticleLoader/Controls/PostList.ascx");
                postListLoader.ModuleId = loadedModuleId;
                postListLoader.PageId = loadedPageId;
                postListLoader.IsEditable = IsEditable;
                postListLoader.Config = config;
                postListLoader.SiteRoot = SiteRoot;
                postListLoader.ImageSiteRoot = ImageSiteRoot;
                postListLoader.CategoryID = categoryId;
                postListLoader.ListModuleId = listModuleId;
                postListLoader.PureModuleId = ModuleId;
                placeHolder.Controls.Add(postListLoader);
            }
        }

        protected virtual void PopulateLabels()
        {
            Title1.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    Title1.Visible = true;
                }
            }
            Title1.ShowEditLinkOverride = false;
            #region grant label for titlecontrol
            //if (listModuleId.Length <= 0 || listModuleId[0] == string.Empty)
            //{
            //Title1.LoadedModuleId = loadedModuleId;
            //Title1.LoadedPageId = loadedPageId;
            //if ((IsEditable) && (countOfDrafts > 0))
            //{
            //    Title1.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/EditPost.aspx?pageid=" + loadedPageId
            //        + "&amp;mid=" + loadedModuleId
            //        + "&amp;catid=" + categoryId
            //        + "' class='ModuleEditLink' title='" + ArticleResources.BlogAddPostLabel +
            //        "'><img src='/Data/SiteImages/pencil.png' alt='" + ArticleResources.BlogAddPostLabel +
            //        " |'</img></a>"
            //        + "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/EditCategory.aspx?pageid=" + loadedPageId
            //        + "&amp;mid=" + loadedModuleId
            //        + "' class='editcategorieslink' title='" + ArticleResources.BlogEditCategoriesLabel +
            //        "'><img src='/Data/Icon16x16/Folder.png' alt='" + ArticleResources.BlogEditCategoriesLabel +
            //        " |'</img></a>"
            //        + "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/Drafts.aspx?pageid=" + loadedPageId
            //        + "&amp;mid=" + loadedModuleId
            //        + "' class='draftlink' title='" + ArticleResources.BlogDraftsLink +
            //        "'><img src='/Data/Icon16x16/Bubble.png' alt='" + ArticleResources.BlogDraftsLink +
            //        " |'</img></a>"
            //        + "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/ManagePost.aspx?pageid=" + loadedPageId
            //        + "&amp;mid=" + loadedModuleId
            //        + "' class='managepostlink' title='" + ArticleResources.AdminManagePostLabel +
            //        "'><img src='/Data/Icon16x16/Text.png' alt='" + ArticleResources.AdminManagePostLabel +
            //        "'</img></a>";
            //}
            //else if (IsEditable)
            //{
            //    Title1.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/EditPost.aspx?pageid=" + loadedPageId
            //        + "&amp;mid=" + loadedModuleId
            //        + "&amp;catid=" + categoryId
            //        + "' class='ModuleEditLink' title='" + ArticleResources.BlogAddPostLabel +
            //        "'><img src='/Data/SiteImages/pencil.png' alt='" + ArticleResources.BlogAddPostLabel +
            //        " |'</img></a>"
            //        + "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/EditCategory.aspx?pageid=" + loadedPageId
            //        + "&amp;mid=" + loadedModuleId
            //        + "' class='editcategorieslink' title='" + ArticleResources.BlogEditCategoriesLabel +
            //        "'><img src='/Data/Icon16x16/Folder.png' alt='" + ArticleResources.BlogEditCategoriesLabel +
            //        " |'</img></a>"
            //        + "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/ManagePost.aspx?pageid=" + loadedPageId
            //        + "&amp;mid=" + loadedModuleId
            //        + "' class='managepostlink' title='" + ArticleResources.AdminManagePostLabel +
            //        "'><img src='/Data/Icon16x16/Text.png' alt='" + ArticleResources.AdminManagePostLabel +
            //        "'</img></a>";
            //}
            //}
            #endregion
        }


        protected virtual void LoadSettings()
        {
            pnlContainer.ModuleId = ModuleId;
            config = new ArticleConfiguration(Settings);
            if (!config.InstanceCssClass.Equals(string.Empty)) { pnlContainer.CssClass += " " + config.InstanceCssClass; }

            if (!config.ArticleCategoriesSelected.Equals(string.Empty))
            {
                string loadedCategory = config.ArticleCategoriesSelected.Contains(';') ? config.ArticleCategoriesSelected.Remove(config.ArticleCategoriesSelected.IndexOf(";")) : config.ArticleCategoriesSelected.ToString();
                if (!loadedCategory.Equals(string.Empty))
                {
                    int.TryParse(loadedCategory.Remove(loadedCategory.IndexOf("-")), out loadedModuleId);
                    int.TryParse(loadedCategory.Remove(0, loadedCategory.IndexOf("-") + 1), out loadedPageId);
                    int.TryParse(config.ArticleCategoriesSelected.Remove(0, config.ArticleCategoriesSelected.IndexOf(";") + 1), out categoryId);
                }
            }
            else
            {
                loadedModuleId = 0;
                categoryId = 0;
                loadedPageId = 0;
            }
            string idAll = config.ArticleModuleSelectorSetting;
            if (idAll != string.Empty)
            {
                idAll = idAll.Remove(idAll.Length - 1);
            }
            char[] param = { ';' };
            listModuleId = idAll.Contains(";") ? idAll.Split(param) : new[] { idAll };
            for (int i = 0; i < listModuleId.Length; i++)
            {
                if (listModuleId[i].Length > 0)
                {
                    listModuleId[i] = listModuleId[i].Remove(listModuleId[i].IndexOf('-'));
                }
            }
            if (ModuleConfiguration == null) return;
            Title = ModuleConfiguration.ModuleTitle;
            Description = ModuleConfiguration.FeatureName;

            //if (IsEditable)
            //{
            //    countOfDrafts = Article.CountOfDrafts(loadedModuleId);
            //}
        }
    }
}
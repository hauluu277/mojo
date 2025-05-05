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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class ManageCommentControl : UserControl
    {
        #region Properties

        private int pageNumber = 1;
        private int totalPages = 1;
        private int totalCounts = 0;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string dateTimeFormat;
        private mojoBasePage basePage;
        private Module module;
        protected ArticleConfiguration config = new ArticleConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryID = -1;
        private int puStatus = -1;
        private int puApprove = -1;
        private string keyword = string.Empty;
        private DateTime? _startDate = null;
        private DateTime? _endDate = null;
        private string startdate = string.Empty;
        private string enddate = string.Empty;
        private int isPublish = -1;
        private int isApprove = -1;
        protected bool isAdmin = false;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        private readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private Guid articleGuid = Guid.Empty;
        private int moderation = -1;
        private CommentRepository repository = new CommentRepository();

        private int step = 0;
        protected int role = -1;

        protected int statusNhuanBut = 0;

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

        public int CategoryId
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        public int PuStatus
        {
            get { return puStatus; }
            set { puStatus = value; }
        }

        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }

        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public ArticleConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }

        public bool IsEditable { get; set; }

        #endregion Properties

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;

            btnSearch.Click += btnSearch_Click;
            ddlCategories.SelectedIndexChanged += ddlCategories_SelectedIndexChanged;
            ddlState.SelectedIndexChanged += ddlState_SelectedIndexChanged;
            ddlPublishStatus.SelectedIndexChanged += ddlPublishStatus_SelectedIndexChanged;
            btnSavecomment.Click += BtnSavecomment_Click;
            SiteUtils.SetupEditor(edComment);
            //EnableViewState = false;
        }



        private void BtnSavecomment_Click(object sender, EventArgs e)
        {
            var result = true;
            if (!string.IsNullOrEmpty(hdfCommentGuid.Value))
            {
                var comment = repository.Fetch(new Guid(hdfCommentGuid.Value));
                if (comment != null)
                {
                    comment.UserComment = edComment.Text;
                    comment.LastModUtc = DateTime.Now;
                    repository.Save(comment);
                }
                else
                {
                    result = false;
                }
            }
            ScriptManager.RegisterStartupScript(pnlReloadComment, this.GetType(), "UpdateComment", "UpdateCommentReponse('" + result.ToString() + "');", true);
            BindArticles();
            pnlReloadComment.Update();
        }

        private void ddlPublishStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUpdateSearch();
        }

        private void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUpdateSearch();
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUpdateSearch();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }

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
            btnSearch.Text = ArticleResources.ArticleSearchButton;
            UIHelper.DisableButtonAfterClick(
                btnSearch,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty)
                );

            edComment.WebEditor.ToolBar = mojoPortal.Web.Editor.ToolBar.Custom1;
            edComment.WebEditor.Height = 120;
            //reqStartDate.ErrorMessage = ArticleResources.BlogBeginDateRequiredHelp;
        }

        protected bool ShowImage(string imageUrl)
        {
            return CommonBusiness.SowImage(imageUrl);
        }

        private void BindApprove()
        {
            var listItem = new List<ListItem>()
            {
                new ListItem
                {
                    Text="-Chọn-",
                    Value="-1"
                },
                new ListItem
                {
                    Text="Đã duyệt",
                    Value="1"
                },
                new ListItem
                {
                    Text="Chưa duyệt",
                    Value="0"
                }
            };

            ddlState.DataValueField = "Value";
            ddlState.DataTextField = "Text";
            ddlState.DataSource = listItem;
            ddlState.DataBind();
        }


        private void PopulateControls()
        {
            BindArticles();
            PopulateCategories();
            PopulateStatus();
            BindApprove();
            if (categoryID > 0)
            {
                ddlCategories.SelectedValue = categoryID.ToString();
            }
            if (puStatus >= 0)
            {
                ddlPublishStatus.SelectedValue = puStatus.ToString();
            }
            if (puApprove >= 0)
            {
                ddlState.SelectedValue = puApprove.ToString();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                txtStartDate.Value = startdate;
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                txtEndDate.Value = enddate;
            }
            if (categoryID > 0 || puStatus >= 0 || puApprove >= 0)
            {
                BindUpdateSearch();
                ddlArticle.SelectedValue = articleGuid.ToString();
            }
        }

        private void BindArticles()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;

            List<Comment> reader = new List<Comment>();
            CommentRepository repository = new CommentRepository();
            var keyword_search = !string.IsNullOrEmpty(keyword) ? keyword.ConvertToVN() : string.Empty;
            reader = repository.GetSelectPage(siteSettings.SiteGuid, Article.FeatureGuid, puStatus, puApprove, categoryID, articleGuid, moderation, _startDate, _endDate, keyword_search, pageNumber, config.PageSize, out totalPages, out totalCounts);
            rptArticles.DataSource = reader;
            rptArticles.DataBind();
            literTotalArticle.Text = string.Format("Hiển thị <span class='text-primary'>{0}</span>/<span class='text-danger'>{1}</span> tổng số bình luận bài viết.", reader.Count, totalCounts);
            string pageUrl = siteSetting.SiteRoot + "/ArticleComment/ManageArticleComment.aspx"
                   + "?catid=" + categoryID.ToInvariantString()
                   + "&pustatus=" + puStatus.ToInvariantString()
                  + "&puapprove=" + puApprove
                   + "&moderation=" + moderation
                   + "&keyword=" + keyword
                   + "&article=" + articleGuid
                   + "&startdate=" + startdate
                   + "&enddate=" + enddate
                   + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            timeZone = SiteUtils.GetUserTimeZone();
            dateTimeFormat = config.DateTimeFormat.ToString();
            timeOffset = SiteUtils.GetUserTimeOffset();
        }

        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            categoryID = WebUtils.ParseInt32FromQueryString("catid", categoryID);
            string pu_status = WebUtils.ParseStringFromQueryString("pustatus", string.Empty);
            string pu_Approve = WebUtils.ParseStringFromQueryString("puapprove", string.Empty);

            startdate = WebUtils.ParseStringFromQueryString("startdate", startdate);
            enddate = WebUtils.ParseStringFromQueryString("enddate", enddate);
            step = WebUtils.ParseInt32FromQueryString("state", step);
            articleGuid = WebUtils.ParseGuidFromQueryString("article", articleGuid);
            moderation = WebUtils.ParseInt32FromQueryString("moderation", moderation);

            if (!string.IsNullOrEmpty(startdate))
            {
                _startDate = startdate.ToDateTime();
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                _endDate = enddate.ToDateTime();
            }

            if (!string.IsNullOrEmpty(pu_status))
            {
                puStatus = int.Parse(pu_status);
                isPublish = puStatus;
            }
            if (!string.IsNullOrEmpty(pu_Approve))
            {
                puApprove = int.Parse(pu_Approve);
                isPublish = puApprove;
            }

            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }

        private void PopulateCategories()
        {
            List<CoreCategory> roots = CoreCategory.GetRootByCat(siteSetting.SiteId, siteSetting.ArticleCategory);
            foreach (CoreCategory item in roots)
            {
                ListItem list = new ListItem
                {
                    Text = item.Name,
                    Value = item.ItemID.ToString()
                };
                ddlCategories.Items.Add(list);
            }
            PopulateChildNode(ddlCategories);
            ddlCategories.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
        }

        private void PopulateChildNode(ListControl root)
        {
            for (int i = 0; i < root.Items.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(root.Items[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root.Items[i].Text.StartsWith("|"))
                {
                    prefix += root.Items[i].Text.Substring(0, 3);
                    root.Items[i].Text = root.Items[i].Text.Remove(0, 3);
                }
                root.Items[i].Text = prefix + root.Items[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString()
                    };
                    root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    index++;
                }
            }
        }
        private void BindUpdateSearch()
        {
            var category = Convert.ToInt32(ddlCategories.SelectedValue);
            var step = Convert.ToInt32(ddlState.SelectedValue);
            var published = Convert.ToInt32(ddlPublishStatus.SelectedValue);
            ddlArticle.DataValueField = "key";
            ddlArticle.DataTextField = "value";
            ddlArticle.DataSource = Article.GetSearch(category, step, published);
            ddlArticle.DataBind();
            ddlArticle.Items.Insert(0, new ListItem { Text = "Tất cả", Value = Guid.Empty.ToString() });
        }


        private void PopulateStatus()
        {
            //Trạng thái xuất bản tin bài
            //var publish_status = SiteUtils.StringToDictionary(ArticleResources.ArticlePublishStatus.ToString(), ",");

            var listItem = new List<ListItem>()
            {
                new ListItem
                {
                    Text="-Chọn-",
                    Value="-1"
                },
                new ListItem
                {
                    Text="Đã xuất bản",
                    Value="1"
                },
                new ListItem
                {
                    Text="Chưa xuất bản",
                    Value="0"
                }
            };

            ddlPublishStatus.DataSource = listItem;
            ddlPublishStatus.DataTextField = "Text";
            ddlPublishStatus.DataValueField = "Value";
            ddlPublishStatus.DataBind();
            //Trạng thái bình luận

            var listItemStatus = new List<ListItem>()
            {
                new ListItem
                {
                    Text="-Chọn-",
                    Value="-1"
                },
                new ListItem
                {
                    Text="Đã xuất bản",
                    Value="1"
                },
                new ListItem
                {
                    Text="Chưa xuất bản",
                    Value="3"
                }
            };

            ddlModeration.DataSource = listItemStatus;
            ddlModeration.DataValueField = "Value";
            ddlModeration.DataTextField = "Text";
            ddlModeration.DataBind();

            //ddlModeration.Items.Insert(0, new ListItem { Text = "Tất cả", Value = "-1" });


        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            categoryID = string.IsNullOrEmpty(ddlCategories.SelectedValue) ? -1 : int.Parse(ddlCategories.SelectedValue);
            string pu_status = ddlPublishStatus.SelectedValue;
            string pu_approve = ddlState.SelectedValue;
            keyword = txtKeyword.Text;
            startdate = txtStartDate.Value;
            enddate = txtEndDate.Value;

            //step = Convert.ToInt32(ddlState.SelectedValue);

            moderation = Convert.ToInt32(ddlModeration.SelectedValue);
            if (!string.IsNullOrEmpty(ddlArticle.SelectedValue))
            {
                articleGuid = Guid.Parse(ddlArticle.SelectedValue);
            }

            if (!string.IsNullOrEmpty(pu_status))
            {
                puStatus = int.Parse(pu_status);
            }
            if (!string.IsNullOrEmpty(pu_approve))
            {
                puApprove = int.Parse(pu_approve);
            }

            string pageUrl = siteSetting.SiteRoot + "/ArticleComment/ManageArticleComment.aspx"
                     + "?catid=" + categoryID.ToInvariantString()
                   + "&pustatus=" + puStatus.ToInvariantString()
                   + "&puapprove=" + puApprove
                   + "&moderation=" + moderation
                   + "&keyword=" + keyword
                   + "&article=" + articleGuid
                   + "&startdate=" + startdate
                   + "&enddate=" + enddate
                   + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }

        protected void rptArticles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    var comment = repository.Fetch(new Guid(e.CommandArgument.ToString()));
                    var script = "ModalUpdateComment();";
                    if (comment != null)
                    {
                        edComment.Text = comment.UserComment;
                        hdfCommentGuid.Value = comment.Guid.ToString();
                        if (comment.UserId > 0)
                        {
                            lblCreateDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}", comment.CreatedUtc);
                            lblEmail.Text = comment.UserEmail;
                            lblHoTen.Text = comment.UserName;
                        }
                        else
                        {
                            lblCreateDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}", comment.CreatedUtc);
                            lblEmail.Text = comment.AuthorEmail;
                            lblHoTen.Text = comment.PostAuthor;
                        }
                        ScriptManager.RegisterStartupScript(pnlReloadComment, this.GetType(), "FormUpdateComment", script, true);
                        pnlReloadComment.Update();
                    }
                    else
                    {
                        script = "NotifyError('Bình luận không tồn tại!')";
                        ScriptManager.RegisterStartupScript(pnlReloadComment, this.GetType(), "FormUpdateComment", script, true);
                    }
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    Guid item = Guid.Parse(e.CommandArgument.ToString());

                    repository.Delete(item);

                    var script = "NotifySuccess('Xóa bình luận thành công!')";
                    ScriptManager.RegisterStartupScript(pnlReloadComment, this.GetType(), "DeleteComment", script, true);
                    BindArticles();
                    pnlReloadComment.Update();

                    //string rediect = string.Format("window.location.href=window.location.href");
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", rediect, true);

                    // WebUtils.SetupRedirect(this, siteSetting.SiteRoot + "/ArticleComment/ManageArticleComment.aspx?pageid="
                    // + "?catid=" + categoryID.ToInvariantString()
                    //+ "&pustatus=" + puStatus.ToInvariantString()
                    //+ "&state=" + step
                    //+ "&moderation=" + moderation
                    //+ "&keyword=" + keyword
                    //+ "&article=" + articleGuid
                    //+ "&startdate=" + startdate
                    //+ "&enddate=" + enddate
                    //+ "&pagenumber=1");
                }
                else if (e.CommandName.Equals("StatusItem"))
                {
                    Guid item = Guid.Parse(e.CommandArgument.ToString());
                          

                    var comment = repository.Fetch(item);
                    if (comment.ModerationStatus == Comment.ModerationApproved)
                    {
                        comment.ModerationStatus = Comment.ModerationRejected;
                    }
                    else
                    {
                        comment.ModerationStatus = Comment.ModerationApproved;
                    }
                    repository.Save(comment);
                    var script = "NotifySuccess('Thay đổi trạng thái bình luận thành công!')";
                    ScriptManager.RegisterStartupScript(pnlReloadComment, this.GetType(), "ChangeStatusComment", script, true);
                    BindArticles();
                    pnlReloadComment.Update();
                }
            }
        }


        protected void rptArticles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, "Dữ liệu xóa sẽ không khôi phục lại được?");

                ImageButton ibtnStatus = e.Item.FindControl("ibtnStatus") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnStatus, "Thay đổi trạng thái xuất bản bình luận?");
            }
        }

        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            if (config.IsDeleteSetting)
            {
                foreach (RepeaterItem rpt in rptArticles.Items)
                {
                    CheckBox chkFlag = rpt.FindControl("chk") as CheckBox;
                    if (chkFlag.Checked)
                    {
                        deleteNumber++;
                        int itemid = Convert.ToInt32((rpt.FindControl("repeaterID") as Literal).Text);
                        Article articleDelete = new Article(itemid);
                        articleDelete.IsDelete = true;
                        articleDelete.Save();
                    }
                }
            }
            else
            {
                foreach (RepeaterItem rpt in rptArticles.Items)
                {
                    CheckBox chkFlag = rpt.FindControl("chk") as CheckBox;
                    if (chkFlag.Checked)
                    {
                        deleteNumber++;
                        int itemid = Convert.ToInt32((rpt.FindControl("repeaterID") as Literal).Text);
                        Article.Delete(itemid);
                    }
                }
            }

            if (deleteNumber > 0)
            {
                string pageUrl = siteSetting.SiteRoot + "/ArticleComment/ManageArticleComment.aspx"
                     + "?catid=" + categoryID.ToInvariantString()
                   + "&pustatus=" + puStatus.ToInvariantString()
                  + "&puapprove=" + puApprove
                   + "&moderation=" + moderation
                   + "&keyword=" + keyword
                   + "&article=" + articleGuid
                   + "&startdate=" + startdate
                   + "&enddate=" + enddate
                   + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }
    }
}
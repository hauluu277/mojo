using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using QuestionAnswerFeatures.Business;
using mojoPortal.Web.Framework;

namespace QuestionAnswerFeatures.UI
{
    public partial class QuestionAnswerRecenlist : System.Web.UI.UserControl
    {
        #region setup private propety
        private int pageId = -1;
        private int moduleId = -1;
        private int totalPage = 1;
        private Guid itemGuid = Guid.Empty;
        protected Double timeOffset;
        private TimeZoneInfo timeZone;
        QuestionAnswerConfiguration config;
        private SiteSettings siteSetting;
        private readonly SiteUser user = SiteUtils.GetCurrentSiteUser();
        private string siteRoot = string.Empty;
        private int pageNumber = 1;
        private int categoryID = -1;
        private int categoryChildID = -1;
        private int orderBy = 1;
        private string keyword = string.Empty;
        protected string GiaoDienHienThiQA = "HienThiDangList";
        #endregion
        #region setup public propety
        public int PageID
        {
            get { return pageId; }
            set { pageId = value; }
        }
        public int ModuleID
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }
        public SiteSettings SiteSetting
        {
            get { return siteSetting; }
            set { siteSetting = value; }
        }
        public QuestionAnswerConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        public int CategoryChildID
        {
            get { return categoryChildID; }
            set { categoryChildID = value; }
        }
        public int OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }

        #endregion
        #region OnInit
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            ddlLinhVuc.SelectedIndexChanged += ddlLinhVuc_SelectedIndexChanged;
            btnSearch.Click += btnSearch_Click;
            btnDangTin.ServerClick += btnDangTin_ServerClick;
        }

        void btnDangTin_ServerClick(object sender, EventArgs e)
        {
            SiteUtils.RedirectToUrl(SiteRoot + "/dang-cau-hoi");
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    orderBy = int.Parse(ddlOrderBy.SelectedValue);
            //}
            //catch
            //{
            //    orderBy = 1;
            //}
            try
            {
                categoryID = int.Parse(ddlLinhVuc.SelectedValue);
            }
            catch
            {

                categoryID = -1;
            }
            //try
            //{
            //    //categoryChildID = int.Parse(ddlLoaiLinhVuc.SelectedValue);
            //}
            //catch
            //{

            //    categoryChildID = -1;
            //}

            keyword = txtKeyword.Text;



            string url = SiteRoot + "/QuestionAnswers/ListQuestionAnswer.aspx"
            + "?pageid=" + pageId.ToInvariantString()
            + "&mid=" + moduleId.ToInvariantString()
            + "&cateId=" + categoryID.ToInvariantString()
            + "&cateChildId=" + categoryChildID.ToInvariantString()
            + "&keyword=" + keyword
            + "&orderby=" + orderBy.ToInvariantString();
            SiteUtils.RedirectToUrl(url);


        }

        void ddlLinhVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLinhVuc.SelectedValue != null && ddlLinhVuc.SelectedValue != "")
            {
                try
                {
                    categoryID = int.Parse(ddlLinhVuc.SelectedValue);
                }
                catch
                {

                    categoryID = -1;
                }
                //List<CoreCategory> ListLoaiLinhVuc = CoreCategory.GetChildrenByParent(categoryID);
                //ddlLoaiLinhVuc.DataTextField = "Name";
                //ddlLoaiLinhVuc.DataValueField = "ItemID";
                //ddlLoaiLinhVuc.DataSource = ListLoaiLinhVuc;
                //ddlLoaiLinhVuc.DataBind();
                //ddlLoaiLinhVuc.Items.Insert(0, new ListItem { Text = "---Loại lĩnh vực---", Value = string.Empty });
            }
            else
            {
                //ddlLoaiLinhVuc.DataTextField = "Name";
                //ddlLoaiLinhVuc.DataValueField = "ItemID";
                //ddlLoaiLinhVuc.DataSource = new List<CoreCategory>();
                //ddlLoaiLinhVuc.DataBind();
                //ddlLoaiLinhVuc.Items.Insert(0, new ListItem { Text = "---Loại lĩnh vực---", Value = string.Empty });
            }
        }


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParam();
            LoadSetting();
            PopulateLabel();
            if (!IsPostBack)
            {
                PopulateControl();
                txtKeyword.Text = keyword;
            }
        }
        private void PopulateLabel()
        {
            lblTitle.Text = "Danh sách hỏi đáp";
            btnDangTin.InnerText = "Đăng thông tin hỏi đáp";
            btnDangTin.ResolveUrl(SiteRoot + "/dang-cau-hoi");
            btnDangTin.ResolveClientUrl(SiteRoot + "/dang-cau-hoi");
            //lblSearch.Text = SwirlingQuestionResource.SearchTopLabel;
            //lblLinhVuc.Text = SwirlingQuestionResource.LinhVucLabel;
            //lblLoaiLinhVuc.Text = SwirlingQuestionResource.LoaiLinhVucLabel;
            //lblOrderby.Text = SwirlingQuestionResource.OrderLabel;
            //lblMoRong.Text = SwirlingQuestionResource.MoRongLabel;
            //lblThuGon.Text = SwirlingQuestionResource.ThuGonLabel;

        }
        private void BindOrderBy()
        {
            //var orderByStatus = SiteUtils.StringToDictionary(SwirlingQuestionResource.OrderByStatus, ",");
            //ddlOrderBy.DataValueField = "Key";
            //ddlOrderBy.DataTextField = "Value";
            //ddlOrderBy.DataSource = orderByStatus;
            //ddlOrderBy.DataBind();
            //ddlOrderBy.SelectedValue = orderBy.ToString();
        }

        private void BindQuestion()
        {
            List<QuestionAnswer> ListQuestion = QuestionAnswer.GetPage(siteSetting.SiteId, categoryID, categoryChildID, 1, orderBy, keyword.ConvertToFTS(), pageNumber, config.RecentListPageSize, -1, out totalPage);
            rptQuestion.DataSource = ListQuestion;
            rptQuestion.DataBind();
            string pageUrl = SiteRoot + "/QuestionAnswers/ListQuestionAnswer.aspx"
            + "?pageid=" + pageId.ToInvariantString()
            + "&mid=" + moduleId.ToInvariantString()
            + "&cateId=" + categoryID.ToInvariantString()
            + "&cateChildId=" + categoryChildID.ToInvariantString()
            + "&orderby=" + orderBy.ToInvariantString()
            + "&keyword=" + keyword
            + "&pagenumber={0}";
            pgrDictionary.PageURLFormat = pageUrl;
            pgrDictionary.ShowFirstLast = true;
            pgrDictionary.PageSize = config.RecentListPageSize;
            pgrDictionary.PageCount = totalPage;
            pgrDictionary.CurrentIndex = pageNumber;
            pgrDictionary.Visible = totalPage > 1;
            lblHoiDapResult.Text = ListQuestion.Count + SwirlingQuestionResource.QAResultLabel;
        }
        private void BindQuestionLabel()
        {
            foreach (RepeaterItem item in rptQuestion.Items)
            {
                Label lblSenderLabel = item.FindControl("lblSenderLabel") as Label;
                lblSenderLabel.Text = SwirlingQuestionResource.SenderLabel;
                Label lblTimeSendLabel = item.FindControl("lblTimeSendLabel") as Label;
                lblTimeSendLabel.Text = SwirlingQuestionResource.TimeSenderLabel;

                Literal lblAnswerLabel = item.FindControl("lblAnswerLabel") as Literal;
                lblAnswerLabel.Text = SwirlingQuestionResource.AnswerLabel;

                Literal lblView = item.FindControl("lblViewLabel") as Literal;
                lblView.Text = SwirlingQuestionResource.ViewLabel;

                Label lblContent = item.FindControl("lblContent") as Label;
                lblContent.Text = SwirlingQuestionResource.ContentTilte;

                HyperLink hplAnswer = item.FindControl("hplAnswer") as HyperLink;
                hplAnswer.Text = SwirlingQuestionResource.AnswerQuestionLabel;
                hplAnswer.ToolTip = SwirlingQuestionResource.AnswerQuestionLabel;
            }
        }
        protected string FomatTotalAnswer(string totalAnswer)
        {
            return "(" + totalAnswer + ")";
        }
        protected string FomatView(string views)
        {
            return "(" + views + ")";
        }
        protected bool DisplayDistrict(string districtId)
        {
            if (!string.IsNullOrEmpty(districtId))
            {
                return true;
            }
            return false;
        }
        protected bool DisplayCategory(string childCategory)
        {
            if (!string.IsNullOrEmpty(childCategory))
            {
                try
                {
                    if (int.Parse(childCategory) > 0)
                    {
                        return true;
                    }
                }
                catch
                {

                    return false;
                }
            }
            return false;

        }
        private void PopulateControl()
        {
            BindOrderBy();
            BindQuestion();
            BindCategory();
            BindQuestionLabel();
        }
        private void BindCategory()
        {
            List<CoreCategory> ListLinhVuc = CoreCategory.GetChildren(siteSetting.CoreLinhVucHoiDap);
            ddlLinhVuc.DataValueField = "ItemID";
            ddlLinhVuc.DataTextField = "Name";
            ddlLinhVuc.DataSource = ListLinhVuc;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem { Value = "-1", Text = "Tất cả" });
            //ddlLoaiLinhVuc.Items.Insert(0, new ListItem { Text = SwirlingQuestionResource.LoaiLinhVucLabel, Value = "-1" });
            //if (categoryID > 0)
            //{
            //    ddlLinhVuc.SelectedValue = categoryID.ToString();
            //    List<CoreCategory> ListCategoryChild = CoreCategory.GetChildrenByParent(categoryID);
            //    ddlLoaiLinhVuc.DataValueField = "ItemID";
            //    ddlLoaiLinhVuc.DataTextField = "Name";
            //    ddlLoaiLinhVuc.DataSource = ListCategoryChild;
            //    ddlLoaiLinhVuc.DataBind();
            //    ddlLoaiLinhVuc.Items.Insert(0, new ListItem { Text = SwirlingQuestionResource.LoaiLinhVucLabel, Value = "-1" });
            //}
        }
        private void LoadParam()
        {
            categoryID = WebUtils.ParseInt32FromQueryString("cateId", categoryID);
            categoryChildID = WebUtils.ParseInt32FromQueryString("cateChildId", categoryChildID);
            orderBy = WebUtils.ParseInt32FromQueryString("orderby", orderBy);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

            //if (categoryID > 0 || orderBy > 1 || !string.IsNullOrEmpty(keyword))
            //{
            //    hdfHasParam.Value = "HasParam";
            //}
        }
        private void LoadSetting()
        {
            GiaoDienHienThiQA = config.GiaoDienHienThi;
        }
    }
}
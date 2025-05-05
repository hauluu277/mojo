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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class ManageArticleComment : mojoBasePage
    {
        protected ArticleConfiguration config = new ArticleConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private static TimeZoneInfo _timeZone;
        protected static Double _timeOffset;

        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            Load += Page_Load;
            base.OnInit(e);
            LoadSettings();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleCommentArticle))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();

            PopulateLabels();

            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            ManageCommentControl.ModuleId = moduleId;
            ManageCommentControl.PageId = pageId;
            ManageCommentControl.Config = config;
            ManageCommentControl.SiteRoot = SiteRoot;
            ManageCommentControl.ImageSiteRoot = ImageSiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Danh sách bình luận tin bài");
            TitleControl.Visible = false;
            TitleControl.ModuleInstance = GetModule(moduleId);
            //if (siteUser.IsInRoles("Admins"))
            //{
            //    TitleControl.Visible = true;
            //}
            heading.Text = "Danh sách bình luận tin bài";

        }

        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            pnlContainer.ModuleId = moduleId;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(moduleSettings);
            _timeZone = SiteUtils.GetUserTimeZone();
            _timeOffset = SiteUtils.GetUserTimeOffset();
        }

        [WebMethod(EnableSession = true)]
        public static void PermissionStep(int itemId, string comment, int step)
        {
            Article article = new Article(itemId);
            if (article != null)
            {
                var users = SiteUtils.GetCurrentSiteUser();
                article.CommentByBoss = comment;
                //article.Step = step;
                //if (step == ArticleStepConstant.DA_NHAN_BIEN_TAP)
                //{
                //    article.BtvGuid = users.UserGuid;
                //}
                //else if (step == ArticleStepConstant.FOWARD_KIEM_DUYET_VIEN)
                //{
                //    article.BtvGuid = users.UserGuid;
                //}
                //else if (step == ArticleStepConstant.RETURN_CONG_TAC_VIEN)
                //{
                //    article.BtvGuid = users.UserGuid;
                //}
                //else if (step == ArticleStepConstant.RETURN_BIEN_TAP_VIEN)
                //{
                //    article.KdvGuid = users.UserGuid;
                //    article.KdvApprovedDate = DateTime.Now;
                //}

                article.LastModUtc = DateTime.Now;
                article.Save();

                //save log

                ArticleLog log = new ArticleLog();
                log.ArticleID = itemId;
                log.Comment = comment;
                log.CreateDate = DateTime.Now;
                log.EndDate = article.EndDate;
                //log.DateApprove = article.ApprovedDate;
                //log.DatePublish = article.PublishedDate;
                log.IsApprove = article.IsApproved;
                log.IsPublic = article.IsPublished;
                log.PostDate = article.CreatedDate;
                log.StartDate = article.StartDate;
                //log.Step = article.Step;
                log.UserID = users.UserId;
                log.Save();
            }
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string GetDetailLisence(int id)
        {
            var article = new Article(id);
            StringBuilder append = new StringBuilder();
            //if (article != null)
            //{
            //    var category = new CoreCategory(article.CategoryID);
            //    var totalNew = article.CtvNhuanButNew + article.BtvNhuanButNew + article.KdvNhuanButNew;
            //    var totalOld = article.CtvNhuanBut + article.BtvNhuanBut + article.KdvNhuanBut;
            //    var hitCount = ArticleViews.GetTotalArticle(article.ItemID);

            //    //render html
            //    append.Append("<div class=\"form-group col-sm-12\"><h4>" + article.Title + "</h4></div>");
            //    append.Append("<div class=\"form-group col-sm-12\">Tổng số lượng view cập nhật lại nhuận bút <strong class=\"text-danger\">" + category.MaxViewArticle + "</strong> view</div>");
            //    append.Append("<div class=\"form-group col-sm-12\"><strong>Tổng số tiền nhuận bút lần 1</strong></div>");
            //    //đã cập nhật nhuận bút lần đầu
            //    if (totalOld > 0)
            //    {
            //        append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Cộng tác viên:</label> <strong class=\"text-danger\">" + string.Format("{0:#,##} đ", article.CtvNhuanBut) + "</strong></div>");
            //        append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Biên tập viên:</label> <strong class=\"text-danger\">" + string.Format("{0:#,##} đ", article.BtvNhuanBut) + "</strong></div>");
            //        append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Kiểm duyệt viên:</label> <strong class=\"text-danger\">" + string.Format("{0:#,##} đ", article.KdvNhuanBut) + "</strong></div>");
            //        append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4 text-danger\">Tổng tiền: </label> <strong class=\"text-danger\">" + string.Format("{0:#,##} đ", totalOld) + "</strong></div>");
            //        //Đã cập nhật nhuận bút sau khi cán mốc view
            //        if (totalNew > 0)
            //        {
            //            append.Append("<div class=\"form-group col-sm-12\"><strong>Tổng số tiền nhuận bút cập nhật sau khi tin bài đạt mốc view</strong></div>");
            //            append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Cộng tác viên:</label> <strong class=\"text-danger\">" + string.Format("{0:#,##} đ", article.CtvNhuanButNew) + "</strong></div>");
            //            append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Biên tập viên:</label> <strong class=\"text-danger\">" + string.Format("{0:#,##} đ", article.BtvNhuanButNew) + "</strong></div>");
            //            append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Kiểm duyệt viên:</label> <strong class=\"text-danger\">" + string.Format("{0:#,##} đ", article.KdvNhuanButNew) + "</strong></div>");
            //            append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4 text-danger\">Tổng tiền: </label> <strong class=\"text-danger\">" + string.Format("{0:#,##} đ", totalNew) + "</strong></div>");
            //        }
            //        else if (category.MaxViewArticle > 0 && hitCount >= category.MaxViewArticle)
            //        {
            //            append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Cộng tác viên:</label> <div class=\"col-sm-8\">");
            //            append.Append("<input type=\"text\" class=\"form-control mask-number\" id=\"NhuanButCTV\" onblur=\"GetTotal()\"></input>");
            //            append.Append("</div></div>");

            //            append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Biên tập viên:</label> <div class=\"col-sm-8\">");
            //            append.Append("<input type=\"text\" class=\"form-control mask-number\" id=\"NhuanButBTV\"  onblur=\"GetTotal()\"></input>");
            //            append.Append("</div></div>");

            //            append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4\">Kiểm duyệt viên:</label> <div class=\"col-sm-8\">");
            //            append.Append("<input type=\"text\" class=\"form-control mask-number\" id=\"NhuanButKDV\" onblur=\"GetTotal()\"></input>");
            //            append.Append("</div></div>");

            //            append.Append("<div class=\"form-group col-sm-12\"><label class=\"col-sm-4 text-danger\">Tổng tiền: </label> <strong class=\"text-danger\" id=\"totalNhuanBut\">0 đ</strong></div>");
            //        }
            //    }
            //    else
            //    {
            //        append.Append("<div class=\"form-group col-sm-12\"><strong>Chưa có thông tin</strong></div>");
            //    }
            //}

            return append.ToString();
        }



        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string GetSetupButton(int id)
        {
            var article = new Article(id);
            StringBuilder append = new StringBuilder();
            if (article != null)
            {
                //var category = new CoreCategory(article.CategoryID);
                //var totalNew = article.CtvNhuanButNew + article.BtvNhuanButNew + article.KdvNhuanButNew;
                //var totalOld = article.CtvNhuanBut + article.BtvNhuanBut + article.KdvNhuanBut;
                //var hitCount = ArticleViews.GetTotalArticle(article.ItemID);
                //if (totalOld > 0 && hitCount >= category.MaxViewArticle)
                //{
                //    append.Append("<button type=\"button\" class=\"btn btn-success\" onclick=\"SaveNhuanBut(" + id + ")\">Cập nhật</button>");
                //}
            }
            append.Append("<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Đóng</button>");
            return append.ToString();
        }

        [WebMethod(EnableSession = true)]
        public static void SaveNhuanButNew(int id, string nhuanButCtv, string nhuanButBtv, string nhuanButKdv)
        {
            var article = new Article(id);
            decimal outCtv = 0;
            decimal.TryParse(nhuanButCtv, out outCtv);
            decimal outBtv = 0;
            decimal.TryParse(nhuanButBtv, out outBtv);
            decimal outKtv = 0;
            decimal.TryParse(nhuanButKdv, out outKtv);

            //article.CtvNhuanButNew = outCtv;
            //article.BtvNhuanButNew = outBtv;
            //article.KdvNhuanButNew = outKtv;
            article.Save();
        }



        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string GetTotalNhuanButNew(string nhuanButCtv, string nhuanButBtv, string nhuanButKdv)
        {
            var result = string.Empty;
            decimal total = 0;
            if (!string.IsNullOrEmpty(nhuanButCtv))
            {
                decimal outCtv = 0;
                decimal.TryParse(nhuanButCtv, out outCtv);
                total += outCtv;
            }
            if (!string.IsNullOrEmpty(nhuanButBtv))
            {
                decimal outBtv = 0;
                decimal.TryParse(nhuanButBtv, out outBtv);
                total += outBtv;
            }
            if (!string.IsNullOrEmpty(nhuanButKdv))
            {
                decimal outKdv = 0;
                decimal.TryParse(nhuanButKdv, out outKdv);
                total += outKdv;

            }
            if (total > 0)
            {
                return string.Format("{0:#,##} đ", total);
            }
            return "0 đ";
        }


        [WebMethod(EnableSession = true)]
        public static void TiepNhanBienTap(int itemId, string comment, int step)
        {
            Article article = new Article(itemId);
            if (article != null)
            {
                var users = SiteUtils.GetCurrentSiteUser();
                article.CommentByBoss = comment;
                //article.Step = step;
                //if (step == ArticleStepConstant.DA_NHAN_BIEN_TAP)
                //{
                //    article.BtvGuid = users.UserGuid;
                //}
                //else if (step == ArticleStepConstant.FOWARD_KIEM_DUYET_VIEN)
                //{
                //    article.BtvGuid = users.UserGuid;
                //}
                //else if (step == ArticleStepConstant.RETURN_CONG_TAC_VIEN)
                //{
                //    article.BtvGuid = users.UserGuid;
                //}
                //else if (step == ArticleStepConstant.RETURN_BIEN_TAP_VIEN)
                //{
                //    article.KdvGuid = users.UserGuid;
                //    article.KdvApprovedDate = DateTime.Now;
                //}

                article.LastModUtc = DateTime.Now;
                article.Save();

                //save log

                ArticleLog log = new ArticleLog();
                log.ArticleID = itemId;
                log.Comment = comment;
                log.CreateDate = DateTime.Now;
                log.EndDate = article.EndDate;
                //log.DateApprove = article.ApprovedDate;
                //log.DatePublish = article.PublishedDate;
                log.IsApprove = article.IsApproved;
                log.IsPublic = article.IsPublished;
                log.PostDate = article.CreatedDate;
                log.StartDate = article.StartDate;
                //log.Step = article.Step;
                log.UserID = users.UserId;
                log.Save();
            }
        }


        [WebMethod(EnableSession = true)]
        public static bool XuatBanNoiDung(string itemid, string ispublish, string role)
        {
            int _role = 0;
            if (!string.IsNullOrEmpty(role))
            {
                _role = int.Parse(role);
            }
            if (_role == RoleConstant.isApprove || _role == RoleConstant.isPost)
            {
                if (!string.IsNullOrEmpty(itemid) && !string.IsNullOrEmpty(ispublish))
                {
                    Article article = new Article(int.Parse(itemid));
                    if (article != null && article.ItemID > 0)
                    {
                        //đồng ý xuất bản
                        if (int.Parse(ispublish) == 1)
                        {
                            article.IsPublished = true;
                        }
                        else
                        {
                            article.IsPublished = false;
                        }
                        article.PublishedDate = DateTime.Now;
                        article.PublishedGuid = SiteUtils.GetCurrentSiteUser().UserGuid;
                        article.Save();
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
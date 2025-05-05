using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnnouncementFeatures.UI
{
    public partial class ManageControl : System.Web.UI.UserControl
    {
        #region Properties
        private string birthday = string.Empty;
        private string lastName = string.Empty;

        private int pageId = 0;
        private int moduleId = 0;
        private int pageNumber = 1;
        private int totalPages = 1;
        private int pageSize = 10;
        private int totalCounts = 0;
        private int categoryId = 0;
        private string keyword = string.Empty;
        public string EditLinkImageUrl = "/Data/SiteImages/" + WebConfigSettings.EditContentImage;
        public string DeleteLinkImageUrl = "/Data/SiteImages/" + WebConfigSettings.DeleteLinkImage;
        private readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private string siteRoot { get; set; }
        #endregion
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }
        #region Bind Event
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            rptAnnouncement.ItemDataBound += rptAnnouncement_ItemDataBound;
            rptAnnouncement.ItemCommand += rptAnnouncement_ItemCommand;
            //btnSearch.Click += BtnSearch_Click;
            btnAddPost.Click += BtnAddPost_Click;
            
        }
        private void BtnAddPost_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Announcement/EditPost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            WebUtils.SetupRedirect(this, url);
        }
        //private void BtnSearch_Click(object sender, EventArgs e)
        //{
        //    string pageUrl = SiteRoot + "/Admission/Manage.aspx?pageid=" + pageId
        //        + "&mid=" + moduleId
        //         + "&LastName=" + txtLastName.Text
        //         //+ "&FirstName=" +txtLastName.Text
        //         + "&gender=" + ddlGender.SelectedValue
        //         + "&birthday=" + dpBirthday.Text
        //         + "&numberCard=" + txtNumberCard.Text
        //         + "&email=" + txtEmail.Text
        //         + "&phoneNumber=" + txtPhoneNumber.Text
        //        + "&pageNumber=1";
        //    WebUtils.SetupRedirect(this, pageUrl);

        //}
        private void rptAnnouncement_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    int item = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/Announcement/EditPost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&itemid=" + item);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    int item = int.Parse(e.CommandArgument.ToString());
                    md_Announcement.Delete(item);
                    string urlRedirect = SiteRoot + "/Announcement/Manage.aspx?pageid=" + pageId
                + "&mid=" + moduleId
                    + "&itemId" + item;

                    WebUtils.SetupRedirect(this, urlRedirect);
                }
            }
        }
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/Announcement/editpost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());

        }
        private void rptAnnouncement_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                var imageButton = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(imageButton, "Dữ liệu xóa sẽ không khôi phục lại được?");
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParam();
            LoadControl();
        }
        private void LoadControl()
        {
            if (!IsPostBack)
            {

                BinAnnouncement();
                
               
            }
        }
        private void BinAnnouncement()
        {

            var data = md_Announcement.GetPage(pageNumber, pageSize, out totalPages);
            rptAnnouncement.DataSource = data;
            rptAnnouncement.DataBind();

            pgrAdmission.ShowFirstLast = true;
            pgrAdmission.PageSize = pageSize;
            pgrAdmission.PageCount = totalPages;
            pgrAdmission.CurrentIndex = pageNumber;
            pnlAdmissionPager.Visible = (totalPages > 1);
        }
        private void LoadParam()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pageNumber", pageNumber);
            categoryId = WebUtils.ParseInt32FromQueryString("categoryID", categoryId);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
        }
        protected string FormatAnnouncementDate(DateTime startDate)
        {
            if (startDate != null)
            {
                return string.Format("{0:dd/MM/yyyy}", startDate);
            }
            return "";
        }
    }
}
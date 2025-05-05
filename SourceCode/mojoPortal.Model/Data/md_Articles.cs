using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("md_Articles")]
    public class md_Articles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public int ModuleID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int CommentCount { get; set; }
        public int HitCount { get; set; }
        public Nullable<System.Guid> ArticleGuid { get; set; }
        public Nullable<System.Guid> ModuleGuid { get; set; }
        public string Location { get; set; }
        public Nullable<System.Guid> UserGuid { get; set; }
        public string CreatedByUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.Guid> LastModUserGuid { get; set; }
        public Nullable<System.DateTime> LastModUtc { get; set; }
        public string ItemUrl { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.Guid> ApprovedGuid { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<bool> AllowComment { get; set; }
        public Nullable<bool> IsHot { get; set; }
        public Nullable<bool> IsHome { get; set; }
        public string Tag { get; set; }
        public string FTS { get; set; }
        public Nullable<int> LangID { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<System.Guid> PublishedGuid { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        public Nullable<bool> IncludeInFeed { get; set; }
        public string CommentByBoss { get; set; }
        public string AudioUrl { get; set; }
        public Nullable<System.Guid> PollGuid { get; set; }
        public Nullable<bool> AllowWCAG { get; set; }
        public string CompiledMeta { get; set; }
        public string MetaCreator { get; set; }
        public string MetaIdentifier { get; set; }
        public string MetaPublisher { get; set; }
        public Nullable<System.DateTime> MetaDate { get; set; }
        public Nullable<bool> IsHotNew { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string ArticleReference { get; set; }
        public string TitleFTS { get; set; }
        public string AuthorFTS { get; set; }
        public string SapoFTS { get; set; }
        public Nullable<System.DateTime> CreateDateArticle { get; set; }
        public bool IsCongThanhVien { get; set; }
        public string ClientId { get; set; }
        public string ViTriHienThiNgayDang { get; set; }
        public bool IsHienThiTacGia { get; set; }
        public int? ClientBaiVietId { get; set; }
    }
}

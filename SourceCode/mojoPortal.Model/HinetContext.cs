using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using mojoPortal.Model.Data;
using System.Data.Entity.Infrastructure;
using mojoPortal.Model.Entities;

namespace mojoPortal.Model
{
    public class HinetContext : DbContext
    {

        public HinetContext()
            : base("Name=HinetContext")
        {
            //sử dụng cho việc unit test
            Database.SetInitializer<HinetContext>(null);
        }
        public virtual DbSet<core_Category> core_Category { get; set; }
        public virtual DbSet<core_CategoryUserArticle> core_CategoryUserArticle { get; set; }
        public virtual DbSet<core_CCTC> core_CCTC { get; set; }
        public virtual DbSet<core_CCTC_Department> core_CCTC_Department { get; set; }
        public virtual DbSet<core_CCTC_Leader> core_CCTC_Leader { get; set; }
        public virtual DbSet<core_Client> core_Client { get; set; }
        public virtual DbSet<md_Articles> md_Articles { get; set; }
        public virtual DbSet<md_ArticleCategory> md_ArticleCategory { get; set; }
        public virtual DbSet<mp_Sites> mp_Sites { get; set; }
        public virtual DbSet<mp_Users> mp_Users { get; set; }
        public virtual DbSet<core_Menu> core_Menu { get; set; }
        public virtual DbSet<core_SettingMenu> core_SettingMenu { get; set; }
        public virtual DbSet<core_SettingService> core_SettingService { get; set; }
        public virtual DbSet<mp_Roles> mp_Roles { get; set; }
        public virtual DbSet<core_Token> core_Token { get; set; }
        public virtual DbSet<mp_UserRoles> mp_UserRoles { get; set; }
        public virtual DbSet<core_TokenAD> core_TokenAD { get; set; }
        public virtual DbSet<md_BaoCao> md_BaoCao { get; set; }
        public virtual DbSet<core_ThuTuc_BieuMau> core_ThuTuc_BieuMau { get; set; }
        public virtual DbSet<core_ThuTuc> core_ThuTuc { get; set; }
        public virtual DbSet<core_ThuTuc_ThanhPhanHS> core_ThuTuc_ThanhPhanHS { get; set; }
        public virtual DbSet<core_ThongKeTruyCap> core_ThongKeTruyCap { get; set; }
        public virtual DbSet<core_GiaoDien> core_GiaoDien { get; set; }
        public virtual DbSet<core_LogHeThong> core_LogHeThong { get; set; }
        public virtual DbSet<mp_ContactFormMessage> mp_ContactFormMessage { get; set; }
        public virtual DbSet<core_CauHinhHienThiLog> core_CauHinhHienThiLog { get; set; }
        public virtual DbSet<bentre_BieuMauThongTin> bentre_BieuMauThongTin { get; set; }
        public virtual DbSet<bentre_TieuChiBieuMau> bentre_TieuChiBieuMau { get; set; }
        public virtual DbSet<bentre_KeKhaiBieuMau> bentre_KeKhaiBieuMau { get; set; }
        public virtual DbSet<bentre_NopBieuMau> bentre_NopBieuMau { get; set; }
        public virtual DbSet<core_ClientCategory> Core_ClientCategories { get; set; }
        public virtual DbSet<core_SettingSSO> core_SettingSSO { get; set; }
        public virtual DbSet<md_LichCongTacNew> md_LichCongTacNew { get; set; } 
        public virtual DbSet<core_LichLamViec> core_LichLamViec { get; set; }
        public virtual DbSet<core_DanhMuc> core_DanhMuc { get; set; }
        public static HinetContext Create()
        {
            return new HinetContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified);

            //foreach (var entry in modifiedEntries)
            //{
            //    IAuditableEntity entity = entry.Entity as IAuditableEntity;
            //    if (entity != null)
            //    {
            //        string identityName = Thread.CurrentPrincipal.Identity.Name;
            //        var userId = this.Users.Where(x => x.UserName == identityName).Select(x => x.Id).FirstOrDefault();

            //        DateTime now = DateTime.Now;

            //        if (entry.State == System.Data.Entity.EntityState.Added)
            //        {
            //            entity.CreatedBy = identityName;
            //            entity.CreatedDate = now;
            //            entity.CreatedID = userId;
            //        }
            //        else
            //        {
            //            base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
            //            base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
            //        }

            //        entity.UpdatedBy = identityName;
            //        entity.UpdatedDate = now;
            //        entity.UpdatedID = userId;
            //    }
            //}

            return base.SaveChanges();
        }
    }
}

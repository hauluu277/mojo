﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mojoPortal.Model.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HinetContext : DbContext
    {
        public HinetContext()
            : base("name=HinetContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<core_Category> core_Category { get; set; }
        public virtual DbSet<core_CategoryUserArticle> core_CategoryUserArticle { get; set; }
        public virtual DbSet<core_CCTC> core_CCTC { get; set; }
        public virtual DbSet<core_CCTC_Department> core_CCTC_Department { get; set; }
        public virtual DbSet<core_CCTC_Leader> core_CCTC_Leader { get; set; }
        public virtual DbSet<core_Client> core_Client { get; set; }
        public virtual DbSet<md_Articles> md_Articles { get; set; }
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
        public virtual DbSet<bentre_BieuMauThongTin> bentre_BieuMauThongTin { get; set; }
        public virtual DbSet<core_ThongKeTruyCap> core_ThongKeTruyCap { get; set; }
        public virtual DbSet<core_GiaoDien> core_GiaoDien { get; set; }
    }
}

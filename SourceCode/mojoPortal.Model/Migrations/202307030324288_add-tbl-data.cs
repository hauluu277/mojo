namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtbldata : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.core_Category",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            ParentID = c.Int(),
            //            SiteID = c.Int(nullable: false),
            //            Name = c.String(),
            //            NameEN = c.String(),
            //            Description = c.String(),
            //            ItemCount = c.Int(nullable: false),
            //            CreatedUtc = c.DateTime(nullable: false),
            //            CreatedBy = c.Guid(nullable: false),
            //            ModifiedUtc = c.DateTime(nullable: false),
            //            ModifiedBy = c.Guid(nullable: false),
            //            Priority = c.Int(),
            //            LangID = c.Int(),
            //            IconID = c.Int(),
            //            Automatic = c.Boolean(),
            //            CoreSkinID = c.Int(),
            //            CoreSkinDefault = c.Boolean(),
            //            IsPhongBan = c.Boolean(),
            //            ShowMenuLeft = c.Boolean(),
            //            PathIMG = c.String(),
            //            PathFile = c.String(),
            //            SubName = c.String(),
            //            IsTinTuc = c.Boolean(),
            //            IsLinhVucDieuTra = c.Boolean(),
            //            Code = c.String(),
            //            Sumary = c.String(),
            //            TargetBlank = c.Boolean(),
            //            Color = c.String(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_CategoryUserArticle",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            UserID = c.Int(),
            //            CategoryID = c.Int(),
            //            TypeRole = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_CCTC",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            SiteID = c.Int(),
            //            Description = c.String(),
            //            ShowLanhDao = c.Boolean(),
            //            ShowPhongBan = c.Boolean(),
            //            TitleBoxLanhDao = c.String(),
            //            TitleBoxPhongBan = c.String(),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedByUser = c.Int(),
            //            EditedDate = c.DateTime(),
            //            EditedBy = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_CCTC_Department",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            CCTC_ID = c.Int(),
            //            Name = c.String(),
            //            PathIMG = c.String(),
            //            ParentID = c.Int(),
            //            Description = c.String(),
            //            OrderBy = c.Int(),
            //            LinkDetail = c.String(),
            //            CCTC_Leader_ID = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_CCTC_Leader",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            CCTC_ID = c.Int(),
            //            Title = c.String(),
            //            PathIMG = c.String(),
            //            OrderBy = c.Int(),
            //            ChucVu = c.String(),
            //            Email = c.String(),
            //            Phone = c.String(),
            //            LinkDetail = c.String(),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_Client",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            ClientID = c.String(),
            //            ClientUrl = c.String(),
            //            ClientCallBack = c.String(),
            //            ClientSignIn = c.String(),
            //            ClientSignOut = c.String(),
            //            ClientName = c.String(),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedByUser = c.Int(),
            //            EditedDate = c.DateTime(),
            //            EditedBy = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_Menu",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            SiteID = c.Int(),
            //            ParentID = c.Int(),
            //            Name = c.String(),
            //            LinkMenu = c.String(),
            //            ImageUrl = c.String(),
            //            OrderBy = c.Int(),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.Int(),
            //            UpdatedDate = c.DateTime(),
            //            UpdateBy = c.Int(),
            //            StyleCss = c.String(),
            //            TypeMenu = c.Int(),
            //            Show = c.Boolean(),
            //            IsDisplayLink = c.Boolean(),
            //            IsPhongBan = c.Boolean(),
            //            IsEnglish = c.Boolean(),
            //            TypeLink = c.Int(),
            //            ItemLink = c.Long(),
            //            IsLogin = c.Boolean(),
            //            NoClick = c.Boolean(),
            //            TargetBlank = c.Boolean(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_SettingMenu",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            SiteID = c.Int(),
            //            UrlIMG = c.String(),
            //            UrlItem = c.String(),
            //            TypeIMG = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_SettingService",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            SiteID = c.Int(),
            //            Name = c.String(),
            //            ServiceUrl = c.String(),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedByUser = c.Int(),
            //            EditedBy = c.Int(),
            //            EditedDate = c.DateTime(),
            //            IsNew = c.Boolean(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_Token",
            //    c => new
            //        {
            //            ItemID = c.Long(nullable: false, identity: true),
            //            ClientID = c.String(),
            //            Token = c.String(),
            //            DateExpired = c.DateTime(),
            //            TokenID = c.Long(),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedByUser = c.Int(),
            //            EditedDate = c.DateTime(),
            //            EditedBy = c.Int(),
            //            UserName = c.String(),
            //            ExpiredIn = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_TokenAD",
            //    c => new
            //        {
            //            ItemId = c.Long(nullable: false, identity: true),
            //            access_token = c.String(),
            //            token_type = c.String(),
            //            expires_in = c.Int(),
            //            refresh_token = c.String(),
            //            apikey = c.String(),
            //            username = c.String(),
            //            date_created = c.DateTime(),
            //            date_expired = c.DateTime(),
            //            clientId = c.String(),
            //        })
            //    .PrimaryKey(t => t.ItemId);
            
            //CreateTable(
            //    "dbo.core_ThongKeTruyCap",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            Type = c.String(),
            //            Total = c.Int(),
            //            CurrentDay = c.DateTime(),
            //            CurrentWeek = c.Int(),
            //            Year = c.Int(),
            //            DateAdd = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_ThuTuc",
            //    c => new
            //        {
            //            ItemID = c.Long(nullable: false, identity: true),
            //            IdCoQuan = c.Int(),
            //            IdMucDo = c.Int(),
            //            IdCapDoThuTuc = c.Int(),
            //            MaThuTuc = c.String(),
            //            TenThuTuc = c.String(),
            //            IdLinhVuc = c.Int(),
            //            CachThucThucHien = c.String(),
            //            IdDoiTuongThucHien = c.Int(),
            //            TrinhTuThucHien = c.String(),
            //            ThoiHanGianQuyet = c.String(),
            //            Phi = c.String(),
            //            LePhi = c.String(),
            //            ThanhPhanHoSo = c.String(),
            //            SoLuongHoSo = c.Int(),
            //            YeuCauDieuKien = c.String(),
            //            CanCuPhapLy = c.String(),
            //            KetQuaThucHien = c.String(),
            //            LinkDVC = c.String(),
            //            IsPublish = c.Boolean(),
            //            CreatedBy = c.String(),
            //            CreatedByUser = c.Int(),
            //            CreatedDate = c.DateTime(),
            //            SiteID = c.Int(),
            //            EditDate = c.DateTime(),
            //            EditByUser = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.core_ThuTuc_BieuMau",
            //    c => new
            //        {
            //            ItemId = c.Long(nullable: false, identity: true),
            //            IdThuTuc = c.Long(),
            //            TenMau = c.String(),
            //            PathFile = c.String(),
            //        })
            //    .PrimaryKey(t => t.ItemId);
            
            //CreateTable(
            //    "dbo.core_ThuTuc_ThanhPhanHS",
            //    c => new
            //        {
            //            ItemID = c.Long(nullable: false, identity: true),
            //            IdThuTuc = c.Long(),
            //            TenGiayTo = c.String(),
            //            MauDonToKhai = c.String(),
            //            SoLuong = c.String(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.md_Articles",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            ModuleID = c.Int(nullable: false),
            //            SiteID = c.Int(),
            //            CategoryID = c.Int(),
            //            Title = c.String(),
            //            Summary = c.String(),
            //            Description = c.String(),
            //            ImageUrl = c.String(),
            //            StartDate = c.DateTime(),
            //            EndDate = c.DateTime(),
            //            CommentCount = c.Int(nullable: false),
            //            HitCount = c.Int(nullable: false),
            //            ArticleGuid = c.Guid(),
            //            ModuleGuid = c.Guid(),
            //            Location = c.String(),
            //            UserGuid = c.Guid(),
            //            CreatedByUser = c.String(),
            //            CreatedDate = c.DateTime(),
            //            LastModUserGuid = c.Guid(),
            //            LastModUtc = c.DateTime(),
            //            ItemUrl = c.String(),
            //            MetaTitle = c.String(),
            //            MetaKeywords = c.String(),
            //            MetaDescription = c.String(),
            //            IsApproved = c.Boolean(),
            //            ApprovedGuid = c.Guid(),
            //            ApprovedDate = c.DateTime(),
            //            AllowComment = c.Boolean(),
            //            IsHot = c.Boolean(),
            //            IsHome = c.Boolean(),
            //            Tag = c.String(),
            //            FTS = c.String(),
            //            LangID = c.Int(),
            //            IsPublished = c.Boolean(),
            //            PublishedGuid = c.Guid(),
            //            PublishedDate = c.DateTime(),
            //            IncludeInFeed = c.Boolean(),
            //            CommentByBoss = c.String(),
            //            AudioUrl = c.String(),
            //            PollGuid = c.Guid(),
            //            AllowWCAG = c.Boolean(),
            //            CompiledMeta = c.String(),
            //            MetaCreator = c.String(),
            //            MetaIdentifier = c.String(),
            //            MetaPublisher = c.String(),
            //            MetaDate = c.DateTime(),
            //            IsHotNew = c.Boolean(),
            //            IsDelete = c.Boolean(),
            //            ArticleReference = c.String(),
            //            TitleFTS = c.String(),
            //            AuthorFTS = c.String(),
            //            SapoFTS = c.String(),
            //            CreateDateArticle = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.md_BaoCao",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            TenBaoCao = c.String(),
            //            NamChuKyBaoCao = c.Int(),
            //            BieuMau = c.String(),
            //            SoQuyetDinhCongBo = c.String(),
            //            NgayCongBo = c.DateTime(),
            //            PathFile = c.String(),
            //            LinhVucID = c.Int(),
            //            CreatedBy = c.String(),
            //            CreatedByUser = c.Int(),
            //            CreatedDate = c.DateTime(),
            //            ItemUrl = c.String(),
            //            ItemGuid = c.Guid(),
            //            SiteID = c.Int(),
            //            IsPublish = c.Boolean(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.mp_Roles",
            //    c => new
            //        {
            //            RoleID = c.Int(nullable: false, identity: true),
            //            SiteID = c.Int(nullable: false),
            //            RoleName = c.String(),
            //            DisplayName = c.String(),
            //            SiteGuid = c.Guid(),
            //            RoleGuid = c.Guid(),
            //        })
            //    .PrimaryKey(t => t.RoleID)
            //    .ForeignKey("dbo.mp_Sites", t => t.SiteID, cascadeDelete: true)
            //    .Index(t => t.SiteID);
            
            //CreateTable(
            //    "dbo.mp_Sites",
            //    c => new
            //        {
            //            SiteID = c.Int(nullable: false, identity: true),
            //            SiteGuid = c.Guid(nullable: false),
            //            SiteAlias = c.String(),
            //            SiteName = c.String(),
            //            Skin = c.String(),
            //            Logo = c.String(),
            //            Icon = c.String(),
            //            AllowUserSkins = c.Boolean(nullable: false),
            //            AllowPageSkins = c.Boolean(nullable: false),
            //            AllowHideMenuOnPages = c.Boolean(nullable: false),
            //            AllowNewRegistration = c.Boolean(nullable: false),
            //            UseSecureRegistration = c.Boolean(nullable: false),
            //            UseSSLOnAllPages = c.Boolean(nullable: false),
            //            DefaultPageKeyWords = c.String(),
            //            DefaultPageDescription = c.String(),
            //            DefaultPageEncoding = c.String(),
            //            DefaultAdditionalMetaTags = c.String(),
            //            IsServerAdminSite = c.Boolean(nullable: false),
            //            UseLdapAuth = c.Boolean(nullable: false),
            //            AutoCreateLdapUserOnFirstLogin = c.Boolean(nullable: false),
            //            LdapServer = c.String(),
            //            LdapPort = c.Int(nullable: false),
            //            LdapDomain = c.String(),
            //            LdapRootDN = c.String(),
            //            LdapUserDNKey = c.String(),
            //            ReallyDeleteUsers = c.Boolean(nullable: false),
            //            UseEmailForLogin = c.Boolean(nullable: false),
            //            AllowUserFullNameChange = c.Boolean(nullable: false),
            //            EditorSkin = c.String(),
            //            DefaultFriendlyUrlPatternEnum = c.String(),
            //            AllowPasswordRetrieval = c.Boolean(nullable: false),
            //            AllowPasswordReset = c.Boolean(nullable: false),
            //            RequiresQuestionAndAnswer = c.Boolean(nullable: false),
            //            MaxInvalidPasswordAttempts = c.Int(nullable: false),
            //            PasswordAttemptWindowMinutes = c.Int(nullable: false),
            //            RequiresUniqueEmail = c.Boolean(nullable: false),
            //            PasswordFormat = c.Int(nullable: false),
            //            MinRequiredPasswordLength = c.Int(nullable: false),
            //            MinReqNonAlphaChars = c.Int(nullable: false),
            //            PwdStrengthRegex = c.String(),
            //            DefaultEmailFromAddress = c.String(),
            //            EnableMyPageFeature = c.Boolean(nullable: false),
            //            EditorProvider = c.String(),
            //            CaptchaProvider = c.String(),
            //            DatePickerProvider = c.String(),
            //            RecaptchaPrivateKey = c.String(),
            //            RecaptchaPublicKey = c.String(),
            //            WordpressAPIKey = c.String(),
            //            WindowsLiveAppID = c.String(),
            //            WindowsLiveKey = c.String(),
            //            AllowOpenIDAuth = c.Boolean(nullable: false),
            //            AllowWindowsLiveAuth = c.Boolean(nullable: false),
            //            GmapApiKey = c.String(),
            //            ApiKeyExtra1 = c.String(),
            //            ApiKeyExtra2 = c.String(),
            //            ApiKeyExtra3 = c.String(),
            //            ApiKeyExtra4 = c.String(),
            //            ApiKeyExtra5 = c.String(),
            //            DisableDbAuth = c.Boolean(),
            //            ArticleCategoryID = c.Int(),
            //            CoreEventCategoryID = c.Int(),
            //            CoreLoaiVanBanCategoryID = c.Int(),
            //            CoreLinhVucVanBanCategoryID = c.Int(),
            //            CoreCoQuanBanHanhVanBanCategoryID = c.Int(),
            //            CoreDonviCategoryID = c.Int(),
            //            CoreLinhVucHoiDapCategoryID = c.Int(),
            //            CoreDuLieuDaPhuongTienCategoryID = c.Int(),
            //            CoreRSSCategoryID = c.Int(),
            //            CoreHoiDongCategoryID = c.Int(),
            //            CoreChuDeCategoryID = c.Int(),
            //            IsView = c.Int(),
            //            TemplateType = c.Int(),
            //            IsTemplate = c.Boolean(),
            //            UrlSiteMap = c.String(),
            //            TemplateSite = c.Int(),
            //            SiteSubTitle = c.String(),
            //            FanPageIframe = c.String(),
            //            Footer = c.String(),
            //            LinhVucID = c.Int(),
            //            NoiDungDieuTra = c.String(),
            //            Nam = c.Int(),
            //            TanSuatDieuTra = c.String(),
            //            PhamViSoLieu = c.String(),
            //            DoiTuongDieuTra = c.String(),
            //            PathIMG = c.String(),
            //            CreatedDate = c.DateTime(),
            //            CreatedByUser = c.Int(),
            //            IsTongDieuTra = c.Boolean(),
            //            IsCuocDieuTra = c.Boolean(),
            //            ParentID = c.Int(),
            //            HanGopY = c.DateTime(),
            //            TrangThaiDieuTra = c.Int(),
            //            FileDuThao = c.String(),
            //        })
            //    .PrimaryKey(t => t.SiteID);
            
            //CreateTable(
            //    "dbo.mp_UserRoles",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            UserID = c.Int(nullable: false),
            //            RoleID = c.Int(nullable: false),
            //            UserGuid = c.Guid(),
            //            RoleGuid = c.Guid(),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.mp_Users",
            //    c => new
            //        {
            //            UserID = c.Int(nullable: false, identity: true),
            //            SiteID = c.Int(nullable: false),
            //            Name = c.String(),
            //            LoginName = c.String(),
            //            Email = c.String(),
            //            LoweredEmail = c.String(),
            //            PasswordQuestion = c.String(),
            //            PasswordAnswer = c.String(),
            //            Gender = c.String(),
            //            ProfileApproved = c.Boolean(nullable: false),
            //            RegisterConfirmGuid = c.Guid(),
            //            ApprovedForForums = c.Boolean(nullable: false),
            //            Trusted = c.Boolean(nullable: false),
            //            DisplayInMemberList = c.Boolean(),
            //            WebSiteURL = c.String(),
            //            Country = c.String(),
            //            State = c.String(),
            //            Occupation = c.String(),
            //            Interests = c.String(),
            //            MSN = c.String(),
            //            Yahoo = c.String(),
            //            AIM = c.String(),
            //            ICQ = c.String(),
            //            TotalPosts = c.Int(nullable: false),
            //            AvatarUrl = c.String(),
            //            TimeOffsetHours = c.Int(nullable: false),
            //            Signature = c.String(),
            //            DateCreated = c.DateTime(nullable: false),
            //            UserGuid = c.Guid(),
            //            Skin = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            LastActivityDate = c.DateTime(),
            //            LastLoginDate = c.DateTime(),
            //            LastPasswordChangedDate = c.DateTime(),
            //            LastLockoutDate = c.DateTime(),
            //            FailedPasswordAttemptCount = c.Int(),
            //            FailedPwdAttemptWindowStart = c.DateTime(),
            //            FailedPwdAnswerAttemptCount = c.Int(),
            //            FailedPwdAnswerWindowStart = c.DateTime(),
            //            IsLockedOut = c.Boolean(nullable: false),
            //            MobilePIN = c.String(),
            //            PasswordSalt = c.String(),
            //            Comment = c.String(),
            //            OpenIDURI = c.String(),
            //            WindowsLiveID = c.String(),
            //            SiteGuid = c.Guid(),
            //            TotalRevenue = c.Decimal(precision: 18, scale: 2),
            //            FirstName = c.String(),
            //            LastName = c.String(),
            //            Pwd = c.String(),
            //            MustChangePwd = c.Boolean(),
            //            NewEmail = c.String(),
            //            EditorPreference = c.String(),
            //            EmailChangeGuid = c.Guid(),
            //            TimeZoneId = c.String(),
            //            PasswordResetGuid = c.Guid(),
            //            RolesChanged = c.Boolean(),
            //            AuthorBio = c.String(),
            //            DateOfBirth = c.DateTime(),
            //            PwdFormat = c.Int(nullable: false),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            SiteSync = c.Int(),
            //            SiteManager = c.String(),
            //            AD_MCS = c.String(),
            //            AD_TenDonViCS = c.String(),
            //            AD_PhongBan = c.String(),
            //            AD_TenPhongBan = c.String(),
            //            ChucVu = c.String(),
            //            MaChucVu = c.String(),
            //            IS_VU = c.Boolean(),
            //            IS_CHICUC = c.Boolean(),
            //            IS_CUCTTDL = c.Boolean(),
            //            DepartmentId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.mp_Roles", "SiteID", "dbo.mp_Sites");
            //DropIndex("dbo.mp_Roles", new[] { "SiteID" });
            //DropTable("dbo.mp_Users");
            //DropTable("dbo.mp_UserRoles");
            //DropTable("dbo.mp_Sites");
            //DropTable("dbo.mp_Roles");
            //DropTable("dbo.md_BaoCao");
            //DropTable("dbo.md_Articles");
            //DropTable("dbo.core_ThuTuc_ThanhPhanHS");
            //DropTable("dbo.core_ThuTuc_BieuMau");
            //DropTable("dbo.core_ThuTuc");
            //DropTable("dbo.core_ThongKeTruyCap");
            //DropTable("dbo.core_TokenAD");
            //DropTable("dbo.core_Token");
            //DropTable("dbo.core_SettingService");
            //DropTable("dbo.core_SettingMenu");
            //DropTable("dbo.core_Menu");
            //DropTable("dbo.core_Client");
            //DropTable("dbo.core_CCTC_Leader");
            //DropTable("dbo.core_CCTC_Department");
            //DropTable("dbo.core_CCTC");
            //DropTable("dbo.core_CategoryUserArticle");
            //DropTable("dbo.core_Category");
        }
    }
}

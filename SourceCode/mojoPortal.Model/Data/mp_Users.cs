using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("mp_Users")]
    public  class mp_Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserID { get; set; }
        public int SiteID { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }
        public string LoweredEmail { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public string Gender { get; set; }
        public bool ProfileApproved { get; set; }
        public Nullable<System.Guid> RegisterConfirmGuid { get; set; }
        public bool ApprovedForForums { get; set; }
        public bool Trusted { get; set; }
        public Nullable<bool> DisplayInMemberList { get; set; }
        public string WebSiteURL { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Occupation { get; set; }
        public string Interests { get; set; }
        public string MSN { get; set; }
        public string Yahoo { get; set; }
        public string AIM { get; set; }
        public string ICQ { get; set; }
        public int TotalPosts { get; set; }
        public string AvatarUrl { get; set; }
        public int TimeOffsetHours { get; set; }
        public string Signature { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.Guid> UserGuid { get; set; }
        public string Skin { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> LastActivityDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public Nullable<System.DateTime> LastLockoutDate { get; set; }
        public Nullable<int> FailedPasswordAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPwdAttemptWindowStart { get; set; }
        public Nullable<int> FailedPwdAnswerAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPwdAnswerWindowStart { get; set; }
        public bool IsLockedOut { get; set; }
        public string MobilePIN { get; set; }
        public string PasswordSalt { get; set; }
        public string Comment { get; set; }
        public string OpenIDURI { get; set; }
        public string WindowsLiveID { get; set; }
        public Nullable<System.Guid> SiteGuid { get; set; }
        public Nullable<decimal> TotalRevenue { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pwd { get; set; }
        public Nullable<bool> MustChangePwd { get; set; }
        public string NewEmail { get; set; }
        public string EditorPreference { get; set; }
        public Nullable<System.Guid> EmailChangeGuid { get; set; }
        public string TimeZoneId { get; set; }
        public Nullable<System.Guid> PasswordResetGuid { get; set; }
        public Nullable<bool> RolesChanged { get; set; }
        public string AuthorBio { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public int PwdFormat { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public Nullable<int> SiteSync { get; set; }
        public string SiteManager { get; set; }
        public string AD_MCS { get; set; }
        public string AD_TenDonViCS { get; set; }
        public string AD_PhongBan { get; set; }
        public string AD_TenPhongBan { get; set; }
        public string ChucVu { get; set; }
        public string MaChucVu { get; set; }
        public Nullable<bool> IS_VU { get; set; }
        public Nullable<bool> IS_CHICUC { get; set; }
        public Nullable<bool> IS_CUCTTDL { get; set; }
        public Nullable<int> DepartmentId { get; set; }
    }
}

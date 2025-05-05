using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("mp_Roles")]
    public  class mp_Roles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RoleID { get; set; }
        public int SiteID { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }
        public Nullable<System.Guid> SiteGuid { get; set; }
        public Nullable<System.Guid> RoleGuid { get; set; }

        public virtual mp_Sites mp_Sites { get; set; }
    }
}

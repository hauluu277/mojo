using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_Token")]
    public class core_Token
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ItemID { get; set; }
        public string ClientID { get; set; }
        public string Token { get; set; }
        public Nullable<System.DateTime> DateExpired { get; set; }
        public Nullable<long> TokenID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> CreatedByUser { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public Nullable<int> EditedBy { get; set; }
        public string UserName { get; set; }
        public Nullable<int> ExpiredIn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_TokenAD")]
    public  class core_TokenAD
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ItemId { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public Nullable<int> expires_in { get; set; }
        public string refresh_token { get; set; }
        public string apikey { get; set; }
        public string username { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<System.DateTime> date_expired { get; set; }
        public string clientId { get; set; }
    }
}

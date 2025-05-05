using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_CCTC_Leader")]
    public class core_CCTC_Leader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public Nullable<int> CCTC_ID { get; set; }
        public string Title { get; set; }
        public string PathIMG { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public string ChucVu { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkDetail { get; set; }
        public string Description { get; set; }
    }
}

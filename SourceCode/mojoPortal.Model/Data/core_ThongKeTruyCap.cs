using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_ThongKeTruyCap")]
    public class core_ThongKeTruyCap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public string Type { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<System.DateTime> CurrentDay { get; set; }
        public Nullable<int> CurrentWeek { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<System.DateTime> DateAdd { get; set; }
        public int? CurrentMonth { get; set; }

    }
}

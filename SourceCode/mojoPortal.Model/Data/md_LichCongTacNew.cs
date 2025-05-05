using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("md_LichCongTacNew")]
    public class md_LichCongTacNew
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public int SideID { get; set; }
        public int ModuleID { get; set; }
        public int PageID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Summary { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Week { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? StartWeek { get; set; }
        public DateTime? EndWeed { get; set; }
        public int? Nam { get; set; }
        public int? DayID { get; set; }
        public string Thu { get; set; }
        public string ThoiGian { get; set; }
        public string NoiDung { get; set; }
        public string DiaDiem { get; set; }
        public string ThanhPhanThamDu { get; set; }
    }
}

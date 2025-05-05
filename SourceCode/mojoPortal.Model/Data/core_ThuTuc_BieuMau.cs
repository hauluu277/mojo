using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_ThuTuc_BieuMau")]
    public  class core_ThuTuc_BieuMau
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ItemId { get; set; }
        public Nullable<long> IdThuTuc { get; set; }
        public string TenMau { get; set; }
        public string PathFile { get; set; }
        public int TotalDownload { get; set; }
    }
}

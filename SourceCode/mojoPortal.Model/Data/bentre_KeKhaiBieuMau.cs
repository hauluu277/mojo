using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("bentre_KeKhaiBieuMau")]
    public  class bentre_KeKhaiBieuMau
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int IdNopBieuMau { get; set; }
        public DateTime? NgayNop { get; set; }
    }
}

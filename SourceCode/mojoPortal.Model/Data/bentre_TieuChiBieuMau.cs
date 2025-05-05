using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("bentre_TieuChiBieuMau")]
    public  class bentre_TieuChiBieuMau
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Ten { get; set; }
        public string Key { get; set; }
        public string CongThuc { get; set; }
        public bool Required { get; set; }
        public bool IsComboBox { get; set; }
        public int? SoThuTu { get; set; }
        public int? DataType { get; set; }
        public double? GioiHanTren { get; set; }
        public double? GioiHanDuoi { get; set; }
        public int IdBieuMau { get; set; }
    }
}

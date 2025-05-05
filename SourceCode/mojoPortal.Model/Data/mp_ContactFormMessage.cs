using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("mp_ContactFormMessage")]
    public class mp_ContactFormMessage
    {
        [Key]
        public Guid RowGuid { get; set; }
        public Guid SiteGuid { get; set; }
        public Guid ModuleGuid { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string CreatedFromIpAddress { get; set; }
        public Guid UserGuid { get; set; }
        public int TrangThai { get; set; }
        public DateTime? ThoiGianPhanHoi { get; set; }
        public string NoiDungPhanHoi { get; set; }
    }
}

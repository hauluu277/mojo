using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mojoPortal.Model.Data
{
    [Table("tbldata")]
    public class tbldata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ItemID { get; set; }
        public string Name { get; set; }
    }
}

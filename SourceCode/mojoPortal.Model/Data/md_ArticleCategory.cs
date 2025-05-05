using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("md_ArticleCategory")]
    public class md_ArticleCategory 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ItemID { get; set; }
        public Nullable<int> CategoryID { get; set; }

        public Nullable<int> SiteID { get; set; }

       
        public Nullable<long> ArticleID { get; set; }
    }
}

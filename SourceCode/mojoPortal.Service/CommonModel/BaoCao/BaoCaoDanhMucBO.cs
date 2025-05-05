using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.BaoCao
{
    public class BaoCaoDanhMucBO
    {
        public core_Category core_Category { get; set; }
        public List<core_Category> core_Categories { get; set; }
    }

    public class BaoCaoDanhMucResultBO
    {
        public core_Category core_Category { get; set; }
        public List<md_Articles> md_Articles { get; set; }
        public int CountArticle { get; set; }
    }

    public class BaoCaoDanhMucResultBieuDoBO
    {
        public int ItemCateId { get; set; }
        public string NameCate { get; set; }
        //public List<md_Articles> md_Articles { get; set; }
        public int CountArticle { get; set; }
    }


}

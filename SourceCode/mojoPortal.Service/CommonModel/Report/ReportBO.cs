using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.Report
{
    public class ReportBO
    {
        public List<string> ListCategory { get; set; }
        public List<ColumnBO> ListColumn { get; set; }
        public int ItemID { get; set; }
        public string Title { get; set; }
    }
    public class ColumnBO
    {
        public string name { get; set; }
        public string color { get; set; }
        public List<decimal?> data { get; set; }
        public double y { get; set; }
        public string type { get; set; }
        public bool visible { get; set; }
    }

}

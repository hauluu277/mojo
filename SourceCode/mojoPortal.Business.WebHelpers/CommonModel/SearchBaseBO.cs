using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Business.WebHelpers.CommonModel
{
    public class SearchBaseBO
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortQuery { get; set; }
    }
}

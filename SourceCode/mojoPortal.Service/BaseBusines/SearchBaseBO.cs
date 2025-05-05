using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoportal.Service.BaseBusines
{
    public class SearchBaseBO
    {
        public string sortQuery { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 20;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Business.WebHelpers.CommonModel
{
    public class PageListResultBO<T> where T : class
    {
        public List<T> ListData { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
    }
}

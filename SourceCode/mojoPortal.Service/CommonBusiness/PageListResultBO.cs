using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoportal.Service.CommonBusiness
{
    public class PageListResultBO<T> where T : class
    {
        public List<T> ListItem { get; set; }
        public int Count { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}

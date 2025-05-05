using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Business.WebHelpers.CommonModel
{
    public class DataReaderBO
    {
        public IDataReader DataReader { get; set; }
        public int Count { get; set; }
        public int TotalPage { get; set; }
    }
}

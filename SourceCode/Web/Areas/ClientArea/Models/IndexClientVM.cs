using DocumentFormat.OpenXml.Office2010.ExcelAc;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.client;
using System.Collections.Generic;

namespace mojoPortal.Web.Areas.ClientArea.Models
{
    public class IndexClientVM
    {
        public PageListResultBO<ClientBO> ListData { get; set; }
        public List<GroupClientBO> listWithGroup { get;set; }
        public int colunm { get; set; }
        public int group { get; set; }
    }
}
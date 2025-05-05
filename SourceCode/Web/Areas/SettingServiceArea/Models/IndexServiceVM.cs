using mojoPortal.Model.Data;
using System.Collections.Generic;

namespace mojoPortal.Web.Areas.SettingServiceArea.Models
{
    public class IndexServiceVM
    {
        public mp_Sites Site { get; set; }
        public List<core_SettingService> ListSetting { get; set; }
    }
}
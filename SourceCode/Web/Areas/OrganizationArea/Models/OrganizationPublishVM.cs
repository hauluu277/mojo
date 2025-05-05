using mojoPortal.Model.Data;
using System.Collections.Generic;

namespace mojoPortal.Web.Areas.OrganizationArea.Models
{
    public class OrganizationPublishVM
    {
        public core_CCTC core_CCTC { get; set; }
        public List<core_CCTC_Leader> ListLeader { get; set; }
        public List<core_CCTC_Department> ListDepartment { get; set; }
    }
}
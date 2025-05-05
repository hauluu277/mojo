using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonBusiness
{
    public class TokenDto
    {
        //access_token từ API AD Tổng cục trả về 
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int? expires_in { get; set; }
        public string refresh_token { get; set; }
        public string apikey { get; set; }
        public string username { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_expired { get; set; }

        public string clientURL { get; set; }
        public string clientId { get; set; }
    }
}

using mojoPortal.Features.Business.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    class mojoPortalLinq : mojoPortalDataContext
    {
        public mojoPortalLinq()
            : base(global::System.Configuration.ConfigurationManager.AppSettings["MSSQLConnectionString"])
        { }
        public mojoPortalLinq(string connectionString)
            : base(connectionString)
        { }
    }
}

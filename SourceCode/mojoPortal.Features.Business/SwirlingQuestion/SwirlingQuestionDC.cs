using mojoPortal.Features.Business.SwirlingQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwirlingQuestionFeature.Business
{
    class SwirlingQuestionDC : DataClassesDataContext
    {
        public SwirlingQuestionDC()
            : base(global::System.Configuration.ConfigurationManager.AppSettings["MSSQLConnectionString"].ToString())
        { }
    }
}

using mojoPortal.Model.Data;
using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mojoportal.Service.UoW;

namespace mojoPortal.Service.Business
{
    public class CategoryUserArticleBusiness : BaseBusiness<core_CategoryUserArticle>
    {
        public CategoryUserArticleBusiness(UnitOfWork unitOfWork) : base(unitOfWork) { }


    }
}

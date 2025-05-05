using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.Business
{
    public class ArticleCategoryBusiness  : BaseBusiness<md_ArticleCategory>
    {
        public ArticleCategoryBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


    }
}

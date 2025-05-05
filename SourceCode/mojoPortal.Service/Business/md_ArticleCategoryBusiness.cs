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
    public class md_ArticleCategoryBusiness : BaseBusiness<md_ArticleCategory>
    {
        public md_ArticleCategoryBusiness(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}

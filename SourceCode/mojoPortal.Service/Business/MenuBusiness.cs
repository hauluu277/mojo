using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Linq;

namespace mojoPortal.Service.Business
{
    public class MenuBusiness : BaseBusiness<core_Menu>
    {
        public MenuBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public int CountByParent(int parentId)
        {
            return this.context.core_Menu.Where(x => x.ParentID == parentId).Count();
        }
    }
}

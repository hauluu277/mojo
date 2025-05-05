using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Linq;

namespace mojoPortal.Service.Business
{
    public class core_TokenBusiness : BaseBusiness<core_Token>
    {
        public core_TokenBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public core_Token GetOneDESC(string clientId)
        {
            var data = repository.GetAllAsQueryable().Where(x => x.ClientID == clientId).OrderByDescending(x => x.ItemID).FirstOrDefault();
            return data;
        }

        public core_Token GetItem(string clientId, string userName)
        {
            var data = repository.GetAllAsQueryable().Where(x => x.ClientID == clientId && x.UserName == userName).OrderByDescending(x => x.ItemID).FirstOrDefault();
            return data;
        }
    }
}

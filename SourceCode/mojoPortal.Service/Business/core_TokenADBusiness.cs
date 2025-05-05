using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Linq;

namespace mojoPortal.Service.Business
{
    public class core_TokenADBusiness : BaseBusiness<core_TokenAD>
    {
        public core_TokenADBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public core_TokenAD GetOneDESC(string clientId)
        {
            var data = repository.GetAllAsQueryable().Where(x => x.clientId == clientId).OrderByDescending(x => x.ItemId).FirstOrDefault();
            return data;
        }

        public core_TokenAD GetByToken(string token)
        {
            var data = repository.GetAllAsQueryable().Where(x => x.access_token == token).FirstOrDefault();
            return data;
        }


        //public core_Token GetItem(string clientId, string userName)
        //{
        //    var data = repository.GetAllAsQueryable().Where(x => x.ClientID == clientId && x.UserName == userName).OrderByDescending(x => x.ItemID).FirstOrDefault();
        //    return data;
        //}
    }
}

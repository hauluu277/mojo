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
    public class core_ClientCategoryBusiness : BaseBusiness<core_ClientCategory>
    {

        public core_ClientCategoryBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public core_ClientCategory ByClientDanhMucClient(int IdClient, int IdDamhMucClient)
        {
            return this.GetAllAsQueryable().Where(x => x.ClientId == IdClient && x.CategoryClientId == IdDamhMucClient).FirstOrDefault();
        }

        public string GetNameCategory(int IdClient)
        {
            var data = (from clientCategory in context.Core_ClientCategories
                        .Where(x => x.ClientId == IdClient)
                        join category in context.core_Category
                        on clientCategory.CategoryId equals category.ItemID
                        select category.Name
                      ).ToArray();
            return string.Join(", ", data);
        }

        public core_ClientCategory GetByIdItem(int ItemId)
        {
            return this.GetAllAsQueryable().Where(x => x.ItemID == ItemId).FirstOrDefault();
        }



    }
}

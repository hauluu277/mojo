using mojoPortal.Model.Entities;
using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mojoPortal.Model.Data;
using mojoportal.Service.UoW;

namespace mojoPortal.Service.Business
{
    public class ArticlesBusiness : BaseBusiness<md_Articles>
    {
        public ArticlesBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public List<md_Articles> GetByIdBaiVetClient(int IdCategory)
        {
            return this.GetAllAsQueryable().Where(x => x.CategoryID == IdCategory).ToList();
        }

        public List<md_Articles> GetByIdBaiVetClientKeyWord(int IdCategory, string keyWord)
        {
            return this.GetAllAsQueryable().Where(x => x.CategoryID == IdCategory 
            && (string.IsNullOrEmpty(keyWord) || x.Title.ToLower().Contains(keyWord.ToLower())
            || x.TitleFTS.ToLower().Contains(keyWord.ToLower()))).ToList();
        }
    }
}

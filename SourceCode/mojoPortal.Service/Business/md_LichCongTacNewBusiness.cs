using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.Business
{
    public class md_LichCongTacNewBusiness : BaseBusiness<md_LichCongTacNew>
    {
        public md_LichCongTacNewBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public List<IndexDTO> getDataAll()
        {
            var result = new List<IndexDTO>();


            var data = this.GetAllAsQueryable().Select(x => new IndexDTO1
            {
                Nam = x.Nam.ToString(),
                Tuan = x.Week.ToString(),
                Ngay = (DateTime)x.StartDate,
                Thu = x.Thu,
                ThoiGian = x.ThoiGian,
                NoiDung = x.NoiDung,
                DiaDiem = x.DiaDiem,
                ThanhPhanThamDu = x.ThanhPhanThamDu,
            }).GroupBy(x => new { x.Nam, x.Tuan, x.Thu })
            .Select(y => new IndexDTO {
                Nam = y.Key.Nam,
                Tuan = y.Key.Tuan,
                Thu = y.Key.Thu,
                listdata = y.ToList()
            }).ToList();



            return result;
        }
    }
}

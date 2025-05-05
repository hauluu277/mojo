using mojoPortal.Service.CommonModel.LichCongTac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.LichLamViec
{
    public class ShowLichCongTacIndexDto
    {
        public List<InforUser> ListUser { get; set; }
        public int CountUser => ListUser.Count();
        public List<InforNgayTrongTuan> NgayTrongTuan { get; set; }
        public List<LichLamViecDto> ListInforLich { get; set; }
        public DateTime sDate { get; set; }
        public DateTime eDate { get; set; }

    }
    public class InforUser
    {
        public long Id { get; set; }
        public string ChucVu { get; set; }
        public string Name { get; set; }
    }

    public class InforNgayTrongTuan
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string TenHienThi { get; set; }
        public DateTime dateTime { get; set; }
    }

    public class InforLich
    {
        public long IdUser { get; set; }
        public string Name { get; set; }
        public string ChucVu { get; set; }
        public DateTime NgayLamViec { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public string GhiChu { get; set; }
    }
}

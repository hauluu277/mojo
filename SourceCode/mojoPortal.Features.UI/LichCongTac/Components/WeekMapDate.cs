using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LichCongTacFeature.UI
{
    public class WeekMapDate
    {
        public int Week { get; set; }
        public string NameWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class YearMapDate
    {
        public int Year { get; set; }
        public string NameYear { get; set; }
    }
}
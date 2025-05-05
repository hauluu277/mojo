using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Business.WebHelpers
{
    public static class WeeksExtension
    {
        public static List<Week> GetWeeksOfYear(DateTime jan1)
        {
            //beware different cultures, see other answers
            DateTime startOfFirstWeek = jan1.AddDays(1 - (int)(jan1.DayOfWeek));
            var weeks =
                Enumerable
                    .Range(0, 54)
                    .Select(i => new
                    {
                        weekStart = startOfFirstWeek.AddDays(i * 7)
                    })
                    .TakeWhile(x => x.weekStart.Year <= jan1.Year)
                    .Select(x => new
                    {
                        x.weekStart,
                        weekFinish = x.weekStart.AddDays(6)
                    })
                    .SkipWhile(x => x.weekFinish.Year < jan1.Year)
                    .Select((x, i) => new Week()
                    {
                        WeekStart = x.weekStart,
                        WeekFinish = x.weekFinish,
                        weekNum = i + 1
                    }).ToList();
            return weeks;
        }

        public static Tuple<DateTime, DateTime> GetStartEndWeek(this DateTime dt)
        {
            var lstDay = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            var sDay = dt.AddDays(-1 * lstDay.IndexOf(dt.DayOfWeek));
            var sDay_New = new DateTime(sDay.Year, sDay.Month, sDay.Day, 0, 0, 0);
            var eDay = sDay_New.AddDays(6);
            return new Tuple<DateTime, DateTime>(sDay_New, eDay);
        }

        public static DateTime GetDayOfWeek(DateTime dt, DayOfWeek dayOfWeek)
        {
            var day = dt.AddDays(dayOfWeek - dt.DayOfWeek);
            return day;
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime GetNgayHoc(DateTime dt, string NgayHoc)
        {
            DateTime dt_NgayHoc = new DateTime();
            var dayOfWeek = dt.DayOfWeek;
            var monDay = dt.AddDays(DayOfWeek.Monday - dayOfWeek);
            switch (NgayHoc)
            {
                case "THU2":
                    dt_NgayHoc = monDay;
                    break;
                case "THU3":
                    dt_NgayHoc = monDay.AddDays(1);
                    break;
                case "THU4":
                    dt_NgayHoc = monDay.AddDays(2);
                    break;
                case "THU5":
                    dt_NgayHoc = monDay.AddDays(3);
                    break;
                case "THU6":
                    dt_NgayHoc = monDay.AddDays(4);
                    break;
                case "THU7":
                    dt_NgayHoc = monDay.AddDays(5);
                    break;
                case "THU8":
                    dt_NgayHoc = monDay.AddDays(6);
                    break;
            }
            return dt_NgayHoc;
        }

        public static DayOfWeek GetDayOfWeek(string NgayHoc)
        {
            var data = new DayOfWeek();
            switch (NgayHoc)
            {
                case "THU2":
                    data = DayOfWeek.Monday;
                    break;
                case "THU3":
                    data = DayOfWeek.Tuesday;
                    break;
                case "THU4":
                    data = DayOfWeek.Wednesday;
                    break;
                case "THU5":
                    data = DayOfWeek.Thursday;
                    break;
                case "THU6":
                    data = DayOfWeek.Friday;
                    break;
                case "THU7":
                    data = DayOfWeek.Saturday;
                    break;
                case "THU8":
                    data = DayOfWeek.Sunday;
                    break;
            }
            return data;
        }

        public static int GetWeekNo(DateTime dt)
        {
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN");
            System.Globalization.Calendar cal = culture.Calendar;
            int iWeekNo = cal.GetWeekOfYear(dt, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
            return iWeekNo;
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public class Week
        {
            public DateTime WeekStart { get; set; }
            public DateTime WeekFinish { get; set; }
            public int weekNum { get; set; }
        }
    }
}

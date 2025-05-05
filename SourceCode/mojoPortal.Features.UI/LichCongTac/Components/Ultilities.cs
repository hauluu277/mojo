using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace LichCongTacFeature.UI
{
    public class Ultilities
    {
        public static List<WeekMapDate> GetWeeksByYear(int iYear)
        {
            //first
            int countDays = 0;
            for (int n = 1; n <= 12; n++)
            {
                countDays += DateTime.DaysInMonth(iYear, n);
            }
            List<WeekMapDate> arrWeeks = new List<WeekMapDate>();
            var j = 0;
            int k = 1;
            int totalDay = DateTime.DaysInMonth(iYear, 1);
            //DateTime.Now.StartWeek(Monday);
            for (int i = 1; i <= countDays; i = i + 7)
            {
                j++;
                WeekMapDate mapdate = new WeekMapDate();
                var startofweek = FirstDateOfWeek(iYear, j, CultureInfo.CurrentCulture);
                mapdate.Week = j;
                mapdate.NameWeek = "Tuần " + j;
                mapdate.StartDate = startofweek;
                mapdate.EndDate = mapdate.StartDate.AddDays(6);
                arrWeeks.Add(mapdate);
            }
            //zero-based array
            return arrWeeks;
        }
        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            var days = (weekOfYear - 1) * 7;

            var daycurrent = jan1.AddDays(days);
            DayOfWeek dow = daycurrent.DayOfWeek;
            var number = (int)dow;
            DateTime startDateOfWork = daycurrent.AddDays(-(number - 1));

            return startDateOfWork;
            //int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            //DateTime firstWeekDay = jan1.AddDays(daysOffset);
            //DateTime firstWeekDay = jan1;
            //int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            //Nếu là tuần đầu tiên của năm
            //if (weekOfYear == 1)
            //{
            //    return firstWeekDay;
            //}
            //if (firstWeek <= 1 || firstWeek > 50)
            //{
            //    weekOfYear = weekOfYear - 1;
            //}
            //return firstWeekDay.AddDays((weekOfYear - 1) * 7);
            //return firstWeekDay.AddDays(weekOfYear * 7);
        }
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static int WeekOfYearISO8601(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static DateTime GetStartOfWeek(int year, int month, int weekofmonth)
        {
            //lấy ngày bắt đầu của tuần trong tháng
            var day = weekofmonth * 7 - 6;
            var StartDate = new DateTime(year, month, day);
            var weekOfYear = GetIso8601WeekOfYear(StartDate);
            return FirstDateOfWeek(year, weekOfYear, CultureInfo.CurrentCulture);
        }

        public static DateTime GetEndOfWeek(DateTime startOfWeek)
        {
            return startOfWeek.AddDays(6);
        }
        public static WeekMapDate GetStartAndEndOfWeek(int iWeek, int iYear)
        {
            DateTime startOfYear = new DateTime(iYear, 1, 1);
            WeekMapDate mapdate = new WeekMapDate();
            mapdate.Week = iWeek;
            mapdate.StartDate = startOfYear.AddDays((iWeek - 1) * 7);
            mapdate.EndDate = mapdate.StartDate.AddDays(7);
            return mapdate;
        }
    }
}
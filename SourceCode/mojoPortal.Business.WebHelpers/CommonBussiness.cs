using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace mojoPortal.Business.WebHelpers
{
    public static class CommonBussiness
    {
        public static string CompareCurrentDate(this DateTime dateImportant)
        {
            var result = string.Empty;
            var currentDate = DateTime.Now;
            var formatDate = dateImportant.ToString("MMMM dd, yyyy",new CultureInfo("en-US"));
            if (dateImportant.Year < currentDate.Year)
            {
                return string.Format("<span class='date_line-throug dateformat'>{0}</span>", formatDate);
            }

            if (dateImportant.Year > currentDate.Year)
            {
                return string.Format("<span class='date_color-red dateformat'>{0}</span>", formatDate);
            }

            if (dateImportant.Month < currentDate.Month && (currentDate.Day - dateImportant.Day) > 27)
            {
                var totalDayOfMonthArticle = (System.DateTime.DaysInMonth(dateImportant.Year, dateImportant.Month) - dateImportant.Day);
                if (((totalDayOfMonthArticle + currentDate.Day) > 4) && (currentDate.Month - dateImportant.Month) > 0)
                {
                    return string.Format("<span class='date_line-throug dateformat'>{0}</span>", formatDate);
                }
            }

            if (dateImportant.Month > currentDate.Month && (currentDate.Day - dateImportant.Day) < 27)
            {
                var totalDayOfMonthArticle = (System.DateTime.DaysInMonth(dateImportant.Year, dateImportant.Month) - dateImportant.Day);
                if (((totalDayOfMonthArticle + currentDate.Day) < 4) && (currentDate.Month - dateImportant.Month) < 0)
                {
                    return string.Format("<span class='date_color-red dateformat'>{0}</span>", formatDate);
                }
            }

            if (dateImportant.Day < currentDate.Day && (currentDate.Day - dateImportant.Day) > 0)
            {
                return string.Format("<span class='date_line-throug dateformat'>{0}</span>", formatDate);
            }
            if (dateImportant.Day > currentDate.Day && (currentDate.Day - dateImportant.Day) < 0)
            {
                return string.Format("<span class='date_color-red dateformat'>{0}</span>", formatDate);
            }

            if (dateImportant.Day < currentDate.Day && dateImportant.Hour > currentDate.Hour)
            {
                var hourseLastDay = (24 - dateImportant.Hour) + currentDate.Hour;
                return string.Format("<span class='date_line-throug dateformat'>{0}</span>", formatDate);
            }
            if (dateImportant.Day > currentDate.Day && dateImportant.Hour < currentDate.Hour)
            {
                var hourseLastDay = (24 - dateImportant.Hour) + currentDate.Hour;
                return string.Format("<span class='date_color-red dateformat'>{0}</span>", formatDate);
            }

            if (dateImportant.Day == currentDate.Day && dateImportant.Hour < currentDate.Hour)
            {
                return string.Format("<span class='date_line-throug dateformat'>{0}</span>", formatDate);
            }

            if (dateImportant.Day == currentDate.Day && dateImportant.Hour > currentDate.Hour)
            {
                return string.Format("<span class='date_color-red dateformat'>{0}</span>", formatDate);
            }
            // tính phút
            if (dateImportant.Minute < currentDate.Minute)
            {
                return string.Format("<span class='date_line-throug dateformat'>{0}</span>", dateImportant);
            }

            if (dateImportant.Minute > currentDate.Minute)
            {
                return string.Format("<span class='date_color-red dateformat'>{0}</span>", formatDate);
            }
            return string.Format("<span class='date_color-red dateformat'>{0}</span>", formatDate);
        }

    }
}

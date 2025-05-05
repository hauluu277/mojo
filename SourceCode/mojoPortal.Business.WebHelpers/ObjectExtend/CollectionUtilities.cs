using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonHelper.ObjectExtend
{
    public static class CollectionUtilities
    {
        /// <summary>
        /// @author:duynn
        /// @description: chuyển FormCollection sang Dictionary
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this FormCollection collection)
        {
            var result = collection.AllKeys.ToDictionary(key => key, value => collection[value]);
            return result;
        }

        public static IEnumerable<SelectListItem> GetDropDown<T>(this IEnumerable<T> source, string displayMember, string valueMember, object selected = null)
        {
            Type objType = typeof(T);
            IEnumerable<SelectListItem> result = new List<SelectListItem>();
            if (string.IsNullOrEmpty(displayMember) == false && string.IsNullOrEmpty(valueMember) == false)
            {
                result = source.Select(x => new SelectListItem()
                {
                    Value = objType.GetProperty(valueMember).GetValue(x).ToString(),
                    Text = objType.GetProperty(displayMember).GetValue(x).ToString(),
                    Selected = (selected != null) && selected.Equals(objType.GetProperty(valueMember).GetValue(x))
                }).OrderBy(x => x.Text);
            }
            return result;
        }

        public static IEnumerable<SelectListItem> GetDropDownMutiple<T>(this IEnumerable<T> source, string displayMember, string valueMember, object[] selected = null)
        {
            Type objType = typeof(T);
            IEnumerable<SelectListItem> result = new List<SelectListItem>();
            if (string.IsNullOrEmpty(displayMember) == false && string.IsNullOrEmpty(valueMember) == false)
            {
                result = source.Select(x => new SelectListItem()
                {
                    Value = objType.GetProperty(valueMember).GetValue(x).ToString(),
                    Text = objType.GetProperty(displayMember).GetValue(x).ToString(),
                    Selected = (selected != null) && selected.Contains(objType.GetProperty(valueMember).GetValue(x))
                }).OrderBy(x => x.Text);
            }
            return result;
        }

        ///// <summary>
        ///// @author:duynn
        ///// @description: trả về một đối tượng mới nếu kết quả trả về bị null
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //public static T FirstOrEmpty<T>(this IQueryable<T> source)
        //{
        //    var result = source.FirstOrDefault();
        //    if (result == null)
        //    {
        //        result = (T)Activator.CreateInstance(typeof(T));
        //    }
        //    return result;
        //}

        public static TSource FirstOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || source.Any() == false)
            {
                return (TSource)Activator.CreateInstance(typeof(TSource));
            }
            else
            {
                return source.First();
            }
        }

        /// <summary>
        /// @author:duynn
        /// @description: trả về chuỗi số phân cách bằng dấu phẩy
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToCommaSeperatedString<T>(this IEnumerable<T> source) where T : IComparable, IFormattable, IConvertible
        {
            if (source != null)
            {
                var result = string.Join(",", source.ToArray());
                return result;
            }
            return string.Empty;
        }

        public static List<T> ToSingleList<T>(this T item)
        {
            return new List<T>() { item };
        }

        public static List<SelectListItem> GetListHours(int selected = 0)
        {
            var result = Enumerable.Range(0, 24).Select(x => new SelectListItem()
            {
                Value = x.ToString(),
                Text = x.ToString("D2"),
                Selected = x == selected
            }).ToList();
            return result;
        }

        public static List<SelectListItem> GetListMinutes(int selected = 0)
        {
            var result = Enumerable.Range(0, 60).Select(x => new SelectListItem()
            {
                Value = x.ToString(),
                Text = x.ToString("D2"),
                Selected = x == selected
            }).ToList();
            return result;
        }
    }
}

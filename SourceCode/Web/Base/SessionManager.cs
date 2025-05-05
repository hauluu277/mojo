using dotless.Core.Parser.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web
{
    public class SessionManager
    {
        public const string USER_INFO = "UserInfo";
        /// <summary>
        /// @author:duynn
        /// @description: thống kê số lượng
        /// </summary>
        public const string STATISTIC_NUMBER = "STATISTIC_NUMBER";
        public const string STATISTIC_AUTHORIZE_NUMBER = "STATISTIC_AUTHORIZE_NUMBER";

        public const string LIST_PERMISSTION = "LIST_PERMISSTION";

        public static void SetValue(string Key, object Value)
        {
            HttpContext context = HttpContext.Current;
            context.Session[Key] = Value;
        }

        public static void ResetValue(string Key)
        {
            HttpContext context = HttpContext.Current;
            context.Session[Key] = null;
        }

        public static T GetValue<T>(string key)
        {
            HttpContext context = HttpContext.Current;
            var data = (T)context.Session[key];
            if (data == null)
            {
                data = (T)Activator.CreateInstance(typeof(T));
            }
            return data;

        }


        public static object GetValue(string Key)
        {
            HttpContext context = HttpContext.Current;
            return context.Session[Key];
        }

        public static void Remove(string Key)
        {
            HttpContext context = HttpContext.Current;
            context.Session.Remove(Key);
        }

        public static void Clear()
        {
            HttpContext context = HttpContext.Current;
            context.Session.RemoveAll();
        }

        public static bool HasValue(string Key)
        {
            HttpContext context = HttpContext.Current;
            return context.Session[Key] != null;
        }

        public static object GetUserInfo()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[USER_INFO];
        }

        public static object GetListPermistion()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[LIST_PERMISSTION];
        }

        /// <summary>
        /// @author:duynn
        /// @since: 25/07/2019
        /// @description: lấy danh sách số lượng
        /// </summary>
        /// <returns></returns>
        public static object GetStatisticNumber()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[STATISTIC_NUMBER];
        }

        /// <summary>
        /// @author:duynn
        /// @since: 20/08/2019
        /// @description: danh sách ủy quyền
        /// </summary>
        /// <returns></returns>
        public static object GetStatisticAuthorizeNumber()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[STATISTIC_AUTHORIZE_NUMBER];
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mojoportal.CoreHelpers
{
    public class MaperData
    {

        /// <summary>
        /// Maper dữ liệu cho 2 object truyền vào
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="source"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TSource Map<TSource, TIn>(TSource source, TIn input) where TSource : class where TIn : class
        {
            if (source == null || input == null) return source;
            var sourceType = source.GetType();
            var inputType = input.GetType();
            foreach (PropertyInfo propertySource in sourceType.GetProperties())
            {
                //nếu propertySource != null & có  thể đọc 
                // nếu propertySource và propertySource có thể ghi dữ liệu

                PropertyInfo inputInfo = (propertySource != null && propertySource.CanWrite)
                    ? inputType.GetProperty(propertySource.Name, propertySource.PropertyType) : null;

                //nếu inputInfo tồn tại & inputInfo có thể đọc dữ liệu
                //nếu kiểu dữ liệu propertySource và inputInfo giống nhau

                if (inputInfo != null && inputInfo.CanRead && propertySource.PropertyType.Equals(inputInfo.PropertyType))
                {
                    //set dữ liệu từ input sang source
                    var valueInput = inputInfo.GetValue(input, null);
                    if (valueInput != null)
                    {
                        propertySource.SetValue(source, valueInput);
                    }
                }
            }
            return source;
        }

        /// <summary>
        /// Maper dữ liệu cho 2 object truyền vào
        /// Cho phép maper cả dữ liệu null
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="source"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TSource MapAllowNull<TSource, TIn>(TSource source, TIn input) where TSource : class where TIn : class
        {
            if (source == null || input == null) return source;
            var sourceType = source.GetType();
            var inputType = input.GetType();
            foreach (PropertyInfo propertySource in sourceType.GetProperties())
            {
                //nếu propertySource != null & có  thể đọc 
                // nếu propertySource và propertySource có thể ghi dữ liệu

                PropertyInfo inputInfo = (propertySource != null && propertySource.CanWrite)
                    ? inputType.GetProperty(propertySource.Name, propertySource.PropertyType) : null;

                //nếu inputInfo tồn tại & inputInfo có thể đọc dữ liệu
                //nếu kiểu dữ liệu propertySource và inputInfo giống nhau

                if (inputInfo != null && inputInfo.CanRead && propertySource.PropertyType.Equals(inputInfo.PropertyType))
                {
                    //set dữ liệu từ input sang source
                    var valueInput = inputInfo.GetValue(input, null);
                    propertySource.SetValue(source, valueInput);
                }
            }
            return source;
        }
    }
}

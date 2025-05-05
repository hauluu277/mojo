using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Business.WebHelpers
{
    public class GenericData<T> where T : class
    {
        public static T CloneData(T data)
        {
            var result = (T)Activator.CreateInstance(typeof(T));
            var obj = data.GetType().GetProperties();
            foreach (PropertyInfo prop in obj)
            {
                var value = prop.GetValue(data);
                var firstData = result.GetType().GetProperties()
                      .Where(x => x.Name == prop.Name).FirstOrDefault();
                if (firstData != null && firstData.CanWrite)
                {
                    firstData.SetValue(result, value, null);
                }
            }
            return result;
        }
    }
}

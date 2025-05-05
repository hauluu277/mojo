using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mojoportal.CoreHelpers
{
    public class GenericData<T>
    {
        public static T GetDataOrDefault(object obj, T dataDefault)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                return (T)obj;
            }
            return dataDefault;
        }

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
